using UnityEngine;
using UnityEngine.UI;

public class Indicators : MonoBehaviour
{
    private Rigidbody2D body;
    Vector2 startPosition;
    Vector2 awayPosition;
    private bool onScreen;
    private Text ups;
    private Text score;
    private Text hiScore;

    public static Indicators Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        body = GetComponent<Rigidbody2D>();
        startPosition = new Vector2(body.position.x, body.position.y);
        awayPosition = new Vector2(body.position.x, body.position.y - 2);
        ups = GameObject.Find("Ups").GetComponent<Text>();
        score = GameObject.Find("Score").GetComponent<Text>();
        hiScore = GameObject.Find("HiScore").GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        if (!onScreen)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPosition, 1);
            return;
        } else //Вывовдим игровой интерфейс с цЫферами
        {
            transform.position = Vector2.MoveTowards(transform.position, awayPosition, 1);
        }       
        
        //Получение данных из объекта игрока (Player)
        ups.text = Player.Instance.getUps() + " UP";
        score.text = "SCORE: " + Player.Instance.getScore();
        hiScore.text = "HI-SCORE: " + Player.Instance.getHiScore();      
    }
    public void MoveToTheScreen()
    {
        onScreen = true;
    }

    public void MoveOutTheScreen()
    {
        onScreen = false;        
    }
}
