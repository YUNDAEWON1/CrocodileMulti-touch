using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Look : MonoBehaviour
{
    public float speed;
    public float waitTime;
    public float startWaitTime;
    public float rotate = 0.5f;
    public float respon = 5f;
    public Transform[] moveSpot;
    private int randomSpot;
    public bool IsTouch;
    public bool IsMove;
    Animator animator;
    
    private void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpot.Length);
        animator = GetComponent<Animator>();
        IsTouch = false;
        IsMove = false;
        

    }
    private void Update()
    {
        
        if(!IsTouch) //��ġ���Ҷ�
        {
            if(!IsMove)//�������� ������
            {
                Move(); //������ Ȱ��ȭ
                animator.SetBool("Sprint", true); // �ִϸ��̼� ������ Ȱ��ȭ
               
                if (Vector3.Distance(transform.position, moveSpot[randomSpot].position) <= 0.2f) // ���꽺�̰��� �Ÿ��� 0.1f ���� ���������
                {
                    //�ð�������, ����Ʈ Ÿ���� 0���� �۾����� �ٽ� �������� (������ ���̵� ����)

                    
                    if (waitTime <= 0)
                    {
                        Debug.Log("�̵�Ȱ��ȭ");
                        randomSpotMaking();//�������� �����Ұ� ����
                        IsMove = false; //������ Ȱ��ȭ
                        waitTime = Random.Range(0, 2f);
                        Debug.Log(waitTime);
                        startWaitTime = waitTime;
                    }
                    else
                    {
                        //������ ��Ȱ��ȭ
                        Debug.Log("�̵� ��Ȱ��ȭ");
                        animator.SetBool("Sprint", false);
                        waitTime -= Time.deltaTime;
                    }


                   
                }
                else //���꽺���� �Ÿ��� 0.1 ���� �ָ�
                {
                    // ������������
                    
                }
            }
            else //�����϶�
            {
                //�ִϸ��̼� ���̵� Ȱ��ȭ
                //������ x
            }
        }
        

        
        else //��ġ �Ҷ�
        {
            //��ġ�� �� ��ġ�� �޾Ƽ� �������� �����Ѱ����� ����
        }

    }
    private void Move() //�����Ӱ� ȸ���� ��
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpot[randomSpot].position, speed * Time.deltaTime);
        Vector3 dir = moveSpot[randomSpot].position - transform.position;
        Quaternion targetAngle = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, rotate * Time.deltaTime);

    }
    private void randomSpotMaking() // ������ ��ġ ����
    {
        randomSpot = Random.Range(0, moveSpot.Length);
    }
    



}
