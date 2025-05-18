Shader "Custom/PerlinNoise"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

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
            
            sampler2D _MainTex;
            float4 _MainTex_ST;

            half u_scope;
            float4 u_color;
            float2 u_offset;

            float cos_interpolate(float _y1, float _y2, float _r1)
            {
	            float _r2 = (1.0 - cos(_r1 * 3.1415926)) / 2.0;
	            return (_y1 * (1.0 - _r2) + _y2 * _r2);
            }

            float rand(fixed2 _v, float _seed)
            {
                float _dot = dot(_v, fixed2(12.9898, 78.233));
	            return frac(sin(_dot) * (43758.5453 + _seed));
            }

            float noise(fixed2 _v, float _seed, fixed2 _freq)
            {
	            float _fl1 = rand(floor(_v * _freq), _seed);
	            float _fl2 = rand(floor(_v * _freq) + fixed2(1.0, 0.0), _seed);
	            float _fl3 = rand(floor(_v * _freq) + fixed2(0.0, 1.0), _seed);
	            float _fl4 = rand(floor(_v * _freq) + fixed2(1.0, 1.0), _seed);
	            fixed2 _fr = frac(_v * _freq);

	            float _r1 = cos_interpolate(_fl1, _fl2, _fr.x);
	            float _r2 = cos_interpolate(_fl3, _fl4, _fr.x);
	            return cos_interpolate(_r1, _r2, _fr.y);
            }

            float perlin_noise(fixed2 _pos, float _seed, float _freq_start, float _amp_start, float _amp_ratio)
            {
	            float _freq = _freq_start;
	            float _amp = _amp_start;
	            float _pn = noise(_pos, _seed, fixed2(_freq, _freq)) * _amp;
	
	            for (int i=0; i<4; i++)
	            {
		            _freq *= 2.0;
		            _amp *= _amp_ratio;
		            _pn += (noise(_pos, _seed, fixed2(_freq, _freq)) * 2.0 - 1.0) * _amp;
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
	            float _pn = perlin_noise(_input_fd.uv / u_scope + u_offset, 0.0, 1.5, 1.0, 0.35);
	            fixed4 _output_col =  fixed4(_pn * u_color.r, _pn * u_color.g, _pn * u_color.b, u_color.a);

                return _output_col;
            }

            ENDCG
        }
    }

    FallBack "Diffuse"
}
