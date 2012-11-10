Shader "Custom/Water" {
	Properties {
	}
	
	Category {
	    SubShader {
	        Pass {
		        Lighting Off 
			    Cull Off
				ZWrite On
				ZTest LEqual
	         
				CGPROGRAM
				#pragma exclude_renderers xbox360
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#pragma fragmentoption ARB_fog_exp2
				#include "UnityCG.cginc"
				
				#define X_SCALE 190
				#define TIME_SCALE 6
				#define SCALE1 0.01
				#define AMP1 0.12
				#define SCALE2 -0.03
				#define AMP2 0.12
				#define SCALE3 0.07
				#define AMP3 0.03
				
				struct data {
				    float4 vertex : POSITION;
				};
				
				struct v2f {
				    float4 position : POSITION;
				};
				
				v2f vert(data i){
				    v2f o;
				    
				    half x = i.vertex.x * X_SCALE;
				    half t = _Time.y * TIME_SCALE;
				    				    
				    half dx = sin(x * SCALE1 + t) * AMP1 + sin(x * SCALE2 + t) * AMP2 + sin(x * SCALE3 + t) * AMP3;
				    dx *= 0.2;
				    i.vertex.z += dx;
				    
				    o.position = mul(UNITY_MATRIX_MVP, i.vertex);
				    
				    
					return o;
				}
				
				half4 frag( v2f i ) : COLOR
				{   
					return 0;
				}
				
				ENDCG
	        }
	    }
	}
}