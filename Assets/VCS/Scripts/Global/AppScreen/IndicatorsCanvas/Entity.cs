using UnityEngine;
using UnityEngine.UI;

public class UI_IndicatorsCanvas_Entity : MonoBehaviour
{
    [SerializeField] GameObject upsUI;
    [SerializeField] GameObject upsIcoUI;
    [SerializeField] GameObject coinsUI;
    [SerializeField] GameObject coinsIcoUI;
    [SerializeField] GameObject completeUI;
    [SerializeField] GameObject midScreenTextUI;
    [SerializeField] GameObject midScreenSubTextUI;
    
    Canvas canvas;
    private Text ups;
    private Image upsIco;
    private Text coins;
    private Image coinsIco;
    private Text complete;
    private Text midScreenText;
    private Text midScreenSubText;

    public static UI_IndicatorsCanvas_Entity Singletone { get; private set; }

    private void Awake()
    {
        Singletone = this;
        canvas = GetComponent<Canvas>();
        ups = upsUI.GetComponent<Text>();
        upsIco = upsIcoUI.GetComponent<Image>();
        coins = coinsUI.GetComponent<Text>();
        coinsIco = coinsIcoUI.GetComponent<Image>();
        complete = completeUI.GetComponent<Text>();
        midScreenText = midScreenTextUI.GetComponent<Text>();
        midScreenSubText = midScreenSubTextUI.GetComponent<Text>();
        PrepareToStart();
    }

    private void FixedUpdate()
    {        
        if (!ControlPers_Globalist.Singletone.canPlay())
        {
            return;
        }
        //Получение данных из объекта игрока (World_Player)
        ups.text = "x" + World_Player.Singletone.Ups;
        coins.text = "x" + World_Player.Singletone.Coins;
        complete.text = "COMPLETE: " + (int)World_Player.Singletone.Complete + " m.";      
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
        midScreenSubText.text = "Distance ramain: " + (int)World_Player.Singletone.Complete + " m.";
        midScreenSubText.fontSize = 48;
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
        midScreenSubText.fontSize = 30;      
    }
}
