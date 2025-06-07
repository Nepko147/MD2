using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AppScreen_UICanvas_Pause_Button_Resume : AppScreen_UICanvas_Button_Parent
{
    public static AppScreen_UICanvas_Pause_Button_Resume SingleOnScene { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        Visible = false;
    }
}
