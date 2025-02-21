using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnTime;
    [SerializeField] private float delayBetweenGroups;
    [SerializeField] private float minDelay;
    [SerializeField] private int groupSize;
    [SerializeField] private bool difficultyCorrelation;
    [SerializeField] private GameObject enemy;
    private GameObject newEnemy;
    private int lineNumber;
    private float difficulty;
    private float timer;
    private float delay; 
    private int size;
    private List<GameObject> enemyList = new List<GameObject>();   

    public static EnemySpawner Instance { get; private set; }

    private void Awake()
    {
        Instance = this;           
    }

    private void FixedUpdate()
    {        
        if (!Globalist.Instance.CanSpawnEnemies())
        {
            return;
        }

        if (difficultyCorrelation)
        {
            difficulty = Globalist.Instance.GetDifficultyScale();
        }

        if (delay > 0)
        {
            delay -= Time.deltaTime;
            return;
        }        
        
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }
        
        lineNumber = Random.Range(1, 5);
        spawnEnemy(enemy, lineNumber);
        timer = difficultyCorrelation ? spawnTime - (difficulty - 1) / 10 : spawnTime;
        timer = timer < 0.12f ? 0.12f : timer;
        --size;
        if (size <= 0)
        {
            delay = difficultyCorrelation ? delayBetweenGroups - (difficulty - 1) / 2: delayBetweenGroups;
            delay = delay < minDelay ? minDelay : delay;
            size = difficultyCorrelation ? groupSize + (int)difficulty - 1 : groupSize;
        }
    }

    public void spawnEnemy(GameObject _enemy, int _lineNumber)
    {
        Vector2 position = new Vector2(12, -1.75f);
        switch (_lineNumber)
        {
            case 2:
                position.y = -2.65f;
                break;
            case 3:
                position.y = -3.55f;
                break;
            case 4:
                position.y = -4.4f;
                break;
        }
        
        newEnemy = Instantiate(_enemy, position, new Quaternion());  //Спауним противника и запомниемм ссылку на него (его Rigidbody2D)
        enemyList.Add(newEnemy);                                //Добавляем противника (его Rigidbody2D) в "Книжечку"
        if (enemyList[0] == null)
        {
            enemyList.RemoveAt(0); //Если противника больше нет, вычеркиваем его из "Книжечки"
        }
    }
    public void PrepareToStart()
    {      
        //Разъёбываем всех, кто записан в "Книжечку"
        foreach (GameObject enemy in enemyList) 
        {
            if (!(enemy == null)) //Нам нужны только ещё существующие противники
            {
                Destroy(enemy.gameObject); // !!! РАЗЪЁБ !!! 
            }                 
        }
        enemyList.Clear(); //Гарантируем, что "Книжечка" пуста
        timer = spawnTime;
        delay = delayBetweenGroups;
        size = groupSize;
        difficulty = Globalist.Instance.GetDifficultyScale();
    }
}
