Shader "Custom/Passthrough"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
    }

    Subshader
    {
        Tags 
        {
            "RenderType" = "Opaque"
        }

        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;

            fixed4 _Color;

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

            fragment_data vert(vertex_data _vd)
            {
                fragment_data _output;
                _output.pos = UnityObjectToClipPos(_vd.pos);
                _output.uv = TRANSFORM_TEX(_vd.uv, _MainTex);
                
                return (_output);
            }

            fixed4 frag(fragment_data _fd) : SV_Target
            {
                return (tex2D(_MainTex, _fd.uv) * _Color);
            }

            ENDCG
        }
    }

    FallBack "Diffuse"
}
