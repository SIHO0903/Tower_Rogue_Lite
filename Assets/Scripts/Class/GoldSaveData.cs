using System;
using UnityEngine;
[System.Serializable]
public class GoldSaveData
{
    [SerializeField] public float gold;
    public GoldSaveData()
    {
        gold = 0;
    }
}