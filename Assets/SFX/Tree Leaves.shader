Shader "Custom/Tree Leaves" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB) Alpha (A)", 2D) = "white" {}
	}
	
	Dependency "OptimizedShader" = "Hidden/Nature/Tree Creator Leaves Optimized"

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
			
				sampler2D _MainTex;
				float4 _MainTex_ST;

				struct data {
				    float4 vertex : POSITION;
				    float2 uv : TEXCOORD0;
				};
				
				struct v2f {
				    float4 position : POSITION;
				    float2 uv : TEXCOORD0;
				};
				
				v2f vert(data i){
				    v2f o;
				    
				    o.position = mul(UNITY_MATRIX_MVP, i.vertex);
				  	  
					return o;
				}
				
				half4 frag( v2f i ) : COLOR
				{   
					return half4(0, 0, 0, tex2D(_MainTex, i.uv).a);
				}
				
				ENDCG
	        }
	    }
	}
}