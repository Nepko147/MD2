using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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

    protected void People_Spawn(World_Local_SceneMain_DriftSection_People _prefab, GameObject[] _positions)
    {
        void _Instantiate(int _ind)
        {
            Instantiate(_prefab, _positions[_ind].transform.position, Quaternion.identity, _positions[_ind].transform.parent);
        }

        var _ind = Random.Range(0, _positions.Length);
        _Instantiate(_ind);

        if (Random.Range(0, 2) == 0)
        {
            var _ind_list = new List<int>();

            for (var _i = 0; _i < _positions.Length; ++_i)
            {
                _ind_list.Add(_i);
            }

            _ind_list.Remove(_ind);
            var _ind_list_rndInd = Random.Range(0, _ind_list.Count);
            _ind = _ind_list.ElementAt(_ind_list_rndInd);
            _Instantiate(_ind);
        }

        for (var _i = 0; _i < _positions.Length; ++_i)
        {
            Destroy(_positions[_i].gameObject);
        }
    }

    protected virtual void Awake()
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
            var _length_current = 0f;
            var _length_max = path_array[_i].Spline_Length - path_coin_ofs_current;

            while (_length_current <= _length_max)
            {
                var _pos = path_array[_i].Spline_Point_Get(_length_current);
                Instantiate(path_coin_prefab, _pos, new Quaternion(), path_array[_i].transform.parent.transform);

                _length_current += path_coin_ofs_current;
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
