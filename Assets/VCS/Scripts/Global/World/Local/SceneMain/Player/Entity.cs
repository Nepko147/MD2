using UnityEngine;
using Utils;
using System.Collections;

public class World_Local_SceneMain_Player_Entity : MonoBehaviour
{
    #region General

    public static World_Local_SceneMain_Player_Entity SingleOnScene { get; private set; }

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
        drift,
        drift_toRoad
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
                    spriteRenderer_material_color = Color.white;
                    spriteRenderer.material.SetColor(Constants.MATERIAL_2D_BUMP_U_COLOR, spriteRenderer_material_color);
                    break;

                case State.road_toDrift_moveDown:
                    VisualState_Set(VisualState.down);
                    break;

                case State.drift:
                    moving_drift_speed_current = MOVING_ROAD_TODRIFT_MOVEDOWN_SPEED_MAX;
                    animator.SetFloat(ANIMATOR_PARAM_SPEED, 1f);
                    break;

                case State.drift_toRoad:
                    spriteRenderer_material_color = Color.white;
                    spriteRenderer.material.SetColor(Constants.MATERIAL_2D_BUMP_U_COLOR, spriteRenderer_material_color);
                    animator.SetFloat(ANIMATOR_PARAM_SPEED, 1f);
                    VisualState_Set(VisualState.up);

