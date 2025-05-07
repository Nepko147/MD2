using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ControlScene_Entity_Main : MonoBehaviour
{
    public static ControlScene_Entity_Main Singletone { get; private set; }

    private AudioSource audio_source;
    [SerializeField] private AudioClip audio_sound_pause;
    [SerializeField] private AudioClip audio_sound_gameOver;
    [SerializeField] private AudioClip audio_sound_hit;

    public bool Pause { get; set; } 
    public bool GameOver { get; set; }
    public bool GameStart { get; set; }    
    public bool CoinRush { get; set; }

    private float coinRushTimer;
    [SerializeField] private float coinRushTimer_init;

    public Vector2 SpawnPoint_Line_1 { get; private set; }
    public Vector2 SpawnPoint_Line_2 { get; private set; }
    public Vector2 SpawnPoint_Line_3 { get; private set; }
    public Vector2 SpawnPoint_Line_4 { get; private set; }
    
    public const int LINE_1_SORTINGORDER_ENEMY = 7;
    public const int LINE_1_SORTINGORDER_PLAYER = 8;
    public const int LINE_2_SORTINGORDER_ENEMY = 9;
    public const int LINE_2_SORTINGORDER_PLAYER = 10;
    public const int LINE_3_SORTINGORDER_ENEMY = 11;
    public const int LINE_3_SORTINGORDER_PLAYER = 12;
    public const int LINE_4_SORTINGORDER_ENEMY = 13;
    public const int LINE_4_SORTINGORDER_PLAYER = 14;

    //Убрать в камеру ++
    private PostProcessVolume   postProcess_volume;
    private DepthOfField        postProcess_profile_depthOfField;
    private ChromaticAberration postProcess_profile_chromaticAberration;
    //Убрать в камеру --

    public void SetGameOver()
    {
        ControlPers_AudioManager.Singletone.Stop();
        audio_source.PlayOneShot(audio_sound_gameOver);
        audio_source.PlayOneShot(audio_sound_hit);
        AppScreen_Canvas_Entity.Singletone.ShowGameOver();
        Appscreen_Camera_WorldCammera_Slope.Singletone.RotationReset();
        Appscreen_Camera_WorldCammera_Zoom.Singletone.ZoomReset();
        postProcess_profile_chromaticAberration.intensity.value = 0;
        GameOver = true;
        GameObjectsActiveState(false);
    }

    public void SetPause()
    {
        postProcess_volume = Appscreen_Camera_WorldCammera_Zoom.Singletone.GetComponent<PostProcessVolume>();
        postProcess_volume.profile.TryGetSettings(out postProcess_profile_depthOfField);
        AppScreen_Canvas_Entity.Singletone.SetPause(true);
        audio_source.PlayOneShot(audio_sound_pause);
        ControlPers_AudioManager.Singletone.Pause();
        postProcess_profile_depthOfField.aperture.value = 1;
        Pause = true;
        GameObjectsActiveState(false);
    }

    public void UnPause()
    {
        postProcess_volume = Appscreen_Camera_WorldCammera_Zoom.Singletone.GetComponent<PostProcessVolume>();
        postProcess_volume.profile.TryGetSettings(out postProcess_profile_depthOfField);
        AppScreen_Canvas_Entity.Singletone.SetPause(false);
        ControlPers_AudioManager.Singletone.UnPause();
        postProcess_profile_depthOfField.aperture.value = 3;
        Pause = false;
        GameObjectsActiveState(true);
    }
    
    private void GameObjectsActiveState(bool _state)
    {
        var World_Enemy_Array = FindObjectsByType<World_Enemy>(FindObjectsSortMode.None);
        foreach (World_Enemy _item in World_Enemy_Array)
        {
            _item.Active = _state;
        }

        var World_LensFlare_Array = FindObjectsByType<World_LensFlare>(FindObjectsSortMode.None);
        foreach (World_LensFlare _item in World_LensFlare_Array)
        {
            _item.Active = _state;
        }

        var World_Bonus_Coin_Array = FindObjectsByType<World_Bonus_Coin>(FindObjectsSortMode.None);
        foreach (World_Bonus_Coin _item in World_Bonus_Coin_Array)
        {
            _item.Active = _state;
        }

        var World_Bonus_Up_Array = FindObjectsByType<World_Bonus_Up>(FindObjectsSortMode.None);
        foreach (World_Bonus_Up _item in World_Bonus_Up_Array)
        {
            _item.Active = _state;
        }

        var World_Bonus_CoinRush_Array = FindObjectsByType<World_Bonus_CoinRush>(FindObjectsSortMode.None);
        foreach (World_Bonus_CoinRush _item in World_Bonus_CoinRush_Array)
        {
            _item.Active = _state;
        }

        var World_Background_Parent_Array = FindObjectsByType<World_Background_Parent>(FindObjectsSortMode.None);
        foreach (World_Background_Parent _item in World_Background_Parent_Array)
        {
            _item.Active = _state;
        }

        var World_BonusString_Array = FindObjectsByType<World_BonusString>(FindObjectsSortMode.None);
        foreach (World_BonusString _item in World_BonusString_Array)
        {
            _item.Active = _state;
        }

        World_Player.Singletone.Active = _state;
        World_EnemySpawner.Singletone.Active = _state;
        World_BonusSpawner.Singletone.Active = _state;
    }

    private void Awake()
    {
        SpawnPoint_Line_1 = new Vector2(12, -1.75f);
        SpawnPoint_Line_2 = new Vector2(12, -2.65f);
        SpawnPoint_Line_3 = new Vector2(12, -3.55f);
        SpawnPoint_Line_4 = new Vector2(12, -4.4f);
    }

    private void Start()
    {
        Singletone = this;

        GameStart = true;

        coinRushTimer = coinRushTimer_init;

        postProcess_volume = Appscreen_Camera_WorldCammera_Zoom.Singletone.GetComponent<PostProcessVolume>();
        postProcess_volume.profile.TryGetSettings(out postProcess_profile_depthOfField);
        postProcess_profile_depthOfField.aperture.value = 3;
        postProcess_volume.profile.TryGetSettings(out postProcess_profile_chromaticAberration);

        audio_source = GetComponent<AudioSource>();
        float volume = (((float)ControlPers_DataHandler.Singletone.Settings_Volume_Get()) / 10);
        audio_source.volume = volume;
    }

    private void Update()
    {
        if (CoinRush)
        {
            coinRushTimer -= Time.deltaTime;
            
            if (coinRushTimer <= 0)
            {
                coinRushTimer = coinRushTimer_init;
                CoinRush = false;
            }
        }
    }
}
