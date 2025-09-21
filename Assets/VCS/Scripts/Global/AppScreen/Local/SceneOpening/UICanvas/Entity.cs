using UnityEngine;

public class AppScreen_Local_SceneOpening_UICanvas_Entity : MonoBehaviour
{
    public static AppScreen_Local_SceneOpening_UICanvas_Entity Singltone { get; private set; }

    private void Awake()
    {
        Singltone = this;
    }
}
