using UnityEngine;

public class AppScreen_General_Camera_World_Entity_Zoom : MonoBehaviour    
{
    public static AppScreen_General_Camera_World_Entity_Zoom SingleOnScene { get; private set; }

    public bool Active { get; set; }

    new private Camera camera;
    private Vector3 camera_position_origin;
    private Vector3 camera_position_correction;
    private const float CAMERA_POSITION_CORRECTION_SIZE = 0.08f;
    private const float CAMERA_POSITION_CORRECTION_SPEED = 0.0001f;

    private float camera_originalFieldOfView;

    private const float CAMERA_ZOOM_SPEED = 0.25f;
    private const float CAMERA_ZOOM_MAX = 23f; //Чем меньше значение, тем сильнее зум
    private const float CAMERA_ZOOM_MIN = 24f; //Чем больше значение, тем слабее зум
    private const float CAMERA_ZOOM_DELAY = 5f;    
    
    private bool    camera_overZoom;
    private float   camera_overZoom_timer; //Таймер периодического оверзума

    private void Awake()
    {
        SingleOnScene = this;

        Active = false;
        camera = GetComponent<Camera>();
        camera_originalFieldOfView = camera.fieldOfView;
        camera_position_origin = transform.localPosition;
        camera_position_correction = new Vector3(camera_position_origin.x, camera_position_origin.y - CAMERA_POSITION_CORRECTION_SIZE, camera_position_origin.z);
    }
    private void Update()
    {       
        if (Active)
        {            
            //Вход в состояние оверЗума
            if (camera.fieldOfView > CAMERA_ZOOM_MAX && camera_overZoom && camera_overZoom_timer <= 0)
            {
                camera.fieldOfView -= CAMERA_ZOOM_SPEED * Time.deltaTime; //Увеличение Зума
                camera.transform.localPosition = Vector3.MoveTowards(transform.localPosition, camera_position_correction, CAMERA_POSITION_CORRECTION_SPEED * Time.deltaTime); //Сдвиг камеры вниз
            
                //При достижении максимального зума (оверЗум), даём команду камере уменьшить зум
                if (camera.fieldOfView <= CAMERA_ZOOM_MAX)
                {
                    camera_overZoom = false;
                }
            }

            camera_overZoom_timer -= Time.deltaTime;

            //Выход из состояния оверЗума
            if (!camera_overZoom)
            {
                camera.fieldOfView += CAMERA_ZOOM_SPEED * Time.deltaTime; //Уменьшение зума
                camera.transform.localPosition = Vector3.MoveTowards(transform.localPosition, camera_position_origin, CAMERA_POSITION_CORRECTION_SPEED * Time.deltaTime); //Сдвиг камеры в изначальное положение
            
                //При достижении минимального зума, разрешаем оверзум и обновляем его таймер
                if (camera.fieldOfView >= CAMERA_ZOOM_MIN)
                {
                    camera.fieldOfView = camera_originalFieldOfView;
                    camera_overZoom_timer = CAMERA_ZOOM_DELAY;
                    camera_overZoom = true;
                }            
            }
        }
    }
}
