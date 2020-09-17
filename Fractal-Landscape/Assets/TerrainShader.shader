Shader "SAM/Terrain Shader"
{
    Properties
    {
        [NoScaleOffset] _MainTex("Texture", 2D) = "white" {}
    }
        SubShader
    {
        Pass
        {
            // indicate that our pass is the "base" pass in forward
            // rendering pipeline. It gets ambient and main directional
            // light data set up; light direction in _WorldSpaceLightPos0
            // and color in _LightColor0
            Tags {"LightMode" = "ForwardBase"}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc" // for UnityObjectToWorldNormal
            #include "UnityLightingCommon.cginc" // for _LightColor0

            struct vertIn
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
            };

            struct v2f
            {
                fixed4 diff : COLOR0; // diffuse lighting color
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata_full i)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(i.vertex);
                // get vertex normal in world space
                half3 worldNormal = UnityObjectToWorldNormal(i.normal);
                // dot product between normal and light direction for
                // standard diffuse (Lambert) lighting
                half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
                // factor in the light color
                o.diff = i.color * nl * _LightColor0;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag(v2f i) : SV_Target
            {
                // sample texture
                fixed4 col = i.diff;
                // multiply by lighting
                return col;
            }
            ENDCG
        }
    }
}