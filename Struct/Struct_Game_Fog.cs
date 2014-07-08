using UnityEngine;
using System.Collections;

[System.Serializable]
public class Struct_Game_Fog {

	public bool IsFog = false;
	public Color FogColor = Color.white;
	public float FogDensity = 0;
	public float FogStartPos = 0;
	public float FogEndPos = 0;
	public float CameraCullFar = 0;
}
