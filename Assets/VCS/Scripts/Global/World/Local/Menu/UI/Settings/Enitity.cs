using UnityEngine;
using UnityEngine.UI;

public class AappScreen_Canvas_Settings_Entity : MonoBehaviour
{
    public static AappScreen_Canvas_Settings_Entity Singletone { get; private set; }
    
    public bool Active { get; set; }

    [SerializeField] private AudioClip  settings_switchSound;
    [SerializeField] private GameObject settings_soundVolume;
    Image                               settings_soundVolume_Image;

    [SerializeField] private GameObject settings_backspace;
    Image                               settings_backspace_image;

    private void Awake()
    {
        Singletone = this;

        Active = false;

        settings_soundVolume_Image = settings_soundVolume.GetComponent<Image>();
        settings_soundVolume_Image.enabled = false;
        settings_backspace_image = settings_backspace.GetComponent<Image>();
        settings_backspace_image.enabled = false;
    }

    private void FixedUpdate()
    {
        //Проверка активности
        if (Active)
        {
            settings_soundVolume_Image.enabled = true;
            settings_backspace_image.enabled = true;

            //Обработка ввода клавиши BackSpace
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                Appscreen_Canvas_Buttons.Singletone.Active = true;
                AappScreen_Canvas_Settings_Sound.Singletone.Active = false;
                ControlPers_AudioManager.Singletone.PlaySound(settings_switchSound);
                Active = false;
            }            
        }
        else
        {
            settings_soundVolume_Image.enabled = false;
            settings_backspace_image.enabled = false;
        }
    } 
}
