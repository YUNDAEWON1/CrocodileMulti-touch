using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPunisher : ScenarioBase
{
    public override void Enter(ScenarioController controller)
    {
        GameObject crocodile = FindObjectOfType<RandMove>().gameObject;
        crocodile.GetComponent<RandMove>().enabled = false;
        crocodile.GetComponent<TouchScreen>().enabled = true;
        Debug.Log("�Ǿ ��Ģ�ڿ��� �̵� ����");
    }

    public override void Execute(ScenarioController controller)
    {

    }

    public override void Exit()
    {

    }
}
