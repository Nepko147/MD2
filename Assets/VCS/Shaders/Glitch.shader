Shader "Custom/Glitch"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ShakePower ("Shake power", float) =  0.03
        _ShakeRate ("Shake range", Range(0, 1)) = 0.2
        _ShakeSpeed ("Shake speed", float) = 5.0
        _ShakeBlockSize ("Shake speed", float) = 30.5
        _ShakeColorRate ("Shake range", Range(0, 1)) = 0.01 
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
                float TIME : TEXCOORD1;
            };

            fragment_data vert(vertex_data _vd)
            {
                fragment_data _output;
                _output.pos = UnityObjectToClipPos(_vd.pos);
                _output.uv = TRANSFORM_TEX(_vd.uv, _MainTex);

                return (_output);
            }
                        
            float _ShakePower;           
            float _ShakeRate;
            float _ShakeSpeed;
            float _ShakeBlockSize;
            float _ShakeColorRate;

            float Rand(float _seed)
            {	            
                float2 _vec1 = float2(_seed, _seed);
                float2 _vec2 = float2(3525.46, -54.3415);                
                float _dot = dot(_vec1, _vec2);
                float _dot_sin = sin(_dot);
                
                return (frac(543.2543 * _dot_sin));
            }

            fixed4 frag(fragment_data _input) : SV_Target
            {                
                float _seed = floor(_Time.y * _ShakeSpeed);
                float _shift = (Rand(_seed) < _ShakeRate) ? 1.0 : 0.0;

	            float2 _fixed_uv = _input.uv;

                float _block_index = floor(_input.uv.y * _ShakeBlockSize);
                float _rand_offset = Rand(_block_index / _ShakeBlockSize + _Time.y);
	            _fixed_uv.x += (_rand_offset - 0.5) * _ShakePower * _shift;

	            float4 _color = tex2D(_MainTex, _fixed_uv);

                float4 _r_offset = tex2D(_MainTex, _fixed_uv + float2(_ShakeColorRate, 0.0));
                float4 _b_offset = tex2D(_MainTex, _fixed_uv + float2(-_ShakeColorRate, 0.0));

	            _color.r = lerp(_color.r, _r_offset, _shift);
	            _color.b = lerp(_color.b, _b_offset, _shift);
	                         
                return _color;
            }

            ENDCG
        }
    }

    FallBack "Diffuse"
}
