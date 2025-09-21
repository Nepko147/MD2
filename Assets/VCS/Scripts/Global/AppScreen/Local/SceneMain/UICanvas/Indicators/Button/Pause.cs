using UnityEngine;
using UnityEngine.UI;

public class AppScreen_Local_SceneMain_UICanvas_Indicators_Button_Pause : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_Local_SceneMain_UICanvas_Indicators_Button_Pause SingleOnScene { get; private set; }

    private bool pressed = false;
    public bool Pressed
    {
        get
        {
            return (pressed);
        }
        set
        {
            pressed = value;

            if (pressed)
            {
                image.sprite = image_pressed;
            }
        }
    }

    private Image image;
    [SerializeField] private Sprite image_idle;
    [SerializeField] private Sprite image_pointed;
    [SerializeField] private Sprite image_pressed;

    [SerializeField] private AudioClip sound_press;

    public bool Visible
    {
        get { return (image.enabled); }
        set { image.enabled = value; }
    }

    public void OnClick()
    {
        ControlPers_AudioMixer_Sounds.SingleOnScene.Play(sound_press);

        Pressed = true;
    }
    public void SetAlpha(float _newAlpha)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, _newAlpha);
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        image = GetComponent<Image>();
    }

    private void Update()
    {
        Image_Highlight_Behaviour(image);

        if (Input.GetKeyDown(KeyCode.Escape)
        || Input.GetKeyDown(KeyCode.P)
        || Input.GetKeyDown(KeyCode.Backspace))
        {
            Pressed = true;
        }
    }
}
