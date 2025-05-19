using UnityEngine;

public class Universal_DistortionDynamic : MonoBehaviour
{
    public static Universal_DistortionDynamic SingleOnScene { get; private set; }

    public bool GameOver { get; set; }

    float screenWidth;
    float screenHeight;

    new Camera camera;

    [SerializeField] Material   distortion_material;
    const string                DISTORION_MATERIAL_U_ASPECT = "u_aspect";
    const string                DISTORION_MATERIAL_U_TEXNORMALMAP = "u_texNormalMap";

    [SerializeField] Material   normalMapMix_material;
    const string                NORMALMAPMIX_MATERIAL_OVERLAYTEX = "_OverlayTex";
    const string                NORMALMAPMIX_MATERIAL_OVERLAYTEXALPHA = "_OverlayTexApha";
    const string                NORMALMAPMIX_MATERIAL_OVERLAYTEXPOSX = "_OverlayTexPosX";
    const string                NORMALMAPMIX_MATERIAL_OVERLAYTEXPOSY = "_OverlayTexPosY";
    const string                NORMALMAPMIX_MATERIAL_OVERLAYTEXSCALE = "_OverlayTexScale";
    const string                NORMALMAPMIX_MATERIAL_ASPECTX = "_AspectX";
    const string                NORMALMAPMIX_MATERIAL_ASPECTY = "_AspectY";
    [SerializeField] Texture2D  normalMapMix_material_clearNormalMap;
    [SerializeField] Texture2D  normalMapMix_material_gameOverNormalMap;
    [SerializeField] Sprite     normalMapMix_material_distorionNormalMap_sprite;
    Texture2D                   normalMapMix_material_distorionNormalMap_texture;
    [SerializeField] float      normalMapMix_material_distorionNormalMap_visualDiameterInPixels;
    [SerializeField] float      normalMapMix_material_distortionScale;
    public bool                 NormalMapMix_Material_Active { get; set; }
    public Vector3              NormalMapMix_Material_WorldPos { get; set; }
    public float                NormalMapMix_Material_ScreenPos_X { get; set; }
    public float                NormalMapMix_Material_ScreenPos_Y { get; set; }
    float                       normalMapMix_material_timer;
    [SerializeField] float      normalMapMix_material_timer_max;
    RenderTexture               normalMapMix_material_output_normalMap_renderTexture;
    Texture2D                   normalMapMix_material_output_normalMap_texture2d;
    Rect                        normalMapMix_material_output_normalMap_rect;


    public void WorldDistortion(Vector3 _position)
    {
        NormalMapMix_Material_Active = true;
        NormalMapMix_Material_WorldPos = _position;
        Vector2 _screenPosition = camera.WorldToScreenPoint(_position);
        NormalMapMix_Material_ScreenPos_X = _screenPosition.x / screenWidth;
        NormalMapMix_Material_ScreenPos_Y = _screenPosition.y / screenHeight;
    }

    public float DistortionDistance_Get()
    {
        var _worldDistance = normalMapMix_material_distorionNormalMap_visualDiameterInPixels / normalMapMix_material_distorionNormalMap_sprite.pixelsPerUnit;
        return (_worldDistance * normalMapMix_material_timer * normalMapMix_material_distortionScale);
    }

