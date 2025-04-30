using UnityEngine;
using UnityEngine.UI;

public class World_BonusString : MonoBehaviour
{
    [SerializeField] private GameObject body;
    [SerializeField] private Text popUpString;    

    void Awake()
    {
        Destroy(this.gameObject, 0.3f);
    }
    
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - 50, transform.position.y + 150), 0.1f);        
    }

    public void DisplayPopUp(string _effect, Rigidbody2D _parentBody) 
    {
        Vector2 position = new Vector2(_parentBody.transform.position.x, _parentBody.transform.position.y);
        popUpString.text = _effect;        
        Instantiate(body, position, new Quaternion());
    }
}
