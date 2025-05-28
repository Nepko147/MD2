using UnityEngine;
using UnityEngine.UI;

public class AppScreen_UICanvas_Car : MonoBehaviour
{
    public static AppScreen_UICanvas_Car SingleOnScene { get; private set; }

    private bool active = false;

    public bool Done { get; set; }

    [SerializeField] private float car_speed = 15.00f;

    [SerializeField] private AudioClip  sound_car;
    [SerializeField] private AudioClip  sound_ding;

    private new Camera camera;

    private float pivotLeftOffset_x;

    public void Activate()
    {
        ControlPers_AudioMixer_Sounds.SingleOnScene.Play(sound_car);
        ControlPers_AudioMixer_Sounds.SingleOnScene.Play(sound_ding);
        active = true;
    }

    private void Awake()
    {
        SingleOnScene = this;
    }

    private void Start()
    {
        camera = AppScreen_Camera_UI_Entity.SingleOnScene.GetComponent<Camera>();
        var _worldToScreenPosition = camera.WorldToScreenPoint(transform.position);
        var _screenToWorldPosition = new Vector3(0, _worldToScreenPosition.y, _worldToScreenPosition.z);
        
        transform.position = camera.ScreenToWorldPoint(_screenToWorldPosition);

        var _rectTransform = GetComponent<RectTransform>();
        var _referenceResolution_x = Opening_AppScreen_UICanvas_Entity.Singltone.GetComponent<CanvasScaler>().referenceResolution.x;
        var _offset = (_rectTransform.rect.width * _rectTransform.pivot.x * _rectTransform.localScale.x) * camera.pixelWidth / _referenceResolution_x + camera.pixelWidth;        
        var _vec3 = new Vector3(_offset, 0, 0);
        pivotLeftOffset_x = camera.ScreenToWorldPoint(_vec3).x;
    }

    private void Update()
    {
        if (active 
            && !Done)
        { 
            transform.position += Vector3.right * car_speed * Time.deltaTime;

            if (transform.position.x >= pivotLeftOffset_x)
            {                
                Done = true;
            }
        }        
    }
}
