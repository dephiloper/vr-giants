Shader "Custom/OutlineShader" {
	 Properties
        {
            // Color property for material inspector, default to white
            _Color ("Main Color", Color) = (0,0,0,0.5)
        }
        SubShader
        {
            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                
                // vertex shader
                // this time instead of using "appdata" struct, just spell inputs manually,
                // and instead of returning v2f struct, also just return a single output
                // float4 clip position
                float4 vert (float4 position : POSITION, float3 normal : NORMAL) : SV_POSITION
                {
                    float4 original = UnityObjectToClipPos(position);
                    float4 normal2 = UnityObjectToClipPos(normal); // der var name ist kacke
                
                    return original + (mul(0.05, normal2));
                }
                
                // color from the material
                fixed4 _Color;
    
                // pixel shader, no inputs needed
                fixed4 frag () : SV_Target
                {
                    return _Color; // just return it
                }
                ENDCG
            }
            //UsePass "Custom/SHADYMCSHADEFACE"
        }
        
}
