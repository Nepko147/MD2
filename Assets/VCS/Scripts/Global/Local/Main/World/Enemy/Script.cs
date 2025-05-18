using UnityEngine;

public class World_Enemy : MonoBehaviour
{
    public bool Active { get; set; }

    [SerializeField] private float      enemy_speed;
    [SerializeField] private AudioClip  enemy_hitSound;
    private bool                        enemy_isDamaged = false;
    private PolygonCollider2D           enemy_collider;

    private void Awake()
    {
        Active = true;

        enemy_collider = GetComponent<PolygonCollider2D>();
    }

    private void FixedUpdate()
    {
        //Проверка на возможность игры        
        if (Active)
        {
            transform.position += Vector3.left * enemy_speed * World_MovingBackground_Entity.Singletone.SpeedScale;

            //Проверка на контакт с игроком
            if (enemy_collider.bounds.Intersects(World_Player.Singletone.GetComponent<BoxCollider2D>().bounds) && !enemy_isDamaged)
            {
                ControlPers_AudioManager.Singletone.PlaySound(enemy_hitSound);
                enemy_isDamaged = true;
                World_Player.Singletone.TakeDamage(1);
            }

            //Уничтожаем объект, когда он уходит за пределы экрана
            if (transform.position.x <= -10.0f)
            {
                Destroy(gameObject);
            }           
        }        
    }
 }
