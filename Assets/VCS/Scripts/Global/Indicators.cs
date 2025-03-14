using UnityEngine;
using UnityEngine.UI;

public class Indicators : MonoBehaviour
{
    Canvas canvas;
    private Text ups;
    private Image upsIco;
    private Text coins;
    private Image coinsIco;
    private Text complete;
    private Text midScreenText;
    private Text midScreenSubText;

    public static Indicators Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        ups = GameObject.Find("Ups").GetComponent<Text>();
        upsIco = GameObject.Find("UpsIco").GetComponent<Image>();
        coins = GameObject.Find("Coins").GetComponent<Text>();
        coinsIco = GameObject.Find("CoinsIco").GetComponent<Image>();
        complete = GameObject.Find("Complete").GetComponent<Text>();
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
        ups.text = "x" + Player.Instance.Ups;
        coins.text = "x" + Player.Instance.Coins;
        complete.text = "COMPLETE: " + (int)Player.Instance.Complete + " m.";      
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
        upsIco.enabled = false;
        coins.text = "";
        coinsIco.enabled = false;
        complete.text = "";
        midScreenText.text = "GAME OVER";
        midScreenSubText.text = "Distance ramain: " + (int)Player.Instance.Complete + " m.";
    }

    public void PrepareToStart()
    {
        upsIco.enabled = true;
        coinsIco.enabled = true;
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
