Shader "JKH/JKH_CharacterShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MainTex2("메탈릭(a = 스무스니스)",2D) = "white" {}
		_Mtl("Metalic",Range(0,1)) = 0
		_Smooth("smoothness", Range(0,1)) = 0
		_BumpMap("노말맵", 2D) = "Bump" {}
		_Occlu("AO맵", 2D) = "white" {}
		_Rimpow ("림 두께", Range(1,30)) = 1
		_RimbalGGi("림세기", float) = 0
		_Eye("EyemaskMap", 2D) = "white" {}
		[HDR] _EyeCol("Eye색" , color) = (1,1,1,1)
		[HDR] _whiteEyeCol("흰자색" , color) = (1,1,1,1)
		[HDR] _bloodEyeCol("핏줄색" , color) = (0,0,0,1)


	}
	SubShader {
		Tags { "RenderType"="Opaque" }

		cull off

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _MainTex2;
		sampler2D _Occlu;
		sampler2D _BumpMap;
		sampler2D _Eye;
		
		 
		struct Input {
			float2 uv_MainTex;
			float2 uv_MainTex2;
			float2 uv_BumpMap;
			float2 uv_Eye;
			float3 viewDir;
			float3 lightDir;
		};

		float4 _Color;
		float _Mtl;
		float _Smooth;
		float _Rimpow;
		float _RimbalGGi;
		float4 _EyeCol;
		float4 _whiteEyeCol;
		float4 _bloodEyeCol;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			float4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			float4 met = tex2D (_MainTex2,IN.uv_MainTex2);
			float4 eyem = tex2D (_Eye,IN.uv_Eye);
			o.Normal = UnpackNormal(tex2D(_BumpMap,IN.uv_BumpMap));
			o.Occlusion = tex2D(_Occlu,IN.uv_MainTex);
			o.Albedo = (c.rgb * eyem.r * _EyeCol) + (c.rgb * eyem.g * _whiteEyeCol) + (c.rgb * (1 - (eyem.r + eyem.g)));
			float rim =  pow(1 - saturate(dot(IN.viewDir,o.Normal)), _Rimpow);
			o.Emission = rim * _RimbalGGi * c.rgb * (1 - eyem.b) + (eyem.b * c.rgb * _bloodEyeCol);
			o.Metallic = met.r * _Mtl;
			o.Smoothness = met.a * _Smooth;
			o.Alpha = c.a;

		}

		ENDCG
	}
	FallBack "Diffuse"
}
