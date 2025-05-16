using UnityEngine;

public class World_BackGround_Bushes : MonoBehaviour
{
    public static World_BackGround_Bushes Singletone { get; private set; }

    [SerializeField] private float bushes_speed;
    Vector2 bushes_awayPosition;    

    private void Awake()
    {
        Singletone = this;
        
        bushes_awayPosition = new Vector2(transform.position.x - 35.5f, transform.position.y);
    }

    private void FixedUpdate()
    {
        //Отодвигаем кусты, после начала игры
        if (ControlScene_Entity_Menu.Singletone.GameStart)
        {            
            transform.position = Vector2.MoveTowards(transform.position, bushes_awayPosition, bushes_speed);
        }
    }
}
