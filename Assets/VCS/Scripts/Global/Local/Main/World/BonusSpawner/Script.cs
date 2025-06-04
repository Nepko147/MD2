using UnityEngine;

public class World_BonusSpawner : MonoBehaviour
{
    public static World_BonusSpawner SingleOnScene { get; private set; }

    public bool Active { get; set; }

    [SerializeField] private GameObject[] bonusArray;

    public Vector2 BonusSpawn_SpawnPoint_Line_1 { get; set; }
    public Vector2 BonusSpawn_SpawnPoint_Line_2 { get; set; }
    public Vector2 BonusSpawn_SpawnPoint_Line_3 { get; set; }
    public Vector2 BonusSpawn_SpawnPoint_Line_4 { get; set; }

    private float                   bonusSpawn_delay;
    [SerializeField] private float  bonusSpawn_delay_init;
    [SerializeField] private float  bonusSpawn_delay_coinRush;
    [SerializeField] private float  bonusSpawn_delay_min;
    [SerializeField] private float  bonusSpawn_delay_max;

    private int         bonusSpawn_currentLine;

    private int         bonusSpawn_amount;
    private const int   BONUSSPAWN_AMOUNT_DEFAULT = 1;
    
    public bool             CoinRush { get; set; }    
    [SerializeField] int    coinRush_amount = 5;    
    private const float     COINRUSH_TIMER_INIT = 5f;
    private float           coinRush_timer = COINRUSH_TIMER_INIT;    

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
                var _bonusArray_index = 0;
                GameObject _bonus;

                if (CoinRush) // Работа спавнера при CoinRush'е
                {
                    if (bonusSpawn_amount <= 0)
                    {
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
                        bonusSpawn_amount = coinRush_amount;
                    }

                    _bonus = Instantiate(bonusArray[_bonusArray_index]);
                    
                    if (Universal_DistortionDynamic.SingleOnScene.NormalMapMix_Material_NormalMap_CoinRush_Active)
                    {
                        _bonus.GetComponent<World_Bonus_Coin>().MakeInvisible();
                    }
                    
                    coinRush_timer -= bonusSpawn_delay_coinRush;

                    if (coinRush_timer <= 0)
                    {
                        coinRush_timer = COINRUSH_TIMER_INIT;
                        CoinRush = false;
                    }
                }
                else  // Работа спавнера в стандартном режиме
                {
                    bonusSpawn_amount = BONUSSPAWN_AMOUNT_DEFAULT;
                    _bonusArray_index = Random.Range(0, bonusArray.Length);
                    _bonus = Instantiate(bonusArray[_bonusArray_index]);
                }

                Vector2 _position = new Vector2();

                switch (bonusSpawn_currentLine)
                {
                    case 1:
                        _position = BonusSpawn_SpawnPoint_Line_1;
                        break;

                    case 2:
                        _position = BonusSpawn_SpawnPoint_Line_2;
                        break;

                    case 3:
                        _position = BonusSpawn_SpawnPoint_Line_3;
                        break;

                    case 4:
                        _position = BonusSpawn_SpawnPoint_Line_4;
                        break;
                }

                _bonus.transform.position = _position;
                //Instantiate(bonusArray[_bonusArray_index], _position, new Quaternion());
                BonusSpawn_Delay_Reset();
                --bonusSpawn_amount;
            }
        }
    }
}
