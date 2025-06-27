Shader "Custom/CleanColor"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        u_color ("Color", Vector) = (0, 0, 0, 1)
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
            
            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 u_color;
            
            struct vertex_data
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct fragment_data
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
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
	            fixed4 _output_col = fixed4(u_color.r, u_color.g, u_color.b, u_color.a);
                return _output_col;
            }

            ENDCG
        }
    }

    FallBack "Diffuse"
}
