using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int state;
    [SerializeField] private AudioClip switchSound;
    [SerializeField] private AudioClip sound;
    Vector2 startPosition;
    Vector2 awayPosition;
    private Rigidbody2D body;    
    private Animator anim;    
    private bool isActive;
    private bool onScreen;

    public static Buttons Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startPosition = new Vector2(body.position.x, body.position.y);
        awayPosition = new Vector2(body.position.x + 15.5f, body.position.y);        
        anim.SetInteger("state", 0);
        isActive = false;
        onScreen = true;
    }

    private void Update()
    {
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
            AudioManager.Instance.PlaySound(switchSound);
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
            AudioManager.Instance.PlaySound(switchSound);
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
            AudioManager.Instance.PlaySound(switchSound);
            switch (anim.GetInteger("state"))
            {
                case 1:
                    MoveOutTheScreen();
                    Globalist.Instance.StartGame();                    
                    break;
                case 2:
                    MoveOutTheScreen();
                    Settings.Instance.MoveToTheScreen();
                    Sound.Instance.Actievate();
                    break;
                case 3:
                    AudioManager.Instance.PlaySound(sound);
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
