using UnityEngine;

public class World_Local_SceneMain_MovingBackground_Parent : MonoBehaviour
{
    public bool Active { get; set; }

    protected float Speed { get; set; }

    private const float WIDTH = 12.8f;

    protected virtual void Awake()
    {
        Active = false;
    }

    private void FixedUpdate()
    {
        if (Active)
        {
            transform.position += Vector3.left * Speed * World_Local_SceneMain_MovingBackground_Entity.SingleOnScene.SpeedScale;

            //Город занимается СамоВоспроизводством
            if (transform.position.x <= -WIDTH)
            {
                transform.position = new Vector2(transform.position.x + (WIDTH * 2), transform.position.y);
            }
        }        
    }
}
