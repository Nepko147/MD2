Shader "Custom/Camera_ZoomBlur"
{
    Properties
    {
        u_surf_dim("Surface Dimentions", Vector) = (1, 1, 0, 0)
        u_center("Center", Vector) = (0.5, 0.5, 0, 0)
        u_intensity("Intensity", Float) = 0.5
        u_focus_radius("Focus Radius", Float) = 1
        u_texel_size("Texel Size", Vector) = (1, 1, 0, 0)
        u_texture_noise ("Texture Noise", 2D) = "white" {}
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

            #define FADE 0.5
            #define SAMPLES 64
            #define STEP_SIZE 0.015625

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float2 u_surf_dim;
            float2 u_center;
            float u_intensity;
            float u_focus_radius;
            float2 u_texel_size;
            sampler2D u_texture_noise;

            struct vertex_data 
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct fragment_data 
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float2 uv_texture_noise : TEXCOORD1;
            };

            fragment_data vert(vertex_data _input_vd)
            {
                fragment_data _output_fd;

                _output_fd.pos = UnityObjectToClipPos(_input_vd.pos);
                _output_fd.uv = _input_vd.uv;

                _output_fd.uv_texture_noise = _input_vd.uv;

                return (_output_fd);
            }

            fixed4 frag(fragment_data _input_fd) : SV_Target
            {
                float2 _uv = _input_fd.uv;

                float4 _samples_total = float4(0.0, 0.0, 0.0, 0.0);
	            float _weights_total = 0.0;
	
	            float2 _pixel_coord = _uv * u_surf_dim;
	            float2 _pixel_center = u_center * u_surf_dim;
	
	            float2 _noise_texcoord = _pixel_coord * u_texel_size;
	            float _noise = tex2D(u_texture_noise, _noise_texcoord).r;
	
	            float _step_size = u_intensity * STEP_SIZE;
	            float _focus_dist = length(_pixel_coord - _pixel_center);
	            _step_size *= smoothstep(0.0, u_focus_radius, _focus_dist);
	
	            float _pos = _noise * _step_size;
	            float _fade = _noise * STEP_SIZE;
	
	            for (int _i = 0; _i < SAMPLES; _i++)
	            {
		            float2 _sample_texcoord = lerp(_uv, u_center, _pos);
		            fixed4 _sample = tex2D(_MainTex, _sample_texcoord);
		            _sample *= _sample;
		            float _weight = 1.0 - _fade * _fade * FADE;
		
		            _samples_total += _sample * _weight;
		            _weights_total += _weight;
		
		            _pos += _step_size;
		            _fade += STEP_SIZE;
	            }

                return (sqrt(_samples_total / _weights_total));
            }

            ENDCG
        }
    }
    
    FallBack "Diffuse"
}
