using UnityEngine;
using UnityEngine.UI;

public class AppScreen_UICanvas_Menu_Settings_Slider_Sound : MonoBehaviour
{    
    public static AppScreen_UICanvas_Menu_Settings_Slider_Sound SingleOnScene { get; private set; }
    
    public bool Updated { get; set; }

    public int State { get; set; }

    [SerializeField] private Sprite[] spriteArray;

    private AudioSource audioSource;

    private void Awake()
    {
        SingleOnScene = this;

        Updated = false;

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        /*
        var _maxState = spriteArray.Length - 1;
            
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            audioSource.Play();
            var _newState = State >= _maxState ? _maxState : State + 1;
            State = _newState;
            Updated = true;
        }
            
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            audioSource.Play();
            var _newState = State <= 0 ? 0 : State - 1;
            State = _newState;
            Updated = true;
        }
            
        image.sprite = spriteArray[(int)State];

        float _volume = (float)(State / _maxState);
        ControlPers_AudioMixer.SingleOnScene.SetVolume(_volume);
        */ 
    }
}
