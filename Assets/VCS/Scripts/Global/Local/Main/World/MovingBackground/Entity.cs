using UnityEngine;

public class World_MovingBackground_Entity : MonoBehaviour
{
    public static World_MovingBackground_Entity SingleOnScene { get; private set; }

    public bool Active { private get; set; }

    public float SpeedScale { get; private set; }
    [SerializeField] private float speedScale_max;
    [SerializeField] private float speedScale_increment;
    [SerializeField] private float speedScale_init;
    
    private void Awake()
    {
        SingleOnScene = this;

        SpeedScale = speedScale_init;
        Active = false;
    }

    void FixedUpdate()
    {
        if (Active)
        {
            SpeedScale += speedScale_increment;
        }
    }
}
