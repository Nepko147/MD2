using UnityEngine;

public class AppScreen_General_Camera_UI_Entity : MonoBehaviour
{
    public static AppScreen_General_Camera_UI_Entity SingleOnScene { get; private set; }

    private void Awake()
    {
        SingleOnScene = this;
    }
}
