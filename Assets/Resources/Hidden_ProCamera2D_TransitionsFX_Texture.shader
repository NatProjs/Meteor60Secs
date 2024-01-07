Shader "Hidden/ProCamera2D/TransitionsFX/Texture" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Step ("Step", Range(0, 1)) = 0
		_BackgroundColor ("Background Color", Vector) = (0,0,0,1)
		_TransitionTex ("Transition Texture", 2D) = "white" {}
		_Smoothing ("Smoothing", Range(0, 1)) = 0.1
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