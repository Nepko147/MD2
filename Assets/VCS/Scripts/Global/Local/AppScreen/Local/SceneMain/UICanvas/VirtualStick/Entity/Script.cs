using UnityEngine;
using Utils;

public class AppScreen_Local_SceneMain_UICanvas_VirtualStick_Entity : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_Local_SceneMain_UICanvas_VirtualStick_Entity SingleOnScene { get; private set; }

    public AppScreen_Local_SceneMain_UICanvas_VirtualStick_Visual_Outer Visual_Outer { private get; set; }
    public AppScreen_Local_SceneMain_UICanvas_VirtualStick_Visual_Inner Visual_Inner { private get; set; }

    private bool active = false;

    [SerializeField] private float inner_position_deadzone = 0.001f;
    [SerializeField] private float inner_position_offset_max = 15f;

    public float Inner_Direction { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        Inner_Direction = 0;
    }

    private void Update()
    {
        Inner_Direction = 0;

        if (ControlPers_InputHandler.SingleOnScene.Screen_Pressed)
        {
            var _screen_position_vec2 = ControlPers_InputHandler.SingleOnScene.Screen_Position;
            var _screen_position_vec3 = new Vector3(_screen_position_vec2.x, _screen_position_vec2.y, 1);
            var _world_position_vec3 = AppScreen_General_UICanvas_Entity.SingleOnScene.Camera.ScreenToWorldPoint(_screen_position_vec3);

            if (!active)
            {
                Visual_Outer.Visible = true;
                Visual_Inner.Visible = true;

                rectTransform.position = _world_position_vec3;

                active = true;
            }
            else
            {
                var _inner_position_offset = _world_position_vec3 - rectTransform.position;
                var _inner_position_offset_clamp = Vector3.ClampMagnitude(_inner_position_offset, inner_position_offset_max);
                Visual_Inner.RectTransform_Position_Set = rectTransform.position + _inner_position_offset_clamp;
                
                if (_inner_position_offset_clamp.magnitude > inner_position_deadzone)
                {
                    Inner_Direction = MathHandler.VectorToAngle(_inner_position_offset_clamp);
                }
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
