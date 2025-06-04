using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlScene_Entity_Main : MonoBehaviour
{
    public static ControlScene_Entity_Main SingleOnScene { get; private set; }

    private bool                    stage_road = true;
    private bool                    stage_drift = false;
    private bool                    stage_pause = false;
    private bool                    stage_gameOver = false;
    private bool                    stage_gameOver_menu_onDisplay = false;
    private float                   stage_gameOver_menu_timer;
    [SerializeField] private float  stage_gameOver_menu_timer_init = 1.5f;

    private AudioSource audio_source;
    [SerializeField] private AudioClip audio_music_mainTheme;
    [SerializeField] private AudioClip audio_music_crickets;
    [SerializeField] private AudioClip audio_sound_pause;
    [SerializeField] private AudioClip audio_sound_gameOver;
    [SerializeField] private AudioClip audio_sound_crash;

    [SerializeField] private GameObject prefab_world_bonus_coin;

    [SerializeField] private Vector2 spawnPoint_line_1 = new Vector2(4f, -0.55f);
    [SerializeField] private Vector2 spawnPoint_line_2 = new Vector2(4f, -0.85f);
    [SerializeField] private Vector2 spawnPoint_line_3 = new Vector2(4f, -1.15f);
    [SerializeField] private Vector2 spawnPoint_line_4 = new Vector2(4f, -1.45f);
    
    private void GameObjectsActiveState(bool _state)
    {
        var _world_enemy_array = FindObjectsByType<World_Enemy>(FindObjectsSortMode.None);
        foreach (World_Enemy _item in _world_enemy_array)
        {
            _item.Active = _state;
        }

        var _world_lensFlare_array = FindObjectsByType<World_LensFlare>(FindObjectsSortMode.None);
        foreach (World_LensFlare _item in _world_lensFlare_array)
        {
            _item.Active = _state;
        }

        var _world_bonus_coin_array = FindObjectsByType<World_Bonus_Coin>(FindObjectsSortMode.None);
        foreach (World_Bonus_Coin _item in _world_bonus_coin_array)
        {
            _item.Active = _state;
        }

        var _world_bonus_up_array = FindObjectsByType<World_Bonus_Up>(FindObjectsSortMode.None);
        foreach (World_Bonus_Up _item in _world_bonus_up_array)
        {
            _item.Active = _state;
        }

        var _world_bonus_coinRush_array = FindObjectsByType<World_Bonus_CoinRush>(FindObjectsSortMode.None);
        foreach (World_Bonus_CoinRush _item in _world_bonus_coinRush_array)
        {
            _item.Active = _state;
        }

        var _world_movingBackground_parent_array = FindObjectsByType<World_MovingBackground_Parent>(FindObjectsSortMode.None);
        foreach (World_MovingBackground_Parent _item in _world_movingBackground_parent_array)
        {
            _item.Active = _state;
        }

        var _world_bonusString_array = FindObjectsByType<World_PopUp>(FindObjectsSortMode.None);
        foreach (World_PopUp _item in _world_bonusString_array)
        {
            _item.Active = _state;
        }

        World_Player.SingleOnScene.Active = _state;
        World_EnemySpawner.SingleOnScene.Active = _state;
        World_BonusSpawner.SingleOnScene.Active = _state;
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

        World_EnemySpawner.SingleOnScene.EnemySpawn_SpawnPoint_Line_1 = spawnPoint_line_1;
        World_EnemySpawner.SingleOnScene.EnemySpawn_SpawnPoint_Line_2 = spawnPoint_line_2;
        World_EnemySpawner.SingleOnScene.EnemySpawn_SpawnPoint_Line_3 = spawnPoint_line_3;
        World_EnemySpawner.SingleOnScene.EnemySpawn_SpawnPoint_Line_4 = spawnPoint_line_4;
        World_BonusSpawner.SingleOnScene.BonusSpawn_SpawnPoint_Line_1 = spawnPoint_line_1;
        World_BonusSpawner.SingleOnScene.BonusSpawn_SpawnPoint_Line_2 = spawnPoint_line_2;
        World_BonusSpawner.SingleOnScene.BonusSpawn_SpawnPoint_Line_3 = spawnPoint_line_3;
        World_BonusSpawner.SingleOnScene.BonusSpawn_SpawnPoint_Line_4 = spawnPoint_line_4;
        World_Fog.SingleOnScene.Material_Offset_StepScale_Change(1f, 0);
        World_MovingBackground_Entity.SingleOnScene.Position_Load();

        AppScreen_Camera_World_Zoom.SingleOnScene.Active = true;
        AppScreen_Camera_World_Slope.SingleOnScene.Active = true;
        AppScreen_Camera_Background_Entity.SingleOnScene.Active = true;
        AppScreen_Camera_Background_Entity.SingleOnScene.ChromaticAberrationEnable(true);
        AppScreen_UICanvas_Indicators.SingleOnScene.Show();
        AppScreen_Camera_World_Entity.SingleOnScene.Blur(0, 0);        

        GameObjectsActiveState(true);
    }

    private void Update()
    {
        if (stage_road)
        {
            if (World_Player.SingleOnScene.Player_Ups > 0)
            {
                ControlPers_FogHandler.Move();

                if (World_BonusSpawner.SingleOnScene.CoinRush
                    && Universal_DistortionDynamic.SingleOnScene.NormalMapMix_Material_NormalMap_CoinRush_Active)
                {
                    var _world_enemy_array = FindObjectsByType<World_Enemy>(FindObjectsSortMode.None);
                    var _distortionPos = Universal_DistortionDynamic.SingleOnScene.NormalMapMix_Material_NormalMap_CoinRush_WorldPos;
                    var _distance_ofDistortion = Universal_DistortionDynamic.SingleOnScene.CoinRush_Distance_Get();
                    float _distance_toDistortion;
                    var _distance_toDistortion_correction = 0.8f; // Чем меньше, тем раньше сработает эффект дисторшена

                    foreach (World_Enemy _item in _world_enemy_array)
                    {
                        _distance_toDistortion = Vector3.Distance(_item.transform.position, _distortionPos);
                        
                        if (_distance_ofDistortion >= _distance_toDistortion * _distance_toDistortion_correction)
                        {
                            Instantiate(prefab_world_bonus_coin, _item.transform.position, new Quaternion());
                            Destroy(_item.gameObject);
                        }
                    }

                    var _world_coin_array = FindObjectsByType<World_Bonus_Coin>(FindObjectsSortMode.None);                    

                    foreach (World_Bonus_Coin _item in _world_coin_array)
                    {
                        _distance_toDistortion = Vector3.Distance(_item.transform.position, _distortionPos);

                        if (_distance_ofDistortion >= _distance_toDistortion * _distance_toDistortion_correction)
                        {
                            _item.MakeVisible();
                        }
                    }
                }

                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    AppScreen_Camera_World_Entity.SingleOnScene.Blur(1f, 1f);
                    AppScreen_Camera_World_Slope.SingleOnScene.Active = false;
                    AppScreen_Camera_World_Zoom.SingleOnScene.Active = false;
                    AppScreen_Camera_Background_Entity.SingleOnScene.Active = false;
                    AppScreen_UICanvas_Indicators_Ups_Sprite.SingleOnScene.Pause();
                    AppScreen_UICanvas_Indicators_Coins_Sprite.SingleOnScene.Pause();
                    AppScreen_UICanvas_Pause_Button_Resume.SingleOnScene.GetComponent<Image>().enabled = true;
                    AppScreen_UICanvas_Button_Menu.SingleOnScene.GetComponent<Image>().enabled = true;
                    Main_AppScreen_UICanvas_Entity.SingleOnScene.SetPause(true);
                    audio_source.PlayOneShot(audio_sound_pause);
                    ControlPers_AudioMixer.SingleOnScene.Pause();

                    GameObjectsActiveState(false);

                    stage_pause = true;
                    stage_road = false;
                }
            }
            else
            {
                audio_source.PlayOneShot(audio_sound_crash);
                Universal_DistortionDynamic.SingleOnScene.GameOver();
                ControlPers_DataHandler.SingleOnScene.ProgressData_Save();
                ControlPers_AudioMixer.SingleOnScene.Stop();
                AppScreen_Camera_World_Slope.SingleOnScene.Active = false;
                AppScreen_Camera_World_Zoom.SingleOnScene.Active = false;
                AppScreen_Camera_Background_Entity.SingleOnScene.ChromaticAberrationEnable(false);
                AppScreen_UICanvas_Indicators.SingleOnScene.Hide();

                GameObjectsActiveState(false);

                stage_road = false;
                stage_gameOver = true;                
            }
        }

        if (stage_drift)
        {
            
        }
        
        if (stage_pause)
        {
            if (AppScreen_UICanvas_Pause_Button_Resume.SingleOnScene.Pressed)
            {                
                AppScreen_Camera_World_Entity.SingleOnScene.Blur(0, 1f);
                AppScreen_Camera_World_Slope.SingleOnScene.Active = true;
                AppScreen_Camera_World_Zoom.SingleOnScene.Active = true;
                AppScreen_Camera_Background_Entity.SingleOnScene.Active = true;
                AppScreen_UICanvas_Indicators_Ups_Sprite.SingleOnScene.UnPause();
                AppScreen_UICanvas_Indicators_Coins_Sprite.SingleOnScene.UnPause();
                AppScreen_UICanvas_Pause_Button_Resume.SingleOnScene.GetComponent<Image>().enabled = false;
                AppScreen_UICanvas_Pause_Button_Resume.SingleOnScene.Pressed = false;
                AppScreen_UICanvas_Button_Menu.SingleOnScene.GetComponent<Image>().enabled = false;
                Main_AppScreen_UICanvas_Entity.SingleOnScene.SetPause(false);
                ControlPers_AudioMixer.SingleOnScene.UnPause();
                
                GameObjectsActiveState(true);

                stage_pause = false;
                stage_road = true;
            }
            else
            {
                if (AppScreen_UICanvas_Button_Menu.SingleOnScene.Pressed)
                {
                    ControlPers_AudioMixer.SingleOnScene.Stop();
                    ControlPers_DataHandler.SingleOnScene.ProgressData_Save();
                    ControlPers_AudioMixer_Music.SingleOnScene.Play(audio_music_crickets);
                    SceneManager.LoadScene(ControlPers_Entity.SCENEINDEX_MENU);
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
                    AppScreen_Camera_World_Entity.SingleOnScene.Blur(1f, 1f);
                    Main_AppScreen_UICanvas_Entity.SingleOnScene.ShowGameOver();
                    AppScreen_UICanvas_GameOver_Button_Restart.SingleOnScene.GetComponent<Image>().enabled = true;
                    AppScreen_UICanvas_Button_Menu.SingleOnScene.GetComponent<Image>().enabled = true;
                    stage_gameOver_menu_onDisplay = true;
                }                               
            }

            if (AppScreen_UICanvas_GameOver_Button_Restart.SingleOnScene.Pressed)
            {
                ControlPers_AudioMixer_Music.SingleOnScene.Stop();
                ControlPers_AudioMixer_Music.SingleOnScene.Play(audio_music_mainTheme);
                SceneManager.LoadScene(ControlPers_Entity.SCENEINDEX_MAIN);
            }
            else
            {
                if (AppScreen_UICanvas_Button_Menu.SingleOnScene.Pressed)
                {
                    ControlPers_AudioMixer_Music.SingleOnScene.Stop();
                    ControlPers_AudioMixer_Music.SingleOnScene.Play(audio_music_crickets);
                    SceneManager.LoadScene(ControlPers_Entity.SCENEINDEX_MENU);
                }
            }                        
        }
    }
}
