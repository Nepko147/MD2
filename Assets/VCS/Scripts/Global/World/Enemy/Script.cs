using UnityEngine;

public class World_Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private AudioClip hitSound;
    private Animator anim;
    private Rigidbody2D body;
    private BoxCollider2D[] boxColliders;
    private BoxCollider2D boxCollider1;
    private BoxCollider2D boxCollider2;
    private SpriteRenderer spriteRenderer;
    private bool isDamaged;
    private float onSpeed; 
    private Vector2 position;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("state", Random.Range(1, 6)); //Выбор "Модели" противника 
        body = GetComponent<Rigidbody2D>();
        boxColliders = GetComponents<BoxCollider2D>();
        boxCollider1 = boxColliders[0];
        boxCollider2 = boxColliders[1];
        spriteRenderer = GetComponent<SpriteRenderer>();
        isDamaged = false;
        position = body.transform.position;        
        speed = speed * (1 + (ControlPers_Globalist.Singletone.GetDifficultyScale() - 1) / 2.5f);
        onSpeed = speed;

        //Изменение порядка отображения на 2D слое и не только
        switch (position.y)
        {
            case -4.4f:
                body.name = "Enemy line " + 4;                
                spriteRenderer.sortingOrder = 13;                
                break;
            case -3.55f:
                body.name = "Enemy line " + 3;
                spriteRenderer.sortingOrder = 11;
                break;
            case -2.65f:
                body.name = "Enemy line " + 2;
                spriteRenderer.sortingOrder = 9;
                break;
            case -1.75f:
                body.name = "Enemy line " + 1;
                spriteRenderer.sortingOrder = 7;
                break;
        }
    }

    private void Update()
    {
        //Проверка на возможность игры        
        if (!ControlPers_Globalist.Singletone.canPlay())
        {
            body.linearVelocity = new Vector2(0, 0);
            return;
        }
        
        //Выход из паузы
        if (body.GetRelativeVector(body.linearVelocity).x < onSpeed)
        {
            body.linearVelocity = new Vector2(-onSpeed, 0);
        }
        //Проверка на контакт с игроком
        if ((boxCollider1.bounds.Intersects(World_Player.Singletone.GetComponent<BoxCollider2D>().bounds) || 
             boxCollider2.bounds.Intersects(World_Player.Singletone.GetComponent<BoxCollider2D>().bounds)) && !isDamaged)
        {
            ControlPers_AudioManager.Singletone.PlaySound(hitSound);
            isDamaged = true;
            World_Player.Singletone.takeDamage(1);
        }

        //Уничтожаем объект, когда он уходит за пределы экрана
        if (this.gameObject.transform.position.x <= -23.6f)
        {
            Destroy(this.gameObject);
        }
    }
 }
