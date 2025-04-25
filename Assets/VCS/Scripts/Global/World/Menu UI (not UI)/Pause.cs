using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class Pause : MonoBehaviour
{
    //[SerializeField] SceneAsset scene_menu;
    //[SerializeField] SceneAsset scene_main;
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
        audioSource.volume = AudioManager.Instance.source.volume;
        menu = false;        
        startPosition = new Vector2(body.position.x, body.position.y);
        awayPosition = new Vector2(body.position.x, body.position.y + 4);

        // Постпроцесс
        postProcessVoolume = MainCameraZoom.Instance.GetComponent<PostProcessVolume>();
        postProcessVoolume.profile.TryGetSettings(out depthOfField);
    }

    private void Update()
    {  
        if (!Globalist.Instance.pause)
        {
            if (Input.GetKey(KeyCode.Backspace))
            {
                Globalist.Instance.Pause();
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
            AudioManager.Instance.PlaySound(switchSound);
            if (!menu)
            {   
                Globalist.Instance.UnPause();
            } else
            {
                Globalist.Instance.ReturnToMainMenu();
                AudioManager.Instance.Stop();
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
