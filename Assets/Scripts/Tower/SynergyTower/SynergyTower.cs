using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
//가로,세로,십자, 주변다, 대각선
public class SynergyTower : BaseTower
{
    TowerManager towerManager;

    [Header("시너지위치")]
    [HideInInspector] public SynergyEnum[] SynergyPos;


    [SerializeField] public SynergyPosition[] SynergyPositions;

    void OnEnable() => towerManager = GetComponentInParent<TowerManager>();
    public List<Vector2> GetAllSynergyPositions(Vector3 oldPos)
    {
        List<Vector2> vectors = new List<Vector2>();
        foreach (var item in SynergyPositions)
        {
            vectors.Add(CalculateSynergyPosition(item, oldPos));
        }
        return vectors;
    }

    public void RemoveSynergy(Vector2 oldPos)
    {
        foreach (var item in SynergyPositions)
        {
            Vector2 pos = CalculateSynergyPosition(item, oldPos);

            towerManager.gridDrawer.UpdateSynergyGridTxt(pos, -item.Amount);

        }
    }

    Vector2 CalculateSynergyPosition(SynergyPosition item, Vector3? customPos = null)
    {
        Vector2 basePos = customPos ?? transform.position;
        Vector2 offset = Constants.EnumToVector3(item.dir) * Constants.TileGap * item.Distance;
        return basePos + offset;
    }

    public void ApplySynergy()
    {
        foreach (var item in SynergyPositions)
        {
            var pos = CalculateSynergyPosition(item);
            towerManager.gridDrawer.UpdateSynergyGridTxt(pos, item.Amount);
            towerManager.UpdateTowerGridTxt();
        }
    }

    public override string GetDescription()
    {
        int gridSize = 5;
        int center = gridSize / 2; 
        string[,] grid = new string[gridSize, gridSize];

        for (int y = 0; y < gridSize; y++)
            for (int x = 0; x < gridSize; x++)
                grid[y, x] = "";

        grid[2, 2] = " T ";

        foreach (var synergy in SynergyPositions)
        {
            Vector2 dir = Constants.EnumToVector3(synergy.dir);

            int dx = Mathf.RoundToInt(dir.x * synergy.Distance);
            int dy = -Mathf.RoundToInt(dir.y * synergy.Distance);

            int targetX = center + dx;
            int targetY = center + dy;

            if (targetX >= 0 && targetX < gridSize && targetY >= 0 && targetY < gridSize)
            {
                string sign = synergy.Amount > 0 ? "+" : "-";
                string color = synergy.Amount > 0 ? "#5CC054" : "red";
                int absAmount = Mathf.Abs(synergy.Amount);

                grid[targetY, targetX] = $"<color={color}>{sign}{absAmount} </color>";
            }
        }

        StringBuilder sb = new StringBuilder();
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                sb.Append(grid[y, x]);
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }
}


