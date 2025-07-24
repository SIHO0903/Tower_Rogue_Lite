using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewInGameOption : MonoBehaviour
{

    public Button Continue;
    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SFXSlider;


    private void Awake()
    {

        Continue.onClick.AddListener(() =>
        {
            //계속하기
            Time.timeScale = 1f;
            Constants.JsonSave<VolumeData>(GetVolume(), Constants.JsonFileName.Volume);
            gameObject.SetActive(gameObject.activeSelf ? false : true);
        });
        LoadVolume();
    }
    public float BGMVolume()
    {
        return BGMSlider.value;
    }
    public float SFXVolume()
    {
        return SFXSlider.value;
    }
    public VolumeData GetVolume()
    {
        float bgm = Mathf.Round(BGMSlider.value * 10f) / 10f;
        float sfx = Mathf.Round(SFXSlider.value * 10f) / 10f;
        VolumeData volumeData = new(bgm, sfx);
        return volumeData;
    }
    public void LoadVolume()
    {
        VolumeData volumeData = Constants.JsonLoad<VolumeData>(Constants.JsonFileName.Volume);
        if(volumeData == null)
        {
            // 기본값 설정
            volumeData = new VolumeData(0.5f, 0.5f);

            // 새 파일 저장
            Constants.JsonSave(volumeData, Constants.JsonFileName.Volume);
        }
        BGMSlider.value = volumeData.bgm;
        SFXSlider.value = volumeData.sfx;
    }
    public void UIonOff()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
            Constants.JsonSave<VolumeData>(GetVolume(), Constants.JsonFileName.Volume);
        }
        else
        {
            gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
