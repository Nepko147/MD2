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

            if (value)
            {
                image.sprite = image_pressed;
            }
        }
    }

    private Image image;
    private Vector2 image_min;
    private Vector2 image_max;
    [SerializeField] private Sprite image_idle;
    [SerializeField] private Sprite image_pointed;
    [SerializeField] private Sprite image_pressed;
    private void Image_PointsRefresh()
    {
        image_min = Image_ScreenPoint_Min(image);
        image_max = Image_ScreenPoint_Max(image);
    }

    [SerializeField] private AudioClip sound_press;

    private Vector3 position_last;

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

    protected override void Awake()
    {
        base.Awake();

        image = GetComponent<Image>();
    }

    private void Start()
    {
        Image_PointsRefresh();

        position_last = transform.position;
    }

    private void Update()
    {
        if (!pressed)
        {
            if (transform.position != position_last)
            {
                Image_PointsRefresh();

                position_last = transform.position;
            }

            if (!Pointed(image_min, image_max))
            {
                if (image.sprite != image_idle)
                {
                    image.sprite = image_idle;
                }
            }
            else
            {
                if (image.sprite != image_pointed)
                {
                    image.sprite = image_pointed;
                }
            }
        }
    }
}
