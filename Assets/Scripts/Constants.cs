using System.Collections;
using System.IO;
using UnityEngine;
public enum SynergyEnum
{
    Up_Left,
    Up,
    Up_Right,
    Left,
    Right,
    Down_Left,
    Down,
    Down_Right
}

public static class Constants
{
    public const float TileGap = 0.65f;
    public static Vector2 EnumToVector3(SynergyEnum pos)
    {
        switch (pos)
        {
            case SynergyEnum.Up_Left:    return new Vector2(-1, 1);
            case SynergyEnum.Up:         return new Vector2(0 , 1);
            case SynergyEnum.Up_Right:   return new Vector2(1 , 1);
            case SynergyEnum.Left:       return new Vector2(-1, 0);
            case SynergyEnum.Right:      return new Vector2(1 , 0);
            case SynergyEnum.Down_Left:  return new Vector2(-1, -1);
            case SynergyEnum.Down:       return new Vector2(0, -1);
            case SynergyEnum.Down_Right: return new Vector2(1 , -1);
            default:                    return Vector2.zero;
        }
    }
    public static IEnumerator SetActiveFalse(GameObject gameObject,float setTimer)
    {
        float timer = 0f;
        while (timer < setTimer)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
        yield return null;
    }
    public static Vector2 SnapToGrid(Vector2 v)
    {
        float x = Mathf.Round(v.x * 100f) / 100f;
        float y = Mathf.Round(v.y * 100f) / 100f;
        return new Vector2(x, y);
    }
    public static void JsonSave<T>(T data, JsonFileName fileName)
    {
        Debug.Log(data);
        string json = JsonUtility.ToJson(data);
        Debug.Log("JsonSave : " + json);
        File.WriteAllText(Application.persistentDataPath + $"/{fileName}.json", json);
        Debug.Log("Application.persistentDataPath : " + Application.persistentDataPath);
    }
    public static T JsonLoad<T>(JsonFileName fileName)
    {
        string path = Application.persistentDataPath + $"/{fileName}.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            T data = JsonUtility.FromJson<T>(json);
            Debug.Log("JsonLoad : " + json);
            return data;
        }
        else
        {
            Debug.Log("Statue_Save : null"); 
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