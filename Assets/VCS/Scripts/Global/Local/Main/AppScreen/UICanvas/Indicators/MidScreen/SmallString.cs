using UnityEngine;

public class AppScreen_UICanvas_Indicators_MidScreen_SmallString : MonoBehaviour
{
    public static AppScreen_UICanvas_Indicators_MidScreen_SmallString SingleOnScene { get; private set; }

    private void Awake()
    {
        SingleOnScene = this;

        GetComponent<UnityEngine.UI.Text>().font.material.mainTexture.filterMode = FilterMode.Point;
    }
}
