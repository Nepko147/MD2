using UnityEngine;

public class World_LensFlare : MonoBehaviour
{
    public bool Active { get; set; }

    float scale = 1.0f;
    [SerializeField] private float scale_decreaseSpeed;

    private void Awake()
    {
        Active = true;
    }

    void FixedUpdate()
    {
        if (Active)
        {
            scale -= scale_decreaseSpeed;
            transform.localScale *= scale;            
            
            if (scale <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
