using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewUpgradeContext
{
    TMP_Text Txt_name;
    Button button;
    TMP_Text Txt_progress;
    TMP_Text Txt_cost;
     

    int key;
    string name;
    int currentLevel = 0;
    int maxLevel;
    int cost;
    float costMultiplier;

    public ViewUpgradeContext(GameObject gameObject)
    {
        TMP_Text[] texts = gameObject.GetComponentsInChildren<TMP_Text>();
        Txt_name = texts[0];
        button = gameObject.GetComponentInChildren<Button>();
        Txt_progress = texts[1];
        Txt_cost = texts[2];
    }
    public void Init(UpgradeBaseDataSO upgradeBaseData,int currentLevel)
    {
        key = upgradeBaseData.key;
        name = upgradeBaseData.UPName;
        this.currentLevel = currentLevel;
        cost = upgradeBaseData.Cost;
        maxLevel = upgradeBaseData.MaxLvl;
        costMultiplier = upgradeBaseData.CostMultiplier;

        Txt_name.text = name;
        Txt_Progress(currentLevel, maxLevel);
        Txt_cost.text = (cost * (currentLevel+1) * costMultiplier).ToString("N0");
        if (IsMaxLevel()) return;
    }

    void Txt_Progress(int currentLevel, int maxLevel)
    {
        if (maxLevel == -1)
            this.Txt_progress.text = currentLevel.ToString();
        else
            this.Txt_progress.text = currentLevel + " / " + maxLevel;
    }

    void Update()
    {
        if (!MinerManager.CanPayGold(cost))
        {
            Debug.Log("돈이부족합니다.");
            return;
        }
        SoundManager.instance.PlaySound(SoundType.UpgradeButton);
        currentLevel += 1;
        Txt_Progress(currentLevel, maxLevel);
        this.Txt_cost.text = ((currentLevel+1) * cost * costMultiplier).ToString("N0");
        SaveCurrentLevelToJson();
        if (IsMaxLevel()) return;
    }

    private void SaveCurrentLevelToJson()
    {
        CurrentUpgradeLvl upgradeData = JsonDataManager.JsonLoad<CurrentUpgradeLvl>(JsonDataManager.JsonFileName.Upgrade);

        upgradeData.level[key] = currentLevel;

        JsonDataManager.JsonSave(upgradeData, JsonDataManager.JsonFileName.Upgrade);
    }

    public void AddBtn(Action action)
    {
        button.onClick.AddListener(() =>
        {
            action?.Invoke();
            Update();

        });
    }
    bool IsMaxLevel()
    {
        if (maxLevel == -1)
            return false;

        if (currentLevel >= maxLevel)
        {
            button.interactable = false;
            return true;
        }
        else
            return false;
    }
}