    private void Awake()
    {
        SingleOnScene = this;

        GameOver = false;

        screenWidth = Screen.width;
        screenHeight = Screen.height;

        camera = GetComponent<Camera>();

        var _distortion_material_aspect = (float)normalMapMix_material_clearNormalMap.height / normalMapMix_material_clearNormalMap.width;
        distortion_material.SetFloat(DISTORION_MATERIAL_U_ASPECT, _distortion_material_aspect);

        normalMapMix_material_distorionNormalMap_texture = normalMapMix_material_distorionNormalMap_sprite.texture;
        normalMapMix_material_output_normalMap_renderTexture = new RenderTexture(normalMapMix_material_clearNormalMap.width, normalMapMix_material_clearNormalMap.height, 32); //ѕодготавливаем рендер-текстуру, в которой будем собирать нормалку из фона и кружочка
        Graphics.Blit(normalMapMix_material_clearNormalMap, normalMapMix_material_output_normalMap_renderTexture, normalMapMix_material); // ќтрисовываем пустую на текстуру пустую нормаль. «атем, помещаем результат в подготовленную рендер-тектуру       
        normalMapMix_material_output_normalMap_rect = new Rect(0, 0, normalMapMix_material_output_normalMap_renderTexture.width, normalMapMix_material_output_normalMap_renderTexture.height); //GETRECT!
        normalMapMix_material_output_normalMap_texture2d = new Texture2D(normalMapMix_material_clearNormalMap.width, normalMapMix_material_clearNormalMap.height, TextureFormat.RGBA32, false); //ѕодготавливаем 2D-текстуру
        normalMapMix_material_output_normalMap_texture2d.ReadPixels(normalMapMix_material_output_normalMap_rect, 0, 0); //ѕомещаем пиксЁлы с подготовленной рендер-текстуры в поле Colors 2D-текстуры
        normalMapMix_material_output_normalMap_texture2d.Apply(); //ќтрисовываем на 2D-текстуре, помещЄнные в еЄ поле Colors пиксЁлы
    }

    private void OnRenderImage(RenderTexture _source, RenderTexture _destination)
    {
        if (NormalMapMix_Material_Active)
        {
            normalMapMix_material.SetTexture(NORMALMAPMIX_MATERIAL_OVERLAYTEX, normalMapMix_material_distorionNormalMap_texture); 
            float _alpha = 1 - normalMapMix_material_timer / normalMapMix_material_timer_max;
            _alpha = Mathf.Clamp(_alpha, 0, 1);
            normalMapMix_material.SetFloat(NORMALMAPMIX_MATERIAL_OVERLAYTEXALPHA, _alpha);                      
            normalMapMix_material.SetFloat(NORMALMAPMIX_MATERIAL_OVERLAYTEXPOSX, NormalMapMix_Material_ScreenPos_X);  
            normalMapMix_material.SetFloat(NORMALMAPMIX_MATERIAL_OVERLAYTEXPOSY, NormalMapMix_Material_ScreenPos_Y);  
            normalMapMix_material.SetFloat(NORMALMAPMIX_MATERIAL_OVERLAYTEXSCALE, normalMapMix_material_timer * normalMapMix_material_distortionScale);
            normalMapMix_material.SetFloat(NORMALMAPMIX_MATERIAL_ASPECTX, screenWidth);
            normalMapMix_material.SetFloat(NORMALMAPMIX_MATERIAL_ASPECTY, screenHeight);
            Graphics.Blit(normalMapMix_material_clearNormalMap, normalMapMix_material_output_normalMap_renderTexture, normalMapMix_material); //—овмещаем пустую нормаль с нормалью искажени€. «атем, помещаем результат в подготовленную рендер-тектуру 
            
            normalMapMix_material_output_normalMap_texture2d.ReadPixels(normalMapMix_material_output_normalMap_rect, 0, 0, false); //ѕомещаем пиксЁлы с подготовленной рендер-текстуры в поле Colors 2D-текстуры
            normalMapMix_material_output_normalMap_texture2d.Apply(); //ќтрисовываем на 2D-текстуре, помещЄнные в еЄ поле Colors пиксЁлы

            normalMapMix_material_timer += Time.deltaTime;
            
            if (normalMapMix_material_timer >= normalMapMix_material_timer_max)
            {
                NormalMapMix_Material_Active = false;
                normalMapMix_material_timer = 0;
            }        
        }

        var _normalMap = normalMapMix_material_output_normalMap_texture2d;
        if (GameOver)
        {
            _normalMap = normalMapMix_material_gameOverNormalMap;
        }
        
        distortion_material.SetTexture(DISTORION_MATERIAL_U_TEXNORMALMAP, _normalMap);
        Graphics.Blit(_source, _destination, distortion_material); //»скажаем рендер-текстуру с камеры текущего GameObject'а. «атем помещаем результат в рендер-текстуру камеры текущего GameObject'а
    }
}
