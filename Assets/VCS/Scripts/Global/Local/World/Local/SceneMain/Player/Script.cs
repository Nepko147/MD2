using UnityEngine;
using Utils;

public class World_Local_SceneMain_Player : MonoBehaviour
{
    #region General
    
    public static World_Local_SceneMain_Player SingleOnScene { get; private set; }
    
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

            if (value)
            {
                animator.speed = 1;
            }
            else
            {
                animator.speed = 0;
            }
        }
    }

    public enum State
    {
        road,
        road_toDrift_alignment,
        road_toDrift_braking,
        road_toDrift_moveDown,
        drift
    }
    private State state_current = State.road;
    public State State_Current
    {
        get
        {
            return (state_current);
        }
        set
        {
            state_current = value;

            switch (value)
            {
                case State.road_toDrift_alignment:
                    spriteRenderer_material_color.a = 1f;
                    spriteRenderer.material.SetColor(Constants.MATERIAL_2D_BUMP_U_COLOR, spriteRenderer_material_color);
                break;

                case State.road_toDrift_moveDown:
                    VisualState_Set(VisualState.down);
                break;

                case State.drift:
                    moving_drift_speed_current = MOVING_ROAD_TODRIFT_MOVEDOWN_SPEED_MAX;
                break;
            }
        }
    }

    private SpriteRenderer spriteRenderer;
    private Color spriteRenderer_material_color = Color.white;
    
    private Animator animator;

    [SerializeField] private GameObject lights_front_1;
    [SerializeField] private GameObject lights_front_2;
    [SerializeField] private GameObject lights_rear_1;
    [SerializeField] private GameObject lights_rear_2;

    private const float LIGHTS_LOCALPOSITION_Z = 0.1f;
    private const float LIGHTS_ROTATION_Y = 90f;

    private struct Lights
    {
        public Lights(float _x, float _y, float _angle)
        {
            localPosition = new Vector3(_x, _y, LIGHTS_LOCALPOSITION_Z);

            var _rotation_euler = new Vector3(_angle, LIGHTS_ROTATION_Y, 0);
            rotation = Quaternion.Euler(_rotation_euler);
        }

        public readonly Vector3 localPosition;
        public readonly Quaternion rotation;
    }

    public int Up_Count { get; set; }
    
    public void Up_Take()
    {
        ++Up_Count;
    }

    public void Up_Lose()
    {
        --Up_Count;

        --AppScreen_Local_SceneMain_UICanvas_Entity.SingleOnScene.Ups_Visual;

        Invul = true;
        invul_timer = invul_timer_init;

        AppScreen_General_Camera_World_Entity_Shake.SingleOnScene.Shake();

        if (Up_Count <= 0)
        {
            spriteRenderer_material_color = Color.red;
            animator.speed = 0;
            crashed = true;
        }
    }

    public bool Invul { get; private set; }
    private float invul_timer;
    [SerializeField] private float invul_timer_init = 1.2f;
    private bool invul_alpha_state = false;
    [SerializeField] private float invul_alpha_step = 12; //„ем больше значение, тем быстрее моргает

    private bool crashed = false;

    public void TakeCoin()
    {
        ++ControlPers_DataHandler.SingleOnScene.ProgressData_Coins;
    }

    #endregion

    #region Moving

        #region Road

        private enum Moving_Road_State
            {
                lnie_1,
                lnie_2,
                lnie_3,
                lnie_4
            }
            private Moving_Road_State moving_road_state = Moving_Road_State.lnie_2;    
        
            [SerializeField] private float moving_road_speed = 0.03f;

            private const float MOVING_ROAD_LINE_1_POSITION_Y = -0.55f;
            private const float MOVING_ROAD_LINE_2_POSITION_Y = -0.85f;
            private const float MOVING_ROAD_LINE_3_POSITION_Y = -1.15f;
            private const float MOVING_ROAD_LINE_4_POSITION_Y = -1.45f;

            private bool moving_road = false;
            private Vector3 moving_road_newPosition;
            private bool moving_road_ending_active = false;
            private Moving_Road_State moving_road_ending_destinationState;
            private bool moving_road_ending_visualStateSwap = false;

            private bool Moving_Road()
            {
                if (moving_road)
                {
                    transform.position = Vector3.MoveTowards(transform.position, moving_road_newPosition, moving_road_speed);

                    if (transform.position == moving_road_newPosition)
                    {
                        transform.position = moving_road_newPosition; // √аранитруем, что игрок будет в нужной точке. ¬Ќ≈«јѕЌќ: "transform.position == moving_road_newPosition" и "Vector3.MoveTowards(...)" не грантируют!
                        moving_road = false;
                    }
                }
                else
                {
                    if (moving_road_ending_active)
                    {
                        moving_road_state = moving_road_ending_destinationState;
                        moving_road_ending_visualStateSwap = true;
                        moving_road_ending_active = false;
                    }
                    else
                    {
                        if (moving_road_ending_visualStateSwap)
                        {
                            VisualState_Set(VisualState.right);
                            moving_road_ending_visualStateSwap = false;
                        }
                    }
                }

                return (moving_road_ending_active);
            }

            private void Moving_Road_Start(float _newPosition, Moving_Road_State _destinationState, VisualState _visualState)
            {
                moving_road = true;
                moving_road_ending_active = true;

                moving_road_newPosition.y = _newPosition;
                moving_road_ending_destinationState = _destinationState;

                VisualState_Set(_visualState);
            }
            private void Moving_Road_Start_Up()
            {
                switch (moving_road_state)
                {
                    case Moving_Road_State.lnie_2:
                        Moving_Road_Start(MOVING_ROAD_LINE_1_POSITION_Y, Moving_Road_State.lnie_1, VisualState.right_up);
                    break;

                    case Moving_Road_State.lnie_3:
                        Moving_Road_Start(MOVING_ROAD_LINE_2_POSITION_Y, Moving_Road_State.lnie_2, VisualState.right_up);
                    break;

                    case Moving_Road_State.lnie_4:
                        Moving_Road_Start(MOVING_ROAD_LINE_3_POSITION_Y, Moving_Road_State.lnie_3, VisualState.right_up);
                    break;
                }
            }
            private void Moving_Road_Start_Down()
            {
                switch (moving_road_state)
                {
                    case Moving_Road_State.lnie_1:
                        Moving_Road_Start(MOVING_ROAD_LINE_2_POSITION_Y, Moving_Road_State.lnie_2, VisualState.right_down);
                    break;

                    case Moving_Road_State.lnie_2:
                        Moving_Road_Start(MOVING_ROAD_LINE_3_POSITION_Y, Moving_Road_State.lnie_3, VisualState.right_down);
                    break;

                    case Moving_Road_State.lnie_3:
                        Moving_Road_Start(MOVING_ROAD_LINE_4_POSITION_Y, Moving_Road_State.lnie_4, VisualState.right_down);
                    break;
                }
            }

        #endregion

        #region Road_ToDrift
        
        private const float MOVING_ROAD_TODRIFT_BRAKING_SPEED = 0.25f;

        private float moving_road_todrift_movedown_speed = MOVING_ROAD_TODRIFT_BRAKING_SPEED;
        private const float MOVING_ROAD_TODRIFT_MOVEDOWN_SPEED_INC = 0.025f;
        private const float MOVING_ROAD_TODRIFT_MOVEDOWN_SPEED_MAX = 2f; 

        #endregion

        #region Drift
        
        private float moving_drift_input_angle = 270f;

        private float moving_drift_speed_current;
        [SerializeField] private float moving_drift_speed_min = 2f;
        [SerializeField] private float moving_drift_speed_step_up = 1.5f;
        [SerializeField] private float moving_drift_speed_max = 3f;
        private float moving_drift_angle_current;
        [SerializeField] private float moving_drift_angle_step = 0.03f;
        private Vector2 moving_drift_ofs = Vector2.zero;
        
        #endregion

    #endregion

    #region VisualState

    private enum VisualState
    {
        left,
        left_up,
        left_down,
        right,
        right_up,
        right_down,
        up,
        down
    }
    private VisualState visual_state_current = VisualState.right;

    private const string VISUALSTATE_LEFT_ANIMATOR_P = "Left";
    [SerializeField] private Texture2D visualState_left_spriteRenderer_material_normalMap;
    private readonly Lights visualState_left_lights_front_1 = new Lights(-0.07f, 0, -180f);
    private readonly Lights visualState_left_lights_front_2 = new Lights(-0.07f, -0.1f, -180f);
    private readonly Lights visualState_left_lights_rear_1 = new Lights(0.225f,-0.02f, 0);
    private readonly Lights visualState_left_lights_rear_2 = new Lights(0.225f, -0.05f, 0);

    private const string VISUALSTATE_LEFT_UP_ANIMATOR_P = "Left_Up";
    [SerializeField] private Texture2D visualState_left_up_spriteRenderer_material_normalMap;
    private readonly Lights visualState_left_up_lights_front_1 = new Lights(0.01f, 0, -140f);
    private readonly Lights visualState_left_up_lights_front_2 = new Lights(-0.1f, -0.07f, -145f);
    private readonly Lights visualState_left_up_lights_rear_1 = new Lights(0.2f, -0.01f, 35f);
    private readonly Lights visualState_left_up_lights_rear_2 = new Lights(0.1f, -0.1f, 35f);

    private const string VISUALSTATE_LEFT_DOWN_ANIMATOR_P = "Left_Down";
    [SerializeField] private Texture2D visualState_left_down_spriteRenderer_material_normalMap;
    private readonly Lights visualState_left_down_lights_front_1 = new Lights(-0.07f, 0.01f, 150f);
    private readonly Lights visualState_left_down_lights_front_2 = new Lights(0, -0.03f, 140f);
    private readonly Lights visualState_left_down_lights_rear_1 = new Lights(0.225f, 0, -40f);
    private readonly Lights visualState_left_down_lights_rear_2 = new Lights(0.1f, 0.1f, -40f);

    private const string VISUALSTATE_RIGHT_ANIMATOR_P = "Right";
    [SerializeField] private Texture2D visualState_right_spriteRenderer_material_normalMap;
    private readonly Lights visualState_right_lights_front_1 = new Lights(0.07f, 0, 0);
    private readonly Lights visualState_right_lights_front_2 = new Lights(0.07f, -0.1f, 0);
    private readonly Lights visualState_right_lights_rear_1 = new Lights(-0.225f, -0.02f, 180);
    private readonly Lights visualState_right_lights_rear_2 = new Lights(-0.225f, -0.05f, 180);

    private const string VISUALSTATE_RIGHT_UP_ANIMATOR_P = "Right_Up";
    [SerializeField] private Texture2D visualState_right_up_spriteRenderer_material_normalMap;
    private readonly Lights visualState_right_up_lights_front_1 = new Lights(-0.01f, -0.02f, 320f);
    private readonly Lights visualState_right_up_lights_front_2 = new Lights(0.07f, -0.1f, 325f);
    private readonly Lights visualState_right_up_lights_rear_1 = new Lights(-0.225f, 0.01f, 145f);
    private readonly Lights visualState_right_up_lights_rear_2 = new Lights(-0.1f, -0.12f, 145f);

    private const string VISUALSTATE_RIGHT_DOWN_ANIMATOR_P = "Right_Down";
    [SerializeField] private Texture2D visualState_right_down_spriteRenderer_material_normalMap;
    private readonly Lights visualState_right_down_lights_front_1 = new Lights(0.05f, 0.025f, 30f);
    private readonly Lights visualState_right_down_lights_front_2 = new Lights(0, -0.025f, 40f);
    private readonly Lights visualState_right_down_lights_rear_1 = new Lights(-0.1f, 0.1f, 220f);
    private readonly Lights visualState_right_down_lights_rear_2 = new Lights(-0.225f, -0.01f, 220f);

    private const string VISUALSTATE_UP_ANIMATOR_P = "Up";
    [SerializeField] private Texture2D visualState_up_spriteRenderer_material_normalMap;
    private readonly Lights visualState_up_lights_front_1 = new Lights(-0.07f, -0.125f, 270f);
    private readonly Lights visualState_up_lights_front_2 = new Lights(0.07f, -0.125f, 270f);
    private readonly Lights visualState_up_lights_rear_1 = new Lights(-0.125f, -0.025f, 90f);
    private readonly Lights visualState_up_lights_rear_2 = new Lights(0.125f, -0.025f, 90f);

    private const string VISUALSTATE_DOWN_ANIMATOR_P = "Down";
    [SerializeField] private Texture2D visualState_down_spriteRenderer_material_normalMap;
    private readonly Lights visualState_down_lights_front_1 = new Lights(0.1f, 0.05f, 90f);
    private readonly Lights visualState_down_lights_front_2 = new Lights(-0.1f, 0.05f, 90f);
    private readonly Lights visualState_down_lights_rear_1 = new Lights(0.07f, 0.07f, 270f);
    private readonly Lights visualState_down_lights_rear_2 = new Lights(-0.07f, 0.07f, 270f);

    private void VisualState_Set(VisualState _visualState)
    {
        if (_visualState != visual_state_current)
        {
            visual_state_current = _visualState;

            void _Lights_Set(Lights _front_1, Lights _front_2, Lights _rear_1, Lights _rear_2)
            {
                lights_front_1.transform.localPosition = _front_1.localPosition;
                lights_front_1.transform.rotation = _front_1.rotation;

                lights_front_2.transform.localPosition = _front_2.localPosition;
                lights_front_2.transform.rotation = _front_2.rotation;
            
                lights_rear_1.transform.localPosition = _rear_1.localPosition;
                lights_rear_1.transform.rotation = _rear_1.rotation;

                lights_rear_2.transform.localPosition = _rear_2.localPosition;
                lights_rear_2.transform.rotation = _rear_2.rotation;
            }

            switch (_visualState)
            {
                case VisualState.left:
                    animator.SetTrigger(VISUALSTATE_LEFT_ANIMATOR_P);
                    spriteRenderer.material.SetTexture(Constants.MATERIAL_BUMPMAP_U_BUMPMAP, visualState_left_spriteRenderer_material_normalMap);
                    _Lights_Set(visualState_left_lights_front_1, visualState_left_lights_front_2, visualState_left_lights_rear_1, visualState_left_lights_rear_2);
                break;

                case VisualState.left_up:
                    animator.SetTrigger(VISUALSTATE_LEFT_UP_ANIMATOR_P);
                    spriteRenderer.material.SetTexture(Constants.MATERIAL_BUMPMAP_U_BUMPMAP, visualState_left_up_spriteRenderer_material_normalMap);
                    _Lights_Set(visualState_left_up_lights_front_1, visualState_left_up_lights_front_2, visualState_left_up_lights_rear_1, visualState_left_up_lights_rear_2);
                break;
            
                case VisualState.left_down:
                    animator.SetTrigger(VISUALSTATE_LEFT_DOWN_ANIMATOR_P);
                    spriteRenderer.material.SetTexture(Constants.MATERIAL_BUMPMAP_U_BUMPMAP, visualState_left_down_spriteRenderer_material_normalMap);
                    _Lights_Set(visualState_left_down_lights_front_1, visualState_left_down_lights_front_2, visualState_left_down_lights_rear_1, visualState_left_down_lights_rear_2);
                break;
            
                case VisualState.right:
                    animator.SetTrigger(VISUALSTATE_RIGHT_ANIMATOR_P);
                    spriteRenderer.material.SetTexture(Constants.MATERIAL_BUMPMAP_U_BUMPMAP, visualState_right_spriteRenderer_material_normalMap);
                    _Lights_Set(visualState_right_lights_front_1, visualState_right_lights_front_2, visualState_right_lights_rear_1, visualState_right_lights_rear_2);
                break;

                case VisualState.right_up:
                    animator.SetTrigger(VISUALSTATE_RIGHT_UP_ANIMATOR_P);
                    spriteRenderer.material.SetTexture(Constants.MATERIAL_BUMPMAP_U_BUMPMAP, visualState_right_up_spriteRenderer_material_normalMap);
                    _Lights_Set(visualState_right_up_lights_front_1, visualState_right_up_lights_front_2, visualState_right_up_lights_rear_1, visualState_right_up_lights_rear_2);
                break;

                case VisualState.right_down:
                    animator.SetTrigger(VISUALSTATE_RIGHT_DOWN_ANIMATOR_P);
                    spriteRenderer.material.SetTexture(Constants.MATERIAL_BUMPMAP_U_BUMPMAP, visualState_right_down_spriteRenderer_material_normalMap);
                    _Lights_Set(visualState_right_down_lights_front_1, visualState_right_down_lights_front_2, visualState_right_down_lights_rear_1, visualState_right_down_lights_rear_2);
                break;

                case VisualState.up:
                    animator.SetTrigger(VISUALSTATE_UP_ANIMATOR_P);
                    spriteRenderer.material.SetTexture(Constants.MATERIAL_BUMPMAP_U_BUMPMAP, visualState_up_spriteRenderer_material_normalMap);
                    _Lights_Set(visualState_up_lights_front_1, visualState_up_lights_front_2, visualState_up_lights_rear_1, visualState_up_lights_rear_2);
                break;

                case VisualState.down:
                    animator.SetTrigger(VISUALSTATE_DOWN_ANIMATOR_P);
                    spriteRenderer.material.SetTexture(Constants.MATERIAL_BUMPMAP_U_BUMPMAP, visualState_down_spriteRenderer_material_normalMap);
                    _Lights_Set(visualState_down_lights_front_1, visualState_down_lights_front_2, visualState_down_lights_rear_1, visualState_down_lights_rear_2);
                break;
            }
        }
    }

    private object[,] visualState_angleRanges = new object[2, 8]
    {
        { -157.5f,          -112.5f,               -67.5f,           -22.5f,                 22.5f,             67.5f,                112.5f,         157.5f              },
        { VisualState.left, VisualState.left_down, VisualState.down, VisualState.right_down, VisualState.right, VisualState.right_up, VisualState.up, VisualState.left_up }
    };
    public void VisualState_FromAngle(float _angle)
    {
        var _visualState = VisualState.left;

        for (var _i = 0; _i < visualState_angleRanges.GetLength(1); ++_i)
        {
            if (_angle < (float)visualState_angleRanges[0, _i])
            {
                _visualState = (VisualState)visualState_angleRanges[1, _i];
                break;
            }
        }

        VisualState_Set(_visualState);
    }

    #endregion

    #region Physics

    private Rigidbody2D rigidBody;
    public BoxCollider2D BoxCollider { get; private set; }

    bool collisionTrigger = false;

    #endregion

    private void Awake()
    {       
        SingleOnScene = this;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer_material_color = spriteRenderer.material.GetColor(Constants.MATERIAL_2D_BUMP_U_COLOR);
        spriteRenderer.material.SetTexture(Constants.MATERIAL_BUMPMAP_U_BUMPMAP, visualState_right_spriteRenderer_material_normalMap);

        animator = GetComponent<Animator>();

        rigidBody = GetComponent<Rigidbody2D>();

        BoxCollider = GetComponent<BoxCollider2D>();

        Up_Count = 1;

        Invul = false;

        transform.position = new Vector3(transform.position.x, MOVING_ROAD_LINE_2_POSITION_Y, transform.position.z);
        moving_road_newPosition = transform.position;

        moving_drift_angle_current = moving_drift_input_angle;
    }

    private void Update()
    {
        if (active)
        {
            switch (state_current)
            {
                case State.road:
                    if (!crashed
                    && !Moving_Road()
                    && AppScreen_Local_SceneMain_UICanvas_VirtualStick_Entity.SingleOnScene.Visual_Inner_Magnitude_Active)
                    {
                        if (AppScreen_Local_SceneMain_UICanvas_VirtualStick_Entity.SingleOnScene.Visual_Inner_Direction > 0)
                        {
                            Moving_Road_Start_Up();
                        }
                        else
                        {
                            if (AppScreen_Local_SceneMain_UICanvas_VirtualStick_Entity.SingleOnScene.Visual_Inner_Direction < 0)
                            {
                                Moving_Road_Start_Down();
                            }
                        }
                    }

                    if (Invul)
                    {
                        if (invul_alpha_state)
                        {
                            spriteRenderer_material_color.a += invul_alpha_step * Time.deltaTime;
                            spriteRenderer.material.SetColor(Constants.MATERIAL_2D_BUMP_U_COLOR, spriteRenderer_material_color);

                            if (spriteRenderer_material_color.a >= 1f)
                            {
                                invul_alpha_state = false;
                            }
                        }
                        else
                        {
                            spriteRenderer_material_color.a -= invul_alpha_step * Time.deltaTime;
                            spriteRenderer.material.SetColor(Constants.MATERIAL_2D_BUMP_U_COLOR, spriteRenderer_material_color);

                            if (spriteRenderer_material_color.a <= 0)
                            {
                                invul_alpha_state = true;
                            }
                        }

                        invul_timer -= Time.deltaTime;

                        if (invul_timer <= 0)
                        {
                            spriteRenderer_material_color.a = 1f;
                            spriteRenderer.material.SetColor(Constants.MATERIAL_2D_BUMP_U_COLOR, spriteRenderer_material_color);
                            Invul = false;
                        }
                    }
                break;

                case State.road_toDrift_alignment:
                    if (!Moving_Road())
                    {
                        switch (moving_road_state)
                        {
                            case Moving_Road_State.lnie_1:
                                Moving_Road_Start_Down();
                            break;

                            case Moving_Road_State.lnie_3:
                            case Moving_Road_State.lnie_4:
                                Moving_Road_Start_Up();
                            break;

                            case Moving_Road_State.lnie_2:
                                VisualState_Set(VisualState.right_down);
                                state_current = State.road_toDrift_braking;
                            break;
                        }
                    }
                break;

                case State.road_toDrift_braking:
                    transform.position += Vector3.down * MOVING_ROAD_TODRIFT_BRAKING_SPEED * Time.deltaTime;
                break;

                case State.road_toDrift_moveDown:
                    transform.position += Vector3.down * moving_road_todrift_movedown_speed * Time.deltaTime;

                    if (moving_road_todrift_movedown_speed < MOVING_ROAD_TODRIFT_MOVEDOWN_SPEED_MAX)
                    {
                        moving_road_todrift_movedown_speed += MOVING_ROAD_TODRIFT_MOVEDOWN_SPEED_INC;
                    }
                break;

                case State.drift:
                    if (collisionTrigger)
                    {
                        moving_drift_speed_current = moving_drift_speed_min;
                    }
                    else
                    {
                        moving_drift_speed_current = moving_drift_speed_max;
                        
                        if (moving_drift_speed_current < moving_drift_speed_max)
                        {
                            moving_drift_speed_current += moving_drift_speed_step_up * Time.deltaTime;

                            if (moving_drift_speed_current > moving_drift_speed_max)
                            {
                                moving_drift_speed_current = moving_drift_speed_max;
                            }
                        }
                    }

                    if (AppScreen_Local_SceneMain_UICanvas_VirtualStick_Entity.SingleOnScene.Visual_Inner_Magnitude_Active)
                    {
                        moving_drift_input_angle = AppScreen_Local_SceneMain_UICanvas_VirtualStick_Entity.SingleOnScene.Visual_Inner_Direction;
                    }

                    moving_drift_angle_current = AngleHandler.Angle_SmoothStep(moving_drift_angle_current, moving_drift_input_angle, moving_drift_angle_step);
                    moving_drift_ofs = AngleHandler.Angle_ToVector(moving_drift_angle_current, moving_drift_speed_current);
                    
                    VisualState_FromAngle(moving_drift_angle_current);
                break;
            }
        }                
    }

    private void FixedUpdate()
    {
        if (active)
        {
            switch (state_current)
            {
                case State.drift:
                    rigidBody.MovePosition(rigidBody.position + moving_drift_ofs * Time.fixedDeltaTime);
                    
                    #if UNITY_EDITOR

                    var _line_scale = 10f;
                    DebugHandler.DrawLine2D(rigidBody.position.x, rigidBody.position.y, rigidBody.position.x + moving_drift_ofs.x * _line_scale, rigidBody.position.y + moving_drift_ofs.y * _line_scale, Color.green);
                    
                    #endif
                break;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D _collision)
    {
        collisionTrigger = true;
    }

    private void OnCollisionExit2D(Collision2D _collision)
    {
        collisionTrigger = false;
    }
}
