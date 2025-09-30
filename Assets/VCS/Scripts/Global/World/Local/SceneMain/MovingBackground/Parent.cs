using UnityEngine;

public class World_Local_SceneMain_MovingBackground_Parent : MonoBehaviour
{
    public bool Active { get; set; }

    public bool Move { get; set; }

    protected float Speed { get; set; }

    protected const float WIDTH = 12.8f;

    private Vector3 position_init;
    public void Position_Reset()
    {
        transform.position = position_init;
    }

    protected virtual void Awake()
    {
        Active = false;

        Move = false;

        position_init = transform.position;
    }

    protected virtual void Update()
    {
        if (Active
        && Move)
        {
            transform.position += Vector3.left * Speed * World_Local_SceneMain_MovingBackground_Entity.SingleOnScene.SpeedScale * Time.deltaTime;

            //Город занимается СамоВоспроизводством
            if (transform.position.x <= -WIDTH)
            {
                transform.position = new Vector2(transform.position.x + (WIDTH * 2), transform.position.y);
            }
        }        
    }
}
