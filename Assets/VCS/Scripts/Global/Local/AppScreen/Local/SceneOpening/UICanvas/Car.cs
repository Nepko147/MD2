using UnityEngine;
using UnityEngine.UI;

public class AppScreen_Local_SceneOpening_UICanvas_Car : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_Local_SceneOpening_UICanvas_Car SingleOnScene { get; private set; }

    private bool active = false;

    public bool Done { get; set; }

    [SerializeField] private float car_speed = 15.00f;

    [SerializeField] private AudioClip  sound_car;
    [SerializeField] private AudioClip  sound_ding;

    private new Camera camera;

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
        camera = AppScreen_General_Camera_UI_Entity.SingleOnScene.GetComponent<Camera>();
        var _worldToScreenPosition = camera.WorldToScreenPoint(transform.position);
        var _screenToWorldPosition = new Vector3(0, _worldToScreenPosition.y, _worldToScreenPosition.z);
        
        transform.position = camera.ScreenToWorldPoint(_screenToWorldPosition);
    }

    private void Update()
    {
        if (active 
            && !Done)
        { 
            transform.position += Vector3.right * car_speed * Time.deltaTime;

            if (RectTransform_ScreenPoint_Min().x >= Screen.width)
            {
                Done = true;
            }
        }        
    }
}
