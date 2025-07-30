using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UISelect : MonoBehaviour
{

    [SerializeField] TowerManager towerManager;
    [SerializeField] ViewSelect viewSelect;
    GameObject Choosed;
    public Action TowerAmount;
    void Start()
    {
        string[] towerNames = {
            "Arrow Tower", "Electric Tower", "Catapult Tower",
            "SlingShot Tower", "Explode Tower", "Fire Tower", "Penetration Arrow Tower"
        };

        for (int i = 0; i < towerNames.Length; i++)
        {
            viewSelect.InitializeButton(i, PoolEnum.AttackTower, towerNames[i],viewSelect.AttackTowerSelects, towerManager.transform);
        }
        for (int i = 0; i < viewSelect.SynergyTowerSelects.Length; i++)
        {
            viewSelect.InitializeButton(i, PoolEnum.SynergyTower, "Synergy Tower "+i, viewSelect.SynergyTowerSelects, towerManager.transform);
        }
        UIInGameMenu.ReStart += ReStartReSelect;

    }
    void TowerPlacement()
    {
        if (Input.GetMouseButtonDown(0) && viewSelect.TowerObj != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("TowerTile"))
            {
                Vector2 pos = GridUtility.SnapToGrid(hit.collider.transform.position);
                viewSelect.TowerObj.transform.position = pos;
                towerManager.tileGrid[pos] = viewSelect.TowerObj;
                viewSelect.TowerObj.SetActive(true);

                towerManager.OnTowerPlaced(viewSelect.TowerObj, pos);
                TowerAmount?.Invoke();

                towerManager.gridDrawer.gameObject.SetActive(false);
                viewSelect.TowerObj = null;
            }
        }
    }
    public void GetRandomSelect(int currentLevel)
    {
        viewSelect.SetActiveFalseAllSelects();
        viewSelect.RandomSelect(currentLevel);
        towerManager.gridDrawer.gameObject.SetActive(true);
    }


    void Update()
    {
        TowerPlacement();
        HandleUIControls();

    }
    void HandleUIControls()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            GetRandomSelect(1);
        if (Input.GetKeyDown(KeyCode.E))
            GetRandomSelect(3);
        if (Input.GetKeyDown(KeyCode.W))
            viewSelect.SetActiveFalseAllSelects();
        if (Input.GetKeyDown(KeyCode.Tab))
            towerManager.gridDrawer.gameObject.SetActive(!towerManager.gridDrawer.gameObject.activeSelf);
    }
    void ReStartReSelect()
    {
        GetRandomSelect(1);
    }
}