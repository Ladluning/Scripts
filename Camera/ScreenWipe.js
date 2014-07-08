private var tex : Texture;
private var renderTex : RenderTexture;
private var tex2D : Texture2D;
private var alpha : float;
private var reEnableListener : boolean;
private var shapeMaterial : Material;
private var shape : Transform;
private var curve : AnimationCurve;
private var useCurve : boolean;
static var use : ScreenWipe;
enum ZoomType {Grow, Shrink}
enum TransitionType {Left, Right, Up, Down}

function Awake () 
{
	if (use) {
		Debug.LogWarning("Only one instance of ScreenCrossFadePro is allowed");
		return;
	}
	use = this;

	this.enabled = false;
}

function OnGUI () {
	if(GUI.color.a>0.001f)
	{
	GUI.depth = -9999999;
	GUI.color.a = alpha;
	GUI.DrawTexture(Rect(0, 0, Screen.width, Screen.height), tex);
	}
}

function AlphaTimer (time : float) {
	var rate = 1.0/time;
	
	if( useCurve ){
		var t : float = 0.0;
		while( t < 1.0 ){
			alpha = curve.Evaluate( t );
			t += Time.deltaTime * rate;
			yield;
		}
	}
	else{
		for (alpha = 1.0; alpha > 0.0; alpha -= Time.deltaTime * rate)
			yield;
	}
	
}

function CameraSetup (cam1 : Camera, cam2 : Camera, cam1Active : boolean, enableThis : boolean) {
	if (enableThis) {
		this.enabled = true;
	}
	for(var tmp :Camera in Camera.allCameras)
	{
		if(cam2!=tmp)
			tmp.enabled = cam1Active;
	}
	cam1.enabled = cam1Active;
	cam2.gameObject.active = true;
	var listener = cam2.GetComponent(AudioListener);
	if (listener) {
		reEnableListener = listener.enabled? true : false;
		listener.enabled = false;
	}
}

function CameraCleanup (cam1 : Camera, cam2 : Camera) {
	var listener = cam2.GetComponent(AudioListener);
	if (listener && reEnableListener) {
		listener.enabled = true;
	}
	for(var tmp :Camera in Camera.allCameras)
	{
		if(cam2!=tmp)
			tmp.enabled = false;
	}
	cam1.gameObject.active = false;
	this.enabled = false;
}

