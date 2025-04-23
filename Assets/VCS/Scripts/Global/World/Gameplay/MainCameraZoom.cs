using UnityEngine;

public class MainCameraZoom : MonoBehaviour    
{
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float cameraMaxZoom; //Чем меньше значение, тем сильнее зум
    [SerializeField] private float cameraMinZoom; //Чем больше значение, тем слабее зум
    [SerializeField] private float zoomDelay;
    private Camera camera;
    private float originalCameraSize;
    private float overZoomTimer; //Таймер периодического оверзума
    private bool overZoom;
    private Vector3 startPosition;
    private Vector3 newPosition;

    public static MainCameraZoom Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        camera = GetComponent<Camera>();
        originalCameraSize = camera.orthographicSize;         
        startPosition = transform.position;
        newPosition = new Vector3(startPosition.x, startPosition.y - 0.4f, startPosition.z);
        ZoomReset();
    }
    private void FixedUpdate()
    {       
        if (!Globalist.Instance.canPlay())
        {            
            return;
        }

        //Вход в состояние оверЗума
        if (camera.orthographicSize > cameraMaxZoom && overZoom && overZoomTimer <= 0)
        {
            camera.orthographicSize -= cameraSpeed; //Увеличение Зума
            camera.transform.position = Vector3.MoveTowards(transform.position, newPosition, cameraSpeed); //Сдвиг камеры вниз
            //При достижении максимального зума (оверЗум), даём команду камере уменьшить зум
            if (camera.orthographicSize <= cameraMaxZoom)
            {
                overZoom = false;
            }
        }
        
        overZoomTimer -= Time.deltaTime;

        //Выход из состояния оверЗума
        if (!overZoom)
        {            
            camera.orthographicSize += cameraSpeed; //Уменьшение зума
            camera.transform.position = Vector3.MoveTowards(transform.position, startPosition, cameraSpeed); //Сдвиг камеры в изначальное положение
            //При достижении минимального зума, разрешаем оверзум и обновляем его таймер
            if (camera.orthographicSize >= cameraMinZoom)
            {
                camera.orthographicSize = originalCameraSize;
                overZoomTimer = zoomDelay;
                overZoom = true;
            }            
        }
    }

    public void ZoomReset()
    {
        camera.orthographicSize = originalCameraSize;
        transform.position = startPosition;
        overZoom = true;
        overZoomTimer = 0;
    }
}
