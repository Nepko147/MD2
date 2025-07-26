using UnityEngine;

public class AppScreen_General_Camera_Entity : MonoBehaviour    
{
    #region General

    public static AppScreen_General_Camera_Entity SingleOnScene { get; private set; }

    public bool Active { get; set; }

    public Vector3 Position_Init { get; private set; }

    #endregion

    #region Move

    private float move_speed = 0.1f;
    public float Move_YMax { get; set; }
    private bool Move(Vector2 _position)
    {
        var _step = _position - new Vector2(transform.position.x, transform.position.y);

        if (transform.position.y + _step.y > Move_YMax)
        {
            _step.y = 0;
        }

        transform.position += new Vector3(_step.x, _step.y, 0) * move_speed;

        if (_step.magnitude > move_speed)
        {
            return (false);
        }
        else
        {
            transform.position = new Vector3(_position.x, _position.y, transform.position.z);
            return (true);
        }
    }

    private enum Move_State
    {
        idle,
        follow,
        destination
    }
    private Move_State move_state = Move_State.idle;

    private GameObject move_follow_target;
    public void Move_Follow(GameObject _target)
    {
        move_follow_target = _target;
        move_state = Move_State.follow;
    }

    private Vector3 move_destination_position;
    public void Move_Destination(Vector3 _position)
    {
        move_destination_position = _position;
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

    private void Awake()
    {
        SingleOnScene = this;

        Active = true;

        Position_Init = transform.position;

        Move_YMax = transform.position.y;

        Slope = false;

        slope_rotation_max_left = new Vector3(0, 0, 360f - slope_rotation_max_ofs);
        slope_rotation_max_right = new Vector3(0, 0, slope_rotation_max_ofs);
    }

    private void Update()
    {        
        if (Active)
        {
            switch (move_state)
            {
                case Move_State.follow:
                    Move(move_follow_target.transform.position);
                break;

                case Move_State.destination:
                    if (Move(move_destination_position))
                    {
                        move_state = Move_State.idle;
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
