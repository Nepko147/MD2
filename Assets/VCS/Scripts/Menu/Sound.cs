using UnityEngine;

public class Sound : MonoBehaviour
{    
    [SerializeField] private AudioClip switchSound;
    private Animator anim;
    private float state;    
    private bool active;
    private bool needToSave;
    private bool needToLoad;

    public static Sound Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        anim = GetComponent<Animator>();
        active = false;
        needToSave = false;
        needToLoad = true;        
    }

    private void FixedUpdate()
    {
        //Проверка активности
        if (!active && !needToSave)
        {            
            return;
        }

        if (needToLoad)
        {
            state = SaveLoader.Instance.Load("volume");
            needToLoad = false;
        }

        //Обработка клавиш ввода
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {            
            AudioManager.Instance.PlaySound(switchSound);
            state = state >= 10 ? 10 : state + 1;
            needToSave = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            AudioManager.Instance.PlaySound(switchSound);
            state = state <= 0 ? 0 : state - 1;
            needToSave = true;
        }     
        
        //Отображаем текущую настройку
        anim.Play("Base Layer.a_menu_sound", 0, state / anim.GetCurrentAnimatorStateInfo(0).length);
        float volume = ((float)(state/10));
        ApplyVolume(volume);

        //Закрытие меню
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SaveLoader.Instance.Save((int)state, "volume");
            needToSave = false;
        }        
    }   
    
    public void Actievate()
    {
        active = true;
    }

    public void Disactrivate()
    {
        active = false;        
    }

    private void ApplyVolume(float _volume)
    {
        Bushes.Instance.SetVolume(_volume);
        Globalist.Instance.SetVolume(_volume);
        AudioManager.Instance.SetVolume(_volume);
    }
}
