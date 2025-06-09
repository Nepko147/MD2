using UnityEngine;
using UnityEngine.UI;

public class World_Local_SceneMain_PopUp : MonoBehaviour
{
    public bool Active { get; set; }

    [SerializeField] private Text textComponent;
    private Vector2 destinationPos = Vector2.zero;
    private const float SPEED = 0.1f;
    
    private bool display = false;
    public void Display_AsUp()
    {
        display = true;
        textComponent.text = "+1 UP";
        destinationPos = AppScreen_Local_SceneMain_UICanvas_Indicators_Ups_Sprite.SingleOnScene.transform.position;
        Behaviour = Behaviour_Up;
    }
    public void Display_AsCoin()
    {
        display = true;
        textComponent.text = "+1 Coin";
        destinationPos = AppScreen_Local_SceneMain_UICanvas_Indicators_Coins_Sprite.SingleOnScene.transform.position;
        Behaviour = Behaviour_Coin;
    }
    public void Display_AsCoinRush()
    {
        display = true;
        textComponent.text = "Coin Rush!";
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

        behaviour_CoinRush_time -= Time.fixedDeltaTime;

        if (behaviour_CoinRush_time <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        Active = true;
    }
    
    void FixedUpdate()
    {
        if (Active
            && display)
        {
            Behaviour();
        }        
    }    
}
