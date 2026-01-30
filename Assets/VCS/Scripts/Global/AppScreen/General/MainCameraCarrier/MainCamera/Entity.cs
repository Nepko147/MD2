using UnityEngine;

public class AppScreen_MainCameraCarrier_MainCamera_Entity : MonoBehaviour
{
    public static AppScreen_MainCameraCarrier_MainCamera_Entity SingleOnScene { get; private set; }

    private Vector2 screen_resolution_last = Vector2.zero;

    public delegate void Screen_Resolution_Update();
    public event Screen_Resolution_Update Screen_Resolution_OnUpdate;

    private void Awake()
    {
        SingleOnScene = this;
    }

    private void Update()
    {
        if (Screen.width != screen_resolution_last.x
        || Screen.height != screen_resolution_last.y)
        {
            Screen_Resolution_OnUpdate();

            screen_resolution_last.x = Screen.width;
            screen_resolution_last.y = Screen.height;
        }
    }
}
