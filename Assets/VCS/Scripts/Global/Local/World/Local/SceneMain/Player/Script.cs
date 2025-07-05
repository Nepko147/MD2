using UnityEngine;

public class World_Local_SceneMain_Player : MonoBehaviour
{
    public static World_Local_SceneMain_Player SingleOnScene { get; private set; }

    public bool Active { get; set; }

    private const int   LINE_1_SORTINGORDER_PLAYER = 90;
    private const float LINE_1_POSITION_Y = -0.55f;

    private const int   LINE_2_SORTINGORDER_PLAYER = 110;
    private const float LINE_2_POSITION_Y = -0.85f;

    private const int   LINE_3_SORTINGORDER_PLAYER = 130;
    private const float LINE_3_POSITION_Y = -1.15f;

    private const int   LINE_4_SORTINGORDER_PLAYER = 150;
    private const float LINE_4_POSITION_Y = -1.45f;

    [SerializeField] private float  player_controlls;

    private Vector3 player_newPosition;
    private bool player_moving = false;
    private bool player_moving_end = false;
    private void Player_Moving_Start_Up(int _line_layer, float _line_y)
    {
        player_moving = true;
        player_moving_end = true;
        player_animation.SetBool(PLAYER_ANIMATION_DOWN, false);
        player_animation.SetBool(PLAYER_ANIMATION_UP, true);
        headlights_forward.transform.rotation = Quaternion.Euler(headlights_forward_rotation_up.x, headlights_forward_rotation_up.y, headlights_forward_rotation_up.z);
        headlights_backward.transform.localPosition = new Vector3(headlights_backward.transform.localPosition.x, HEADLIGHTS_BACKWARD_LOCALPOSITION_UP_Y, headlights_backward.transform.localPosition.z);
        headlights_backward.transform.rotation = Quaternion.Euler(headlights_backward_rotation_up.x, headlights_backward_rotation_up.y, headlights_backward_rotation_up.z);
        player_spriteRenderer.sortingOrder = _line_layer;
        player_newPosition = new Vector3(transform.position.x, _line_y, transform.position.z);
    }
    private void Player_Moving_Start_Down(int _line_layer, float _line_y)
    {
        player_moving = true;
        player_moving_end = true;
        player_animation.SetBool(PLAYER_ANIMATION_UP, false);
        player_animation.SetBool(PLAYER_ANIMATION_DOWN, true);
        headlights_forward.transform.rotation = Quaternion.Euler(headlights_forward_rotation_down.x, headlights_forward_rotation_down.y, headlights_forward_rotation_down.z);
        headlights_backward.transform.localPosition = new Vector3(headlights_backward.transform.localPosition.x, HEADLIGHTS_BACKWARD_LOCALPOSITION_DOWN_Y, headlights_backward.transform.localPosition.z);
        headlights_backward.transform.rotation = Quaternion.Euler(headlights_backward_rotation_down.x, headlights_backward_rotation_down.y, headlights_backward_rotation_down.z);
        player_spriteRenderer.sortingOrder = _line_layer;
        player_newPosition = new Vector3(transform.position.x, _line_y, transform.position.z);
    }

    public bool                     Player_Invul { get; private set; }
    private bool                    player_invul_alpha_increase = false;
    [SerializeField] private float  player_invul_alpha_delta = 12; //Чем больше значение, тем быстрее моргает
    [SerializeField] private float  player_invul_timer_init = 1.2f;
    private float                   player_invul_timer;    

    public int                      Player_Ups { get; set; }
    [SerializeField] private int    player_ups_init;

    private int                     player_kilometersLeft;
    public int                      Player_KilometersLeft 
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
    [SerializeField] private int    player_kilometersLeft_init = 25;
    
    private float                   player_kilometersLeft_delta_timer;
    [SerializeField] private float  player_kilometersLeft_delta_timer_init = 30; // Через сколько проедим километр, без учёта ускорения
    
    public int                      Player_Coins { get; set; }

    Animator                        player_animation;
    const string                    PLAYER_ANIMATION_UP = "up";
    const string                    PLAYER_ANIMATION_DOWN = "down";

    private SpriteRenderer          player_spriteRenderer;
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

    private void Awake()
    {       
        SingleOnScene = this;

        Active = true;
        Player_Invul = false;
        Player_Ups = player_ups_init;
        Player_KilometersLeft = player_kilometersLeft_init;
        player_kilometersLeft_delta_timer = player_kilometersLeft_delta_timer_init;
        player_animation = GetComponent<Animator>();
        player_spriteRenderer = GetComponent<SpriteRenderer>();
        Player_BoxCollider = GetComponent<BoxCollider2D>();

        transform.position = new Vector3(transform.position.x, LINE_2_POSITION_Y, transform.position.z);
    }

