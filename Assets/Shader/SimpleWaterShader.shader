Shader "SAM/SimpleWaterShader"
{
    Properties
    {
        _MainTex("Base (RGB)", 2D) = "white" {}
        _MainColor("Main Coloe (RGBA)", Color) = (1,1,1,1)
        _WaveHeight("Wave Height",Range(0,0.1)) = 0.01
        _WaveFrequency("Wave Frequency",Range(1,100)) = 50
        _WaveSpeed("Wave Speed",Range(0,10)) = 1
    }
    SubShader
    {
        Tags{"Queue" = "Transparent"}
        //LOD 100

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert 
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct v2f
            {
                float4 vertex:SV_POSITION;
                float2 uv:TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _MainColor;

            float _WaveHeight;
            float _WaveFrequency;
            float _WaveSpeed;

            v2f vert(appdata_base v)
            {
                v2f o;
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.vertex = UnityObjectToClipPos(v.vertex);

                return o;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                float2 uvCoord = _MainTex_ST.xy * 0.5;

                fixed2 uvDir = normalize(IN.uv - uvCoord);
                fixed uvDis = distance(IN.uv,uvCoord);

                //clip(uvCoord - uvDis);

                fixed2 uv = IN.uv + sin(uvDis * _WaveFrequency - _Time.y * _WaveSpeed) * _WaveHeight * uvDir;

                fixed4 color;
                color.rgb = tex2D(_MainTex, uv) * _MainColor;
                color.a = _MainColor.a;
                return color;
            }
            ENDCG
        }
    }
    FallBack "Transparent"
}
