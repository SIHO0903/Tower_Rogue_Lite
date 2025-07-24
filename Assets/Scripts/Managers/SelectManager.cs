using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SelectManager : MonoBehaviour
{

    public TowerManager towerManager;
    public GridDrawer gridDrawer;
    public GameObject LevelSelect;
    public Button[] AttackTowerSelects;
    public Button[] SynergyTowerSelects;

    GameObject Choosed;

    GameObject selectedTowerPrefab;
    public Action TowerInstalled;
    void Start()
    {
        string[] towerNames = {
            "Arrow Tower", "Electric Tower", "Catapult Tower",
            "SlingShot Tower", "Explode Tower", "Fire Tower", "Penetration Arrow Tower"
        };

        for (int i = 0; i < towerNames.Length; i++)
        {
            InitializeButton(i, PoolEnum.AttackTower, towerNames[i],AttackTowerSelects);
        }
        for (int i = 0; i < SynergyTowerSelects.Length; i++)
        {
            InitializeButton(i, PoolEnum.SynergyTower, "Synergy Tower "+i,SynergyTowerSelects);
        }
        UIInGameMenu.ReStart += ReStartReSelect;

    }
    void InitializeButton(int index,PoolEnum poolEnum, string towerName, Button[] buttons)
    {
        GameObject towerPrefab = PoolManager.Instance.Get(poolEnum, towerName, Vector3.zero, Quaternion.identity, towerManager.transform);
        towerPrefab.SetActive(false);

        SetButtonDescription(index, towerPrefab,buttons);

        buttons[index].onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySound(SoundType.Button);
            selectedTowerPrefab = PoolManager.Instance.Get(poolEnum, towerName, Vector3.zero, Quaternion.identity, towerManager.transform);
            selectedTowerPrefab.SetActive(false);
            LevelSelect.SetActive(false);
            SetActiveFalseAllSelects();
            Debug.Log("시너지");
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
    void Update()
    {
        HandleTowerPlacement();
        //HandleTempUIControls();

    }

    void HandleTowerPlacement()
    {
        if (Input.GetMouseButtonDown(0) && selectedTowerPrefab != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("TowerTile"))
            {
                Vector2 pos = Constants.SnapToGrid(hit.collider.transform.position);
                selectedTowerPrefab.transform.position = pos;
                towerManager.tileGrid[pos] = selectedTowerPrefab;
                selectedTowerPrefab.SetActive(true);

                towerManager.OnTowerPlaced(selectedTowerPrefab, pos);
                TowerInstalled?.Invoke();

                gridDrawer.gameObject.SetActive(false);
                selectedTowerPrefab = null;
            }
        }
    }
    void HandleTempUIControls()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            GetRandomSelect(1);
        if (Input.GetKeyDown(KeyCode.E))
            GetRandomSelect(3);
        if (Input.GetKeyDown(KeyCode.W))
            SetActiveFalseAllSelects();
        if (Input.GetKeyDown(KeyCode.Tab))
            gridDrawer.gameObject.SetActive(!gridDrawer.gameObject.activeSelf);
    }
    public void GetRandomSelect(int currentLevel)
    {
        LevelSelect.SetActive(true);
        SetActiveFalseAllSelects();

        bool isSynergyLevel = currentLevel % 3 == 0;

        Button[] targetButtons = isSynergyLevel ? SynergyTowerSelects : AttackTowerSelects;
        int max = targetButtons.Length;


        var indices = GetUniqueRandomNumbers(UpgradeData.LevelChoice, 0, max);
        foreach (var idx in indices)
        {
            targetButtons[idx].gameObject.SetActive(true);
        }
        gridDrawer.gameObject.SetActive(true);

    }
    void ReStartReSelect()
    {
        GetRandomSelect(1);
    }
    HashSet<int> GetUniqueRandomNumbers(int count, int min, int max)
    {
        HashSet<int> unique = new HashSet<int>();
        while (unique.Count < count)
            unique.Add(Random.Range(min, max));
        return unique;
    }
    void SetActiveFalseAllSelects()
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
    }
    
}