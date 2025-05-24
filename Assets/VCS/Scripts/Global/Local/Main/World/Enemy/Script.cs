using UnityEngine;

public class World_Enemy : MonoBehaviour
{
    public bool Active { get; set; }

    public const int LINE_1_SORTINGORDER = 80;
    public const int LINE_2_SORTINGORDER = 100;
    public const int LINE_3_SORTINGORDER = 120;
    public const int LINE_4_SORTINGORDER = 140;

    [SerializeField] private float  enemy_speed = 8f;
    private AudioSource             enemy_audioSource;
    private bool                    enemy_isDamaged = false;
    private PolygonCollider2D       enemy_collider;

    private void Awake()
    {
        Active = true;

        enemy_audioSource = GetComponent<AudioSource>();
        enemy_collider = GetComponent<PolygonCollider2D>();
    }

    private void FixedUpdate()
    {
        //Проверка на возможность игры        
        if (Active)
        {
            transform.position += Vector3.left * enemy_speed * World_MovingBackground_Entity.SingleOnScene.SpeedScale;

            //Проверка на контакт с игроком
            if (enemy_collider.bounds.Intersects(World_Player.SingleOnScene.Player_BoxCollider.bounds) 
                && !enemy_isDamaged)
            {
                enemy_audioSource.Play();
                enemy_isDamaged = true;
                World_Player.SingleOnScene.LoseUp();
            }

            //Уничтожаем объект, когда он уходит за пределы экрана
            if (transform.position.x <= -10.0f)
            {
                Destroy(gameObject);
            }           
        }        
    }
 }
