using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlScene_Main : MonoBehaviour
{
    public static ControlScene_Main SingleOnScene { get; private set; }

    private bool stage_road = true;
    private bool stage_road_toDrift = false;
    private float stage_road_toDrift_timer = 15f;
    private enum Stage_Road_ToDrift_State
    {
        clearing,
        alignment,
        braking,
        moveDown
    }
    private Stage_Road_ToDrift_State stage_road_toDrift_state = Stage_Road_ToDrift_State.clearing;
    private bool stage_drift = false;
    private bool stage_pause = false;
    private bool stage_gameOver = false;
    private bool stage_gameOver_menu_onDisplay = false;
    private float stage_gameOver_menu_timer;
    [SerializeField] private float stage_gameOver_menu_timer_init = 1.5f;

    private AudioSource audio_source;
    [SerializeField] private AudioClip audio_music_mainTheme;
    [SerializeField] private AudioClip audio_music_crickets;
    [SerializeField] private AudioClip audio_sound_pause;
    [SerializeField] private AudioClip audio_sound_gameOver;
    [SerializeField] private AudioClip audio_sound_crash;

    [SerializeField] private GameObject prefab_world_bonus_coin;

    [SerializeField] private Vector2 spawnPoint_line_1 = new Vector2(4.7f, -0.55f);
    [SerializeField] private Vector2 spawnPoint_line_2 = new Vector2(4.7f, -0.85f);
    [SerializeField] private Vector2 spawnPoint_line_3 = new Vector2(4.7f, -1.15f);
    [SerializeField] private Vector2 spawnPoint_line_4 = new Vector2(4.7f, -1.45f);

    private void ActiveState_General(bool _state)
    {
        foreach (var _item in FindObjectsByType<World_Local_SceneMain_Enemy_Entity>(FindObjectsSortMode.None))
        {
            _item.Active = _state;
        }

        foreach (var _item in FindObjectsByType<World_Local_SceneMain_Enemy_LensFlare>(FindObjectsSortMode.None))
        {
            _item.Active = _state;
        }

        foreach (var _item in FindObjectsByType<World_Local_SceneMain_Bonus_Parent>(FindObjectsSortMode.None))
        {
            _item.Active = _state;
        }

        foreach (var _item in FindObjectsByType<World_Local_SceneMain_MovingBackground_Parent>(FindObjectsSortMode.None))
        {
            _item.Active = _state;
        }

        foreach (var _item in FindObjectsByType<World_Local_SceneMain_PopUp>(FindObjectsSortMode.None))
        {
            _item.Active = _state;
        }

        World_Local_SceneMain_EnemySpawner.SingleOnScene.Active = _state;
        World_Local_SceneMain_BonusSpawner.SingleOnScene.Active = _state;
        AppScreen_Local_SceneMain_Camera_Background_Entity.SingleOnScene.Active = _state;
        AppScreen_General_Camera_Entity_Slope.SingleOnScene.Active = _state;
        AppScreen_General_Camera_World_Entity_Zoom.SingleOnScene.Active = _state;
        AppScreen_Local_SceneMain_UICanvas_VirtualStick_Entity.SingleOnScene.Active = _state;
        AppScreen_Local_SceneMain_UICanvas_Indicators_Complete_Entity.SingleOnScene.Active = _state;
    }

    private void ActiveState_Local_Pause(bool _state)
    {
        World_Local_SceneMain_Player.SingleOnScene.Active = _state;
        AppScreen_General_Camera_World_Entity_Shake.SingleOnScene.Active = _state;
        AppScreen_Local_SceneMain_Camera_World_CameraDistortion.SingleOnScene.Material_Overlay_Active = _state;
    }

    private void Awake()
    {
        SingleOnScene = this;

        audio_source = GetComponent<AudioSource>();

        stage_gameOver_menu_timer = stage_gameOver_menu_timer_init;
    }

    private void Start()
    {
        ControlPers_FogHandler.Color_Load();

        World_Local_SceneMain_EnemySpawner.SingleOnScene.EnemySpawn_SpawnPoint_Line_1 = spawnPoint_line_1;
        World_Local_SceneMain_EnemySpawner.SingleOnScene.EnemySpawn_SpawnPoint_Line_2 = spawnPoint_line_2;
        World_Local_SceneMain_EnemySpawner.SingleOnScene.EnemySpawn_SpawnPoint_Line_3 = spawnPoint_line_3;
        World_Local_SceneMain_EnemySpawner.SingleOnScene.EnemySpawn_SpawnPoint_Line_4 = spawnPoint_line_4;
        World_Local_SceneMain_BonusSpawner.SingleOnScene.BonusSpawn_SpawnPoint_Line_1 = spawnPoint_line_1;
        World_Local_SceneMain_BonusSpawner.SingleOnScene.BonusSpawn_SpawnPoint_Line_2 = spawnPoint_line_2;
        World_Local_SceneMain_BonusSpawner.SingleOnScene.BonusSpawn_SpawnPoint_Line_3 = spawnPoint_line_3;
        World_Local_SceneMain_BonusSpawner.SingleOnScene.BonusSpawn_SpawnPoint_Line_4 = spawnPoint_line_4;
        World_General_Fog.SingleOnScene.Material_Offset_StepScale_Change(1f, 0);
        World_Local_SceneMain_MovingBackground_Entity.SingleOnScene.Position_Load();

        AppScreen_Local_SceneMain_Camera_Background_Entity.SingleOnScene.ChromaticAberrationEnable(true);
        AppScreen_Local_SceneMain_UICanvas_Indicators_Entity.SingleOnScene.Show();
        AppScreen_Local_SceneMain_UICanvas_Indicators_Complete_Entity.SingleOnScene.Show(1f);
        AppScreen_Local_SceneMain_UICanvas_Indicators_Button_Pause.SingleOnScene.Visible = true;
        AppScreen_General_Camera_World_Entity.SingleOnScene.Blur(0, 0);        

        ActiveState_General(true);
        ActiveState_Local_Pause(true);
    }

    private void Update()
    {
        if (stage_road)
        {
            if (World_Local_SceneMain_Player.SingleOnScene.Player_Ups > 0)
            {
                ControlPers_FogHandler.Move();

                if (!stage_road_toDrift)
                {
                    if (World_Local_SceneMain_BonusSpawner.SingleOnScene.CoinRush
                    && AppScreen_Local_SceneMain_Camera_World_CameraDistortion.SingleOnScene.Material_Overlay_NormalMap_CoinRish_Active)
                    {
                        var _distortion_pos = AppScreen_Local_SceneMain_Camera_World_CameraDistortion.SingleOnScene.Material_Overlay_NormalMap_CoinRush_WorldPos;
                        var _distance_ofDistortion = AppScreen_Local_SceneMain_Camera_World_CameraDistortion.SingleOnScene.Material_Overlay_NormalMap_CoinRush_Distance_Get();
                        float _distance_toDistortion;

                        foreach (var _item in FindObjectsByType<World_Local_SceneMain_Enemy_Entity>(FindObjectsSortMode.None))
                        {
                            _distance_toDistortion = Vector3.Distance(_item.transform.position, _distortion_pos);

                            if (_distance_ofDistortion >= _distance_toDistortion)
                            {
                                Instantiate(prefab_world_bonus_coin, _item.transform.position, new Quaternion(), _item.transform.parent);
                                Destroy(_item.gameObject);
                            }
                        }

                        foreach (var _item in FindObjectsByType<World_Local_SceneMain_Bonus_Coin>(FindObjectsSortMode.None))
                        {
                            _distance_toDistortion = Vector3.Distance(_item.transform.position, _distortion_pos);

                            if (_distance_ofDistortion >= _distance_toDistortion)
                            {
                                _item.MakeVisible();
                            }
                        }
                    }

                    stage_road_toDrift_timer -= Time.deltaTime;
                    Debug.Log(stage_road_toDrift_timer);

                    if (stage_road_toDrift_timer <= 0)
                    {
                        Debug.Log("clearing"); //

                        World_Local_SceneMain_EnemySpawner.SingleOnScene.Active = false;
                        World_Local_SceneMain_BonusSpawner.SingleOnScene.Active = false;

                        stage_road_toDrift = true;
                    }
                }
                else
                {
                    switch (stage_road_toDrift_state)
                    {
                        case Stage_Road_ToDrift_State.clearing:
                            int _num = 0;

                            foreach (var _item in FindObjectsByType<World_Local_SceneMain_Enemy_Entity>(FindObjectsSortMode.None))
                            {
                                ++_num;
                            }

                            foreach (var _item in FindObjectsByType<World_Local_SceneMain_Bonus_Parent>(FindObjectsSortMode.None))
                            {
                                ++_num;
                            }

                            if (_num == 0)
                            {
                                Debug.Log("alignment"); //

                                stage_road_toDrift_state = Stage_Road_ToDrift_State.alignment;
                            }
                        break;

                        case Stage_Road_ToDrift_State.alignment:

                        break;

                        case Stage_Road_ToDrift_State.braking:

                        break;

                        case Stage_Road_ToDrift_State.moveDown:

                        break;
                    }
                }

                if (AppScreen_Local_SceneMain_UICanvas_Indicators_Button_Pause.SingleOnScene.Pressed
                || Input.GetKeyDown(KeyCode.Backspace))
                {
                    AppScreen_General_Camera_World_Entity.SingleOnScene.Blur(1f, 0f);
                    AppScreen_Local_SceneMain_UICanvas_Indicators_Ups_Icon.SingleOnScene.Pause();
                    AppScreen_Local_SceneMain_UICanvas_Indicators_Coins_Icon.SingleOnScene.Pause();
                    AppScreen_Local_SceneMain_UICanvas_Indicators_Button_Pause.SingleOnScene.Visible = false;
                    AppScreen_Local_SceneMain_UICanvas_Pause_Button_Resume.SingleOnScene.Visible = true;
                    AppScreen_Local_SceneMain_UICanvas_Button_Menu.SingleOnScene.Visible = true;
                    AppScreen_Local_SceneMain_UICanvas_Entity.SingleOnScene.SetPause(true);
                    audio_source.PlayOneShot(audio_sound_pause);
                    ControlPers_AudioMixer.SingleOnScene.Pause();

                    ActiveState_General(false);
                    ActiveState_Local_Pause(false);

                    stage_pause = true;
                    stage_road = false;
                }
            }
            else
            {
                audio_source.PlayOneShot(audio_sound_crash);
                ControlPers_DataHandler.SingleOnScene.ProgressData_Save();
                ControlPers_AudioMixer.SingleOnScene.Stop();
                AppScreen_Local_SceneMain_Camera_Background_Entity.SingleOnScene.ChromaticAberrationEnable(false);
                AppScreen_Local_SceneMain_UICanvas_Indicators_Entity.SingleOnScene.Hide();
                AppScreen_Local_SceneMain_Camera_World_CameraDistortion.SingleOnScene.Material_Main_NormalMap_GameOver_Apply();

                ActiveState_General(false);

                stage_road = false;
                stage_gameOver = true;                
            }
        }

        if (stage_drift)
        {
            
        }
        
        if (stage_pause)
        {
            if (AppScreen_Local_SceneMain_UICanvas_Pause_Button_Resume.SingleOnScene.Pressed)
            {                
                AppScreen_General_Camera_World_Entity.SingleOnScene.Blur(0, 0f);
                AppScreen_Local_SceneMain_UICanvas_Indicators_Ups_Icon.SingleOnScene.UnPause();
                AppScreen_Local_SceneMain_UICanvas_Indicators_Coins_Icon.SingleOnScene.UnPause();
                AppScreen_Local_SceneMain_UICanvas_Indicators_Button_Pause.SingleOnScene.Visible = true;
                AppScreen_Local_SceneMain_UICanvas_Indicators_Button_Pause.SingleOnScene.Pressed = false;
                AppScreen_Local_SceneMain_UICanvas_Pause_Button_Resume.SingleOnScene.Visible = false;
                AppScreen_Local_SceneMain_UICanvas_Pause_Button_Resume.SingleOnScene.Pressed = false;
                AppScreen_Local_SceneMain_UICanvas_Button_Menu.SingleOnScene.Visible = false;
                AppScreen_Local_SceneMain_UICanvas_Entity.SingleOnScene.SetPause(false);
                ControlPers_AudioMixer.SingleOnScene.UnPause();
                
                ActiveState_General(true);
                ActiveState_Local_Pause(true);

                stage_pause = false;
                stage_road = true;
            }
            else
            {
                if (AppScreen_Local_SceneMain_UICanvas_Button_Menu.SingleOnScene.Pressed)
                {
                    ControlPers_AudioMixer.SingleOnScene.Stop();
                    ControlPers_DataHandler.SingleOnScene.ProgressData_Save();
                    ControlPers_AudioMixer_Music.SingleOnScene.Play(audio_music_crickets);
                    SceneManager.LoadScene(ControlPers_BuildSettings.SCENEINDEX_MENU);
                }
            }              
        }

        if (stage_gameOver)
        {
            if (stage_gameOver_menu_timer > 0)
            {
                stage_gameOver_menu_timer -= Time.deltaTime;
            }
            else
            {
                if (!stage_gameOver_menu_onDisplay)
                {
                    audio_source.PlayOneShot(audio_sound_gameOver);
                    AppScreen_General_Camera_World_Entity.SingleOnScene.Blur(1f, 1f);
                    AppScreen_Local_SceneMain_UICanvas_Entity.SingleOnScene.ShowGameOver();
                    AppScreen_Local_SceneMain_UICanvas_GameOver_Button_Restart.SingleOnScene.Visible = true;
                    AppScreen_Local_SceneMain_UICanvas_Button_Menu.SingleOnScene.Visible = true;
                    stage_gameOver_menu_onDisplay = true;
                }                               
            }

            if (AppScreen_Local_SceneMain_UICanvas_GameOver_Button_Restart.SingleOnScene.Pressed)
            {
                ControlPers_AudioMixer_Music.SingleOnScene.Stop();
                ControlPers_AudioMixer_Music.SingleOnScene.Play(audio_music_mainTheme);
                SceneManager.LoadScene(ControlPers_BuildSettings.SCENEINDEX_MAIN);
            }
            else
            {
                if (AppScreen_Local_SceneMain_UICanvas_Button_Menu.SingleOnScene.Pressed)
                {
                    ControlPers_AudioMixer_Music.SingleOnScene.Stop();
                    ControlPers_AudioMixer_Music.SingleOnScene.Play(audio_music_crickets);
                    SceneManager.LoadScene(ControlPers_BuildSettings.SCENEINDEX_MENU);
                }
            }                        
        }
    }
}
