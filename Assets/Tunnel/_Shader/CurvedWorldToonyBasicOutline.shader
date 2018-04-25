// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/CurvedWorldTonnyBasicOutline" 
{
	Properties {
		_Color ("Main Color", Color) = (.5,.5,.5,1)
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Range (.002, 0.03)) = .005
		_MainTex ("Base (RGB)", 2D) = "white" { }
		_ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { }
		_QOffset ("Offset", Vector) = (0,0,0,0)
	  	_Dist ("Distance", Float) = 100.0

	}
	
	CGINCLUDE
	#include "UnityCG.cginc"
	
	struct appdata {
		float4 vertex : POSITION;
		float2 texcoord : TEXCOORD0;
		float3 normal : NORMAL;
	};

	struct v2f {
		float4 pos : SV_POSITION;
		float2 texcoord : TEXCOORD0;
		float3 cubenormal : TEXCOORD1;
	  	UNITY_FOG_COORDS(0)
		fixed4 color : COLOR;
	};
	
	uniform float _Outline;
	uniform float4 _OutlineColor;
	float _Dist;
	float4 _QOffset;
	uniform float4 _MainTex_ST;
	
	v2f vert(appdata v) {
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);

		UNITY_INITIALIZE_OUTPUT(v2f, o);



	// transform vertex position from object to world space
	  	float4 worldPos = mul(unity_ObjectToWorld, v.vertex); 

  		// Now adjust the coordinates to be relative to the camera position
	  	worldPos.xyz -= _WorldSpaceCameraPos.xyz;

		// do stuff to worldPos.xyz
		float zOff = worldPos.z/_Dist;
		worldPos += _QOffset*zOff*zOff;

		worldPos.xyz += _WorldSpaceCameraPos.xyz;

		// transform updated position back to object space
		float4 objectPos = mul(unity_WorldToObject, worldPos); 

		// usual transform from object to projection space
		o.pos = UnityObjectToClipPos(objectPos); 
		o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

//  	o.cubenormal = mul (UNITY_MATRIX_MV, float4(v.normal,0));
//		UNITY_TRANSFER_FOG(o,o.pos);





		float3 norm   = normalize(mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal));
		float2 offset = TransformViewToProjection(norm.xy);

		o.pos.xy += offset * o.pos.z * _Outline;
		o.color = _OutlineColor;
		UNITY_TRANSFER_FOG(o,o.pos);
		return o;
	}
	ENDCG

	SubShader {
		Tags { "RenderType"="Opaque" }
		UsePass "Custom/CurvedWorldTonnyBasic/BASED"
		Pass {
			Name "OUTLINE"
			Tags { "LightMode" = "Always" }
			Cull Front
			ZWrite On
//			ColorMask RGB
//			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			fixed4 frag(v2f i) : SV_Target
			{
				UNITY_APPLY_FOG(i.fogCoord, i.color);
				return i.color;
			}
			ENDCG
		}
	}
	
	Fallback "Toon/Basic"
}
