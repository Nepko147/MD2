using UnityEngine;
using UnityEngine.UI;

public class AppScreen_Canvas_Settings_SoundVolume : MonoBehaviour
{    
    public static AppScreen_Canvas_Settings_SoundVolume SingleOnScene { get; private set; }

    public bool Active { get; set; }

    public bool Updated { get; set; }

    public float                        Settings_Sound_State { get; set; }
    AudioSource                         settings_audioSource;
    Image                               settings_sound_image;
    [SerializeField] private Sprite[]   settings_sound_spriteArray;
    

    private void Awake()
    {
        SingleOnScene = this;

        Updated = false;

        settings_audioSource = GetComponent<AudioSource>();
        settings_sound_image = GetComponent<Image>();
    }

    private void Update()
    {        
        if (Active)
        {
            var _maxState = settings_sound_spriteArray.Length - 1;
            
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                settings_audioSource.Play();
                var _newState = Settings_Sound_State >= _maxState ? _maxState : Settings_Sound_State + 1;
                Settings_Sound_State = _newState;
                Updated = true;
            }
            
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                settings_audioSource.Play();
                var _newState = Settings_Sound_State <= 0 ? 0 : Settings_Sound_State - 1;
                Settings_Sound_State = _newState;
                Updated = true;
            }
            
            settings_sound_image.sprite = settings_sound_spriteArray[(int)Settings_Sound_State];

            float _volume = (float)(Settings_Sound_State / _maxState);
            ControlPers_AudioMixer.SingleOnScene.SetVolume(_volume);                     
        }  
    }
}
