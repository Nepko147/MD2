using UnityEngine;

public class UI_GeneralCanvas_Entity : MonoBehaviour
{
    public static UI_GeneralCanvas_Entity Singleton { get; private set; }

    public Camera canvas_camera;

    private void Awake()
    {
        Singleton = this;

        canvas_camera = GetComponent<Canvas>().worldCamera;
    }
}
