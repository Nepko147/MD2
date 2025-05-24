using UnityEngine;

public class AppScreen_UICanvas_Indicators_Ups_Sprite : MonoBehaviour
{
    public static AppScreen_UICanvas_Indicators_Ups_Sprite SingleOnScene { get; private set; }

    private void Awake()
    {
        SingleOnScene = this;
    }
}
