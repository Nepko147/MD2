Shader "Custom/Sprites/Sky"
{
    Properties
    {
        _color1 ("Color 1", Color) = (1,0,0,1)
        _color2 ("Color 2", Color) = (0,1,0,1)
        _color3 ("Color 3", Color) = (0,0,1,1)
        _blend_offset ("Blend Offset", Range(-2,2)) = 0.5
        _blend_scale ("Blend Scale", Range(-2,2)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            fixed4 _color1;
            fixed4 _color2;
            fixed4 _color3;
            float _blend_offset;
            float _blend_scale;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float blend = i.uv.y * _blend_scale + _blend_offset;

                if (blend < 0.5)
                {
                    return lerp(_color3, _color2, blend * 2);
                }
                else
                {
                    return lerp(_color2, _color1, (blend - 0.5) * 2);
                }
            }
            ENDCG
        }
    }
}