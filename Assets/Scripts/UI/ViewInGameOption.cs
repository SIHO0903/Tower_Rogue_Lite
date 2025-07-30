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
            JsonDataManager.JsonSave<VolumeData>(GetVolume(), JsonDataManager.JsonFileName.Volume);
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
        VolumeData volumeData = JsonDataManager.JsonLoad<VolumeData>(JsonDataManager.JsonFileName.Volume);
        if(volumeData == null)
        {
            volumeData = new VolumeData(0.5f, 0.5f);

            JsonDataManager.JsonSave(volumeData, JsonDataManager.JsonFileName.Volume);
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
            JsonDataManager.JsonSave<VolumeData>(GetVolume(), JsonDataManager.JsonFileName.Volume);
        }
        else
        {
            gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
