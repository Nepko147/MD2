using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Appscreen_Canvas_GameOverButtons : MonoBehaviour
{    
    [SerializeField] private AudioClip  gameOver_switchSound;
    Image                               gameOver_image;
    int                                 gameOver_state;

    [SerializeField] private Sprite texture_menu;
    [SerializeField] private Sprite texture_resume;

    private void Awake()
    {
        gameOver_state = 1;
        gameOver_image = GetComponent<Image>();
        gameOver_image.enabled = false;
    }

    private void Update()
    {        
        if (ControlScene_Entity_Main.Singletone.GameOver)
        {
            gameOver_image.enabled = true;

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                ControlPers_AudioManager.Singletone.PlaySound(gameOver_switchSound);                

                if (gameOver_state == 1)
                {
                    gameOver_state = 2;
                    GetComponent<Image>().sprite = texture_menu;
                }
                else
                {
                    gameOver_state = 1;
                    GetComponent<Image>().sprite = texture_resume;
                }
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                ControlPers_AudioManager.Singletone.PlaySound(gameOver_switchSound);
                if (gameOver_state == 1)
                {
                    ControlPers_AudioManager.Singletone.PlayMusic();
                    SceneManager.LoadScene(2);
                }
                else
                {
                    SceneManager.LoadScene(1);
                }
            }
        }
    }
}
