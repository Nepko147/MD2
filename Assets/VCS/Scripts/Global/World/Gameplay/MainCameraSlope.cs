using UnityEngine;

public class MainCameraSlope : MonoBehaviour    
{
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float cameraMaxAngle; 
    [SerializeField] private float slopeDelay;
    private Vector3 originRotation;
    private Vector3 leftMaxRotation;
    private Vector3 rightMaxRotation;
    private Vector3 rotate;
    private float slopeTimer;
    private bool toLeft;

    public static MainCameraSlope Instance { get; private set; }

    private void Awake()
    {
        Instance = this;        
        originRotation = transform.eulerAngles;
        leftMaxRotation = new Vector3(0, 0, 360.0f - cameraMaxAngle);
        rightMaxRotation = new Vector3(0, 0, cameraMaxAngle);
    }
    private void FixedUpdate()
    {
        if (!Globalist.Instance.canPlay())
        {
            return;
        }

        if (slopeTimer >= 0)
        {
            slopeTimer -= Time.deltaTime;
            return;
        }

        //Наклон влево
        if (toLeft)
        {                        
            if (transform.eulerAngles.z <= 360.0f - cameraMaxAngle && transform.eulerAngles.z > cameraMaxAngle) // !!!
            {
                transform.eulerAngles = leftMaxRotation; //Присвоить максимальный угол наклона влево
                toLeft = false;
                slopeTimer = slopeDelay;
                return;
            }
            rotate.z = transform.eulerAngles.z - cameraSpeed;
            transform.rotation = Quaternion.Euler(rotate);
        }
        //Наклон вправо
        if (!toLeft)
        {            
            if (transform.eulerAngles.z >= cameraMaxAngle && transform.eulerAngles.z + cameraMaxAngle < 360.0f)
            {
                transform.eulerAngles = rightMaxRotation; //Присвоить максимальный угол наклона вправо
                toLeft = true;
                slopeTimer = slopeDelay;
                return;
            }
            rotate.z = transform.eulerAngles.z + cameraSpeed;
            transform.rotation = Quaternion.Euler(rotate);
        }
    }

    public void RotationReset()
    {
        transform.eulerAngles = originRotation;
        toLeft = true;
        slopeTimer = slopeDelay;
    }
}
