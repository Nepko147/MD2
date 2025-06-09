using UnityEngine;
using UnityEngine.UI;

public class AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Audio_Button_Parent : AppScreen_General_UICanvas_Parent
{
    protected bool mute = false;
    private float mute_on_sliderValue;
    public virtual void Mute_On(float _sliderValue)
    {
        mute_on_sliderValue = _sliderValue;
        image.sprite = sprite_off;
        mute = true;
    }
    public virtual void Mute_Off(float _sliderValue)
    {
        if (mute)
        {
            ImageRefresh(_sliderValue);
            mute = false;
        }
    }
    
    private Image image;
    private Vector2 image_min;
    private Vector2 image_max;

    public void ImageRefresh(float _sliderValue)
    {
        var _ind = (int)Mathf.Round((sprite_on_array.Length - 1) * _sliderValue);
        image.sprite = sprite_on_array[_ind];
    }

    [SerializeField] private Sprite sprite_off;
    [SerializeField] private Sprite[] sprite_on_array;

    private AudioSource audioSource;

    protected void OnClick(float _sliderValue)
    {
        if (mute)
        {
            Mute_Off(mute_on_sliderValue);
        }
        else
        {
            Mute_On(_sliderValue);
        }

        audioSource.Play();
    }

    protected override void Awake()
    {
        base.Awake();

        image = GetComponent<Image>();

        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {
        image_min = Image_ScreenPoint_Min(image);
        image_max = Image_ScreenPoint_Max(image);
    }

    private void Update()
    {
        Image_Highlight_Behaviour(image, image_min, image_max);
    }
}
