using UnityEngine;

public class World_Local_SceneMain_DriftSection_Coin : MonoBehaviour
{
    public bool Active { get; set; } = true;

    [SerializeField] private World_Local_SceneMain_PopUp_Entity popUp;

    [SerializeField] private AudioClip sound;

    private Animator animator;

    private BoxCollider2D boxCollider;

    private bool coinMagnet_active = false;
    private bool coinMagnet_triggered = false;
    private float coinMagnet_radius_current = 1f;
    private const float COINMAGNET_RADIUS_UPGRADED = 4f;
    private float coinMagnet_speed_current = 1f;
    private const float COINMAGNET_SPEED_INC_INIT = 1f;
    private const float COINMAGNET_SPEED_INC_TURBO = 10f;
    private bool coinMagnet_pickUp = false;

    public void CoinMagnet_Speed_Inc_Turbo()
    {
        coinMagnet_speed_current = COINMAGNET_SPEED_INC_TURBO;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();

        boxCollider = GetComponent<BoxCollider2D>();
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

    private void Update()
    {
        if (Active)
        {
            animator.speed = ControlScene_Main.SingleOnScene.TimeDilation_Coef;

            if (coinMagnet_active)
            {
                var _pos_target = World_Local_SceneMain_Player_Entity.SingleOnScene.transform.position;
                var _dist_toTarget = (_pos_target - transform.position).magnitude;

                if (!coinMagnet_triggered)
                {
                    if (_dist_toTarget <= coinMagnet_radius_current)
                    {
                        coinMagnet_triggered = true;
                    }
                }
                else
                {
                    coinMagnet_speed_current += COINMAGNET_SPEED_INC_INIT * ControlScene_Main.SingleOnScene.TimeDilation_Coef * Time.deltaTime;

                    var _step = coinMagnet_speed_current * ControlScene_Main.SingleOnScene.TimeDilation_Coef * Time.deltaTime;

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
        else
        {
            animator.speed = 0;
        }
    }
}
