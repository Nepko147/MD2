Shader "Custom/Sprites/TextureMix + Rotation"
{
    Properties
    {
        _MainTex ("Background Texture", 2D) = "white" {}
        _OverlayTex ("Overlay Texture", 2D) = "black" {}
        _PositionX ("Overlay Position X", Float) = 1.0
        _PositionY ("Overlay Position Y", Float) = 1.0
        _OverlayScaleX ("Overlay Scale X", Float) = 1.0
        _OverlayScaleY ("Overlay Scale Y", Float) = 1.0
        _OverlayRotation ("Overlay Rotation", Float) = 0.0
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
            float _PositionX;
            float _PositionY;
            float _OverlayScaleX;
            float _OverlayScaleY;
            float _OverlayRotation;

            v2f vert (appdata_t _v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(_v.vertex);
                o.uv_MainTex = _v.uv;

                // Преобразование угла из градусов в радианы
                float rotationRad = _OverlayRotation * 3.14159265 / 180.0;

                // Создание матрицы поворота
                float cosTheta = cos(rotationRad);
                float sinTheta = sin(rotationRad);

                // Позиционируем текстуру наложения
                float2 overlayOffset = float2(_PositionX, _PositionY);
                float2 uvOffset = _v.uv - overlayOffset;

                // Применяем поворот к координатам текстуры наложения
                o.uv_OverlayTex.x = cosTheta * uvOffset.x - sinTheta * uvOffset.y;
                o.uv_OverlayTex.y = sinTheta * uvOffset.x + cosTheta * uvOffset.y;                

                // Применение масштаба
                o.uv_OverlayTex.x /= _OverlayScaleX * (1);
                o.uv_OverlayTex.y /= _OverlayScaleY * (1);

                // Устанавливаем координаты текстуры наложения (Overlay)
                //o.uv_OverlayTex.x = (_v.uv.x - _PositionX) / _OverlayScaleX;
                //o.uv_OverlayTex.y = (_v.uv.y - _PositionY) / _OverlayScaleY;
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
                    fixed4 newColor = (col * (1 - overlayCol.a)) + overlayCol * overlayCol.a;                     
                    newColor.a = col.a + overlayCol.a; 
                    return newColor;
                }
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}