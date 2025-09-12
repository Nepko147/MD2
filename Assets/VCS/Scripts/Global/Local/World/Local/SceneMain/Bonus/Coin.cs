using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class World_Local_SceneMain_Bonus_Coin : World_Local_SceneMain_Bonus_Parent
{
    private SpriteRenderer spriteRenderer;

    private bool visible = true;
    public bool Visible
    {
        get
        {
            return (visible);
        }
        set
        {
            visible = value;
            spriteRenderer.enabled = value;
        }
    }

    private bool coinMagnet_active = false;
    private bool coinMagnet_triggered = false;
    private float coinMagnet_radius_current = 0.75f;
    private const float COINMAGNET_RADIUS_UPGRADED = 3f;
    private float coinMagnet_speed_current = 1f;
    private const float COINMAGNET_SPEED_INC_INIT = 4f;
    private const float COINMAGNET_SPEED_INC_TURBO = 10f;
    private bool coinMagnet_pickUp = false;

    public void CoinMagnet_Trigger()
    { 
        coinMagnet_triggered = true;
        destroy_position_x = float.NegativeInfinity;
    }

    public void CoinMagnet_Speed_Inc_Turbo()
    {
        coinMagnet_speed_current = COINMAGNET_SPEED_INC_TURBO;
    }

    protected override void Awake()
    {
        base.Awake();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_CoinMagnet_IsBought())
        {
            coinMagnet_active = true;
        }
        else
        {
            if (ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_CoinMagnet_IsImproved())
            {
                coinMagnet_radius_current = COINMAGNET_RADIUS_UPGRADED;
            }
        }
    }

    protected override void Update()
    {
        base.Update();

        if (Active
        && visible)
        {
            if (coinMagnet_active)
            {
                var _pos_target = World_Local_SceneMain_Player_Entity.SingleOnScene.transform.position;
                var _dist_toTarget = (_pos_target - transform.position).magnitude;

                if (!coinMagnet_triggered)
                {
                    if (_dist_toTarget <= coinMagnet_radius_current)
                    {
                        CoinMagnet_Trigger();
                    }
                }
                else
                {
                    coinMagnet_speed_current += COINMAGNET_SPEED_INC_INIT * Time.deltaTime;

                    var _step = coinMagnet_speed_current * Time.deltaTime;

                    transform.position = Vector3.MoveTowards(transform.position, _pos_target, _step);

                    if (_dist_toTarget <= _step)
                    {
                        coinMagnet_pickUp = true;
                    }
                }
            }

            if (boxCollider.bounds.Intersects(World_Local_SceneMain_Player_Entity.SingleOnScene.Collision_Bonus.bounds)
            || coinMagnet_pickUp)
            {
                ControlPers_AudioMixer_Sounds.SingleOnScene.Play(sound);

                World_Local_SceneMain_Player_Entity.SingleOnScene.TakeCoin();

                var _popUp = Instantiate(popUp, transform.position, transform.rotation, transform.parent);
                _popUp.Display_AsCoin();

                Destroy(gameObject);
            }
        }
    }
 }
