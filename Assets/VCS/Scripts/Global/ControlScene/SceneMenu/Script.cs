using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class ControlScene_Menu : MonoBehaviour
{
    public static ControlScene_Menu SingleOnScene { get; private set; }

    private bool stage_init = true;
    private const float STAGE_INIT_TOCUTSCENE_TIME = 0.2f;
    private bool stage_cutscene = true;
    private const float STAGE_CUTSCENE_TOSTART_TIME = 1f;
    private bool stage_start = false;
    private float stage_start_time = STAGE_CUTSCENE_TOSTART_TIME;
    private bool stage_upgrades = false;
    private bool stage_settings = false;

    [SerializeField] private float menuShiftTime = 0.2f;

    [SerializeField] private AudioClip audio_music_intro;

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
                AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Entity.SingleOnScene.Shift_Pos_ToDestination(STAGE_INIT_TOCUTSCENE_TIME);
                AppScreen_Local_SceneMenu_UICanvas_Title.SingleOnScene.Shift_Pos_ToDestination(STAGE_INIT_TOCUTSCENE_TIME);
                AppScreen_Local_SceneMenu_UICanvas_Cutscene_Entity.SingleOnScene.Show(STAGE_INIT_TOCUTSCENE_TIME);

                stage_init = false;
                stage_cutscene = true;
            }
            else
            {
                if (AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Button_Upgrades.SingleOnScene.Pressed)
                {
                    AppScreen_Local_SceneMneu_UICanvas_Menu_Local_Upgrades_Button_Menu.SingleOnScene.Pressed = false;

                    AppScreen_Local_SceneMenu_UICanvas_Title.SingleOnScene.Shift_Pos_ToDestination(menuShiftTime);
                    AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Entity.SingleOnScene.Shift_Pos_ToDestination(menuShiftTime);
                    AppScreen_UICanvas_Menu_Upgrades_Entity.SingleOnScene.Shift_Pos_ToDestination(menuShiftTime);
                    AppScreen_UICanvas_Menu_Upgrades_Entity.SingleOnScene.Show_Instantly();
                    AppScreen_UICanvas_Menu_Upgrades_Coins_Entity.SingleOnScene.Show(menuShiftTime);

                    stage_init = false;
                    stage_upgrades = true;
                }
                else
                {
                    if (AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Button_Settings.SingleOnScene.Pressed)
                    {
                        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Button_Menu.SingleOnScene.Pressed = false;

                        AppScreen_Local_SceneMenu_UICanvas_Title.SingleOnScene.Shift_Pos_ToDestination(menuShiftTime);
                        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Entity.SingleOnScene.Shift_Pos_ToDestination(menuShiftTime);
                        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Entity.SingleOnScene.Shift_Pos_ToDestination(menuShiftTime);

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

        if (stage_cutscene)
        {
            if (AppScreen_Local_SceneMenu_UICanvas_Cutscene_Entity.SingleOnScene.Done)
            {
                ControlPers_AudioMixer_Music.SingleOnScene.Stop();
                ControlPers_AudioMixer_Music.SingleOnScene.Play(audio_music_intro, false);
                World_General_Fog.SingleOnScene.Material_Offset_StepScale_Change(1f, STAGE_CUTSCENE_TOSTART_TIME);
                AppScreen_Local_SceneMenu_UICanvas_Bushes.SingleOnScene.Shift_Pos_ToDestination(STAGE_CUTSCENE_TOSTART_TIME);
                AppScreen_General_MainCameraCarrier_MainCamera_World.SingleOnScene.Blur(0, STAGE_CUTSCENE_TOSTART_TIME);

                var _world_movingBackground_parent_array = FindObjectsByType<World_General_MovingBackground_Parent>(FindObjectsSortMode.None);
                foreach (World_General_MovingBackground_Parent _item in _world_movingBackground_parent_array)
                {
                    _item.Active = true;
                    _item.Move = true;
                }

                stage_cutscene = false;
                stage_start = true;
            }            
        }

        if (stage_start)
        {
            ControlPers_FogHandler.Move();

            stage_start_time -= Time.deltaTime;             

            if (stage_start_time <= 0)
            {
                ControlPers_FogHandler.Color_Save();
                World_General_MovingBackground_Entity.SingleOnScene.Position_Save();
                SceneManager.LoadScene(Constants.SCENEINDEX_MAIN);
            }
        }

        if (stage_upgrades)
        {
            ControlPers_FogHandler.Move();

            if (AppScreen_Local_SceneMneu_UICanvas_Menu_Local_Upgrades_Button_Menu.SingleOnScene.Pressed)
            {
                AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Button_Upgrades.SingleOnScene.Pressed = false;

                AppScreen_Local_SceneMenu_UICanvas_Title.SingleOnScene.Shift_Pos_ToSource(menuShiftTime);
                AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Entity.SingleOnScene.Shift_Pos_ToSource(menuShiftTime);
                AppScreen_UICanvas_Menu_Upgrades_Entity.SingleOnScene.Shift_Pos_ToSource(menuShiftTime);
                AppScreen_UICanvas_Menu_Upgrades_Entity.SingleOnScene.Hide(0.0f);
                AppScreen_UICanvas_Menu_Upgrades_Coins_Entity.SingleOnScene.Hide(menuShiftTime);

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

                AppScreen_Local_SceneMenu_UICanvas_Title.SingleOnScene.Shift_Pos_ToSource(menuShiftTime);
                AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Entity.SingleOnScene.Shift_Pos_ToSource(menuShiftTime);
                AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Entity.SingleOnScene.Shift_Pos_ToSource(menuShiftTime);

                stage_settings = false;
                stage_init = true;
            }
        }
    }
}
