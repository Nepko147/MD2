using UnityEngine;

public class AppScreen_UICanvas_Menu_Settings_Audio_Button_Sound : AppScreen_UICanvas_Menu_Settings_Audio_Button_Parent
{
    public static AppScreen_UICanvas_Menu_Settings_Audio_Button_Sound SingleOnScene { get; private set; }

    public override void Mute_On(float _soundValue)
    {
        base.Mute_On(_soundValue);
        AppScreen_UICanvas_Menu_Settings_Audio_Slider_Sound.SingleOnScene.Value = 0;
        ControlPers_AudioMixer_Sounds.SingleOnScene.Volume_Mute();
    }
    public override void Mute_Off(float _soundValue)
    {
        if (mute)
        {
            AppScreen_UICanvas_Menu_Settings_Audio_Slider_Sound.SingleOnScene.Value = _soundValue;
            ControlPers_AudioMixer_Sounds.SingleOnScene.Volume_Set(_soundValue);
            base.Mute_Off(_soundValue);
        }
    }

    public void OnClick()
    {
        base.OnClick(AppScreen_UICanvas_Menu_Settings_Audio_Slider_Sound.SingleOnScene.Value);
    }

    private new void Awake()
    {
        SingleOnScene = this;

        base.Awake();
    }
}
