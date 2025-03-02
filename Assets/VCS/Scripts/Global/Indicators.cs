using UnityEngine;
using UnityEngine.UI;

public class Indicators : MonoBehaviour
{
    Canvas canvas;
    private Text ups;
    private Text score;
    private Text hiScore;
    private Text midScreenText;
    private Text midScreenSubText;

    public static Indicators Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        ups = GameObject.Find("Ups").GetComponent<Text>();
        score = GameObject.Find("Score").GetComponent<Text>();
        hiScore = GameObject.Find("HiScore").GetComponent<Text>();
        midScreenText = GameObject.Find("MidScreenText").GetComponent<Text>();
        midScreenSubText = GameObject.Find("MidScreenSubText").GetComponent<Text>();
    }

    private void FixedUpdate()
    {        
        if (!Globalist.Instance.canPlay())
        {
            return;
        }
        //Получение данных из объекта игрока (Player)
        ups.text = Player.Instance.getUps() + " UP";
        score.text = "SCORE: " + Player.Instance.getScore();
        hiScore.text = "HI-SCORE: " + Player.Instance.getHiScore();      
    }
    public void MoveToTheScreen()
    {
        canvas.enabled = true;
    }

    public void MoveOutTheScreen()
    {
        canvas.enabled = false;
    }

    public void ShowGameOver()
    {
        ups.text = "";
        score.text = "";
        hiScore.text = "";
        midScreenText.text = "GAME OVER";
        midScreenSubText.text = "SCORE: " + Player.Instance.getScore();
    }

    public void PrepareToStart()
    {
        midScreenText.text = "";
        midScreenSubText.text = "";
    }

    public void SetPause(bool _pause)
    {        
        midScreenText.text = _pause ? "PAUSE" : "";
        midScreenSubText.fontSize = _pause ? 30 : 64;
        midScreenSubText.text = _pause ? "[Backspace] to resume" : "";        
    }
}
