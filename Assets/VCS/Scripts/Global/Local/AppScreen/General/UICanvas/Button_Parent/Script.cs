using UnityEngine;
using UnityEngine.UI;

public class AppScreen_General_UICanvas_Button_Parent : AppScreen_General_UICanvas_Parent
{
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
                image.sprite = image_currennt_pressed;
            }
        }
    }

    private Image image;
    private Vector2 image_min;
    private Vector2 image_max;
    [SerializeField] private Sprite image_idle;
    [SerializeField] private Sprite image_pointed;
    [SerializeField] private Sprite image_pressed;

    [SerializeField] private Sprite image_ru_idle;
    [SerializeField] private Sprite image_ru_pointed;
    [SerializeField] private Sprite image_ru_pressed;

    private Sprite image_currennt_idle;
    private Sprite image_currennt_pointed;
    private Sprite image_currennt_pressed;

    private void Image_PointsRefresh()
    {
        image_min = Image_ScreenPoint_Min(image);
        image_max = Image_ScreenPoint_Max(image);
    }

    public void Image_LanguageRefresh()
    {
        position_last = transform.position;
        switch (ControlPers_LanguageHandler.SingleOnScene.GameLanguage_Current)
        {
            case ControlPers_LanguageHandler.GameLanguage.english:
                image_currennt_idle = image_idle;
                image_currennt_pointed = image_pointed;
                image_currennt_pressed = image_pressed;
                break;

            case ControlPers_LanguageHandler.GameLanguage.russian:
                image_currennt_idle = image_ru_idle;
                image_currennt_pointed = image_ru_pointed;
                image_currennt_pressed = image_ru_pressed;
                break;
        }

        rectTransform.sizeDelta = new Vector2(image_currennt_idle.rect.width, image_currennt_idle.rect.height);
        Image_PointsRefresh();
        transform.position = position_last;
    }

    [SerializeField] private AudioClip sound_press;

    private Vector3 position_last;

    public bool Visible
    {
        get { return (image.enabled); }
        set { image.enabled = value; }
    }

    protected bool ActiveOnPopUpMessage = false;

    private bool Pressable()
    {
        if (pressed)
        {
            return (false);
        }
        else
        {
            if (ActiveOnPopUpMessage)
            {
                return (true);
            }
            else
            {
                if (AppScreen_General_UICanvas_Entity.SingleOnScene.PopUpMessage_IsActive)
                {
                    return (false);
                }
                else
                {
                    return (true);
                }
            }
        }
    }

    public void OnClick()
    {
        if (Pressable())
        {
            ControlPers_AudioMixer_Sounds.SingleOnScene.Play(sound_press);

            Pressed = true;
        }
    }

    public void SetAlpha(float _newAlpha)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, _newAlpha);
    }

    protected override void Awake()
    {
        base.Awake();

        image = GetComponent<Image>();
    }

    protected virtual void Start()
    {
        Image_LanguageRefresh();
    }

    private void Update()
    {
        if (Pressable())
        {
            if (transform.position != position_last)
            {
                Image_PointsRefresh();

                position_last = transform.position;
            }

            if (!Pointed(image_min, image_max))
            {
                if (image.sprite != image_currennt_idle)
                {
                    image.sprite = image_currennt_idle;
                }
            }
            else
            {
                if (image.sprite != image_currennt_pointed)
                {
                    image.sprite = image_currennt_pointed;
                }
            }
        }
    }
}
