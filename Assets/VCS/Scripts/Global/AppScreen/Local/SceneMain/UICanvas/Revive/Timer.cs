using UnityEngine;
using UnityEngine.UI;

public class AppScreen_Local_SceneMain_UICanvas_Revive_Timer : MonoBehaviour
{
    public static AppScreen_Local_SceneMain_UICanvas_Revive_Timer SingleOnScene { get; private set; }

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

    private Text text;

    private void Awake()
    {
        SingleOnScene = this;

        text = GetComponent<Text>();
    }
}
