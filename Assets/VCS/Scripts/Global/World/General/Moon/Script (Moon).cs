using UnityEngine;

public class World_General_Moon : MonoBehaviour
{
    public static World_General_Moon SingleOnScene { get; private set; }

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

    private void Awake()
    {
        SingleOnScene = this;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
