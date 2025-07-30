using System.Collections;
using UnityEngine;

public static class CoroutineUtility
{
    public static IEnumerator SetActiveFalse(GameObject gameObject, float setTimer)
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
}
