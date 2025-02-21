using UnityEngine;

public class Sound : MonoBehaviour
{    
    [SerializeField] private AudioClip switchSound;
    private Animator anim;
    private string settingsFileName;
    private float state;    
    private bool active;
    private bool needToSave;
    private bool needToLoad;

    public static Sound Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        anim = GetComponent<Animator>();
        settingsFileName = "Settings.db";
        active = false;
        needToSave = false;
        needToLoad = true;        
    }

    private void FixedUpdate()
    {
        //�������� ����������
        if (!active && !needToSave)
        {            
            return;
        }

        if (needToLoad)
        {
            state = SaveLoader.Instance.Load(settingsFileName);
            needToLoad = false;
        }

        //��������� ������ �����
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
        
        //���������� ������� ���������
        anim.Play("Base Layer.a_menu_sound", 0, state / anim.GetCurrentAnimatorStateInfo(0).length);
        float volume = ((float)(state/10));
        Bushes.Instance.SetVolume(volume);
        Globalist.Instance.SetVolume(volume);
        AudioManager.Instance.SetVolume(volume);
        //�������� ����
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SaveLoader.Instance.Save((int)state, settingsFileName);
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
}
