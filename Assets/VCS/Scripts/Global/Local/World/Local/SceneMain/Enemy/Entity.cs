using UnityEngine;

public class World_Local_SceneMain_Enemy_Entity : MonoBehaviour
{
    public bool Active { get; set; }

    public const int LINE_1_SORTINGORDER = 80;
    public const int LINE_2_SORTINGORDER = 90;
    public const int LINE_3_SORTINGORDER = 100;
    public const int LINE_4_SORTINGORDER = 110;

    private SpriteRenderer spriteRenderer;
    [SerializeField] Texture2D normalMap;

    [SerializeField] private float  enemy_speed = 8f;
    private AudioSource             enemy_audioSource;
    private bool                    enemy_isDamaged = false;
    private PolygonCollider2D       enemy_collider;

    public void SetSortingOrder(int _currentLineNumber)
    {        
        switch (_currentLineNumber)
        {
            case 1:
                spriteRenderer.sortingOrder = LINE_1_SORTINGORDER;
            break;

            case 2:
                spriteRenderer.sortingOrder = LINE_2_SORTINGORDER;
            break;

            case 3:
                spriteRenderer.sortingOrder = LINE_3_SORTINGORDER;
            break;

            case 4:
                spriteRenderer.sortingOrder = LINE_4_SORTINGORDER;
            break;
        }
    }

    private void Awake()
    {
        Active = true;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetTexture("_BumpMap", normalMap);

        enemy_audioSource = GetComponent<AudioSource>();
        enemy_collider = GetComponent<PolygonCollider2D>();
    }

    private void FixedUpdate()
    {
        //Проверка на возможность игры        
        if (Active)
        {
            transform.position += Vector3.left * enemy_speed * World_Local_SceneMain_MovingBackground_Entity.SingleOnScene.SpeedScale;

            //Проверка на контакт с игроком
            if (enemy_collider.bounds.Intersects(World_Local_SceneMain_Player.SingleOnScene.Player_BoxCollider.bounds)
                && !World_Local_SceneMain_Player.SingleOnScene.Player_Invul
                && !enemy_isDamaged)
            {
                enemy_audioSource.Play();
                enemy_isDamaged = true;
                World_Local_SceneMain_Player.SingleOnScene.LoseUp();
            }

            //Уничтожаем объект, когда он уходит за пределы экрана
            if (transform.position.x <= -10.0f)
            {
                Destroy(gameObject);
            }           
        }        
    }
 }
