using UnityEngine;
using Utils;

public class AppScreen_GeneralCanvas_VirtualStick_Entity : MonoBehaviour
{
    public static AppScreen_GeneralCanvas_VirtualStick_Entity Singleton { get; private set; }

    public AppScreen_GeneralCanvas_VirtualStick_Visual_Outer Visual_Outer { private get; set; }
    public AppScreen_GeneralCanvas_VirtualStick_Visual_Inner Visual_Inner { private get; set; }

    private RectTransform rectTransrotm;

    private bool active = false;

    [SerializeField] private float inner_position_offset_max = 15f; //Для координат камеры: 0.42f

    public float Inner_Direction { get; private set; }

    private void Awake()
    {
        Singleton = this;

        rectTransrotm = GetComponent<RectTransform>();

        Inner_Direction = 0;
    }

    private void Update()
    {
        Inner_Direction = 0;

        if (InputHandler.Singleton.Screen_Pressed)
        {
            var _screen_position_vec2 = InputHandler.Singleton.Screen_Position;
            var _screen_position_vec3 = new Vector3(_screen_position_vec2.x, _screen_position_vec2.y, 100);
            var _world_position_vec3 = _screen_position_vec3; //Для координат камеры: AppScreen_GeneralCanvas_Entity.Singleton.canvas_camera.ScreenToWorldPoint(_screen_position_vec3);

            if (!active)
            {
                Visual_Outer.Visible = true;
                Visual_Inner.Visible = true;

                rectTransrotm.position = _world_position_vec3;

                active = true;
            }
            else
            {
                var _inner_position_offset = _world_position_vec3 - rectTransrotm.position;
                var _inner_position_offset_clamp = Vector3.ClampMagnitude(_inner_position_offset, inner_position_offset_max);
                Visual_Inner.RectTransform_Position_Set = rectTransrotm.position + _inner_position_offset_clamp;
                Inner_Direction = MathHandler.VectorToAngle(_inner_position_offset_clamp);
            }
        }
        else
        {
            if (active)
            {
                Visual_Outer.Visible = false;
                Visual_Inner.Visible = false;

                active = false;
            }
        }
    }
}
