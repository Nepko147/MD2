using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class Appscreen_Canvas_Buttons : MonoBehaviour
{
    public static Appscreen_Canvas_Buttons Singletone { get; private set; }

    public bool Active { get; set; }

    public bool GO { get; private set; }

    [SerializeField] private float      buttons_sceneSwitchTimer;    
    private int                         buttons_state;
    [SerializeField] private AudioClip  buttons_switchSound;
    [SerializeField] private Sprite[]   buttons_spriteArray;
    Image                               settings_sound_image;

    // Переедет в камеру
    private PostProcessVolume postProcessVoolume;
    private DepthOfField depthOfField;

    private void Start()
    {
        postProcessVoolume = Appscreen_Camera_WorldCammera_Zoom.Singletone.GetComponent<PostProcessVolume>();
        postProcessVoolume.profile.TryGetSettings(out depthOfField);
    }

    private void Awake()
    {
        Singletone = this;

        buttons_state = 0;
        settings_sound_image = GetComponent<Image>();      
        Active = true;
    }

    private void Update()
    {
        if (ControlScene_Entity_Menu.Singletone.GameStart)
        {
            buttons_sceneSwitchTimer -= Time.deltaTime;
            depthOfField.aperture.value = depthOfField.aperture.value < 3 ? depthOfField.aperture.value + 0.045f : 3;
            if (buttons_sceneSwitchTimer < 0)
            {
                GO = true;                
            }
        }
        
        //Проверка активности
        if (Active)
        {
            settings_sound_image.enabled = true;
            settings_sound_image.sprite = buttons_spriteArray[buttons_state];            
            
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ControlPers_AudioManager.Singletone.PlaySound(buttons_switchSound);
                if (buttons_state == 0)
                {
                    buttons_state = 3;
                } else
                {
                    --buttons_state;
                }
            }            
            
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ControlPers_AudioManager.Singletone.PlaySound(buttons_switchSound);
                if (buttons_state == 3)
                {
                    buttons_state = 0;
                }
                else
                {
                    ++buttons_state;
                }
            }
           
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ControlPers_AudioManager.Singletone.PlaySound(buttons_switchSound);
                switch (buttons_state)
                {
                    case 0: 
                        Active = false;
                        ControlScene_Entity_Menu.Singletone.GameStart = true;
                        ControlPers_AudioManager.Singletone.PlayMusic();
                        Destroy(GameObject.Find("Lights (Bushes)"));
                        break;
                    case 1: 
                        Active = false;
                        AappScreen_Canvas_Settings_Entity.Singletone.Active = true;
                        AappScreen_Canvas_Settings_Sound.Singletone.Active = true;
                        break;
                    case 2:
                        break;
                    case 3:
                        Application.Quit();
                        break;
                }
            }
        }
        else
        {
            settings_sound_image.enabled = false;
        }
    }   
}
