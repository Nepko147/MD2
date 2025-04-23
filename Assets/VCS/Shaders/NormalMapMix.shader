Shader "Custom/Sprites/NormalMapMix"
{
    Properties
    {
        _MainTex ("Background Texture", 2D) = "white" {}
        _OverlayTex ("Overlay Texture", 2D) = "black" {}
        _OverlayTexApha("Overlay Texture Alpha", Range (0, 1)) = 0.04
        _OverlayTexPosX ("Overlay Texture Position X", Float) = 0.0
        _OverlayTexPosY ("Overlay Texture Position Y", Float) = 0.0
        _OverlayTexScale ("Overlay Texture Scale", Float) = 0.2

        _AspectX ("_AspectX", Float) = 0.2
        _AspectY ("_AspectY", Float) = 0.2
    }
    SubShader
    {
        Tags 
        { 
            "RenderType" = "TransparentCutOut"            
        }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv_MainTex : TEXCOORD0;      
                float2 uv_OverlayTex : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _OverlayTex;
            float _OverlayTexApha;
            float _OverlayTexPosX;
            float _OverlayTexPosY;
            float _OverlayTexScale;            

            float _AspectX;
            float _AspectY;

            v2f vert (appdata_t _v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(_v.vertex);
                o.uv_MainTex = _v.uv;
                // Устанавливаем координаты текстуры наложения (Overlay)
                o.uv_OverlayTex.x = ((_v.uv.x - _OverlayTexPosX + ((_AspectY / _AspectX) / 2) *_OverlayTexScale) / _OverlayTexScale * (_AspectX / _AspectY));
                o.uv_OverlayTex.y = ((_v.uv.y - _OverlayTexPosY + 0.5 *_OverlayTexScale) / _OverlayTexScale);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv_MainTex); // текстура фона
                fixed4 overlayCol = tex2D(_OverlayTex, i.uv_OverlayTex); // текстура наложения

                if (overlayCol.a == 0 
                    || i.uv_OverlayTex.x > 1
                    || i.uv_OverlayTex.y > 1
                    || i.uv_OverlayTex.y < 0
                    || i.uv_OverlayTex.x < 0)
                {
                    return col;
                } else {
                    overlayCol.a = _OverlayTexApha;
                    fixed4 newColor = (col * (1 - overlayCol.a)) + overlayCol * overlayCol.a;                     
                    newColor.a = col.a; 
                    return newColor;
                }
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}