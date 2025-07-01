using UnityEngine;

public class AppScreen_Local_SceneOpening_UICanvas_Car : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_Local_SceneOpening_UICanvas_Car SingleOnScene { get; private set; }

    private bool active = false;

    public bool Done { get; set; }

    private Camera uiCamera;
    
    [SerializeField] private float move_time = 1f;
    private Vector3 move_pos_screen_start;
    private Vector3 move_pos_screen_final;
    private float move_step_x;

    [SerializeField] private AudioClip  sound_car;
    [SerializeField] private AudioClip  sound_ding;

    public void Activate()
    {
        ControlPers_AudioMixer_Sounds.SingleOnScene.Play(sound_car);
        ControlPers_AudioMixer_Sounds.SingleOnScene.Play(sound_ding);

        active = true;
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        uiCamera = AppScreen_General_UICanvas_Entity.SingleOnScene.Camera;
    }

    private void Update()
    {
        if (!active)
        {
            var _pos_screen_init = uiCamera.WorldToScreenPoint(transform.position);

            move_pos_screen_start.x = AppScreen_General_UICanvas_Entity.SingleOnScene.RectTransform_ScreenPoint_Min().x;
            move_pos_screen_start.y = _pos_screen_init.y;
            move_pos_screen_start.z = _pos_screen_init.z;

            transform.position = uiCamera.ScreenToWorldPoint(move_pos_screen_start);

            move_pos_screen_final.x = AppScreen_General_UICanvas_Entity.SingleOnScene.RectTransform_ScreenPoint_Max().x;
            move_pos_screen_final.y = move_pos_screen_start.y;
            move_pos_screen_final.z = move_pos_screen_start.z;

            var _pos_world_final = uiCamera.ScreenToWorldPoint(move_pos_screen_final);

            move_step_x = (_pos_world_final.x - transform.position.x) / move_time;
        }
        else
        {
            if (!Done)
            {
                transform.position += Vector3.right * move_step_x * Time.deltaTime;
                
                if (RectTransform_ScreenPoint_Min().x >= AppScreen_General_UICanvas_Entity.SingleOnScene.RectTransform_ScreenPoint_Max().x)
                {
                    Done = true;
                }
            }
        }
    }
}
