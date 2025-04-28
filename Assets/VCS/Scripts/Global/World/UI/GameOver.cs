using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class GameOver : MonoBehaviour
{
    //[SerializeField] SceneAsset scene_menu;
    //[SerializeField] SceneAsset scene_main;
    [SerializeField] private AudioClip switchSound;
    private Rigidbody2D body;
    private Animator anim;
    private bool menu;
    Vector2 startPosition;
    Vector2 awayPosition;

    // Постпроцесс
    private PostProcessVolume postProcessVoolume;
    private DepthOfField depthOfField;

    private void Start()
    {
        // Постпроцесс
        postProcessVoolume = MainCameraZoom.Instance.GetComponent<PostProcessVolume>();
        postProcessVoolume.profile.TryGetSettings(out depthOfField);
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        menu = false;        
        startPosition = new Vector2(body.position.x, body.position.y);
        awayPosition = new Vector2(body.position.x, body.position.y + 4);
    }

    private void Update()
    {        
        if (!Globalist.Instance.gameOver)
        {
            MoveOutTheScreen();
            return;
        }

        MoveToTheScreen();

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            AudioManager.Instance.PlaySound(switchSound);
            menu = !menu;
            anim.SetBool("menu", menu);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            AudioManager.Instance.PlaySound(switchSound);
            if (!menu)
            {
                SceneManager.LoadScene(2);
                Globalist.Instance.StartGame();
                
            } else
            {
                SceneManager.LoadScene(1);
                Globalist.Instance.gameOver = false;
                Globalist.Instance.gameStart = false;
                depthOfField.aperture.value = 1;
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
