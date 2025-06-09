using UnityEngine;

public class AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Audio_Slider_Sound : AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Audio_Slider_Parent
{    
    public static AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Audio_Slider_Sound SingleOnScene { get; private set; }
    
    public new void OnClick()
    {
        base.OnClick();

        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Audio_Button_Sound.SingleOnScene.Mute_Off(Value);
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Audio_Button_Sound.SingleOnScene.ImageRefresh(Value);
        ControlPers_AudioMixer_Sounds.SingleOnScene.Volume_Set(Value);
        ControlPers_DataHandler.SingleOnScene.SettingsData_SoundValue = Value;
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;
    }

    protected override void Start()
    {
        base.Start();

        Value = ControlPers_DataHandler.SingleOnScene.SettingsData_SoundValue;
    }
}
