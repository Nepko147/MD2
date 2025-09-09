using UnityEngine;

public class AppScreen_General_Camera_Entity : MonoBehaviour    
{
    #region General

    public static AppScreen_General_Camera_Entity SingleOnScene { get; private set; }

    public bool Active { get; set; }

    public Vector3 Position_Init { get; private set; }

    #endregion

    #region Move

    private enum Move_State
    {
        idle,
        follow,
        destination,
        overZoom_In,
        overZoom_Out,
        size
    }
    private Move_State move_state = Move_State.idle;

    private float move_speed = 0;

    private GameObject move_follow_target;
    private const float MOVE_FOLLOW_SPEED = 10f;
    public float Move_Follow_YMax { get; set; }

    public void Move_Follow(GameObject _target)
    {
        move_follow_target = _target;
        move_speed = MOVE_FOLLOW_SPEED;
        move_state = Move_State.follow;
    }

    private Vector3 move_destination_position;
    private const float MOVE_DESTINATION_SPEED = 3f;

    public void Move_Destination(Vector3 _position)
    {
        move_destination_position = _position;
        move_speed = MOVE_DESTINATION_SPEED;
        move_state = Move_State.destination;
    }

    #endregion

    #region Slope

    public bool Slope { get; set; }
    private bool slope_stage = true;
    [SerializeField] private float slope_speed = 0.001f;
     private Vector3 slope_rotation;
    [SerializeField] private float slope_rotation_max_ofs = 1f;
    private Vector3 slope_rotation_max_left;
    private Vector3 slope_rotation_max_right;
    private float slope_delay;
    [SerializeField] private float slope_delay_init = 5f;

    #endregion

    #region ZoomToTarget

    public void ZoomToTarget(Vector3 _targetPosition)
    {
        zoomToTarget_position = _targetPosition + Vector3.forward * zoomToTarget_maximumZoom;
        var _camera_position_x = Vector3.right * transform.position.x;          //Запоминаем положение камеры до отдаления по оси X
        var _camera_position_y = Vector3.up * transform.position.y;             //Запоминаем положение камеры до отдаления по оси Y
        var _camera_position_z = Vector3.forward * zoomToTarget_position_init_z;//Возвращаем положение камеры по оси Z на исходное место
        zoomToTarget_position_init = _camera_position_x + _camera_position_y + _camera_position_z; //Координаты камеры, куда она должна вернуться после "ZoomToTarget_Disable()"
        zoomToTarget_state = ZoomToTarget_state.zoomToTarget_In;
    }

    public void ZoomToTarget_Disable()
    {
        zoomToTarget_state = ZoomToTarget_state.zoomToTarget_Out;
    }

    private enum ZoomToTarget_state
    {
        idle,
        zoomToTarget_In,
        zoomToTarget_Out,
        size
    }

    private ZoomToTarget_state zoomToTarget_state = ZoomToTarget_state.idle;

    private Vector3 zoomToTarget_position;
    private Vector3 zoomToTarget_position_init;
    private float   zoomToTarget_position_init_z;

    private float zoomToTarget_step_in = 30.0f;
    private float zoomToTarget_step_out = 20.0f;

    private float zoomToTarget_maximumZoom = -2.0f;

    #endregion

    private void Awake()
    {
        SingleOnScene = this;

        Active = true;

        Position_Init = transform.position;

        Move_Follow_YMax = Position_Init.y;

        Slope = false;

        slope_rotation_max_left = new Vector3(0, 0, 360f - slope_rotation_max_ofs);
        slope_rotation_max_right = new Vector3(0, 0, slope_rotation_max_ofs);

        zoomToTarget_position_init_z = transform.position.z;
    }

    private void LateUpdate()
    {
        if (Active)
        {
            switch (move_state)
            {
                case Move_State.follow:
                    var _pos = move_follow_target.transform.position - transform.position;
                    _pos.z = 0;
                    _pos = transform.position + _pos * move_speed * Time.deltaTime;

                    if (_pos.y > Move_Follow_YMax)
                    {
                        _pos.y = Move_Follow_YMax;
                    }
                    
                    transform.position = _pos;
                break;

                case Move_State.destination:
                    var _spd = move_speed * Time.deltaTime;

                    transform.position = Vector3.MoveTowards(transform.position, move_destination_position, _spd);

                    var _dif = move_destination_position - transform.position;

                    if (_dif.magnitude <= _spd)
                    {
                        move_state = Move_State.idle;
                    }
                break;
            }

            switch (zoomToTarget_state)
            {
                case ZoomToTarget_state.zoomToTarget_In:
                    var _zoomToTarget_step = zoomToTarget_step_in * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, zoomToTarget_position, _zoomToTarget_step);
                    
                    if(transform.position == zoomToTarget_position)
                    {
                        zoomToTarget_state = ZoomToTarget_state.idle;
                    }
                break;
                case ZoomToTarget_state.zoomToTarget_Out:
                    _zoomToTarget_step = zoomToTarget_step_out * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, zoomToTarget_position_init, _zoomToTarget_step);

                    if (transform.position == zoomToTarget_position_init)
                    {
                        zoomToTarget_state = ZoomToTarget_state.idle;
                    }
                break;
            }


            if (slope_delay >= 0)
            {
                slope_delay -= Time.deltaTime;
            }
            else
            {
                if (slope_stage)
                {
                    slope_rotation.z = transform.eulerAngles.z + slope_speed;
                    transform.rotation = Quaternion.Euler(slope_rotation);
                    
                    if (transform.eulerAngles.z >= slope_rotation_max_ofs 
                    && transform.eulerAngles.z + slope_rotation_max_ofs < 360f)
                    {
                        transform.eulerAngles = slope_rotation_max_right;
                        slope_delay = slope_delay_init;
                        slope_stage = false;
                    }                  
                }
                else
                {
                    slope_rotation.z = transform.eulerAngles.z - slope_speed;
                    transform.rotation = Quaternion.Euler(slope_rotation);

                    if (transform.eulerAngles.z <= 360f - slope_rotation_max_ofs 
                    && transform.eulerAngles.z > slope_rotation_max_ofs)
                    {
                        transform.eulerAngles = slope_rotation_max_left;
                        slope_delay = slope_delay_init;
                        slope_stage = true;
                    }                 
                }
            }
        }                
    }
}
