using UnityEngine;
using UnityEngine.UI;

public abstract class AppScreen_General_UICanvas_Button_Parent : AppScreen_General_UICanvas_Parent
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

    private Sprite image_currennt_idle;
    private Sprite image_currennt_pointed;
    private Sprite image_currennt_pressed;

    public void Image_PointsRefresh()
    {
        image_min = Image_ScreenPoint_Min(image);
        image_max = Image_ScreenPoint_Max(image);
    }

    public void Image_LanguageRefresh(ControlPers_LanguageHandler.ButtonName _buttonName)
    {
        position_last = transform.position;

        var _spriteArray = ControlPers_LanguageHandler.SingleOnScene.Buttons_GetSprites(_buttonName, 3);
        
        image_currennt_idle = _spriteArray[0];
        image_currennt_pointed = _spriteArray[1];
        image_currennt_pressed = _spriteArray[2];

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

    public virtual void OnClick()
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
