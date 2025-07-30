using System.IO;
using UnityEngine;

public static class JsonDataManager
{
    public static void JsonSave<T>(T data, JsonFileName fileName)
    {
        //Debug.Log(data);

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + $"/{fileName}.json", json);

        //Debug.Log("Application.persistentDataPath : " + Application.persistentDataPath);
    }
    public static T JsonLoad<T>(JsonFileName fileName)
    {
        string path = Application.persistentDataPath + $"/{fileName}.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            T data = JsonUtility.FromJson<T>(json);
            //Debug.Log("JsonLoad : " + json);
            return data;
        }
        else
        {
            //Debug.Log("Statue_Save : null"); 
            return default(T);
        }
    }
    public enum JsonFileName
    {
        Upgrade,
        Volume,
        Gold,
    }
}