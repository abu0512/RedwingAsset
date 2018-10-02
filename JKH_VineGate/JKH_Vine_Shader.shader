Shader "JKH/Vine_Shader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpMap("노말맵", 2D) = "Bump" {}
		_MaskTex ("MaskMap",2D) = "white" {}
		_Thinkness("얇기",float) = 0
		[HideinInspector]_CutOut("CutOut",float) = 0.5
		_growth("자라나라", float) = 0
		_Occulusion("오쿨루젼" , 2D) = "white"{}
		_SmoothMap("스무스니스맵", 2D) = "black" {}
		[HDR]_MaruckCol("마력색", color) = (1,1,1,1)
		[HDR]_RimCol("림컬러", color) = (1,1,1,1)
		_HlTex("흘러가는 마력", 2D) = "black" {}
		_DispMap("디스플레이스먼트맵", 2D) = "black"{} 
		_DisTk("디스플레이스먼트두께" ,float) = 0
		_Tess("테셀레이션", Range(1,32)) = 4
	}
	SubShader {
		Tags { "RenderType"="AlphatestCutOut" "Queue" = "AlphaTest" }
		LOD 200
		cull off
		
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alphatest:_CutOut vertex:vert addshadow tessellate:tessFixed
		#pragma target 5.0

		fixed4 _Color, _RimCol, _MaruckCol;
		float _growth ,_Thinkness, _DisTk;


		float _Tess;

		float4 tessFixed()
		{
			return _Tess;
		}

		sampler2D _MainTex;
		sampler2D _MaskTex;
		sampler2D _BumpMap;
		sampler2D _Occulsion;
		sampler2D _SmoothMap;
		sampler2D _HlTex;
		sampler2D _DispMap;

		struct Input {
			float2 uv_MainTex;
			float2 uv_MaskTex;
			float2 uv_BumpMap;
			float2 uv_DispMap;
			float2 uv_SmoothMap;
			float3 viewDir;
			float2 uv_HlTex;
		};



		void vert (inout appdata_full v)
		{
		float m = tex2Dlod(_MaskTex,float4(v.texcoord.x +_growth,v.texcoord.y,0,0)).r;
		float disp = tex2Dlod(_DispMap,float4(v.texcoord.x * 3 + _growth,v.texcoord.y * 3,0,0));
		v.vertex.xyz += v.normal * _Thinkness * 2 * (1-m) + (disp * v.normal * _DisTk);
		}


		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, float2(IN.uv_MainTex.x + _growth,IN.uv_MainTex.y)) * _Color;
			float M = tex2D (_MaskTex,float2(IN.uv_MaskTex.x +_growth, IN.uv_MainTex.y));
			o.Albedo = c.rgb;
			o.Occlusion = tex2D(_Occulsion,IN.uv_MainTex);
			o.Smoothness = tex2D(_SmoothMap,IN.uv_SmoothMap);
			o.Metallic = 0;
			o.Normal = UnpackNormal(tex2D(_BumpMap,float2(IN.uv_BumpMap.x + _growth,IN.uv_BumpMap.y)));
			o.Emission = tex2D(_HlTex,float2(IN.uv_HlTex.x + _growth + _Time.x,IN.uv_HlTex.y)).r * tex2D(_HlTex,float2(IN.uv_HlTex.x +  _growth + _Time.x,IN.uv_HlTex.y)).g * tex2D(_HlTex,float2(IN.uv_HlTex.x + _growth,IN.uv_HlTex.y)).b * 0.1 * _MaruckCol + (pow((1 - saturate(dot(o.Normal,IN.viewDir))), 5) * 0.0006) * _RimCol;
			o.Alpha = M.r;
		}
		ENDCG

	}
	FallBack "Diffuse"
}
