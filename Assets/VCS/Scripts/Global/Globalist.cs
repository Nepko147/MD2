using UnityEngine;
using UnityEngine.UI;

public class Globalist : MonoBehaviour
{
    [SerializeField] private float difficultyMax;
    [SerializeField] private float difficultyScale;
    [SerializeField] private float difficultyUpTime;
    [SerializeField] private float difficulty;
    [SerializeField] private AudioClip pauseSound;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip Music;    
    private AudioSource sourcePause;
    private Text midScreenText;
    private Text midScreenSubText;
    private float timer;
    private float coinUpTimer;
    private float coinValue;
    public float luckyTime;
    public bool pause;
    public bool gameStart;
    public bool gameOver;
    public bool luck;

    public static Globalist Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
        difficulty = 1;
        timer = difficultyUpTime;
        luckyTime = 0;
        pause = false;
        gameStart = false;
        gameOver = false;
        luck = false;
        sourcePause = GetComponent<AudioSource>();
        midScreenText = GameObject.Find("MidScreenText").GetComponent<Text>();
        midScreenSubText = GameObject.Find("MidScreenSubText").GetComponent<Text>();        
    }

    private void Start()
    {        
        float volume = (((float)SaveLoader.Instance.Load("Settings.db")) / 10);
        sourcePause.volume = volume;        
    }

    private void FixedUpdate()
    {       
        //Проверяем, идёт ли игра
        if (!gameStart || gameOver)
        {
            return;
        }        

        //Включение паузы
        if (Input.GetKeyDown(KeyCode.Backspace))
        {            
            if (!pause)
            {
                midScreenText.text = "PAUSE";
                midScreenSubText.fontSize = 30;
                midScreenSubText.text = "[Backspace] to resume";
                sourcePause.PlayOneShot(pauseSound);
                AudioManager.Instance.Pause();
                pause = !pause;
                return;
            }
            midScreenText.text = "";
            midScreenSubText.fontSize = 64;
            midScreenSubText.text = "";
            AudioManager.Instance.UnPause();
            pause = !pause;
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
            timer = difficultyUpTime;
        }

        coinUpTimer -= Time.deltaTime;
        if (coinUpTimer <= 0)
        {
            coinValue++;
            coinUpTimer = 1;
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
        difficulty = 1;
        timer = difficultyUpTime;
        coinUpTimer = 1;
        coinValue = BonusSpawner.Instance.GetCoinValue();
        gameStart = true;
        gameOver = false;
        luck = false;
        luckyTime = 0;
        midScreenText.text = "";
        midScreenSubText.text = "";
        AudioManager.Instance.PlaySound(Music);
        Indicators.Instance.MoveToTheScreen();
        Player.Instance.goToSartPosition();
        EnemySpawner.Instance.PrepareToStart();
        BonusSpawner.Instance.PrepareToStart();
    }

    public void EndGame() // ГЙЕМ ОВЕР, ЧУВАК
    {
        AudioManager.Instance.Stop();
        AudioManager.Instance.PlaySound(gameOverSound);
        AudioManager.Instance.PlaySound(hitSound);
        Indicators.Instance.MoveOutTheScreen();
        midScreenText.text = "GAME OVER";
        midScreenSubText.text = "SCORE: " + Player.Instance.getScore();
        gameOver = true;
    }
    public void ReturnToMainMenu() //Возврат в главное меню
    {
        midScreenText.text = "";
        midScreenSubText.text = "";
        pause = false;
        gameStart = false;
        gameOver = false;
        Buttons.Instance.MoveToTheScreen();
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

    public float GetCoinValue()
    {
        return coinValue;
    }
}
