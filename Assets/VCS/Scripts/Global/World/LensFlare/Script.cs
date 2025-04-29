using UnityEngine;

public class World_LensFlare : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float downScaleSpeed;
    [SerializeField] private float timeToDestroy;
    private Rigidbody2D body;
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        speed = speed * (1 + (ControlPers_Globalist.Singletone.GetDifficultyScale() - 1) / 2.5f);
        downScaleSpeed /= 100;
    }
        
    void FixedUpdate()
    {
        //Проверка на возможность игры        
        if (!ControlPers_Globalist.Singletone.canPlay())
        {
            body.linearVelocity = new Vector2(0, 0);
            return;
        }

        body.transform.localScale = new Vector3 (body.transform.localScale.x - downScaleSpeed, body.transform.localScale.y - downScaleSpeed, 1);

        //Выход из паузы
        if (body.GetRelativeVector(body.linearVelocity).x < speed)
        {
            body.linearVelocity = new Vector2(-speed, 0);
        }

        timeToDestroy -= Time.deltaTime;
        if (timeToDestroy <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
