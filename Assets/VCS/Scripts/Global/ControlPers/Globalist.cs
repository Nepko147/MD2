using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class ControlPers_Globalist : MonoBehaviour
{
    [SerializeField] private float difficultyMax;
    [SerializeField] private float difficultyScale;
    [SerializeField] private float difficultyUpTime;
    [SerializeField] private float difficultyOnStart;
    [SerializeField] private AudioClip pauseSound;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip Music;    
    private AudioSource sourcePause;
    private float timer;
    private float difficulty;
    public float luckyTime;
    public bool pause;
    public bool gameStart;
    public bool gameOver;
    public bool luck;

    // Постпроцесс
    private PostProcessVolume postProcessVoolume;
    private DepthOfField depthOfField;
    private ChromaticAberration chromaticAberration;

    public static ControlPers_Globalist Singletone { get; private set; }

    private void Awake()
    {
        Application.targetFrameRate = 60;        
        Singletone = this;        
        difficulty = difficultyOnStart;
        timer = difficultyUpTime;
        luckyTime = 0;
        pause = false;
        gameStart = false;
        gameOver = false;
        luck = false;
        sourcePause = GetComponent<AudioSource>();

        // Постпроцесс
        postProcessVoolume = AppScreen_Camera_MainCameraZoom.Singletone.GetComponent<PostProcessVolume>();
        postProcessVoolume.profile.TryGetSettings(out depthOfField);
        postProcessVoolume.profile.TryGetSettings(out chromaticAberration);
    }

    private void Start()
    {              
        float volume = (((float)ControlPers_SaveLoader.Singletone.Load("volume")) / 10);
        sourcePause.volume = volume;
    }

    private void FixedUpdate()
    {       
        //Проверяем, идёт ли игра
        if (!gameStart || gameOver)
        {
            return;
        }
        
        if (pause){ return; }

        if (luckyTime > 0)
        {
            luckyTime -= Time.deltaTime;
        } else
        {
            luck = false;
        }
        
        //Повышаем сложность игры через определённые промежутки времени
        timer -= Time.deltaTime;
        if (timer <= 0 && difficulty < difficultyMax)
        {
            difficulty += difficultyScale;
            postProcessVoolume = AppScreen_Camera_MainCameraZoom.Singletone.GetComponent<PostProcessVolume>();
            postProcessVoolume.profile.TryGetSettings(out chromaticAberration);
            chromaticAberration.intensity.value = (difficulty - 1.0f) / 10.0f;
            timer = difficultyUpTime;
        }

        //Запуск трека, после его окончания
        if (!ControlPers_AudioManager.Singletone.source.isPlaying)
        {
            ControlPers_AudioManager.Singletone.PlaySound(Music);
        }
 }

    public bool canPlay() //Даём возможность другим объектам проверять, идёт ли игра
    {
        return gameStart && !gameOver && !pause;
    }
    public bool CanSpawnEnemies() //Даём возможность другим объектам проверять, идёт ли игра
    {
        return gameStart && !gameOver && !pause && !luck;
    }

    public void StartGame() //Предворительная подготовка к старту/рестарту
    {
        timer = difficultyUpTime;
        gameStart = true;
        gameOver = false;
        luck = false;
        luckyTime = 0;
        difficulty = difficultyOnStart;
        ControlPers_AudioManager.Singletone.PlaySound(Music);
        AppScreen_Camera_MainCameraSlope.Singletone.RotationReset();
        chromaticAberration.intensity.value = 0;
    }

    public void EndGame() // ГЙЕМ ОВЕР, ЧУВАК
    {
        ControlPers_AudioManager.Singletone.Stop();
        ControlPers_AudioManager.Singletone.PlaySound(gameOverSound);
        ControlPers_AudioManager.Singletone.PlaySound(hitSound);
        UI_IndicatorsCanvas_Entity.Singletone.ShowGameOver();
        AppScreen_Camera_MainCameraSlope.Singletone.RotationReset();
        AppScreen_Camera_MainCameraZoom.Singletone.ZoomReset();
        chromaticAberration.intensity.value = 0;
        gameOver = true;
    }
    public void ReturnToMainMenu() //Возврат в главное меню
    {        
        pause = false;
        gameStart = false;
        gameOver = false;
        chromaticAberration.intensity.value = 0;
        AppScreen_Camera_MainCameraZoom.Singletone.ZoomReset();
    }

    public float GetDifficultyScale() //Даём возможность другим объектам получать текущую сложность
    {
        return difficulty;
    }

    public void StartLuckyTime(float _time)
    {
        luck = true;
        luckyTime = _time;
    }
    public bool IsLuckyTime()
    {
        return luck;
    }
    public void SetVolume(float _volume)
    {
        sourcePause.volume = _volume;
    }

    public void Pause()
    {
        postProcessVoolume = AppScreen_Camera_MainCameraZoom.Singletone.GetComponent<PostProcessVolume>();
        postProcessVoolume.profile.TryGetSettings(out depthOfField);
        UI_IndicatorsCanvas_Entity.Singletone.SetPause(true);
        sourcePause.PlayOneShot(pauseSound);
        ControlPers_AudioManager.Singletone.Pause();
        depthOfField.aperture.value = 1;
        pause = true;
    }

    public void UnPause()
    {
        postProcessVoolume = AppScreen_Camera_MainCameraZoom.Singletone.GetComponent<PostProcessVolume>();
        postProcessVoolume.profile.TryGetSettings(out depthOfField);
        UI_IndicatorsCanvas_Entity.Singletone.SetPause(false);
        ControlPers_AudioManager.Singletone.UnPause();
        depthOfField.aperture.value = 3;
        pause = false;
    }
}
