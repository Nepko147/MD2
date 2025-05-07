using UnityEngine;
using UnityEngine.UI;

public class AappScreen_Canvas_Settings_Sound : MonoBehaviour
{    
    public static AappScreen_Canvas_Settings_Sound Singletone { get; private set; }

    public bool Active { get; set; }

    [SerializeField] private AudioClip  settings_sound_switchSound;
    Image                               settings_sound_image;

    [SerializeField] private Sprite[]   settings_sound_spriteArray;
    private float                       settings_sound_state;

    private void Awake()
    {
        Singletone = this;
        
        settings_sound_image = GetComponent<Image>();
    }

    private void Start()
    {
        settings_sound_state = ControlPers_DataHandler.Singletone.Settings_Volume_Get();
    }

    private void Update()
    {
        var _maxState = settings_sound_spriteArray.Length - 1;
        //Проверка активности
        if (Active)
        { 
            //Обработка клавиш ввода
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {            
                ControlPers_AudioManager.Singletone.PlaySound(settings_sound_switchSound);                
                var _newState = settings_sound_state >= _maxState ? _maxState : settings_sound_state + 1;
                settings_sound_state = _newState;
            }
            
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ControlPers_AudioManager.Singletone.PlaySound(settings_sound_switchSound);
                var _newState = settings_sound_state <= 0 ? 0 : settings_sound_state - 1;
                settings_sound_state = _newState;
            }
            
            //Отображаем текущую настройку
            settings_sound_image.sprite = settings_sound_spriteArray[(int)settings_sound_state];
            float _volume = (float)(settings_sound_state / _maxState);
            //World_BackGround_Bushes.Singletone.SetVolume(_volume);
            ControlPers_AudioManager.Singletone.SetVolume(_volume);

            //Закрытие меню
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                ControlPers_DataHandler.Singletone.Settings_Volume_Set((int)settings_sound_state);
                ControlPers_DataHandler.Singletone.SaveSettings();
            }  
        }  
    }
}
