using UnityEngine;
using UnityEngine.UI;

public class AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_General_Button_Parent : AppScreen_General_UICanvas_Parent
{
    [SerializeField] private AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_General_Price price_prefab;
    private AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_General_Price price_instance;
    protected int price_coins_buy;
    protected int price_coins_improve;
    private Vector3 price_offset;
    private void Price_Spawn()
    {
        price_instance = Instantiate(price_prefab, Vector3.zero, transform.rotation, transform.parent);
        price_instance.LocalPosition_Set(rectTransform.localPosition);
    }
    
    private Image image;
    private Vector2 image_min;
    private Vector2 image_max;
    private bool image_pointsRefresh = false;
    protected Sprite image_idle;
    protected Sprite image_pointed;
    [SerializeField] protected Sprite image_idle_buy;
    [SerializeField] protected Sprite image_idle_improve;
    [SerializeField] protected Sprite image_pointed_buy;
    [SerializeField] protected Sprite image_pointed_improve;
    [SerializeField] protected Sprite image_received;

    [SerializeField] protected Sprite image_ru_idle_buy;
    [SerializeField] protected Sprite image_ru_idle_improve;
    [SerializeField] protected Sprite image_ru_pointed_buy;
    [SerializeField] protected Sprite image_ru_pointed_improve;
    [SerializeField] protected Sprite image_ru_received;

    protected Sprite image_current_idle_buy;
    protected Sprite image_current_idle_improve;
    protected Sprite image_current_pointed_buy;
    protected Sprite image_current_pointed_improve;
    protected Sprite image_current_received;

    [SerializeField] private AudioClip sound_button;
    [SerializeField] private AudioClip sound_upgrade;

    private Vector3 position_last;

    private const string POPUPMESSAGE_TEXT_EN = "NOT ENOUGH COINS";
    private const string POPUPMESSAGE_TEXT_RU = "Õ≈ƒŒ—“¿“Œ◊ÕŒ ÃŒÕ≈“";

    private string popupMessege_text;

    protected void Image_Set(Sprite _idle, Sprite _pointed)
    {
        image_idle = _idle;
        image_pointed = _pointed;

        image.sprite = image_idle;

        image_pointsRefresh = true;

        var _sizeInPixels = image.sprite.bounds.size * image.sprite.pixelsPerUnit;
        rectTransform.sizeDelta = new Vector2(_sizeInPixels.x, _sizeInPixels.y);
        rectTransform.localPosition += new Vector3(_sizeInPixels.x - image.sprite.pivot.x, 0, 0);

        price_offset = new Vector3(-rectTransform.sizeDelta.x, 0, 0);

        switch (ControlPers_LanguageHandler.SingleOnScene.CurrentGameLanguage)
        {
            case ControlPers_LanguageHandler.GameLanguage.english:
                popupMessege_text = POPUPMESSAGE_TEXT_EN;
                break;

            case ControlPers_LanguageHandler.GameLanguage.russian:
                popupMessege_text = POPUPMESSAGE_TEXT_RU;
                break;
        }
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

                    Image_Set(image_current_idle_improve, image_current_idle_improve);

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
        switch (ControlPers_LanguageHandler.SingleOnScene.CurrentGameLanguage)
        {
            case ControlPers_LanguageHandler.GameLanguage.english:
                image_current_idle_buy = image_idle_buy;
                image_current_idle_improve = image_idle_improve;
                image_current_pointed_buy = image_pointed_buy;
                image_current_pointed_improve = image_pointed_improve;
                image_current_received = image_received;
            break;

            case ControlPers_LanguageHandler.GameLanguage.russian:
                image_current_idle_buy = image_ru_idle_buy;
                image_current_idle_improve = image_ru_idle_improve;
                image_current_pointed_buy = image_ru_pointed_buy;
                image_current_pointed_improve = image_ru_pointed_improve;
                image_current_received = image_ru_received;
            break;
        }
        
        if (IsImproved())
        {
            Image_Set(image_current_received, image_current_received);
            rectTransform.localPosition -= new Vector3(image_current_received.rect.width - image_current_received.pivot.x, 0, 0);
        }
        else
        {
            if (IsBought())
            {
                Image_Set(image_current_idle_improve, image_current_idle_improve);
                price_instance.LocalPosition_Set(rectTransform.localPosition + price_offset);
            }
            else
            {
                Image_Set(image_current_idle_buy, image_current_idle_buy);
                price_instance.LocalPosition_Set(rectTransform.localPosition + price_offset);
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();

        image = GetComponent<Image>();

        switch (ControlPers_LanguageHandler.SingleOnScene.CurrentGameLanguage)
        {
            case ControlPers_LanguageHandler.GameLanguage.english:
                image_current_idle_buy = image_idle_buy;
                image_current_idle_improve = image_idle_improve;
                image_current_pointed_buy = image_pointed_buy;
                image_current_pointed_improve = image_pointed_improve;
                image_current_received = image_received;
            break;

            case ControlPers_LanguageHandler.GameLanguage.russian:
                image_current_idle_buy = image_ru_idle_buy;
                image_current_idle_improve = image_ru_idle_improve;
                image_current_pointed_buy = image_ru_pointed_buy;
                image_current_pointed_improve = image_ru_pointed_improve;
                image_current_received = image_ru_received;
                break;
        }
    }

    protected virtual void Start()
    {
        image_min = Image_ScreenPoint_Min(image);
        image_max = Image_ScreenPoint_Max(image);

        position_last = transform.position;

        if (!IsBought())
        {
            Price_Spawn();

            Image_Set(image_current_idle_buy, image_current_pointed_buy);

            price_instance.LocalPosition_Set(rectTransform.localPosition + price_offset);
            price_instance.Coins_Set(price_coins_buy);
        }
        else
        {
            if (!IsImproved())
            {
                Price_Spawn();

                Image_Set(image_current_idle_improve, image_current_pointed_improve);

                price_instance.LocalPosition_Set(rectTransform.localPosition + price_offset);
                price_instance.Coins_Set(price_coins_improve);
            }
            else
            {
                Image_Set(image_current_received, image_current_received);
            }
        }
    }

    private void Update()
    {
        if (!AppScreen_General_UICanvas_Entity.SingleOnScene.PopUpMessage_IsActive)
        {
            if (transform.position != position_last)
            {
                image_pointsRefresh = true;
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

    private void LateUpdate()
    {
        if (image_pointsRefresh)
        {
            image_min = Image_ScreenPoint_Min(image);
            image_max = Image_ScreenPoint_Max(image);

            image_pointsRefresh = false;
        }
    }
}
