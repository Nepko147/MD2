using UnityEngine;

public class World_General_Moon_Part : MonoBehaviour
{
    public bool Active { get; set; }

    public bool Visible
    {
        get
        {
            return spriteRenderer.enabled;
        }
        set
        {
            spriteRenderer.enabled = value;
        }
    }

    public Color Color
    {
        get
        {
            return spriteRenderer.color;
        }
        set
        {
            spriteRenderer.color = value;
        }
    }

    private SpriteRenderer spriteRenderer;

    public Vector3 Position_Destination { get; set; }

    private float position_step = 0.005f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Active)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Position_Destination, position_step * Time.deltaTime);
        }
    }
}
