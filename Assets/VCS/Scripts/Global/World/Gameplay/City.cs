using UnityEngine;

public class City : MonoBehaviour
{
    [SerializeField] private float speed;
    private float difficultyScale;
    private Rigidbody2D body;
    private bool NeedClone;
  
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        NeedClone = true;        
        difficultyScale = Globalist.Instance.GetDifficultyScale(); 
        body.name = body.name.Substring(0,4);
        if (Globalist.Instance.gameStart)
        {
            body.linearVelocity = new Vector2(-speed * difficultyScale, 0);
        } else {
            body.linearVelocity = new Vector2(0, 0);
        }

    }
    
    private void FixedUpdate()
    {                
        //Проверка для паузы
        if (!Globalist.Instance.canPlay())
        {
            body.linearVelocity = new Vector2(0, 0);
            return;
        }
        if (body.GetRelativeVector(body.linearVelocity).x < speed)
        {
            body.linearVelocity = new Vector2(-speed * Globalist.Instance.GetDifficultyScale(), 0);
        }
           
        //Город занимается СамоВоспроизводством
        if (this.gameObject.transform.position.x <= 2f && NeedClone)
        { 
            Vector2 position = new Vector2(transform.position.x + 38.4f, transform.position.y);
            Quaternion rotation = new Quaternion();
            Instantiate(body, position, rotation, transform.parent);
            NeedClone = false;
        }
        
        //Уничтожаем объект, когда он уходит за пределы экрана
        if (this.gameObject.transform.position.x <= -36.4f)
        {
            Destroy(this.gameObject);
        }
    }
}
