using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float controlls;
    [SerializeField] private int ups;
    public Animator anim;
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;    
    private float score;
    private int hiScore;
    private Vector2 position;
    private Vector2 startPosition;

    public static Player Instance { get; private set; }

    public int Ups { get; set; }

    private void Awake()
    {       
        Instance = this;
        Ups = ups;
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();        
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = body.transform.position;                
    }

    private void FixedUpdate()
    {
        position = body.transform.position;        
        //Включение паузы для объекта
        if (!Globalist.Instance.canPlay())
        {
            body.linearVelocity = new Vector2(0, 0);
            anim.StartPlayback();
            return;
        }
        //Выключение паузы анимации объекта
        anim.StopPlayback();
        //Начисление очков
        score += Globalist.Instance.GetDifficultyScale();          
        //Обработка ввода
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetBool("up", false);
            anim.SetBool("down", false);
            body.linearVelocity = new Vector2(body.linearVelocity.x, 0);
            return;
        }
        
        //Ограничение вертикального передвижения
        if (Input.GetKey(KeyCode.UpArrow) && position.y < -1.7f)
        {
            anim.SetBool("up", true);
            anim.SetBool("down", false);
            body.linearVelocity = new Vector2(body.linearVelocity.x, controlls);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && position.y > -4.3f)
        {
            anim.SetBool("up", false);
            anim.SetBool("down", true);
            body.linearVelocity = new Vector2(body.linearVelocity.x, -controlls);
        }
        else
        {
            anim.SetBool("up", false);
            anim.SetBool("down", false);
            body.linearVelocity = new Vector2(body.linearVelocity.x, 0);
        }        
        UpdateOrder(position.y);           
    }

    //Изменение порядка отображения на 2D слое
    private void UpdateOrder(float y)
    {
        switch (y)
        {
            case < -4.4f:
                spriteRenderer.sortingOrder = 14;
                break;
            case < -3.55f:
                spriteRenderer.sortingOrder = 12;
                break;
            case < -2.65f:
                spriteRenderer.sortingOrder = 10;
                break;
            case < -1.75f:
                spriteRenderer.sortingOrder = 8;
                break;
            case > -1.75f:
                spriteRenderer.sortingOrder = 6;
                break;
        }
    }

    //Выдача кол-ва жизней другим объектамм    
    public int getUps()
    {
        return Ups;
    }

    //Выдача кол-ва очков другим объектамм    
    public int getScore()
    {
        return (int)score;
    }
    public int getHiScore()
    {
        return hiScore;
    }
    //Получение урона объектом
    public void takeDamage(int _damage)
    {
        Ups -= _damage;
        if (Ups <= 0)
        {
            if ((int)score > SaveLoader.Instance.Load("hiscore"))
            {
                hiScore = (int)score;
                SaveLoader.Instance.Save(hiScore, "hiscore");
            }
            Globalist.Instance.EndGame();
        }
    }

    public void takePoints(float _points)
    {
        score += _points;
    }

    public void goToSartPosition()
    {
        body.MovePosition(startPosition);
        Ups = ups;
        score = 0;
        hiScore = SaveLoader.Instance.Load("hiscore");
    }
}
