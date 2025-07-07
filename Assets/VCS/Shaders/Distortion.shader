Shader "Custom/Post Process/Distortion"
{
    Properties
    {
        _OverlayNormalMap_pos_X ("Overlay Norma Map Position X", Range (-1, 1)) = 0.5
        _OverlayNormalMap_pos_Y ("Overlay Norma Map Position Y", Range (-1, 1)) = 0.5
        _OverlayNormalMap_aplha("Overlay Norma Map Alpha", Range (0, 1)) = 0.5
        _OverlayNormalMap_Scale ("Overlay Texture Scale", Range (0, 10)) = 0.5

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

            sampler2D _MainTex;
            float4 _MainTex_ST;

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

            struct vertex_data 
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct fragment_data 
            {
                float2 uv : TEXCOORD0;
                float2 uv_texNormalMap : TEXCOORD1;
                float2 uv_OverlayNormalMap : TEXCOORD2;
                float4 vertex : SV_POSITION;
            };

            fragment_data vert(vertex_data _input_vd)
            {
                fragment_data _output_fd;

                _output_fd.vertex = UnityObjectToClipPos(_input_vd.vertex);
                _output_fd.uv = TRANSFORM_TEX(_input_vd.uv, _MainTex);
                _output_fd.uv_texNormalMap = _input_vd.uv;
                _output_fd.uv_OverlayNormalMap.x = ((_input_vd.uv.x - _OverlayNormalMap_pos_X + ((_Screen_height / _Screen_widht) / 2) * _OverlayNormalMap_Scale) / _OverlayNormalMap_Scale * (_Screen_widht / _Screen_height));
                _output_fd.uv_OverlayNormalMap.y = ((_input_vd.uv.y - _OverlayNormalMap_pos_Y + 0.5 * _OverlayNormalMap_Scale) / _OverlayNormalMap_Scale);

                return _output_fd;
            }

            fixed4 frag(fragment_data _input_fd) : SV_Target
            {
                fixed4 col = tex2D(_MainNormalMap, _input_fd.uv_texNormalMap);
                fixed4 overlayCol = tex2D(_OverlayNormalMap, _input_fd.uv_OverlayNormalMap);

                if (!(overlayCol.a == 0 
                    || _input_fd.uv_OverlayNormalMap.x > 1
                    || _input_fd.uv_OverlayNormalMap.y > 1
                    || _input_fd.uv_OverlayNormalMap.y < 0
                    || _input_fd.uv_OverlayNormalMap.x < 0))
                {
                    overlayCol.a = _OverlayNormalMap_aplha;
                    fixed4 newColor = (col * (1 - overlayCol.a)) + overlayCol * overlayCol.a;                     
                    newColor.a = col.a; 
                    col = newColor;
                }

                float2 _offset = (col.rg - 0.5) * 2.0 * _Strength + _Strength / _MainNormalMap_aspect;
                fixed4 _output_color = tex2D(_MainTex, _input_fd.uv + _offset);

                return _output_color;
            }

            ENDCG
        }
    }

    FallBack "Diffuse"
}