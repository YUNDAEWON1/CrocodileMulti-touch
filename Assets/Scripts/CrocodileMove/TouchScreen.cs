using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScreen : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float upSpeed = 100f;

    public float rotateSpeed = 10f;

    public Vector3 destinationPoint;

    public float waitTime = 2f;
    public bool ShoudMove = false;
    public bool ShouldAttack = false;
    private MoveAround moveAround;

    Animator animator;
    ParticleSystem Swim;
    GameManager gameManager;
    GameObject punisher;
    // Start is called before the first frame update
    void Start()
    {
        moveAround = GetComponent<MoveAround>();
        animator = GetComponent<Animator>();
        punisher =GameManager.Instance.Touchpoints[GameManager.Instance.TouchpointIndex];//�й��� ����
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (ShoudMove) // �������� true �϶� ����
        {
            moveAround.IsAround = false;//�����̵�����
            animator.SetBool("Sprint", true);
            //���� �ִϸ��̼� ����
            animator.SetBool("Attack", false);
            //�Ǿ ���ƾ��� ������ targetRotation �� ����
            Quaternion targetRotation = Quaternion.LookRotation(punisher.transform.position - transform.position);
            //ȸ���� ������ �ϰ� targetRotation �������� �� 
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed*Time.deltaTime);
            //�������� ����Ʈ�� ��ġ�� �̵�
            transform.position = Vector3.MoveTowards(transform.position, punisher.transform.position, moveSpeed*Time.deltaTime);
            //���� ��Ȱ��ȭ
            ShouldAttack = false;
            //����Ʈ�� ��ġ�� 0.2f���� ����� ���� 
            if (Vector3.Distance(transform.position, punisher.transform.position) <= 0.2f)
            {
                ShoudMove = false; //������ ����
                animator.SetBool("Sprint", false);//�̵� �ִϸ��̼� ����
                ShouldAttack = true;//���� ����              
            }
            if (ShouldAttack)
            {
                StartCoroutine(attack());
                //���� �ִϸ��̼� ���          
                StartCoroutine(attackRotation());
                //�����Ҷ� x ������ -90�� ȸ��
            }
        }
    }
    IEnumerator attack()
    {
        animator.SetBool("Attack", true);

        yield return new WaitForSeconds(1.4f);

        animator.SetBool("Attack", false);
    }
    IEnumerator attackRotation()
    {
        Swim.Stop();
        transform.Rotate(new Vector3(-90,0,0)*0.8f);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 100f, transform.position.z), upSpeed);
        yield return new WaitForSeconds(1.4f); // 1�ʴ��
        transform.position = new Vector3(transform.position.x, 12, transform.position.z);
        transform.Rotate(new Vector3(90, 0, 0) * 0.8f);
        Swim.Play();

    }
}
