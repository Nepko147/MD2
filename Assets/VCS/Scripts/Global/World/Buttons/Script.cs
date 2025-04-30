using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class World_Buttons : MonoBehaviour
{
    [SerializeField] private float sceneSwitchTimer;
    [SerializeField] private float speed;
    [SerializeField] private int state;
    [SerializeField] private AudioClip switchSound;
    Vector2 startPosition;
    Vector2 awayPosition;
    private Rigidbody2D body;    
    private Animator anim;    
    private bool isActive;
    private bool onScreen;

    // Постпроцесс
    private PostProcessVolume postProcessVoolume;
    private DepthOfField depthOfField;

    public bool GO { get; private set; }

    public static World_Buttons Singletone { get; private set; }

    private void Start()
    {
        postProcessVoolume = AppScreen_Camera_MainCameraZoom.Singletone.GetComponent<PostProcessVolume>();
        postProcessVoolume.profile.TryGetSettings(out depthOfField);
    }

    private void Awake()
    {
        Singletone = this;
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startPosition = new Vector2(body.position.x, body.position.y);
        awayPosition = new Vector2(body.position.x + 15.5f, body.position.y);        
        anim.SetInteger("state", 1);
        isActive = true;
        onScreen = true;
    }

    private void Update()
    {

        if (ControlPers_Globalist.Singletone.gameStart)
        {
            sceneSwitchTimer -= Time.deltaTime;
            depthOfField.aperture.value = depthOfField.aperture.value < 3 ? depthOfField.aperture.value + 0.045f : 3;
            if (sceneSwitchTimer < 0)
            {
                GO = true;                
            }
        }

        //Отодвигаем кнопки, при необходимости
        if (!onScreen)
        {
            transform.position = Vector2.MoveTowards(transform.position, awayPosition, speed);
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, startPosition, speed);
        //Проверка активности
        if (!isActive)
        {
            return;
        }
        
        //Обработка ввода клавиши ВВЕРХ
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ControlPers_AudioManager.Singletone.PlaySound(switchSound);
            if (anim.GetInteger("state") == 1)
            {
                anim.SetInteger("state", 4);
            } else
            {
                anim.SetInteger("state", anim.GetInteger("state") - 1);
            }
        }
        //Обработка ввода клавиши ВНИЗ
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ControlPers_AudioManager.Singletone.PlaySound(switchSound);
            if (anim.GetInteger("state") == 4)
            {
                anim.SetInteger("state", 1);
            }
            else
            {
                anim.SetInteger("state", anim.GetInteger("state") + 1);
            }
        }
        //Обработка ввода клавиши Enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ControlPers_AudioManager.Singletone.PlaySound(switchSound);
            switch (anim.GetInteger("state"))
            {
                case 1:
                    MoveOutTheScreen();
                    Destroy(World_BackGround_Lights.Singletone.gameObject);
                    ControlPers_Globalist.Singletone.StartGame();                    
                    break;
                case 2:
                    MoveOutTheScreen();
                    World_Settings_Entity.Singletone.MoveToTheScreen();
                    World_Settings_Sound.Singletone.Actievate();
                    break;
                case 3:
                    break;
                case 4:
                    Application.Quit();
                    break;
            }
        }
    }
   
    //Процедура активации ДАННОГО объекта из ДРУГИХ объектов 
    public void MakeActive(bool _activation)
    {
        isActive = _activation;
        anim.SetInteger("state", 1);
    }
    public void MoveOutTheScreen()
    {
        onScreen = false;
        MakeActive(false);
    }

    public void MoveToTheScreen()
    {
        onScreen = true;
        MakeActive(true);
    }
}
