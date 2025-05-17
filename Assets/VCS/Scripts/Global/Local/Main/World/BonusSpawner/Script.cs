using UnityEngine;

public class World_BonusSpawner : MonoBehaviour
{
    public static World_BonusSpawner Singletone { get; private set; }

    public bool Active { get; set; }

    [SerializeField] private GameObject[] bonusArray;

    private float                   bonusSpawn_delay;
    [SerializeField] private float  bonusSpawn_delay_init;
    [SerializeField] private float  bonusSpawn_delay_coinRush;
    [SerializeField] private float  bonusSpawn_delay_min;
    [SerializeField] private float  bonusSpawn_delay_max;    

    public void BonusSpawn()
    {
        int _lineNumber = Random.Range(1, 5);
        Vector2 _position = new Vector2();
        switch (_lineNumber)
        {
            case 1:
                _position = ControlScene_Entity_Main.Singletone.SpawnPoint_Line_1;
                break;
            case 2:
                _position = ControlScene_Entity_Main.Singletone.SpawnPoint_Line_2;
                break;
            case 3:
                _position = ControlScene_Entity_Main.Singletone.SpawnPoint_Line_3;
                break;
            case 4:
                _position = ControlScene_Entity_Main.Singletone.SpawnPoint_Line_4;
                break;
        }
        
        int _bonusArray_index = Random.Range(0, bonusArray.Length);

        if (ControlScene_Entity_Main.Singletone.CoinRush)
        {
            _bonusArray_index = 0;
        }
        
        Instantiate(bonusArray[_bonusArray_index], _position, new Quaternion());
    }

    public void BonusSpawn_Delay_Reset()
    {
        var _bonusSpawn_delay = Random.Range(bonusSpawn_delay_min, bonusSpawn_delay_max);
        bonusSpawn_delay = ControlScene_Entity_Main.Singletone.CoinRush ? bonusSpawn_delay_coinRush : _bonusSpawn_delay;
    }

    private void Awake()
    {
        Singletone = this;

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
                BonusSpawn();
                BonusSpawn_Delay_Reset();
            }  
        }
    }
}
