Shader "Unlit/Stencil"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Geometry-10"}
         
        Pass {
            ColorMask 0
            ZWrite Off
            Stencil {
                Ref 1
                Comp Always
                Pass Replace
            }  
        }
    }
}
