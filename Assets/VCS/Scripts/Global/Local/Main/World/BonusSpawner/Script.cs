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

    public bool CoinRush { get; set; }
    private const float COINRUSH_TIMER_INIT = 5f;
    private float coinRush_timer = COINRUSH_TIMER_INIT;

    public void BonusSpawn_Delay_Reset()
    {
        var _bonusSpawn_delay = Random.Range(bonusSpawn_delay_min, bonusSpawn_delay_max);
        bonusSpawn_delay = CoinRush ? bonusSpawn_delay_coinRush : _bonusSpawn_delay;
    }

    private void Awake()
    {
        SingleOnScene = this;

        Active = true;

        //Контроль МинМакса. Будет глупо, если минимум будет больше, чем максимум
        bonusSpawn_delay_min = bonusSpawn_delay_min >= bonusSpawn_delay_max ? bonusSpawn_delay_max - 1 : bonusSpawn_delay_min;
        bonusSpawn_delay_max = bonusSpawn_delay_max <= bonusSpawn_delay_min ? bonusSpawn_delay_min + 1 : bonusSpawn_delay_max;
        bonusSpawn_delay = bonusSpawn_delay_init;
    }

    private void Update()
    {        
        if (Active)
        {
            if (bonusSpawn_delay > 0)
            {
                bonusSpawn_delay -= Time.deltaTime;
            } 
            else
            {
                int _lineNumber = Random.Range(1, 5);
                Vector2 _position = new Vector2();

                switch (_lineNumber)
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

                int _bonusArray_index = 0;

                if (!CoinRush)
                {
                    _bonusArray_index = Random.Range(0, bonusArray.Length);
                }

                Instantiate(bonusArray[_bonusArray_index], _position, new Quaternion());
                BonusSpawn_Delay_Reset();
            }

            if (CoinRush)
            {
                coinRush_timer -= Time.deltaTime;

                if (coinRush_timer <= 0)
                {
                    coinRush_timer = COINRUSH_TIMER_INIT;
                    CoinRush = false;
                }
            }
        }
    }
}
