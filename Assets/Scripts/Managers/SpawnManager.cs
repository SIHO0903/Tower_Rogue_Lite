using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{

    int[] spawnPoints;
    [HideInInspector]public float spawnTimer;
    float timer;
    float levelTimer;
    public Action<float> Die;
    public Action<float> EnemyLevel;
    public int EnemyAmount { get; set; }

    bool isReduced_180 = false;
    bool isReduced_300 = false;
    bool isReduced_480 = false;
    private void Awake()
    {
        spawnPoints = new int[4] { -10, 5, -6, 6 };
        Die += DecreaseEnemyAmount;
        UIInGameMenu.ReStart += ClearEnemy;
        UIInGameMenu.ReStart += ResetTimer;
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
            DecreaseSpawnTimer();
        }
    }

    void SpawnEnemy()
    {
        int tier = GetUnlockedTier(); // 시간 기반 티어 계산
        int index = UnityEngine.Random.Range(0, tier + 1); // 0 ~ tier 범위 내 랜덤

        Vector3 spawnPos = GetSpawnPosition();

        GameObject enemy = PoolManager.Instance.Get(PoolEnum.Enemy, index, spawnPos, Quaternion.identity, transform);
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
        spawnTimer = 4f;
    }
    void DecreaseSpawnTimer()
    {
        if (!isReduced_180 && levelTimer >= 180f)
        {
            spawnTimer -= 0.5f;
            isReduced_180 = true;
        }
        if (!isReduced_300 && levelTimer >= 300f)
        {
            spawnTimer -= 0.5f;
            isReduced_300 = true;
        }
        if (!isReduced_480 && levelTimer >= 480f)
        {
            spawnTimer -=  0.5f;
            isReduced_480 = true;
        }
    }
    
}
