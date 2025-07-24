using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

//�����̸� enumŸ������ ����
public enum SoundType
{
    Attack,
    GetHit,
    EnemyAttack,
    EnemyGetHit,
    Button,
    UpgradeButton,
}
public enum BGMType
{
    Town,

}
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public AudioClip[] soundList;
    public AudioClip[] bgmList;

    public static SoundManager instance;

    private AudioSource sfxAudioSource;
    [SerializeField] AudioSource bgmAudioSource;
    [SerializeField] ViewInGameOption uiSound;
    private void Awake()
    {
        //�̱���
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
            //SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
        sfxAudioSource = GetComponent<AudioSource>();

    }

    //���ε�� ���� �´� BGM���
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i < bgmList.Length; i++)
        {
            if (arg0.name == bgmList[i].name)
                PlayBGM(BGMType.Town);
            else
                bgmAudioSource.Stop();
        }
    }
    public void PlaySound(SoundType sound)
    {
        sfxAudioSource.PlayOneShot(soundList[(int)sound], uiSound.SFXVolume());

    }
    public void PlayBGM(BGMType sound, float volume = 1)
    {
        bgmAudioSource.clip = bgmList[(int)sound];
        bgmAudioSource.loop = true;
        bgmAudioSource.volume = volume;
        bgmAudioSource.Play();
    }
}
