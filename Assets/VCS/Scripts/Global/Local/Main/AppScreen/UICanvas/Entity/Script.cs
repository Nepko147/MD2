using UnityEngine;
using UnityEngine.UI;

public class Main_AppScreen_UICanvas_Entity : MonoBehaviour
{
    public static Main_AppScreen_UICanvas_Entity Singletone { get; private set; }

    public Camera AppScreen_Canvas_Camera { get; private set; }
    
    [SerializeField] GameObject appScreen_canvas_ups_string;
    private Text                appScreen_canvas_ups_string_text;

    [SerializeField] GameObject appScreen_canvas_ups_sprite;
    private Image               appScreen_canvas_ups_sprite_image;

    [SerializeField] GameObject appScreen_canvas_coins_string;
    private Text                appScreen_canvas_coins_string_text;

    [SerializeField] GameObject appScreen_canvas_coins_sprite;
    private Image               appScreen_canvas_coins_sprite_image;

    [SerializeField] GameObject appScreen_canvas_complete_string;
    private Text                appScreen_canvas_complete_string_text;

    [SerializeField] GameObject appScreen_canvas_midScreenBigString;
    private Text                appScreen_canvas_midScreenBigString_text;

    [SerializeField] GameObject appScreen_canvas_midScreenSmallString;
    private Text                appScreen_canvas_midScreenSmallString_text;

    const string APPSCREEN_CANVAS_TEXT_DISTANCEREMMAIN = "Distance ramain: ";
    const string APPSCREEN_CANVAS_TEXT_COMPLETE = "COMPLETE: ";
    const string APPSCREEN_CANVAS_TEXT_GAMEOVER = "GAME OVER";
    const string APPSCREEN_CANVAS_TEXT_PAUSE = "PAUSE";
    const string APPSCREEN_CANVAS_TEXT_METERS = " m.";
    const string APPSCREEN_CANVAS_TEXT_X = "x";

    public void ShowGameOver()
    {        
        appScreen_canvas_ups_string_text.enabled = false;
        appScreen_canvas_ups_sprite_image.enabled = false;
        appScreen_canvas_coins_string_text.enabled = false;
        appScreen_canvas_coins_sprite_image.enabled = false;
        appScreen_canvas_complete_string_text.enabled = false;
        appScreen_canvas_midScreenBigString_text.enabled = true;
        appScreen_canvas_midScreenBigString_text.text = APPSCREEN_CANVAS_TEXT_GAMEOVER;
        appScreen_canvas_midScreenSmallString_text.enabled = true;
        appScreen_canvas_midScreenSmallString_text.text = APPSCREEN_CANVAS_TEXT_DISTANCEREMMAIN + (int)World_Player.Singletone.Player_Complete + APPSCREEN_CANVAS_TEXT_METERS;
    }

    public void SetPause(bool _pause)
    {        
        appScreen_canvas_midScreenBigString_text.enabled = _pause;
        appScreen_canvas_midScreenBigString_text.text = APPSCREEN_CANVAS_TEXT_PAUSE;
    }

    private void Awake()
    {
        Singletone = this;

        AppScreen_Canvas_Camera = GetComponent<Canvas>().worldCamera;
        appScreen_canvas_ups_string_text = appScreen_canvas_ups_string.GetComponent<Text>();
        appScreen_canvas_ups_sprite_image = appScreen_canvas_ups_sprite.GetComponent<Image>();
        appScreen_canvas_coins_string_text = appScreen_canvas_coins_string.GetComponent<Text>();
        appScreen_canvas_coins_sprite_image = appScreen_canvas_coins_sprite.GetComponent<Image>();
        appScreen_canvas_complete_string_text = appScreen_canvas_complete_string.GetComponent<Text>();
        appScreen_canvas_midScreenBigString_text = appScreen_canvas_midScreenBigString.GetComponent<Text>();
        appScreen_canvas_midScreenSmallString_text = appScreen_canvas_midScreenSmallString.GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        //Получение данных из объекта игрока (World_Player)
        appScreen_canvas_ups_string_text.text = APPSCREEN_CANVAS_TEXT_X + World_Player.Singletone.Player_Ups;
        appScreen_canvas_coins_string_text.text = APPSCREEN_CANVAS_TEXT_X + World_Player.Singletone.Plyer_Coins;
        appScreen_canvas_complete_string_text.text = APPSCREEN_CANVAS_TEXT_COMPLETE + (int)World_Player.Singletone.Player_Complete + APPSCREEN_CANVAS_TEXT_METERS;
    }
}
