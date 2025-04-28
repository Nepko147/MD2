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
    [SerializeField] private GameObject lensFlare;
    private float difficulty;
    private float timer;
    private float delay; 
    private int size;
    private List<GameObject> delObjList = new List<GameObject>();   

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
        
        int lineNumber = Random.Range(1, 5);
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
        Vector2 enemy_position = new Vector2(12, -1.75f);
        Vector2 lensFlare_position = new Vector2(11, -1.75f);
        switch (_lineNumber)
        {
            case 2:
                enemy_position.y = -2.65f;
                lensFlare_position.y = -2.65f;
                break;
            case 3:
                enemy_position.y = -3.55f;
                lensFlare_position.y = -3.55f;
                break;
            case 4:
                enemy_position.y = -4.4f;
                lensFlare_position.y = -4.4f;
                break;
        }
        GameObject newLensFlare = Instantiate(lensFlare, lensFlare_position, new Quaternion(), transform.parent);
        delObjList.Add(newLensFlare);
        GameObject newEnemy = Instantiate(_enemy, enemy_position, new Quaternion(), transform.parent);  //Спауним противника и запомниемм ссылку на него (его Rigidbody2D)
        delObjList.Add(newEnemy);                                //Добавляем противника (его Rigidbody2D) в "Книжечку"
        if (delObjList[0] == null)
        {
            delObjList.RemoveAt(0); //Если противника больше нет, вычеркиваем его из "Книжечки"
        }
    }
    public void PrepareToStart()
    {      
        //Разъёбываем всех, кто записан в "Книжечку"
        foreach (GameObject obj in delObjList) 
        {
            if (!(obj == null)) //Нам нужны только ещё существующие противники
            {
                Destroy(obj.gameObject); // !!! РАЗЪЁБ !!! 
            }                 
        }
        delObjList.Clear(); //Гарантируем, что "Книжечка" пуста
        timer = spawnTime;
        delay = delayBetweenGroups;
        size = groupSize;
        difficulty = Globalist.Instance.GetDifficultyScale();
    }
}
