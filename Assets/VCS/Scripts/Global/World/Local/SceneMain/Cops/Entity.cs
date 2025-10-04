using UnityEngine;
using Utils;

public class World_Local_SceneMain_Cops_Entity : MonoBehaviour
{
    public static World_Local_SceneMain_Cops_Entity SingleOnScene { get; private set; }
    
    private bool active = true;
    public bool Active 
    {
        get 
        { 
            return (active); 
        } 
        set
        {
            active = value;
            World_Local_SceneMain_Cops_Lights.SingleOnScene.Animated = value;
        }
    }
    
    public bool Visible 
    { 
        get 
        { 
            return (spriteRenderer.enabled); 
        }
        set 
        { 
            spriteRenderer.enabled = value; 
            World_Local_SceneMain_Cops_Lights.SingleOnScene.Visible = value;
        } 
    }

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Texture2D normalMap;

    public bool Move { get; private set; }
    private Vector3 move_position_init;

    public void Move_Start()
    {
        Move = true;

        World_Local_SceneMain_Cops_Lights.SingleOnScene.Light_On();
    }

    public void Move_Reset()
    {
        Move = false;

        transform.position = move_position_init;

        World_Local_SceneMain_Cops_Lights.SingleOnScene.Light_Off();
    }

    private void Awake()
    {
        SingleOnScene = this;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetTexture(Constants.MATERIAL_BUMPMAP_U_BUMPMAP, normalMap);

        Move = false;
        move_position_init = transform.position;
    }

    private void Update()
    {
        if (active
        && Move)
        {
            transform.position += Vector3.left * World_Local_SceneMain_MovingBackground_Road.SPEED * World_General_MovingBackground_Entity.SingleOnScene.SpeedScale * Time.deltaTime;
        }
    }
}
