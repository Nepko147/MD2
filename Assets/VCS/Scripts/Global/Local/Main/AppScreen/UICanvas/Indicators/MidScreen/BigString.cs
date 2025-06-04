using UnityEngine;
using UnityEngine.UI;

public class AppScreen_UICanvas_Indicators_MidScreen_BigString : MonoBehaviour
{
    public static AppScreen_UICanvas_Indicators_MidScreen_BigString SingleOnScene { get; private set; }
    
    Text text;

    public void Enable(bool _state)
    {
        text.enabled = _state;
    }

    public void UpdateText(string _string)
    {
        text.text = _string;
    }

    private void Awake()
    {
        SingleOnScene = this;

        text = GetComponent<Text>();
        text.font.material.mainTexture.filterMode = FilterMode.Point;
    }
}
