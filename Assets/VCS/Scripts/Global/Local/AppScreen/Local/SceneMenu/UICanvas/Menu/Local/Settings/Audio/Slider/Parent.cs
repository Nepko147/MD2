using UnityEngine;
using UnityEngine.UI;

public class AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Audio_Slider_Parent : AppScreen_General_UICanvas_Parent
{
    private Image image;
    private Vector2 image_max;
    private float image_width;
    
    [SerializeField] private Sprite[] spriteArray;

    private AudioSource audioSource;

    private float value;
    public float Value
    {
        get
        {
            return (value);
        }
        set
        {
            this.value = value;

            var _ind = (int)Mathf.Ceil((spriteArray.Length - 1) * value);
            image.sprite = spriteArray[_ind];
        }
    }

    protected void OnClick()
    {
        Value = 1f - (image_max.x - ControlPers_InputHandler.SingleOnScene.Screen_Position.x) / image_width;
        
        audioSource.Play();
    }

    protected override void Awake()
    {
        base.Awake();

        image = GetComponent<Image>();
        
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        var _image_min = Image_ScreenPoint_Min(image);
        image_max = Image_ScreenPoint_Max(image);
        image_width = image_max.x - _image_min.x;

        Image_Highlight_Behaviour(image, _image_min, image_max);
    }
}
