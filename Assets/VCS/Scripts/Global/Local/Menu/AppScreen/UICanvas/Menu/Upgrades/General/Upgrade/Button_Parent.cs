using UnityEngine;
using UnityEngine.UI;

public class AppScreen_UICanvas_Menu_Upgrades_Upgrade_Button_Parent : AppScreen_UICanvas_Parent
{
    [SerializeField] private AppScreen_UICanvas_Menu_Upgrades_Upgrade_Price price_prefab;
    private AppScreen_UICanvas_Menu_Upgrades_Upgrade_Price price_instance;
    protected int price_coins_buy;
    protected int price_coins_improve;
    private Vector3 price_offset;
    private void Price_Spawn()
    {
        price_instance = Instantiate(price_prefab, Vector3.zero, transform.rotation, transform.parent);
        price_instance.Position_Set(rectTransform.localPosition);
    }

    private Image image;
    private Vector2 image_min;
    private Vector2 image_max;
    private bool image_isPointed = false;
    protected Sprite image_idle;
    protected Sprite image_pointed;
    [SerializeField] protected Sprite image_idle_buy;
    [SerializeField] protected Sprite image_idle_improve;
    [SerializeField] protected Sprite image_pointed_buy;
    [SerializeField] protected Sprite image_pointed_improve;
    [SerializeField] protected Sprite image_received;
    
    protected void Image_Set(Sprite _idle, Sprite _pointed)
    {
        image_idle = _idle;
        image_pointed = _pointed;

        if (!image_isPointed)
        {
            image.sprite = image_idle;
        }
        else
        {
            image.sprite = image_pointed;
        }

        image_min = Image_ScreenPoint_Min(image);
        image_max = Image_ScreenPoint_Max(image);

        var _image_scale = image_idle.pixelsPerUnit;
        rectTransform.sizeDelta = new Vector2(image_idle.bounds.size.x * _image_scale, image_idle.bounds.size.y * _image_scale);

        price_offset = new Vector3(-rectTransform.sizeDelta.x, 0, 0);
    }

    protected delegate bool IsState();
    protected IsState IsBought;
    protected IsState IsImproved;

    protected delegate void StateAction();
    protected StateAction Buy;
    protected StateAction Improve;

    public void OnClick()
    {
        if (!IsBought())
        {
            if (ControlPers_DataHandler.SingleOnScene.ProgressData_Coins < price_coins_buy)
            {
                //сообщение о нехватке монет
            }
            else
            {
                Buy();

                ControlPers_DataHandler.SingleOnScene.ProgressData_Coins -= price_coins_buy;
                ControlPers_DataHandler.SingleOnScene.ProgressData_Save();

                Image_Set(image_idle_improve, image_pointed_improve);

                price_instance.Position_Set(rectTransform.localPosition + price_offset);
                price_instance.Coins_Set(price_coins_improve);
            }
        }
        else
        {
            if (!IsImproved())
            {
                if (ControlPers_DataHandler.SingleOnScene.ProgressData_Coins < price_coins_improve)
                {
                    //сообщение о нехватке монет
                }
                else
                {
                    Improve();

                    ControlPers_DataHandler.SingleOnScene.ProgressData_Coins -= price_coins_improve;
                    ControlPers_DataHandler.SingleOnScene.ProgressData_Save();

                    Image_Set(image_received, image_received);

                    Destroy(price_instance.gameObject);
                }
            }
        }
    }
    
    protected override void Awake()
    {
        base.Awake();

        image = GetComponent<Image>();
    }

    private void Start()
    {
        if (!IsBought())
        {
            Price_Spawn();

            Image_Set(image_idle_buy, image_pointed_buy);

            price_instance.Position_Set(rectTransform.localPosition + price_offset);
            price_instance.Coins_Set(price_coins_buy);
        }
        else
        {
            if (!IsImproved())
            {
                Price_Spawn();

                Image_Set(image_idle_improve, image_pointed_improve);

                price_instance.Position_Set(rectTransform.localPosition + price_offset);
                price_instance.Coins_Set(price_coins_improve);
            }
            else
            {
                Image_Set(image_received, image_received);
            }
        }
    }

    private void Update()
    {
        if (Pointed(image_min, image_max))
        {
            if (!image_isPointed)
            {
                image.sprite = image_pointed;
                image_isPointed = true;
            }
        }
        else
        {
            if (image_isPointed)
            {
                image.sprite = image_idle;
                image_isPointed = false;
            }
        }
    }
}
