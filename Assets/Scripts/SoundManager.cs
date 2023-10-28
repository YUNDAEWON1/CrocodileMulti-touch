using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgmSource; // BGM�� ���� AudioSource ������Ʈ
    public AudioSource sfxSource; // Sound FX�� ���� AudioSource ������Ʈ

    public AudioClip gameStartClip; // ���� ���� ��ư�� ���� �� ����� ȿ����
    public AudioClip buttonClickClip; // ������ ��ư�� ���� �� ����� ȿ����

    private static SoundManager _instance;

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<SoundManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // BGM ���� ����
    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    // Sound FX ���� ����
    public void SetSoundFxVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    // BGM ���Ұ� ����
    public void MuteBGM(bool mute)
    {
        bgmSource.mute = mute;
    }

    // Sound FX ���Ұ� ����
    public void MuteSoundFx(bool mute)
    {
        sfxSource.mute = mute;
    }

    public void PlayGameStartSound()
    {
        sfxSource.PlayOneShot(gameStartClip);
    }

    public void PlayButtonClickSound()
    {
        sfxSource.PlayOneShot(buttonClickClip);
    }

}
