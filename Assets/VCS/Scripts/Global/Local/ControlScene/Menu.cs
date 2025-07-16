using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScene_Menu : MonoBehaviour
{
    public static ControlScene_Menu SingleOnScene { get; private set; }

    bool stage_init = true;
    bool stage_start = false;
    bool stage_upgrades = false;
    bool stage_settings = false;

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
    }

    public void Update()
    {
        if (stage_init)
        {
            ControlPers_FogHandler.Move();

            if (AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Button_Play.SingleOnScene.Pressed)
            {
                ControlPers_AudioMixer_Music.SingleOnScene.Stop();
                ControlPers_AudioMixer_Music.SingleOnScene.Play(audio_mainTheme);
                World_General_Fog.SingleOnScene.Material_Offset_StepScale_Change(1f, sceneSwitchTime);
                AppScreen_Local_SceneMenu_UICanvas_Bushes.SingleOnScene.Shift_toDestination(sceneSwitchTime * 2);
                AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Entity.SingleOnScene.PrepareToGameStart();
                AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Entity.SingleOnScene.Shift_toDestination(sceneSwitchTime * 2);
                AppScreen_Local_SceneMenu_UICanvas_Title.SingleOnScene.Shift_toDestination(sceneSwitchTime * 2);

                AppScreen_General_Camera_World_Entity.SingleOnScene.Blur(0, sceneSwitchTime);

                var _world_movingBackground_parent_array = FindObjectsByType<World_Local_SceneMain_MovingBackground_Parent>(FindObjectsSortMode.None);
                foreach (World_Local_SceneMain_MovingBackground_Parent _item in _world_movingBackground_parent_array)
                {
                    _item.Active = true;
                }

                stage_init = false;
                stage_start = true;
            }
            else
            {
                if (AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Button_Upgrades.SingleOnScene.Pressed)
                {
                    AppScreen_Local_SceneMneu_UICanvas_Menu_Local_Upgrades_Button_Menu.SingleOnScene.Pressed = false;

                    AppScreen_Local_SceneMenu_UICanvas_Title.SingleOnScene.Shift_toDestination(menuShiftTime);
                    AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Entity.SingleOnScene.Shift_toDestination(menuShiftTime);
                    AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Entity.SingleOnScene.Shift_toDestination(menuShiftTime);
                    AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Coins_Entity.SingleOnScene.Show(menuShiftTime);

                    stage_init = false;
                    stage_upgrades = true;
                }
                else
                {
                    if (AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Button_Settings.SingleOnScene.Pressed)
                    {
                        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Button_Menu.SingleOnScene.Pressed = false;

                        AppScreen_Local_SceneMenu_UICanvas_Title.SingleOnScene.Shift_toDestination(menuShiftTime);
                        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Entity.SingleOnScene.Shift_toDestination(menuShiftTime);
                        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Entity.SingleOnScene.Shift_toDestination(menuShiftTime);

                        stage_init = false;
                        stage_settings = true;
                    }
                    else
                    {
                        if (AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Button_Quit.SingleOnScene.Pressed)
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
                World_Local_SceneMain_MovingBackground_Entity.SingleOnScene.Position_Save();
                SceneManager.LoadScene(Constants.SCENEINDEX_MAIN);
            }
        }

        if (stage_upgrades)
        {
            ControlPers_FogHandler.Move();

            if (AppScreen_Local_SceneMneu_UICanvas_Menu_Local_Upgrades_Button_Menu.SingleOnScene.Pressed)
            {
                AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Button_Upgrades.SingleOnScene.Pressed = false;

                AppScreen_Local_SceneMenu_UICanvas_Title.SingleOnScene.Shift_toSource(menuShiftTime);
                AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Entity.SingleOnScene.Shift_toSource(menuShiftTime);
                AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Entity.SingleOnScene.Shift_toSource(menuShiftTime);
                AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Coins_Entity.SingleOnScene.Hide(menuShiftTime);

                stage_upgrades = false;
                stage_init = true;
            }
        }

        if (stage_settings)
        {
            ControlPers_FogHandler.Move();

            if (AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Button_Menu.SingleOnScene.Pressed)
            {
                AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Button_Settings.SingleOnScene.Pressed = false;

                ControlPers_DataHandler.SingleOnScene.SettingsData_Save();

                AppScreen_Local_SceneMenu_UICanvas_Title.SingleOnScene.Shift_toSource(menuShiftTime);
                AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Entity.SingleOnScene.Shift_toSource(menuShiftTime);
                AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Entity.SingleOnScene.Shift_toSource(menuShiftTime);

                stage_settings = false;
                stage_init = true;
            }
        }
    }
}
