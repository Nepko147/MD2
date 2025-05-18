using UnityEngine;

public class World_Player : MonoBehaviour
{
    public static World_Player Singletone { get; private set; }

    public bool Active { get; set; }

    [SerializeField] private float  player_controlls;

    public int                      Player_Ups { get; set; }
    [SerializeField] private int    player_ups_init;    

    public float                    Player_Complete { get; set; }
    [SerializeField] private float  player_complete_init;

    public int                      Plyer_Coins { get; set; }

    Animator                        player_animation;
    const string                    PLAYER_ANIMATION_UP = "up";
    const string                    PLAYER_ANIMATION_DOWN = "down";

    private SpriteRenderer          player_spriteRenderer;

    private void Awake()
    {       
        Singletone = this;

        Active = true;

        Player_Ups = player_ups_init;
        Plyer_Coins = ControlPers_DataHandler.Singletone.ProgressData_Coins_Get();
        Player_Complete = player_complete_init;
        player_animation = GetComponent<Animator>();
        player_spriteRenderer = GetComponent<SpriteRenderer>();       
    }

    private void FixedUpdate()
    {    
        if (Active)
        {
            player_animation.speed = 1;

            Player_Complete -= World_MovingBackground_Entity.Singletone.SpeedScale;                    
            
            if ((Input.GetKey(KeyCode.UpArrow) 
                || AppScreen_GeneralCanvas_VirtualStick_Entity.Singleton.Inner_Direction > 0) 
                && transform.position.y < ControlScene_Entity_Main.Singletone.SpawnPoint_Line_1.y)
            {
                player_animation.SetBool(PLAYER_ANIMATION_UP, true);
                player_animation.SetBool(PLAYER_ANIMATION_DOWN, false);
                transform.position += Vector3.up * player_controlls;                
            }
            else if ((Input.GetKey(KeyCode.DownArrow) 
                || AppScreen_GeneralCanvas_VirtualStick_Entity.Singleton.Inner_Direction < 0)
                && transform.position.y > ControlScene_Entity_Main.Singletone.SpawnPoint_Line_4.y)
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
                    player_spriteRenderer.sortingOrder = ControlScene_Entity_Main.LINE_4_SORTINGORDER_PLAYER;
                    break;
                case < -1.15f:
                    player_spriteRenderer.sortingOrder = ControlScene_Entity_Main.LINE_3_SORTINGORDER_PLAYER;
                    break;
                case < -0.85f:
                    player_spriteRenderer.sortingOrder = ControlScene_Entity_Main.LINE_2_SORTINGORDER_PLAYER;
                    break;
                case < -0.6f:
                    player_spriteRenderer.sortingOrder = ControlScene_Entity_Main.LINE_1_SORTINGORDER_PLAYER;
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
        
    //Получение урона объектом
    public void TakeDamage(int _damage)
    {
        Player_Ups -= _damage;
    }

    public void TakeCoin()
    {
        ++Plyer_Coins;
    }
}
