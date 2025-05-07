using UnityEngine;
using UnityEngine.UI;

public class World_BonusString : MonoBehaviour
{
    public bool Active { get; set; }

    [SerializeField] private Text popup_string;
    [SerializeField] float popup_time;

    public void DisplayPopUp(string _text, float _x, float _y)
    {
        popup_string.text = _text;
        Vector2 _position = new Vector2(_x, _y);
        Instantiate(transform, _position, new Quaternion());
    }

    private void Awake()
    {
        Active = true;
    }

    void FixedUpdate()
    {
        if (Active)
        {
            popup_time -= Time.fixedDeltaTime;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - 50, transform.position.y + 150), 0.1f);
            
            if (popup_time <= 0)
            {
                Destroy(gameObject);
            }
        }        
    }    
}
