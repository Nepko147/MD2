using UnityEngine;

public class AppScreen_General_Camera_GlobalLightning_Entity : MonoBehaviour
{
    public static AppScreen_General_Camera_GlobalLightning_Entity SingleOnScene { get; private set; }

    public Color Color_Top
    {
        get
        {
            return light_top.color;
        }
        set
        {
            light_top.color = value;
        }
    }

    [SerializeField] private Light light_top; 

    private Vector3 position_init;
    private Vector3 position_buffer;
    private float position_z_zoomOfs;

    public void Position_Z_ZoomOfs_Road()
    {
        position_z_zoomOfs = 3.85f;
    }

    public void Position_Z_ZoomOfs_Drift()
    {
        position_z_zoomOfs = 1.15f;
    }

    private void Awake()
    {
        SingleOnScene = this;

        position_init = transform.position;
        Position_Z_ZoomOfs_Road();
    }

    private void Update()
    {
        position_buffer = transform.position;
        var _scale = AppScreen_General_Camera_Entity.SingleOnScene.transform.position.z / AppScreen_General_Camera_Entity.SingleOnScene.Position_Init.z;
        position_buffer.z = position_init.z * _scale + position_z_zoomOfs * (1f - _scale);
        transform.position = position_buffer;
    }
}
