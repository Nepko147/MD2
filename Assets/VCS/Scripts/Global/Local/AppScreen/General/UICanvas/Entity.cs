using UnityEngine;

public class AppScreen_General_UICanvas_Entity : MonoBehaviour
{
    public static AppScreen_General_UICanvas_Entity SingleOnScene { get; private set; }

    public Camera Camera { get; private set; }

    private void Awake()
    {
        SingleOnScene = this;

        Camera = GetComponent<Canvas>().worldCamera;
    }
}
