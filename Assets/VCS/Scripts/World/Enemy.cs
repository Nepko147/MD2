using UnityEngine;

public class Enemy : MonoBehaviour
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
    private Globalist globalist;
    private Player player;    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        boxColliders = GetComponents<BoxCollider2D>();
        boxCollider1 = boxColliders[0];
        boxCollider2 = boxColliders[1];
        spriteRenderer = GetComponent<SpriteRenderer>();
        isDamaged = false;        
        onSpeed = 0;
        position = body.transform.position;
        globalist = GameObject.Find("Globalist").GetComponent<Globalist>();
        player = GameObject.Find("Player").GetComponent<Player>();
        speed = speed * (1 + (globalist.GetDifficultyScale() - 1) / 2.5f);

        //Изменение порядка отображения на 2D слое и не только
        switch (position.y)
        {
            case -4.4f:
                body.name = "Enemy line " + 4;
                onSpeed = speed;
                spriteRenderer.sortingOrder = 13;
                anim.SetInteger("state", Random.Range(1,3)); //Выбор "Модели" противника 
                break;
            case -3.55f:
                body.name = "Enemy line " + 3;
                onSpeed = speed;
                anim.SetInteger("state", Random.Range(1, 3));
                spriteRenderer.sortingOrder = 11;
                break;
            case -2.65f:
                body.name = "Enemy line " + 2;
                onSpeed = speed;
                anim.SetInteger("state", Random.Range(1, 3));
                spriteRenderer.sortingOrder = 9;
                break;
            case -1.75f:
                body.name = "Enemy line " + 1;
                onSpeed = speed;
                anim.SetInteger("state", Random.Range(1, 3));
                spriteRenderer.sortingOrder = 7;
                break;
        }
    }

    private void Update()
    {
        //Проверка на возможность игры        
        if (!globalist.canPlay())
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
        if ((boxCollider1.bounds.Intersects(player.GetComponent<BoxCollider2D>().bounds) || 
             boxCollider2.bounds.Intersects(player.GetComponent<BoxCollider2D>().bounds)) && !isDamaged)
        {
            AudioManager.Instance.PlaySound(hitSound);
            isDamaged = true;
            player.takeDamage(1);
        }

        //Уничтожаем объект, когда он уходит за пределы экрана
        if (this.gameObject.transform.position.x <= -23.6f)
        {
            Destroy(this.gameObject);
        }
    }
 }
