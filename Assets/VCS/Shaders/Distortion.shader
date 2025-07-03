Shader "Custom/Post Process/Distortion"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _MainNormalMap ("Main Normal Map", 2D) = "bump" {}

        _OverlayNormalMap ("Overlay Norma Map", 2D) = "bump" {}        
        _OverlayNormalMap_aplha("Overlay Norma Map Alpha", Range (0, 1)) = 0.04
        _OverlayNormalMap_pos_X ("Overlay Norma Map Position X", Range (-1, 1)) = 0.04
        _OverlayNormalMap_pos_Y ("Overlay Norma Map Position Y", Range (-1, 1)) = 0.04
        _OverlayNormalMap_Scale ("Overlay Texture Scale", Range (0, 10)) = 0.04

        _Strength ("_Strength", Range (0, 1)) = 0.1
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
                float2 uv_texNormalMap : TEXCOORD1;
                float2 uv_OverlayNormalMap : TEXCOORD2;
                float4 vertex : SV_POSITION;
            };

            float4 _MainTex_ST;

            sampler2D _MainTex;

            float _Screen_widht;
            float _Screen_height;

            sampler2D   _MainNormalMap;
            fixed       _MainNormalMap_aspect;

            sampler2D   _OverlayNormalMap;
            float       _OverlayNormalMap_aplha;
            fixed       _OverlayNormalMap_pos_X;
            fixed       _OverlayNormalMap_pos_Y;
            fixed       _OverlayNormalMap_Scale;

            fixed _Strength;            

            float2 correction;  

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv_texNormalMap = v.uv;
                o.uv_OverlayNormalMap.x = ((v.uv.x - _OverlayNormalMap_pos_X + ((_Screen_height / _Screen_widht) / 2) * _OverlayNormalMap_Scale) / _OverlayNormalMap_Scale * (_Screen_widht / _Screen_height));
                o.uv_OverlayNormalMap.y = ((v.uv.y - _OverlayNormalMap_pos_Y + 0.5 * _OverlayNormalMap_Scale) / _OverlayNormalMap_Scale);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainNormalMap, i.uv_texNormalMap);
                fixed4 overlayCol = tex2D(_OverlayNormalMap, i.uv_OverlayNormalMap);
                

                if (!(overlayCol.a == 0 
                    || i.uv_OverlayNormalMap.x > 1
                    || i.uv_OverlayNormalMap.y > 1
                    || i.uv_OverlayNormalMap.y < 0
                    || i.uv_OverlayNormalMap.x < 0))
                {
                    overlayCol.a = _OverlayNormalMap_aplha;
                    fixed4 newColor = (col * (1 - overlayCol.a)) + overlayCol * overlayCol.a;                     
                    newColor.a = col.a; 
                    col = newColor;
                }

                // Получаем текущий цвет пикселя
                float2 offset = (col.rg - 0.5) * 2.0 * _Strength;
                
                correction.x = _Strength * _MainNormalMap_aspect + (_Strength * 0.014); // Автоматическая крректировка положения изображения вместо "костыликов"
                correction.y = _Strength * _MainNormalMap_aspect + (_Strength * 0.006); // Автоматическая крректировка положения изображения вместо "костыликов"
                
                fixed4 color = tex2D(_MainTex, i.uv + offset + correction);
                return color; // Комбинируем оригинальный цвет с размытым
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}