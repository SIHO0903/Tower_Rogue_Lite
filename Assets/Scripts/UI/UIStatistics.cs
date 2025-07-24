using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStatistics : MonoBehaviour
{
    [Header("Model")]
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] SelectManager selectManager;
    [SerializeField] MinerManager minerManager;
    [Header("View")]
    [SerializeField] ViewStatistics view;
    float timer;
    int towerCount = 0;
    private void Start()
    {
        view.Init();
        spawnManager.Die += view.EXP_Slider;
        view.LevelUP += selectManager.GetRandomSelect;
        spawnManager.EnemyLevel += view.EnemyLevel_Txt;
        view.EnemySpawnSpeed_Txt(spawnManager.spawnTimer);
        selectManager.TowerInstalled += () =>
        {
            towerCount++;
            view.TowerAmount_Txt(towerCount);
        };
        minerManager.OnGoldUpdate += UpdateMinerUI;
        UIInGameMenu.ReStart += ResetTimer;
        UIInGameMenu.ReStart += view.Init;
        UIInGameMenu.ReStart += view.ResetUI;

    }
    void ResetTimer()
    {
        timer = 0;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        view.Timer_Txt(timer);
        view.EnemySpawnAmount_Txt(spawnManager.EnemyAmount);
        view.TownHallHealth_Txt();
        view.Gold_Txt();
    }
    void UpdateMinerUI()
    {
        view.MinerCount_Txt(minerManager.MinerAmount);
        view.GoldPerMin_Txt(minerManager.MiningPerMinute());
    }
}
