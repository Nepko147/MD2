using UnityEngine;

public class World_EnemySpawner : MonoBehaviour
{
    public static World_EnemySpawner Singletone { get; private set; }
    
    public bool Active { get; set; }

    [SerializeField] private GameObject[] enemyArray;
    
    private float                  enemySpawn_wave_enemyDelay;
    [SerializeField] private float enemySpawn_wave_enemyDelay_init;
    [SerializeField] private float enemySpawn_wave_enemyDelay_init_decreaseCf;
    [SerializeField] private float enemySpawn_wave_enemyDelay_init_min; // ~ 0.12f

    private float                  enemySpawn_wave_delay;
    [SerializeField] private float enemySpawn_wave_delay_init;
    [SerializeField] private float enemySpawn_wave_delay_init_decreaseCf;
    [SerializeField] private float enemySpawn_wave_delay_init_min;

    private int                    enemySpawn_wave_size;
    [SerializeField] private int   enemySpawn_wave_size_init;
    private float                  enemySpawn_wave_size_counter;
    [SerializeField] private float enemySpawn_wave_size_counter_increaseCf;
    [SerializeField] private int   enemySpawn_wave_size_counter_max;    

    public void spawnEnemy()
    {
        int _lineNumber = Random.Range(1, 5);
        var _sortingOrder = 0;
        Vector2 _position = new Vector2();

        switch (_lineNumber)
        {
            case 1:
                _position = ControlScene_Entity_Main.Singletone.SpawnPoint_Line_1;
                _sortingOrder = ControlScene_Entity_Main.LINE_1_SORTINGORDER_ENEMY;
                break;
            case 2:
                _position = ControlScene_Entity_Main.Singletone.SpawnPoint_Line_2;
                _sortingOrder = ControlScene_Entity_Main.LINE_2_SORTINGORDER_ENEMY;
                break;
            case 3:
                _position = ControlScene_Entity_Main.Singletone.SpawnPoint_Line_3;
                _sortingOrder = ControlScene_Entity_Main.LINE_3_SORTINGORDER_ENEMY;
                break;
            case 4:
                _position = ControlScene_Entity_Main.Singletone.SpawnPoint_Line_4;
                _sortingOrder = ControlScene_Entity_Main.LINE_4_SORTINGORDER_ENEMY;
                break;
        }
        
        int _enemyArray_index = Random.Range(0, enemyArray.Length);
        var _newEnemy = Instantiate(enemyArray[_enemyArray_index], _position, new Quaternion(), transform.parent);
        _newEnemy.GetComponent<SpriteRenderer>().sortingOrder = _sortingOrder;
    }

    private void Awake()
    {
        Singletone = this;

        Active = true;

        enemySpawn_wave_size = enemySpawn_wave_size_init;
        enemySpawn_wave_size_counter = enemySpawn_wave_size_init;
    }

    private void FixedUpdate()
    {        
        if (Active && !ControlScene_Entity_Main.Singletone.CoinRush)
        {
            if (enemySpawn_wave_delay > 0)
            {
                enemySpawn_wave_delay -= Time.deltaTime;                
            } 
            else
            {
                if (enemySpawn_wave_enemyDelay > 0)
                {
                    enemySpawn_wave_enemyDelay -= Time.deltaTime;                   
                } 
                else
                {
                    spawnEnemy();                    
                    enemySpawn_wave_enemyDelay = enemySpawn_wave_enemyDelay_init;

                    --enemySpawn_wave_size;

                    if (enemySpawn_wave_size <= 0)
                    {
                        enemySpawn_wave_delay_init = Mathf.Clamp(enemySpawn_wave_delay_init - enemySpawn_wave_delay_init_decreaseCf, enemySpawn_wave_delay_init_min, enemySpawn_wave_delay_init);
                        enemySpawn_wave_delay = enemySpawn_wave_delay_init;

                        enemySpawn_wave_enemyDelay_init = Mathf.Clamp(enemySpawn_wave_enemyDelay_init - enemySpawn_wave_enemyDelay_init_decreaseCf, enemySpawn_wave_enemyDelay_init_min, enemySpawn_wave_enemyDelay);

                        enemySpawn_wave_size_counter += enemySpawn_wave_size_counter_increaseCf;
                        var _enemySpawn_wave_size_counter_clamp = Mathf.Clamp(enemySpawn_wave_size_counter, enemySpawn_wave_size_counter, enemySpawn_wave_size_counter_max);
                        var _enemySpawn_wave_size_counter_clamp_floor = Mathf.Floor(_enemySpawn_wave_size_counter_clamp);
                        enemySpawn_wave_size = (int)_enemySpawn_wave_size_counter_clamp_floor;
                    }
                }
            }
        }
    }
}
