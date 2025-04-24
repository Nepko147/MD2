using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        menu = true;        
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
            anim.SetBool("menu", menu);
            menu = !menu;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            AudioManager.Instance.PlaySound(switchSound);
            if (!menu)
            {                
                SceneManager.LoadScene(1);
                Globalist.Instance.gameOver = false;
                Globalist.Instance.gameStart = false;
            } else
            {
                SceneManager.LoadScene(2);
                Globalist.Instance.gameOver = false;
                Globalist.Instance.gameStart = true;

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
