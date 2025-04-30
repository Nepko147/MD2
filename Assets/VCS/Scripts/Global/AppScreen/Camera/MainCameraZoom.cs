using UnityEngine;

public class AppScreen_Camera_MainCameraZoom : MonoBehaviour    
{
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float cameraMaxZoom; //��� ������ ��������, ��� ������� ���
    [SerializeField] private float cameraMinZoom; //��� ������ ��������, ��� ������ ���
    [SerializeField] private float zoomDelay;
    private Camera thisCamera;
    private float originalCameraSize;
    private float overZoomTimer; //������ �������������� ��������
    private bool overZoom;
    private Vector3 startPosition;
    private Vector3 newPosition;

    public static AppScreen_Camera_MainCameraZoom Singletone { get; private set; }

    private void Awake()
    {
        Singletone = this;
        thisCamera = GetComponent<Camera>();
        originalCameraSize = thisCamera.orthographicSize;         
        startPosition = transform.position;
        newPosition = new Vector3(startPosition.x, startPosition.y - 0.4f, startPosition.z);
        ZoomReset();
    }
    private void FixedUpdate()
    {       
        if (!ControlPers_Globalist.Singletone.canPlay())
        {            
            return;
        }

        //���� � ��������� ��������
        if (thisCamera.orthographicSize > cameraMaxZoom && overZoom && overZoomTimer <= 0)
        {
            thisCamera.orthographicSize -= cameraSpeed; //���������� ����
            thisCamera.transform.position = Vector3.MoveTowards(transform.position, newPosition, cameraSpeed); //����� ������ ����
            //��� ���������� ������������� ���� (�������), ��� ������� ������ ��������� ���
            if (thisCamera.orthographicSize <= cameraMaxZoom)
            {
                overZoom = false;
            }
        }
        
        overZoomTimer -= Time.deltaTime;

        //����� �� ��������� ��������
        if (!overZoom)
        {
            thisCamera.orthographicSize += cameraSpeed; //���������� ����
            thisCamera.transform.position = Vector3.MoveTowards(transform.position, startPosition, cameraSpeed); //����� ������ � ����������� ���������
            //��� ���������� ������������ ����, ��������� ������� � ��������� ��� ������
            if (thisCamera.orthographicSize >= cameraMinZoom)
            {
                thisCamera.orthographicSize = originalCameraSize;
                overZoomTimer = zoomDelay;
                overZoom = true;
            }            
        }
    }

    public void ZoomReset()
    {
        thisCamera.orthographicSize = originalCameraSize;
        transform.position = startPosition;
        overZoom = true;
        overZoomTimer = 0;
    }
}
