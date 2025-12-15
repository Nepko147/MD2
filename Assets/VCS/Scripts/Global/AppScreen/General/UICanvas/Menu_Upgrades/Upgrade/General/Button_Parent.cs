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
    private const int PRICE_MULT_WEB_YANDEXGAMES = 2;

    private void Price_Spawn()
    {
        price_instance = Instantiate(price_prefab, Vector3.zero, transform.rotation, transform.parent);
        price_instance.LocalPosition_Set(rectTransform.localPosition);
    }
    
    private Image image;
    protected Sprite image_idle;
    protected Sprite image_pointed;

    protected Sprite image_current_buy_idle;
    protected Sprite image_current_improve_idle;
    protected Sprite image_current_buy_pointed;
    protected Sprite image_current_improve_pointed;
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

        popupMessege_text = ControlPers_LanguageHandler_Entity.SingleOnScene.Text_Get(ControlPers_LanguageHandler_Entity.Text_Key.popUpMessage_notEnoughCoins);
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

                    Image_Set(image_current_improve_idle, image_current_improve_pointed);

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
    
    public void Image_LanguageRefresh()
    {
        image_current_buy_idle = ControlPers_LanguageHandler_Entity.SingleOnScene.Sprite_Get(ControlPers_LanguageHandler_Entity.Sprite_Key.button_upgrade_buy_idle);
        image_current_buy_pointed = ControlPers_LanguageHandler_Entity.SingleOnScene.Sprite_Get(ControlPers_LanguageHandler_Entity.Sprite_Key.button_upgrade_buy_pointed);
        image_current_improve_idle = ControlPers_LanguageHandler_Entity.SingleOnScene.Sprite_Get(ControlPers_LanguageHandler_Entity.Sprite_Key.button_upgrade_improve_idle);
        image_current_improve_pointed = ControlPers_LanguageHandler_Entity.SingleOnScene.Sprite_Get(ControlPers_LanguageHandler_Entity.Sprite_Key.button_upgrade_improve_pointed);
        image_current_received = ControlPers_LanguageHandler_Entity.SingleOnScene.Sprite_Get(ControlPers_LanguageHandler_Entity.Sprite_Key.button_upgrade_received);
        
        if (IsImproved())
        {
            Image_Set(image_current_received, image_current_received);
        }
        else
        {
            if (IsBought())
            {
                Image_Set(image_current_improve_idle, image_current_improve_pointed);
                price_instance.LocalPosition_Set(rectTransform.localPosition + price_offset);
            }
            else
            {
                Image_Set(image_current_buy_idle, image_current_buy_pointed);
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
        if (ControlPers_BuildSettings.SingleOnScene.PlatformType_Current == ControlPers_BuildSettings.PlatformType.web_yandexGames_desktop
        || ControlPers_BuildSettings.SingleOnScene.PlatformType_Current == ControlPers_BuildSettings.PlatformType.web_yandexGames_mobile_android)
        {
            price_coins_buy *= PRICE_MULT_WEB_YANDEXGAMES;
            price_coins_improve *= PRICE_MULT_WEB_YANDEXGAMES;
        }

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
