using UnityEngine;
using UnityEngine.UI;

public class AppScreen_Local_SceneMain_UICanvas_Revive_Cost : MonoBehaviour
{
    public static AppScreen_Local_SceneMain_UICanvas_Revive_Cost SingleOnScene { get; private set; }

    private Text text;

    public string Text
    {
        get
        {
            return text.text;
        }
        set
        {
            text.text = value;
        }
    }

    private void Awake()
    {
        SingleOnScene = this;

        text = GetComponent<Text>();
    }
}
