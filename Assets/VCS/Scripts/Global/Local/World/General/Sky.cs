using UnityEngine;

public class World_General_Sky : MonoBehaviour
{
    public static World_General_Sky SingleOnScene { get; private set; }

    public bool Active { get; set; }

    private const float Y_MAX = 1f;
    private const float Y_MIN = 0.5f;
    private const float SPEED = 0.15f;

    private bool state = true;

    private void Awake()
    {
        SingleOnScene = this;

        Active = false;
    }

    private void Update()
    {
        if (Active)
        {
            if (state)
            {
                transform.position += Vector3.up * SPEED * Time.deltaTime;

                if (transform.position.y >= Y_MAX)
                {
                    state = false;
                }
            }
            else
            {
                transform.position -= Vector3.up * SPEED * Time.deltaTime;

                if (transform.position.y <= Y_MIN)
                {
                    state = true;
                }
            }
        }
    }
}
