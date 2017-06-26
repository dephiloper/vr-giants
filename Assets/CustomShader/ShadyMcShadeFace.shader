Shader "Custom/ShadyMcShadeFace" {
    Properties {
        _Color ("Main Color", Color) = (1,1,1,0.5)
        _SpecColor ("Spec Color", Color) = (1,1,1,1)
        _Emission ("Emmisive Color", Color) = (0,0,0,0)
        _Shininess ("Shininess", Range (0.01, 1)) = 0.7
        _FogColor ("Fog Color", Color) = (1,1,1,0.5)
        _FogDensity ("Fog Density", Float) = 0.5
        _FogRange ("Fog Range", Range (0.01, 1)) = 0.5
    }
    // DX11 / GLES3.1
    SubShader {
        Tags { "Queue" = "Transparent" }
        Pass {
            Material {
                    Diffuse [_Color]
                    Ambient [_Color]
                    Shininess [_Shininess]
                    Specular [_SpecColor]
                    Emission [_Emission]
                }
            Lighting On
            SeparateSpecular On
            ZWrite Off
        }
        UsePass "VertexLit/SHADOWCASTER"
    }
    // DX9 SM3 / GLES3
    //SubShader {
    //
    //}
    // way to old hardware
    //SubShader {
    //
    //}
}