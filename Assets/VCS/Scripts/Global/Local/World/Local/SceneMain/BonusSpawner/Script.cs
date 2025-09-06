using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class World_Local_SceneMain_BonusSpawner : MonoBehaviour
{
    #region General

    public static World_Local_SceneMain_BonusSpawner SingleOnScene { get; private set; }

    public bool Active_General { get; set; }
    public bool Active_Local_Road { get; set; }

    [SerializeField] private GameObject bonusPrefab_up;
    [SerializeField] private GameObject bonusPrefab_coinRush;
    [SerializeField] private GameObject bonusPrefab_coin;

    #endregion

    #region Spawn

    private GameObject spawn_bonusPrefab_current;

    private float spawn_delay_start = 2f;
    private const float SPAWN_DELAY_INC = 1f;
    private const float SPAWN_DELAY_RAND = 2f;
    private float spawn_delay_current;

    private void Spawn_Delay_New()
    {
        spawn_delay_start += SPAWN_DELAY_INC;
        spawn_delay_current = spawn_delay_start + Random.Range(0, SPAWN_DELAY_RAND);
    }

    private const float SPAWN_SPAWNPOINT_LINE_X = 5.7f;
    private readonly Vector3 spawn_spawnPoint_line_1 = new Vector3(SPAWN_SPAWNPOINT_LINE_X, -0.55f, 0);
    private readonly Vector3 spawn_spawnPoint_line_2 = new Vector3(SPAWN_SPAWNPOINT_LINE_X, -0.85f, 0);
    private readonly Vector3 spawn_spawnPoint_line_3 = new Vector3(SPAWN_SPAWNPOINT_LINE_X, -1.15f, 0);
    private readonly Vector3 spawn_spawnPoint_line_4 = new Vector3(SPAWN_SPAWNPOINT_LINE_X, -1.45f, 0);
    private Vector3 spawn_spawnPoint_current;
    
    private Constants.RoadLine spawn_line_current;
    private List<Constants.RoadLine> spawn_line_list = new List<Constants.RoadLine>((int)Constants.RoadLine.size);

    private void Spawn_Line_Next()
    {
        spawn_line_list.Clear();
        spawn_line_list.Add(Constants.RoadLine.first);
        spawn_line_list.Add(Constants.RoadLine.second);
        spawn_line_list.Add(Constants.RoadLine.third);
        spawn_line_list.Add(Constants.RoadLine.fourth);
        spawn_line_list.Remove(World_Local_SceneMain_EnemySpawner.SingleOnScene.EnemySpawn_Line_Taken);
        spawn_line_current = (Constants.RoadLine)Random.Range(0, (int)spawn_line_list.Count);

        switch (spawn_line_current)
        {
            case Constants.RoadLine.first:
                spawn_spawnPoint_current = spawn_spawnPoint_line_1;
            break;

            case Constants.RoadLine.second:
                spawn_spawnPoint_current = spawn_spawnPoint_line_2;
            break;

            case Constants.RoadLine.third:
                spawn_spawnPoint_current = spawn_spawnPoint_line_3;
            break;

            case Constants.RoadLine.fourth:
                spawn_spawnPoint_current = spawn_spawnPoint_line_4;
            break;
        }
    }

    private struct Spawn_RandomBonus
    {
        public Spawn_RandomBonus(GameObject _prefab, int _priority)
        {
            prefab = _prefab;
            priority = _priority;
        }

        public GameObject prefab;
        public int priority;
    }

    private List<Spawn_RandomBonus> spawn_randomBonus_list = new List<Spawn_RandomBonus>();

    private GameObject Spawn_RandomBonus_GetPrefab()
    {
        var _sum = spawn_randomBonus_list.Sum(_t => _t.priority);
        var _randNum = Random.Range(0, _sum);
        GameObject _prefab = null;

        foreach (var _item in spawn_randomBonus_list)
        {
            if (_randNum < _item.priority)
            {
                _prefab = _item.prefab;
                break;
            }

            _randNum -= _item.priority;
        }

        return (_prefab);
    }
    
    private const int SPAWN_RANDOMBONUS_UP_PRIORITY_INIT = 1;
    private const int SPAWN_RANDOMBONUS_UP_PRIORITY_UPGRADED = 2;
    private const int SPAWN_RANDOMBONUS_COINRUSH_PRIORITY_INIT = 4;
    private const int SPAWN_RANDOMBONUS_COINRUSH_PRIORITY_UPGRADED = 8;
    private const int SPAWN_RANDOMBONUS_COIN_PRIORITY = 8;

    private bool spawn_guaranteedBonus = true;
    private List<GameObject> spawn_guaranteedBonus_list = new List<GameObject>(2);

    #endregion

    #region CoinRush

    public bool CoinRush { get; set; }
    private const float COINRUSH_GROUP_DELAY_REFRESH = 0.3f; //Задержка между группами монет во время CoinRush'а
    private float coinRush_group_delay_current = 0;
    private const int COINRUSH_GROUP_NUMBER_INIT = 5; //Кол-во групп из монеток в CoinRush'е
    private int coinRush_group_number_current = COINRUSH_GROUP_NUMBER_INIT;
    private bool coinRush_end = false;
    private const float COINRISH_END_DELAY_INIT = 1f;
    private float coinRush_end_delay_current = COINRISH_END_DELAY_INIT;

    private int coins_amount = 3; //Кол-во монет в одной группе
    public const float COINS_OFFSET = 0.15f; //Расстояние между монетами в одной группе

    private void Coins_SpawnGroup()
    {
        GameObject _SpawnNext(int _num)
        {
            return (Instantiate(bonusPrefab_coin, spawn_spawnPoint_current + Vector3.right * COINS_OFFSET * _num, new Quaternion(), transform.parent));
        }

        if (!AppScreen_Local_SceneMain_Camera_World_CameraDistortion.SingleOnScene.Material_Overlay_NormalMap_CoinRush_Active)
        {
            for (var _i = 0; _i < coins_amount; ++_i)
            {
                _SpawnNext(_i);
            }
        }
        else
        {
            for (var _i = 0; _i < coins_amount; ++_i)
            {
                _SpawnNext(_i).GetComponent<World_Local_SceneMain_Bonus_Coin>().Visible = false;
            }
        }
    }

    #endregion

    #region Prepared

    public bool Prepared { get; private set; }
    private const float PREPATED_DELAY_INIT = 0.5f;
    private float prepared_begin_delay = PREPATED_DELAY_INIT;
    private bool prepared_end = false;
    private float prepared_end_delay = PREPATED_DELAY_INIT;

    #endregion

    private void Awake()
    {
        SingleOnScene = this;

        Active_General = true;
        Active_Local_Road = true;

        spawn_delay_current = spawn_delay_start + Random.Range(0, SPAWN_DELAY_RAND);
        spawn_line_current = (Constants.RoadLine)Random.Range(0, (int)Constants.RoadLine.size);
        spawn_guaranteedBonus_list.Add(bonusPrefab_up);
        spawn_guaranteedBonus_list.Add(bonusPrefab_coinRush);

        CoinRush = false;

        Prepared = false;
    }

    private void Start()
    {
        if (ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_MoreCoins_IsBought())
        {
            ++coins_amount;

            if (ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_MoreCoins_IsImproved())
            {
                ++coins_amount;
            }
        }

        var _up_priority = SPAWN_RANDOMBONUS_UP_PRIORITY_INIT;
        var _coinRush_priority = SPAWN_RANDOMBONUS_COINRUSH_PRIORITY_INIT;

        if (ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_MoreBonuses_IsBought())
        {
            _coinRush_priority = SPAWN_RANDOMBONUS_COINRUSH_PRIORITY_UPGRADED;

            if (ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_MoreBonuses_IsImproved())
            {
                _up_priority = SPAWN_RANDOMBONUS_UP_PRIORITY_UPGRADED;
            }
        }

        spawn_randomBonus_list.Add(new Spawn_RandomBonus(bonusPrefab_up, _up_priority));
        spawn_randomBonus_list.Add(new Spawn_RandomBonus(bonusPrefab_coinRush, _coinRush_priority));
        spawn_randomBonus_list.Add(new Spawn_RandomBonus(bonusPrefab_coin, SPAWN_RANDOMBONUS_COIN_PRIORITY));
    }

    private void FixedUpdate()
    {
        if (Active_General
        && Active_Local_Road)
        {
            if (!Prepared)
            {
                if (!CoinRush)
                {
                    if (spawn_delay_current > 0)
                    {
                        spawn_delay_current -= Time.fixedDeltaTime;
                    }
                    else
                    {
                        if (spawn_guaranteedBonus)
                        {
                            var _randInd = Random.Range(0, spawn_guaranteedBonus_list.Count);
                            spawn_bonusPrefab_current = spawn_guaranteedBonus_list[_randInd];
                            spawn_guaranteedBonus_list.RemoveAt(_randInd);

                            if (spawn_guaranteedBonus_list.Count == 0)
                            {
                                spawn_guaranteedBonus = false;
                            }
                        }
                        else
                        {
                            spawn_bonusPrefab_current = Spawn_RandomBonus_GetPrefab();
                        }

                        Spawn_Delay_New();
                        Spawn_Line_Next();

                        Prepared = true;
                    }
                }
                else
                {
                    Spawn_Delay_New();
                    Spawn_Line_Next();

                    Prepared = true;
                }
            }
            else
            {
                if (prepared_begin_delay > 0)
                {
                    prepared_begin_delay -= Time.fixedDeltaTime;
                }
                else
                {
                    if (!prepared_end)
                    {
                        if (!CoinRush)
                        {
                            if (spawn_bonusPrefab_current != bonusPrefab_coin)
                            {
                                Instantiate(spawn_bonusPrefab_current, spawn_spawnPoint_current, new Quaternion(), transform.parent);
                            }
                            else
                            {
                                Coins_SpawnGroup();
                            }

                            prepared_end = true;
                        }
                        else
                        {
                            if (!coinRush_end)
                            {
                                if (coinRush_group_delay_current > 0)
                                {
                                    coinRush_group_delay_current -= Time.fixedDeltaTime;
                                }
                                else
                                {
                                    Coins_SpawnGroup();

                                    --coinRush_group_number_current;

                                    if (coinRush_group_number_current != 0)
                                    {
                                        Spawn_Line_Next();

                                        coinRush_group_delay_current = COINRUSH_GROUP_DELAY_REFRESH;
                                    }
                                    else
                                    {
                                        coinRush_end = true;
                                    }
                                }
                            }
                            else
                            {
                                if (coinRush_end_delay_current > 0)
                                {
                                    coinRush_end_delay_current -= Time.fixedDeltaTime;
                                }
                                else
                                {
                                    CoinRush = false;
                                    coinRush_group_delay_current = 0;
                                    coinRush_group_number_current = COINRUSH_GROUP_NUMBER_INIT;
                                    coinRush_end = false;  
                                    coinRush_end_delay_current = COINRISH_END_DELAY_INIT;

                                    prepared_end = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (prepared_end_delay > 0)
                        {
                            prepared_end_delay -= Time.fixedDeltaTime;
                        }
                        else
                        {
                            Prepared = false;
                            prepared_begin_delay = PREPATED_DELAY_INIT;
                            prepared_end = false;
                            prepared_end_delay = PREPATED_DELAY_INIT;
                        }
                    }
                }
            }
        }
    }
}
