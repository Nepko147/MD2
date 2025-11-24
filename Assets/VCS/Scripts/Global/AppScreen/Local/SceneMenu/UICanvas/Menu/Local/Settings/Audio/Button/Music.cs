using UnityEngine;

public class AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Audio_Button_Music : AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Audio_Button_Parent
{
    public static AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Audio_Button_Music SingleOnScene { get; private set; }

    public override void Mute_On(float _musicValue)
    {
        base.Mute_On(_musicValue);
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Audio_Slider_Music.SingleOnScene.Value = 0;
        ControlPers_AudioMixer_Music.SingleOnScene.Volume_Mute();
        ControlPers_DataHandler.SingleOnScene.SettingsData_MusicValue = 0;
    }
    public override void Mute_Off(float _musicValue)
    {
        if (mute)
        {
            AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Audio_Slider_Music.SingleOnScene.Value = _musicValue;
            ControlPers_AudioMixer_Music.SingleOnScene.Volume_Settings_Set(_musicValue);
            ControlPers_DataHandler.SingleOnScene.SettingsData_MusicValue = _musicValue;
            base.Mute_Off(_musicValue);
        }
    }

    public void OnClick()
    {
        base.OnClick(AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Audio_Slider_Music.SingleOnScene.Value);
    }

    protected override void Awake()
    {
        SingleOnScene = this;

        base.Awake();
    }

    private void Start()
    {
        if (ControlPers_DataHandler.SingleOnScene.SettingsData_MusicValue == 0)
        {
            Mute_On(ControlPers_DataHandler.SETTINGSDATA_AUDIO_MUSIC_DEFAULTVALUE);
        }
        else
        {
            ImageRefresh(ControlPers_DataHandler.SingleOnScene.SettingsData_MusicValue);
        }
    }
}
