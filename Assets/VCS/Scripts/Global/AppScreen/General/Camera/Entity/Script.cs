using UnityEngine;
using System.Collections;

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
        size
    }

    private Move_State move_state_current = Move_State.idle;

    private float move_speed = 0;

    private GameObject move_follow_target;
    private const float MOVE_FOLLOW_SPEED = 10f;
    public float Move_Follow_YMax { get; set; }

    public void Move_Follow(GameObject _target)
    {
        move_follow_target = _target;
        move_speed = MOVE_FOLLOW_SPEED;
        move_state_current = Move_State.follow;
    }

    private Vector3 move_destination_position;

    public void Move_Destination(Vector3 _position, float _speed)
    {
        move_destination_position = _position;
        move_speed = _speed;
        move_state_current = Move_State.destination;
    }

    #endregion

    #region Slope

    public bool Slope_Active { get; set; }
    private bool slope_stage = true;
    private const float SLOPE_SPEED = 0.1f;
     private Vector3 slope_rotation;
    private const float SLOPE_ROTATION_MAX_OFS = 1f;
    private Vector3 slope_rotation_max_left;
    private Vector3 slope_rotation_max_right;
    private float slope_delay;
    private const float SLOPE_DELAY_INIT = 5f;

    #endregion

    #region ZoomToTarget

    private bool zoomToTarget_active = false;

    private enum ZoomToTarget_state
    {
        idle,
        on,
        off,
        size
    }

    private ZoomToTarget_state zoomToTarget_state_current = ZoomToTarget_state.idle;

    private Vector3 zoomToTarget_position_on;
    private Vector3 zoomToTarget_position_off;
    private float ZOOMTOTARGET_STEP_ON = 30f;
    private float ZOOMTOTARGET_STEP_OFF = 20f;

    private IEnumerator zoomToTarget_autoOff_routine = null;
    
    public void ZoomToTarget_On(Vector3 _pos_on, float _pos_on_z_ofs)
    {
        zoomToTarget_active = true;
        zoomToTarget_position_on = _pos_on + Vector3.forward * _pos_on_z_ofs;
        zoomToTarget_state_current = ZoomToTarget_state.on;

        if (zoomToTarget_autoOff_routine != null)
        {
            StopCoroutine(zoomToTarget_autoOff_routine);
        }
    }

    public void ZoomToTarget_On_AutoOff(Vector3 _pos_on, float _pos_on_z_ofs, Vector3 _pos_off)
    {
        ZoomToTarget_On(_pos_on, _pos_on_z_ofs);
        
        IEnumerator _Coroutine()
        {
            while (zoomToTarget_state_current != ZoomToTarget_state.idle)
            {
                yield return (null);
            }

            ZoomToTarget_Off(_pos_off);
        }
        
        if (zoomToTarget_autoOff_routine != null)
        {
            StopCoroutine(zoomToTarget_autoOff_routine);
        }

        zoomToTarget_autoOff_routine = _Coroutine();
        StartCoroutine(zoomToTarget_autoOff_routine);
    }

    public void ZoomToTarget_Off(Vector3 _pos_off)
    {
        _pos_off.z = Position_Init.z;
        zoomToTarget_position_off = _pos_off;
        zoomToTarget_state_current = ZoomToTarget_state.off;
    }

    public void ZoomToTarget_Off_Instant()
    {
        zoomToTarget_active = false;
        zoomToTarget_state_current = ZoomToTarget_state.idle;
    }

    #endregion

    private void Awake()
    {
        SingleOnScene = this;

        Active = true;

        Position_Init = transform.position;

        Move_Follow_YMax = Position_Init.y;

        Slope_Active = false;
        slope_rotation_max_left = new Vector3(0, 0, 360f - SLOPE_ROTATION_MAX_OFS);
        slope_rotation_max_right = new Vector3(0, 0, SLOPE_ROTATION_MAX_OFS);
    }

    private void LateUpdate()
    {
        if (Active)
        {
            if (!zoomToTarget_active)
            {
                #region Move

                switch (move_state_current)
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
                            move_state_current = Move_State.idle;
                        }
                    break;
                }

                    #endregion
            }
            else
            {
                #region ZoomToTarget

                switch (zoomToTarget_state_current)
                {
                    case ZoomToTarget_state.on:
                        var _step = ZOOMTOTARGET_STEP_ON * Time.deltaTime;
                        transform.position = Vector3.MoveTowards(transform.position, zoomToTarget_position_on, _step);
                    
                        if ((zoomToTarget_position_on - transform.position).magnitude <= _step)
                        {
                            zoomToTarget_state_current = ZoomToTarget_state.idle;
                            transform.position = zoomToTarget_position_on;
                        }
                    break;

                    case ZoomToTarget_state.off:
                        _step = ZOOMTOTARGET_STEP_OFF * Time.deltaTime;
                        transform.position = Vector3.MoveTowards(transform.position, zoomToTarget_position_off, _step);

                        if ((zoomToTarget_position_off - transform.position).magnitude <= _step)
                        {
                            zoomToTarget_active = false;
                            zoomToTarget_state_current = ZoomToTarget_state.idle;
                            transform.position = zoomToTarget_position_off;
                        }
                    break;
                }

                #endregion
            }

            #region Slope

            if (Slope_Active)
            {
                if (slope_delay >= 0)
                {
                    slope_delay -= Time.deltaTime;
                }
                else
                {
                    if (slope_stage)
                    {
                        slope_rotation.z = transform.eulerAngles.z + SLOPE_SPEED * Time.deltaTime;
                        transform.rotation = Quaternion.Euler(slope_rotation);
                    
                        if (transform.eulerAngles.z >= SLOPE_ROTATION_MAX_OFS 
                        && transform.eulerAngles.z + SLOPE_ROTATION_MAX_OFS < 360f)
                        {
                            transform.eulerAngles = slope_rotation_max_right;
                            slope_delay = SLOPE_DELAY_INIT;
                            slope_stage = false;
                        }                  
                    }
                    else
                    {
                        slope_rotation.z = transform.eulerAngles.z - SLOPE_SPEED * Time.deltaTime;
                        transform.rotation = Quaternion.Euler(slope_rotation);

                        if (transform.eulerAngles.z <= 360f - SLOPE_ROTATION_MAX_OFS 
                        && transform.eulerAngles.z > SLOPE_ROTATION_MAX_OFS)
                        {
                            transform.eulerAngles = slope_rotation_max_left;
                            slope_delay = SLOPE_DELAY_INIT;
                            slope_stage = true;
                        }                 
                    }
                }
            }

            #endregion
        }
    }
}
