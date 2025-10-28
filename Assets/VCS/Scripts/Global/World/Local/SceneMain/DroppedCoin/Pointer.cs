using UnityEngine;

public class World_Local_SceneMain_DroppedCoin_Pointer : MonoBehaviour
{
    public bool Active
    {
        get 
        {
            return active;
        } 
        set 
        {
            animator.speed = value ? 1 : 0;
            spriteRenderer.material.SetFloat("_ShakePower", value ? SHAKE_POWER_MAX : SHAKE_POWER_MIN);
            spriteRenderer.material.SetFloat("_ShakeRate", value ? SHAKE_RATE_MAX : SHAKE_RATE_MIN);
            active = value;
        }
    }
    private bool active;
    
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private const float SHAKE_POWER_MIN = 0.0f;
    private const float SHAKE_POWER_MAX = 0.1f;
    private const float SHAKE_RATE_MIN = 0.0f;
    private const float SHAKE_RATE_MAX = 0.2f;

    private float scale;
    private float scale_init = 1.5f;
    private float scale_step = 2.0f;

    private float time_borning;
    private float time_toGlitch;
    private float time_toGlitch_init = 0.2f;    

    private void Awake()
    {
        scale = scale_init;

        time_borning = Time.time;
        time_toGlitch = time_toGlitch_init;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetFloat("_ShakePower", SHAKE_POWER_MAX);
        spriteRenderer.material.SetFloat("_ShakeSpeed", 10.0f);
        spriteRenderer.material.SetFloat("_Intermittency", 5.0f);

        animator = GetComponent<Animator>();
        Active = true;
    }

    private void Update()
    {
        if (Active)
        {
            if (time_toGlitch > 0)
            {
                time_toGlitch -= Time.deltaTime;
            }
            else
            {
                scale -= scale_step * Time.deltaTime;
                scale = Mathf.Clamp(scale, 0, scale);
                transform.localScale = Vector3.one * scale;
                spriteRenderer.material.SetFloat("_Alpha", Mathf.PingPong((Time.time - time_borning) * 8.0f, 1));
            }
        }            
    }
}
