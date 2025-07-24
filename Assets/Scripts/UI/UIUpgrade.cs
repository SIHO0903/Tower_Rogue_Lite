using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpgrade : MonoBehaviour
{
    [Header("Model")]
    [SerializeField] MinerManager minerManager;
    [SerializeField] TowerManager towerManager;
    [Header("View")]
    [SerializeField] ViewUpgrade viewUpgrade;

    private void Start()
    {
        InitData();
        viewUpgrade.gameObject.SetActive(true);

        viewUpgrade.atkDmgUpgradeLevel.AddBtn(TowerAtkDmg);
        viewUpgrade.atkSpdUpgradeLevel.AddBtn(TowerAtckSpd);
        viewUpgrade.minerAmountUpgradeLevel.AddBtn(minerManager.AddMiner);
        viewUpgrade.minerSpeedUpgradeLevel.AddBtn(minerManager.IncreaseMinerSpeed);
        viewUpgrade.townHallHealthUpgradeLevel.AddBtn(TownHallHealth);
        viewUpgrade.levelChoiceUpgradeLevel.AddBtn(LevelChoice);
        viewUpgrade.rerollCountUpgradeLevel.AddBtn(RerollCount);
        viewUpgrade.expDropRateUpgradeLevel.AddBtn(ExpDropIncrease);
        viewUpgrade.gameObject.SetActive(false);


    }

    void TowerAtkDmg()
    {
        UpgradeData.TowerAttackDamage += 1;
        towerManager.UpgradeAllAttackTower();
    }
    void TowerAtckSpd()
    {
        UpgradeData.TowerAttackSpeed += 0.1f;
        towerManager.UpgradeAllAttackTower();
    }
    void TownHallHealth()
    {
        UpgradeData.TownHallHealth += 50;
    }
    void LevelChoice()
    {
        UpgradeData.LevelChoice += 1;
    }
    void RerollCount()
    {
        UpgradeData.RerollCount += 1;
    }
    void ExpDropIncrease()
    {
        UpgradeData.EXPDropIncrese += 0.1f;
    }
    void InitData()
    {
        CurrentUpgradeLvl upgradeData = Constants.JsonLoad<CurrentUpgradeLvl>(Constants.JsonFileName.Upgrade);
        UpgradeData.TowerAttackDamage = upgradeData.level[0] * 1;
        UpgradeData.TowerAttackSpeed = upgradeData.level[1] * 0.1f;
        UpgradeData.TownHallHealth = upgradeData.level[4] * 50;
        UpgradeData.LevelChoice = upgradeData.level[5] * 1;
        UpgradeData.RerollCount = upgradeData.level[6] * 1;
        UpgradeData.EXPDropIncrese = upgradeData.level[7] * 0.1f;
    }

}

