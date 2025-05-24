using UnityEngine;

public class AppScreen_Camera_World_Zoom : MonoBehaviour    
{
    public static AppScreen_Camera_World_Zoom SingleOnScene { get; private set; }

    public bool Active { get; set; }

    new private Camera              camera;
    private Vector3                 camera_position_origin;
    private Vector3                 camera_position_correction;
    [SerializeField] private float  camera_position_correction_size;
    [SerializeField] private float  camera_position_correction_speed;

    private float                   camera_originalFieldOfView;

    [SerializeField] private float  camera_zoom_speed;
    [SerializeField] private float  camera_zoom_max; //��� ������ ��������, ��� ������� ���
    [SerializeField] private float  camera_zoom_min; //��� ������ ��������, ��� ������ ���
    [SerializeField] private float  camera_zoom_delay;    
    
    private bool    camera_overZoom;
    private float   camera_overZoomt_timer; //������ �������������� ��������

    private void Awake()
    {
        SingleOnScene = this;

        camera = GetComponent<Camera>();
        camera_originalFieldOfView = camera.fieldOfView;
        camera_position_origin = transform.position;
        camera_position_correction = new Vector3(camera_position_origin.x, camera_position_origin.y - camera_position_correction_size, camera_position_origin.z);
        ZoomReset();
    }
    private void FixedUpdate()
    {       
        if (Active)
        {            
            //���� � ��������� ��������
            if (camera.fieldOfView > camera_zoom_max && camera_overZoom && camera_overZoomt_timer <= 0)
            {
                camera.fieldOfView -= camera_zoom_speed; //���������� ����
                camera.transform.position = Vector3.MoveTowards(transform.position, camera_position_correction, camera_position_correction_speed); //����� ������ ����
            
                //��� ���������� ������������� ���� (�������), ��� ������� ������ ��������� ���
                if (camera.fieldOfView <= camera_zoom_max)
                {
                    camera_overZoom = false;
                }
            }

            camera_overZoomt_timer -= Time.deltaTime;

            //����� �� ��������� ��������
            if (!camera_overZoom)
            {
                camera.fieldOfView += camera_zoom_speed; //���������� ����
                camera.transform.position = Vector3.MoveTowards(transform.position, camera_position_origin, camera_position_correction_speed); //����� ������ � ����������� ���������
            
                //��� ���������� ������������ ����, ��������� ������� � ��������� ��� ������
                if (camera.fieldOfView >= camera_zoom_min)
                {
                    camera.fieldOfView = camera_originalFieldOfView;
                    camera_overZoomt_timer = camera_zoom_delay;
                    camera_overZoom = true;
                }            
            }
        }
    } 

    public void ZoomReset()
    {
        camera.fieldOfView = camera_originalFieldOfView;
        transform.position = camera_position_origin;
        camera_overZoom = true;
        camera_overZoomt_timer = 0;
    }
}
