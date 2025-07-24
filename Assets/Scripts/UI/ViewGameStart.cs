using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewGameStart : MonoBehaviour
{

    public Button Start;

    private void Awake()
    {
        Time.timeScale = 0f;
        Start.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
        });
    }

}
