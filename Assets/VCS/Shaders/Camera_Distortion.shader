Shader "Custom/Camera_Distortion"
{
    Properties
    {
        u_strength ("Strength", Range (0, 1)) = 0.1
        u_strength_correction("Strength Correction", Float) = 3.44

        u_overlay_screenPos("Overlay Screen Position", Vector) = (0, 0, 0, 0)
        u_overlay_scale("Overlay Scale", Vector) = (1, 1, 0, 0)
        u_overlay_aplha("Overlay Alpha", Range (0, 1)) = 0.5 
    }

    Subshader
    {
        LOD 100

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;

            fixed u_strength;
            half u_strength_correction;

            sampler2D u_main_normalMap;

            sampler2D u_overlay_normalMap;
            half2 u_overlay_screenPos;
            half2 u_overlay_scale;
            fixed u_overlay_aplha;

            struct vertex_data 
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct fragment_data 
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float2 uv_main : TEXCOORD1;
                float2 uv_overlay : TEXCOORD2;
            };

            fragment_data vert(vertex_data _input_vd)
            {
                fragment_data _output_fd;

                _output_fd.vertex = UnityObjectToClipPos(_input_vd.vertex);
                _output_fd.uv = _input_vd.uv;

                _output_fd.uv_main = _input_vd.uv;

                _output_fd.uv_overlay.x = (_input_vd.uv.x + 0.5 * u_overlay_scale.x - u_overlay_screenPos.x) / u_overlay_scale.x;
                _output_fd.uv_overlay.y = (_input_vd.uv.y + 0.5 * u_overlay_scale.y - u_overlay_screenPos.y) / u_overlay_scale.y;

                return (_output_fd);
            }

            fixed4 frag(fragment_data _input_fd) : SV_Target
            {
                fixed4 _main_col = tex2D(u_main_normalMap, _input_fd.uv_main);
                fixed4 _overlay_col = tex2D(u_overlay_normalMap, _input_fd.uv_overlay);
                fixed4 _mixed_col = _main_col * (1 - u_overlay_aplha) + _overlay_col * u_overlay_aplha; 
                
                float2 _offset = (_mixed_col.rg - 0.5) * u_strength + u_strength / u_strength_correction;

                return (tex2D(_MainTex, _input_fd.uv + _offset));
            }

            ENDCG
        }
    }

    FallBack "Diffuse"
}
