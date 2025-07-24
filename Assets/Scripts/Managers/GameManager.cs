using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Action PopUpEnd;
    public void DescreaseHealth(float damage)
    {
        UpgradeData.TownHallHealth -= damage;
        SoundManager.instance.PlaySound(SoundType.GetHit);
        if (UpgradeData.TownHallHealth <= 0)
        {
            GameEnd();
        }
    }
    void GameEnd()
    {
        PopUpEnd?.Invoke();
        GoldSaveData goldSaveData = new GoldSaveData();
        goldSaveData.gold = MinerManager.Gold;
        Constants.JsonSave<GoldSaveData>(goldSaveData, Constants.JsonFileName.Gold);
        Debug.Log("게임종료");

    }
}
