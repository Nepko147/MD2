using UnityEngine;

public class World_Local_SceneMain_Player : MonoBehaviour
{
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
                player_animation.speed = 1;
            }
            else
            {
                player_animation.speed = 0;
            }
        }
    }

    private const float LINE_1_POSITION_Y = -0.55f;

    private const float LINE_2_POSITION_Y = -0.85f;

    private const float LINE_3_POSITION_Y = -1.15f;

    private const float LINE_4_POSITION_Y = -1.45f;

    [SerializeField] private float player_moving_maxDistanceDelta = 0.03f;

    private Vector3 player_newPosition;
    private bool player_moving = false;
    private bool player_moving_ending = false;
    private void Player_Moving_Start_Up(float _line_y)
    {
        player_moving = true;
        player_moving_ending = true;
        player_animation.SetBool(PLAYER_ANIMATION_DOWN, false);
        player_animation.SetBool(PLAYER_ANIMATION_UP, true);
        headlights_forward.transform.rotation = Quaternion.Euler(headlights_forward_rotation_up.x, headlights_forward_rotation_up.y, headlights_forward_rotation_up.z);
        headlights_backward.transform.localPosition = new Vector3(headlights_backward.transform.localPosition.x, HEADLIGHTS_BACKWARD_LOCALPOSITION_UP_Y, headlights_backward.transform.localPosition.z);
        headlights_backward.transform.rotation = Quaternion.Euler(headlights_backward_rotation_up.x, headlights_backward_rotation_up.y, headlights_backward_rotation_up.z);
        player_newPosition = new Vector3(transform.position.x, _line_y, transform.position.z);
        player_spriteRenderer.material.SetTexture(Constants.MATERIAL_2D_BUMP_U_BUMPMAP, player_normalMap_up);
    }
    private void Player_Moving_Start_Down(float _line_y)
    {
        player_moving = true;
        player_moving_ending = true;
        player_animation.SetBool(PLAYER_ANIMATION_UP, false);
        player_animation.SetBool(PLAYER_ANIMATION_DOWN, true);
        headlights_forward.transform.rotation = Quaternion.Euler(headlights_forward_rotation_down.x, headlights_forward_rotation_down.y, headlights_forward_rotation_down.z);
        headlights_backward.transform.localPosition = new Vector3(headlights_backward.transform.localPosition.x, HEADLIGHTS_BACKWARD_LOCALPOSITION_DOWN_Y, headlights_backward.transform.localPosition.z);
        headlights_backward.transform.rotation = Quaternion.Euler(headlights_backward_rotation_down.x, headlights_backward_rotation_down.y, headlights_backward_rotation_down.z);
        player_newPosition = new Vector3(transform.position.x, _line_y, transform.position.z);
        player_spriteRenderer.material.SetTexture(Constants.MATERIAL_2D_BUMP_U_BUMPMAP, player_normalMap_down);
    }
    private bool Player_Moving()
    {
        if (player_moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, player_newPosition, player_moving_maxDistanceDelta);

            if (transform.position == player_newPosition)
            {
                transform.position = player_newPosition; // √аранитруем, что игрок будет в нужной точке. ¬Ќ≈«јѕЌќ: "transform.position == player_newPosition" и "Vector3.MoveTowards(...)" не грантируют!
                player_moving = false;
            }
        }
        else
        {
            if (player_moving_ending)
            {
                player_animation.SetBool(PLAYER_ANIMATION_UP, false);
                player_animation.SetBool(PLAYER_ANIMATION_DOWN, false);
                headlights_forward.transform.rotation = Quaternion.Euler(headlights_forward_rotation_straight.x, headlights_forward_rotation_straight.y, headlights_forward_rotation_straight.z);
                headlights_backward.transform.localPosition = new Vector3(headlights_backward.transform.localPosition.x, HEADLIGHTS_BACKWARD_LOCALPOSITION_STRAIGHT_Y, headlights_backward.transform.localPosition.z);
                headlights_backward.transform.rotation = Quaternion.Euler(headlights_backward_rotation_straight.x, headlights_backward_rotation_straight.y, headlights_backward_rotation_straight.z);
                player_spriteRenderer.material.SetTexture(Constants.MATERIAL_2D_BUMP_U_BUMPMAP, player_normalMap_stright);

                player_moving_ending = false;
            }
        }

        return (player_moving_ending);
    }

    public bool                     Player_Invul { get; private set; }
    private bool                    player_invul_alpha_increase = false;
    [SerializeField] private float  player_invul_alpha_delta = 12; //„ем больше значение, тем быстрее моргает
    [SerializeField] private float  player_invul_timer_init = 1.2f;
    private float                   player_invul_timer;    

    public int Player_Ups { get; set; }

    private int player_kilometersLeft;
    public int Player_KilometersLeft 
    { 
        get 
        {
            return (player_kilometersLeft);
        }
        set 
        { 
            player_kilometersLeft = value;
            AppScreen_Local_SceneMain_UICanvas_Indicators_Complete_Entity.SingleOnScene.Text_Number = player_kilometersLeft.ToString();
        }
    }
    [SerializeField] private int player_kilometersLeft_init = 25;
    
    private float player_kilometersLeft_delta_timer;
    [SerializeField] private float player_kilometersLeft_delta_timer_init = 30; // „ерез сколько проедем километр, без учЄта ускорени€
    
    public int                      Player_Coins { get; set; }

    Animator                        player_animation;
    const string                    PLAYER_ANIMATION_UP = "up";
    const string                    PLAYER_ANIMATION_DOWN = "down";

    private SpriteRenderer          player_spriteRenderer;
    [SerializeField] Texture2D      player_normalMap_stright;
    [SerializeField] Texture2D      player_normalMap_up;
    [SerializeField] Texture2D      player_normalMap_down;

    private Color player_color = Color.white;

    public BoxCollider2D            Player_BoxCollider { get; private set; }

    [SerializeField] private GameObject headlights_forward;
    private readonly Vector3 headlights_forward_rotation_up = new Vector3(-35, 90f, 0);
    private readonly Vector3 headlights_forward_rotation_straight = new Vector3(0, 90f, 0);
    private readonly Vector3 headlights_forward_rotation_down = new Vector3(35, 90f, 0);
    [SerializeField] private GameObject headlights_backward;
    private const float HEADLIGHTS_BACKWARD_LOCALPOSITION_UP_Y = -0.1f;
    private const float HEADLIGHTS_BACKWARD_LOCALPOSITION_STRAIGHT_Y = -0.05f;
    private const float HEADLIGHTS_BACKWARD_LOCALPOSITION_DOWN_Y = 0.1f;
    private readonly Vector3 headlights_backward_rotation_up = new Vector3(35, -90f, 0);
    private readonly Vector3 headlights_backward_rotation_straight = new Vector3(0, -90f, 0);
    private readonly Vector3 headlights_backward_rotation_down = new Vector3(-35, -90f, 0);

    private bool crashed = false;

    public enum State
    {
        road,
        road_toDrift_alignment,
        road_toDrift_braking,
        road_toDrift_moveDown
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
                    player_color.a = 1f;
                    player_spriteRenderer.material.SetColor(Constants.MATERIAL_2D_BUMP_U_COLOR, player_color);
                break;
            }
        }
    }

    [SerializeField] private Sprite state_road_toDrift_breaking_sprite;

    private void Awake()
    {       
        SingleOnScene = this;

        Player_Invul = false;
        Player_Ups = 1;
        Player_KilometersLeft = player_kilometersLeft_init;
        player_kilometersLeft_delta_timer = player_kilometersLeft_delta_timer_init;
        player_spriteRenderer = GetComponent<SpriteRenderer>();
        player_spriteRenderer.material.SetTexture(Constants.MATERIAL_2D_BUMP_U_BUMPMAP, player_normalMap_stright);
        player_animation = GetComponent<Animator>();
        Player_BoxCollider = GetComponent<BoxCollider2D>();

        transform.position = new Vector3(transform.position.x, LINE_2_POSITION_Y, transform.position.z);
    }

    private void FixedUpdate()
    {
        if (active)
        {
            switch (state_current)
            {
                case State.road:
                if (Player_Invul)
                {
                    player_color = player_spriteRenderer.material.GetColor(Constants.MATERIAL_2D_BUMP_U_COLOR);

                    if (player_invul_alpha_increase)
                    {
                        player_color.a += player_invul_alpha_delta * Time.deltaTime;
                        player_spriteRenderer.material.SetColor(Constants.MATERIAL_2D_BUMP_U_COLOR, player_color);

                        if (player_color.a >= 1)
                        {
                            player_invul_alpha_increase = false;
                        }
                    }
                    else
                    {
                        player_color.a -= player_invul_alpha_delta * Time.deltaTime;
                        player_spriteRenderer.material.SetColor(Constants.MATERIAL_2D_BUMP_U_COLOR, player_color);

                        if (player_color.a <= 0)
                        {
                            player_invul_alpha_increase = true;
                        }
                    }

                    player_invul_timer -= Time.deltaTime;

                    if (player_invul_timer <= 0)
                    {
                        player_color.a = 1f;
                        player_spriteRenderer.material.SetColor(Constants.MATERIAL_2D_BUMP_U_COLOR, player_color);
                        Player_Invul = false;
                    }
                }

                if (!crashed)
                {
                    if (player_kilometersLeft_delta_timer >= 0)
                    {
                        player_kilometersLeft_delta_timer -= Time.deltaTime * (1 + World_Local_SceneMain_MovingBackground_Entity.SingleOnScene.SpeedScale);
                    }
                    else
                    {
                        Player_KilometersLeft -= 1;
                        AppScreen_Local_SceneMain_UICanvas_Indicators_Complete_Entity.SingleOnScene.Show();
                        player_kilometersLeft_delta_timer = player_kilometersLeft_delta_timer_init;
                    }

                    if (!Player_Moving())
                    {
                        if (AppScreen_Local_SceneMain_UICanvas_VirtualStick_Entity.SingleOnScene.Inner_Direction > 0)
                        {
                            switch (transform.position.y)
                            {
                                case LINE_2_POSITION_Y:
                                    Player_Moving_Start_Up(LINE_1_POSITION_Y);
                                break;

                                case LINE_3_POSITION_Y:
                                    Player_Moving_Start_Up(LINE_2_POSITION_Y);
                                break;

                                case LINE_4_POSITION_Y:
                                    Player_Moving_Start_Up(LINE_3_POSITION_Y);
                                break;
                            }
                        }
                        else
                        {
                            if (AppScreen_Local_SceneMain_UICanvas_VirtualStick_Entity.SingleOnScene.Inner_Direction < 0)
                            {
                                switch (transform.position.y)
                                {
                                    case LINE_1_POSITION_Y:
                                        Player_Moving_Start_Down(LINE_2_POSITION_Y);
                                    break;

                                    case LINE_2_POSITION_Y:
                                        Player_Moving_Start_Down(LINE_3_POSITION_Y);
                                    break;

                                    case LINE_3_POSITION_Y:
                                        Player_Moving_Start_Down(LINE_4_POSITION_Y);
                                    break;
                                }
                            }
                        }
                    }
                }
                break;

                case State.road_toDrift_alignment:
                if (!Player_Moving())
                {
                    switch (transform.position.y)
                    {
                        case LINE_1_POSITION_Y:
                            Player_Moving_Start_Down(LINE_2_POSITION_Y);
                        break;

                        case LINE_3_POSITION_Y:
                            Player_Moving_Start_Up(LINE_2_POSITION_Y);
                        break;

                        case LINE_4_POSITION_Y:
                            Player_Moving_Start_Up(LINE_3_POSITION_Y);
                        break;

                        case LINE_2_POSITION_Y:
                            player_animation.SetBool(PLAYER_ANIMATION_UP, false);
                            player_animation.SetBool(PLAYER_ANIMATION_DOWN, true);
                            headlights_forward.transform.rotation = Quaternion.Euler(headlights_forward_rotation_down.x, headlights_forward_rotation_down.y, headlights_forward_rotation_down.z);
                            headlights_backward.transform.localPosition = new Vector3(headlights_backward.transform.localPosition.x, HEADLIGHTS_BACKWARD_LOCALPOSITION_DOWN_Y, headlights_backward.transform.localPosition.z);
                            headlights_backward.transform.rotation = Quaternion.Euler(headlights_backward_rotation_down.x, headlights_backward_rotation_down.y, headlights_backward_rotation_down.z);
                            player_spriteRenderer.material.SetTexture(Constants.MATERIAL_2D_BUMP_U_BUMPMAP, player_normalMap_down);

                            state_current = State.road_toDrift_braking;
                        break;
                    }
                }
                break;

                case State.road_toDrift_moveDown:
                    
                break;
            }
        }                
    }    
    
    public void LoseUp()
    {
        --Player_Ups;
        --AppScreen_Local_SceneMain_UICanvas_Entity.SingleOnScene.Ups_Visual;
        Player_Invul = true;
        player_invul_timer = player_invul_timer_init;
        AppScreen_General_Camera_World_Entity_Shake.SingleOnScene.Shake();

        if (Player_Ups <= 0)
        {
            player_spriteRenderer.material.SetColor(Constants.MATERIAL_2D_BUMP_U_COLOR, Color.red);
            crashed = true;
        }     
    }

    public void TakeUp()
    {
        ++Player_Ups;
    }

    public void TakeCoin()
    {
        ++ControlPers_DataHandler.SingleOnScene.ProgressData_Coins;
    }
}
