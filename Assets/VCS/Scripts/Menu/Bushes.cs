using UnityEngine;

public class Bushes : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private AudioClip soundMenu;
    private AudioSource audioSource;
    Vector2 startPosition;
    Vector2 awayPosition;
    private Rigidbody2D body;

    public static Bushes Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
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
            float volume = (((float)SaveLoader.Instance.Load("volume")) / 10);
            audioSource.volume = volume;
        }            
    }

    private void FixedUpdate()
    {
        //���������� �����, ����� ������ ����
        if (GameObject.Find("Globalist").GetComponent<Globalist>().gameStart == true)
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
