using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewSelect : MonoBehaviour
{

    public Button[] AttackTowerSelects;
    public Button[] SynergyTowerSelects;
    [SerializeField] Button reRollBtn;
    TMP_Text reRollTxt;
    [HideInInspector] public GameObject TowerObj;
    int currentLevel;
    int currentReRollCount;
    private void Awake()
    {
        InitReRollBtn();
    }
    public void InitializeButton(int index, PoolEnum poolEnum, string towerName, Button[] buttons,Transform transform)
    {
        GameObject towerPrefab = PoolManager.Instance.Get(poolEnum, towerName, Vector3.zero, Quaternion.identity, transform);
        towerPrefab.SetActive(false);

        SetButtonDescription(index, towerPrefab, buttons);

        buttons[index].onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySound(SoundType.Button);
            TowerObj = PoolManager.Instance.Get(poolEnum, towerName, Vector3.zero, Quaternion.identity, transform);
            TowerObj.SetActive(false);
            SetActiveFalseAllSelects();
        });
    }
    void SetButtonDescription(int index, GameObject towerPrefab, Button[] buttons)
    {
        var texts = buttons[index].GetComponentsInChildren<TMP_Text>();
        if (towerPrefab.TryGetComponent<BaseTower>(out var baseTower))
        {
            texts[0].text = baseTower.GetName();
            texts[1].text = baseTower.GetDescription();
        }
    }
    public void SetActiveFalseAllSelects()
    {
        foreach (var item in AttackTowerSelects)
        {
            if (!item.gameObject.activeSelf)
                continue;
            else
                item.gameObject.SetActive(false);
        }
        foreach (var item in SynergyTowerSelects)
        {
            if (!item.gameObject.activeSelf)
                continue;
            else
                item.gameObject.SetActive(false);
        }
        reRollBtn.gameObject.SetActive(false);
    }
    public void RandomSelect(int currentLevel)
    {
        this.currentLevel = currentLevel;
        ShowReRollBtn();
        //3�� ����� �������� �ó��� Ÿ������UI Ȱ��ȭ
        bool isSynergyLevel = currentLevel % 3 == 0;

        Button[] targetButtons = isSynergyLevel ? SynergyTowerSelects : AttackTowerSelects;
        int max = targetButtons.Length;


        var indices = GetUniqueRandomNumbers(UpgradeData.LevelChoice, 0, max);
        foreach (var idx in indices)
        {
            targetButtons[idx].gameObject.SetActive(true);
        }
    }
    
    HashSet<int> GetUniqueRandomNumbers(int count, int min, int max)
    {
        HashSet<int> unique = new HashSet<int>();
        while (unique.Count < count)
            unique.Add(Random.Range(min, max));
        return unique;
    }

    void ShowReRollBtn()
    {
        currentReRollCount = UpgradeData.RerollCount;
        reRollTxt.text = $"���� ({currentReRollCount}ȸ ����)";
        reRollBtn.gameObject.SetActive(true);
        reRollBtn.interactable = currentReRollCount == 0 ? false : true;
    }
    void InitReRollBtn()
    {
        reRollTxt = reRollBtn.GetComponentInChildren<TMP_Text>();
        reRollBtn.onClick.AddListener(() =>
        {
            SetActiveFalseAllSelects();
            RandomSelect(currentLevel);
            currentReRollCount--;
            reRollTxt.text = $"���� ({currentReRollCount}ȸ ����)";
            reRollBtn.interactable = currentReRollCount == 0 ? false : true;
        });

        reRollBtn.gameObject.SetActive(false);
    }
}
