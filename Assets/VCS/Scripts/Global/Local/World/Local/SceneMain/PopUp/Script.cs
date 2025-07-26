using UnityEngine;
using UnityEngine.UI;

public class World_Local_SceneMain_PopUp : MonoBehaviour
{
    public bool Active { get; set; }

    private bool display = false;

    private Text text;

    private Image image;
    [SerializeField] private Sprite image_sprite_up;
    [SerializeField] private Sprite image_sprite_coin;
    [SerializeField] private Sprite image_sprite_coinRush;
    private void Image_Sprite_Set(Sprite _sprite)
    {
        image.sprite = _sprite;
        image.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(_sprite.textureRect.width, _sprite.textureRect.height);
    }

    private Vector2 destinationPos = Vector2.zero;
    private const float SPEED = 0.1f;
    
    public void Display_AsUp()
    {
        display = true;
        text.text = "+1 UP";
        Image_Sprite_Set(image_sprite_up);
        destinationPos = AppScreen_Local_SceneMain_UICanvas_Indicators_Ups_Icon.SingleOnScene.transform.position;
        Behaviour = Behaviour_Up;
    }
    public void Display_AsCoin()
    {
        display = true;
        text.text = "+1 Coin";
        Image_Sprite_Set(image_sprite_coin);
        destinationPos = AppScreen_Local_SceneMain_UICanvas_Indicators_Coins_Icon.SingleOnScene.transform.position;
        Behaviour = Behaviour_Coin;
    }
    public void Display_AsCoinRush()
    {
        display = true;
        text.text = "Coin Rush!";
        Image_Sprite_Set(image_sprite_coinRush);
        Behaviour = Behaviour_CoinRush;
    }

    private delegate void Behaviour_Delegate();
    private Behaviour_Delegate Behaviour;
    private void Behaviour_Up()
    {
        transform.position = Vector2.MoveTowards(transform.position, destinationPos, SPEED);

        if (Vector2.Distance(transform.position, destinationPos) <= SPEED)
        {
            ++AppScreen_Local_SceneMain_UICanvas_Entity.SingleOnScene.Ups_Visual;
            Destroy(gameObject);
        }
    }
    private void Behaviour_Coin()
    {
        transform.position = Vector2.MoveTowards(transform.position, destinationPos, SPEED);

        if (Vector2.Distance(transform.position, destinationPos) <= SPEED)
        {
            ++AppScreen_Local_SceneMain_UICanvas_Entity.SingleOnScene.Coins_Visual;
            Destroy(gameObject);
        }
    }
    private float behaviour_CoinRush_time = 0.3f;
    private void Behaviour_CoinRush()
    {
        destinationPos.x = transform.position.x - SPEED;
        destinationPos.y = transform.position.y + SPEED * 3f;
        transform.position = Vector2.MoveTowards(transform.position, destinationPos, SPEED);

        behaviour_CoinRush_time -= Time.deltaTime;

        if (behaviour_CoinRush_time <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        Active = true;

        text = GetComponentInChildren<Text>();

        image = GetComponentInChildren<Image>();
    }
    
    void Update()
    {
        if (Active
        && display)
        {
            Behaviour();
        }        
    }    
}
