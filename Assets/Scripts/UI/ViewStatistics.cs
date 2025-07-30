using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class ViewStatistics : MonoBehaviour
{
    [Header("View")]
    [SerializeField] TMP_Text Txt_Timer;
    [SerializeField] TMP_Text Txt_Level;
    [SerializeField] TMP_Text Txt_Score;
    [SerializeField] TMP_Text Txt_DestoryEnemy;
    [SerializeField] TMP_Text Txt_Scaling;
    [SerializeField] TMP_Text Txt_SpawnSpeed;
    [SerializeField] TMP_Text Txt_SpawnAmount;
    [SerializeField] TMP_Text Txt_Miner;
    [SerializeField] TMP_Text Txt_GoldPerMin;
    [SerializeField] TMP_Text Txt_TowerAmount;
    [SerializeField] TMP_Text Txt_TownHallHealth;
    [SerializeField] TMP_Text Txt_Gold;
    [SerializeField] Slider Slider_EXP;
    
    public Action<int> LevelUP;
    float currentEXP;
    float totalEXP;
    int currentLevel;
    int killCount;
    float enemyScaling;
    float score;
    public void Init()
    {
        currentLevel = 1;
        killCount = 0;
        totalEXP = 10;
        currentEXP = 0;
        Level_Txt(currentLevel);
        Gold_Txt();
        TowerAmount_Txt(0);
    }
    public void Timer_Txt(float currentTime)
    {
        int min = Mathf.FloorToInt(currentTime / 60);
        int sec = Mathf.FloorToInt(currentTime % 60);
        Txt_Timer.text = string.Format($"{min:D2} : {sec:D2}");
    }
    void Level_Txt(int currentLevel)
    {
        Txt_Level.text = currentLevel.ToString();
    }
    void EnemyKillCount_Txt(int killCount)
    {
        Txt_DestoryEnemy.text = killCount.ToString();
    }
    void Score_Txt(float score)
    {
        Txt_Score.text = string.Format($"{score:F0}");
    }
    public void EnemyLevel_Txt(float enemyScaling)
    {
        this.enemyScaling = enemyScaling;
        Txt_Scaling.text = enemyScaling.ToString();
    }
    public void EnemySpawnSpeed_Txt(float spawnSpeed)
    {
        Txt_SpawnSpeed.text = spawnSpeed + "ÃÊ";
    }
    public void EnemySpawnAmount_Txt(float spawnAmount)
    {
        Txt_SpawnAmount.text = spawnAmount.ToString();
    }
    public void MinerCount_Txt(int minerCount)
    {
        Txt_Miner.text = minerCount.ToString();
    }
    public void GoldPerMin_Txt(float miningGoldAmount)
    {
        Txt_GoldPerMin.text = string.Format($"{miningGoldAmount:F1} /min");
    }
    public void TowerAmount_Txt(int currentTowerAmount)
    {
        Txt_TowerAmount.text = currentTowerAmount.ToString();
    }
    public void EXP_Slider(float exp)
    {
        float getExp = exp * UpgradeData.EXPDropIncrese;
        currentEXP += getExp;
        Slider_EXP.value = currentEXP / totalEXP;

        EnemyKillCount_Txt(killCount += 1);
        if (currentEXP >= totalEXP)
        {
            currentLevel += 1;
            Level_Txt(currentLevel);
            LevelUP?.Invoke(currentLevel);
            currentEXP -= totalEXP;
            currentEXP = currentEXP > 0 ? currentEXP : 0;
            totalEXP += 12;
            Slider_EXP.value = currentEXP / totalEXP;
        }
        GetGold();
        score += getExp * enemyScaling;
        Score_Txt(score);
    }
    public void TownHallHealth_Txt()
    {
        Txt_TownHallHealth.text = string.Format($"{UpgradeData.TownHallHealth:F0}");
    }
    public void Gold_Txt()
    {
        Txt_Gold.text = string.Format($"{MinerManager.Gold:N0}");
    }
    void GetGold()
    {
        if(Random.value <= 0.5f)
        {
            MinerManager.Gold += 3;
        }
    }
    public void ResetUI()
    {
        score = 0;
        Score_Txt(score);

        CurrentUpgradeLvl upgradeData = JsonDataManager.JsonLoad<CurrentUpgradeLvl>(JsonDataManager.JsonFileName.Upgrade);
        UpgradeData.TownHallHealth = upgradeData.level[4] * 50 + 50;
        TownHallHealth_Txt();

        EnemyKillCount_Txt(0);

        Slider_EXP.value = currentEXP / totalEXP;
    }

}
