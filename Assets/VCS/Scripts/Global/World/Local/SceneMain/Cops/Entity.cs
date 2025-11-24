using UnityEngine;
using Utils;
using static UnityEngine.Rendering.DebugUI;

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

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Texture2D normalMap;

    public bool Move { get; private set; }
    private Vector3 move_position_init;

    private bool coins_spawned = false;

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

    public void Coins_Spawn()
    {
        if (!coins_spawned)
        {
            ControlScene_SceneMain_Sound_Police.SingleOnScene.Stop();
            World_Local_SceneMain_Cops_Coins.SingleOnScene.Coins_Spawn();
            spriteRenderer.enabled = false; 
            World_Local_SceneMain_Cops_Lights.SingleOnScene.Sprite_Visible = false;
            World_Local_SceneMain_Cops_Lights.SingleOnScene.Light_Off();

            coins_spawned = true;
        }

    }

    public void Coins_Refresh()
    {
        spriteRenderer.enabled = true; 
        World_Local_SceneMain_Cops_Lights.SingleOnScene.Sprite_Visible = true;
        coins_spawned = false;
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
