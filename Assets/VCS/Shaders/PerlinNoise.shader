Shader "Custom/Perlin Noise"
{
    Properties
    {
        u_scope ("Scope", Float) = 0.25
        u_color ("Color", Vector) = (0.5, 0.5, 0.5, 0.5)
        u_offset ("Offset", Vector) = (10, 10, 0, 0)
    }

    SubShader
    {
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            fixed4 _MainTex_ST;

            fixed u_scope;
            fixed4 u_color;
            half2 u_offset;

            float Rand(float2 _vec1, float _seed)
            {
                half2 _vec2 = half2(12.9898, 78.233);
                float _dot = dot(_vec1, _vec2);
                float _dot_sin = sin(_dot);

	            return (frac(_dot_sin * (43758.5453 + _seed)));
            }

            float CosInterpolate(float _y1, float _y2, float _r1)
            {
                float _r1_cos = cos(_r1 * 3.1415926);
	            float _r2 = (1.0 - _r1_cos) / 2.0;

	            return (_y1 * (1.0 - _r2) + _y2 * _r2);
            }

            float Noise(float2 _vec1, float _seed, float2 _freq)
            {
                float2 _vec1_feeq_floor = floor(_vec1 * _freq);
                fixed2 _vec2;
                
	            float _fl1 = Rand(_vec1_feeq_floor, _seed);

                _vec2 = fixed2(1.0, 0.0);
	            float _fl2 = Rand(_vec1_feeq_floor + _vec2, _seed);

                _vec2 = fixed2(0.0, 1.0);
	            float _fl3 = Rand(_vec1_feeq_floor + _vec2, _seed);

                _vec2 = fixed2(1.0, 1.0);
	            float _fl4 = Rand(_vec1_feeq_floor + _vec2, _seed);

	            float2 _vec1_freq_frac = frac(_vec1 * _freq);

	            float2 _r1 = CosInterpolate(_fl1, _fl2, _vec1_freq_frac.x);
	            float2 _r2 = CosInterpolate(_fl3, _fl4, _vec1_freq_frac.x);

	            return (CosInterpolate(_r1, _r2, _vec1_freq_frac.y));
            }

            float PerlinNoise(float2 _pos, float _seed, float _freq_start, float _amp_start, float _amp_ratio)
            {
	            float _freq = _freq_start;
                float2 _freq_float2 = float2(_freq, _freq);
                float _amp = _amp_start;

	            float _pn = Noise(_pos, _seed, _freq_float2) * _amp;
	
	            for (int i = 0; i < 4; ++i)
	            {
		            _freq *= 2.0;
                    _freq_float2 = float2(_freq, _freq);
		            _amp *= _amp_ratio;

		            _pn += (Noise(_pos, _seed, _freq_float2) * 2.0 - 1.0) * _amp;
	            }
	
	            return _pn;
            }

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
	            float _pn = PerlinNoise(_input_fd.uv / u_scope + u_offset, 0.0, 1.5, 1.0, 0.35);

                return (fixed4(_pn * u_color.r, _pn * u_color.g, _pn * u_color.b, u_color.a));
            }

            ENDCG
        }
    }

    FallBack "Diffuse"
}
