using UnityEngine;

public class World_Local_SceneMain_MovingBackground_Entity : MonoBehaviour
{
    public static World_Local_SceneMain_MovingBackground_Entity SingleOnScene { get; private set; }

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
    public const float SPEEDSCALE_INIT = 0.008f;
    private const float SPEEDSCALE_INCREMENT = 0.0001f;
    private const float SPEEDSCALE_MAX = 0.02f;

    private void Awake()
    {
        SingleOnScene = this;

        SpeedScale_Active = false;

        SpeedScale = SPEEDSCALE_INIT; 
    }

    private void FixedUpdate()
    {
        if (SpeedScale_Active)
        {
            SpeedScale += SPEEDSCALE_INCREMENT * Time.deltaTime;

            if (SpeedScale >= SPEEDSCALE_MAX)
            {
                SpeedScale = SPEEDSCALE_MAX;
                SpeedScale_Active = false;
            }
        }
    }
}
