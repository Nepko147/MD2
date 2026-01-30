using UnityEngine;

public class AppScreen_Entity : MonoBehaviour
{
    public static AppScreen_Entity SingleOnScene { get; private set; }
    
    private const float MAINCAMERA_ASPECT_MIN = 4f / 3f;
    private const float MAINCAMERA_ASPECT_MAX = 22f / 9f;

    public float MainCamera_Aspect_Get()
    {
        return (Mathf.Clamp((float)Screen.width / (float)Screen.height, MAINCAMERA_ASPECT_MIN, MAINCAMERA_ASPECT_MAX));
    }

    public Rect MainCamera_Rect_Get()
    {
        float _screen_w = Screen.width;
        float _screen_h = Screen.height;

        var _mainCam_ofs_w = (_screen_w - _screen_w / (_screen_w / _screen_h / MAINCAMERA_ASPECT_MAX)) / _screen_w;
        _mainCam_ofs_w = Mathf.Clamp(_mainCam_ofs_w, 0, 1f);

        var _mainCam_ofs_h = (_screen_h - _screen_h * (_screen_w / _screen_h / MAINCAMERA_ASPECT_MIN)) / _screen_h;
        _mainCam_ofs_h = Mathf.Clamp(_mainCam_ofs_h, 0, 1f);

        return (new Rect(_mainCam_ofs_w / 2f, _mainCam_ofs_h / 2f, 1f - _mainCam_ofs_w, 1f - _mainCam_ofs_h));
    }

    private void Awake()
    {
        SingleOnScene = this;
    }
}
