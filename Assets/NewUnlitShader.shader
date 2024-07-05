Shader "Custom/PlayerOutline"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineThickness ("Outline Thickness", Range(0.0, 0.05)) = 0.02
    }

    SubShader
    {
        Tags { "Queue" = "Overlay" }
        LOD 100

        Pass
        {
            Name "BASE"
            Tags { "LightMode"="Always" }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _OutlineColor;
            float _OutlineThickness;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 offsets[8] = {
                    float2(-_OutlineThickness, -_OutlineThickness),
                    float2(0, -_OutlineThickness),
                    float2(_OutlineThickness, -_OutlineThickness),
                    float2(-_OutlineThickness, 0),
                    float2(_OutlineThickness, 0),
                    float2(-_OutlineThickness, _OutlineThickness),
                    float2(0, _OutlineThickness),
                    float2(_OutlineThickness, _OutlineThickness)
                };
                
                fixed4 c = tex2D(_MainTex, i.uv);
                if (c.a == 0)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        fixed4 sample = tex2D(_MainTex, i.uv + offsets[j]);
                        if (sample.a > 0)
                        {
                            return _OutlineColor;
                        }
                    }
                }
                return c;
            }
            ENDCG
        }
    }
}
