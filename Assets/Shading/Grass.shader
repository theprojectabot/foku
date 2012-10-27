// Simplified Diffuse shader. Differences from regular Diffuse one:
// - no Main Color
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.

Shader "Custom/Grass" {
	Properties {
		_MainTex ("Base (RGBA)", 2D) = "white" {}
	}
	
	Category {
		Tags {"Queue"="Transparent"  "RenderType"="Transparent"}
	
		SubShader {
			LOD 200
			Pass{
		        Lighting Off 
			    Cull Off
				ZWrite On
				ZTest LEqual
				Blend SrcAlpha OneMinusSrcAlpha
				AlphaTest Greater 0
			
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#pragma fragmentoption ARB_fog_exp2
				
				#include "UnityCG.cginc"
				
				sampler2D _MainTex;
				float4 _MainTex_ST;
				float4x4 Transform;
				
				struct v2f {
				    float4  pos : SV_POSITION;
				    float2  uv : TEXCOORD0;
				};
				
				v2f vert (appdata_base v)
				{
				    v2f o;
				    o.pos = mul(mul(UNITY_MATRIX_MVP, Transform), v.vertex); 
				    o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				    return o;
				}
				
				half4 frag (v2f i) : COLOR
				{
				    half4 texcol = tex2D (_MainTex, i.uv);
				    return texcol;
				}
				ENDCG
			}
		}
	}
}