using UnityEngine;

public abstract class AppScreen_General_MainCameraCarrier_MainCamera_Parent : MonoBehaviour
{
    protected Camera camera_component;

    protected virtual void Awake()
    {
        camera_component = GetComponent<Camera>();
    }

    protected virtual void Start()
    {
        AppScreen_MainCameraCarrier_MainCamera_Entity.SingleOnScene.Screen_Resolution_OnUpdate += () =>
        {
            camera_component.aspect = AppScreen_Entity.SingleOnScene.MainCamera_Aspect_Get();
            camera_component.rect = AppScreen_Entity.SingleOnScene.MainCamera_Rect_Get();
        };
    }
}
