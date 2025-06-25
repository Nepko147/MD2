using UnityEngine;

public class AppScreen_Local_SceneMenu_UICanvas_Title : MonoBehaviour
{
    public static AppScreen_Local_SceneMenu_UICanvas_Title SingleOnScene { get; private set; }

    private SpriteRenderer spriteRenderer;
    
    public bool Visible 
    {
        get { return (spriteRenderer.enabled); }
        set { spriteRenderer.enabled = value; } 
    }
    
    private bool    shift = false;
    private float   shift_time = 0;
    private float   shift_time_max;
    private Vector3 shift_pos_target;
    private Vector3 shift_pos_source;
    private Vector3 shift_pos_destination;
    private Vector3 shift_pos_stepInSec;

    private void Shift_toTarget(Vector3 _targetPos, float _time)
    {
        shift_pos_target = _targetPos;
        shift_time = 0;
        shift_time_max = _time;
        shift_pos_stepInSec = (_targetPos - transform.position) / _time;
        shift = true;
    }

    public void Shift_toSource(float _time)
    {
        Shift_toTarget(shift_pos_source, _time);
    }

    public void Shift_toDestination(float _time)
    {
        Shift_toTarget(shift_pos_destination, _time);
    }

    private void Awake()
    {
        SingleOnScene = this;

        spriteRenderer = GetComponent<SpriteRenderer>();

        shift_pos_source = transform.position;
        shift_pos_destination = new Vector3(transform.position.x - 36f, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        if (shift)
        {
            transform.position += shift_pos_stepInSec * Time.deltaTime;
            shift_time += Time.deltaTime;

            if (shift_time >= shift_time_max)
            {
                transform.position = shift_pos_target;
                shift = false;
            }
        }
    }
}
