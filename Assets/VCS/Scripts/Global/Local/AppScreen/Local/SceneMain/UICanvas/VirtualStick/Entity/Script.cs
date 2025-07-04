using UnityEngine;
using Utils;

public class AppScreen_Local_SceneMain_UICanvas_VirtualStick_Entity : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_Local_SceneMain_UICanvas_VirtualStick_Entity SingleOnScene { get; private set; }

    public AppScreen_Local_SceneMain_UICanvas_VirtualStick_Visual_Outer Visual_Outer { private get; set; }
    public AppScreen_Local_SceneMain_UICanvas_VirtualStick_Visual_Inner Visual_Inner { private get; set; }

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

                Inner_Direction = 0;

                pressed = false;
            }
        } 
    }

    private bool pressed = false;

    private Vector3 screenPosition = new Vector3(0, 0, 1);

    [SerializeField] private float inner_position_magnitude_edge = 0.15f;
    [SerializeField] private float inner_position_offset_max = 0.325f;

    public float Inner_Direction { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        Inner_Direction = 0;
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
                    Visual_Outer.Visible = true;
                    Visual_Inner.Visible = true;

                    rectTransform.position = _worldPosition;

                    pressed = true;
                }
                else
                {
                    var _inner_position_offset = _worldPosition - rectTransform.position;
                    var _inner_position_offset_clamp = Vector3.ClampMagnitude(_inner_position_offset, inner_position_offset_max);
                    Visual_Inner.RectTransform_Position_Set = rectTransform.position + _inner_position_offset_clamp;

                    if (_inner_position_offset_clamp.magnitude > inner_position_magnitude_edge)
                    {
                        Inner_Direction = MathHandler.VectorToAngle(_inner_position_offset_clamp);
                    }
                    else
                    {
                        Inner_Direction = 0;
                    }
                }
            }
            else
            {
                if (pressed)
                {
                    Visual_Outer.Visible = false;
                    Visual_Inner.Visible = false;

                    Inner_Direction = 0;

                    pressed = false;
                }
            }
        }
    }
}
