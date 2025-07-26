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

            if (value) 
            {
                World_Local_SceneMain_Cops_Lights.SingleOnScene.Animation_On();
            }
            else
            {
                World_Local_SceneMain_Cops_Lights.SingleOnScene.Animation_Off();
            }
        }
    }

    private bool move = false;
    private Vector3 move_position_init;
    public void Move_Start()
    {
        move = true;

        World_Local_SceneMain_Cops_Lights.SingleOnScene.Light_On();
    }
    public void Move_Reset()
    {
        move = false;

        transform.position = move_position_init;

        World_Local_SceneMain_Cops_Lights.SingleOnScene.Light_Off();
    }

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Texture2D normalMap;

    private void Awake()
    {
        SingleOnScene = this;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetTexture(Constants.MATERIAL_2D_BUMP_U_BUMPMAP, normalMap);

        move_position_init = transform.position;
    }

    private void FixedUpdate()
    {
        if (active)
        {
            if (move)
            {
                transform.position += Vector3.left * World_Local_SceneMain_MovingBackground_Road.SPEED * World_Local_SceneMain_MovingBackground_Entity.SingleOnScene.SpeedScale;
            }
        }
    }
}
