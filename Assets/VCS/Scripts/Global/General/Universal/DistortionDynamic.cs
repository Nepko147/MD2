using System;
using UnityEngine;

public class Universal_DistortionDynamic : MonoBehaviour
{
    public static Universal_DistortionDynamic SingleOnScene { get; private set; }

    public bool Active { get; set; }

    private float screenWidth;
    private float screenHeight;

    private new Camera camera;

    [SerializeField] private Material   distortion_material;
    private const string                DISTORION_MATERIAL_U_ASPECT = "u_aspect";
    private const string                DISTORION_MATERIAL_U_TEXNORMALMAP = "u_texNormalMap";

    [SerializeField] private Material   normalMapMix_material;
    private const string                NORMALMAPMIX_MATERIAL_OVERLAYTEX = "_OverlayTex";
    private const string                NORMALMAPMIX_MATERIAL_OVERLAYTEXALPHA = "_OverlayTexApha";
    private const string                NORMALMAPMIX_MATERIAL_OVERLAYTEXPOSX = "_OverlayTexPosX";
    private const string                NORMALMAPMIX_MATERIAL_OVERLAYTEXPOSY = "_OverlayTexPosY";
    private const string                NORMALMAPMIX_MATERIAL_OVERLAYTEXSCALE = "_OverlayTexScale";
    private const string                NORMALMAPMIX_MATERIAL_ASPECTX = "_AspectX";
    private const string                NORMALMAPMIX_MATERIAL_ASPECTY = "_AspectY";

    [SerializeField] private Texture2D  normalMapMix_material_normalMap_clear;

    public bool                         NormalMapMix_Material_NormalMap_CoinRush_Active { get; private set; }
    [SerializeField] private Sprite     normalMapMix_material_normalMap_coinRush_sprite;
    private Texture2D                   normalMapMix_material_normalMap_coinRush_texture;
    [SerializeField] private float      normalMapMix_material_normalMap_coinRush_visualDiameterInPixels = 120f;
    private float                       normalMapMix_material_normalMap_coinRush_scale = 0;
    private const float                 NORMALMAPMIX_MATERIAL_NORMALMAP_COINRUSH_SCALE_MAX = 4f;
    private const float                 NORMALMAPMIX_MATERIAL_NORMALMAP_COINRUSH_SCALE_STEP = NORMALMAPMIX_MATERIAL_NORMALMAP_COINRUSH_SCALE_MAX / NORMALMAPMIX_MATERIAL_NORMALMAP_COINRUSH_TIME_MAX;
    private float                       normalMapMix_material_normalMap_coinRush_time = 0;
    private const float                 NORMALMAPMIX_MATERIAL_NORMALMAP_COINRUSH_TIME_MAX = 2f;
    public Vector3                      NormalMapMix_Material_NormalMap_CoinRush_WorldPos { get; private set; }
    private float                       normalMapMix_material_normalMap_coinRush_screenPos_x;
    private float                       normalMapMix_material_normalMap_coinRush_screenPos_y;

    [SerializeField] private Texture2D  normalMapMix_material_normalMap_gameOver;
    private bool                        normalMapMix_material_normalMap_gameOver_Active = false;
    private bool                        normalMapMix_material_normalMap_gameOver_updated = false;

    private RenderTexture               normalMapMix_material_normalMap_output_renderTexture;
    private Rect                        normalMapMix_material_normalMap_output_rect;
    private Texture2D                   normalMapMix_material_normalMap_output_texture2d;

    public void CoinRush(Vector3 _position)
    {
        NormalMapMix_Material_NormalMap_CoinRush_Active = true;
        NormalMapMix_Material_NormalMap_CoinRush_WorldPos = _position;
        Vector2 _screenPosition = camera.WorldToScreenPoint(_position);
        normalMapMix_material_normalMap_coinRush_screenPos_x = _screenPosition.x / screenWidth;
        normalMapMix_material_normalMap_coinRush_screenPos_y = _screenPosition.y / screenHeight;
    }

    public float CoinRush_Distance_Get()
    {
        var _visualDiameterInWorld = normalMapMix_material_normalMap_coinRush_visualDiameterInPixels / normalMapMix_material_normalMap_coinRush_sprite.pixelsPerUnit;
        return (_visualDiameterInWorld * normalMapMix_material_normalMap_coinRush_scale);
    }

    public void GameOver()
    {
        normalMapMix_material_normalMap_gameOver_Active = true;
    }

