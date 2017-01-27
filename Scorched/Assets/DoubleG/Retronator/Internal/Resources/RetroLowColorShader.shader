Shader "Unlit/RetroLowColorShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_ColorTint("Tint", Color) = (1.0, 0.6, 0.6, 1.0)
	}
	SubShader
	{

		Tags
		{
			"Queue" = "Overlay"
			"RenderType"="Opaque"
		}
		LOD 100

		Pass
		{
			CGPROGRAM

			float reduceColorComponent(float component, float colorCountPerComponent, bool dither)
			{
				if (colorCountPerComponent >= 255)
					return component;

				float a = round(component * colorCountPerComponent) / colorCountPerComponent;

				//if (dither)
				//{
				//	float step = 1.0 / colorCountPerComponent;
				//	if (a + step * 0.5 < component)
				//		return a + step;
				//}
				return a;
			}

			float3 reduceColor(float3 color, float colorCountPerComponent, bool dither, bool greyscale = false)
			{
				float3 colors = float3(reduceColorComponent(color.x, colorCountPerComponent, dither),
					reduceColorComponent(color.y, colorCountPerComponent, dither),
					reduceColorComponent(color.z, colorCountPerComponent, dither));
				if (greyscale)
				{
					float luminance = colors.x * 0.299 + colors.y * 0.587 + colors.z * 0.114;
					return float3(luminance, luminance, luminance);
				}
				return colors;
			}

			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			// #pragma multi_compile_fog
			
			#include "UnityCG.cginc"
			
			int _ColorDepth;
			int _Greyscale;

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
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _ColorTint;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				//UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				float4 col = tex2D(_MainTex, i.uv);

				col.xyz = reduceColor(col.xyz, _ColorDepth, false, _Greyscale == 1);
				col *= (_ColorTint * _ColorTint[3]);
				return col;
			}
			ENDCG
		}
	}
}
