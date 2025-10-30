using UnityEngine;
using UnityEngine.UI;

public class AppScreen_UICanvas_Menu_Upgrades_Upgrade_General_Button_Parent : AppScreen_General_UICanvas_Parent
{
    private Vector3 rectTransform_localPosition_init;
    
    [SerializeField] private AppScreen_UICanvas_Menu_Upgrades_Upgrade_General_Price price_prefab;
    private AppScreen_UICanvas_Menu_Upgrades_Upgrade_General_Price price_instance;
    protected int price_coins_buy;
    protected int price_coins_improve;
    private Vector3 price_offset;

    private void Price_Spawn()
    {
        price_instance = Instantiate(price_prefab, Vector3.zero, transform.rotation, transform.parent);
        price_instance.LocalPosition_Set(rectTransform.localPosition);
    }
    
    private Image image;
    protected Sprite image_idle;
    protected Sprite image_pointed;

    protected Sprite image_current_idle_buy;
    protected Sprite image_current_idle_improve;
    protected Sprite image_current_pointed_buy;
    protected Sprite image_current_pointed_improve;
    protected Sprite image_current_received;

    [SerializeField] private AudioClip sound_button;
    [SerializeField] private AudioClip sound_upgrade;

    private string popupMessege_text;

    protected void Image_Set(Sprite _idle, Sprite _pointed)
    {
        image_idle = _idle;
        image_pointed = _pointed;

        image.sprite = image_idle;

        var _sizeInPixels = image.sprite.bounds.size * image.sprite.pixelsPerUnit;
        rectTransform.sizeDelta = new Vector2(_sizeInPixels.x, _sizeInPixels.y);
        rectTransform.localPosition = rectTransform_localPosition_init + new Vector3(_sizeInPixels.x - image.sprite.pivot.x, 0, 0);

        price_offset = new Vector3(-rectTransform.sizeDelta.x, 0, 0);

        popupMessege_text = ControlPers_LanguageHandler.SingleOnScene.Text_Get(ControlPers_LanguageHandler.Text_Key.popUpMessage_notEnoughCoins);
    }

    protected delegate bool IsState();
    protected IsState IsBought;
    protected IsState IsImproved;

    protected delegate void StateAction();
    protected StateAction Buy;
    protected StateAction Improve;
    protected StateAction Animation;

    public void OnClick()
    {
        if (!AppScreen_General_UICanvas_Entity.SingleOnScene.PopUpMessage_IsActive)
        {
            if (!IsBought())
            {
                if (ControlPers_DataHandler.SingleOnScene.ProgressData_Coins < price_coins_buy)
                {
                    ControlPers_AudioMixer_Sounds.SingleOnScene.Play(sound_button);

                    AppScreen_General_UICanvas_Entity.SingleOnScene.PopUpMessage_Show(popupMessege_text);
                }
                else
                {
                    Buy();
                    Animation();

                    ControlPers_DataHandler.SingleOnScene.ProgressData_Coins -= price_coins_buy;
                    ControlPers_DataHandler.SingleOnScene.ProgressData_Save();

                    ControlPers_AudioMixer_Sounds.SingleOnScene.Play(sound_button);
                    ControlPers_AudioMixer_Sounds.SingleOnScene.Play(sound_upgrade);

                    Image_Set(image_current_idle_improve, image_current_pointed_improve);

                    price_instance.LocalPosition_Set(rectTransform.localPosition + price_offset);
                    price_instance.Coins_Set(price_coins_improve);
                }
            }
            else
            {
                if (!IsImproved())
                {
                    if (ControlPers_DataHandler.SingleOnScene.ProgressData_Coins < price_coins_improve)
                    {
                        ControlPers_AudioMixer_Sounds.SingleOnScene.Play(sound_button);

                        AppScreen_General_UICanvas_Entity.SingleOnScene.PopUpMessage_Show(popupMessege_text);
                    }
                    else
                    {
                        Improve();
                        Animation();

                        ControlPers_DataHandler.SingleOnScene.ProgressData_Coins -= price_coins_improve;
                        ControlPers_DataHandler.SingleOnScene.ProgressData_Save();

                        ControlPers_AudioMixer_Sounds.SingleOnScene.Play(sound_button);
                        ControlPers_AudioMixer_Sounds.SingleOnScene.Play(sound_upgrade);

                        Image_Set(image_current_received, image_current_received);

                        Destroy(price_instance.gameObject);
                    }
                }
            }
        }
    }
    
    public void Image_LanguageRefresh(ControlPers_LanguageHandler.ButtonName _buttonName)
    {
        var _spriteArray = ControlPers_LanguageHandler.SingleOnScene.Buttons_GetSprites(_buttonName, 5);
        
        image_current_idle_buy = _spriteArray[0];
        image_current_idle_improve = _spriteArray[1];
        image_current_pointed_buy = _spriteArray[2];
        image_current_pointed_improve = _spriteArray[3];
        image_current_received = _spriteArray[4];
                
        if (IsImproved())
        {
            Image_Set(image_current_received, image_current_received);
        }
        else
        {
            if (IsBought())
            {
                Image_Set(image_current_idle_improve, image_current_pointed_improve);
                price_instance.LocalPosition_Set(rectTransform.localPosition + price_offset);
            }
            else
            {
                Image_Set(image_current_idle_buy, image_current_pointed_buy);
                price_instance.LocalPosition_Set(rectTransform.localPosition + price_offset);
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();

        rectTransform_localPosition_init = rectTransform.localPosition;

        image = GetComponent<Image>();
    }

    protected virtual void Start()
    {     
        if (!IsBought())
        {
            Price_Spawn();
            price_instance.Coins_Set(price_coins_buy);
        }
        else
        {
            if (!IsImproved())
            {
                Price_Spawn();
                price_instance.Coins_Set(price_coins_improve);
            }
        }
    }

    private void Update()
    {
        if (!AppScreen_General_UICanvas_Entity.SingleOnScene.PopUpMessage_IsActive)
        {
            var _image_min = Image_ScreenPoint_Min(image);
            var _image_max = Image_ScreenPoint_Max(image);

            if (!Pointed(_image_min, _image_max))
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
