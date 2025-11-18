Shader "Custom/Camera_Passthrough"
{
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

            struct vertex_data 
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct fragment_data 
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            fragment_data vert(vertex_data _input_vd)
            {
                fragment_data _output_fd;

                _output_fd.pos = UnityObjectToClipPos(_input_vd.pos);
                _output_fd.uv = _input_vd.uv;

                return _output_fd;
            }

            fixed4 frag(fragment_data _input_fd) : SV_Target
            {
                return (tex2D(_MainTex, _input_fd.uv));
            }

            ENDCG
        }
    }

    FallBack "Diffuse"
}
