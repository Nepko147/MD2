using UnityEngine;

public class AppScreen_UICanvas_Menu_Settings_Audio_Button_Music : AppScreen_UICanvas_Menu_Settings_Audio_Button_Parent
{
    public static AppScreen_UICanvas_Menu_Settings_Audio_Button_Music SingleOnScene { get; private set; }

    public override void Mute_On(float _musicValue)
    {
        base.Mute_On(_musicValue);
        AppScreen_UICanvas_Menu_Settings_Audio_Slider_Music.SingleOnScene.Value = 0;
        ControlPers_AudioMixer_Music.SingleOnScene.Volume_Mute();
    }
    public override void Mute_Off(float _musicValue)
    {
        if (mute)
        {
            AppScreen_UICanvas_Menu_Settings_Audio_Slider_Music.SingleOnScene.Value = _musicValue;
            ControlPers_AudioMixer_Music.SingleOnScene.Volume_Set(_musicValue);
            base.Mute_Off(_musicValue);
        }
    }

    public void OnClick()
    {
        base.OnClick(AppScreen_UICanvas_Menu_Settings_Audio_Slider_Music.SingleOnScene.Value);
    }

    private new void Awake()
    {
        SingleOnScene = this;

        base.Awake();
    }
}
