using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Appscreen_Canvas_PauseButtons : MonoBehaviour
{
    [SerializeField] private AudioClip  pauseButtons_switchSound;
    Image                               pauseButtons_image;
    int                                 pauseButtons_state;

    [SerializeField] private Sprite texture_menu;
    [SerializeField] private Sprite texture_resume;

    private void Awake()
    {        
        pauseButtons_state = 1;
        pauseButtons_image = GetComponent<Image>();
        pauseButtons_image.enabled = false;
    }

    private void Update()
    {  
        if (ControlScene_Entity_Main.Singletone.Pause)
        {
            pauseButtons_image.enabled = true;
            
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                ControlPers_AudioManager.Singletone.PlaySound(pauseButtons_switchSound);
                
                if (pauseButtons_state == 1)
                {
                    pauseButtons_state = 2;
                    GetComponent<Image>().sprite = texture_menu;
                }
                else
                {
                    pauseButtons_state = 1;
                    GetComponent<Image>().sprite = texture_resume;
                }
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                ControlPers_AudioManager.Singletone.PlaySound(pauseButtons_switchSound);
                
                if (pauseButtons_state == 1)
                {
                    pauseButtons_image.enabled = false;
                    ControlScene_Entity_Main.Singletone.UnPause();
                }
                else
                {
                    ControlPers_AudioManager.Singletone.Stop();
                    SceneManager.LoadScene(1);
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Backspace))
            {
                pauseButtons_state = 1;
                ControlScene_Entity_Main.Singletone.SetPause();
            }
            
        }        
    }
}
