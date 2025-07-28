using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Progress;

public class GridDrawer : MonoBehaviour
{
    [Header("타일 프리팹")]
    public GameObject tile_Prefab;

    [Header("타일크기")]
    [SerializeField] int width = 5;
    [SerializeField] int height = 5;

    public Dictionary<Vector2, int> levelGrid = new();


    Dictionary<Vector2, TMP_Text> tiles = new();
    Vector2 townhallPos = new Vector2(-2.5f, 0);

    void Awake()
    {
        Vector2 tilePos = new Vector2(-3.8f, -1.3f);
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                Vector2 pos = tilePos + new Vector2(x * Constants.TileGap, y * Constants.TileGap);
                var tile = Instantiate(tile_Prefab, pos, Quaternion.identity, transform);
                tile.name = $"Tile[{x},{y}]";
                tiles[pos] = tile.GetComponentInChildren<TMP_Text>();
                levelGrid[pos] = 0;
            }

        gameObject.SetActive(false);
    }

    public void UpdateSynergyGridTxt(Vector2 pos, int lvl)
    {
        Vector2 snapPos = Constants.SnapToGrid(pos);

        if (levelGrid.TryGetValue(snapPos, out int value))
        {
            levelGrid[snapPos] += lvl;
            UpdateTileGridTxt(snapPos);
        }
    }

    string Sign(int lvl)
    {
        string sign = string.Empty;
        if (lvl > 0) sign = "+";
        return sign + lvl;
    }

    public void UpdateAttackTowerGridTxt(GridTxtInfo gridTxtInfo)
    { 
        int currentLvl = gridTxtInfo.lvl;
        int maxLvl = gridTxtInfo.maxLvl;
        string txt = string.Empty;
        if (currentLvl > 0)
            txt = currentLvl + " / " + maxLvl;
        else
            txt = "0";

        tiles[gridTxtInfo.pos].text = txt;
    }
    public void WriteGridTxt()
    {
        foreach (var item in tiles)
        {
            if (levelGrid[item.Key] == 0)
                tiles[item.Key].text = string.Empty;
            else
            {
                int value = levelGrid[item.Key];
                tiles[item.Key].text = value > 0 ? "+" + value : value.ToString();
            }
        }
        tiles[townhallPos].text = string.Empty;

    }
    public void UpdateTileGridTxt(Vector2 pos)
    {
        if (levelGrid[pos] == 0)
            tiles[pos].text = string.Empty;
        else
            tiles[pos].text = Sign(levelGrid[pos]);
    }
    public void ResetLevelGrid()
    {
        var keys = new List<Vector2>(levelGrid.Keys);
        foreach (var key in keys)
        {
            levelGrid[key] = 0;
        }

        WriteGridTxt();
        
    }
}
