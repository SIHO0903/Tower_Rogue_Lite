using UnityEditor;
using System.IO;
using UnityEngine;
using System;


public class CSVtoSO
{
    //Editor폴더에 있는 .csv파일위치
    //public static string enemyCSVPath = "/Editor/CSVs/Enemy.csv";

    [MenuItem("MyUtilities/GenerateUnitSO")]
    public static void GenerateUnitSO()
    {
        string path = EditorUtility.OpenFilePanel("Select CSV file", Application.dataPath + "/Editor/CSVs", "csv");

        if (string.IsNullOrEmpty(path))
        {
            Debug.LogError("No file selected");
            return;
        }

        string[] data = File.ReadAllLines(path);

        for (int i = 1; i < data.Length; i++)
        {
            string[] splitData = data[i].Split(',');

            UnitDataSO unit = ScriptableObject.CreateInstance<UnitDataSO>();
            unit.ID = int.Parse(splitData[0]);
            unit.UnitKorName = splitData[1];
            unit.UnitName = splitData[2];
            unit.Health = float.Parse(splitData[3]);
            unit.Damage = float.Parse(splitData[4]);
            unit.MoveSpeed = float.Parse(splitData[5]);
            unit.AttackRange = float.Parse(splitData[6]);
            unit.HealthGrowth = float.Parse(splitData[7]);
            unit.AttackGrowth = float.Parse(splitData[8]);

            AssetDatabase.CreateAsset(unit, $"Assets/DataSO/EnemyData/{unit.UnitName}.asset");
        }
        AssetDatabase.SaveAssets();
    }


}
