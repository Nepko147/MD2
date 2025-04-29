using UnityEngine;

public class World_Player : MonoBehaviour
{
    [SerializeField] private float controlls;
    [SerializeField] private int ups;
    public Animator anim;
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private Vector2 position;
    private Vector2 startPosition;

    public static World_Player Singletone { get; private set; }

    public int Ups { get; set; }
    public int Coins { get; set; }
    public float Complete { get; set; }

    private void Awake()
    {       
        Singletone = this;
        Ups = ups;
        Coins = ControlPers_SaveLoader.Singletone.Load("coins");
        Complete = ControlPers_SaveLoader.Singletone.Load("complete");
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();        
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = body.transform.position;                
    }

    private void FixedUpdate()
    {
        position = body.transform.position;        
        //��������� ����� ��� �������
        if (!ControlPers_Globalist.Singletone.canPlay())
        {
            body.linearVelocity = new Vector2(0, 0);
            anim.StartPlayback();
            return;
        }
        //���������� ����� �������� �������
        anim.StopPlayback();

        //������ ����������� ����������        
        Complete -= ControlPers_Globalist.Singletone.GetDifficultyScale();

        //��������� �����
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetBool("up", false);
            anim.SetBool("down", false);
            body.linearVelocity = new Vector2(body.linearVelocity.x, 0);
            return;
        }
        
        //����������� ������������� ������������
        if ((Input.GetKey(KeyCode.UpArrow) 
            || AppScreen_GeneralCanvas_VirtualStick_Entity.Singleton.Inner_Direction > 0) 
            && position.y < -1.7f)
        {
            anim.SetBool("up", true);
            anim.SetBool("down", false);
            body.linearVelocity = new Vector2(body.linearVelocity.x, controlls);
        }
        else if ((Input.GetKey(KeyCode.DownArrow) 
            || AppScreen_GeneralCanvas_VirtualStick_Entity.Singleton.Inner_Direction < 0)
            && position.y > -4.3f)
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

    //��������� ������� ����������� �� 2D ����
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
        
    //��������� ����� ��������
    public void takeDamage(int _damage)
    {
        Ups -= _damage;
        if (Ups <= 0)
        {
            ControlPers_SaveLoader.Singletone.Save((int)Complete, "complete");
            ControlPers_SaveLoader.Singletone.Save(Coins, "coins");
            ControlPers_Globalist.Singletone.EndGame();
        }
    }

    public void TakeCoin(int _number)
    {        
        Coins += _number;
    }

    public void goToSartPosition()
    {
        body.MovePosition(startPosition);
        Ups = ups;
        Complete = ControlPers_SaveLoader.Singletone.Load("complete");
    }
}
