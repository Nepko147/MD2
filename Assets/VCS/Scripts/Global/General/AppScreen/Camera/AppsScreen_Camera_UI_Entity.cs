using UnityEngine;

public class AppScreen_Camera_UI_Entity : MonoBehaviour
{
    public static AppScreen_Camera_UI_Entity Singletone { get; private set; }

    private void Awake()
    {
        Singletone = this;
    }
}
