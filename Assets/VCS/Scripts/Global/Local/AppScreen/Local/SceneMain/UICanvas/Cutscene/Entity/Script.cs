using UnityEngine;

public class AppScreen_Local_SceneMain_UICanvas_Cutscene_Entity : MonoBehaviour
{
    public static AppScreen_Local_SceneMain_UICanvas_Cutscene_Entity SingleOnScene { get; private set; }

    private void Awake()
    {
        SingleOnScene = this;
    }
}
