using UnityEngine;

public class World_Local_SceneMain_MovingBackground_Entity : MonoBehaviour
{
    public static World_Local_SceneMain_MovingBackground_Entity SingleOnScene { get; private set; }

    public bool Active { private get; set; }

    public float SpeedScale { get; private set; }
    [SerializeField] private float speedScale_max;
    [SerializeField] private float speedScale_increment;
    [SerializeField] private float speedScale_init;

    [SerializeField] GameObject city_3;
    [SerializeField] GameObject city_3_1;
    [SerializeField] GameObject city_2;
    [SerializeField] GameObject city_2_1;
    [SerializeField] GameObject city_1;
    [SerializeField] GameObject city_1_1;
    [SerializeField] GameObject bushes;
    [SerializeField] GameObject bushes_1;
    [SerializeField] GameObject road;
    [SerializeField] GameObject road_1;
    
    static Vector3 position_city_3;
    static Vector3 position_city_3_1;
    static Vector3 position_city_2;
    static Vector3 position_city_2_1;
    static Vector3 position_city_1;
    static Vector3 position_city_1_1;
    static Vector3 position_bushes;
    static Vector3 position_bushes_1;
    static Vector3 position_road;
    static Vector3 position_road_1;

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

    private void Awake()
    {
        SingleOnScene = this;

        SpeedScale = speedScale_init;
        Active = false;
    }

    private void FixedUpdate()
    {
        if (Active)
        {
            SpeedScale += speedScale_increment;
        }
    }
}
