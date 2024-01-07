Shader "Hidden/ProCamera2D/TransitionsFX/Blinds" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Step ("Step", Range(0, 1)) = 0
		_BackgroundColor ("Background Color", Vector) = (0,0,0,1)
		_Direction ("Direction", Float) = 0
		_Blinds ("Blinds", Float) = 2
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}