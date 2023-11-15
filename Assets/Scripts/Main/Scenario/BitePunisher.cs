using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BitePunisher : ScenarioBase
{
    GameUIManager gameUIManager;

    public override void Enter(ScenarioController controller)
    {
        GameObject crocodile = FindObjectOfType<RandMove>().gameObject;
        crocodile.GetComponent<MoveAround>().enabled = false;
        crocodile.GetComponent<TouchScreen>().enabled = true;

        // ��Ģ�� ���� UI Ȱ��ȭ
        StartCoroutine(ShowPunisherUI());

        //controller.SetNextScenario();
    }

    public override void Execute(ScenarioController controller)
    {
    }

    public override void Exit()
    {
    }

    IEnumerator ShowPunisherUI()
    {
        // �Ǿ ��Ģ�ڿ��� �̵��ϰ� ���� �ö���� �ð��� 5�� ��
        yield return new WaitForSeconds(5f);

        gameUIManager = FindObjectOfType<GameUIManager>();
        gameUIManager.BittenPay();

        // TODO
        // �Ǿ ���� �ö�� �Ŀ� UI�� 3�ʰ� ��쵵�� �����ϱ� 
        // �Ǿ��� attack -> sprint ���� �̿��ϱ�

        // ��Ģ�ڰ� �����Ǿ��ٴ� UI�� 5�ʰ� ���
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Title");
    }
}
