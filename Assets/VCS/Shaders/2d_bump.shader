﻿Shader "Custom/Sprites/2D Bump"
{
    Properties
    {        
        _Color ("Color", Color) = (1, 1, 1, 1)
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _BumpMap ("Normalmap", 2D) = "bump" {}
        _BumpIntensity ("NormalMap Intensity", Range (-1, 2)) = 1
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
    }

    SubShader
    {
        Tags
        { 
            "Queue"="AlphaTest" 
            "IgnoreProjector"="True" 
            "RenderType"="TransparentCutOut" 
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        LOD 300

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting On
        ZWrite On
        Fog { Mode Off }

        CGPROGRAM

        #pragma target 3.0
        #pragma surface surf Lambert alpha vertex:vert
        #pragma multi_compile DUMMY PIXELSNAP_ON 
        #pragma exclude_renderers flash

        sampler2D _MainTex;
        sampler2D _BumpMap;
        fixed _BumpIntensity;
        
        fixed4 _Color;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
            fixed4 color;
        };
        
        void vert (inout appdata_full v, out Input o)
        {
            #if defined(PIXELSNAP_ON) && !defined(SHADER_API_FLASH)
            v.vertex = UnityPixelSnap (v.vertex);
            #endif
            v.normal = float3(0,0,-1);
            v.tangent =  float4(1, 0, 0, 1);
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.color = _Color;
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * IN.color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            _BumpIntensity = 1 / _BumpIntensity;
            o.Normal.z = o.Normal.z * _BumpIntensity;
            o.Normal = normalize((half3)o.Normal);
        }

        ENDCG
    }

    Fallback "Transparent/Cutout/Diffuse"
}