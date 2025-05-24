using UnityEngine;

public class World_Player : MonoBehaviour
{
    public static World_Player SingleOnScene { get; private set; }

    public bool Active { get; set; }

    public const int LINE_1_SORTINGORDER_PLAYER = 90;
    public const int LINE_2_SORTINGORDER_PLAYER = 110;
    public const int LINE_3_SORTINGORDER_PLAYER = 130;
    public const int LINE_4_SORTINGORDER_PLAYER = 150;

    [SerializeField] private float  player_controlls;

    public int                      Player_Ups { get; set; }
    [SerializeField] private int    player_ups_init;    

    public float                    Player_Complete { get; set; }
    [SerializeField] private float  player_complete_init;

    public int                      Player_Coins { get; set; }

    Animator                        player_animation;
    const string                    PLAYER_ANIMATION_UP = "up";
    const string                    PLAYER_ANIMATION_DOWN = "down";

    private SpriteRenderer          player_spriteRenderer;
    public BoxCollider2D            Player_BoxCollider { get; private set; }

    private float border_top = -0.55f; //Временно
    private float border_bot = -1.45f; //Временно

    private void Awake()
    {       
        SingleOnScene = this;

        Active = true;

        Player_Ups = player_ups_init;
        Player_Coins = ControlPers_DataHandler.SingleOnScene.ProgressData_Coins_Get();
        Player_Complete = player_complete_init;
        player_animation = GetComponent<Animator>();
        player_spriteRenderer = GetComponent<SpriteRenderer>();
        Player_BoxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {    
        if (Active)
        {
            player_animation.speed = 1;

            Player_Complete -= World_MovingBackground_Entity.SingleOnScene.SpeedScale;                    
            
            if ((Input.GetKey(KeyCode.UpArrow) 
                || AppScreen_GeneralCanvas_VirtualStick_Entity.Singleton.Inner_Direction > 0) 
                && transform.position.y < border_top)
            {
                player_animation.SetBool(PLAYER_ANIMATION_UP, true);
                player_animation.SetBool(PLAYER_ANIMATION_DOWN, false);
                transform.position += Vector3.up * player_controlls;                
            }
            else if ((Input.GetKey(KeyCode.DownArrow) 
                || AppScreen_GeneralCanvas_VirtualStick_Entity.Singleton.Inner_Direction < 0)
                && transform.position.y > border_bot)
            {
                player_animation.SetBool(PLAYER_ANIMATION_UP, false);
                player_animation.SetBool(PLAYER_ANIMATION_DOWN, true);
                transform.position -= Vector3.up * player_controlls;
            }
            else
            {
                player_animation.SetBool(PLAYER_ANIMATION_UP, false);
                player_animation.SetBool(PLAYER_ANIMATION_DOWN, false);
            }
            
            switch (transform.position.y) // ВРЕМЕННО
            {
                case < -1.45f:
                    player_spriteRenderer.sortingOrder = LINE_4_SORTINGORDER_PLAYER;
                    break;
                case < -1.15f:
                    player_spriteRenderer.sortingOrder = LINE_3_SORTINGORDER_PLAYER;
                    break;
                case < -0.85f:
                    player_spriteRenderer.sortingOrder = LINE_2_SORTINGORDER_PLAYER;
                    break;
                case < -0.6f:
                    player_spriteRenderer.sortingOrder = LINE_1_SORTINGORDER_PLAYER;
                    break;
            }

            if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                player_animation.SetBool(PLAYER_ANIMATION_UP, false);
                player_animation.SetBool(PLAYER_ANIMATION_DOWN, false);
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
