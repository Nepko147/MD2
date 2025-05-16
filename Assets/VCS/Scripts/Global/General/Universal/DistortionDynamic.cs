using UnityEngine;

public class Universal_DistortionDynamic : MonoBehaviour
{
    public static Universal_DistortionDynamic Singletone { get; private set; }

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
    [SerializeField] Texture2D  normalMapMix_material_baseNormalMap;
    [SerializeField] Texture2D  normalMapMix_material_gameOverNormalMap;
    [SerializeField] Texture2D  normalMapMix_material_distorionNormalMap;
    [SerializeField] float      normalMapMix_material_distortionSpeed;
    public float                NormalMapMix_Material_Pos_X { get; set; }
    public float                NormalMapMix_Material_Pos_Y { get; set; }
    public bool                 NormalMapMix_Material_Active { get; set; }
    float                       normalMapMix_material_timer;
    [SerializeField] float      normalMapMix_material_timer_max;
    RenderTexture               normalMapMix_material_output_normalMap_renderTexture;
    Texture2D                   normalMapMix_material_output_normalMap_texture2d;
    Rect                        normalMapMix_material_output_normalMap_rect;

    public void WorldDistortion(Vector3 _position)
    {
        Vector2 _screenPosition = camera.WorldToScreenPoint(_position);
        NormalMapMix_Material_Active = true;
        NormalMapMix_Material_Pos_X = _screenPosition.x / screenWidth;
        NormalMapMix_Material_Pos_Y = _screenPosition.y / screenHeight;
    }


    private void Awake()
    {
        Singletone = this;
        
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        camera = GetComponent<Camera>();
        var _distortion_material_aspect = (float)normalMapMix_material_baseNormalMap.height / normalMapMix_material_baseNormalMap.width;
        distortion_material.SetFloat(DISTORION_MATERIAL_U_ASPECT, _distortion_material_aspect);
            
        normalMapMix_material_output_normalMap_renderTexture = new RenderTexture(normalMapMix_material_baseNormalMap.width, normalMapMix_material_baseNormalMap.height, 32); //ѕодготавливаем рендер-текстуру, в которой будем собирать нормалку из фона и кружочка
        Graphics.Blit(normalMapMix_material_baseNormalMap, normalMapMix_material_output_normalMap_renderTexture, normalMapMix_material); // ќтрисовываем пустую на текстуру пустую нормаль. «атем, помещаем результат в подготовленную рендер-тектуру       
        normalMapMix_material_output_normalMap_rect = new Rect(0, 0, normalMapMix_material_output_normalMap_renderTexture.width, normalMapMix_material_output_normalMap_renderTexture.height); //GETRECT!
        
        normalMapMix_material_output_normalMap_texture2d = new Texture2D(normalMapMix_material_baseNormalMap.width, normalMapMix_material_baseNormalMap.height, TextureFormat.RGBA32, false); //ѕодготавливаем 2D-текстуру
        normalMapMix_material_output_normalMap_texture2d.ReadPixels(normalMapMix_material_output_normalMap_rect, 0, 0); //ѕомещаем пиксЁлы с подготовленной рендер-текстуры в поле Colors 2D-текстуры
        normalMapMix_material_output_normalMap_texture2d.Apply(); //ќтрисовываем на 2D-текстуре, помещЄнные в еЄ поле Colors пиксЁлы
    }

    private void OnRenderImage(RenderTexture _source, RenderTexture _destination)
    {
        if (NormalMapMix_Material_Active)
        {
            normalMapMix_material.SetTexture(NORMALMAPMIX_MATERIAL_OVERLAYTEX, normalMapMix_material_distorionNormalMap); 
            float _alpha = 1 - normalMapMix_material_timer / normalMapMix_material_timer_max;
            _alpha = Mathf.Clamp(_alpha, 0, 1);
            normalMapMix_material.SetFloat(NORMALMAPMIX_MATERIAL_OVERLAYTEXALPHA, _alpha);                      
            normalMapMix_material.SetFloat(NORMALMAPMIX_MATERIAL_OVERLAYTEXPOSX, NormalMapMix_Material_Pos_X);  
            normalMapMix_material.SetFloat(NORMALMAPMIX_MATERIAL_OVERLAYTEXPOSY, NormalMapMix_Material_Pos_Y);  
            normalMapMix_material.SetFloat(NORMALMAPMIX_MATERIAL_OVERLAYTEXSCALE, normalMapMix_material_timer * normalMapMix_material_distortionSpeed);
            normalMapMix_material.SetFloat(NORMALMAPMIX_MATERIAL_ASPECTX, screenWidth);
            normalMapMix_material.SetFloat(NORMALMAPMIX_MATERIAL_ASPECTY, screenHeight);
            Graphics.Blit(normalMapMix_material_baseNormalMap, normalMapMix_material_output_normalMap_renderTexture, normalMapMix_material); //—овмещаем пустую нормаль с нормалью искажени€. «атем, помещаем результат в подготовленную рендер-тектуру 
            
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
        if (ControlScene_Entity_Main.Singletone.GameOver)
        {
            _normalMap = normalMapMix_material_gameOverNormalMap;
        }
        
        distortion_material.SetTexture(DISTORION_MATERIAL_U_TEXNORMALMAP, _normalMap);
        Graphics.Blit(_source, _destination, distortion_material); //»скажаем рендер-текстуру с камеры текущего GameObject'а. «атем помещаем результат в рендер-текстуру камеры текущего GameObject'а
    }
}