function CrossFade (cam1 : Camera, cam2 : Camera, time : float ) {
	yield CrossFade( cam1, cam2, time, null );
}
function CrossFade (cam1 : Camera, cam2 : Camera, time : float, _curve : AnimationCurve ) {
	curve = _curve;
	useCurve = curve != null;
	if (!tex2D) {
		tex2D = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
	}
	yield WaitForEndOfFrame();
	tex2D.ReadPixels(Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
	tex2D.Apply();
	tex = tex2D;
	yield;
	CameraSetup (cam1, cam2, false, true);

	yield AlphaTimer(time);

	CameraCleanup (cam1, cam2);
}

var planeResolution = 90;	// Higher numbers make the DreamWipe effect smoother, but take more CPU time
private var baseVertices : Vector3[];
private var newVertices : Vector3[];
private var planeMaterial : Material;
private var plane : GameObject;
private var renderTex2 : RenderTexture;

function InitializeDreamWipe () {
	if (planeMaterial && plane) return;
	
	planeMaterial = new Material (
		"Shader \"Unlit2Pass\" {" +
		"Properties {" +
		"	_Color (\"Main Color\", Color) = (1,1,1,1)" +
		"	_Tex1 (\"Base\", Rect) = \"white\" {}" +
		"	_Tex2 (\"Base\", Rect) = \"white\" {}" +
		"}" +
		"Category {" +
		"	ZWrite Off Alphatest Greater 0 ColorMask RGB Lighting Off" +
		"	Tags {\"Queue\"=\"Transparent\" \"IgnoreProjector\"=\"True\" \"RenderType\"=\"Transparent\"}" +
		"	Blend SrcAlpha OneMinusSrcAlpha" +
		"	SubShader {" +
		"		Pass {SetTexture [_Tex2]}" +
		"		Pass {SetTexture [_Tex1] {constantColor [_Color] Combine texture * constant, texture * constant}}" +
		"	}" +
		"}}"
	);
	
	// Set up plane object
	plane = new GameObject("Plane", MeshFilter, MeshRenderer);
	plane.renderer.material = planeMaterial;
	plane.renderer.castShadows = false;
	plane.renderer.receiveShadows = false;
	plane.renderer.enabled = false;

	// Create the mesh used for the distortion effect
	var planeMesh = new Mesh();
	(plane.GetComponent(MeshFilter) as MeshFilter).mesh = planeMesh;
	
	planeResolution = Mathf.Clamp(planeResolution, 1, 16380);
	baseVertices = new Vector3[4*planeResolution + 4];
	newVertices = new Vector3[baseVertices.Length];
	var newUV = new Vector2[baseVertices.Length];
	var newTriangles = new int[18*planeResolution];
	
	var idx = 0;
	for (i = 0; i <= planeResolution; i++) {
		var add : float = 1.0/planeResolution*i;
		newUV[idx] = Vector2(0.0, 1.0-add);
		baseVertices[idx++] = Vector3(-1.0, .5-add, 0.0);
		newUV[idx] = Vector2(0.0, 1.0-add);
		baseVertices[idx++] = Vector3(-.5, .5-add, 0.0);
		newUV[idx] = Vector2(1.0, 1.0-add);
		baseVertices[idx++] = Vector3(.5, .5-add, 0.0);
		newUV[idx] = Vector2(1.0, 1.0-add);
		baseVertices[idx++] = Vector3(1.0, .5-add, 0.0);
	}
	
	idx = 0;
	for (y = 0; y < planeResolution; y++) {
		for (x = 0; x < 3; x++) {
			newTriangles[idx++] = (y*4	  )+x;
			newTriangles[idx++] = (y*4	  )+x+1;
			newTriangles[idx++] = ((y+1)*4)+x;

			newTriangles[idx++] = ((y+1)*4)+x;
			newTriangles[idx++] = (y	*4)+x+1;
			newTriangles[idx++] = ((y+1)*4)+x+1;
		}
	}
	
	planeMesh.vertices = baseVertices;
	planeMesh.uv = newUV;
	planeMesh.triangles = newTriangles;
		
	// Set up rendertextures
	renderTex = new RenderTexture(Screen.width, Screen.height, 24);
	renderTex2 = new RenderTexture(Screen.width, Screen.height, 24);
	renderTex.filterMode = renderTex2.filterMode = FilterMode.Point;
	planeMaterial.SetTexture("_Tex1", renderTex);
	planeMaterial.SetTexture("_Tex2", renderTex2);
}

function DreamWipe (cam1 : Camera, cam2 : Camera, time : float) {
	yield DreamWipe (cam1, cam2, time, .07, 25.0);
}

function DreamWipe (cam1 : Camera, cam2 : Camera, time : float, waveScale : float, waveFrequency : float) {
	if (!plane) {
		InitializeDreamWipe();
	}
	waveScale = Mathf.Clamp(waveScale, -.5, .5);
	waveFrequency = .25/(planeResolution/waveFrequency);
	
	CameraSetup (cam1, cam2, true, false);

	// Make a camera that will show a plane with the combined rendertextures from cam1 and cam2,
	// and make it have the highest depth so it's always on top
	var cam2Clone : Camera = Instantiate(cam2, cam2.transform.position, cam2.transform.rotation);
	cam2Clone.depth = cam1.depth+1;
	if (cam2Clone.depth <= cam2.depth) {
		cam2Clone.depth = cam2.depth+1;
	}
	// Get screen coodinates of 0,0 in local spatial coordinates, so we know how big to scale the plane (make sure clip planes are reasonable)
	cam2Clone.nearClipPlane = .5;
	cam2Clone.farClipPlane = 1.0;
	var p = cam2Clone.transform.InverseTransformPoint(cam2.ScreenToWorldPoint(Vector3(0.0, 0.0, cam2Clone.nearClipPlane)));
	plane.transform.localScale = Vector3(-p.x*2.0, -p.y*2.0, 1.0);
	plane.transform.parent = cam2Clone.transform;
	plane.transform.localPosition = plane.transform.localEulerAngles = Vector3.zero;
	// Must be a tiny bit beyond the nearClipPlane, or it might not show up
	plane.transform.Translate(Vector3.forward * (cam2Clone.nearClipPlane+.00005));
	// Move the camera back so cam2 won't see the renderPlane, and parent it to cam2 so that if cam2 is moving, it won't see the plane
	cam2Clone.transform.Translate(Vector3.forward * -1.0);
	cam2Clone.transform.parent = cam2.transform;
		
	// Initialize some stuff
	plane.renderer.enabled = true;
	var scale = 0.0;
	var planeMesh = plane.GetComponent(MeshFilter).mesh;
	cam1.targetTexture = renderTex;
	cam2.targetTexture = renderTex2;

	// Do the cross-fade
	var rate = 1.0/time;
	for (i = 0.0; i < 1.0; i += Time.deltaTime * rate) {
		planeMaterial.color.a = Mathf.SmoothStep(0.0, 1.0, Mathf.InverseLerp(.75, .15, i));
		// Make plane undulate
		for (var j = 0; j < newVertices.Length; j++) {
			newVertices[j] = baseVertices[j];
			newVertices[j].x += Mathf.Sin(j*waveFrequency + i*time) * scale;
		}
		planeMesh.vertices = newVertices;
		scale = Mathf.Sin(Mathf.PI * Mathf.SmoothStep(0.0, 1.0, i)) * waveScale;
		yield;
	}
	
	// Clean up
	CameraCleanup (cam1, cam2);
	plane.renderer.enabled = false;
	plane.transform.parent = null;
	Destroy(cam2Clone.gameObject);
	cam1.targetTexture = cam2.targetTexture = null;
	renderTex.Release();
	renderTex2.Release();
}