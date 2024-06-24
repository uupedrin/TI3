Shader "Unlit/Dither"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Colors ("Color", Range(1,16)) = 16
        _Dither ("Dither", Range(0,0.5)) = 0.5
        _Size ("Size", float) = 256        
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
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
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Colors, _Dither, _Size;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 color = tex2D(_MainTex, i.uv);
                float a = floor((i.uv.x / _Size) % 2.0);
                float b = floor((i.uv.y / _Size) % 2.0);	
                float c = (a + b) % 2.0;
                float4 col;

                col.r = (round(color.r * _Colors + _Dither) / _Colors) * c;
                col.g = (round(color.g * _Colors + _Dither) / _Colors) * c;
                col.b = (round(color.b * _Colors + _Dither) / _Colors) * c;
                c = 1.0 - c;
                col.r += (round(color.r * _Colors - _Dither) / _Colors) * c;
                col.g += (round(color.g * _Colors - _Dither) / _Colors) * c;
                col.b += (round(color.b * _Colors - _Dither) / _Colors) * c;

                return col;
            }
            ENDCG
        }
    }
}
