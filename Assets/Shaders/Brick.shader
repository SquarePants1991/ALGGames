// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/Brick"
{
	Properties
	{
		_MainColor ("MainColor", Color) = (1.0, 0.0, 0.0, 1.0)
		_BorderColor ("BorderColor", Color) = (0.0, 0.0, 0.0, 1.0)
		_BorderWidth ("BorderWidth", Float) = 0.05
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			Cull Front

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			

			#include "UnityCG.cginc"

			struct appdata 
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			fixed4 _BorderColor;
			float _BorderWidth;
			float4 _MainTex_ST;
			
            v2f vert (appdata_base v)
            {
                v2f o;
                float4 worldVertex = mul(unity_ObjectToWorld, v.vertex);
                half3 worldNormal = UnityObjectToWorldNormal(v.normal);
                float3 expandVertex = worldVertex.xyz + worldNormal * _BorderWidth;
                o.vertex = mul(UNITY_MATRIX_VP, float4(expandVertex, 1.0));
                return o;
            }
            
            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
				return _BorderColor;
            }
			ENDCG
		}

		Pass
		{
			Cull Back

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog



			#include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float4 worldVertex: COLOR0;
				float3 normal: COLOR1;
			};

			fixed4 _MainColor;
			float4 _MainTex_ST;
			
            v2f vert (appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                o.normal = v.normal;
                o.worldVertex = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }
            
            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                half3 worldNormal = UnityObjectToWorldNormal(i.normal);
                float diffuseStrength = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
                float3 halfVec = normalize(_WorldSpaceLightPos0.xyz + (_WorldSpaceCameraPos.xyz - i.worldVertex.xyz));
                float specularStrength = max(0, pow(dot(worldNormal, halfVec), 900));

                fixed4 col = _MainColor;
                float factor = 0.0;

                if (specularStrength > 0) {
                	factor = 1.0;
                } else {
					if (diffuseStrength > 0.2) {
						factor = 0.8;
					} else {
						factor = 0.6;
					}
                }
                return col * factor;
            }
			ENDCG
		}
	}
}
