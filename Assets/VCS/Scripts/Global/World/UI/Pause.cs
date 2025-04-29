using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class World_UI_Pause : MonoBehaviour
{
    [SerializeField] private AudioClip switchSound;
    private Rigidbody2D body;
    private Animator anim;
    private AudioSource audioSource;
    private bool menu;
    Vector2 startPosition;
    Vector2 awayPosition;

    // Постпроцесс
    private PostProcessVolume postProcessVoolume;
    private DepthOfField depthOfField;    

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = ControlPers_AudioManager.Singletone.source.volume;
        menu = false;        
        startPosition = new Vector2(body.position.x, body.position.y);
        awayPosition = new Vector2(body.position.x, body.position.y + 4);        
    }

    private void Start()
    {
        // Постпроцесс
        postProcessVoolume = AppScreen_Camera_MainCameraZoom.Singletone.GetComponent<PostProcessVolume>();
        postProcessVoolume.profile.TryGetSettings(out depthOfField);
    }

    private void Update()
    {  
        if (!ControlPers_Globalist.Singletone.pause)
        {
            if (Input.GetKey(KeyCode.Backspace))
            {
                ControlPers_Globalist.Singletone.Pause();
            }
            MoveOutTheScreen();
            return;
        }

        MoveToTheScreen();

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            audioSource.PlayOneShot(switchSound);
            menu = !menu;
            anim.SetBool("menu", menu);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            ControlPers_AudioManager.Singletone.PlaySound(switchSound);
            if (!menu)
            {   
                ControlPers_Globalist.Singletone.UnPause();
            } else
            {
                ControlPers_Globalist.Singletone.ReturnToMainMenu();
                ControlPers_AudioManager.Singletone.Stop();
                SceneManager.LoadScene(1);
            }
        }
    }

    public void MoveToTheScreen()
    {
        transform.position = Vector2.MoveTowards(transform.position, awayPosition, 1);
    }

    public void MoveOutTheScreen()
    {
        transform.position = Vector2.MoveTowards(transform.position, startPosition, 1);
    }
}
