using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCrocodile : ScenarioBase
{
    public override void Enter(ScenarioController controller)
    {
        StartCoroutine(MoveToPunisher(controller));
    }

    public override void Execute(ScenarioController controller)
    {

    }

    public override void Exit()
    {

    }

    // 5�ʰ� �Ǿ ���ƴٴϴٰ� ��Ģ�� ������ 
    IEnumerator MoveToPunisher(ScenarioController controller)
    {
        Debug.Log("�Ǿ ��Ģ�ڿ��� �̵� ����");
        yield return new WaitForSeconds(5f);

        GameObject crocodile = FindObjectOfType<RandMove>().gameObject;
        crocodile.GetComponent<RandMove>().enabled = false;
        crocodile.GetComponent<TouchScreen>().enabled = true;

        controller.SetNextScenario();
    }
}
