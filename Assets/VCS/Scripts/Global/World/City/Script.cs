using UnityEngine;

public class World_City : MonoBehaviour
{
    [SerializeField] private float speed;
    private float difficultyScale;
    private Rigidbody2D body;
    private bool NeedClone;
  
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        NeedClone = true;        
        difficultyScale = ControlPers_Globalist.Singletone.GetDifficultyScale(); 
        body.name = body.name.Substring(0,4);
        if (ControlPers_Globalist.Singletone.gameStart)
        {
            body.linearVelocity = new Vector2(-speed * difficultyScale, 0);
        } else {
            body.linearVelocity = new Vector2(0, 0);
        }
    }
    
    private void FixedUpdate()
    {                
        //�������� ��� �����
        if (!ControlPers_Globalist.Singletone.canPlay())
        {
            body.linearVelocity = new Vector2(0, 0);
            return;
        }
        if (body.GetRelativeVector(body.linearVelocity).x < speed)
        {
            body.linearVelocity = new Vector2(-speed * ControlPers_Globalist.Singletone.GetDifficultyScale(), 0);
        }
           
        //����� ���������� ��������������������
        if (this.gameObject.transform.position.x <= 2f && NeedClone)
        { 
            Vector2 position = new Vector2(transform.position.x + 38.4f, transform.position.y);
            Quaternion rotation = new Quaternion();
            Instantiate(body, position, rotation, transform.parent);
            NeedClone = false;
        }
        
        //���������� ������, ����� �� ������ �� ������� ������
        if (this.gameObject.transform.position.x <= -36.4f)
        {
            Destroy(this.gameObject);
        }
    }
}
