// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/CurvedWorldTonnyBasic" 
{
	Properties 
	{
	  	_MainTex ("Base (RGB)", 2D) = "white" {}
	  	_QOffset ("Offset", Vector) = (0,0,0,0)
	  	_Dist ("Distance", Float) = 100.0
	  	_Color("Color", COLOR) = (1,1,1,1)

	  	_ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { }
	}

	SubShader 
	{
		Tags { "RenderType"="Opaque" }

		Pass
		{
			Name "BASED"
			Cull Off
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog

			#include "UnityCG.cginc"
		
			sampler2D _MainTex;
			samplerCUBE _ToonShade;
			float4 _QOffset;
			float _Dist;
			uniform float4 _MainTex_ST;

			fixed4 _Color;

			struct appdata 
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f 
			{
	  			float4 pos : SV_POSITION;
	  			float2 texcoord : TEXCOORD0;
	  			float3 cubenormal : TEXCOORD1;
				UNITY_FOG_COORDS(2)
			};
		
			v2f vert (appdata v)
			{
	  			v2f o;
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

  				o.cubenormal = mul (UNITY_MATRIX_MV, float4(v.normal,0));
				UNITY_TRANSFER_FOG(o,o.pos);

			  	return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = _Color * tex2D(_MainTex, i.texcoord);
				fixed4 cube = texCUBE(_ToonShade, i.cubenormal);
				fixed4 c = fixed4(2.0f * cube.rgb * col.rgb, col.a);
				UNITY_APPLY_FOG(i.fogCoord, c);
				return c;
			}
	
			ENDCG

		}



	}

	Fallback "Toon/Lit"
}