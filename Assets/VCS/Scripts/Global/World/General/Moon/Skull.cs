using UnityEngine;

public class World_General_Moon_Skull : MonoBehaviour
{
    public bool Visible
    {
        get
        {
            return sprite_Renderer.enabled;
        }
        set
        {
            sprite_Renderer.enabled = value;
        }
    }

    public Color Color
    {
        get
        {
            return sprite_Renderer.color;
        }
        set
        {
            sprite_Renderer.color = value;
        }
    }

    public Sprite Sprite
    {
        get
        {
            return sprite_Renderer.sprite;
        }
        set
        {
            sprite_Renderer.sprite = value;
        }
    }

    private SpriteRenderer sprite_Renderer;

    private void Awake()
    {
        sprite_Renderer = GetComponent<SpriteRenderer>();
    }
}
