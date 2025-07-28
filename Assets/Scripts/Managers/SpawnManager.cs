using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{

    int[] spawnPoints;
    public float spawnTimer;
    float timer;
    float levelTimer;
    public Action<float> Die;
    public Action<float> EnemyLevel;
    public int EnemyAmount { get; set; }
    private void Awake()
    {
        spawnPoints = new int[4] { -10, 5, -6, 6 };
        Die += DecreaseEnemyAmount;
        UIInGameMenu.ReStart += ClearEnemy;
        UIInGameMenu.ReStart += ResetTimer;
    }
    private void Start()
    {
        //TEMPSpawnEnemy();
    }
    void ClearEnemy()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        EnemyAmount = 0;
    }


    void Update()
    {
        levelTimer += Time.deltaTime;
        SpawnTimer();

    }
    void SpawnTimer()
    {
        timer += Time.deltaTime;
        if (timer > spawnTimer)
        {
            SpawnEnemy();
            timer = 0;
        }
    }

    void SpawnEnemy()
    {
        int tier = GetUnlockedTier(); // 시간 기반 티어 계산
        int index = UnityEngine.Random.Range(0, tier + 1); // 0 ~ tier 범위 내 랜덤

        Vector3 spawnPos = GetSpawnPosition();

        GameObject enemy = PoolManager.Instance.Get(PoolEnum.Enemy, 5, spawnPos, Quaternion.identity, transform);
        BaseUnit unit = enemy.GetComponent<BaseUnit>();
        unit.Die = Die;
        unit.Init(LevelChange(levelTimer));
        EnemyAmount += 1;
    }
    Vector2 GetSpawnPosition()
    {
        Vector2 SpawnPoint = Vector2.zero;

        int rand = spawnPoints[Random.Range(0, 4)];
        if (rand == -10 || rand == 5)
        {
            SpawnPoint.x = rand;
            int YAxis = Random.Range(-6, 7);
            SpawnPoint.y = YAxis;
        }
        else if (Mathf.Abs(rand) == 6)
        {
            SpawnPoint.y = rand;
            int XAxis = Random.Range(-10, 6);
            SpawnPoint.x = XAxis;
        }

        return SpawnPoint;
    }
    int GetUnlockedTier()
    {
        return Mathf.Clamp((int)(levelTimer / 30f), 0, 10);
    }
    int LevelChange(float timer)
    {
        int level = Mathf.FloorToInt((timer + 60) / 60);
        EnemyLevel?.Invoke(level);
        return level;
    }
    void DecreaseEnemyAmount(float amount)
    {
        EnemyAmount -= 1;
    }
    void ResetTimer()
    {
        levelTimer = 0;
    }
    void TEMPSpawnEnemy()
    {
        Vector2 spawnPos1 = new Vector2(-1.3f, 3f);
        GameObject enemy1 = PoolManager.Instance.Get(PoolEnum.Enemy, 5, spawnPos1, Quaternion.identity, transform);
                                                                     
        //Vector2 spawnPos2 = new Vector2(-1f, 3f);                    
        //GameObject enemy2 = PoolManager.Instance.Get(PoolEnum.Enemy, 5, spawnPos2, Quaternion.identity, transform);
                                                                     
        //Vector2 spawnPos3 = new Vector2(-0.7f, 3f);                  
        //GameObject enemy3 = PoolManager.Instance.Get(PoolEnum.Enemy, 5, spawnPos3, Quaternion.identity, transform);
                                                                     
        //Vector2 spawnPos4 = new Vector2(-1.3f, 2.7f);                
        //GameObject enemy4 = PoolManager.Instance.Get(PoolEnum.Enemy, 5, spawnPos4, Quaternion.identity, transform);
                                                                     
        //Vector2 spawnPos5 = new Vector2(-1f, 2.7f);                  
        //GameObject enemy5 = PoolManager.Instance.Get(PoolEnum.Enemy, 5, spawnPos5, Quaternion.identity, transform);
                                                                     
        //Vector2 spawnPos6 = new Vector2(-0.7f, 2.7f);                
        //GameObject enemy6 = PoolManager.Instance.Get(PoolEnum.Enemy, 5, spawnPos6, Quaternion.identity, transform);

        enemy1.GetComponent<UnitState<RangeEnemy>>().Die = Die;
        //enemy2.GetComponent<UnitState<MeleeEnemy>>().Die = Die;
        //enemy3.GetComponent<UnitState<MeleeEnemy>>().Die = Die;
        //enemy4.GetComponent<UnitState<MeleeEnemy>>().Die = Die;
        //enemy5.GetComponent<UnitState<MeleeEnemy>>().Die = Die;
        //enemy6.GetComponent<UnitState<MeleeEnemy>>().Die = Die;

    }
}
