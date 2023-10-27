using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager Instance => instance;

    private void Awake()
    {
        // �ν��Ͻ��� �̹� �����ϴ� ���, ���� ����� �ν��Ͻ� ����
        if (instance)
        {
            Destroy(this.gameObject);
            return;
        }

        // �ν��Ͻ��� ���� ������Ʈ�� ����
        instance = this;

        DontDestroyOnLoad(gameObject);
    }
}
