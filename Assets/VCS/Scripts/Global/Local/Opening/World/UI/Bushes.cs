using UnityEngine;

public class World_UI_Bushes : MonoBehaviour
{
    public static World_UI_Bushes SingleOnScene { get; private set; }

    [SerializeField] private float bushes_speed;
    Vector2 bushes_awayPosition;    

    private void Awake()
    {
        SingleOnScene = this;
        
        bushes_awayPosition = new Vector2(transform.position.x - 35.5f, transform.position.y);
    }

    private void FixedUpdate()
    {
        //Отодвигаем кусты, после начала игры
        if (ControlScene_Entity_Menu.SingleOnScene.GameStart)
        {            
            transform.position = Vector2.MoveTowards(transform.position, bushes_awayPosition, bushes_speed);
        }
    }
}
