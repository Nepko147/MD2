using UnityEngine;

public class AppScrren_General_Camera_Parent : MonoBehaviour
{
    protected Camera camera_component;
    private const float CAMERA_ASPECT_MIN = 4f / 3f;
    private const float CAMERA_ASPECT_MAX = 22f / 9f;

    private Vector2 resolution_last = Vector2.zero;

    protected virtual void Awake()
    {
        camera_component = GetComponent<Camera>();
    }

    protected virtual void Update()
    {
        if (Screen.width != resolution_last.x
        || Screen.height != resolution_last.y)
        {
            var _aspect = (float)Screen.width / (float)Screen.height;
            camera_component.aspect = Mathf.Clamp(_aspect, CAMERA_ASPECT_MIN, CAMERA_ASPECT_MAX);

            resolution_last.x = Screen.width;
            resolution_last.y = Screen.height;
        }
    }
}
