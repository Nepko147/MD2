using UnityEngine;
using UnityEngine.UI;

public class AppScreen_UICanvas_Menu_Settings_Slider_Parent : MonoBehaviour
{
    private RectTransform rectTransform;
    private Image image;
    [SerializeField] private Sprite[] spriteArray;
    private AudioSource audioSource;

    private Vector2 border_max;
    private float border_width;

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
        Value = 1f - (border_max.x - ControlPers_InputHandler.SingleOnScene.Screen_Position.x) / border_width;
    }

    protected void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        var _border_min = General_AppScreen_UICanvas_Entity.SingleOnScene.RectTransformToScreenPoint_Min(rectTransform);
        border_max = General_AppScreen_UICanvas_Entity.SingleOnScene.RectTransformToScreenPoint_Max(rectTransform);
        border_width = border_max.x - _border_min.x;
    }
}