                break;
            }
        }
    }

    public Vector3 Position_Init { get; private set; }

    private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer  { get; private set; }
    private Color spriteRenderer_material_color = Color.white;

    private Animator animator;
    private const string ANIMATOR_PARAM_SPEED = "Speed";

    private Rigidbody2D rigidBody;

    private bool crashed = false;

    public void TakeCoin()
    {
        ++ControlPers_DataHandler.SingleOnScene.ProgressData_Coins;
        ++ControlPers_DataHandler.SingleOnScene.ProgressData_Statistics_CoinsTotal;
        ++AppScreen_Local_SceneMain_UICanvas_Received_Entity.SingleOnScene.Received_Coins_Count;
    }

    public void Resurrect()
    {
        Up_Take();

        crashed = false;
    }

    [SerializeField] private GameObject droppedCoin;

    #endregion

    #region Audio

    private AudioSource audio_source;
    [SerializeField] private AudioClip audio_sound_hit;
    [SerializeField] private AudioClip audio_sound_crash;
    [SerializeField] private AudioClip[] audio_sound_brake;

    private void Audio_Sound_Brake_Play()
    {
        var _ind = Random.Range(0, 4);
        audio_source.PlayOneShot(audio_sound_brake[_ind]);
    }

    #endregion

    #region Lights

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

    public Color Lights_Front_Color
    {
        get
        {
            var _color_lights_1 = lights_front_1.GetComponent<Light>().color;
            var _color_lights_2 = lights_front_2.GetComponent<Light>().color;
            var _color = Color.Lerp(_color_lights_1, _color_lights_2, 0.5f);
            return _color;
        }
        set
        {
            lights_front_1.GetComponent<Light>().color = value;
            lights_front_2.GetComponent<Light>().color = value;
        }
    }

    #endregion

    #region Tyres

    [SerializeField] private Texture2D tyres_texture;
    public Texture2D Tyres_Texture { get; private set; }
    [SerializeField] private GameObject tyres_front_1;
    [SerializeField] private GameObject tyres_front_2;
    [SerializeField] private GameObject tyres_rear_1;
    [SerializeField] private GameObject tyres_rear_2;
    public Vector3[] Tyres_Position { get; private set; } = new Vector3[4];
    public bool     Tyres_IsDrawing { get; private set; }
    private float   tyres_isDrawing_timer;
    private float   tyres_isDrawing_timer_init = 0.5f;

    private IEnumerator Tyres_IsDrawing_Corutine()
    {
        tyres_isDrawing_timer = tyres_isDrawing_timer_init;
        Tyres_IsDrawing = true;

        while (true)
        {
            tyres_isDrawing_timer -= ControlScene_Main.SingleOnScene.TimeDilation_Coef * Time.deltaTime;
            Tyres_Position = new Vector3[4] { tyres_front_1.transform.position, tyres_front_2.transform.position, tyres_rear_1.transform.position, tyres_rear_2.transform.position };

            if (tyres_isDrawing_timer > 0)
            {
                yield return null;
            }
            else
            {
                Tyres_IsDrawing = false;
                break;
            }
        }
    }

    private IEnumerator tyres_isDrawing__coroutine_current;

    private const float TYRES_LOCALPOSITION_Z = 0.0f;

    private struct Tyres
    {
        public Tyres(float _x, float _y)
        {
            localPosition = new Vector3(_x, _y, TYRES_LOCALPOSITION_Z);
        }

        public readonly Vector3 localPosition;
    }

    #endregion

    #region Up

    public int Up_Count { get; set; }

    public void Up_Take()
    {
        ++Up_Count;
    }

    public delegate void Up_Lose_Delegate();
    public event Up_Lose_Delegate Up_Lose_Event;

    public void Up_Lose()
    {
        Up_Lose_Event();

        --Up_Count;
        --AppScreen_Local_SceneMain_UICanvas_Entity.SingleOnScene.Ups_Visual;

        Invul_Activate(1.2f, Color.white);

        AppScreen_General_Camera_World_Entity.SingleOnScene.Shake();

        if (Up_Count > 0)
        {
            audio_source.PlayOneShot(audio_sound_hit);
        }
        else
        {
            spriteRenderer_material_color = Color.red;
            spriteRenderer.material.SetColor(Constants.MATERIAL_2D_BUMP_U_COLOR, spriteRenderer_material_color);

            animator.speed = 0;

            audio_source.PlayOneShot(audio_sound_crash);

            crashed = true;
        }
    }

    #endregion

    #region Invul

    public bool Invul_Active { get; private set; }
    private float invul_timer_current;
    private float invul_timer_max;
    private Color invul_color;
    private const float INVUL_COLOR_DELAY_INIT = 0.5f;
    private float invul_color_delay_current = INVUL_COLOR_DELAY_INIT;
    private bool invul_alpha_state = false;
    private float INVUL_ALPHA_STEP = 12; //Чем больше значение, тем быстрее моргает

    public void Invul_Activate(float _time, Color _color)
    {
        spriteRenderer_material_color = _color;

        Invul_Active = true;
        invul_timer_current = _time;
        invul_timer_max = _time;
        invul_color = _color;
        invul_color_delay_current = INVUL_COLOR_DELAY_INIT;
        invul_alpha_state = false;
    }

    private void Invul_Behaviour()
    {
        if (Invul_Active)
        {
            if (invul_color_delay_current > 0)
            {
                invul_color_delay_current -= ControlScene_Main.SingleOnScene.TimeDilation_Coef * Time.deltaTime;
            }
            else
            {
                var _col = Color.Lerp(Color.white, invul_color, invul_timer_current / invul_timer_max);
                spriteRenderer_material_color.r = _col.r;
                spriteRenderer_material_color.g = _col.g;
                spriteRenderer_material_color.b = _col.b;
            }

            if (!invul_alpha_state)
            {
                spriteRenderer_material_color.a -= INVUL_ALPHA_STEP * ControlScene_Main.SingleOnScene.TimeDilation_Coef * Time.deltaTime;
                spriteRenderer.material.SetColor(Constants.MATERIAL_2D_BUMP_U_COLOR, spriteRenderer_material_color);

                if (spriteRenderer_material_color.a <= 0)
                {
                    invul_alpha_state = true;
                }
            }
            else
            {
                spriteRenderer_material_color.a += INVUL_ALPHA_STEP * ControlScene_Main.SingleOnScene.TimeDilation_Coef * Time.deltaTime;
                spriteRenderer.material.SetColor(Constants.MATERIAL_2D_BUMP_U_COLOR, spriteRenderer_material_color);

                if (spriteRenderer_material_color.a >= 1f)
                {
                    invul_alpha_state = false;
                }
            }

            invul_timer_current -= ControlScene_Main.SingleOnScene.TimeDilation_Coef * Time.deltaTime;

            if (invul_timer_current <= 0)
            {
                spriteRenderer_material_color = Color.white;
                spriteRenderer.material.SetColor(Constants.MATERIAL_2D_BUMP_U_COLOR, spriteRenderer_material_color);

                Invul_Active = false;
            }
        }
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

        private const float MOVING_ROAD_SPEED = 2f;

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
                var _step = MOVING_ROAD_SPEED * ControlScene_Main.SingleOnScene.TimeDilation_Coef * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, moving_road_newPosition, _step);

                if ((moving_road_newPosition - transform.position).magnitude <= _step)
                {
                    transform.position = moving_road_newPosition;
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

        private float moving_road_toDrift_moveDown_speed = MOVING_ROAD_TODRIFT_BRAKING_SPEED;
        private const float MOVING_ROAD_TODRIFT_MOVEDOWN_SPEED_STEP = 2f;
        private const float MOVING_ROAD_TODRIFT_MOVEDOWN_SPEED_MAX = 2f;

        #endregion

        #region Drift
        
        private const float MOVING_DRIFT_SPEED_MIN = 2.5f;
        private const float MOVING_DRIFT_SPEED_MAX = 3.5f;
        private const float MOVING_DRIFT_SPEED_STEP = 1.5f;
        private float moving_drift_speed_current = MOVING_DRIFT_SPEED_MIN;
        private const float MOVING_DRIFT_ANGLE_INIT = 270f;
        private float moving_drift_angle_current = MOVING_DRIFT_ANGLE_INIT;
        private float moving_drift_angle_input = MOVING_DRIFT_ANGLE_INIT;
        private float MOVING_DRIFT_ANGLE_STEP = 1f;
        private Vector2 moving_drift_moveVector = Vector2.zero;
        private const float MOVING_DRIFT_MOVEVECTOR_SIZE_MAX = 3f;
        private Vector2 moving_drift_hitVector = Vector2.zero;
        private float moving_drift_hitVector_size = 0;
        private const float MOVING_DRIFT_HITVECTOR_SIZE_HIT_SOFT = 1.5f;
        private const float MOVING_DRIFT_HITVECTOR_SIZE_MAX = 3f;
        private const float MOVING_DRIFT_HITVECTOR_TIME = 1f;
        private bool moving_drift_braking = true;
        private const float MOVING_DRIFT_BRAKING_TIME_INIT = 0.5f;
        private float moving_drift_braking_time = MOVING_DRIFT_BRAKING_TIME_INIT;
        private bool moving_drift_braking_swap = true;
        private const float MOVING_DRIFT_BRAKING_SPEED = 1f;
        
        public float Moving_Drift_Speed_Current_Normalized_Get()
        {
            return ((moving_drift_speed_current - MOVING_DRIFT_SPEED_MIN) / (MOVING_DRIFT_SPEED_MAX - MOVING_DRIFT_SPEED_MIN));
        }
        
        #endregion

        #region Drift_ToRoad

        private Vector3 moving_drift_toRoad_position_target;
        public bool Moving_Drift_ToRoad_Braking { get; private set; }
        private const float MOVING_DRIFT_TOROAD_BRAKING_DIST = 3f;
        private const float MOVING_DRIFT_TOROAD_BRAKING_SPEED_STEP = 3f;
        private const float MOVING_DRIFT_BRAKING_SPEED_MIN = 2f;
        public bool Moving_Drift_ToRoad_Braking_Right { get; private set; }
        private const float MOVING_DRIFT_TOROAD_BRAKING_RIGHT_DIST = 1.5f;

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
        down,
        size
    }
    private VisualState visual_state_current = VisualState.right;

    private const string VISUALSTATE_LEFT_ANIMATOR_P = "Left";
    [SerializeField] private Texture2D visualState_left_spriteRenderer_material_normalMap;

    private readonly Lights visualState_left_lights_front_1 = new Lights(-0.07f, 0, -180f);
    private readonly Lights visualState_left_lights_front_2 = new Lights(-0.07f, -0.1f, -180f);
    private readonly Lights visualState_left_lights_rear_1 = new Lights(0.225f, -0.02f, 0);
    private readonly Lights visualState_left_lights_rear_2 = new Lights(0.225f, -0.05f, 0);

    private readonly Tyres visualState_left_tyres_front_1 = new Tyres(-0.2f, -0.08f);
    private readonly Tyres visualState_left_tyres_front_2 = new Tyres(-0.2f, -0.0f);
    private readonly Tyres visualState_left_tyres_rear_1 = new Tyres(0.25f, -0.08f);
    private readonly Tyres visualState_left_tyres_rear_2 = new Tyres(0.25f, -0.0f);

    private const string VISUALSTATE_LEFT_UP_ANIMATOR_P = "Left_Up";
    [SerializeField] private Texture2D visualState_left_up_spriteRenderer_material_normalMap;

    private readonly Lights visualState_left_up_lights_front_1 = new Lights(0.01f, 0, -140f);
    private readonly Lights visualState_left_up_lights_front_2 = new Lights(-0.1f, -0.07f, -145f);
    private readonly Lights visualState_left_up_lights_rear_1 = new Lights(0.2f, -0.01f, 35f);
    private readonly Lights visualState_left_up_lights_rear_2 = new Lights(0.1f, -0.1f, 35f);

    private readonly Tyres visualState_left_up_tyres_front_1 = new Tyres(-0.25f, 0.01f);
    private readonly Tyres visualState_left_up_tyres_front_2 = new Tyres(-0.08f, 0.11f);
    private readonly Tyres visualState_left_up_tyres_rear_1 = new Tyres(0.06f, -0.15f);
    private readonly Tyres visualState_left_up_tyres_rear_2 = new Tyres(0.23f, -0.02f);

    private const string VISUALSTATE_LEFT_DOWN_ANIMATOR_P = "Left_Down";
    [SerializeField] private Texture2D visualState_left_down_spriteRenderer_material_normalMap;

    private readonly Lights visualState_left_down_lights_front_1 = new Lights(-0.07f, 0.01f, 150f);
    private readonly Lights visualState_left_down_lights_front_2 = new Lights(0, -0.03f, 140f);
    private readonly Lights visualState_left_down_lights_rear_1 = new Lights(0.225f, 0, -40f);
    private readonly Lights visualState_left_down_lights_rear_2 = new Lights(0.1f, 0.1f, -40f);

    private readonly Tyres visualState_left_down_tyres_front_1 = new Tyres(-0.05f, -0.16f);
    private readonly Tyres visualState_left_down_tyres_front_2 = new Tyres(-0.24f, -0.05f);
    private readonly Tyres visualState_left_down_tyres_rear_1 = new Tyres(0.27f, 0.0f);
    private readonly Tyres visualState_left_down_tyres_rear_2 = new Tyres(0.08f, 0.11f);

    private const string VISUALSTATE_RIGHT_ANIMATOR_P = "Right";
    [SerializeField] private Texture2D visualState_right_spriteRenderer_material_normalMap;

    private readonly Lights visualState_right_lights_front_1 = new Lights(0.07f, 0, 0);
    private readonly Lights visualState_right_lights_front_2 = new Lights(0.07f, -0.1f, 0);
    private readonly Lights visualState_right_lights_rear_1 = new Lights(-0.225f, -0.02f, 180);
    private readonly Lights visualState_right_lights_rear_2 = new Lights(-0.225f, -0.05f, 180);

    private readonly Tyres visualState_right_tyres_front_1 = new Tyres(0.2f, -0.02f);
    private readonly Tyres visualState_right_tyres_front_2 = new Tyres(0.2f, -0.1f);
    private readonly Tyres visualState_right_tyres_rear_1 = new Tyres(-0.25f, -0.02f);
    private readonly Tyres visualState_right_tyres_rear_2 = new Tyres(-0.25f, -0.1f);

    private const string VISUALSTATE_RIGHT_UP_ANIMATOR_P = "Right_Up";
    [SerializeField] private Texture2D visualState_right_up_spriteRenderer_material_normalMap;

    private readonly Lights visualState_right_up_lights_front_1 = new Lights(-0.01f, -0.02f, 320f);
    private readonly Lights visualState_right_up_lights_front_2 = new Lights(0.07f, -0.1f, 325f);
    private readonly Lights visualState_right_up_lights_rear_1 = new Lights(-0.225f, 0.01f, 145f);
    private readonly Lights visualState_right_up_lights_rear_2 = new Lights(-0.1f, -0.12f, 145f);

    private readonly Tyres visualState_right_up_tyres_front_1 = new Tyres(0.1f, 0.12f);
    private readonly Tyres visualState_right_up_tyres_front_2 = new Tyres(0.25f, 0.01f);
    private readonly Tyres visualState_right_up_tyres_rear_1 = new Tyres(-0.25f, -0.01f);
    private readonly Tyres visualState_right_up_tyres_rear_2 = new Tyres(-0.07f, -0.15f);
    
    private const string VISUALSTATE_RIGHT_DOWN_ANIMATOR_P = "Right_Down";
    [SerializeField] private Texture2D visualState_right_down_spriteRenderer_material_normalMap;

    private readonly Lights visualState_right_down_lights_front_1 = new Lights(0.05f, 0.025f, 30f);
    private readonly Lights visualState_right_down_lights_front_2 = new Lights(0, -0.025f, 40f);
    private readonly Lights visualState_right_down_lights_rear_1 = new Lights(-0.1f, 0.1f, 220f);
    private readonly Lights visualState_right_down_lights_rear_2 = new Lights(-0.225f, -0.01f, 220f);

    private readonly Tyres visualState_right_down_tyres_front_1 = new Tyres(0.24f, -0.04f);
    private readonly Tyres visualState_right_down_tyres_front_2 = new Tyres(0.05f, -0.15f);
    private readonly Tyres visualState_right_down_tyres_rear_1 = new Tyres(-0.1f, 0.12f);
    private readonly Tyres visualState_right_down_tyres_rear_2 = new Tyres(-0.27f, 0.0f);

    private const string VISUALSTATE_UP_ANIMATOR_P = "Up";
    [SerializeField] private Texture2D visualState_up_spriteRenderer_material_normalMap;

    private readonly Lights visualState_up_lights_front_1 = new Lights(-0.07f, -0.125f, 270f);
    private readonly Lights visualState_up_lights_front_2 = new Lights(0.07f, -0.125f, 270f);
    private readonly Lights visualState_up_lights_rear_1 = new Lights(-0.125f, -0.025f, 90f);
    private readonly Lights visualState_up_lights_rear_2 = new Lights(0.125f, -0.025f, 90f);

    private readonly Tyres visualState_up_tyres_front_1 = new Tyres(-0.1f, 0.12f);
    private readonly Tyres visualState_up_tyres_front_2 = new Tyres(0.1f, 0.12f);
    private readonly Tyres visualState_up_tyres_rear_1 = new Tyres(-0.14f, -0.09f);
    private readonly Tyres visualState_up_tyres_rear_2 = new Tyres(0.14f, -0.09f);

    private const string VISUALSTATE_DOWN_ANIMATOR_P = "Down";
    [SerializeField] private Texture2D visualState_down_spriteRenderer_material_normalMap;

    private readonly Lights visualState_down_lights_front_1 = new Lights(0.1f, 0.05f, 90f);
    private readonly Lights visualState_down_lights_front_2 = new Lights(-0.1f, 0.05f, 90f);
    private readonly Lights visualState_down_lights_rear_1 = new Lights(0.07f, 0.07f, 270f);
    private readonly Lights visualState_down_lights_rear_2 = new Lights(-0.07f, 0.07f, 270f);

    private readonly Tyres visualState_down_tyres_front_1 = new Tyres(0.15f, -0.1f);
    private readonly Tyres visualState_down_tyres_front_2 = new Tyres(-0.15f, -0.1f);
    private readonly Tyres visualState_down_tyres_rear_1 = new Tyres(0.14f, 0.11f);
    private readonly Tyres visualState_down_tyres_rear_2 = new Tyres(-0.14f, 0.11f);

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

            void _Tyres_Set(Tyres _front_1, Tyres _front_2, Tyres _rear_1, Tyres _rear_2)
            {
                tyres_front_1.transform.localPosition = _front_1.localPosition;

                tyres_front_2.transform.localPosition = _front_2.localPosition;

                tyres_rear_1.transform.localPosition = _rear_1.localPosition;

                tyres_rear_2.transform.localPosition = _rear_2.localPosition;
            }

            switch (_visualState)
            {
                case VisualState.left:
                    animator.SetTrigger(VISUALSTATE_LEFT_ANIMATOR_P);
                    spriteRenderer.material.SetTexture(Constants.MATERIAL_BUMPMAP_U_BUMPMAP, visualState_left_spriteRenderer_material_normalMap);
                    _Lights_Set(visualState_left_lights_front_1, visualState_left_lights_front_2, visualState_left_lights_rear_1, visualState_left_lights_rear_2);
                    _Tyres_Set(visualState_left_tyres_front_1, visualState_left_tyres_front_2, visualState_left_tyres_rear_1, visualState_left_tyres_rear_2);
                    World_Local_SceneMain_Player_Collision_Bonus.SingleOnScene.Collision_Param_Set(180f, 0.76f, 0.24f);
                    break;

                case VisualState.left_up:
                    animator.SetTrigger(VISUALSTATE_LEFT_UP_ANIMATOR_P);
                    spriteRenderer.material.SetTexture(Constants.MATERIAL_BUMPMAP_U_BUMPMAP, visualState_left_up_spriteRenderer_material_normalMap);
                    _Lights_Set(visualState_left_up_lights_front_1, visualState_left_up_lights_front_2, visualState_left_up_lights_rear_1, visualState_left_up_lights_rear_2);
                    _Tyres_Set(visualState_left_up_tyres_front_1, visualState_left_up_tyres_front_2, visualState_left_up_tyres_rear_1, visualState_left_up_tyres_rear_2);
                    World_Local_SceneMain_Player_Collision_Bonus.SingleOnScene.Collision_Param_Set(162f, 0.7f, 0.3f);
                    break;

                case VisualState.left_down:
                    animator.SetTrigger(VISUALSTATE_LEFT_DOWN_ANIMATOR_P);
                    spriteRenderer.material.SetTexture(Constants.MATERIAL_BUMPMAP_U_BUMPMAP, visualState_left_down_spriteRenderer_material_normalMap);
                    _Lights_Set(visualState_left_down_lights_front_1, visualState_left_down_lights_front_2, visualState_left_down_lights_rear_1, visualState_left_down_lights_rear_2);
                    _Tyres_Set(visualState_left_down_tyres_front_1, visualState_left_down_tyres_front_2, visualState_left_down_tyres_rear_1, visualState_left_down_tyres_rear_2);
                    World_Local_SceneMain_Player_Collision_Bonus.SingleOnScene.Collision_Param_Set(208f, 0.7f, 0.3f);
                    break;

                case VisualState.right:
                    animator.SetTrigger(VISUALSTATE_RIGHT_ANIMATOR_P);
                    spriteRenderer.material.SetTexture(Constants.MATERIAL_BUMPMAP_U_BUMPMAP, visualState_right_spriteRenderer_material_normalMap);
                    _Lights_Set(visualState_right_lights_front_1, visualState_right_lights_front_2, visualState_right_lights_rear_1, visualState_right_lights_rear_2);
                    _Tyres_Set(visualState_right_tyres_front_1, visualState_right_tyres_front_2, visualState_right_tyres_rear_1, visualState_right_tyres_rear_2);
                    World_Local_SceneMain_Player_Collision_Bonus.SingleOnScene.Collision_Param_Set(0, 0.76f, 0.24f);
                    break;

                case VisualState.right_up:
                    animator.SetTrigger(VISUALSTATE_RIGHT_UP_ANIMATOR_P);
                    spriteRenderer.material.SetTexture(Constants.MATERIAL_BUMPMAP_U_BUMPMAP, visualState_right_up_spriteRenderer_material_normalMap);
                    _Lights_Set(visualState_right_up_lights_front_1, visualState_right_up_lights_front_2, visualState_right_up_lights_rear_1, visualState_right_up_lights_rear_2);
                    _Tyres_Set(visualState_right_up_tyres_front_1, visualState_right_up_tyres_front_2, visualState_right_up_tyres_rear_1, visualState_right_up_tyres_rear_2);
                    World_Local_SceneMain_Player_Collision_Bonus.SingleOnScene.Collision_Param_Set(27f, 0.7f, 0.3f);
                    break;

                case VisualState.right_down:
                    animator.SetTrigger(VISUALSTATE_RIGHT_DOWN_ANIMATOR_P);
                    spriteRenderer.material.SetTexture(Constants.MATERIAL_BUMPMAP_U_BUMPMAP, visualState_right_down_spriteRenderer_material_normalMap);
                    _Lights_Set(visualState_right_down_lights_front_1, visualState_right_down_lights_front_2, visualState_right_down_lights_rear_1, visualState_right_down_lights_rear_2);
                    _Tyres_Set(visualState_right_down_tyres_front_1, visualState_right_down_tyres_front_2, visualState_right_down_tyres_rear_1, visualState_right_down_tyres_rear_2);
                    World_Local_SceneMain_Player_Collision_Bonus.SingleOnScene.Collision_Param_Set(332f, 0.7f, 0.3f);
                    break;

                case VisualState.up:
                    animator.SetTrigger(VISUALSTATE_UP_ANIMATOR_P);
                    spriteRenderer.material.SetTexture(Constants.MATERIAL_BUMPMAP_U_BUMPMAP, visualState_up_spriteRenderer_material_normalMap);
                    _Lights_Set(visualState_up_lights_front_1, visualState_up_lights_front_2, visualState_up_lights_rear_1, visualState_up_lights_rear_2);
                    _Tyres_Set(visualState_up_tyres_front_1, visualState_up_tyres_front_2, visualState_up_tyres_rear_1, visualState_up_tyres_rear_2);
                    World_Local_SceneMain_Player_Collision_Bonus.SingleOnScene.Collision_Param_Set(90f, 0.5f, 0.36f);
                    break;

                case VisualState.down:
                    animator.SetTrigger(VISUALSTATE_DOWN_ANIMATOR_P);
                    spriteRenderer.material.SetTexture(Constants.MATERIAL_BUMPMAP_U_BUMPMAP, visualState_down_spriteRenderer_material_normalMap);
                    _Lights_Set(visualState_down_lights_front_1, visualState_down_lights_front_2, visualState_down_lights_rear_1, visualState_down_lights_rear_2);
                    _Tyres_Set(visualState_down_tyres_front_1, visualState_down_tyres_front_2, visualState_down_tyres_rear_1, visualState_down_tyres_rear_2);
                    World_Local_SceneMain_Player_Collision_Bonus.SingleOnScene.Collision_Param_Set(90f, 0.5f, 0.36f);
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

    #region Collision

    public CircleCollider2D Collision_Hit { get; set; }

    public void Collision_Hit_Soft(Vector2 _position)
    {
        moving_drift_hitVector = (Vector2)transform.position - _position;
        moving_drift_hitVector = moving_drift_hitVector.normalized;
        moving_drift_hitVector_size = MOVING_DRIFT_HITVECTOR_SIZE_HIT_SOFT;

        if (Up_Count > 1)
        {
            Up_Lose();
        }
        else
        {
            var _hit_soft_coinsLose = 50;
            
            if (ControlPers_DataHandler.SingleOnScene.ProgressData_Coins > 0)
            {
                var _coinsCurrent = ControlPers_DataHandler.SingleOnScene.ProgressData_Coins - _hit_soft_coinsLose;
                ControlPers_DataHandler.SingleOnScene.ProgressData_Coins = Mathf.Clamp(_coinsCurrent, 0, _coinsCurrent);

                IEnumerator CoinDecreaser()
                {
                    while (true)
                    {               
                        if (Active)
                        {
                            if (AppScreen_Local_SceneMain_UICanvas_Entity.SingleOnScene.Coins_Visual > ControlPers_DataHandler.SingleOnScene.ProgressData_Coins)
                            {
                                --AppScreen_Local_SceneMain_UICanvas_Entity.SingleOnScene.Coins_Visual;
                                Instantiate(droppedCoin, transform.position, new Quaternion());
                                yield return null;
                            }
                            else
                            {
                                AppScreen_Local_SceneMain_UICanvas_Entity.SingleOnScene.Coins_Visual = ControlPers_DataHandler.SingleOnScene.ProgressData_Coins;
                                break;
                            }
                        }
                        else
                        {
                            yield return null;
                        }
                    }
                }

                var _routine = CoinDecreaser();
                StartCoroutine(_routine);
            }
            else
            {
                Up_Lose();
            }
        }
    }

    public BoxCollider2D Collision_Bonus { get; set; }

    private bool collision_on = false;

    #endregion

    private void Awake()
    {       
        SingleOnScene = this;
        
        Position_Init = transform.position;
        Tyres_Texture = tyres_texture;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetTexture(Constants.MATERIAL_BUMPMAP_U_BUMPMAP, visualState_right_spriteRenderer_material_normalMap);
        SpriteRenderer = spriteRenderer;

        animator = GetComponent<Animator>();

        audio_source = GetComponent<AudioSource>();

        rigidBody = GetComponent<Rigidbody2D>();

        Up_Count = 1;

        Invul_Active = false;

        transform.position = new Vector3(transform.position.x, MOVING_ROAD_LINE_2_POSITION_Y, transform.position.z);
        moving_road_newPosition = transform.position;
        moving_drift_toRoad_position_target = new Vector3(Position_Init.x, MOVING_ROAD_LINE_3_POSITION_Y, Position_Init.z);
        Moving_Drift_ToRoad_Braking = false;
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

                    Invul_Behaviour();
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
                                animator.SetFloat(ANIMATOR_PARAM_SPEED, 0);
                                Audio_Sound_Brake_Play();

                                tyres_isDrawing__coroutine_current = Tyres_IsDrawing_Corutine();
                                StartCoroutine(tyres_isDrawing__coroutine_current);

                                state_current = State.road_toDrift_braking;
                            break;
                        }
                    }
                break;

                case State.road_toDrift_braking:
                    transform.position += Vector3.down * MOVING_ROAD_TODRIFT_BRAKING_SPEED * ControlScene_Main.SingleOnScene.TimeDilation_Coef * Time.deltaTime;
                break;

                case State.road_toDrift_moveDown:
                    if (moving_road_toDrift_moveDown_speed < MOVING_DRIFT_SPEED_MAX)
                    {
                        moving_road_toDrift_moveDown_speed += MOVING_ROAD_TODRIFT_MOVEDOWN_SPEED_STEP * ControlScene_Main.SingleOnScene.TimeDilation_Coef * Time.deltaTime;
                    }

					var _destination = World_Local_SceneMain_DriftSection_Point_Start.SingleOnScene.transform.position;
					var _speed = moving_road_toDrift_moveDown_speed * ControlScene_Main.SingleOnScene.TimeDilation_Coef * Time.deltaTime;

                    transform.position = Vector3.MoveTowards(transform.position, _destination, _speed);

                    var _dif = _destination - transform.position;

					if (_dif.magnitude <= _speed)
					{
                        moving_drift_angle_current = MOVING_DRIFT_ANGLE_INIT;
                        moving_drift_angle_input = MOVING_DRIFT_ANGLE_INIT;
                        moving_drift_hitVector = Vector2.zero;
                        moving_drift_hitVector_size = 0;

						state_current = State.drift;
					}
                break;

                case State.drift:

                    if (!crashed)
                    {
                        if (collision_on)
                        {
                            moving_drift_speed_current = MOVING_DRIFT_SPEED_MIN;
                        }
                        else
                        {
                            switch (visual_state_current)
                            {
                                case VisualState.left_up:
                                case VisualState.left_down:
                                case VisualState.right_up:
                                case VisualState.right_down:
                                    if (moving_drift_braking)
                                    {
                                        if (moving_drift_braking_swap)
                                        {
                                            animator.SetFloat(ANIMATOR_PARAM_SPEED, 0);
                                                                                        
                                            Audio_Sound_Brake_Play();

                                            tyres_isDrawing__coroutine_current = Tyres_IsDrawing_Corutine();
                                            StartCoroutine(tyres_isDrawing__coroutine_current);

                                            moving_drift_braking_swap = false;
                                        }

                                        moving_drift_speed_current = Mathf.Clamp(moving_drift_speed_current - MOVING_DRIFT_BRAKING_SPEED * ControlScene_Main.SingleOnScene.TimeDilation_Coef * Time.deltaTime, MOVING_DRIFT_SPEED_MIN, MOVING_DRIFT_SPEED_MAX);
                                        moving_drift_braking_time -= ControlScene_Main.SingleOnScene.TimeDilation_Coef * Time.deltaTime;

                                        if (moving_drift_braking_time <= 0)
                                        {
                                            animator.SetFloat(ANIMATOR_PARAM_SPEED, 1f);

                                            moving_drift_braking = false;
                                        }
                                    }
                                    else
                                    {
                                        moving_drift_speed_current = Mathf.Clamp(moving_drift_speed_current + MOVING_DRIFT_SPEED_STEP * ControlScene_Main.SingleOnScene.TimeDilation_Coef * Time.deltaTime, MOVING_DRIFT_SPEED_MIN, MOVING_DRIFT_SPEED_MAX);
                                    }
                                break;

                                default:
                                     moving_drift_speed_current = Mathf.Clamp(moving_drift_speed_current + MOVING_DRIFT_SPEED_STEP * ControlScene_Main.SingleOnScene.TimeDilation_Coef * Time.deltaTime, MOVING_DRIFT_SPEED_MIN, MOVING_DRIFT_SPEED_MAX);

                                    if (!moving_drift_braking_swap)
                                    {
                                        animator.SetFloat(ANIMATOR_PARAM_SPEED, 1f);

                                        moving_drift_braking = true;
                                        moving_drift_braking_time = MOVING_DRIFT_BRAKING_TIME_INIT;

                                        moving_drift_braking_swap = true;
                                    }
                                break;
                            }
                        }

                        if (AppScreen_Local_SceneMain_UICanvas_VirtualStick_Entity.SingleOnScene.Visual_Inner_Magnitude_Active)
                        {
                            moving_drift_angle_input = AppScreen_Local_SceneMain_UICanvas_VirtualStick_Entity.SingleOnScene.Visual_Inner_Direction;
                        }

                        moving_drift_angle_current = AngleHandler.Angle_SmoothStep(moving_drift_angle_current, moving_drift_angle_input, MOVING_DRIFT_ANGLE_STEP * Time.deltaTime);

                        VisualState_FromAngle(moving_drift_angle_current);
                    }

                    Invul_Behaviour();
                break;

                case State.drift_toRoad:
                    _speed = moving_drift_speed_current * ControlScene_Main.SingleOnScene.TimeDilation_Coef * Time.deltaTime;

                    transform.position = Vector3.MoveTowards(transform.position, moving_drift_toRoad_position_target, _speed);

                    _dif = moving_drift_toRoad_position_target - transform.position;

                    if (!Moving_Drift_ToRoad_Braking)
                    {
                        moving_drift_speed_current = Mathf.Clamp(moving_drift_speed_current + MOVING_DRIFT_SPEED_STEP * ControlScene_Main.SingleOnScene.TimeDilation_Coef * Time.deltaTime, moving_drift_speed_current, MOVING_DRIFT_SPEED_MAX);

                        if (_dif.magnitude <= MOVING_DRIFT_TOROAD_BRAKING_DIST)
					    {
                            VisualState_Set(VisualState.right_up);
                            animator.SetFloat(ANIMATOR_PARAM_SPEED, 0);
                            Audio_Sound_Brake_Play();

                            tyres_isDrawing__coroutine_current = Tyres_IsDrawing_Corutine();
                            StartCoroutine(tyres_isDrawing__coroutine_current);

                            Moving_Drift_ToRoad_Braking = true;
					    }
                    }
                    else
                    {
                        moving_drift_speed_current = Mathf.Clamp(moving_drift_speed_current - MOVING_DRIFT_TOROAD_BRAKING_SPEED_STEP * ControlScene_Main.SingleOnScene.TimeDilation_Coef * Time.deltaTime, MOVING_DRIFT_BRAKING_SPEED_MIN, moving_drift_speed_current);
                    
                        if (!Moving_Drift_ToRoad_Braking_Right)
                        {
                            if (_dif.magnitude <= MOVING_DRIFT_TOROAD_BRAKING_RIGHT_DIST)
                            {
                                VisualState_Set(VisualState.right);
                                animator.SetFloat(ANIMATOR_PARAM_SPEED, 1f);

                                Moving_Drift_ToRoad_Braking_Right = true;
                            }
                        }
                        else
                        {
                            if (_dif.magnitude <= _speed)
					        {
                                transform.position = moving_drift_toRoad_position_target;

                                moving_road_toDrift_moveDown_speed = MOVING_ROAD_TODRIFT_BRAKING_SPEED;
                                Moving_Drift_ToRoad_Braking = false;
                                Moving_Drift_ToRoad_Braking_Right = false;

					            state_current = State.road;
					        }
                        }
                    }
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
                    if (!crashed)
                    {
                        moving_drift_moveVector = AngleHandler.Angle_ToVector(moving_drift_angle_current, moving_drift_speed_current);

                        if (moving_drift_hitVector_size > 0)
                        {
                            moving_drift_hitVector_size -= MOVING_DRIFT_HITVECTOR_SIZE_MAX / MOVING_DRIFT_HITVECTOR_TIME * ControlScene_Main.SingleOnScene.TimeDilation_Coef * Time.deltaTime;
                            moving_drift_moveVector += moving_drift_hitVector * moving_drift_hitVector_size;
                            moving_drift_moveVector = Vector2.ClampMagnitude(moving_drift_moveVector, MOVING_DRIFT_MOVEVECTOR_SIZE_MAX);
                        }

                        rigidBody.MovePosition(rigidBody.position + moving_drift_moveVector * ControlScene_Main.SingleOnScene.TimeDilation_Coef * Time.deltaTime);
                    }

                    #if UNITY_EDITOR

                    Debug.DrawLine(rigidBody.position, moving_drift_moveVector, Color.green);
                    Debug.DrawLine(moving_drift_moveVector, moving_drift_hitVector, Color.red);

                    #endif
                break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (!collision_on)
        {
            moving_drift_hitVector = (Vector2)transform.position - (Vector2)_collision.gameObject.transform.position;
            moving_drift_hitVector = moving_drift_hitVector.normalized;
            moving_drift_hitVector_size = MOVING_DRIFT_HITVECTOR_SIZE_MAX;

            collision_on = true;
        }
    }

    private void OnCollisionStay2D()
    {
        if (!Invul_Active)
        {
            Up_Lose();
        }
    }

    private void OnCollisionExit2D(Collision2D _collision)
    {
        collision_on = false;
    }
}
