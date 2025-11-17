using UnityEngine;

public class World_General_Moon : MonoBehaviour
{
    public static World_General_Moon SingleOnScene { get; private set; }

    [SerializeField] private World_General_Moon_Skull skull;

    private bool skull_isVisible; 
    public bool Skull_IsVisible
    {
        get
        {
            return skull_isVisible;
        }
        set
        {
            skull_isVisible = value;
            skull.Visible = value;
        }
    }

    [SerializeField] private Sprite[] skull_state_sprites;
    private int skull_state_current = 0;
    public void Skull_State_Next()
    {
        ++skull_state_current;
        skull_state_current = Mathf.Clamp(skull_state_current, 0, skull_state_sprites.Length - 1);
        skull.Sprite = skull_state_sprites[skull_state_current];
    }

    [SerializeField] private World_General_Moon_Part skull_parts_left;
    private Vector3 skull_parts_left_position_destination = new Vector3(-0.2f, 0.1f, 0);

    [SerializeField] private World_General_Moon_Part skull_parts_right;
    private Vector3 skull_parts_right_position_destination = new Vector3(0.2f, -0.1f, 0);

    private bool skull_parts_isActive;
    public bool Skull_Parts_IsActive
    {
        get
        {
            return skull_parts_isActive;
        }
        set
        {
            skull_parts_isActive = value;
            skull_parts_left.Active = value;
            skull_parts_right.Active = value;
        }
    }

    private bool skull_parts_isVisible;

    public bool Skull_Parts_IsVisible
    {
        get
        {
            return skull_parts_isVisible;
        }
        set
        {
            skull_parts_isVisible = value;
            skull_parts_left.Visible = value;
            skull_parts_right.Visible = value;
        }
    }

    private Color color;
    public Color Color
    {
        get
        {            
            return color;
        }
        set
        {
            color = value;
            spriteRenderer.color = value;
            skull.Color = value;
            skull_parts_left.Color = value;
            skull_parts_right.Color = value;
        }
    }

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

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        SingleOnScene = this;

        spriteRenderer = GetComponent<SpriteRenderer>();
        Color = spriteRenderer.color;
    }

    private void Start()
    {
        Skull_IsVisible = false;

        Skull_Parts_IsActive = false;
        Skull_Parts_IsVisible = false;

        skull_parts_left.Position_Destination = skull_parts_left_position_destination;
        skull_parts_right.Position_Destination = skull_parts_right_position_destination;
    }
}
