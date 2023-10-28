using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    #region �̱��� �ν��Ͻ�
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<UIManager>();
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
    #endregion

    #region ������
    //��ư ���� ����

    public Button btnSetting;
    public Button btnExit;
    public Button btnMin;
    public Button btnMax;
    public Button btnGameStart;

    public GameObject pnlSetting;
    public Text txtSelect;

    //�ο� ���� ����
    private int value = 1;

    //���� â ���� ����
    public Slider sldSoundFxVolume;
    public Slider sldBGMVolume;

    public Toggle toggleSoundFxMute;
    public Toggle toggleBGMMute;


    private bool isSoundFxMuted = false;
    private bool isBGMMuted = false;
    #endregion

    #region AddListner ����
    private void Start()
        {
            //��ư AddListner
            btnExit.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlayButtonClickSound();
                OnClickExit();
            });
            btnSetting.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlayButtonClickSound();
                OnClickToggleSetting();
            });
            btnMin.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlayButtonClickSound();
                OnClickDecrease();
            });
            btnMax.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlayButtonClickSound();
                OnClickIncrease();
            });
            btnGameStart.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlayGameStartSound();
                OnClickGameStart();
            });


        //����
        sldBGMVolume.onValueChanged.AddListener((value) =>
        {
            SoundManager.Instance.SetBGMVolume(value);
         
        });

        sldSoundFxVolume.onValueChanged.AddListener((value) =>
        {
            SoundManager.Instance.SetSoundFxVolume(value);
            
        });

        toggleBGMMute.onValueChanged.AddListener((isMuted) =>
        {
            SoundManager.Instance.MuteBGM(isMuted);
           
        });

        toggleSoundFxMute.onValueChanged.AddListener((isMuted) =>
        {
            SoundManager.Instance.MuteSoundFx(isMuted);
          
        });
    }

        #endregion

    #region TITLE �� ��ư ��� �Լ�
        //���� ����
        public void OnClickExit()
        {
            Application.Quit();
        }
        //Setting â Ȱ��ȭ/��Ȱ��ȭ
        public void OnClickToggleSetting()
        {
            pnlSetting.SetActive(!pnlSetting.activeSelf);
        }

        //Max��ư Ŭ���� value �� 1�� ���� �ִ� 5
        public void OnClickIncrease()
        {
            value = int.Parse(txtSelect.text);
            if (value < 5)
            {
                txtSelect.text = (value + 1).ToString();
            }
        }
        //Min��ư Ŭ���� value �� 1�� ���� �ּ� 1
        public void OnClickDecrease()
        {
            value = int.Parse(txtSelect.text);
            if (value > 1)
            {
                txtSelect.text = (value - 1).ToString();
            }
        }
        //���� ����
        public void OnClickGameStart()
        {
            SceneManager.LoadScene("Game");
        }
    #endregion

    #region SETTING â ��� �Լ�

    //���� ���� �Լ�
    public void SetSoundFxVolume(float volume)
    {

        SoundManager.Instance.SetSoundFxVolume(volume);

        if (volume <= 0)
        {
            isSoundFxMuted = true;
            toggleSoundFxMute.isOn = true;
        }
        else
        {
            isSoundFxMuted = false;
            toggleSoundFxMute.isOn = false;
        }
    }

    public void SetBGMVolume(float volume)
    {
        Debug.Log("Setting BGM volume to: " + volume);
        SoundManager.Instance.SetBGMVolume(volume);

        if (volume <= 0)
        {
            isBGMMuted = true;
            toggleBGMMute.isOn = true;
        }
        else
        {
            isBGMMuted = false;
            toggleBGMMute.isOn = false;
        }
    }

    //���Ұ� �Լ�
    public void ToggleBGMMute(bool isMuted)
    {
        isBGMMuted = isMuted;

        // SoundManager�� ���Ұ� ��� ȣ��
        SoundManager.Instance.MuteBGM(isBGMMuted);
    }

    public void ToggleSoundFxMute(bool isMuted)
    {
        isSoundFxMuted = isMuted;

        // SoundManager�� ���Ұ� ��� ȣ��
        SoundManager.Instance.MuteSoundFx(isSoundFxMuted);

    }



    #endregion



}



