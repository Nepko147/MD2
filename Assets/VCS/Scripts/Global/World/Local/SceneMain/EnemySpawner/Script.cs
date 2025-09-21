using System.Collections.Generic;
using UnityEngine;
using Utils;

public class World_Local_SceneMain_EnemySpawner : MonoBehaviour
{
    public static World_Local_SceneMain_EnemySpawner SingleOnScene { get; private set; }
    
    public bool Active_General { get; set; }
    public bool Active_Local_Road { get; set; }

    [SerializeField] private World_Local_SceneMain_Enemy_Entity[] enemyArray;

    private const float ENEMYSPAWN_SPAWNPOINT_LINE_X = 5.7f;
    private readonly Vector3 EnemySpawn_SpawnPoint_Line_1 = new Vector3(ENEMYSPAWN_SPAWNPOINT_LINE_X, -0.55f, 0);
    private readonly Vector3 EnemySpawn_SpawnPoint_Line_2 = new Vector3(ENEMYSPAWN_SPAWNPOINT_LINE_X, -0.85f, 0);
    private readonly Vector3 EnemySpawn_SpawnPoint_Line_3 = new Vector3(ENEMYSPAWN_SPAWNPOINT_LINE_X, -1.15f, 0);
    private readonly Vector3 EnemySpawn_SpawnPoint_Line_4 = new Vector3(ENEMYSPAWN_SPAWNPOINT_LINE_X, -1.45f, 0);

    private float enemySpawn_wave_delay_refresh = 1f;
    private const float ENEMYSPAWN_WAVE_DELAY_REFRESH_DEC = 0.1f;
    private const float ENEMYSPAWN_WAVE_DELAY_REFRESH_MIN = 0;
    private float enemySpawn_wave_delay_current;

    private float enemySpawn_wave_enemyDelay_current = 0;
    private float enemySpawn_wave_enemyDelay_refresh = 1f;
    private const float ENEMYSPAWN_WAVE_ENEMYDELAY_REFRESH_DEC = 0.1f;
    private const float ENEMYSPAWN_WAVE_ENEMYDELAY_REFRESH_MIN = 0.25f;

    private const int ENEMYSPAWN_WAVE_SIZE_INIT = 3;
    private int enemySpawn_wave_size_current = ENEMYSPAWN_WAVE_SIZE_INIT;
    private float enemySpawn_wave_size_refresh_current = ENEMYSPAWN_WAVE_SIZE_INIT;
    private float ENEMYSPAWN_WAVE_SIZE_REFRESH_INC = 0.5f;
    private float ENEMYSPAWN_WAVE_SIZE_REFRESH_MAX = 10f;

    public Constants.RoadLine EnemySpawn_Line_Taken { get; set; }
    private List<Constants.RoadLine> enemySpawn_line_list = new List<Constants.RoadLine>((int)Constants.RoadLine.size);

    private void Awake()
    {
        SingleOnScene = this;

        Active_General = true;
        Active_Local_Road = true;

        enemySpawn_wave_delay_current = enemySpawn_wave_delay_refresh;

        EnemySpawn_Line_Taken = (Constants.RoadLine)Random.Range(0, (int)Constants.RoadLine.size);
    }
    
    private void FixedUpdate()
    {
        if (Active_General
        && Active_Local_Road
        && !World_Local_SceneMain_BonusSpawner.SingleOnScene.CoinRush)
        {
            if (enemySpawn_wave_delay_current > 0)
            {
                enemySpawn_wave_delay_current -= Time.fixedDeltaTime;
            } 
            else
            {
                if (enemySpawn_wave_enemyDelay_current > 0)
                {
                    enemySpawn_wave_enemyDelay_current -= Time.fixedDeltaTime;                   
                } 
                else
                {
                    if (!World_Local_SceneMain_BonusSpawner.SingleOnScene.Prepared)
                    {
                        enemySpawn_line_list.Clear();
                        enemySpawn_line_list.Add(Constants.RoadLine.first);
                        enemySpawn_line_list.Add(Constants.RoadLine.second);
                        enemySpawn_line_list.Add(Constants.RoadLine.third);
                        enemySpawn_line_list.Add(Constants.RoadLine.fourth);
                        enemySpawn_line_list.Remove(EnemySpawn_Line_Taken);

                        var _randInd = Random.Range(0, enemySpawn_line_list.Count);
                        EnemySpawn_Line_Taken = enemySpawn_line_list[_randInd];
                        
                        var _position = EnemySpawn_SpawnPoint_Line_1;
                        
                        switch (EnemySpawn_Line_Taken)
                        {
                            case Constants.RoadLine.second:
                                _position = EnemySpawn_SpawnPoint_Line_2;
                            break;

                            case Constants.RoadLine.third:
                                _position = EnemySpawn_SpawnPoint_Line_3;
                            break;

                            case Constants.RoadLine.fourth:
                                _position = EnemySpawn_SpawnPoint_Line_4;
                            break;
                        }

                        var _ind = Random.Range(0, enemyArray.Length);
                        Instantiate(enemyArray[_ind], _position, new Quaternion(), transform.parent);

                        --enemySpawn_wave_size_current;
                        
                        if (enemySpawn_wave_size_current > 0)
                        {
                            enemySpawn_wave_enemyDelay_current = enemySpawn_wave_enemyDelay_refresh;
                        }
                        else
                        {
                            enemySpawn_wave_delay_refresh = Mathf.Clamp(enemySpawn_wave_delay_refresh - ENEMYSPAWN_WAVE_DELAY_REFRESH_DEC, ENEMYSPAWN_WAVE_DELAY_REFRESH_MIN, enemySpawn_wave_delay_refresh);
                            enemySpawn_wave_delay_current = enemySpawn_wave_delay_refresh;

                            enemySpawn_wave_enemyDelay_refresh = Mathf.Clamp(enemySpawn_wave_enemyDelay_refresh - ENEMYSPAWN_WAVE_ENEMYDELAY_REFRESH_DEC, ENEMYSPAWN_WAVE_ENEMYDELAY_REFRESH_MIN, enemySpawn_wave_enemyDelay_refresh);
                            enemySpawn_wave_enemyDelay_current = enemySpawn_wave_enemyDelay_refresh;

                            enemySpawn_wave_size_refresh_current = Mathf.Clamp(enemySpawn_wave_size_refresh_current + ENEMYSPAWN_WAVE_SIZE_REFRESH_INC, enemySpawn_wave_size_refresh_current, ENEMYSPAWN_WAVE_SIZE_REFRESH_MAX);
                            enemySpawn_wave_size_current = (int)Mathf.Floor(enemySpawn_wave_size_refresh_current);
                        }
                    }
                }
            }
        }
    }
}
