Shader "Unlit/LineArt"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Displace ("_Displace", Range(0, 0.5)) = 0 
        _Line("Line Size", Range(0, 0.5)) = 0.1     
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
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Displace, _Line;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float2 uv2 = i.uv + float2(0.01, 0.01) * _Displace;
                fixed4 col1 = tex2D(_MainTex, uv);
                fixed4 col2 = tex2D(_MainTex, uv2);
                if (length(col1 - col2 ) > _Line) 
                    return 0;
                else
                    return 1;
            }
            ENDCG
        }
    }
}
