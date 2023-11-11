using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TouchScreen : MonoBehaviour
{
   [SerializeField] float moveSpeed = 10f;
   [SerializeField] float upSpeed = 100f;
   [SerializeField] float rotateSpeed = 10f;
   [SerializeField] float waitTime = 2f;
   [SerializeField] GameObject upmove;
    public bool ShouldMove = true;
    public float maxY = 130f; // ���ϴ� Y �� �ִ� ����
    private bool canMove = true;
    private bool canMoveUp = true;
    private bool canAttack = true;
    Animator animator;
    ParticleSystem Swim;
    GameObject punisher;

    void Start()
    {
        animator = GetComponent<Animator>();
        punisher =GameManager.Instance.Touchpoints[GameManager.Instance.PunisherIndex];//�й��� ����
        Swim = GetComponentInChildren<ParticleSystem>();
        upmove = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        if (ShouldMove) // �������� true �϶� ����
        {
            if(canMove)
            {
                animator.SetBool("Sprint", true);
                //�Ǿ ���ƾ��� ������ targetRotation �� ����
                Quaternion targetRotation = Quaternion.LookRotation(punisher.transform.position - transform.position);
                //ȸ���� ������ �ϰ� targetRotation �������� �� 
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
                //�������� ����Ʈ�� ��ġ�� �̵�
                transform.position = Vector3.MoveTowards(transform.position, punisher.transform.position, moveSpeed * Time.deltaTime);              
            }                     
            //����Ʈ�� ��ġ�� 0.2f���� ����� ���� 
            if (Vector3.Distance(transform.position, punisher.transform.position) <= 0.2f)
            {
                canMove = false; //�¿� �̵� ����
                animator.SetBool("Sprint", false);//�̵� �ִϸ��̼� ����
                if (transform.position.y >= maxY)
                {
                    canMoveUp = false;
                    if (canAttack)
                    {
                        StartCoroutine(Attack());
                    }
                }
                if(transform.position.y <= maxY)
                {
                    Swim.gameObject.SetActive(false);
                    StartCoroutine(Move());
                }
            }
        }        
    }
    
    IEnumerator Move()
    {
        Quaternion lookAt = Quaternion.identity;
        Vector3 lookatVec = (upmove.transform.position - this.transform.position).normalized;

        lookAt.SetLookRotation(lookatVec);
        transform.root.rotation = lookAt;

        transform.position = Vector3.Lerp(transform.position, upmove.transform.position, 0.01f);
        transform.localScale = new Vector3(25, 25, 25);
        yield return null;

    }
    IEnumerator Attack()
    {
        animator.SetBool("Attack", true);
        Handheld.Vibrate();//�����ֱ�
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("Attack", false);
        yield return new WaitForSeconds(0.8f);
        canAttack = false;
    }
    void OnAttackReady()
    {
        animator.SetFloat("AttackSpeed", 1f);
    }
    void OnAttack()
    {
        animator.SetFloat("AttackSpeed", 0.8f);
    }

}
