using UnityEngine;

[System.Serializable] public class World_General_MovingBackground_Entity : MonoBehaviour
{
    public static World_General_MovingBackground_Entity SingleOnScene { get; private set; }

    public bool Active { get; set; }

    public Color Color
    {
        get
        {
            return color;
        }
        set
        {
            color = value;
            city_3.GetComponent<SpriteRenderer>().color = value;
            city_3_1.GetComponent<SpriteRenderer>().color = value;
            city_2.GetComponent<SpriteRenderer>().color = value;
            city_2_1.GetComponent<SpriteRenderer>().color = value;
            city_1.GetComponent<SpriteRenderer>().color = value;
            city_1_1.GetComponent<SpriteRenderer>().color = value;
            bushes.GetComponent<SpriteRenderer>().color = value;
            bushes_1.GetComponent<SpriteRenderer>().color = value;
        }
    }

    private Color color;
    [SerializeField] private Color color_init = Color.white;    

    [SerializeField] private GameObject city_3;
    [SerializeField] private GameObject city_3_1;
    [SerializeField] private GameObject city_2;
    [SerializeField] private GameObject city_2_1;
    [SerializeField] private GameObject city_1;
    [SerializeField] private GameObject city_1_1;
    [SerializeField] private GameObject bushes;
    [SerializeField] private GameObject bushes_1;
    [SerializeField] private GameObject road;
    [SerializeField] private GameObject road_1;

    private static Vector3 position_city_3;
    private static Vector3 position_city_3_1;
    private static Vector3 position_city_2;
    private static Vector3 position_city_2_1;
    private static Vector3 position_city_1;
    private static Vector3 position_city_1_1;
    private static Vector3 position_bushes;
    private static Vector3 position_bushes_1;
    private static Vector3 position_road;
    private static Vector3 position_road_1;

    public void Position_Save()
    {
        position_city_3 = city_3.transform.position;
        position_city_3_1 = city_3_1.transform.position;  
        position_city_2 = city_2.transform.position;
        position_city_2_1 = city_2_1.transform.position; 
        position_city_1 = city_1.transform.position;
        position_city_1_1 = city_1_1.transform.position; 
        position_bushes = bushes.transform.position;
        position_bushes_1 = bushes_1.transform.position;
        position_road = road.transform.position;
        position_road_1 = road_1.transform.position;
    }

    public void Position_Load()
    {
        city_3.transform.position = position_city_3;
        city_3_1.transform.position = position_city_3_1;
        city_2.transform.position = position_city_2;
        city_2_1.transform.position = position_city_2_1;
        city_1.transform.position = position_city_1;
        city_1_1.transform.position = position_city_1_1;
        bushes.transform.position = position_bushes;
        bushes_1.transform.position = position_bushes_1;
        road.transform.position = position_road;
        road_1.transform.position = position_road_1;  
    }

    public bool SpeedScale_Active { get; set; }
    public float SpeedScale { get; set; }
    public const float SPEEDSCALE_MIN = 0.008f;
    private const float SPEEDSCALE_MAX = 0.016f;
    private const float SPEEDSCALE_STEP_MIN = 0.00015f;
    private const float SPEEDSCALE_STEP_MAX = 0.00045f;
    private float speedScale_step = SPEEDSCALE_STEP_MIN;

    public float SpeedScale_Normalized()
    {
        return ((SpeedScale - SPEEDSCALE_MIN) / (SPEEDSCALE_MAX - SPEEDSCALE_MIN));
    }

    /// <summary>
    /// _boost - нормализованный коэфициэнт увеличения ускорения
    /// </summary>
    public void SpeedScale_Step_Boost(float _boost)
    {
        speedScale_step = SPEEDSCALE_STEP_MIN + (SPEEDSCALE_STEP_MAX - SPEEDSCALE_STEP_MIN) * _boost;
    }

    private void Awake()
    {
        SingleOnScene = this;

        SpeedScale_Active = true;

        SpeedScale = SPEEDSCALE_MIN;

        Color = color_init;
    }

    private void Update()
    {
        if (Active
        && SpeedScale_Active)
        {
            SpeedScale += speedScale_step * Time.deltaTime;

            if (SpeedScale >= SPEEDSCALE_MAX)
            {
                SpeedScale = SPEEDSCALE_MAX;
                SpeedScale_Active = false;
            }
        }
    }
}
