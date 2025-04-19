using UnityEngine;

public class MainCameraZoom : MonoBehaviour    
{
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float cameraMaxZoom; //��� ������ ��������, ��� ������� ���
    [SerializeField] private float cameraMinZoom; //��� ������ ��������, ��� ������ ���
    [SerializeField] private float zoomDelay;
    private Camera camera;
    private float originalCameraSize;
    private float overZoomTimer; //������ �������������� ��������
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

        //���� � ��������� ��������
        if (camera.orthographicSize > cameraMaxZoom && overZoom && overZoomTimer <= 0)
        {
            camera.orthographicSize -= cameraSpeed; //���������� ����
            camera.transform.position = Vector3.MoveTowards(transform.position, newPosition, cameraSpeed); //����� ������ ����
            //��� ���������� ������������� ���� (�������), ��� ������� ������ ��������� ���
            if (camera.orthographicSize <= cameraMaxZoom)
            {
                overZoom = false;
            }
        }
        
        overZoomTimer -= Time.deltaTime;

        //����� �� ��������� ��������
        if (!overZoom)
        {            
            camera.orthographicSize += cameraSpeed; //���������� ����
            camera.transform.position = Vector3.MoveTowards(transform.position, startPosition, cameraSpeed); //����� ������ � ����������� ���������
            //��� ���������� ������������ ����, ��������� ������� � ��������� ��� ������
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
