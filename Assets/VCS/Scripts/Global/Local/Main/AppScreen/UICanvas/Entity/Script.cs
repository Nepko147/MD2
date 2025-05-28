using UnityEngine;
using UnityEngine.UI;

public class Main_AppScreen_UICanvas_Entity : MonoBehaviour
{
    public static Main_AppScreen_UICanvas_Entity SingleOnScene { get; private set; }

    [SerializeField] GameObject ups_string;
    private Text                ups_string_text;
    private int                 ups_visual;
    public int                  Ups_Visual
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

    [SerializeField] GameObject ups_sprite;
    private Image               ups_sprite_image;

    [SerializeField] GameObject coins_string;
    private Text                coins_string_text;
    private int                 coins_visual;
    public int                  Coins_Visual
    {
        get
        {
            return (coins_visual);
        }
        set
        {
            coins_visual = value;
            coins_string_text.text = TEXT_X + coins_visual.ToString();
        }
    }

    [SerializeField] GameObject coins_sprite;
    private Image               coins_sprite_image;

    [SerializeField] GameObject complete_string;
    private Text                complete_string_text;

    [SerializeField] GameObject midScreenBigString;
    private Text                midScreenBigString_text;

    [SerializeField] GameObject midScreenSmallString;
    private Text                midScreenSmallString_text;

    const string TEXT_DISTANCEREMMAIN = "Distance ramain: ";
    const string TEXT_COMPLETE = "COMPLETE: ";
    const string TEXT_GAMEOVER = "GAME OVER";
    const string TEXT_PAUSE = "PAUSE";
    const string TEXT_METERS = " m.";
    const string TEXT_X = "x";

    public void ShowGameOver()
    {        
        ups_string_text.enabled = false;
        ups_sprite_image.enabled = false;
        coins_string_text.enabled = false;
        coins_sprite_image.enabled = false;
        complete_string_text.enabled = false;
        midScreenBigString_text.enabled = true;
        midScreenBigString_text.text = TEXT_GAMEOVER;
        midScreenSmallString_text.enabled = true;
        midScreenSmallString_text.text = TEXT_DISTANCEREMMAIN + (int)World_Player.SingleOnScene.Player_Complete + TEXT_METERS;
    }

    public void SetPause(bool _pause)
    {        
        midScreenBigString_text.enabled = _pause;
        midScreenBigString_text.text = TEXT_PAUSE;
    }

    private void Awake()
    {
        SingleOnScene = this;

        ups_string_text = ups_string.GetComponent<Text>();
        ups_sprite_image = ups_sprite.GetComponent<Image>();
        coins_string_text = coins_string.GetComponent<Text>();
        coins_sprite_image = coins_sprite.GetComponent<Image>();
        complete_string_text = complete_string.GetComponent<Text>();
        midScreenBigString_text = midScreenBigString.GetComponent<Text>();
        midScreenSmallString_text = midScreenSmallString.GetComponent<Text>();
    }

    private void Start()
    {
        Ups_Visual = World_Player.SingleOnScene.Player_Ups;
        Coins_Visual = ControlPers_DataHandler.SingleOnScene.ProgressData_Coins;
    }

    private void FixedUpdate()
    {
        complete_string_text.text = TEXT_COMPLETE + (int)World_Player.SingleOnScene.Player_Complete + TEXT_METERS;
    }
}
