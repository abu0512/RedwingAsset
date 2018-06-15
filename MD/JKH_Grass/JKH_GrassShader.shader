Shader "Custom/JKH_GrassShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		[HideInInspector]_cutout("알파테스트" ,float) = 0.41
	}
	SubShader {
		Tags { "RenderType"="TransparentCutout" "Queue" = "AlphaTest" }
		LOD 200
		cull off
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows vertex:vert alphatest:_cutout
		#pragma target 3.0

		  
		void vert (inout appdata_full v)
		{
		v.vertex.z += ((v.texcoord.y * sin(v.texcoord.y + _Time.y)) * 0.15) * sin((v.texcoord.x * 2 -1) * 2 + _Time.y * 1) * cos((v.texcoord.x * 2 -1) + _Time.y * 2);
		v.vertex.x += ((v.texcoord.y * cos(v.texcoord.y + _Time.y)) * 0.1) * cos((v.texcoord.x * 2 -1) + _Time.y * 3) * sin((v.texcoord.x * 2 -1) + _Time.y * 1);
		}


		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 vertex;
			
		};

		half _Glossiness;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
