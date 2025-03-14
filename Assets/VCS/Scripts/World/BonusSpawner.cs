using UnityEngine;
using System.Collections.Generic;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;
    [SerializeField] private float luckyCoinCoolDown;
    [SerializeField] private GameObject bonus;
    private GameObject newBonus;
    private int lineNumber;
    private float timer;    
    private List<GameObject> bonusList = new List<GameObject>();

    public static BonusSpawner Instance { get; private set; }

    private void Awake()
    {
        Instance = this;                
        maxSpawnTime = maxSpawnTime <= minSpawnTime ? minSpawnTime + 1 : maxSpawnTime;
        minSpawnTime = minSpawnTime >= maxSpawnTime ? maxSpawnTime - 1 : minSpawnTime;               
    }

    private void FixedUpdate()
    {        
        if (!Globalist.Instance.canPlay())
        {
            return;
        }
        
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }
        
        lineNumber = Random.Range(1, 5);
        spawnBonus(bonus, lineNumber);
        //���� Lucky Time, �� BonusSpawner ������� ���������, ����� �������� � �������� ������
        timer = Globalist.Instance.IsLuckyTime() ? luckyCoinCoolDown : Random.Range(minSpawnTime, maxSpawnTime);
    }

    public void spawnBonus(GameObject _bonus, int _lineNumber)
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
        
        newBonus = Instantiate(_bonus, position, new Quaternion());  //������� ����� � ���������� ������ �� ���� (��� Rigidbody2D)
        bonusList.Add(newBonus);                                //��������� ����� (��� Rigidbody2D) � "��������"
        if (bonusList[0] == null)
        {
            bonusList.RemoveAt(0); //���� ������ ������ ���, ����������� ��� �� "��������"
        }
    }
    public void PrepareToStart()
    {      
        //����������� ��, ��� ���� � "��������"
        foreach (GameObject bonus in bonusList) 
        {
            if (!(bonus == null)) //��� ����� ������ ��� ������������ ������
            {
                Destroy(bonus.gameObject); // !!! ���ڨ� !!! 
            }                 
        }
        bonusList.Clear(); //�����������, ��� "��������" �����
        timer = Random.Range(minSpawnTime + 1, maxSpawnTime + 1);        
    }

    public void SetLuckyTimer()
    {
        timer = luckyCoinCoolDown;
    }
}
