using UnityEngine;

public class AppScreen_General_Camera_World_Slope : MonoBehaviour    
{
    public static AppScreen_General_Camera_World_Slope SingleOnScene { get; private set; }

    public bool Active { get; set; }
    
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

        Active = false;        
        camera_leftMaxRotation = new Vector3(0, 0, 360.0f - camera_slope_maxAngle);
        camera_rightMaxRotation = new Vector3(0, 0, camera_slope_maxAngle);
    }
    private void FixedUpdate()
    {        
        if (Active)
        {
            if (camera_slope_delay >= 0)
            {
                camera_slope_delay -= Time.deltaTime;
            }
            else
            {
                //Наклон влево
                if (camera_slope_toleft)
                {                        
                    if (transform.eulerAngles.z <= 360.0f - camera_slope_maxAngle && transform.eulerAngles.z > camera_slope_maxAngle) // !!!
                    {
                        transform.eulerAngles = camera_leftMaxRotation; //Присвоить максимальный угол наклона влево
                        camera_slope_toleft = false;
                        camera_slope_delay = camera_slope_delay_init;
                    }
                    else
                    {
                        camera_slope.z = transform.eulerAngles.z - camera_slope_speed;
                        transform.rotation = Quaternion.Euler(camera_slope);
                    }                        
                }

                //Наклон вправо
                if (!camera_slope_toleft)
                {            
                    if (transform.eulerAngles.z >= camera_slope_maxAngle && transform.eulerAngles.z + camera_slope_maxAngle < 360.0f)
                    {
                        transform.eulerAngles = camera_rightMaxRotation; //Присвоить максимальный угол наклона вправо
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
    }
}