    private void FixedUpdate()
    {
        if (Active)
        {
            if (Player_Invul)
            {
                if (player_invul_alpha_increase)
                {
                    player_spriteRenderer.color += new Color(0, 0, 0, player_invul_alpha_delta * Time.deltaTime);
                    if (player_spriteRenderer.color.a >= 1)
                    {
                        player_invul_alpha_increase = false;
                    }
                }
                else
                {
                    player_spriteRenderer.color -= new Color(0, 0, 0, player_invul_alpha_delta * Time.deltaTime);
                    if (player_spriteRenderer.color.a <= 0)
                    {
                        player_invul_alpha_increase = true;
                    }
                }
                player_invul_timer -= Time.deltaTime;
                if (player_invul_timer <= 0)
                {
                    player_spriteRenderer.color += new Color(0, 0, 0, 1);
                    Player_Invul = false;
                }
            }

            if (!crashed)
            {
                player_animation.speed = 1;

                if (player_kilometersLeft_delta_timer >= 0)
                {
                    // Вычитаем из таймера с учётом ускорения
                    player_kilometersLeft_delta_timer -= Time.deltaTime * (1 + World_Local_SceneMain_MovingBackground_Entity.SingleOnScene.SpeedScale);
                } 
                else
                {
                    Player_KilometersLeft -= 1;
                    AppScreen_Local_SceneMain_UICanvas_Indicators_Complete_Entity.SingleOnScene.Show();
                    player_kilometersLeft_delta_timer = player_kilometersLeft_delta_timer_init;
                }

                if (AppScreen_Local_SceneMain_UICanvas_VirtualStick_Entity.SingleOnScene.Inner_Direction > 0)
                {
                    switch (transform.position.y)
                    {
                        case LINE_2_POSITION_Y:
                        Player_Moving_Start_Up(LINE_1_SORTINGORDER_PLAYER, LINE_1_POSITION_Y);
                        break;

                        case LINE_3_POSITION_Y:
                        Player_Moving_Start_Up(LINE_2_SORTINGORDER_PLAYER, LINE_2_POSITION_Y);
                        break;

                        case LINE_4_POSITION_Y:
                        Player_Moving_Start_Up(LINE_3_SORTINGORDER_PLAYER, LINE_3_POSITION_Y);
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
                            Player_Moving_Start_Down(LINE_2_SORTINGORDER_PLAYER, LINE_2_POSITION_Y);
                            break;

                            case LINE_2_POSITION_Y:
                            Player_Moving_Start_Down(LINE_3_SORTINGORDER_PLAYER, LINE_3_POSITION_Y);
                            break;

                            case LINE_3_POSITION_Y:
                            Player_Moving_Start_Down(LINE_4_SORTINGORDER_PLAYER, LINE_4_POSITION_Y);
                            break;
                        }
                    }
                }

                if (player_moving)
                {
                    transform.position = Vector3.MoveTowards(transform.position, player_newPosition, player_controlls);

                    if (transform.position == player_newPosition)
                    {
                        transform.position = player_newPosition; // Гаранитруем, что игрок будет в нужной точке. ВНЕЗАПНО: "transform.position == player_newPosition" и "Vector3.MoveTowards(...)" не грантируют!
                        player_moving = false;
                    }
                }
                else
                {
                    if (player_moving_end)
                    {
                        player_animation.SetBool(PLAYER_ANIMATION_UP, false);
                        player_animation.SetBool(PLAYER_ANIMATION_DOWN, false);
                        headlights_forward.transform.rotation = Quaternion.Euler(headlights_forward_rotation_straight.x, headlights_forward_rotation_straight.y, headlights_forward_rotation_straight.z);
                        headlights_backward.transform.localPosition = new Vector3(headlights_backward.transform.localPosition.x, HEADLIGHTS_BACKWARD_LOCALPOSITION_STRAIGHT_Y, headlights_backward.transform.localPosition.z);
                        headlights_backward.transform.rotation = Quaternion.Euler(headlights_backward_rotation_straight.x, headlights_backward_rotation_straight.y, headlights_backward_rotation_straight.z);

                        player_moving_end = false;
                    }
                }
            }
        }
        else
        {
            player_animation.speed = 0;
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
            player_spriteRenderer.color = Color.red;
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
