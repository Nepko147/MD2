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
    [SerializeField] private AudioClip audio_sound_police;

    [SerializeField] private GameObject prefab_world_bonus_coin;

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

    private int driftSection_array_current_ind = 0; //0 - по дефолту. 13 - для отладки финальной катсцены

    #endregion

    #region Stage

        #region Road
        
        private bool stage_road = true;
        private bool stage_road_coinRush_swap = false;
        private const float STAGE_ROAD_TODRIFT_TIMER_INIT = 15f;
        private float stage_road_toDrift_timer = STAGE_ROAD_TODRIFT_TIMER_INIT;
        private const float STAGE_ROAD_TODRIFT_SIRENS_TIME = 5f; // За сколько секунд до перехода в дрифт станет слышно звук сирен
        private bool stage_road_toDrift_sirens_activate = true;
        private bool stage_road_toDrift_clearing = false;
        private bool stage_road_toDrift_cutscene = false;
        private enum Stage_Road_ToDrift_Cutscene_State
        {
            alignment,
            braking,
            moveDown
        }
        private Stage_Road_ToDrift_Cutscene_State stage_road_toDrift_cutscene_state = Stage_Road_ToDrift_Cutscene_State.alignment;
        private const float STAGE_ROAD_TODRIFT_CUTSCENE_STATE_BRAKING_TIME_MIN = 1f;
        private const float STAGE_ROAD_TODRIFT_CUTSCENE_STATE_BRAKING_TIME_MAX = 1.7f;
        private float stage_road_toDrift_cutscene_state_braking_time_buffer;
        private float stage_road_toDrift_cutscene_state_braking_scale = 1f;
        private float stage_road_toDrift_cutscene_state_braking_movingBackground_speedScale_buffer;
        private bool stage_road_toDrift_cutscene_state_braking_playerMoveDown_start = true;
        private const float STAGE_ROAD_TODRIFT_CUTSCENE_STATE_BRAKING_PLAYERMOVEDOWN_SCALE = 0.5f;
        private const float STAGE_ROAD_TODRIFT_CUTSCENE_STATE_MOVEDOWN_CAMERA_YMAX = -4f;
        private bool stage_road_last = false;    

        private void Stage_Road_PlayerHitZoom()
        {
            AppScreen_General_Camera_Entity.SingleOnScene.ZoomToTarget_On_AutoOff(Vector3.zero, -6.5f, AppScreen_General_Camera_Entity.SingleOnScene.Position_Init);
        }

        #endregion

        #region Drift

        private bool stage_drift = false;
        private bool stage_drift_toRoad_cutscene = false;
        private bool stage_drift_toRoad_braking_swap = true;
        private bool stage_drift_toRoad_braking_right_swap = true;
        private const float STAGE_DRIFT_TOROAD_BRAKING_MOVINGBACKGROUND_SPEEDSCALE = 0.002f;
        
        private void Stage_Drift_PlayerHitZoom()
        {
            var _pos = World_Local_SceneMain_Player_Entity.SingleOnScene.transform.position;
            AppScreen_General_Camera_Entity.SingleOnScene.ZoomToTarget_On_AutoOff(_pos, -6.5f, _pos);
        }
        
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
            
            if (InstanceHandler.AnyInstanceExists<World_Local_SceneMain_DriftSection_Enity_Parent>())
            {
                World_Local_SceneMain_DriftSection_Enity_Parent.SingleOnScene.Active = false;
            }
        }
        
        private void Stage_Pause_Event_Off_ToRoad()
        {
            World_General_Sky.SingleOnScene.Active = true;
            World_Local_SceneMain_Cops_Entity.SingleOnScene.Active = true;
            
            if (InstanceHandler.AnyInstanceExists<World_Local_SceneMain_DriftSection_Enity_Parent>())
            {
                World_Local_SceneMain_DriftSection_Enity_Parent.SingleOnScene.Active = true;
            }
            
            foreach (var _item in FindObjectsByType<World_General_MovingBackground_Parent>(FindObjectsSortMode.None))
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

        #region Revive

        private bool stage_revive = false;

        private const float STAGE_REVIVE_TIMER_INIT = 5f;
        private const float STAGE_REVIVE_TIMER_NOCOINS = 2.5f;
        private float stage_revive_timer_current;
        
        private int stage_revive_cost_current;
        private int stage_revive_cost_bought = 200;
        private int stage_revive_cost_imprved = 100;

        private delegate void Stage_Revive_Event();
        private event Stage_Revive_Event Stage_Revive_Event_On;
        private event Stage_Revive_Event Stage_Revive_Event_Off;

        private void Stage_Revive_Event_Off_ToRoad()
        {
            AppScreen_General_Camera_Entity.SingleOnScene.ZoomToTarget_Off(AppScreen_General_Camera_Entity.SingleOnScene.Position_Init);

            stage_road = true;
        }

        private void Stage_Revive_Event_Off_ToDrift()
        {
            AppScreen_General_Camera_Entity.SingleOnScene.ZoomToTarget_Off(World_Local_SceneMain_Player_Entity.SingleOnScene.transform.position);

            stage_drift = true;
        }

        private bool Stage_Revive_Condition()
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

        #region GameOver

        private bool stage_gameOver = false;
        private bool stage_gameOver_menu_onDisplay = false;
        private float stage_gameOver_menu_timer_current;
        private const float STAGE_GAMEOVER_MENU_TIMER_INIT = 1.5f;
        
        private delegate void Stage_GameOver_Event();
        private event Stage_GameOver_Event Stage_GameOver_Event_On;
        
        private bool Stage_GameOver_Condition()
        {
            if (stage_revive_timer_current > 0)
            {
                if (ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_Revive_IsBought())
                {
                    stage_gameOver_menu_timer_current = 0.0f;
                    return (false);
                }
                else
                {
                    if (ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_Revive_IsImproved())
                    {
                        stage_gameOver_menu_timer_current = 0.0f;
                        return (false);
                    }
                    else
                    {
                        return (true);
                    }
                }
            }
            else
            {
                return (true);
            }
        }

    #endregion

        #region Cutscene

        private bool stage_cutscene = false;
        private bool stage_cutscene_isCrushed = false;

        private bool Stage_Ending_isCrushed_Condition()
        {
            if (AppScreen_Local_SceneMain_UICanvas_Cutscene_Dialogue_Entity.SingleOnScene.IsCrushed
                && !stage_cutscene_isCrushed)
            {
                stage_cutscene_isCrushed = true;
                return (true);
            }
            else
            {
                return (false);
            }
        }

        private delegate void Stage_Cutscene_Event();
        private event Stage_Cutscene_Event Stage_Cutscene_Event_On;
        private event Stage_Cutscene_Event Stage_Cutscene_Event_Crush;
        private event Stage_Cutscene_Event Stage_Cutscene_Event_Off;

    #endregion

        #region Statistics

        private bool stage_statistics = false;
        private float stage_statistics_shiftTime = 20.0f;
        private float stage_statistics_continueTextTime = 22.0f;

        private bool Stage_Statistics_Condition()
        {
            if (AppScreen_Local_SceneMain_UICanvas_Cutscene_Dialogue_Entity.SingleOnScene.Done)
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }

        #endregion

        #region Ending

        private bool    stage_ending = false;
        private float   stage_ending_timer = 1.0f;

        private bool Stage_Ending_Condition()
        {
            if (AppScreen_Local_SceneMain_UICanvas_Cutscene_Statistics_Entity.SingleOnScene.Done)
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

            foreach (var _item in FindObjectsByType<World_Local_SceneMain_PopUp_Entity>(FindObjectsSortMode.None))
            {
                _item.Active = _state;
            }

            foreach (var _item in FindObjectsByType<World_Local_SceneMain_DroppedCoin_Pointer>(FindObjectsSortMode.None))
            {
                _item.Active = _state;
            }

            foreach (var _item in FindObjectsByType<World_Local_SceneMain_DroppedCoin_Entity>(FindObjectsSortMode.None))
            {
                _item.Active = _state;
            }

            AppScreen_General_Camera_Entity.SingleOnScene.Active = _state;
            AppScreen_Local_SceneMain_Camera_Background_Entity.SingleOnScene.Active = _state;
            AppScreen_Local_SceneMain_UICanvas_VirtualStick_Entity.SingleOnScene.Active = _state;
            AppScreen_Local_SceneMain_UICanvas_Indicators_Complete_Entity.SingleOnScene.Active = _state;
        }

        Stage_Pause_Event_On += () =>
        {
            audio_source.PlayOneShot(audio_sound_pause);
            
            _GeneralActiveState(false);

            AppScreen_General_Camera_Entity.SingleOnScene.Active = false;
            ControlPers_AudioMixer.SingleOnScene.Pause();
            World_Local_SceneMain_Player_Entity.SingleOnScene.Active = false;
            World_General_MovingBackground_Entity.SingleOnScene.Active = false;

            foreach (var _item in FindObjectsByType<World_General_MovingBackground_Parent>(FindObjectsSortMode.None))
            {
                _item.Active = false;
            }

            if (InstanceHandler.AnyInstanceExists<World_Local_SceneMain_DriftSection_Enity_Parent>())
            {
                World_Local_SceneMain_DriftSection_Enity_Parent.SingleOnScene.Active = false;
            }

            AppScreen_General_Camera_World_Entity.SingleOnScene.Blur(1f, 0f);
            AppScreen_General_Camera_World_Entity.SingleOnScene.Zoom_Active = false;
            AppScreen_General_Camera_World_Entity.SingleOnScene.Shake_Active = false;
            AppScreen_General_Camera_World_Entity.SingleOnScene.Distortion_Material_Overlay_Active = false;
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
            World_General_MovingBackground_Entity.SingleOnScene.Active = true;

            if (InstanceHandler.AnyInstanceExists<World_Local_SceneMain_DriftSection_Enity_Parent>())
            {
                World_Local_SceneMain_DriftSection_Enity_Parent.SingleOnScene.Active = true;
            }

            AppScreen_General_Camera_Entity.SingleOnScene.Active = true;
            AppScreen_General_Camera_World_Entity.SingleOnScene.Blur(0, 0f);
            AppScreen_General_Camera_World_Entity.SingleOnScene.Zoom_Active = true;
            AppScreen_General_Camera_World_Entity.SingleOnScene.Shake_Active = true;
            AppScreen_General_Camera_World_Entity.SingleOnScene.Distortion_Material_Overlay_Active = true;
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

        Stage_Revive_Event_On += () =>
        {
            _GeneralActiveState(false);

            ControlPers_AudioMixer_Music.SingleOnScene.Pitch_ToZero();
            ControlPers_AudioMixer_Sounds.SingleOnScene.Pitch_ToZero();
            World_Local_SceneMain_Player_Entity.SingleOnScene.Active = false;
            AppScreen_General_Camera_Entity.SingleOnScene.Active = true;
            AppScreen_General_Camera_Entity.SingleOnScene.ZoomToTarget_On(World_Local_SceneMain_Player_Entity.SingleOnScene.transform.position, -2f);

            if (ControlPers_DataHandler.SingleOnScene.ProgressData_Coins >= stage_revive_cost_current)
            {
                AppScreen_Local_SceneMain_UICanvas_Revive_Cost.SingleOnScene.Text = stage_revive_cost_current + "x";
                stage_revive_timer_current = STAGE_REVIVE_TIMER_INIT;
            }
            else
            {
                AppScreen_Local_SceneMain_UICanvas_Revive_Cost.SingleOnScene.Text = "<color=#DD0000>" + stage_revive_cost_current + "x</color>";
                stage_revive_timer_current = STAGE_REVIVE_TIMER_NOCOINS;
            }

            foreach (var _item in FindObjectsByType<World_General_MovingBackground_Parent>(FindObjectsSortMode.None))
            {
                _item.Active = false;
            }

            AppScreen_Local_SceneMain_UICanvas_Revive_Entity.SingleOnScene.Show();
            AppScreen_Local_SceneMain_UICanvas_Revive_Button.SingleOnScene.Visible = true;
            AppScreen_Local_SceneMain_UICanvas_Indicators_Button_Pause.SingleOnScene.Visible = false;
        };

        Stage_Revive_Event_Off += () =>
        {
            _GeneralActiveState(true);

            ControlPers_AudioMixer_Music.SingleOnScene.Pitch_ToNormal();
            ControlPers_AudioMixer_Sounds.SingleOnScene.Pitch_ToNormal();
            
            ControlPers_DataHandler.SingleOnScene.ProgressData_Coins -= stage_revive_cost_current;
            ControlPers_DataHandler.SingleOnScene.ProgressData_Statistics_CoinsSpentOnRevivals += stage_revive_cost_current;
            ControlPers_DataHandler.SingleOnScene.ProgressData_Statistics_ReviveNumber += 1;

            World_Local_SceneMain_Player_Entity.SingleOnScene.Active = true;
            World_Local_SceneMain_Player_Entity.SingleOnScene.Invul_Activate(2.4f, Color.red);
            World_Local_SceneMain_Player_Entity.SingleOnScene.Resurrect();

            foreach (var _item in FindObjectsByType<World_General_MovingBackground_Parent>(FindObjectsSortMode.None))
            {
                _item.Active = true;
            }
            
            AppScreen_Local_SceneMain_UICanvas_Entity.SingleOnScene.Coins_Visual = ControlPers_DataHandler.SingleOnScene.ProgressData_Coins;
            AppScreen_Local_SceneMain_UICanvas_Entity.SingleOnScene.Ups_Visual = World_Local_SceneMain_Player_Entity.SingleOnScene.Up_Count;
            AppScreen_Local_SceneMain_UICanvas_Revive_Entity.SingleOnScene.Hide();
            AppScreen_Local_SceneMain_UICanvas_Revive_Button.SingleOnScene.Visible = false;
            AppScreen_Local_SceneMain_UICanvas_Revive_Button.SingleOnScene.Pressed = false;
            AppScreen_Local_SceneMain_UICanvas_Indicators_Button_Pause.SingleOnScene.Visible = true;
        };

        Stage_Revive_Event_Off += Stage_Revive_Event_Off_ToRoad;

        Stage_GameOver_Event_On += () =>
        {
            _GeneralActiveState(false);

            ControlPers_DataHandler.SingleOnScene.ProgressData_Save();
            ControlPers_AudioMixer_Sounds.SingleOnScene.Pitch_ToNormal();
            ControlPers_AudioMixer.SingleOnScene.Stop();
            World_General_Sky.SingleOnScene.Active = false;
            World_General_MovingBackground_Entity.SingleOnScene.Active = false;
            World_Local_SceneMain_Cops_Entity.SingleOnScene.Active = false;
            
            if (InstanceHandler.AnyInstanceExists<World_Local_SceneMain_DriftSection_Enity_Parent>())
            {
                World_Local_SceneMain_DriftSection_Enity_Parent.SingleOnScene.Active = false;
            }

            foreach (var _item in FindObjectsByType<World_General_MovingBackground_Parent>(FindObjectsSortMode.None))
            {
                _item.Active = false;
            }

            AppScreen_General_Camera_World_Entity.SingleOnScene.Zoom_Active = false;
            AppScreen_General_Camera_World_Entity.SingleOnScene.Material_Main_NormalMap_GameOver_Apply();
            AppScreen_Local_SceneMain_Camera_Background_Entity.SingleOnScene.PostProcess_Profile_ChromaticAberration_Discard();
            AppScreen_Local_SceneMain_UICanvas_Indicators_Entity.SingleOnScene.Hide();
            AppScreen_Local_SceneMain_UICanvas_Revive_Entity.SingleOnScene.Hide();
        };

        stage_gameOver_menu_timer_current = STAGE_GAMEOVER_MENU_TIMER_INIT;

        Stage_Cutscene_Event_On += () =>
        {
            World_Local_SceneMain_Player_Entity.SingleOnScene.Up_Count = 1;
            
            var _cutscenePreparingTime = 1.0f;
            AppScreen_Local_SceneMain_UICanvas_Cutscene_Background_Entity.SingleOnScene.IsMoving = true;
            AppScreen_Local_SceneMain_UICanvas_Cutscene_Background_Entity.SingleOnScene.Shift_toDestination(_cutscenePreparingTime);
            AppScreen_Local_SceneMain_UICanvas_Cutscene_Dialogue_Entity.SingleOnScene.Show(_cutscenePreparingTime);
            AppScreen_Local_SceneMain_UICanvas_Cutscene_Background_Entity.SingleOnScene.Show(0.0f);
            AppScreen_Local_SceneMain_UICanvas_Indicators_Entity.SingleOnScene.Hide();
            AppScreen_Local_SceneMain_UICanvas_VirtualStick_Entity.SingleOnScene.Active = false;
        };

        Stage_Cutscene_Event_Crush += () =>
        {
            ControlPers_AudioMixer_Music.SingleOnScene.Pitch_ToZero();

            foreach (var _item in FindObjectsByType<World_General_MovingBackground_Parent>(FindObjectsSortMode.None))
            {
                _item.Active = false;
            }

            World_Local_SceneMain_Player_Entity.SingleOnScene.Up_Lose();
            AppScreen_General_Camera_Entity.SingleOnScene.Active = false;
            AppScreen_General_Camera_World_Entity.SingleOnScene.Zoom_Active = false;
            AppScreen_General_Camera_World_Entity.SingleOnScene.Material_Main_NormalMap_GameOver_Apply();
            AppScreen_Local_SceneMain_UICanvas_Cutscene_Background_Entity.SingleOnScene.IsMoving = false;
            AppScreen_Local_SceneMain_UICanvas_Cutscene_Background_Entity.SingleOnScene.Shake();
            AppScreen_Local_SceneMain_UICanvas_Cutscene_Dialogue_Entity.SingleOnScene.Shake();
            AppScreen_Local_SceneMain_Camera_Background_Entity.SingleOnScene.Shake();
        };

        Stage_Cutscene_Event_Off += () =>
        {
            AppScreen_Local_SceneMain_UICanvas_Cutscene_Statistics_Entity.SingleOnScene.Statistics_Refresh();
            AppScreen_Local_SceneMain_UICanvas_Cutscene_Statistics_Entity.SingleOnScene.Show(0.0f);            
            AppScreen_Local_SceneMain_UICanvas_Cutscene_ContinueText.SingleOnScene.Show(stage_statistics_continueTextTime);
            AppScreen_Local_SceneMain_UICanvas_Cutscene_Entity.SingleOnScene.Shift_toDestination(stage_statistics_shiftTime);
        };
    }

    private void Start()
    {
        ControlPers_FogHandler.Color_Load();
        ControlPers_DataHandler.SingleOnScene.ProgressData_Statistics_ReviveNumber = 0;
        ControlPers_DataHandler.SingleOnScene.ProgressData_Statistics_TotalDrivings += 1;
        World_General_Fog.SingleOnScene.Material_Offset_StepScale_Change(1f, 0);
        World_General_Sky.SingleOnScene.Active = true;
        World_Local_SceneMain_Player_Entity.SingleOnScene.Active = true;
        World_Local_SceneMain_Player_Entity.SingleOnScene.Up_Lose_Event += Stage_Road_PlayerHitZoom;
        World_General_MovingBackground_Entity.SingleOnScene.Active = true;
        World_General_MovingBackground_Entity.SingleOnScene.Position_Load();

        foreach (var _item in FindObjectsByType<World_General_MovingBackground_Parent>(FindObjectsSortMode.None))
        {
            _item.Active = true;
            _item.Move = true;
        }

        AppScreen_General_Camera_Entity.SingleOnScene.Slope_Active = true;
        AppScreen_General_Camera_World_Entity.SingleOnScene.Blur(0, 0);
        AppScreen_General_Camera_World_Entity.SingleOnScene.Zoom_Active = true;
        AppScreen_General_Camera_World_Entity.SingleOnScene.Distortion_Material_Overlay_Active = true;
        AppScreen_Local_SceneMain_Camera_Background_Entity.SingleOnScene.PostProcess_Profile_ChromaticAberration_Start();
        AppScreen_Local_SceneMain_UICanvas_Indicators_Entity.SingleOnScene.Show();
        AppScreen_Local_SceneMain_UICanvas_Indicators_Complete_Entity.SingleOnScene.Text_Number = driftSection_array[driftSection_array_current_ind].distanceLeft;
        AppScreen_Local_SceneMain_UICanvas_Indicators_Complete_Entity.SingleOnScene.Show(1f);
        AppScreen_Local_SceneMain_UICanvas_Indicators_Button_Pause.SingleOnScene.Visible = true;

        if (ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_Revive_IsImproved())
        {
            stage_revive_cost_current = stage_revive_cost_imprved;
        }
        else
        {
            stage_revive_cost_current = stage_revive_cost_bought;
        }
    }

    private void Update()
    {
        if (stage_road)
        {
            if (Stage_Revive_Condition())
            {
                Stage_Revive_Event_On();

                stage_road = false;
                stage_revive = true;    
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

                    if (World_Local_SceneMain_BonusSpawner.SingleOnScene.CoinRush
                    && AppScreen_General_Camera_World_Entity.SingleOnScene.Distortion_Material_Overlay_NormalMap_CoinRush_Active)
                    {
                        stage_road_coinRush_swap = true;

                        var _distortion_pos = AppScreen_General_Camera_World_Entity.SingleOnScene.Distortion_Material_Overlay_NormalMap_CoinRush_WorldPos;
                        var _distance_ofDistortion = AppScreen_General_Camera_World_Entity.SingleOnScene.Material_Overlay_NormalMap_CoinRush_Distance_Get();
                        float _distance_toDistortion;

                        foreach (var _item in FindObjectsByType<World_Local_SceneMain_Enemy_Entity>(FindObjectsSortMode.None))
                        {
                            _distance_toDistortion = Vector3.Distance(_item.transform.position, _distortion_pos);

                            if (_distance_ofDistortion >= _distance_toDistortion)
                            {
                                var _pos = _item.transform.position;
                                var _rot = new Quaternion();
                                var _tran = _item.transform.parent;

                                Instantiate(prefab_world_bonus_coin, _pos, _rot, _tran);
                                Instantiate(prefab_world_bonus_coin, _pos + Vector3.left * World_Local_SceneMain_BonusSpawner.COINS_OFFSET, _rot, _tran);
                                Instantiate(prefab_world_bonus_coin, _pos + Vector3.right * World_Local_SceneMain_BonusSpawner.COINS_OFFSET, _rot, _tran);

                                Destroy(_item.gameObject);
                            }
                        }

                        foreach (var _item in FindObjectsByType<World_Local_SceneMain_Bonus_Coin>(FindObjectsSortMode.None))
                        {
                            _distance_toDistortion = Vector3.Distance(_item.transform.position, _distortion_pos);

                            if (_distance_ofDistortion >= _distance_toDistortion)
                            {
                                _item.Visible = true;
                            }
                        }

                        if (World_Local_SceneMain_Cops_Entity.SingleOnScene.Move)
                        {
                            _distance_toDistortion = Vector3.Distance(World_Local_SceneMain_Cops_Entity.SingleOnScene.transform.position, _distortion_pos);

                            if (_distance_ofDistortion >= _distance_toDistortion)
                            {
                                World_Local_SceneMain_Cops_Coins.SingleOnScene.Coins_Spawn();
                                World_Local_SceneMain_Cops_Entity.SingleOnScene.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        if (stage_road_coinRush_swap)
                        {
                            foreach (var _item in FindObjectsByType<World_Local_SceneMain_Bonus_Coin>(FindObjectsSortMode.None))
                            {
                                _item.Visible = true;
                            }

                            stage_road_coinRush_swap = false;
                        }
                    }

                    if (!stage_road_toDrift_cutscene)
                    {
                        if (stage_road_toDrift_timer > 0)
                        {
                            stage_road_toDrift_timer -= Time.deltaTime;

                            if (stage_road_toDrift_sirens_activate
                            && stage_road_toDrift_timer <= STAGE_ROAD_TODRIFT_SIRENS_TIME)
                            {
                                ControlPers_AudioMixer_Sounds.SingleOnScene.Play(audio_sound_police);
                                stage_road_toDrift_sirens_activate = false;
                            }
                        }
                        else
                        {
                            if (!stage_road_toDrift_clearing)
                            {
                                World_Local_SceneMain_EnemySpawner.SingleOnScene.Active_Local_Road = false;
                                World_Local_SceneMain_BonusSpawner.SingleOnScene.Active_Local_Road = false;

                                foreach (var _item in FindObjectsByType<World_Local_SceneMain_Bonus_Coin>(FindObjectsSortMode.None))
                                {
                                    _item.CoinMagnet_Speed_Inc_Turbo();
                                }

                                stage_road_toDrift_clearing = true;
                            }
                            else
                            {
                                if (!InstanceHandler.AnyInstanceExists<World_Local_SceneMain_Enemy_Entity>()
                                && !InstanceHandler.AnyInstanceExists<World_Local_SceneMain_Bonus_Parent>())
                                {
                                    if (!stage_road_last)
                                    {
                                        if (InstanceHandler.AnyInstanceExists<World_Local_SceneMain_DriftSection_Enity_Parent>())
                                        {
                                            Destroy(World_Local_SceneMain_DriftSection_Enity_Parent.SingleOnScene.gameObject);
                                        }

                                        Instantiate(driftSection_array[driftSection_array_current_ind].prefab, World_Entity.SingleOnScene.transform);

                                        if (driftSection_array_current_ind < driftSection_array.Length - 1)
                                        {
                                            ++driftSection_array_current_ind;

                                            stage_road_toDrift_timer = STAGE_ROAD_TODRIFT_TIMER_INIT * driftSection_array[driftSection_array_current_ind].timerMult;
                                            stage_road_toDrift_sirens_activate = true;
                                        }
                                        else
                                        {
                                            stage_road_last = true;
                                        }

                                        World_Local_SceneMain_Player_Entity.SingleOnScene.State_Current = World_Local_SceneMain_Player_Entity.State.road_toDrift_alignment;

                                        stage_road_toDrift_clearing = false;
                                        stage_road_toDrift_cutscene = true;
                                    }
                                    else
                                    {
                                        Stage_Cutscene_Event_On();

                                        stage_road = false;
                                        stage_cutscene = true;  
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
                                    stage_road_toDrift_cutscene_state_braking_time_buffer = (STAGE_ROAD_TODRIFT_CUTSCENE_STATE_BRAKING_TIME_MAX - (STAGE_ROAD_TODRIFT_CUTSCENE_STATE_BRAKING_TIME_MAX - STAGE_ROAD_TODRIFT_CUTSCENE_STATE_BRAKING_TIME_MIN) * World_General_MovingBackground_Entity.SingleOnScene.SpeedScale_Normalized());
                                    stage_road_toDrift_cutscene_state_braking_movingBackground_speedScale_buffer = World_General_MovingBackground_Entity.SingleOnScene.SpeedScale;

                                    World_General_Fog.SingleOnScene.Material_Offset_StepScale_Change(0, stage_road_toDrift_cutscene_state_braking_time_buffer);
                                    World_General_MovingBackground_Entity.SingleOnScene.SpeedScale_Active = false;
                                    World_Local_SceneMain_Cops_Entity.SingleOnScene.Move_Start();
                                    World_Local_SceneMain_DriftSection_Enity_Parent.SingleOnScene.Move = true;

                                    stage_road_toDrift_cutscene_state = Stage_Road_ToDrift_Cutscene_State.braking;
                                }
                            break;

                            case Stage_Road_ToDrift_Cutscene_State.braking:
                                stage_road_toDrift_cutscene_state_braking_scale -= Time.deltaTime / stage_road_toDrift_cutscene_state_braking_time_buffer;
                                World_General_MovingBackground_Entity.SingleOnScene.SpeedScale = stage_road_toDrift_cutscene_state_braking_movingBackground_speedScale_buffer * stage_road_toDrift_cutscene_state_braking_scale;
                                
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
                                            World_General_MovingBackground_Entity.SingleOnScene.SpeedScale = 0;
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

                                    Stage_Revive_Event_Off -= Stage_Revive_Event_Off_ToRoad;
                                    Stage_Revive_Event_Off += Stage_Revive_Event_Off_ToDrift;

                                    World_General_Sky.SingleOnScene.Active = false;
                                    World_General_MovingBackground_Entity.SingleOnScene.SpeedScale_Active = true;

                                    foreach (var _item in FindObjectsByType<World_General_MovingBackground_Parent>(FindObjectsSortMode.None))
                                    {
                                        _item.Move = false;
                                        _item.Position_Reset();
                                    }

                                    World_Local_SceneMain_Cops_Entity.SingleOnScene.Active = false;
                                    World_Local_SceneMain_Cops_Entity.SingleOnScene.Visible = true;
                                    World_Local_SceneMain_Cops_Entity.SingleOnScene.Move_Reset();
                                    World_Local_SceneMain_Cops_Coins.SingleOnScene.Coins_Spawn_Refresh();
                                    World_Local_SceneMain_Player_Entity.SingleOnScene.Up_Lose_Event -= Stage_Road_PlayerHitZoom;
                                    World_Local_SceneMain_Player_Entity.SingleOnScene.Up_Lose_Event += Stage_Drift_PlayerHitZoom;
                                    World_Local_SceneMain_DriftSection_Enity_Parent.SingleOnScene.Move = false;
                                    World_Local_SceneMain_DriftSection_Transition_Barrier.SingleOnScene.Active = true;
                                    AppScreen_General_Camera_Entity.SingleOnScene.Move_Follow_YMax = STAGE_ROAD_TODRIFT_CUTSCENE_STATE_MOVEDOWN_CAMERA_YMAX;
                                    AppScreen_General_Camera_GlobalLightning_Entity.SingleOnScene.Position_Z_ZoomOfs_Drift();
                                    AppScreen_General_Camera_World_Entity.SingleOnScene.Zoom_State_ToSpeedUp();
                                    AppScreen_Local_SceneMain_Camera_Background_Entity.SingleOnScene.PostProcess_Profile_ChromaticAberration_Discard();

                                    var _pos_x_ofs = World_Local_SceneMain_DriftSection_Point_End_Offset.SingleOnScene.transform.position.x - World_Local_SceneMain_Player_Entity.SingleOnScene.Position_Init.x;
                                    
                                    var _pos = World_Local_SceneMain_Player_Entity.SingleOnScene.transform.position;
                                    _pos.x -= _pos_x_ofs;
                                    World_Local_SceneMain_Player_Entity.SingleOnScene.transform.position = _pos;

                                    _pos = World_Local_SceneMain_DriftSection_Enity_Parent.SingleOnScene.transform.position;
                                    _pos.x -= _pos_x_ofs;
                                    World_Local_SceneMain_DriftSection_Enity_Parent.SingleOnScene.transform.position = _pos;

                                    foreach (var _item in FindObjectsByType<World_Local_SceneMain_Bonus_Coin>(FindObjectsSortMode.None))
                                    {
                                        _pos = _item.transform.position;
                                        _pos.x -= _pos_x_ofs;
                                        _item.transform.position = _pos;
                                    }

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
            if (Stage_Revive_Condition())
            {
                Stage_Revive_Event_On();

                stage_drift = false;
                stage_revive = true;
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
                            Stage_Pause_Event_On += Stage_Pause_Event_On_FromRoad;
                            Stage_Pause_Event_Off += Stage_Pause_Event_Off_ToRoad;

                            World_Local_SceneMain_Player_Entity.SingleOnScene.State_Current = World_Local_SceneMain_Player_Entity.State.drift_toRoad;
                            AppScreen_General_Camera_Entity.SingleOnScene.Move_Follow_YMax = AppScreen_General_Camera_Entity.SingleOnScene.Position_Init.y;

                            foreach (var _item in FindObjectsByType<World_Local_SceneMain_DriftSection_Coin>(FindObjectsSortMode.None))
                            {
                                _item.CoinMagnet_Speed_Inc_Turbo();
                            }

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

                            foreach (World_General_DrawableSurface _item in FindObjectsByType<World_General_DrawableSurface>(FindObjectsSortMode.None))
                            {
                                _item.Texture_Refresh();
                            }

                            stage_drift_toRoad_braking_swap = false;
                        }
                        else
                        {
                            if (stage_drift_toRoad_braking_right_swap
                            && World_Local_SceneMain_Player_Entity.SingleOnScene.Moving_Drift_ToRoad_Braking_Right)
                            {
                                World_General_MovingBackground_Entity.SingleOnScene.SpeedScale = STAGE_DRIFT_TOROAD_BRAKING_MOVINGBACKGROUND_SPEEDSCALE;

                                foreach (var _item in FindObjectsByType<World_General_MovingBackground_Parent>(FindObjectsSortMode.None))
                                {
                                    _item.Move = true;
                                }

                                World_Local_SceneMain_DriftSection_Enity_Parent.SingleOnScene.Move = true;

                                stage_drift_toRoad_braking_right_swap = false;
                            }
                            else
                            {
                                if (World_Local_SceneMain_Player_Entity.SingleOnScene.State_Current == World_Local_SceneMain_Player_Entity.State.road)
                                {
                                    Stage_Pause_Event_Off -= Stage_Pause_Event_Off_ToDrift;

                                    Stage_Revive_Event_Off -= Stage_Revive_Event_Off_ToDrift;
                                    Stage_Revive_Event_Off += Stage_Revive_Event_Off_ToRoad;

                                    World_Local_SceneMain_EnemySpawner.SingleOnScene.Active_Local_Road = true;
                                    World_Local_SceneMain_BonusSpawner.SingleOnScene.Active_Local_Road = true;
                                    World_Local_SceneMain_Cops_Entity.SingleOnScene.Active = true;
                                    World_General_MovingBackground_Entity.SingleOnScene.SpeedScale = World_General_MovingBackground_Entity.SPEEDSCALE_INIT;
                                    World_Local_SceneMain_Player_Entity.SingleOnScene.Up_Lose_Event += Stage_Road_PlayerHitZoom;
                                    World_Local_SceneMain_Player_Entity.SingleOnScene.Up_Lose_Event -= Stage_Drift_PlayerHitZoom;
                                    World_Local_SceneMain_DriftSection_Transition_Barrier.SingleOnScene.Active = false;
                                    AppScreen_General_Camera_World_Entity.SingleOnScene.Zoom_State_ToPulse();
                                    AppScreen_General_Camera_GlobalLightning_Entity.SingleOnScene.Position_Z_ZoomOfs_Road();
                                    AppScreen_Local_SceneMain_UICanvas_Indicators_Complete_Entity.SingleOnScene.Text_Number = driftSection_array[driftSection_array_current_ind].distanceLeft;
                                    AppScreen_Local_SceneMain_UICanvas_Indicators_Complete_Entity.SingleOnScene.Show(1f);

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

        if (stage_revive)
        {
            if (Stage_GameOver_Condition())
            {
                Stage_GameOver_Event_On();

                stage_revive = false;
                stage_gameOver = true;                
            }
            else
            {
                if (!AppScreen_Local_SceneMain_UICanvas_Revive_Button.SingleOnScene.Pressed)
                {
                    stage_revive_timer_current -= Time.deltaTime;

                    var _string = "";

                    if (stage_revive_timer_current > 0)
                    {
                        _string = stage_revive_timer_current.ToString().Substring(0, 4);
                    }

                    AppScreen_Local_SceneMain_UICanvas_Revive_Timer.SingleOnScene.Text = _string;
                }
                else
                {
                    if (ControlPers_DataHandler.SingleOnScene.ProgressData_Coins >= stage_revive_cost_current)
                    {
                        Stage_Revive_Event_Off();

                        stage_revive = false;
                    }
                    else
                    {
                        AppScreen_Local_SceneMain_UICanvas_Revive_Button.SingleOnScene.Pressed = false;
                    }
                }                
            }
        }

        if (stage_gameOver)
        {
            if (stage_gameOver_menu_timer_current > 0)
            {
                stage_gameOver_menu_timer_current -= Time.deltaTime;
            }
            else
            {
                if (!stage_gameOver_menu_onDisplay)
                {
                    audio_source.PlayOneShot(audio_sound_gameOver);

                    ControlPers_DataHandler.SingleOnScene.ProgressData_Statistics_Defeats += 1;
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
    
        if (stage_cutscene)
        {
            if (Stage_Ending_isCrushed_Condition())
            {
                Stage_Cutscene_Event_Crush();
            }
            else
            {
                if (Stage_Statistics_Condition())
                {
                    Stage_Cutscene_Event_Off();

                    stage_cutscene = false;
                    stage_statistics = true;
                }
            }
        }

        if (stage_statistics)
        {
            if (Stage_Ending_Condition())
            {
                stage_statistics = false;
                stage_ending = true;
            }
            else
            {
                if (stage_statistics_shiftTime > 0)
                {
                    stage_statistics_shiftTime -= Time.deltaTime;
                }
                else
                {
                    if (Input.anyKey)
                    {
                        AppScreen_Local_SceneMain_UICanvas_Cutscene_Statistics_Entity.SingleOnScene.Hide();
                        AppScreen_Local_SceneMain_UICanvas_Cutscene_ContinueText.SingleOnScene.Hide();
                    }
                }                
            }
        }

        if (stage_ending)
        {
            if (stage_ending_timer > 0)
            {
                stage_ending_timer -= Time.deltaTime;                
            }
            else
            {
                ControlPers_AudioMixer.SingleOnScene.Stop();
                ControlPers_AudioMixer_Music.SingleOnScene.Play(audio_music_crickets);
                ControlPers_DataHandler.SingleOnScene.ProgressData_Save();

                SceneManager.LoadScene(Constants.SCENEINDEX_MENU);
            }
        }
    }
}
