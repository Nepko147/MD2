using UnityEngine;

public class AppScreen_Camera_World_Shake : MonoBehaviour
{
    public static AppScreen_Camera_World_Shake Singleton { get; private set; }

    Vector3                 camera_startPosition;

    float                   camera_shake_steps;
    [SerializeField] float  camera_shake_steps_init = 20;
    bool                    camera_shake_isShaked;

    float                   camera_shake_x;
    [SerializeField] float  camera_shake_x_min = 0.1f;
    [SerializeField] float  camera_shake_x_max = 0.2f;

    float                   camera_shake_y;
    [SerializeField] float  camera_shake_y_min = 0.02f;
    [SerializeField] float  camera_shake_y_max = 0.1f;

    const int ROUND_100 = 100;

    // ёзаю oкругление, из-за бесконечно-малых чисел там, где должен быть 0
    float Round(float _inputValue, int _accuracy)
    {
        var _outputValue = ((float)(int)(_inputValue * _accuracy)) / _accuracy;
        return _outputValue;
    }

    public void Shake()
    {
        camera_shake_x = Round(Random.Range(camera_shake_x_min, camera_shake_x_max), ROUND_100);
        camera_shake_y = Round(Random.Range(camera_shake_y_min, camera_shake_y_max), ROUND_100);
        transform.localPosition = new Vector3(transform.localPosition.x + camera_shake_x, transform.localPosition.y + camera_shake_y, transform.localPosition.z);
        camera_shake_steps = camera_shake_steps_init;
        camera_shake_isShaked = true;
    }

    private void Awake()
    {
        Singleton = this;

        camera_startPosition = transform.localPosition;
    }
   
    void FixedUpdate()
    {
        if (camera_shake_isShaked)
        {
            var _new_x = transform.localPosition.x - camera_shake_x / camera_shake_steps_init;
            var _new_y = transform.localPosition.y - camera_shake_y / camera_shake_steps_init;
            transform.localPosition = new Vector3(_new_x, _new_y, transform.localPosition.z);

            --camera_shake_steps;

            if (camera_shake_steps == 0)
            {
                transform.localPosition = camera_startPosition; // √арантируем, что после всех встр€сок камера вернЄтс€ в начальную точку.
                camera_shake_isShaked = false;
            }
        }
    }
}
