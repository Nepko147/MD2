using UnityEngine;

public class World_General_Sky : MonoBehaviour
{
    public static World_General_Sky SingleOnScene { get; private set; }

    public bool Active { get; set; }

    [SerializeField] float y_max = 1f;
    [SerializeField] float y_min = 0;
    [SerializeField] float speed = 0.1f;

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
                transform.position += Vector3.up * speed * Time.deltaTime;

                if (transform.position.y >= y_max)
                {
                    state = false;
                }
            }
            else
            {
                transform.position -= Vector3.up * speed * Time.deltaTime;

                if (transform.position.y <= y_min)
                {
                    state = true;
                }
            }
        }
    }
}
