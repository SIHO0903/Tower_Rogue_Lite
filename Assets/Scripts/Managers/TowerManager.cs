using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;


public class TowerManager : MonoBehaviour
{
    public GridDrawer gridDrawer;
    public Dictionary<Vector3, GameObject> tileGrid = new Dictionary<Vector3, GameObject>();

    BaseTower selectedTower;
    GameObject ghostObj;

    void Start()
    {
        PlaceTownHall();
        UIInGameMenu.ReStart += ClearDic;
        UIInGameMenu.ReStart += ClearObject;
        UIInGameMenu.ReStart += gridDrawer.ResetLevelGrid;
        UIInGameMenu.ReStart += PlaceTownHall;
    }
    void ClearDic()
    {
        tileGrid.Clear();
    }
    void ClearObject()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    void PlaceTownHall()
    {
        GameObject townHall = PoolManager.Instance.Get(PoolEnum.TownHall, "TownHall", new Vector2(-2.5f, 0f), Quaternion.identity, transform);
        townHall.GetComponent<SynergyTower>().ApplySynergy();
        tileGrid[new Vector2(-2.5f, 0f)] = townHall;
    }

    void HandleTowerSelectionAndMove()
    {
        if (Input.GetMouseButtonDown(0))
            TrySelectTower();

        if (Input.GetMouseButton(0))
            MoveGhostObject();

        if (Input.GetMouseButtonUp(0))
            PlaceSelectedTower();
    }
    public void Update()
    {
        HandleTowerSelectionAndMove();
    }
    void TrySelectTower()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("Tower"))
        {
            if (hit.collider.TryGetComponent<BaseTower>(out var tower))
            {
                selectedTower = tower;
                ghostObj = CreateGhostObj(mousePos, tower.TowerInfo());
            }
        }
    }
    void MoveGhostObject()
    {
        if (ghostObj != null)
        {
            gridDrawer.gameObject.SetActive(true);
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ghostObj.transform.position = mousePos;
        }
    }
    void PlaceSelectedTower()
    {
        if (ghostObj != null && selectedTower != null)
        {
            gridDrawer.gameObject.SetActive(false);

            Vector3 targetPos = ClampTilePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Vector3 oldPos = Constants.SnapToGrid(selectedTower.transform.position);
            
            if (oldPos == targetPos)
            {
                ghostObj.SetActive(false);
                ghostObj = null;
                selectedTower = null;
                return;
            }
            UpdateTileGrid(oldPos, targetPos, selectedTower.gameObject);
            UpdateTowerGridTxt();
            gridDrawer.UpdateTileGridTxt(oldPos);
            ghostObj.SetActive(false);
            ghostObj = null;
            selectedTower = null;
        }


    }

    public void OnTowerPlaced(GameObject towerObj, Vector2 pos)
    {
        if (towerObj.TryGetComponent<SynergyTower>(out SynergyTower synergyTower))
        {
            synergyTower.ApplySynergy();
            UpgradeAllAttackTower();
        }
        else if (towerObj.TryGetComponent<AttackTower>(out AttackTower attackTower))
        {
            attackTower.LevelChanged(gridDrawer.levelGrid[pos]);
            gridDrawer.UpdateAttackTowerGridTxt(attackTower.gridTxtInfo);
        }
    }
    GameObject CreateGhostObj(Vector2 cursorPos, TowerInfo info)
    {
        GameObject ghost = PoolManager.Instance.Get(PoolEnum.Etc, "GhostObj", cursorPos, Quaternion.identity);

        SpriteRenderer[] sprites = ghost.GetComponentsInChildren<SpriteRenderer>();
        sprites[0].sprite = info.headSprite;
        sprites[1].sprite = info.bodySprite;

        ghost.transform.GetChild(0).position = info.headTransform.position;
        ghost.transform.GetChild(1).position = info.bodyTransform.position;

        Color headColor = sprites[0].color;
        headColor.a = 0.5f;
        sprites[0].color = headColor;

        Color bodyColor = sprites[1].color;
        bodyColor.a = 0.5f;
        sprites[1].color = bodyColor;

        return ghost;
    }
    public Vector3 ClampTilePosition(Vector3 pos)
    {
        Vector2 gridOrigin = new Vector2(-3.8f, 1.3f);
        float x = Mathf.Round((Mathf.Clamp(pos.x,-3.8f,-1.2f) - gridOrigin.x) / Constants.TileGap) * Constants.TileGap + gridOrigin.x;
        float y = Mathf.Round((Mathf.Clamp(pos.y,-1.3f,1.3f) - gridOrigin.y) / Constants.TileGap) * Constants.TileGap + gridOrigin.y;
        return Constants.SnapToGrid(new Vector3(x, y));
    }
    public void UpdateTileGrid(Vector2 oldPos, Vector2 newPos, GameObject towerObj)
    {
        if (!tileGrid.ContainsKey(newPos))
        {
            Debug.Log("새로운곳에 놓기");
            tileGrid.Add(newPos, towerObj);
            tileGrid.Remove(oldPos);
            towerObj.transform.position = newPos;
            if(towerObj.TryGetComponent<SynergyTower>(out SynergyTower synergyTower))
            {
                synergyTower.RemoveSynergy(oldPos);
            }
            OnTowerPlaced(towerObj, newPos);
        }
        else
        {
            if (newPos == TownHall.Position)
                return;

            var otherTower = tileGrid[newPos];
            otherTower.transform.position = oldPos;
            towerObj.transform.position = newPos;

            tileGrid[oldPos] = otherTower;
            tileGrid[newPos] = towerObj;

            OnTowerPlaced(otherTower, oldPos);
            OnTowerPlaced(towerObj, newPos);
        }

    }
    public void UpgradeAllAttackTower()
    {
        foreach (var item in tileGrid.Values)
        {
            if(item.TryGetComponent<AttackTower>(out AttackTower attackTower))
            {
                attackTower.UpgradeChanged();
            }
        }
    }
    public void UpdateTowerGridTxt()
    {
        foreach (var item in tileGrid)
        {
            if(item.Value.TryGetComponent<AttackTower>(out AttackTower attackTower))
            {
                attackTower.LevelChanged(gridDrawer.levelGrid[item.Key]);
                gridDrawer.UpdateAttackTowerGridTxt(attackTower.gridTxtInfo);
            }

        } 
    }
}
