using UnityEngine;

public class World_Local_SceneMain_Bonus_Parent : MonoBehaviour
{
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
    
    [SerializeField] protected World_Local_SceneMain_PopUp_Entity popUp;

    [SerializeField] protected AudioClip sound;

    protected Animator animator;

    protected BoxCollider2D boxCollider;

    private const float SPEED = 500f;

    protected float destroy_position_x = -5.0f; //Позиция уничтожения объекта за пределами экрана

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();

        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        if (active) 
        {
            transform.position += Vector3.left * SPEED * World_General_MovingBackground_Entity.SingleOnScene.SpeedScale * Time.deltaTime;

            if (transform.position.x <= destroy_position_x)
            {
                Destroy(gameObject);
            }
        }
    }
}
