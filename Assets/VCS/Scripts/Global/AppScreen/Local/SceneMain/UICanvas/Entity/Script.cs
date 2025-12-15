using UnityEngine;

public class AppScreen_Local_SceneMain_UICanvas_Entity : MonoBehaviour
{
    public static AppScreen_Local_SceneMain_UICanvas_Entity SingleOnScene { get; private set; }

    private int ups_visual;
    public int Ups_Visual
    {
        get 
        {
            return (ups_visual); 
        }
        set 
        {
            ups_visual = value;
            var _string = TEXT_X + " " + ups_visual.ToString();
            AppScreen_Local_SceneMain_UICanvas_Indicators_Ups_Text.SingleOnScene.Text = _string;
        }
    }
        
    private int coins_visual;
    public int Coins_Visual
    {
        get
        {
            return (coins_visual);
        }
        set
        {
            coins_visual = value;

            var _string = coins_visual.ToString() + " " + TEXT_X;
            AppScreen_Local_SceneMain_UICanvas_Indicators_Coins_Text.SingleOnScene.Text = _string;
        }
    }

    private const string    TEXT_GAMEOVER_DISTANCEREMAIN_SUFF = " KM"; 
    private const string    TEXT_X = "x";

    public void ShowGameOver()
    {
        AppScreen_Local_SceneMain_UICanvas_MidScreen_BigString.SingleOnScene.Enable(true);
        AppScreen_Local_SceneMain_UICanvas_MidScreen_SmallString.SingleOnScene.Enable(true);

        var _text_gameOver = ControlPers_LanguageHandler_Entity.SingleOnScene.Text_Get(ControlPers_LanguageHandler_Entity.Text_Key.midscreen_gameOver);
        AppScreen_Local_SceneMain_UICanvas_MidScreen_BigString.SingleOnScene.UpdateText(_text_gameOver);
        
        var _text_distanceRemain = ControlPers_LanguageHandler_Entity.SingleOnScene.Text_Get(ControlPers_LanguageHandler_Entity.Text_Key.midscreen_distanceRemain) 
            + ": "
            + AppScreen_Local_SceneMain_UICanvas_Indicators_Complete_Entity.SingleOnScene.Text_Number 
            + TEXT_GAMEOVER_DISTANCEREMAIN_SUFF;
        AppScreen_Local_SceneMain_UICanvas_MidScreen_SmallString.SingleOnScene.UpdateText(_text_distanceRemain);
    }

    public void SetPause(bool _pause)
    {
        AppScreen_Local_SceneMain_UICanvas_MidScreen_BigString.SingleOnScene.Enable(_pause);
        var _text_pause = ControlPers_LanguageHandler_Entity.SingleOnScene.Text_Get(ControlPers_LanguageHandler_Entity.Text_Key.midscreen_pause);
        AppScreen_Local_SceneMain_UICanvas_MidScreen_BigString.SingleOnScene.UpdateText(_text_pause);
    }

    private void Awake()
    {
        SingleOnScene = this; 
    }

    private void Start()
    {
        Ups_Visual = World_Local_SceneMain_Player_Entity.SingleOnScene.Up_Count;
        Coins_Visual = ControlPers_DataHandler.SingleOnScene.ProgressData_Coins;
    }
}
