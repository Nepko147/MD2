using UnityEngine;

public class AppScreen_UICanvas_Menu_Settings_Audio_Slider_Music : AppScreen_UICanvas_Menu_Settings_Audio_Slider_Parent
{
    public static AppScreen_UICanvas_Menu_Settings_Audio_Slider_Music SingleOnScene { get; private set; }

    public new void OnClick()
    {
        base.OnClick();

        AppScreen_UICanvas_Menu_Settings_Audio_Button_Music.SingleOnScene.Mute_Off(Value);
        AppScreen_UICanvas_Menu_Settings_Audio_Button_Music.SingleOnScene.ImageRefresh(Value);
        ControlPers_AudioMixer_Music.SingleOnScene.Volume_Set(Value);
    }

    private new void Awake()
    {
        base.Awake();

        SingleOnScene = this;
    }
}
