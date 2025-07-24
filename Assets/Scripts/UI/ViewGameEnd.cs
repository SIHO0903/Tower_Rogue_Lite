using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewGameEnd : MonoBehaviour
{

    public Button ReStart;

    private void Awake()
    {

        ReStart.onClick.AddListener(() =>
        {
            //¿ÁΩ√¿€
        });
    }
    private void OnEnable()
    {
        Time.timeScale = 0f;
    }
}
