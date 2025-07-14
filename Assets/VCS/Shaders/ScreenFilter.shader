Shader "Custom/Post Process/Screen Filter"
{
    Properties
    {
        u_lines_num ("Lines Number", Float) = 100.0
        u_lines_alpha ("Alpha", Range(0, 1)) = 0.85
    }

    SubShader
    {
        LOD 100

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            #define PI 3.1415926

            sampler2D _MainTex;
            float4 _MainTex_ST;

            half u_lines_num;
            fixed u_lines_alpha;

            struct vertex_data
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct fragment_data
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            fragment_data vert (vertex_data _input_vd)
            {
                fragment_data _output_fd;

                _output_fd.vertex = UnityObjectToClipPos(_input_vd.vertex);
                _output_fd.uv = TRANSFORM_TEX(_input_vd.uv, _MainTex);
                
                return _output_fd;
            }

            fixed4 frag (fragment_data _input_fd) : SV_Target
            {
                fixed _transitions = sin(PI * _input_fd.uv.y * u_lines_num * 2.0);
                _transitions = ceil(_transitions);
                
                return (tex2D(_MainTex, _input_fd.uv) * clamp(_transitions + u_lines_alpha, u_lines_alpha, 1.0));
            }
            
            ENDCG
        }
    }

    FallBack "Diffuse"
}
