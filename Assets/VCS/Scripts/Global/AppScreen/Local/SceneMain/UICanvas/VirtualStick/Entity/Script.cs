using UnityEngine;
using Utils;

public class AppScreen_Local_SceneMain_UICanvas_VirtualStick_Entity : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_Local_SceneMain_UICanvas_VirtualStick_Entity SingleOnScene { get; private set; }

    private bool active = true;
    public bool Active 
    {
        get 
        {
            return (active);
        }
        set 
        {
            active = value;

            if (!value)
            {
                Visual_Outer.Visible = false;
                Visual_Inner.Visible = false;

                pressed = false;
            }
        } 
    }

    public bool pressed = false;

    private Vector3 screenPosition = new Vector3(0, 0, 1);

    public AppScreen_Local_SceneMain_UICanvas_VirtualStick_Visual_Outer Visual_Outer { private get; set; }

    public AppScreen_Local_SceneMain_UICanvas_VirtualStick_Visual_Inner Visual_Inner { private get; set; }
    [SerializeField] private float visual_inner_position_offset_max = 0.325f;
    public bool Visual_Inner_Magnitude_Active { get; private set; }
    [SerializeField] private float visual_inner_magnitude_active_edge = 0.15f;
    public float Visual_Inner_Direction { get; private set; }
    
    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        Visual_Inner_Direction = 0;
    }

    private void Update()
    {
        if (active)
        {
            if (ControlPers_InputHandler.SingleOnScene.Screen_Pressed)
            {
                var _screenPosition_vec2 = ControlPers_InputHandler.SingleOnScene.Screen_Position;
                screenPosition.x = _screenPosition_vec2.x;
                screenPosition.y = _screenPosition_vec2.y;
                var _worldPosition = AppScreen_General_UICanvas_Entity.SingleOnScene.Camera.ScreenToWorldPoint(screenPosition);

                if (!pressed)
                {
                    rectTransform.position = _worldPosition;

                    Visual_Outer.Visible = true;
                    Visual_Inner.Visible = true;

                    pressed = true;
                }
                else
                {
                    var _visual_inner_position_offset = Vector3.ClampMagnitude(_worldPosition - rectTransform.position, visual_inner_position_offset_max);
                    Visual_Inner.RectTransform_Position_Set = rectTransform.position + _visual_inner_position_offset;

                    if (_visual_inner_position_offset.magnitude > visual_inner_magnitude_active_edge)
                    {
                        Visual_Inner_Magnitude_Active = true;
                        Visual_Inner_Direction = AngleHandler.Vector_ToAngle(_visual_inner_position_offset);
                    }
                    else
                    {
                        Visual_Inner_Magnitude_Active = false;
                    }
                }
            }
            else
            {
                if (pressed)
                {
                    Visual_Outer.Visible = false;
                    Visual_Inner.Visible = false;
                    Visual_Inner_Magnitude_Active = false;

                    pressed = false;
                }
            }
        }
    }
}
