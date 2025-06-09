using UnityEngine;

public class AppScreen_General_Camera_World_Shake : MonoBehaviour
{
    public static AppScreen_General_Camera_World_Shake SingleOnScene { get; private set; }

    public bool Active { get; set; }

    Vector3 startPosition;

    private bool shake = false;
    private float shake_steps;
    [SerializeField] private float shake_steps_init = 20f;
    [SerializeField] private float shake_ofs_x = 0.2f;
    [SerializeField] private float shake_ofs_y = 0.1f;
    private Vector3 shake_ofs_vec3 = Vector3.zero;

    public void Shake()
    {
        shake_steps = shake_steps_init;
        shake = true;
    }

    private void Awake()
    {
        SingleOnScene = this;

        startPosition = transform.localPosition;
    }
   
    void FixedUpdate()
    {
        if (Active
        && shake)
        {
			var _shake_ofs_scale = shake_steps / shake_steps_init;
            shake_ofs_vec3.x = startPosition.x + Random.Range(-shake_ofs_x, shake_ofs_x) * _shake_ofs_scale;
            shake_ofs_vec3.y = startPosition.y + Random.Range(-shake_ofs_y, shake_ofs_y) * _shake_ofs_scale;
            transform.localPosition = shake_ofs_vec3;
			
            --shake_steps;

            if (shake_steps == 0)
            {
                transform.localPosition = startPosition; // Гарантируем, что после всех встрясок камера вернётся в начальную точку.
                shake = false;
            }
        }
    }
}
