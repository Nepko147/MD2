using UnityEngine;

public class AppScreen_UICanvas_Indicators_Coins_String : MonoBehaviour
{
    public static AppScreen_UICanvas_Indicators_Coins_String SingleOnScene { get; private set; }

    private void Awake()
    {
        SingleOnScene = this;

        GetComponent<UnityEngine.UI.Text>().font.material.mainTexture.filterMode = FilterMode.Point;
    }
}
