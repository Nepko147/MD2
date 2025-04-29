using UnityEngine;

public class World_BackGround_Bushes : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private AudioClip soundMenu;
    private AudioSource audioSource;
    Vector2 startPosition;
    Vector2 awayPosition;
    private Rigidbody2D body;

    public static World_BackGround_Bushes Singletone { get; private set; }

    private void Awake()
    {
        Singletone = this;
        body = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }                
        startPosition = new Vector2(body.position.x, body.position.y);
        awayPosition = new Vector2(body.position.x - 35.5f, body.position.y);
    }

    private void Start()
    {
        if(!(audioSource == null))
        {
            float volume = (((float)ControlPers_SaveLoader.Singletone.Load("volume")) / 10);
            audioSource.volume = volume;
        }            
    }

    private void FixedUpdate()
    {
        //Отодвигаем кусты, после начала игры
        if (ControlPers_Globalist.Singletone.gameStart == true)
        {
            if (audioSource != null)
            {
                audioSource.Stop();
                audioSource.loop = false;
            }
            MoveAway();
        } else
        {
            if (audioSource != null && !audioSource.loop)
            {
                audioSource.Play();
                audioSource.loop = true;
            }
            MoveBack();
        }
    }
   
    public void MoveAway()
    {
        transform.position = Vector2.MoveTowards(transform.position, awayPosition, speed);
    }

    public void MoveBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, startPosition, speed * 4);
    }    

    public void SetVolume(float _volume)
    {
        audioSource.volume = _volume;
    }
}
