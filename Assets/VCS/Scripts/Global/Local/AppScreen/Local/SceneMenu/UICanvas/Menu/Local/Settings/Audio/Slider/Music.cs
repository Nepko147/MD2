using UnityEngine;

public class AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Audio_Slider_Music : AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Audio_Slider_Parent
{
    public static AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Audio_Slider_Music SingleOnScene { get; private set; }

    public new void OnClick()
    {
        base.OnClick();

        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Audio_Button_Music.SingleOnScene.Mute_Off(Value);
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Audio_Button_Music.SingleOnScene.ImageRefresh(Value);
        ControlPers_AudioMixer_Music.SingleOnScene.Volume_Set(Value);
        ControlPers_DataHandler.SingleOnScene.SettingsData_MusicValue = Value;
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;
    }

    protected override void Start()
    {
        base.Start();

        Value = ControlPers_DataHandler.SingleOnScene.SettingsData_MusicValue;
    }
}
