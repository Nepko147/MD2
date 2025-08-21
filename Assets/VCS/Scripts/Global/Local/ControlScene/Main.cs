using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class ControlScene_Main : MonoBehaviour
{
    #region General

    private AudioSource audio_source;
    [SerializeField] private AudioClip audio_music_mainTheme;
    [SerializeField] private AudioClip audio_music_crickets;
    [SerializeField] private AudioClip audio_sound_pause;
    [SerializeField] private AudioClip audio_sound_gameOver;

    [SerializeField] private GameObject prefab_world_bonus_coin;

    [SerializeField] private Vector2 spawnPoint_line_1 = new Vector2(5.7f, -0.55f);
    [SerializeField] private Vector2 spawnPoint_line_2 = new Vector2(5.7f, -0.85f);
    [SerializeField] private Vector2 spawnPoint_line_3 = new Vector2(5.7f, -1.15f);
    [SerializeField] private Vector2 spawnPoint_line_4 = new Vector2(5.7f, -1.45f);

    [SerializeField] private GameObject[] driftSection_prefab;

    private struct DriftSection 
    {
        public DriftSection(GameObject _prefab, string _distanceLeft, float _timerMult)
        {
            prefab = _prefab;
            distanceLeft = _distanceLeft;
            timerMult = _timerMult;
        }

        public GameObject prefab;
        public string distanceLeft;
        public float timerMult;
    }
    private DriftSection[] driftSection_array; 
    private int driftSection_array_current_ind = 0;

    private bool DriftSection_New()
    {
        if (driftSection_array_current_ind <= driftSection_array.Length)
        {
            if (InstanceHandler.AnyInstanceExists<World_Local_SceneMain_DriftSection_Enity>())
            {
                Destroy(World_Local_SceneMain_DriftSection_Enity.SingleOnScene.gameObject);
            }

            Instantiate(driftSection_array[driftSection_array_current_ind].prefab, World_Entity.SingleOnScene.transform);

            ++driftSection_array_current_ind;

            return (true);
        }
        else
        {
            return (false);
        }
    }

    #endregion

    #region Stage

        #region Road

        private bool stage_road = true;
        private const float STAGE_ROAD_TODRIFT_TIMER_INIT = 15f;
        private float stage_road_toDrift_timer = STAGE_ROAD_TODRIFT_TIMER_INIT;
        private bool stage_road_toDrift_clearing = false;
        private bool stage_road_toDrift_cutscene = false;
        private enum Stage_Road_ToDrift_Cutscene_State
        {
            alignment,
            braking,
            moveDown
        }
        private Stage_Road_ToDrift_Cutscene_State stage_road_toDrift_cutscene_state = Stage_Road_ToDrift_Cutscene_State.alignment;
        private const float STAGE_ROAD_TODRIFT_CUTSCENE_STATE_BRAKING_TIME = 2f;
        private float stage_road_toDrift_cutscene_state_braking_scale = 1f;
        private float stage_road_toDrift_cutscene_state_braking_movingBackground_speedScale_buffer;
        private bool stage_road_toDrift_cutscene_state_braking_playerMoveDown_start = true;
        private const float STAGE_ROAD_TODRIFT_CUTSCENE_STATE_BRAKING_PLAYERMOVEDOWN_SCALE = 0.5f;
        private const float STAGE_ROAD_TODRIFT_CUTSCENE_STATE_MOVEDOWN_CAMERA_YMAX = -4f;

        #endregion

        #region Drift

        private bool stage_drift = false;
        private bool stage_drift_toRoad_cutscene = false;
        private bool stage_drift_toRoad_braking_swap = true;
        private bool stage_drift_toRoad_braking_right_swap = true;
        private const float STAGE_DRIFT_TOROAD_BRAKING_MOVINGBACKGROUND_SPEEDSCALE = 0.002f;
        
        #endregion

        #region Pause

        private bool stage_pause = false;

        private delegate void Stage_Pause_Event();
        private event Stage_Pause_Event Stage_Pause_Event_On;
        private event Stage_Pause_Event Stage_Pause_Event_Off;

        private bool Stage_Pause_Condition()
        {
            if (AppScreen_Local_SceneMain_UICanvas_Indicators_Button_Pause.SingleOnScene.Pressed)
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }
        
        private void Stage_Pause_Event_On_FromRoad()
        {
            World_General_Sky.SingleOnScene.Active = false;
            World_Local_SceneMain_Cops_Entity.SingleOnScene.Active = false;
            
            if (InstanceHandler.AnyInstanceExists<World_Local_SceneMain_DriftSection_Enity>())
            {
                World_Local_SceneMain_DriftSection_Enity.SingleOnScene.Active = false;
            }
        }
        private void Stage_Pause_Event_Off_ToRoad()
        {
            World_General_Sky.SingleOnScene.Active = true;
            World_Local_SceneMain_Cops_Entity.SingleOnScene.Active = true;
            
            if (InstanceHandler.AnyInstanceExists<World_Local_SceneMain_DriftSection_Enity>())
            {
                World_Local_SceneMain_DriftSection_Enity.SingleOnScene.Active = true;
            }
            
            foreach (var _item in FindObjectsByType<World_Local_SceneMain_MovingBackground_Parent>(FindObjectsSortMode.None))
            {
                _item.Active = true;
            }

            stage_road = true;
        }
        private void Stage_Pause_Event_Off_ToDrift()
        {
            stage_drift = true;
        }
        
        #endregion

        #region GameOver

        private bool stage_gameOver = false;
        private bool stage_gameOver_menu_onDisplay = false;
        private float stage_gameOver_menu_timer;
        [SerializeField] private float stage_gameOver_menu_timer_init = 1.5f;
        
        private delegate void Stage_GameOver_Event();
        private event Stage_GameOver_Event Stage_GameOver_Event_On;
        
        private bool Stage_GameOver_Condition()
        {
            if (World_Local_SceneMain_Player_Entity.SingleOnScene.Up_Count <= 0)
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }
        
        #endregion

    #endregion

    private void Awake()
    {
        audio_source = GetComponent<AudioSource>();

        driftSection_array = new DriftSection[14]
        {
            new DriftSection(driftSection_prefab[0], "14", 1f),
            new DriftSection(driftSection_prefab[1], "13.75", 1f),
            new DriftSection(driftSection_prefab[2], "13.5", 2f),
            new DriftSection(driftSection_prefab[3], "13", 2f),
            new DriftSection(driftSection_prefab[4], "12.5", 3f),
            new DriftSection(driftSection_prefab[5], "11.75", 3f),
            new DriftSection(driftSection_prefab[6], "11", 4f),
            new DriftSection(driftSection_prefab[7], "10", 4f),
            new DriftSection(driftSection_prefab[8], "9", 5f),
            new DriftSection(driftSection_prefab[9], "7.75", 5f),
            new DriftSection(driftSection_prefab[10], "6.5", 6f),
            new DriftSection(driftSection_prefab[11], "5", 6f),
            new DriftSection(driftSection_prefab[12], "3.5", 7f),
            new DriftSection(driftSection_prefab[13], "1.75", 7f)
        };
        
        void _GeneralActiveState(bool _state)
        {
            World_Local_SceneMain_EnemySpawner.SingleOnScene.Active_General = _state;
            World_Local_SceneMain_BonusSpawner.SingleOnScene.Active_General = _state;

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

            foreach (var _item in FindObjectsByType<World_Local_SceneMain_DriftSection_Coin>(FindObjectsSortMode.None))
            {
                _item.Active = _state;
            }

            foreach (var _item in FindObjectsByType<World_Local_SceneMain_PopUp>(FindObjectsSortMode.None))
            {
                _item.Active = _state;
            }

            AppScreen_General_Camera_Entity.SingleOnScene.Active = _state;
            AppScreen_General_Camera_World_Entity_Zoom.SingleOnScene.Active = _state;
            AppScreen_Local_SceneMain_Camera_Background_Entity.SingleOnScene.Active = _state;
            AppScreen_Local_SceneMain_UICanvas_VirtualStick_Entity.SingleOnScene.Active = _state;
            AppScreen_Local_SceneMain_UICanvas_Indicators_Complete_Entity.SingleOnScene.Active = _state;
        }

        Stage_Pause_Event_On += () =>
        {
            audio_source.PlayOneShot(audio_sound_pause);

            _GeneralActiveState(false);

            ControlPers_AudioMixer.SingleOnScene.Pause();
            World_Local_SceneMain_Player_Entity.SingleOnScene.Active = false;

            foreach (var _item in FindObjectsByType<World_Local_SceneMain_MovingBackground_Parent>(FindObjectsSortMode.None))
            {
                _item.Active = false;
            }

            AppScreen_General_Camera_World_Entity.SingleOnScene.Blur(1f, 0f);
            AppScreen_General_Camera_World_Entity_Shake.SingleOnScene.Active = false;
            AppScreen_Local_SceneMain_Camera_World_CameraDistortion.SingleOnScene.Material_Overlay_Active = false;
            AppScreen_Local_SceneMain_UICanvas_Indicators_Ups_Icon.SingleOnScene.Pause();
            AppScreen_Local_SceneMain_UICanvas_Indicators_Coins_Icon.SingleOnScene.Pause();
            AppScreen_Local_SceneMain_UICanvas_Indicators_Button_Pause.SingleOnScene.Visible = false;
            AppScreen_Local_SceneMain_UICanvas_Pause_Button_Resume.SingleOnScene.Visible = true;
            AppScreen_Local_SceneMain_UICanvas_Button_Menu.SingleOnScene.Visible = true;
            AppScreen_Local_SceneMain_UICanvas_Entity.SingleOnScene.SetPause(true);
        };
        Stage_Pause_Event_On +=  Stage_Pause_Event_On_FromRoad;

        Stage_Pause_Event_Off += () =>
        {
            _GeneralActiveState(true);

            ControlPers_AudioMixer.SingleOnScene.UnPause();
            World_Local_SceneMain_Player_Entity.SingleOnScene.Active = true;
            AppScreen_General_Camera_World_Entity.SingleOnScene.Blur(0, 0f);
            AppScreen_General_Camera_World_Entity_Shake.SingleOnScene.Active = true;
            AppScreen_Local_SceneMain_Camera_World_CameraDistortion.SingleOnScene.Material_Overlay_Active = true;
            AppScreen_Local_SceneMain_UICanvas_Indicators_Ups_Icon.SingleOnScene.UnPause();
            AppScreen_Local_SceneMain_UICanvas_Indicators_Coins_Icon.SingleOnScene.UnPause();
            AppScreen_Local_SceneMain_UICanvas_Indicators_Button_Pause.SingleOnScene.Visible = true;
            AppScreen_Local_SceneMain_UICanvas_Indicators_Button_Pause.SingleOnScene.Pressed = false;
            AppScreen_Local_SceneMain_UICanvas_Pause_Button_Resume.SingleOnScene.Visible = false;
            AppScreen_Local_SceneMain_UICanvas_Pause_Button_Resume.SingleOnScene.Pressed = false;
            AppScreen_Local_SceneMain_UICanvas_Button_Menu.SingleOnScene.Visible = false;
            AppScreen_Local_SceneMain_UICanvas_Entity.SingleOnScene.SetPause(false);
        };
        Stage_Pause_Event_Off += Stage_Pause_Event_Off_ToRoad;

        Stage_GameOver_Event_On += () =>
        {
            _GeneralActiveState(false);

            ControlPers_DataHandler.SingleOnScene.ProgressData_Save();
            ControlPers_AudioMixer.SingleOnScene.Stop();
            World_General_Sky.SingleOnScene.Active = false;
            World_Local_SceneMain_Cops_Entity.SingleOnScene.Active = false;
            
            if (InstanceHandler.AnyInstanceExists<World_Local_SceneMain_DriftSection_Enity>())
            {
                World_Local_SceneMain_DriftSection_Enity.SingleOnScene.Active = false;
            }

            foreach (var _item in FindObjectsByType<World_Local_SceneMain_MovingBackground_Parent>(FindObjectsSortMode.None))
            {
                _item.Active = false;
            }

            AppScreen_Local_SceneMain_Camera_Background_Entity.SingleOnScene.PostProcess_Profile_ChromaticAberration_Discard();
            AppScreen_Local_SceneMain_Camera_World_CameraDistortion.SingleOnScene.Material_Main_NormalMap_GameOver_Apply();
            AppScreen_Local_SceneMain_UICanvas_Indicators_Entity.SingleOnScene.Hide();
        };

        stage_gameOver_menu_timer = stage_gameOver_menu_timer_init;
    }

    private void Start()
    {
        ControlPers_FogHandler.Color_Load();
        World_General_Fog.SingleOnScene.Material_Offset_StepScale_Change(1f, 0);
        World_General_Sky.SingleOnScene.Active = true;
        World_Local_SceneMain_Player_Entity.SingleOnScene.Active = true;
        World_Local_SceneMain_EnemySpawner.SingleOnScene.EnemySpawn_SpawnPoint_Line_1 = spawnPoint_line_1;
        World_Local_SceneMain_EnemySpawner.SingleOnScene.EnemySpawn_SpawnPoint_Line_2 = spawnPoint_line_2;
        World_Local_SceneMain_EnemySpawner.SingleOnScene.EnemySpawn_SpawnPoint_Line_3 = spawnPoint_line_3;
        World_Local_SceneMain_EnemySpawner.SingleOnScene.EnemySpawn_SpawnPoint_Line_4 = spawnPoint_line_4;
        World_Local_SceneMain_BonusSpawner.SingleOnScene.BonusSpawn_SpawnPoint_Line_1 = spawnPoint_line_1;
        World_Local_SceneMain_BonusSpawner.SingleOnScene.BonusSpawn_SpawnPoint_Line_2 = spawnPoint_line_2;
        World_Local_SceneMain_BonusSpawner.SingleOnScene.BonusSpawn_SpawnPoint_Line_3 = spawnPoint_line_3;
        World_Local_SceneMain_BonusSpawner.SingleOnScene.BonusSpawn_SpawnPoint_Line_4 = spawnPoint_line_4;
        World_Local_SceneMain_MovingBackground_Entity.SingleOnScene.Position_Load();
        World_Local_SceneMain_MovingBackground_Entity.SingleOnScene.SpeedScale_Active = true;

        foreach (var _item in FindObjectsByType<World_Local_SceneMain_MovingBackground_Parent>(FindObjectsSortMode.None))
        {
            _item.Active = true;
        }

        AppScreen_General_Camera_Entity.SingleOnScene.Slope = true;
        AppScreen_General_Camera_World_Entity.SingleOnScene.Blur(0, 0);
        AppScreen_General_Camera_World_Entity_Shake.SingleOnScene.Active = true;
        AppScreen_Local_SceneMain_Camera_World_CameraDistortion.SingleOnScene.Material_Overlay_Active = true;
        AppScreen_Local_SceneMain_Camera_Background_Entity.SingleOnScene.PostProcess_Profile_ChromaticAberration_Start();
        AppScreen_Local_SceneMain_UICanvas_Indicators_Entity.SingleOnScene.Show();
        AppScreen_Local_SceneMain_UICanvas_Indicators_Complete_Entity.SingleOnScene.Show(driftSection_array[driftSection_array_current_ind].distanceLeft,1f);
        AppScreen_Local_SceneMain_UICanvas_Indicators_Button_Pause.SingleOnScene.Visible = true;
    }

    private void Update()
    {
        if (stage_road)
        {
            if (Stage_GameOver_Condition())
            {
                Stage_GameOver_Event_On();

                stage_road = false;
                stage_gameOver = true;    
            }
            else
            {
                if (Stage_Pause_Condition())
                {
                    Stage_Pause_Event_On();

                    stage_road = false;
                    stage_pause = true;
                }
                else
                {
                    ControlPers_FogHandler.Move();

                    if (!stage_road_toDrift_cutscene)
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

                        if (stage_road_toDrift_timer > 0)
                        {
                            stage_road_toDrift_timer -= Time.deltaTime;
                        }
                        else
                        {
                            if (!stage_road_toDrift_clearing)
                            {
                                World_Local_SceneMain_EnemySpawner.SingleOnScene.Active_Local_Road = false;
                                World_Local_SceneMain_BonusSpawner.SingleOnScene.Active_Local_Road = false;

                                stage_road_toDrift_clearing = true;
                            }
                            else
                            {
                                if (!InstanceHandler.AnyInstanceExists<World_Local_SceneMain_Enemy_Entity>()
                                && !InstanceHandler.AnyInstanceExists<World_Local_SceneMain_Bonus_Parent>())
                                {
                                    if (DriftSection_New())
                                    {
                                        World_Local_SceneMain_Player_Entity.SingleOnScene.State_Current = World_Local_SceneMain_Player_Entity.State.road_toDrift_alignment;
                                        
                                        stage_road_toDrift_timer = STAGE_ROAD_TODRIFT_TIMER_INIT * driftSection_array[driftSection_array_current_ind].timerMult;
                                        stage_road_toDrift_clearing = false;
                                        stage_road_toDrift_cutscene = true;
                                    }
                                    else
                                    {
                                        //TODO: переход к конечной заставке
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        switch (stage_road_toDrift_cutscene_state)
                        {
                            case Stage_Road_ToDrift_Cutscene_State.alignment:
                                if (World_Local_SceneMain_Player_Entity.SingleOnScene.State_Current == World_Local_SceneMain_Player_Entity.State.road_toDrift_braking)
                                {
                                    World_General_Fog.SingleOnScene.Material_Offset_StepScale_Change(0, STAGE_ROAD_TODRIFT_CUTSCENE_STATE_BRAKING_TIME);
                                    World_Local_SceneMain_MovingBackground_Entity.SingleOnScene.SpeedScale_Active = false;
                                    stage_road_toDrift_cutscene_state_braking_movingBackground_speedScale_buffer = World_Local_SceneMain_MovingBackground_Entity.SingleOnScene.SpeedScale;
                                    World_Local_SceneMain_Cops_Entity.SingleOnScene.Move_Start();
                                    World_Local_SceneMain_DriftSection_Enity.SingleOnScene.Move = true;

                                    stage_road_toDrift_cutscene_state = Stage_Road_ToDrift_Cutscene_State.braking;
                                }
                            break;

                            case Stage_Road_ToDrift_Cutscene_State.braking:
                                stage_road_toDrift_cutscene_state_braking_scale -= Time.deltaTime / STAGE_ROAD_TODRIFT_CUTSCENE_STATE_BRAKING_TIME;
                                World_Local_SceneMain_MovingBackground_Entity.SingleOnScene.SpeedScale = stage_road_toDrift_cutscene_state_braking_movingBackground_speedScale_buffer * stage_road_toDrift_cutscene_state_braking_scale;
                            
                                if (stage_road_toDrift_cutscene_state_braking_scale < STAGE_ROAD_TODRIFT_CUTSCENE_STATE_BRAKING_PLAYERMOVEDOWN_SCALE)
                                {
                                    if (stage_road_toDrift_cutscene_state_braking_playerMoveDown_start)
                                    {
                                        World_Local_SceneMain_Player_Entity.SingleOnScene.State_Current = World_Local_SceneMain_Player_Entity.State.road_toDrift_moveDown;
                                        AppScreen_General_Camera_Entity.SingleOnScene.Move_Follow(World_Local_SceneMain_Player_Entity.SingleOnScene.gameObject);

                                        stage_road_toDrift_cutscene_state_braking_playerMoveDown_start = false;
                                    }
                                    else
                                    {
                                        if (stage_road_toDrift_cutscene_state_braking_scale <= 0)
                                        {
                                            World_Local_SceneMain_MovingBackground_Entity.SingleOnScene.SpeedScale = 0;
                                            stage_road_toDrift_cutscene_state_braking_scale = 1f;

                                            stage_road_toDrift_cutscene_state_braking_playerMoveDown_start = true;
                                            stage_road_toDrift_cutscene_state = Stage_Road_ToDrift_Cutscene_State.moveDown;
                                        }
                                    }
                                }
                            break;

                            case Stage_Road_ToDrift_Cutscene_State.moveDown:
                                if (AppScreen_General_Camera_Entity.SingleOnScene.transform.position.y <= STAGE_ROAD_TODRIFT_CUTSCENE_STATE_MOVEDOWN_CAMERA_YMAX)
                                {
                                    Stage_Pause_Event_On -= Stage_Pause_Event_On_FromRoad;
                                    Stage_Pause_Event_Off -= Stage_Pause_Event_Off_ToRoad;
                                    Stage_Pause_Event_Off += Stage_Pause_Event_Off_ToDrift;

                                    World_General_Sky.SingleOnScene.Active = false;
                                    World_Local_SceneMain_Cops_Entity.SingleOnScene.Active = false;
                                    World_Local_SceneMain_Cops_Entity.SingleOnScene.Move_Reset();
                                    World_Local_SceneMain_DriftSection_Enity.SingleOnScene.Move = false;
                                    World_Local_SceneMain_DriftSection_Barrier.SingleOnScene.Active = true;

                                    foreach (var _item in FindObjectsByType<World_Local_SceneMain_MovingBackground_Parent>(FindObjectsSortMode.None))
                                    {
                                        _item.Active = false;
                                        _item.Position_Reset();
                                    }

                                    AppScreen_General_Camera_Entity.SingleOnScene.Move_Follow_YMax = STAGE_ROAD_TODRIFT_CUTSCENE_STATE_MOVEDOWN_CAMERA_YMAX;
                                    AppScreen_Local_SceneMain_Camera_Background_Entity.SingleOnScene.PostProcess_Profile_ChromaticAberration_Discard();

                                    var _pos_x_ofs = World_Local_SceneMain_DriftSection_Point_End_Offset.SingleOnScene.transform.position.x - World_Local_SceneMain_Player_Entity.SingleOnScene.Position_Init.x;
                                    
                                    var _pos = World_Local_SceneMain_DriftSection_Enity.SingleOnScene.transform.position;
                                    _pos.x -= _pos_x_ofs;
                                    World_Local_SceneMain_DriftSection_Enity.SingleOnScene.transform.position = _pos;

                                    _pos = World_Local_SceneMain_Player_Entity.SingleOnScene.transform.position;
                                    _pos.x -= _pos_x_ofs;
                                    World_Local_SceneMain_Player_Entity.SingleOnScene.transform.position = _pos;

                                    _pos = AppScreen_General_Camera_Entity.SingleOnScene.transform.position;
                                    _pos.x -= _pos_x_ofs;
                                    AppScreen_General_Camera_Entity.SingleOnScene.transform.position = _pos;

                                    stage_road_toDrift_cutscene_state = Stage_Road_ToDrift_Cutscene_State.alignment;
                                    stage_road_toDrift_cutscene = false;
                                    stage_road = false;
                                    stage_drift = true;
                                }
                            break;
                        }
                    }
                }
            }
        }

        if (stage_drift)
        {
            if (Stage_GameOver_Condition())
            {
                Stage_GameOver_Event_On();

                stage_drift = false;
                stage_gameOver = true;
            }
            else
            {
                if (Stage_Pause_Condition())
                {
                    Stage_Pause_Event_On();

                    stage_drift = false;
                    stage_pause = true;
                }
                else
                {
                    if (!stage_drift_toRoad_cutscene)
                    {
                        var _pl_pos = World_Local_SceneMain_Player_Entity.SingleOnScene.transform.position;
                        var _cutscene_pos = World_Local_SceneMain_DriftSection_Point_End_Cutscene.SingleOnScene.transform.position;

                        if (_pl_pos.x > _cutscene_pos.x
                        && _pl_pos.y > _cutscene_pos.y) 
                        {
                            World_Local_SceneMain_Player_Entity.SingleOnScene.State_Current = World_Local_SceneMain_Player_Entity.State.drift_toRoad;
                            AppScreen_General_Camera_Entity.SingleOnScene.Move_Follow_YMax = AppScreen_General_Camera_Entity.SingleOnScene.Position_Init.y;

                            stage_drift_toRoad_cutscene = true;
                        }
                    }
                    else
                    {
                        if (stage_drift_toRoad_braking_swap
                        && World_Local_SceneMain_Player_Entity.SingleOnScene.Moving_Drift_ToRoad_Braking)
                        {
                            World_General_Sky.SingleOnScene.Active = true;
                            AppScreen_General_Camera_Entity.SingleOnScene.Move_Destination(AppScreen_General_Camera_Entity.SingleOnScene.Position_Init);

                            stage_drift_toRoad_braking_swap = false;
                        }
                        else
                        {
                            if (stage_drift_toRoad_braking_right_swap
                            && World_Local_SceneMain_Player_Entity.SingleOnScene.Moving_Drift_ToRoad_Braking_Right)
                            {
                                World_Local_SceneMain_MovingBackground_Entity.SingleOnScene.SpeedScale = STAGE_DRIFT_TOROAD_BRAKING_MOVINGBACKGROUND_SPEEDSCALE;

                                foreach (var _item in FindObjectsByType<World_Local_SceneMain_MovingBackground_Parent>(FindObjectsSortMode.None))
                                {
                                    _item.Active = true;
                                }

                                World_Local_SceneMain_DriftSection_Enity.SingleOnScene.Move = true;

                                stage_drift_toRoad_braking_right_swap = false;
                            }
                            else
                            {
                                if (World_Local_SceneMain_Player_Entity.SingleOnScene.State_Current == World_Local_SceneMain_Player_Entity.State.road)
                                {
                                    Stage_Pause_Event_On += Stage_Pause_Event_On_FromRoad;
                                    Stage_Pause_Event_Off += Stage_Pause_Event_Off_ToRoad;
                                    Stage_Pause_Event_Off -= Stage_Pause_Event_Off_ToDrift;

                                    World_Local_SceneMain_EnemySpawner.SingleOnScene.Active_Local_Road = true;
                                    World_Local_SceneMain_BonusSpawner.SingleOnScene.Active_Local_Road = true;

                                    World_Local_SceneMain_Cops_Entity.SingleOnScene.Active = true;
                                    World_Local_SceneMain_DriftSection_Barrier.SingleOnScene.Active = false;
                                    World_Local_SceneMain_MovingBackground_Entity.SingleOnScene.SpeedScale = World_Local_SceneMain_MovingBackground_Entity.SPEEDSCALE_INIT;
                                    AppScreen_Local_SceneMain_UICanvas_Indicators_Complete_Entity.SingleOnScene.Show(driftSection_array[driftSection_array_current_ind].distanceLeft, 1f);

                                    stage_drift_toRoad_cutscene = false;
                                    stage_drift_toRoad_braking_swap = true;
                                    stage_drift_toRoad_braking_right_swap = true;

                                    stage_drift = false;
                                    stage_road = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        
        if (stage_pause)
        {
            if (AppScreen_Local_SceneMain_UICanvas_Pause_Button_Resume.SingleOnScene.Pressed)
            {
                Stage_Pause_Event_Off();

                stage_pause = false;
            }
            else
            {
                if (AppScreen_Local_SceneMain_UICanvas_Button_Menu.SingleOnScene.Pressed)
                {
                    ControlPers_AudioMixer.SingleOnScene.Stop();
                    ControlPers_AudioMixer_Music.SingleOnScene.Play(audio_music_crickets);
                    ControlPers_DataHandler.SingleOnScene.ProgressData_Save();

                    SceneManager.LoadScene(Constants.SCENEINDEX_MENU);
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
                else
                {
                    if (AppScreen_Local_SceneMain_UICanvas_GameOver_Button_Restart.SingleOnScene.Pressed)
                    {
                        ControlPers_AudioMixer_Music.SingleOnScene.Stop();
                        ControlPers_AudioMixer_Music.SingleOnScene.Play(audio_music_mainTheme);

                        SceneManager.LoadScene(Constants.SCENEINDEX_MAIN);
                    }
                    else
                    {
                        if (AppScreen_Local_SceneMain_UICanvas_Button_Menu.SingleOnScene.Pressed)
                        {
                            ControlPers_AudioMixer_Music.SingleOnScene.Stop();
                            ControlPers_AudioMixer_Music.SingleOnScene.Play(audio_music_crickets);

                            SceneManager.LoadScene(Constants.SCENEINDEX_MENU);
                        }
                    }
                }
            }                    
        }
    }
}
