Shader "Custom/Diffuse_Gray" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_GrayValue ("Value",float) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		float _GrayValue;
		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = ((c.r+c.g+c.b)*_GrayValue/3,(c.r+c.g+c.b)*_GrayValue/3,(c.r+c.g+c.b)*_GrayValue/3);
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
