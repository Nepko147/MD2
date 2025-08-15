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
            AppScreen_Local_SceneMain_UICanvas_Indicators_Ups_Text.SingleOnScene.UpdateText(_string);
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
            AppScreen_Local_SceneMain_UICanvas_Indicators_Coins_Text.SingleOnScene.UpdateText(_string);
        }
    }

    const string    TEXT_GAMEOVER_MAIN_EN = "GAME OVER";
    const string    TEXT_GAMEOVER_MAIN_RU = " ŒÕ≈÷ »√–€";
    private string  text_gameOver_main_current;

    const string    TEXT_GAMEOVER_DISTANCEREMAIN_PREF_EN = "Distance remain: ";
    const string    TEXT_GAMEOVER_DISTANCEREMAIN_PREF_RU = "ŒÒÚ‡ÎÓÒ¸ ‰Ó ˆÂÎË: ";
    private string text_gameover_distanceremain_pref_current;

    const string    TEXT_GAMEOVER_DISTANCEREMAIN_SUFF = " KM";    

    const string    TEXT_PAUSE_EN = "PAUSE";
    const string    TEXT_PAUSE_RU = "œ¿”«¿";
    private string  text_pause_current;

    private const string TEXT_X = "x";

    public void ShowGameOver()
    {
        AppScreen_Local_SceneMain_UICanvas_Indicators_MidScreen_BigString.SingleOnScene.Enable(true);
        AppScreen_Local_SceneMain_UICanvas_Indicators_MidScreen_BigString.SingleOnScene.UpdateText(text_gameOver_main_current);
        AppScreen_Local_SceneMain_UICanvas_Indicators_MidScreen_SmallString.SingleOnScene.Enable(true);
        AppScreen_Local_SceneMain_UICanvas_Indicators_MidScreen_SmallString.SingleOnScene.UpdateText(text_gameover_distanceremain_pref_current + AppScreen_Local_SceneMain_UICanvas_Indicators_Complete_Entity.SingleOnScene.Text_Number + TEXT_GAMEOVER_DISTANCEREMAIN_SUFF);
    }

    public void SetPause(bool _pause)
    {
        AppScreen_Local_SceneMain_UICanvas_Indicators_MidScreen_BigString.SingleOnScene.Enable(_pause);
        AppScreen_Local_SceneMain_UICanvas_Indicators_MidScreen_BigString.SingleOnScene.UpdateText(text_pause_current);
    }

    private void Awake()
    {
        SingleOnScene = this; 
    }

    private void Start()
    {
        Ups_Visual = World_Local_SceneMain_Player_Entity.SingleOnScene.Up_Count;
        Coins_Visual = ControlPers_DataHandler.SingleOnScene.ProgressData_Coins;

        switch (ControlPers_LanguageHandler.SingleOnScene.GameLanguage_Current)
        {
            case ControlPers_LanguageHandler.GameLanguage.english:
                text_gameOver_main_current = TEXT_GAMEOVER_MAIN_EN;
                text_gameover_distanceremain_pref_current = TEXT_GAMEOVER_DISTANCEREMAIN_PREF_EN;
                text_pause_current = TEXT_PAUSE_EN;
            break;

            case ControlPers_LanguageHandler.GameLanguage.russian:
                text_gameOver_main_current = TEXT_GAMEOVER_MAIN_RU;
                text_gameover_distanceremain_pref_current = TEXT_GAMEOVER_DISTANCEREMAIN_PREF_RU;
                text_pause_current = TEXT_PAUSE_RU;
            break;
        }
    }
}
