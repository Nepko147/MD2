using UnityEngine;
using UnityEngine.UI;

public class AppScreen_UICanvas_Car : MonoBehaviour
{
    public static AppScreen_UICanvas_Car Singleton { get; private set; }

    bool active = false;

    public bool Done { get; set; }

    [SerializeField] private float      car_speed;
    [SerializeField] private AudioClip  car_sound;

    [SerializeField] private AudioClip dingSound;

    new Camera camera;
    private Vector3 screenWidth;

    float pivotLeftOffset_x;

    public void Activate()
    {
        ControlPers_AudioManager.Singletone.PlaySound(dingSound);
        ControlPers_AudioManager.Singletone.PlaySound(car_sound);
        active = true;
    }

    private void Awake()
    {
        Singleton = this;
    }

    private void Start()
    {
        camera = AppScreen_Camera_UI_Entity.Singletone.GetComponent<Camera>();
        screenWidth = new Vector3(camera.pixelWidth, 0, 0);
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
            transform.position += Vector3.right * car_speed;

            if (transform.position.x >= pivotLeftOffset_x)
            {                
                Done = true;
            }
        }        
    }
}
