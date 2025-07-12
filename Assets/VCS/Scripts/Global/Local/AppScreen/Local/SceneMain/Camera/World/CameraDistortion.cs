using UnityEngine;

public class AppScreen_Local_SceneMain_Camera_World_CameraDistortion : MonoBehaviour
{
    public static AppScreen_Local_SceneMain_Camera_World_CameraDistortion SingleOnScene { get; private set; }

    private new Camera camera;

    [SerializeField] private Material material;

    private const string MATERIAL_U_MAIN_NORMALMAP = "u_main_normalMap";
    private const string MATERIAL_U_OVERLAY_NORMALMAP = "u_overlay_normalMap";
    private const string MATERIAL_U_OVERLAY_SCREENPOS = "u_overlay_screenPos";
    private const string MATERIAL_U_OVERLAY_SCALE = "u_overlay_scale";
    private const string MATERIAL_U_OVERLAY_ALPHA = "u_overlay_aplha";

    private Texture2D material_main_normalMap_clear;
    [SerializeField] private Texture2D material_main_normalMap_gameOver;

    public void Material_Main_NormalMap_GameOver_Apply()
    {
        material.SetTexture(MATERIAL_U_MAIN_NORMALMAP, material_main_normalMap_gameOver);
    }

    public bool Material_Overlay_Active { get; set; }

    [SerializeField] private Texture2D material_overlay_normalMap_coinRush;
    [SerializeField] private float material_overlay_normalMap_coinRush_visualDiameterInPixels = 120f;
    public bool Material_Overlay_NormalMap_CoinRish_Active { get; set; }
    public Vector3 Material_Overlay_NormalMap_CoinRush_WorldPos { get; private set; }
    private Vector4 material_overlay_normalMap_coinRush_screenPos_normalized = Vector2.zero;
    private float material_overlay_normalMap_coinRush_time = 0;
    private const float MATERIAL_OVERLAY_NORMALMAP_COINRUSH_TIME_MAX = 2f;
    private float material_overlay_normalMap_coinRush_scale = 0;
    private Vector4 material_overlay_normalMap_coinRush_scale_withAspect = Vector4.zero;
    private const float MATERIAL_OVERLAY_NORMALMAP_COINRUSH_SCALE_MAX = 8f;
    private const float MATERIAL_OVERLAY_NORMALMAP_COINRUSH_SCALE_STEP = MATERIAL_OVERLAY_NORMALMAP_COINRUSH_SCALE_MAX / MATERIAL_OVERLAY_NORMALMAP_COINRUSH_TIME_MAX;

    public void Material_Overlay_NormalMap_CoinRush_Start(Vector3 _position)
    {
        Material_Overlay_NormalMap_CoinRish_Active = true;
        Material_Overlay_NormalMap_CoinRush_WorldPos = _position;
        material_overlay_normalMap_coinRush_time = 0;
        material_overlay_normalMap_coinRush_scale = 0;
    }

    public float Material_Overlay_NormalMap_CoinRush_Distance_Get()
    {
        var _pos_screen_source = camera.WorldToScreenPoint(Material_Overlay_NormalMap_CoinRush_WorldPos);
        var _pos_screen_diameter = new Vector3(_pos_screen_source.x + material_overlay_normalMap_coinRush_visualDiameterInPixels * material_overlay_normalMap_coinRush_scale, _pos_screen_source.y, _pos_screen_source.z);
        var _pos_world_source = camera.ScreenToWorldPoint(_pos_screen_source);
        var _pos_world_diameter = camera.ScreenToWorldPoint(_pos_screen_diameter);
        var _visualDiameterInWorld = Mathf.Abs(_pos_world_diameter.x - _pos_world_source.x);
        
        return (_visualDiameterInWorld);
    }

    private void Awake()
    {
        SingleOnScene = this;

        camera = GetComponent<Camera>();

        Material_Overlay_Active = true;

        Material_Overlay_NormalMap_CoinRish_Active = false;
    }

    private void Start()
    {
        material_main_normalMap_clear = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        var _color = new Color(0.5f, 0.5f, 1f);
        material_main_normalMap_clear.SetPixel(0, 0, _color);
        material_main_normalMap_clear.Apply();
        material.SetTexture(MATERIAL_U_MAIN_NORMALMAP, material_main_normalMap_clear);

        material.SetTexture(MATERIAL_U_OVERLAY_NORMALMAP, material_overlay_normalMap_coinRush);
        material.SetFloat(MATERIAL_U_OVERLAY_ALPHA, 0);
    }

    private void Update()
    {
        if (Material_Overlay_Active
        && Material_Overlay_NormalMap_CoinRish_Active)
        {
            material_overlay_normalMap_coinRush_time += Time.deltaTime;

            Vector2 _screen_pos = camera.WorldToScreenPoint(Material_Overlay_NormalMap_CoinRush_WorldPos);
            material_overlay_normalMap_coinRush_screenPos_normalized.x = _screen_pos.x / Screen.width;
            material_overlay_normalMap_coinRush_screenPos_normalized.y = _screen_pos.y / Screen.height;
            material.SetVector(MATERIAL_U_OVERLAY_SCREENPOS, material_overlay_normalMap_coinRush_screenPos_normalized);

            material_overlay_normalMap_coinRush_scale += MATERIAL_OVERLAY_NORMALMAP_COINRUSH_SCALE_STEP * Time.deltaTime;
            material_overlay_normalMap_coinRush_scale_withAspect.x = material_overlay_normalMap_coinRush_scale * material_overlay_normalMap_coinRush.width / Screen.width;
            material_overlay_normalMap_coinRush_scale_withAspect.y = material_overlay_normalMap_coinRush_scale * material_overlay_normalMap_coinRush.height / Screen.height;
            material.SetVector(MATERIAL_U_OVERLAY_SCALE, material_overlay_normalMap_coinRush_scale_withAspect);

            var _alpha = Mathf.Clamp(1f - material_overlay_normalMap_coinRush_scale / MATERIAL_OVERLAY_NORMALMAP_COINRUSH_SCALE_MAX, 0, 1f);
            material.SetFloat(MATERIAL_U_OVERLAY_ALPHA, _alpha);

            if (material_overlay_normalMap_coinRush_time >= MATERIAL_OVERLAY_NORMALMAP_COINRUSH_TIME_MAX)
            {
                material.SetFloat(MATERIAL_U_OVERLAY_ALPHA, 0);
                Material_Overlay_NormalMap_CoinRish_Active = false;
            }
        }
    }

    private void OnRenderImage(RenderTexture _source, RenderTexture _destination)
    {
        Graphics.Blit(_source, _destination, material); //ѕримен€ем шейдер искажени€ к исходной рендер-текстуре камеры текущего GameObject'а. «атем помещаем результат в конечную рендер-текстуру камеры текущего GameObject'а
    }
}
