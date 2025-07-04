using UnityEngine;

public class World_Local_SceneMain_MovingBackground_Parent : MonoBehaviour
{
    public bool Active { get; set; }

    protected float Speed { get; set; }    
    

    protected virtual void Awake()
    {
        Active = false;
    }

    private void FixedUpdate()
    {
        if (Active)
        {
            transform.position += Vector3.left * Speed * World_Local_SceneMain_MovingBackground_Entity.SingleOnScene.SpeedScale;

            //����� ���������� ��������������������
            if (transform.position.x <= -11f)
            {
                Vector2 position = new Vector2(transform.position.x + (12.8f * 2), transform.position.y);
                transform.position = position;
            }
        }        
    }
}
