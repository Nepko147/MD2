using UnityEngine;
//using UnityEditor;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lag;
    [SerializeField] private AudioClip carSound;
    [SerializeField] private AudioClip dingSound;
    private Rigidbody2D body;
    private bool timeToDestroyThisShit;
    bool isPlayed = false;

    public static Car Singleton { get; private set; }

    public bool Done { get; private set; }

    private void Awake()
    {
        Singleton = this;
        body = GetComponent<Rigidbody2D>();
        timeToDestroyThisShit = false;
    }

    private void Update()
    {
        //Ожидание ввода от игрока
        if (Input.GetKey(KeyCode.UpArrow) || 
            Input.GetKey(KeyCode.DownArrow) ||
            Input.GetKey(KeyCode.Return) ||
            Input.GetKey(KeyCode.Backspace))
        {
            timeToDestroyThisShit = true;
        }
        //Начинаем отсчёт
        if (timeToDestroyThisShit)
        {
            lag -= Time.deltaTime;
            Lights.Instance.PlayAnimation();
        }
        //Если время пришло, объект начинает движение
        if (lag <= 0)
        {
            AudioManager.Instance.PlaySound(carSound);            
            body.linearVelocity = new Vector2(speed, 0);
            lag = 99;
        }

        //после выхода из кадра, объект активирует меню и самоуничтожается!
        if (transform.position.x >= 42.5f && !isPlayed)
        {
            AudioManager.Instance.PlaySound(dingSound);
            isPlayed = true;
        }

        if (transform.position.x >= 42.5f * 1.8f)
        {
            Done = true;
        }
    }
}
