using UnityEngine;

public class General_AppScreen_UICanvas_Entity : MonoBehaviour
{
    public static General_AppScreen_UICanvas_Entity SingleOnScene { get; private set; }

    public Camera Camera { get; private set; }

    private void Awake()
    {
        SingleOnScene = this;

        Camera = GetComponent<Canvas>().worldCamera;
    }
}
