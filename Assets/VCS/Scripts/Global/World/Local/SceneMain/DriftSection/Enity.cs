using UnityEngine;

public class World_Local_SceneMain_DriftSection_Enity : MonoBehaviour
{
    public static World_Local_SceneMain_DriftSection_Enity SingleOnScene { get; private set; }

    public bool Active { get; set; }

    public bool Move { get; set; }

    [SerializeField] private World_Path_Entity path;
    [SerializeField] private World_Local_SceneMain_DriftSection_Coin path_coin;
    
    private const float PATH_COIN_OFS_INIT = 0.75f;
    private const float PATH_COIN_OFS_UPGRADED = 0.5f;
    private const float PATH_COIN_OFS_IMPROVED = 0.35f;

    private void Awake()
    {
        SingleOnScene = this;

        Active = true;
    }

    private void Start()
    {
        var _distance_current = 0f;
        var _distance_step = PATH_COIN_OFS_INIT;

        if (ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_MoreCoins_IsBought())
        {
            if (!ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_MoreCoins_IsImproved())
            {
                _distance_step = PATH_COIN_OFS_UPGRADED;
            }
            else
            {
                _distance_step = PATH_COIN_OFS_IMPROVED;
            }
        }

        while (_distance_current <= path.Spline_Length)
        {
            var _pos = path.Spline_Point_Get(_distance_current);
            Instantiate(path_coin, _pos, new Quaternion(), transform);

            _distance_current += _distance_step;
        }
        
        Destroy(path.gameObject);
    }

    private void Update()
    {
        if (Active
        && Move)
        {
            transform.position += Vector3.left * World_Local_SceneMain_MovingBackground_Road.SPEED * World_Local_SceneMain_MovingBackground_Entity.SingleOnScene.SpeedScale * Time.deltaTime;
        }
    }
}
