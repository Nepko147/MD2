using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlScene_Entity_Menu : MonoBehaviour
{
    public static ControlScene_Entity_Menu SingleOnScene { get; private set; }

    bool stage_init = true;
    bool stage_start = false;
    bool stage_settings = false;
    float stage_settings_slider_soundValue_last;
    float stage_settings_slider_musicValue_last;

    [SerializeField] private float sceneSwitchTime = 1f;
    [SerializeField] private float menuShiftTime = 0.2f;

    [SerializeField] private AudioClip audio_mainTheme;

    private void Awake()
    {
        SingleOnScene = this;
    }

    private void Start()
    {
        ControlPers_FogHandler.Color_Load();

        var _soundValue = ControlPers_DataHandler.SingleOnScene.Settings_SoundValue;
        AppScreen_UICanvas_Menu_Settings_Audio_Slider_Sound.SingleOnScene.Value = _soundValue;
        stage_settings_slider_soundValue_last = _soundValue;

        if (_soundValue == 0)
        {
            AppScreen_UICanvas_Menu_Settings_Audio_Button_Sound.SingleOnScene.Mute_On(_soundValue);
        }
        else
        {
            AppScreen_UICanvas_Menu_Settings_Audio_Button_Sound.SingleOnScene.ImageRefresh(_soundValue);
        }

        var _musicValue = ControlPers_DataHandler.SingleOnScene.Settings_MusicValue;
        AppScreen_UICanvas_Menu_Settings_Audio_Slider_Music.SingleOnScene.Value = _musicValue;
        stage_settings_slider_musicValue_last = _musicValue;

        if (_musicValue == 0)
        {
            AppScreen_UICanvas_Menu_Settings_Audio_Button_Music.SingleOnScene.Mute_On(_musicValue);
        }
        else
        {
            AppScreen_UICanvas_Menu_Settings_Audio_Button_Music.SingleOnScene.ImageRefresh(_musicValue);
        }
    }

    public void Update()
    {
        if (stage_init)
        {
            ControlPers_FogHandler.Move();

            if (AppScreen_UICanvas_Menu_Main_Button_Play.SingleOnScene.Pressed)
            {
                ControlPers_AudioMixer_Music.SingleOnScene.Stop();
                ControlPers_AudioMixer_Music.SingleOnScene.Play(audio_mainTheme);
                World_MovingBackground_Entity.SingleOnScene.Active = true;
                World_Fog.SingleOnScene.Material_Offset_StepScale_Change(1f, sceneSwitchTime);
                World_UI_Title.SingleOnScene.Visible = false;
                World_UI_Bushes.SingleOnScene.GameStart = true;
                AppScreen_Camera_World_Entity.SingleOnScene.Blur(0, sceneSwitchTime);
                AppScreen_UICanvas_Menu_Main_Button_Play.SingleOnScene.Visible = false;
                AppScreen_UICanvas_Menu_Main_Button_Upgrades.SingleOnScene.Visible = false;
                AppScreen_UICanvas_Menu_Main_Button_Settings.SingleOnScene.Visible = false;
                AppScreen_UICanvas_Menu_Main_Button_Quit.SingleOnScene.Visible = false;

                var _world_movingBackground_parent_array = FindObjectsByType<World_MovingBackground_Parent>(FindObjectsSortMode.None);
                foreach (World_MovingBackground_Parent _item in _world_movingBackground_parent_array)
                {
                    _item.Active = true;
                }

                stage_init = false;
                stage_start = true;
            }
            else
            {
                if (AppScreen_UICanvas_Menu_Main_Button_Upgrades.SingleOnScene.Pressed)
                {
                    
                }
                else
                {
                    if (AppScreen_UICanvas_Menu_Main_Button_Settings.SingleOnScene.Pressed)
                    {
                        AppScreen_UICanvas_Menu_Main_Button_Settings.SingleOnScene.Pressed = false;

                        World_UI_Title.SingleOnScene.Shift_toDestination(menuShiftTime);
                        AppScreen_UICanvas_Menu_Main_Entity.SingleOnScene.Shift_toDestination(menuShiftTime);
                        AppScreen_UICanvas_Menu_Settings_Entity.SingleOnScene.Shift_toDestination(menuShiftTime);

                        stage_init = false;
                        stage_settings = true;
                    }
                    else
                    {
                        if (AppScreen_UICanvas_Menu_Main_Button_Quit.SingleOnScene.Pressed)
                        {
                            Application.Quit();
                        }
                    }
                }
            }                    
        }

        if (stage_start)
        {
            ControlPers_FogHandler.Move();

            sceneSwitchTime -= Time.deltaTime;             

            if (sceneSwitchTime < 0)
            {
                ControlPers_FogHandler.Color_Save();
                World_MovingBackground_Entity.SingleOnScene.Position_Save();
                SceneManager.LoadScene(ControlPers_Entity.SCENEINDEX_MAIN);
            }
        }

        if (stage_settings)
        {
            ControlPers_FogHandler.Move();

            var _soundValue = AppScreen_UICanvas_Menu_Settings_Audio_Slider_Sound.SingleOnScene.Value;

            if (stage_settings_slider_soundValue_last != _soundValue)
            {
                ControlPers_DataHandler.SingleOnScene.Settings_SoundValue = _soundValue;
                stage_settings_slider_soundValue_last = _soundValue;
            }

            var _musicValue = AppScreen_UICanvas_Menu_Settings_Audio_Slider_Music.SingleOnScene.Value;

            if (stage_settings_slider_musicValue_last != _musicValue)
            {
                ControlPers_DataHandler.SingleOnScene.Settings_MusicValue = _musicValue;
                stage_settings_slider_musicValue_last = _musicValue;
            }

            if (AppScreen_UICanvas_Menu_Settings_Button_Menu.SingleOnScene.Pressed)
            {
                AppScreen_UICanvas_Menu_Settings_Button_Menu.SingleOnScene.Pressed = false;

                ControlPers_DataHandler.SingleOnScene.SaveSettings();
                World_UI_Title.SingleOnScene.Shift_toSource(menuShiftTime);
                AppScreen_UICanvas_Menu_Main_Entity.SingleOnScene.Shift_toSource(menuShiftTime);
                AppScreen_UICanvas_Menu_Settings_Entity.SingleOnScene.Shift_toSource(menuShiftTime);

                stage_settings = false;
                stage_init = true;
            }
        }
    }
}
