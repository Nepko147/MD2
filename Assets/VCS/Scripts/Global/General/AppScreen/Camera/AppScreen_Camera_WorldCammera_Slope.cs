using UnityEngine;

public class AppScreen_Camera_WorldCammera_Slope : MonoBehaviour    
{
    public static AppScreen_Camera_WorldCammera_Slope SingleOnScene { get; private set; }
    
    private Vector3 camera_originRotation;
    private Vector3 camera_leftMaxRotation;
    private Vector3 camera_rightMaxRotation;

    private Vector3                 camera_slope;
    private bool                    camera_slope_toleft;
    [SerializeField] private float  camera_slope_speed;
    [SerializeField] private float  camera_slope_maxAngle; 
    private float                   camera_slope_delay;
    [SerializeField] private float  camera_slope_delay_init;

    private void Awake()
    {
        SingleOnScene = this;

        camera_originRotation = transform.eulerAngles;
        camera_leftMaxRotation = new Vector3(0, 0, 360.0f - camera_slope_maxAngle);
        camera_rightMaxRotation = new Vector3(0, 0, camera_slope_maxAngle);
    }
    private void FixedUpdate()
    {        
        if (camera_slope_delay >= 0)
        {
            camera_slope_delay -= Time.deltaTime;
        }
        else
        {
            //������ �����
            if (camera_slope_toleft)
            {                        
                if (transform.eulerAngles.z <= 360.0f - camera_slope_maxAngle && transform.eulerAngles.z > camera_slope_maxAngle) // !!!
                {
                    transform.eulerAngles = camera_leftMaxRotation; //��������� ������������ ���� ������� �����
                    camera_slope_toleft = false;
                    camera_slope_delay = camera_slope_delay_init;
                }
                else
                {
                    camera_slope.z = transform.eulerAngles.z - camera_slope_speed;
                    transform.rotation = Quaternion.Euler(camera_slope);
                }                        
            }

            //������ ������
            if (!camera_slope_toleft)
            {            
                if (transform.eulerAngles.z >= camera_slope_maxAngle && transform.eulerAngles.z + camera_slope_maxAngle < 360.0f)
                {
                    transform.eulerAngles = camera_rightMaxRotation; //��������� ������������ ���� ������� ������
                    camera_slope_toleft = true;
                    camera_slope_delay = camera_slope_delay_init;
                }
                else
                {
                    camera_slope.z = transform.eulerAngles.z + camera_slope_speed;
                    transform.rotation = Quaternion.Euler(camera_slope);
                }                    
            }
        }        
    }

    public void RotationReset()
    {
        transform.eulerAngles = camera_originRotation;
        camera_slope_toleft = true;
        camera_slope_delay = camera_slope_delay_init;
    }
}
