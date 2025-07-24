using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIInGameMenu : MonoBehaviour
{
    [SerializeField] ViewInGameOption gamePause;
    [SerializeField] ViewGameEnd gameEnd;
    [SerializeField] ViewGameStart gameStart;
    public static Action ReStart;
    void Awake()
    {
        gameEnd.ReStart.onClick.AddListener(() =>
        {
            Debug.Log("Àç½ÃÀÛ");
            Time.timeScale = 1f;
            gameEnd.gameObject.SetActive(false);
            ReStart?.Invoke();
            SoundManager.instance.PlaySound(SoundType.Button);
        });
        gameStart.Start.onClick.AddListener(() =>
        {
            ReStart?.Invoke();
            SoundManager.instance.PlaySound(SoundType.Button);
        });

        gamePause.LoadVolume();

    }

    void Start()
    {
        GameManager.Instance.PopUpEnd = ShowUI;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
            gamePause.UIonOff();
    }
    void ShowUI()
    {
        gameEnd.gameObject.SetActive(true);
    }
}
