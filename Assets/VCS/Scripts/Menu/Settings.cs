using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private AudioClip switchSound;
    Vector2 startPosition;
    Vector2 onScreenPosition;
    private Rigidbody2D body;    
    private bool onScreen;

    public static Settings Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        body = GetComponent<Rigidbody2D>();
        startPosition = new Vector2(body.position.x, body.position.y);
        onScreenPosition = new Vector2(body.position.x, body.position.y + 7);
        onScreen = false;
    }

    private void FixedUpdate()
    {
        //Проверка активности
        if (!onScreen)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPosition, speed);
            return;
        }             
        transform.position = Vector2.MoveTowards(transform.position, onScreenPosition, speed);              
        
        //Обработка ввода клавиши BackSpace
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Buttons.Instance.MoveToTheScreen();
            Sound.Instance.Disactrivate();
            AudioManager.Instance.PlaySound(switchSound);
            MoveOut();
        }
    }   
    
    public void MoveToTheScreen()
    {
        onScreen = true;
    }

    public void MoveOut()
    {
        onScreen = false;        
    }
}
