using UnityEngine;

public class World_Background_Parent : MonoBehaviour
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
            transform.position += Vector3.left * Speed * World_Background_Entity.Singletone.SpeedScale;

            //Город занимается СамоВоспроизводством
            if (transform.position.x <= -11f)
            {
                Vector2 position = new Vector2(transform.position.x + (12.8f * 2), transform.position.y);
                transform.position = position;
            }
        }        
    }
}
