using UnityEngine;

public class World_Player : MonoBehaviour
{
    public static World_Player SingleOnScene { get; private set; }

    public bool Active { get; set; }

    const int   LINE_1_SORTINGORDER_PLAYER = 90;
    const float LINE_1_POSITION_Y = -0.55f;

    const int   LINE_2_SORTINGORDER_PLAYER = 110;
    const float LINE_2_POSITION_Y = -0.85f;

    const int   LINE_3_SORTINGORDER_PLAYER = 130;
    const float LINE_3_POSITION_Y = -1.15f;

    const int   LINE_4_SORTINGORDER_PLAYER = 150;
    const float LINE_4_POSITION_Y = -1.45f;

    [SerializeField] private float  player_controlls;

    Vector3 player_newPosition;
    bool player_moving;

    public int                      Player_Ups { get; set; }
    [SerializeField] private int    player_ups_init;    

    public float                    Player_Complete { get; set; }
    [SerializeField] private float  player_complete_init;

    public int                      Player_Coins { get; set; }

    Animator                        player_animation;
    const string                    PLAYER_ANIMATION_UP = "up";
    const string                    PLAYER_ANIMATION_DOWN = "down";

    private SpriteRenderer          player_spriteRenderer;  
    
    private void Awake()
    {       
        SingleOnScene = this;

        Active = true;

        Player_Ups = player_ups_init;
        Player_Coins = ControlPers_DataHandler.SingleOnScene.ProgressData_Coins_Get();
        Player_Complete = player_complete_init;
        player_animation = GetComponent<Animator>();
        player_spriteRenderer = GetComponent<SpriteRenderer>();        
        transform.position = new Vector3(transform.position.x, LINE_2_POSITION_Y, transform.position.z);
    }

    private void FixedUpdate()
    {     
        if (Active)
        {
            player_animation.speed = 1;

            Player_Complete -= World_MovingBackground_Entity.SingleOnScene.SpeedScale;

            if (!player_moving)
            {
                player_animation.SetBool(PLAYER_ANIMATION_UP, false);
                player_animation.SetBool(PLAYER_ANIMATION_DOWN, false);
            }

            if (AppScreen_GeneralCanvas_VirtualStick_Entity.Singleton.Inner_Direction > 0
                && !player_moving)
            {
                player_moving = true;                
                player_animation.SetBool(PLAYER_ANIMATION_DOWN, false);
                var _y = transform.position.y;

                switch (transform.position.y)
                {
                    case LINE_1_POSITION_Y:
                        player_animation.SetBool(PLAYER_ANIMATION_UP, false);
                        player_moving = false;
                        break;
                    case LINE_2_POSITION_Y:
                        player_animation.SetBool(PLAYER_ANIMATION_UP, true);
                        player_spriteRenderer.sortingOrder = LINE_1_SORTINGORDER_PLAYER;
                        _y = LINE_1_POSITION_Y;
                        break;
                    case LINE_3_POSITION_Y:
                        player_animation.SetBool(PLAYER_ANIMATION_UP, true);
                        player_spriteRenderer.sortingOrder = LINE_2_SORTINGORDER_PLAYER;
                        _y = LINE_2_POSITION_Y;
                        break;
                    case LINE_4_POSITION_Y:
                        player_animation.SetBool(PLAYER_ANIMATION_UP, true);
                        player_spriteRenderer.sortingOrder = LINE_3_SORTINGORDER_PLAYER;
                        _y = LINE_3_POSITION_Y;
                        break;
                }

                player_newPosition = new Vector3(transform.position.x, _y , transform.position.z);
            }

            if (AppScreen_GeneralCanvas_VirtualStick_Entity.Singleton.Inner_Direction < 0
                && !player_moving)
            {
                player_moving = true;
                player_animation.SetBool(PLAYER_ANIMATION_UP, false);                
                var _y = transform.position.y;

                switch (transform.position.y)
                {
                    case LINE_1_POSITION_Y:
                        player_animation.SetBool(PLAYER_ANIMATION_DOWN, true);
                        player_spriteRenderer.sortingOrder = LINE_2_SORTINGORDER_PLAYER;
                        _y = LINE_2_POSITION_Y;
                        break;
                    case LINE_2_POSITION_Y:
                        player_animation.SetBool(PLAYER_ANIMATION_DOWN, true);
                        player_spriteRenderer.sortingOrder = LINE_3_SORTINGORDER_PLAYER;
                        _y = LINE_3_POSITION_Y;
                        break;
                    case LINE_3_POSITION_Y:
                        player_animation.SetBool(PLAYER_ANIMATION_DOWN, true);
                        player_spriteRenderer.sortingOrder = LINE_4_SORTINGORDER_PLAYER;
                        _y = LINE_4_POSITION_Y;
                        break;
                    case LINE_4_POSITION_Y:
                        player_animation.SetBool(PLAYER_ANIMATION_DOWN, false);
                        player_moving = false;
                        break;
                }

                player_newPosition = new Vector3(transform.position.x, _y, transform.position.z);
            }
            
            if (player_moving)
            {
                transform.position = Vector3.MoveTowards(transform.position, player_newPosition, player_controlls);

                if (transform.position == player_newPosition)
                {                    
                    transform.position = player_newPosition; // √аранитруем, что игрок будет в нужной точке. ¬Ќ≈«јѕЌќ: "transform.position == player_newPosition" и "Vector3.MoveTowards(...)" не грантируют!
                    player_moving = false;
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
        Main_AppScreen_UICanvas_Entity.SingleOnScene.Ups_Visual -= 1;
    }

    public void TakeUp()
    {
        ++Player_Ups;
    }

    public void TakeCoin()
    {
        ++Player_Coins;
    }
}
