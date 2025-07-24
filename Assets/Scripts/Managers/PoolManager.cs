using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolEnum
{
    TownHall,
    AttackTower,
    SynergyTower,
    Enemy,
    Projectile,
    Projectile_Effect,
    EnemyProjectile,
    Etc,
}

[System.Serializable]
public class PoolType
{
    public PoolEnum poolType;
    public GameObject[] prefabs;
    [HideInInspector] public List<GameObject>[] pools;

    public Dictionary<string, int> poolNames;
}

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] List<PoolType> objectDatas = new List<PoolType>();

    public override void Awake()
    {
        for (int dataIdx = 0; dataIdx < objectDatas.Count; dataIdx++)
        {
            PoolType poolData = objectDatas[dataIdx];
            poolData.pools = new List<GameObject>[poolData.prefabs.Length];
            poolData.poolNames = new Dictionary<string, int>();

            for (int i = 0; i < poolData.pools.Length; i++)
            {
                poolData.pools[i] = new List<GameObject>();
            }
            
            for (int i = 0; i < poolData.pools.Length; i++)
            {
                poolData.poolNames.Add(poolData.prefabs[i].name, i);

            }

        }

    }
    public GameObject Get(PoolEnum prefabType, string name, Vector3 startPos, Quaternion rot, Transform parent = null)
    {
        if (parent == null) parent = this.transform;

        PoolType poolData = objectDatas[(int)prefabType];
        int index = poolData.poolNames[name];
        List<GameObject> poolList = poolData.pools[index];

        foreach (GameObject obj in poolList)
        {
            if (!obj.activeSelf)
            {
                obj.transform.SetPositionAndRotation(startPos, rot);
                obj.SetActive(true);
                return obj;
            }
        }

        GameObject prefab = poolData.prefabs[index];
        GameObject newObj = Instantiate(prefab, startPos, rot, parent);
        poolList.Add(newObj);
        return newObj;
    }
    public GameObject Get(PoolEnum prefabTypes, int prefabIndex, Vector3 startPos, Quaternion rot, Transform parent = null)
    {
        if (parent == null) parent = this.transform;

        var poolData = objectDatas[(int)prefabTypes];

        if (prefabIndex < 0 || prefabIndex >= poolData.prefabs.Length)
        {
            Debug.LogWarning($"[PoolManager] Àß¸øµÈ ÇÁ¸®ÆÕ ÀÎµ¦½º {prefabIndex} (type: {prefabTypes})");
            return null;
        }

        var poolList = poolData.pools[prefabIndex];

        foreach (var obj in poolList)
        {
            if (!obj.activeSelf)
            {
                obj.transform.SetPositionAndRotation(startPos, rot);
                obj.SetActive(true);
                return obj;
            }
        }

        GameObject prefab = poolData.prefabs[prefabIndex];
        GameObject newObj = Instantiate(prefab, startPos, rot, parent);
        poolList.Add(newObj);
        return newObj;
    }
    public void DeactivateAllChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

}

