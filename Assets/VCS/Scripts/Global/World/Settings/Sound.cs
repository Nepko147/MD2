using UnityEngine;

public class World_Settings_Sound : MonoBehaviour
{    
    [SerializeField] private AudioClip switchSound;
    private Animator anim;
    private float state;    
    private bool active;
    private bool needToSave;
    private bool needToLoad;

    public static World_Settings_Sound Singletone { get; private set; }

    private void Awake()
    {
        Singletone = this;
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
            state = ControlPers_SaveLoader.Singletone.Load("volume");
            needToLoad = false;
        }

        //Обработка клавиш ввода
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {            
            ControlPers_AudioManager.Singletone.PlaySound(switchSound);
            state = state >= 10 ? 10 : state + 1;
            needToSave = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ControlPers_AudioManager.Singletone.PlaySound(switchSound);
            state = state <= 0 ? 0 : state - 1;
            needToSave = true;
        }     
        
        //Отображаем текущую настройку
        anim.Play("Base Layer.State", 0, state / anim.GetCurrentAnimatorStateInfo(0).length);
        float volume = ((float)(state/10));
        ApplyVolume(volume);

        //Закрытие меню
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ControlPers_SaveLoader.Singletone.Save((int)state, "volume");
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
        World_BackGround_Bushes.Singletone.SetVolume(_volume);
        ControlPers_Globalist.Singletone.SetVolume(_volume);
        ControlPers_AudioManager.Singletone.SetVolume(_volume);
    }
}
