using UnityEngine;

public class World_Fog : MonoBehaviour
{
    public static World_Fog Singleton;

    private Material material;

    private const string MATERIAL_COLOR_UNIFORM = "u_color";
    private Vector4 material_color_val = new Vector4();
    public Vector4 Material_Color_Val
    {
        get 
        { 
            return material_color_val; 
        }
        set 
        { 
            material_color_val = value;
            material.SetVector(MATERIAL_COLOR_UNIFORM, material_color_val);
        }
    }
    private float material_color_red_min = 0.1f;
    private float material_color_red_max = 0.5f;
    private float material_color_green_min = 0.1f;
    private float material_color_green_max = 0.2f;
    private const float MATERIAL_COLOR_BLUE = 0.4f;
    private const float MATERIAL_COLOR_ALPHA = 0.15f;
    private const float MATERIAL_COLOR_STEP = 0.00025f;
    public enum Material_Color_State
    {
        red_down,
        green_up,
        green_down,
        red_up
    }
    public Material_Color_State Material_Color_CurrentState { get; set; }
    private const float MATERIAL_COLOR_DELAY_INIT = 1f;
    private float material_color_delay = MATERIAL_COLOR_DELAY_INIT;

    private const string MATERIAL_OFFSET_UNIFORM = "u_offset";
    private Vector4 material_offset_val = new Vector4();
    private const float MATERIAL_OFFSET_STEPSCALE_MIN = 0.1f;
    private const float MATERIAL_OFFSET_STEPSCALE_MAX = 1f;
    private float material_offset_stepScale = MATERIAL_OFFSET_STEPSCALE_MIN;
    private float material_offset_stepScale_step = 0;
    private bool material_offset_stepScale_step_change = false;
    private float material_offset_stepScale_step_change_duration = 0f;
    public float Material_Offset_Step(float _old_offset) 
    {
        var _new_offset = _old_offset + Time.deltaTime * material_offset_stepScale;
        material_offset_val.x = _new_offset;
        material.SetVector(MATERIAL_OFFSET_UNIFORM, material_offset_val);

        return (_new_offset);
    }
    /// <summary>
    /// <para> _value - нормализованная величина скейла шага отступа. </para>
    /// <para> _duration - кол-во секунд, за которое величина скейла шага отступа приёдт к _value. </para>
    /// </summary>
    public void Material_Offset_StepScale_Change(float _value, float _duration)
    {
        material_offset_stepScale_step_change = true;
        material_offset_stepScale_step_change_duration = _duration;
        _value = Mathf.Clamp(_value, 0, 1);
        var _stepScale_newValue = MATERIAL_OFFSET_STEPSCALE_MIN + (MATERIAL_OFFSET_STEPSCALE_MAX - MATERIAL_OFFSET_STEPSCALE_MIN) * _value;
        material_offset_stepScale_step = (_stepScale_newValue - material_offset_stepScale) / _duration;
    }

    private void Awake()
    {
        Singleton = this;

        material = GetComponent<SpriteRenderer>().material;

        material_color_val.x = material_color_red_max;
        material_color_val.y = material_color_green_min;
        material_color_val.z = MATERIAL_COLOR_BLUE;
        material_color_val.w = MATERIAL_COLOR_ALPHA;
        material.SetVector(MATERIAL_COLOR_UNIFORM, material_color_val);
        Material_Color_CurrentState = Material_Color_State.red_down;
    }

    private void Update()
    {
        if (material_color_delay > 0)
        {
            material_color_delay -= Time.deltaTime;
        }
        else
        {
            switch (Material_Color_CurrentState)
            {
                case Material_Color_State.red_down:
                    material_color_val.x -= MATERIAL_COLOR_STEP;

                    if (material_color_val.x <= material_color_red_min)
                    {
                        Material_Color_CurrentState = Material_Color_State.green_up;
                    }
                break;

                case Material_Color_State.green_up:
                    material_color_val.y += MATERIAL_COLOR_STEP;

                    if (material_color_val.y >= material_color_green_max)
                    {
                        material_color_delay = MATERIAL_COLOR_DELAY_INIT;
                        Material_Color_CurrentState = Material_Color_State.green_down;
                    }
                break;
                
                case Material_Color_State.green_down:
                    material_color_val.y -= MATERIAL_COLOR_STEP;

                    if (material_color_val.y <= material_color_green_min)
                    {
                        Material_Color_CurrentState = Material_Color_State.red_up;
                    }
                break;

                case Material_Color_State.red_up:
                    material_color_val.x += MATERIAL_COLOR_STEP;

                    if (material_color_val.x >= material_color_red_max)
                    {
                        material_color_delay = MATERIAL_COLOR_DELAY_INIT;
                        Material_Color_CurrentState = Material_Color_State.red_down;
                    }
                break;
            }

            material.SetVector(MATERIAL_COLOR_UNIFORM, material_color_val);
        }

        if (material_offset_stepScale_step_change)
        {
            material_offset_stepScale_step_change_duration -= Time.deltaTime;
            material_offset_stepScale += material_offset_stepScale_step * Time.deltaTime;
            material_offset_stepScale = Mathf.Clamp(material_offset_stepScale, MATERIAL_OFFSET_STEPSCALE_MIN, MATERIAL_OFFSET_STEPSCALE_MAX);

            if (material_offset_stepScale_step_change_duration <= 0)
            {
                material_offset_stepScale_step_change = false;
            }
        }
    }
}
