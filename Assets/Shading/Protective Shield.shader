// Upgrade NOTE: replaced 'glstate.matrix.modelview[0]' with 'UNITY_MATRIX_MV'
// Upgrade NOTE: replaced 'glstate.matrix.mvp' with 'UNITY_MATRIX_MVP'

Shader "Custom/Protective Shield" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
    _NoiseTex ("Noise Texture (RG)", 2D) = "white" {}
    strength("strength", Range(0, 1)) = 0.2
    transparency("transparency", Range(0, 1)) = 0.5
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
// Upgrade NOTE: excluded shader from Xbox360; has structs without semantics (struct v2f members distortion)
#pragma exclude_renderers xbox360
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#pragma fragmentoption ARB_fog_exp2
#include "UnityCG.cginc"

sampler2D _GrabTexture : register(s0);
float4 _NoiseTex_ST;
float4 _Color;
sampler2D _NoiseTex;
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
	float distortion;
	fixed4 color : COLOR;
};

v2f vert(data i){
    v2f o;
    o.position = mul(UNITY_MATRIX_MVP, i.vertex);      // compute transformed vertex position
    o.uvmain = TRANSFORM_TEX(i.texcoord, _NoiseTex);   // compute the texcoords of the noise
    //float viewAngle = dot(normalize(mul((float3x3)glstate.matrix.invtrans.modelview[0], i.normal)),
	//					 float3(0,0,1));
	float viewAngle = dot(normalize(ObjSpaceViewDir(i.vertex)),
						 i.normal);
	o.distortion = viewAngle * viewAngle;	// square viewAngle to make the effect fall off stronger
	float depth = -mul( UNITY_MATRIX_MV, i.vertex ).z;	// compute vertex depth
	o.distortion /= 1+depth;		// scale effect with vertex depth
	o.distortion *= strength;	// multiply with user controlled strength
	o.screenPos = o.position; 
	o.color = i.color;
	return o;
}

half4 frag( v2f i ) : COLOR
{   
    float2 screenPos = i.screenPos.xy / i.screenPos.w;   // screenpos ranges from -1 to 1
    screenPos.x = (screenPos.x + 1) * 0.5;   // I need 0 to 1
    screenPos.y = (screenPos.y + 1) * 0.5;   // I need 0 to 1
 
    // check if anti aliasing is used
    if (_ProjectionParams.x < 0)
        screenPos.y = 1 - screenPos.y;
        
#if SHADER_API_D3D9
	screenPos.y = 1 - screenPos.y;
#endif
   
    // get two offset values by looking up the noise texture shifted in different directions
    half4 offsetColor1 = tex2D(_NoiseTex, i.uvmain + _Time.xz);
    half4 offsetColor2 = tex2D(_NoiseTex, i.uvmain - _Time.yx);
    
    // use the r values from the noise texture lookups and combine them for x offset
    // use the g values from the noise texture lookups and combine them for y offset
    // scale with distortion amount
    screenPos.x += ((offsetColor1.r + offsetColor2.r) - 1) * strength;
    screenPos.y += ((offsetColor1.g + offsetColor2.g) - 1) * strength;
   
    half4 col = tex2D( _GrabTexture, screenPos ) * _Color;
    col.a = transparency * _Color.a * i.color.a;
    return col;
}

ENDCG
        }
    }
}

}