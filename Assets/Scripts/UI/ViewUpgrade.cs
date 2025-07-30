using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ViewUpgrade : MonoBehaviour
{
    [SerializeField] GameObject OBJ_atkUpgradeLevel;
    [SerializeField] GameObject OBJ_aspdUpgradeLevel;
    [SerializeField] GameObject OBJ_minerAmountUpgradeLevel;
    [SerializeField] GameObject OBJ_minerSpeedUpgradeLevel;
    [SerializeField] GameObject OBJ_hpUpgradeLevel;
    [SerializeField] GameObject OBJ_levelChoiceUpgradeLevel;
    [SerializeField] GameObject OBJ_rerollCountUpgradeLevel;
    [SerializeField] GameObject OBJ_expDropRateUpgradeLevel;

    public UpgradeBaseDataSO[] upgradeBaseDataSOs;

    public ViewUpgradeContext   atkDmgUpgradeLevel;
    public ViewUpgradeContext   atkSpdUpgradeLevel;
    public ViewUpgradeContext   minerAmountUpgradeLevel;
    public ViewUpgradeContext   minerSpeedUpgradeLevel;
    public ViewUpgradeContext   townHallHealthUpgradeLevel;
    public ViewUpgradeContext   levelChoiceUpgradeLevel;
    public ViewUpgradeContext   rerollCountUpgradeLevel;
    public ViewUpgradeContext   expDropRateUpgradeLevel;

    private void Awake()
    {
        atkDmgUpgradeLevel =        new ViewUpgradeContext(OBJ_atkUpgradeLevel);
        atkSpdUpgradeLevel =        new ViewUpgradeContext(OBJ_aspdUpgradeLevel);
        minerAmountUpgradeLevel =   new ViewUpgradeContext(OBJ_minerAmountUpgradeLevel);
        minerSpeedUpgradeLevel =    new ViewUpgradeContext(OBJ_minerSpeedUpgradeLevel);
        townHallHealthUpgradeLevel= new ViewUpgradeContext(OBJ_hpUpgradeLevel);
        levelChoiceUpgradeLevel =   new ViewUpgradeContext(OBJ_levelChoiceUpgradeLevel);
        rerollCountUpgradeLevel =   new ViewUpgradeContext(OBJ_rerollCountUpgradeLevel);
        expDropRateUpgradeLevel =   new ViewUpgradeContext(OBJ_expDropRateUpgradeLevel);

        CurrentUpgradeLvl upgradeData = JsonDataManager.JsonLoad<CurrentUpgradeLvl>(JsonDataManager.JsonFileName.Upgrade);
        if (upgradeData == null)
        {
            upgradeData = new CurrentUpgradeLvl();
            upgradeData.Init();
            JsonDataManager.JsonSave(upgradeData, JsonDataManager.JsonFileName.Upgrade);
        }
        atkDmgUpgradeLevel.Init(upgradeBaseDataSOs[0], upgradeData.level[0]);
        atkSpdUpgradeLevel.Init(upgradeBaseDataSOs[1], upgradeData.level[1]);
        minerAmountUpgradeLevel.Init(upgradeBaseDataSOs[2], upgradeData.level[2]);
        minerSpeedUpgradeLevel.Init(upgradeBaseDataSOs[3], upgradeData.level[3]);
        townHallHealthUpgradeLevel.Init(upgradeBaseDataSOs[4], upgradeData.level[4]);
        levelChoiceUpgradeLevel.Init(upgradeBaseDataSOs[5], upgradeData.level[5]);
        rerollCountUpgradeLevel.Init(upgradeBaseDataSOs[6], upgradeData.level[6]);
        expDropRateUpgradeLevel.Init(upgradeBaseDataSOs[7], upgradeData.level[7]);

    }

}
