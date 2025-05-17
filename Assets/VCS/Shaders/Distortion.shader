Shader "Custom/Post Process/Distortion"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        u_texNormalMap ("u_texNormalMap", 2D) = "bump" {}
        u_strength ("u_strength", Range (0, 1)) = 0.04
    }

    Subshader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 _MainTex_ST;

            sampler2D _MainTex;
            sampler2D u_texNormalMap;
            fixed u_strength;
            fixed u_aspect;            

            float2 correction;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Получаем текущий цвет пикселя
                float2 offset = (tex2D(u_texNormalMap, i.uv).rg - 0.5) * 2.0 * u_strength;
                //offset.x /= u_aspect;
                
                correction.x = u_strength * u_aspect + (u_strength * 0.014); // Автоматическая крректировка положения изображения вместо "костыликов"
                correction.y = u_strength * u_aspect + (u_strength * 0.006); // Автоматическая крректировка положения изображения вместо "костыликов"
                
                fixed4 color = tex2D(_MainTex, i.uv + offset + correction);
                return color; // Комбинируем оригинальный цвет с размытым
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}