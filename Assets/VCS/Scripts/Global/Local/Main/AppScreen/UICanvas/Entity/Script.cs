using UnityEngine;
using UnityEngine.UI;

public class Main_AppScreen_UICanvas_Entity : MonoBehaviour
{
    public static Main_AppScreen_UICanvas_Entity SingleOnScene { get; private set; }
        
    private Text    ups_string_text;
    private int     ups_visual;
    public int      Ups_Visual
    {
        get 
        {
            return (ups_visual); 
        }
        set 
        {
            ups_visual = value;
            ups_string_text.text = TEXT_X + ups_visual.ToString();
        }
    }
    
    private Text    coins_string_text;
    private int     coins_visual;
    public int      Coins_Visual
    {
        get
        {
            return (coins_visual);
        }
        set
        {
            coins_visual = value;
            coins_string_text.text = coins_visual.ToString();
        }
    }

    private Text complete_string_text;
    private Text midScreenBigString_text;
    private Text midScreenSmallString_text;

    const string TEXT_DISTANCEREMAIN = "Distance remain: ";
    const string TEXT_COMPLETE = "COMPLETE: ";
    const string TEXT_GAMEOVER = "GAME OVER";
    const string TEXT_PAUSE = "PAUSE";
    const string TEXT_METERS = " m.";
    const string TEXT_X = "x";

    public void ShowGameOver()
    {        
        ups_string_text.enabled = false;
        coins_string_text.enabled = false;
        complete_string_text.enabled = false;
        midScreenBigString_text.enabled = true;
        midScreenBigString_text.text = TEXT_GAMEOVER;
        midScreenSmallString_text.enabled = true;
        midScreenSmallString_text.text = TEXT_DISTANCEREMAIN + (int)World_Player.SingleOnScene.Player_Complete + TEXT_METERS;
    }

    public void SetPause(bool _pause)
    {        
        midScreenBigString_text.enabled = _pause;
        midScreenBigString_text.text = TEXT_PAUSE;
    }

    private void Awake()
    {
        SingleOnScene = this; 
    }

    private void Start()
    {
        ups_string_text = AppScreen_UICanvas_Indicators_Ups_String.SingleOnScene.GetComponent<Text>();
        coins_string_text = AppScreen_UICanvas_Indicators_Coins_String.SingleOnScene.GetComponent<Text>();
        complete_string_text = AppScreen_UICanvas_Indicators_Complete_String.SingleOnScene.GetComponent<Text>();
        midScreenBigString_text = AppScreen_UICanvas_Indicators_MidScreen_BigString.SingleOnScene.GetComponent<Text>();
        midScreenSmallString_text = AppScreen_UICanvas_Indicators_MidScreen_SmallString.SingleOnScene.GetComponent<Text>();

        Ups_Visual = World_Player.SingleOnScene.Player_Ups;
        Coins_Visual = ControlPers_DataHandler.SingleOnScene.ProgressData_Coins;
    }

    private void FixedUpdate()
    {
        complete_string_text.text = TEXT_COMPLETE + (int)World_Player.SingleOnScene.Player_Complete + TEXT_METERS;
    }
}