    private void Awake()
    {
        SingleOnScene = this;

        Active = true;

        screenWidth = Screen.width;
        screenHeight = Screen.height;

        camera = GetComponent<Camera>();

        distortion_material.SetTexture(DISTORION_MATERIAL_U_TEXNORMALMAP, normalMapMix_material_normalMap_clear); //ѕомещаем пустую нормаль в шейдере искажени€

        NormalMapMix_Material_NormalMap_CoinRush_Active = false;
        var _distortion_material_aspect = (float)normalMapMix_material_normalMap_clear.height / normalMapMix_material_normalMap_clear.width;
        distortion_material.SetFloat(DISTORION_MATERIAL_U_ASPECT, _distortion_material_aspect);
        normalMapMix_material_normalMap_coinRush_texture = normalMapMix_material_normalMap_coinRush_sprite.texture;

        normalMapMix_material_normalMap_output_renderTexture = new RenderTexture(normalMapMix_material_normalMap_clear.width, normalMapMix_material_normalMap_clear.height, 32); //ѕодготавливаем рендер-текстуру, в которой будем собирать нормаль дл€ шейдера искажени€ из пустой нормали и нормали искажени€
        normalMapMix_material_normalMap_output_rect = new Rect(0, 0, normalMapMix_material_normalMap_output_renderTexture.width, normalMapMix_material_normalMap_output_renderTexture.height); //GETRECT!
        normalMapMix_material_normalMap_output_texture2d = new Texture2D(normalMapMix_material_normalMap_clear.width, normalMapMix_material_normalMap_clear.height, TextureFormat.RGBA32, false); //ѕодготавливаем 2D-текстуру, в которую будем помещать нормаль дл€ шейдера искажени€ собранную в рендер-текстуре
    }

    private void OnRenderImage(RenderTexture _source, RenderTexture _destination)
    {
        if (Active)
        {
            if (normalMapMix_material_normalMap_gameOver_Active)
            {
                if (!normalMapMix_material_normalMap_gameOver_updated)
                {
                    distortion_material.SetTexture(DISTORION_MATERIAL_U_TEXNORMALMAP, normalMapMix_material_normalMap_gameOver); //ѕомещаем нормаль искажени€ Game Over'а в шейдер искажени€
                    normalMapMix_material_normalMap_gameOver_updated = true;
                }
            }
            else
            {
                if (NormalMapMix_Material_NormalMap_CoinRush_Active)
                {
                    normalMapMix_material.SetTexture(NORMALMAPMIX_MATERIAL_OVERLAYTEX, normalMapMix_material_normalMap_coinRush_texture);
                    float _alpha = 1f - normalMapMix_material_normalMap_coinRush_scale / NORMALMAPMIX_MATERIAL_NORMALMAP_COINRUSH_SCALE_MAX;
                    _alpha = Mathf.Clamp(_alpha, 0, 1f);
                    normalMapMix_material.SetFloat(NORMALMAPMIX_MATERIAL_OVERLAYTEXALPHA, _alpha);
                    normalMapMix_material.SetFloat(NORMALMAPMIX_MATERIAL_OVERLAYTEXPOSX, normalMapMix_material_normalMap_coinRush_screenPos_x);
                    normalMapMix_material.SetFloat(NORMALMAPMIX_MATERIAL_OVERLAYTEXPOSY, normalMapMix_material_normalMap_coinRush_screenPos_y);
                    normalMapMix_material.SetFloat(NORMALMAPMIX_MATERIAL_OVERLAYTEXSCALE, normalMapMix_material_normalMap_coinRush_scale);
                    normalMapMix_material.SetFloat(NORMALMAPMIX_MATERIAL_ASPECTX, screenWidth);
                    normalMapMix_material.SetFloat(NORMALMAPMIX_MATERIAL_ASPECTY, screenHeight);
                    Graphics.Blit(normalMapMix_material_normalMap_clear, normalMapMix_material_normalMap_output_renderTexture, normalMapMix_material); //—овмещаем пустую нормаль с нормалью искажени€ Coin Rush'а через шейдер совмещени€ нормалей. «атем, помещаем результат в подготовленную рендер-тектуру 

                    normalMapMix_material_normalMap_output_texture2d.ReadPixels(normalMapMix_material_normalMap_output_rect, 0, 0, false); //ѕомещаем пиксели с подготовленной рендер-текстуры в поле Colors подготовленной 2D-текстуры
                    normalMapMix_material_normalMap_output_texture2d.Apply(); //ќтрисовываем на подготовленной 2D-текстуре, помещЄнные в еЄ поле Colors пиксели, с получением нормали дл€ шейдера искажени€, котора€ совмещает в себе пустую нормаль и нормаль искажени€ Coin Rush'а

                    distortion_material.SetTexture(DISTORION_MATERIAL_U_TEXNORMALMAP, normalMapMix_material_normalMap_output_texture2d); //ѕомещаем нормаль из подготовленной 2D-текстуры в шейдер искажени€

                    normalMapMix_material_normalMap_coinRush_scale += NORMALMAPMIX_MATERIAL_NORMALMAP_COINRUSH_SCALE_STEP * Time.deltaTime;
                    normalMapMix_material_normalMap_coinRush_time += Time.deltaTime;

                    if (normalMapMix_material_normalMap_coinRush_time >= NORMALMAPMIX_MATERIAL_NORMALMAP_COINRUSH_TIME_MAX)
                    {
                        normalMapMix_material_normalMap_coinRush_scale = 0;
                        normalMapMix_material_normalMap_coinRush_time = 0;
                        NormalMapMix_Material_NormalMap_CoinRush_Active = false;
                    }
                }
            }
        }

        Graphics.Blit(_source, _destination, distortion_material); //»скажаем рендер-текстуру с камеры текущего GameObject'а через шейдер искажени€. «атем помещаем результат в рендер-текстуру камеры текущего GameObject'а
    }
}
