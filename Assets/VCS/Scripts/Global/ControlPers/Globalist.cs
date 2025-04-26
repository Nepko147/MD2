using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class Globalist : MonoBehaviour
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

    public static Globalist Instance { get; private set; }

    private void Awake()
    {
        Application.targetFrameRate = 60;        
        Instance = this;        
        difficulty = difficultyOnStart;
        timer = difficultyUpTime;
        luckyTime = 0;
        pause = false;
        gameStart = false;
        gameOver = false;
        luck = false;
        sourcePause = GetComponent<AudioSource>();

        // Постпроцесс
        postProcessVoolume = MainCameraZoom.Instance.GetComponent<PostProcessVolume>();
        postProcessVoolume.profile.TryGetSettings(out depthOfField);
        postProcessVoolume.profile.TryGetSettings(out chromaticAberration);
    }

    private void Start()
    {              
        float volume = (((float)SaveLoader.Instance.Load("volume")) / 10);
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
            chromaticAberration.intensity.value = (difficulty - 1.0f) / 10.0f;
            timer = difficultyUpTime;
        }

        //Запуск трека, после его окончания
        if (!AudioManager.Instance.source.isPlaying)
        {
            AudioManager.Instance.PlaySound(Music);
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
        AudioManager.Instance.PlaySound(Music);
        MainCameraSlope.Instance.RotationReset();
        chromaticAberration.intensity.value = 0;
    }

    public void EndGame() // ГЙЕМ ОВЕР, ЧУВАК
    {
        AudioManager.Instance.Stop();
        AudioManager.Instance.PlaySound(gameOverSound);
        AudioManager.Instance.PlaySound(hitSound);
        UI_IndicatorsCanvas_Entity.Instance.ShowGameOver();
        MainCameraSlope.Instance.RotationReset();
        MainCameraZoom.Instance.ZoomReset();
        chromaticAberration.intensity.value = 0;
        gameOver = true;
    }
    public void ReturnToMainMenu() //Возврат в главное меню
    {        
        pause = false;
        gameStart = false;
        gameOver = false;
        chromaticAberration.intensity.value = 0;
        MainCameraZoom.Instance.ZoomReset();
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
        UI_IndicatorsCanvas_Entity.Instance.SetPause(true);
        sourcePause.PlayOneShot(pauseSound);
        AudioManager.Instance.Pause();
        depthOfField.aperture.value = 1;
        pause = true;
    }

    public void UnPause()
    {
        UI_IndicatorsCanvas_Entity.Instance.SetPause(false);
        AudioManager.Instance.UnPause();
        depthOfField.aperture.value = 3;
        pause = false;
    }
}
