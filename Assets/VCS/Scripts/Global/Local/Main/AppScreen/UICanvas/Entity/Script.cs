using UnityEngine;

public class Main_AppScreen_UICanvas_Entity : MonoBehaviour
{
    public static Main_AppScreen_UICanvas_Entity SingleOnScene { get; private set; }

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
            var _string = TEXT_X + ups_visual.ToString();
            AppScreen_UICanvas_Indicators_Ups_String.SingleOnScene.UpdateText(_string);
        }
    }
        
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
            var _string = TEXT_X + coins_visual.ToString();
            AppScreen_UICanvas_Indicators_Coins_String.SingleOnScene.UpdateText(_string);
        }
    }

    const string TEXT_DISTANCEREMAIN = "Distance ramain: ";
    const string TEXT_COMPLETE = "COMPLETE: ";
    const string TEXT_GAMEOVER = "GAME OVER";
    const string TEXT_PAUSE = "PAUSE";
    const string TEXT_METERS = " m.";
    const string TEXT_X = "x";

    public void ShowGameOver()
    {
        AppScreen_UICanvas_Indicators_Ups_String.SingleOnScene.Enable(false);
        AppScreen_UICanvas_Indicators_Coins_String.SingleOnScene.Enable(false);
        AppScreen_UICanvas_Indicators_Complete_String.SingleOnScene.Enable(false);
        AppScreen_UICanvas_Indicators_MidScreen_BigString.SingleOnScene.Enable(true);
        AppScreen_UICanvas_Indicators_MidScreen_BigString.SingleOnScene.UpdateText(TEXT_GAMEOVER);
        AppScreen_UICanvas_Indicators_MidScreen_SmallString.SingleOnScene.Enable(true);
        var _string = TEXT_DISTANCEREMAIN + (int)World_Player.SingleOnScene.Player_Complete + TEXT_METERS;
        AppScreen_UICanvas_Indicators_MidScreen_SmallString.SingleOnScene.UpdateText(_string);
    }

    public void SetPause(bool _pause)
    {
        AppScreen_UICanvas_Indicators_MidScreen_BigString.SingleOnScene.Enable(_pause);
        AppScreen_UICanvas_Indicators_MidScreen_BigString.SingleOnScene.UpdateText(TEXT_PAUSE);
    }

    private void Awake()
    {
        SingleOnScene = this; 
    }

    private void Start()
    {
        Ups_Visual = World_Player.SingleOnScene.Player_Ups;
        Coins_Visual = ControlPers_DataHandler.SingleOnScene.ProgressData_Coins;
    }

    private void FixedUpdate()
    {
        var _string = TEXT_COMPLETE + (int)World_Player.SingleOnScene.Player_Complete + TEXT_METERS;
        AppScreen_UICanvas_Indicators_Complete_String.SingleOnScene.UpdateText(_string);
    }
}
