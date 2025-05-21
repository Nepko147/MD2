using UnityEngine;

public class World_UI_Bushes : MonoBehaviour
{
    public static World_UI_Bushes SingleOnScene { get; private set; }

    public bool GameStart { get; set; }

    [SerializeField] private float bushes_speed;
    Vector2 bushes_awayPosition;    

    private void Awake()
    {
        SingleOnScene = this;

        GameStart = false;
        bushes_awayPosition = new Vector2(transform.position.x - 35.5f, transform.position.y);
    }

    private void FixedUpdate()
    {
        //Отодвигаем кусты, после начала игры
        if (GameStart)
        {            
            transform.position = Vector2.MoveTowards(transform.position, bushes_awayPosition, bushes_speed);
        }
    }
}
