using UnityEngine;

public class AppScreen_UICanvas_Menu_Entity_Parent : AppScreen_UICanvas_Parent
{
    private bool        shift = false;
    private float       shift_time = 0;
    private float       shift_time_max;
    protected Vector3   shift_pos_target;
    protected Vector3   shift_pos_source;
    protected Vector3   shift_pos_destination;
    protected Vector3   shift_pos_stepInSec;

    protected void Shift_Positions_Set(Vector3 _source, Vector3 _destination)
    {
        shift_pos_source = _source;
        shift_pos_destination = _destination;
        rectTransform.localPosition = shift_pos_source;
    }

    private void Shift_toTarget(Vector3 _targetPos, float _time)
    {
        shift_pos_target = _targetPos;
        shift_time = 0;
        shift_time_max = _time;
        shift_pos_stepInSec = (_targetPos - rectTransform.localPosition) / _time;
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

    private void Update()
    {
        if (shift)
        {
            rectTransform.localPosition += shift_pos_stepInSec * Time.deltaTime;
            shift_time += Time.deltaTime;
            
            if (shift_time >= shift_time_max)
            {
                rectTransform.localPosition = shift_pos_target;
                shift = false;
            }
        }
    }
}
