using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerManager : MonoBehaviour
{
    public int MinerAmount { get; set; } = 0;
    float miningSpeed = 0.016f;
    float miningTimer;
    public static float Gold { get; set; } = 0;

    public Action OnGoldUpdate;
    void Awake()
    {
        LoadMinerStatus();
        GoldSaveData goldData = Constants.JsonLoad<GoldSaveData>(Constants.JsonFileName.Gold);
        if (goldData == null)
        {
            goldData = new GoldSaveData();
            Constants.JsonSave(goldData, Constants.JsonFileName.Gold);
        }
        Gold = goldData.gold;
    }

    void LoadMinerStatus()
    {
        CurrentUpgradeLvl upgradeData = Constants.JsonLoad<CurrentUpgradeLvl>(Constants.JsonFileName.Upgrade);
        if (upgradeData == null)
        {
            upgradeData = new CurrentUpgradeLvl();
            upgradeData.Init();
            Constants.JsonSave(upgradeData, Constants.JsonFileName.Upgrade);
        }
        MinerAmount = upgradeData.level[2];
        miningSpeed = upgradeData.level[3] * 0.016f;

    }

    void Update()
    {
        miningTimer += Time.deltaTime;
        if (miningTimer >= 1f)
        {
            MiningPerSecond();
            miningTimer = 0f;
            OnGoldUpdate?.Invoke();
        }
    }
    void MiningPerSecond()
    {
        Gold += MinerAmount * miningSpeed;
        OnGoldUpdate?.Invoke();
    }

    public float MiningPerMinute()
    {
        Debug.Log("MiningPerMinute : " + Mathf.Round(MinerAmount * miningSpeed * 60 * 10) / 10);
        return Mathf.Round(MinerAmount * miningSpeed * 60 * 10) / 10;
    }
    public static bool CanPayGold(float amount)
    {
        if (Gold < amount)
            return false;
        else
        {
            Gold -= amount;

            return true;
        }
    }
    public void AddMiner()
    {
        MinerAmount += 1;
        OnGoldUpdate?.Invoke();
    }
    public void IncreaseMinerSpeed()
    {
        miningSpeed += 0.016f;
        OnGoldUpdate?.Invoke();
    }
}
