using UnityEngine;

public class World_Local_SceneMain_DriftSection_Enity_Parent : MonoBehaviour
{
    public static World_Local_SceneMain_DriftSection_Enity_Parent SingleOnScene { get; private set; }

    public bool Active { get; set; }

    public bool Move { get; set; }

    [SerializeField] private World_Path_Entity[] path_array;

    [SerializeField] protected World_Local_SceneMain_DriftSection_Coin path_coin_prefab;
    private const float PATH_COIN_OFS_INIT = 0.75f;
    private const float PATH_COIN_OFS_UPGRADED = 0.5f;
    private const float PATH_COIN_OFS_IMPROVED = 0.35f;
    protected float path_coin_ofs_current = PATH_COIN_OFS_INIT;

    private void Awake()
    {
        SingleOnScene = this;

        Active = true;
    }

    protected virtual void Start()
    {
        if (ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_MoreCoins_IsBought())
        {
            if (!ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_MoreCoins_IsImproved())
            {
                path_coin_ofs_current = PATH_COIN_OFS_UPGRADED;
            }
            else
            {
                path_coin_ofs_current = PATH_COIN_OFS_IMPROVED;
            }
        }

        for (var _i = 0; _i < path_array.Length; ++_i)
        {
            var _length = path_coin_ofs_current;

            while (_length <= path_array[_i].Spline_Length)
            {
                var _pos = path_array[_i].Spline_Point_Get(_length);
                Instantiate(path_coin_prefab, _pos, new Quaternion(), path_array[_i].transform.parent.transform);

                _length += path_coin_ofs_current;
            }
            
            Destroy(path_array[_i].gameObject);
        }
    }

    protected virtual void Update()
    {
        if (Active
        && Move)
        {
            transform.position += Vector3.left * World_Local_SceneMain_MovingBackground_Road.SPEED * World_General_MovingBackground_Entity.SingleOnScene.SpeedScale * Time.deltaTime;
        }
    }
}
