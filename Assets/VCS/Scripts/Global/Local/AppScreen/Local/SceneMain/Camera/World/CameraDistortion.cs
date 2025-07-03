using UnityEngine;

public class AppScreen_Local_SceneMain_Camera_World_CameraDistortion : MonoBehaviour
{
    public static AppScreen_Local_SceneMain_Camera_World_CameraDistortion SingleOnScene { get; private set; }

    public bool Active { get; set; }

    private new Camera camera;

    [SerializeField] private Material distortion_material;
    private const string DISTORION_MATERIAL_U_ASPECT = "_MainNormalMap_aspect";
    private const string DISTORION_MATERIAL_U_TEXNORMALMAP = "_MainNormalMap";

    private const string OVERLAYNORMALMAP = "_OverlayNormalMap";
    private const string OVERLAYNORMALMAP_ALPHA = "_OverlayNormalMap_aplha";
    private const string OVERLAYNORMALMAP_POS_X = "_OverlayNormalMap_pos_X";
    private const string OVERLAYNORMALMAP_POS_Y = "_OverlayNormalMap_pos_Y";
    private const string OVERLAYNORMALMAP_SCALE = "_OverlayNormalMap_Scale";
    [SerializeField] private Sprite overlayNormalMap_sprite;
    [SerializeField] private float overlayNormalMap_coinRush_visualDiameterInPixels = 120f;
    private float overlayNormalMap_coinRush_scale = 0;
    private const float OVERLAYNORMALMAP_COINRUSH_SCALE_MAX = 4f;
    private const float OVERLAYNORMALMAP_COINRUSH_SCALE_STEP = OVERLAYNORMALMAP_COINRUSH_SCALE_MAX / OVERLAYNORMALMAP_COINRUSH_TIME_MAX;
    private float overlayNormalMap_coinRush_time = 0;
    private const float OVERLAYNORMALMAP_COINRUSH_TIME_MAX = 2f;
    private float overlayNormalMap_coinRush_screenPos_x;
    private float overlayNormalMap_coinRush_screenPos_y;

    private const string SCREEN_WIDTH = "_Screen_widht";
    private const string SCREEN_HEIGHT = "_Screen_height";

    [SerializeField] private Texture2D normalMap_clear;
    [SerializeField] private Texture2D normalMap_gameOver;
    private bool normalMap_gameOver_Active = false;
    private bool normalMap_gameOver_updated = false;

    public bool NormalMapMix_Material_NormalMap_CoinRush_Active { get; private set; }

    public Vector3 NormalMapMix_Material_NormalMap_CoinRush_WorldPos { get; private set; }

    public void CoinRush(Vector3 _position)
    {
        NormalMapMix_Material_NormalMap_CoinRush_Active = true;
        NormalMapMix_Material_NormalMap_CoinRush_WorldPos = _position;
        Vector2 _screenPosition = camera.WorldToScreenPoint(_position);
        overlayNormalMap_coinRush_screenPos_x = _screenPosition.x / Screen.width;
        overlayNormalMap_coinRush_screenPos_y = _screenPosition.y / Screen.height;
    }

    public float CoinRush_Distance_Get()
    {
        var _visualDiameterInWorld = overlayNormalMap_coinRush_visualDiameterInPixels / overlayNormalMap_sprite.pixelsPerUnit;
        return (_visualDiameterInWorld * overlayNormalMap_coinRush_scale);
    }

    public void GameOver()
    {
        normalMap_gameOver_Active = true;
    }

    private void Awake()
    {
        SingleOnScene = this;

        Active = true;
        NormalMapMix_Material_NormalMap_CoinRush_Active = false;

        camera = GetComponent<Camera>();
    }

    private void Start()
    {
        distortion_material.SetTexture(DISTORION_MATERIAL_U_TEXNORMALMAP, normalMap_clear); //ѕомещаем пустую нормаль в шейдере искажени€
        distortion_material.SetTexture(OVERLAYNORMALMAP, overlayNormalMap_sprite.texture);
        var _distortion_material_aspect = (float)normalMap_clear.height / normalMap_clear.width;
        distortion_material.SetFloat(DISTORION_MATERIAL_U_ASPECT, _distortion_material_aspect);
        distortion_material.SetFloat(OVERLAYNORMALMAP_SCALE, overlayNormalMap_coinRush_scale);
    }

    private void Update()
    {
        if (Active)
        {
            if (normalMap_gameOver_Active)
            {
                if (!normalMap_gameOver_updated)
                {
                    distortion_material.SetTexture(DISTORION_MATERIAL_U_TEXNORMALMAP, normalMap_gameOver); //ѕомещаем нормаль искажени€ Game Over'а в шейдер искажени€
                    normalMap_gameOver_updated = true;
                }
            }
            else
            {
                if (NormalMapMix_Material_NormalMap_CoinRush_Active)
                {
                    float _alpha = 1f - overlayNormalMap_coinRush_scale / OVERLAYNORMALMAP_COINRUSH_SCALE_MAX;
                    _alpha = Mathf.Clamp(_alpha, 0, 1f);

                    distortion_material.SetFloat(OVERLAYNORMALMAP_ALPHA, _alpha);
                    distortion_material.SetFloat(OVERLAYNORMALMAP_POS_X, overlayNormalMap_coinRush_screenPos_x);
                    distortion_material.SetFloat(OVERLAYNORMALMAP_POS_Y, overlayNormalMap_coinRush_screenPos_y);
                    distortion_material.SetFloat(OVERLAYNORMALMAP_SCALE, overlayNormalMap_coinRush_scale);
                    distortion_material.SetFloat(SCREEN_WIDTH, Screen.width);
                    distortion_material.SetFloat(SCREEN_HEIGHT, Screen.height);

                    overlayNormalMap_coinRush_scale += OVERLAYNORMALMAP_COINRUSH_SCALE_STEP * Time.deltaTime;
                    overlayNormalMap_coinRush_time += Time.deltaTime;

                    if (overlayNormalMap_coinRush_time >= OVERLAYNORMALMAP_COINRUSH_TIME_MAX)
                    {
                        _alpha = 0;
                        distortion_material.SetFloat(OVERLAYNORMALMAP_ALPHA, _alpha);
                        overlayNormalMap_coinRush_scale = 0;
                        distortion_material.SetFloat(OVERLAYNORMALMAP_SCALE, overlayNormalMap_coinRush_scale);
                        overlayNormalMap_coinRush_time = 0;
                        NormalMapMix_Material_NormalMap_CoinRush_Active = false;
                    }
                }
            }
        }
    }

    private void OnRenderImage(RenderTexture _source, RenderTexture _destination)
    {
        Graphics.Blit(_source, _destination, distortion_material); //»скажаем рендер-текстуру с камеры текущего GameObject'а через шейдер искажени€. «атем помещаем результат в рендер-текстуру камеры текущего GameObject'а
    }
}
