using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

using UnityEngine;

public class RandMove : MonoBehaviour
{
   [SerializeField] float speed;
   [SerializeField] float waitTime;
   [SerializeField] float startWaitTime;
   [SerializeField] float rotate = 0.5f;
   [SerializeField] float attackRotate = 0.5f;
   [SerializeField] float respon = 5f;
   [SerializeField] Transform[] moveSpots;
   int randomSpot;
   Animator animator;

    private void Start()
    {
        waitTime = startWaitTime;
        randomSpotMaking();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move(); //������ Ȱ��ȭ
        animator.SetBool("Sprint", true); // �ִϸ��̼� ������ Ȱ��ȭ

        if (Vector3.Distance(transform.position, moveSpots[randomSpot].position) <= 0.2f) // ���꽺�̰��� �Ÿ��� 0.1f ���� ���������
        {
            //�ð�������, ����Ʈ Ÿ���� 0���� �۾����� �ٽ� �������� (������ ���̵� ����)                  
            if (waitTime <= 0)
            {                       
                randomSpotMaking();//�������� �����Ұ� ����
                //IsMove = false; //������ Ȱ��ȭ
                waitTime = Random.Range(0, 2f);                      
                startWaitTime = waitTime;
            }
            else
            {                       
                animator.SetBool("Sprint", false);
                waitTime -= Time.deltaTime;
            }
        }               
    }
    private void Move() //�����Ӱ� ȸ���� ��
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
        Vector3 dir = moveSpots[randomSpot].position - transform.position;
        if(dir != Vector3.zero)
        {
            Quaternion targetAngle = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, rotate * Time.deltaTime);
        }
    }

    private void randomSpotMaking() // ������ ��ġ ����
    {
        randomSpot = Random.Range(0, moveSpots.Length);
    }

}
