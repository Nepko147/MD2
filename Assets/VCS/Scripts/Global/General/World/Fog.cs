using UnityEngine;

public class World_Fog : MonoBehaviour
{
    private Material material;

    private const string MATERIAL_COLOR_UNIFORM = "u_color";
    private Vector4 material_color_val = new Vector4();
    private float material_color_red_min = 0.1f;
    private float material_color_red_max = 0.5f;
    private float material_color_green_min = 0.1f;
    private float material_color_green_max = 0.2f;
    private const float MATERIAL_COLOR_BLUE = 0.4f;
    private const float MATERIAL_COLOR_ALPHA = 0.15f;
    private const float MATERIAL_COLOR_STEP = 0.00025f;
    private enum Material_Color_State
    {
        red_down,
        green_up,
        green_down,
        red_up
    }
    private Material_Color_State material_color_state = Material_Color_State.red_down;
    private const float MATERIAL_COLOR_DELAY_INIT = 1f;
    private float material_color_delay = MATERIAL_COLOR_DELAY_INIT;

    private const string MATERIAL_OFFSET_UNIFORM = "u_offset";
    private Vector4 material_offset_val = new Vector4();
    private const float MATERIAL_OFFSET_STEPSCALE = 1f;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;

        material_color_val.x = material_color_red_max;
        material_color_val.y = material_color_green_min;
        material_color_val.z = MATERIAL_COLOR_BLUE;
        material_color_val.w = MATERIAL_COLOR_ALPHA;
        material.SetVector(MATERIAL_COLOR_UNIFORM, material_color_val);
    }

    private void Update()
    {
        if (material_color_delay > 0)
        {
            material_color_delay -= Time.deltaTime;
        }
        else
        {
            switch (material_color_state)
            {
                case Material_Color_State.red_down:
                    material_color_val.x -= MATERIAL_COLOR_STEP;

                    if (material_color_val.x <= material_color_red_min)
                    {
                        material_color_state = Material_Color_State.green_up;
                    }
                    break;
                case Material_Color_State.green_up:
                    material_color_val.y += MATERIAL_COLOR_STEP;

                    if (material_color_val.y >= material_color_green_max)
                    {
                        material_color_delay = MATERIAL_COLOR_DELAY_INIT;
                        material_color_state = Material_Color_State.green_down;
                    }
                    break;
                case Material_Color_State.green_down:
                    material_color_val.y -= MATERIAL_COLOR_STEP;

                    if (material_color_val.y <= material_color_green_min)
                    {
                        material_color_state = Material_Color_State.red_up;
                    }
                    break;
                case Material_Color_State.red_up:
                    material_color_val.x += MATERIAL_COLOR_STEP;

                    if (material_color_val.x >= material_color_red_max)
                    {
                        material_color_delay = MATERIAL_COLOR_DELAY_INIT;
                        material_color_state = Material_Color_State.red_down;
                    }
                    break;
            }

            material.SetVector(MATERIAL_COLOR_UNIFORM, material_color_val);
        }

        material_offset_val.x += Time.deltaTime * MATERIAL_OFFSET_STEPSCALE;
        material.SetVector(MATERIAL_OFFSET_UNIFORM, material_offset_val);
    }
}
