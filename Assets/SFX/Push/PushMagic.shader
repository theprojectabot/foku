Shader "Custom/Push" {
	Properties {
	    _NoiseTex ("Normal Texture (RG)", 2D) = "white" {}
	    _Alpha ("Alpha (A)", 2D) = "white" {}
	    strength("Strength", Range(0, 0.4)) = 0.2
	    transparency("Transparency", Range(0, 1)) = 0.5
	}
	
	Category {
	    Tags { "Queue" = "Transparent+10" }
	    SubShader {
	        GrabPass {
		        Name "BASE"
	            Tags { "LightMode" = "Always" }
	        }
	       
	        Pass {
				Name "BASE"
				Tags { "LightMode" = "Always" }
		        Lighting Off 
			    Cull Off
				ZWrite On
				ZTest LEqual
				Blend SrcAlpha OneMinusSrcAlpha
				AlphaTest Greater 0
	         
				CGPROGRAM
				#pragma exclude_renderers xbox360
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#pragma fragmentoption ARB_fog_exp2
				#include "UnityCG.cginc"
				
				sampler2D _GrabTexture : register(s0);
				float4 _NoiseTex_ST;
				float4 _Alpha_ST;
				sampler2D _NoiseTex;
				sampler2D _Alpha;
				float strength;
				float transparency;
				
				struct data {
				    float4 vertex : POSITION;
				    float3 normal : NORMAL;
				    float4 texcoord : TEXCOORD0;
					fixed4 color : COLOR;
				};
				
				struct v2f {
				    float4 position : POSITION;
				    float4 screenPos : TEXCOORD0;
				    float2 uvmain : TEXCOORD2;
					fixed4 color : COLOR;
				};
				
				v2f vert(data i){
				    v2f o;
				    o.position = mul(UNITY_MATRIX_MVP, i.vertex);      // compute transformed vertex position
				    o.uvmain = TRANSFORM_TEX(i.texcoord, _NoiseTex);   // compute the texcoords of the noise
					float viewAngle = dot(normalize(ObjSpaceViewDir(i.vertex)),
										 i.normal);
					o.screenPos = o.position; 
					o.color = i.color;
					return o;
				}
				
				half4 frag( v2f i ) : COLOR
				{   
				    float2 screenPos = i.screenPos.xy / i.screenPos.w;
				    screenPos.x = (screenPos.x + 1) * 0.5;  
				    screenPos.y = (screenPos.y + 1) * 0.5;  
				 
				    if (_ProjectionParams.x < 0)
				        screenPos.y = 1 - screenPos.y;
				        
				#if SHADER_API_D3D9
					screenPos.y = 1 - screenPos.y;
				#endif
				   
				    half4 offsetColor1 = tex2D(_NoiseTex, i.uvmain);
				    half4 alpha = tex2D(_Alpha, i.uvmain);
				    
				    half2 dv = (0.5 - i.uvmain.xy);
				    half dvl = sqrt(dv.x*dv.x+dv.y*dv.y) * 2;
				    half force = (1 - saturate(dvl)) * strength;
				    
				    screenPos += dv * force;
				    half4 col = tex2D( _GrabTexture, screenPos);
				    col.a = transparency;
				    return col;
				}
				
				ENDCG
	        }
	    }
	}
}