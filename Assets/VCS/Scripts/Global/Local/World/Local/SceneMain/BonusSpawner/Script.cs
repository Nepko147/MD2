using UnityEngine;

public class World_Local_SceneMain_BonusSpawner : MonoBehaviour
{
    public static World_Local_SceneMain_BonusSpawner SingleOnScene { get; private set; }

    public bool Active { get; set; }

    [SerializeField] private GameObject[] bonusArray;

    public Vector2 BonusSpawn_SpawnPoint_Line_1 { get; set; }
    public Vector2 BonusSpawn_SpawnPoint_Line_2 { get; set; }
    public Vector2 BonusSpawn_SpawnPoint_Line_3 { get; set; }
    public Vector2 BonusSpawn_SpawnPoint_Line_4 { get; set; }

    private float                   bonusSpawn_delay;
    [SerializeField] private float  bonusSpawn_delay_init;
    private float                   bonusSpawn_delay_coinRush = 0.25f; //Задержка между группами монет во время CoinRush'а
    [SerializeField] private float  bonusSpawn_delay_min;
    [SerializeField] private float  bonusSpawn_delay_max;

    private int         bonusSpawn_currentLine;
    private float       bonusSpawn_offset = 0.15f; // Расстояние между бонусами в одной группе

    private int         bonusSpawn_amount; // Текущще кол-во бонусов в группе
    private const int   BONUSSPAWN_AMOUNT_DEFAULT = 1; // Кол-во бонусов в группе по умолчанию
    private int         bonusSpawn_amount_coins = 3; // Кол-во монет в группе по умолчанию

    public bool             CoinRush 
    {
        get 
        {
            return state == BonusSpawnerMode.coinRush;
        }
        set 
        { 
            if (value)
            {
                coinRush_groups = coinRush_groups_init;
                state = BonusSpawnerMode.coinRush;
            }
            else
            {
                state = BonusSpawnerMode.standart;
            }
        } 
    }

    private int coinRush_groups_init = 3; // Кол-во групп из монеток во время CoinRush'а по умолчанию
    private int coinRush_groups; // Текущее кол-во групп из монеток во время CoinRush'а

    enum BonusSpawnerMode
    {
        coinRush,
        standart
    }

    BonusSpawnerMode state;

    public void BonusSpawn_Delay_Reset()
    {
        var _bonusSpawn_delay = Random.Range(bonusSpawn_delay_min, bonusSpawn_delay_max);
        bonusSpawn_delay = CoinRush ? bonusSpawn_delay_coinRush : _bonusSpawn_delay;
    }

    private void Awake()
    {
        SingleOnScene = this;

        Active = true;
        CoinRush = false;        
        //Контроль МинМакса. Будет глупо, если минимум будет больше, чем максимум
        bonusSpawn_delay_min = bonusSpawn_delay_min >= bonusSpawn_delay_max ? bonusSpawn_delay_max - 1 : bonusSpawn_delay_min;
        bonusSpawn_delay_max = bonusSpawn_delay_max <= bonusSpawn_delay_min ? bonusSpawn_delay_min + 1 : bonusSpawn_delay_max;
        bonusSpawn_delay = bonusSpawn_delay_init;
        bonusSpawn_currentLine = Random.Range(1, 5);
    }

    private void Start()
    {
        var _upgradeBonus = 0;
        if (ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_MoreCoins_IsBought())
        {
            ++_upgradeBonus;
            if (ControlPers_DataHandler.SingleOnScene.ProgressData_Upgrade_MoreCoins_IsImproved())
            {
                ++_upgradeBonus;
            }
        }
        coinRush_groups_init += _upgradeBonus;
        coinRush_groups = coinRush_groups_init; // Колво групп из монеток с учётом апгрейда
        bonusSpawn_amount_coins += _upgradeBonus; // Колво монеток в группе с учётом апгрейда
    }

    private void FixedUpdate()
    {
        if (Active)
        {
            if (bonusSpawn_delay > 0)
            {
                bonusSpawn_delay -= Time.deltaTime;
            }
            else
            {
                int _bonusArray_index = 0;

                switch (state)
                {                    
                    case BonusSpawnerMode.coinRush:                        
                        
                        // При CoinRush'е всегда выбираем соседнюю линию
                        if (bonusSpawn_currentLine <= 1)
                        {
                            bonusSpawn_currentLine = 2;
                        }
                        else
                        {
                            if (bonusSpawn_currentLine >= 4)
                            {
                                bonusSpawn_currentLine = 3;
                            }
                            else
                            {
                                // Случайно выбираем линию выше или ниже
                                var _isAddiсtion = Random.value > 0.5f;
                                if (_isAddiсtion)
                                {
                                    ++bonusSpawn_currentLine;
                                }
                                else
                                {
                                    --bonusSpawn_currentLine;
                                }
                            }
                        }

                        if (coinRush_groups > 0)
                        {
                            bonusSpawn_amount = bonusSpawn_amount_coins;
                            coinRush_groups -= 1;
                        }
                        else
                        {
                            bonusSpawn_amount = 0;
                            if (!AppScreen_Local_SceneMain_Camera_World_CameraDistortion.SingleOnScene.NormalMapMix_Material_NormalMap_CoinRush_Active)
                            {                                
                                CoinRush = false;
                            }
                        } 

                        break;

                    case BonusSpawnerMode.standart:

                        bonusSpawn_currentLine = Random.Range(1, 5); 

                        _bonusArray_index = Random.Range(0, bonusArray.Length);

                        switch (_bonusArray_index)
                        {
                            case 0:
                                bonusSpawn_amount = bonusSpawn_amount_coins;
                                break;
                            case > 0:
                                bonusSpawn_amount = BONUSSPAWN_AMOUNT_DEFAULT;
                                break;
                        }

                        break;
                }

                Vector2 _newPosition = Vector2.zero;

                switch (bonusSpawn_currentLine)
                {
                    case 1:
                        _newPosition = BonusSpawn_SpawnPoint_Line_1;
                        break;

                    case 2:
                        _newPosition = BonusSpawn_SpawnPoint_Line_2;
                        break;

                    case 3:
                        _newPosition = BonusSpawn_SpawnPoint_Line_3;
                        break;

                    case 4:
                        _newPosition = BonusSpawn_SpawnPoint_Line_4;
                        break;
                }

                for (int _i = 0; _i < bonusSpawn_amount; ++_i)
                {
                    var _offsetPosition = _newPosition + Vector2.right * bonusSpawn_offset * _i;
                    var _bonus = Instantiate(bonusArray[_bonusArray_index], _offsetPosition, new Quaternion());
                    if (AppScreen_Local_SceneMain_Camera_World_CameraDistortion.SingleOnScene.NormalMapMix_Material_NormalMap_CoinRush_Active)
                    {
                        _bonus.GetComponent<World_Local_SceneMain_Bonus_Coin>().MakeInvisible();
                    }
                }

                BonusSpawn_Delay_Reset();
            }
        }
    }
}
