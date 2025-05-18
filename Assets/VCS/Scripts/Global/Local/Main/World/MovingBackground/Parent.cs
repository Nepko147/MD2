using UnityEngine;

public class World_MovingBackground_Parent : MonoBehaviour
{
    public bool Active { get; set; }

    protected float Speed { get; set; }    
    

    protected void Awake()
    {
        Active = true;
    }

    private void FixedUpdate()
    {
        if (Active)
        {
            transform.position += Vector3.left * Speed * World_MovingBackground_Entity.Singletone.SpeedScale;

            //Город занимается СамоВоспроизводством
            if (transform.position.x <= -11f)
            {
                Vector2 position = new Vector2(transform.position.x + (12.8f * 2), transform.position.y);
                transform.position = position;
            }
        }        
    }
}
