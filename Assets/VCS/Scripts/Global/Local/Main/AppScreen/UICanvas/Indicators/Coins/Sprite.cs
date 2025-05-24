using UnityEngine;

public class AppScreen_UICanvas_Indicators_Coins_Sprite : MonoBehaviour
{
    public static AppScreen_UICanvas_Indicators_Coins_Sprite SingleOnScene { get; private set; }

    private void Awake()
    {
        SingleOnScene = this;
    }
}
