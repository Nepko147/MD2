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
    private float timer;
    public float luckyTime;
    public bool pause;
    public bool gameStart;
    public bool gameOver;
    public bool luck;

    public static Globalist Instance { get; private set; }

    private void Awake()
    {
        Application.targetFrameRate = 60;        
        Instance = this;        
        difficulty = 1;
        timer = difficultyUpTime;
        luckyTime = 0;
        pause = false;
        gameStart = false;
        gameOver = false;
        luck = false;
        sourcePause = GetComponent<AudioSource>();        
    }

    private void Start()
    {              
        float volume = (((float)SaveLoader.Instance.Load("volume")) / 10);
        sourcePause.volume = volume;        
    }

    private void FixedUpdate()
    {       
        //���������, ��� �� ����
        if (!gameStart || gameOver)
        {
            return;
        }        

        //��������� �����
        if (Input.GetKeyDown(KeyCode.Backspace))
        {            
            if (!pause)
            {
                UI_IndicatorsCanvas_Entity.Instance.SetPause(true);
                sourcePause.PlayOneShot(pauseSound);
                AudioManager.Instance.Pause();
                pause = !pause;
                return;
            }
            UI_IndicatorsCanvas_Entity.Instance.SetPause(false);
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
        
        //�������� ��������� ���� ����� ����������� ���������� �������
        timer -= Time.deltaTime;
        if (timer <= 0 && difficulty < difficultyMax)
        {
            difficulty += difficultyScale;
            timer = difficultyUpTime;
        }

        //������ �����, ����� ��� ���������
        if (!AudioManager.Instance.source.isPlaying)
        {
            AudioManager.Instance.PlaySound(Music);
        }
 }

    public bool canPlay() //��� ����������� ������ �������� ���������, ��� �� ����
    {
        return gameStart && !gameOver && !pause;
    }
    public bool CanSpawnEnemies() //��� ����������� ������ �������� ���������, ��� �� ����
    {
        return gameStart && !gameOver && !pause && !luck;
    }

    public void StartGame() //��������������� ���������� � ������/��������
    {
        difficulty = 1;
        timer = difficultyUpTime;
        gameStart = true;
        gameOver = false;
        luck = false;
        luckyTime = 0;
        AudioManager.Instance.PlaySound(Music);
        UI_IndicatorsCanvas_Entity.Instance.MoveToTheScreen();
        Player.Instance.goToSartPosition();
        EnemySpawner.Instance.PrepareToStart();
        BonusSpawner.Instance.PrepareToStart();
        MainCameraSlope.Instance.RotationReset();
        UI_IndicatorsCanvas_Entity.Instance.PrepareToStart();
    }

    public void EndGame() // ���� ����, �����
    {
        AudioManager.Instance.Stop();
        AudioManager.Instance.PlaySound(gameOverSound);
        AudioManager.Instance.PlaySound(hitSound);
        UI_IndicatorsCanvas_Entity.Instance.ShowGameOver();
        MainCameraSlope.Instance.RotationReset();
        MainCameraZoom.Instance.ZoomReset();        
        gameOver = true;
    }
    public void ReturnToMainMenu() //������� � ������� ����
    {        
        pause = false;
        gameStart = false;
        gameOver = false;
        Buttons.Instance.MoveToTheScreen();
        MainCameraZoom.Instance.ZoomReset();
        UI_IndicatorsCanvas_Entity.Instance.MoveOutTheScreen();
    }

    public float GetDifficultyScale() //��� ����������� ������ �������� �������� ������� ���������
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
}
