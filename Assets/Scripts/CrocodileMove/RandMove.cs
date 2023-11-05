using UnityEngine;

public class RandMove : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float waitTime;
    [SerializeField] float startWaitTime;
    [SerializeField] float rotate = 0.5f;
    [SerializeField] float attackRotate = 0.5f;
    [SerializeField] float respon = 5f;
    [SerializeField] Transform[] moveSpot;
    int randomSpot;
    public bool IsTouch;
    public bool IsMove;
    public bool IsMoving;
    Animator animator;
    private void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpot.Length);
        animator = GetComponent<Animator>();
        IsTouch = false;
        IsMove = false;
        IsMoving = true;
    }
    private void Update()
    {

        if (!IsTouch) //��ġ���Ҷ�
        {
            
            if(IsMoving)
            {
                Move(); //������ Ȱ��ȭ
                animator.SetBool("Sprint", true); // �ִϸ��̼� ������ Ȱ��ȭ
            }
                
            

            if (Vector3.Distance(transform.position, moveSpot[randomSpot].position) <= 0.2f) // ���꽺�̰��� �Ÿ��� 0.1f ���� ���������
            {
                //�ð�������, ����Ʈ Ÿ���� 0���� �۾����� �ٽ� �������� (������ ���̵� ����)                  
                if (waitTime <= 0)
                {
                    randomSpotMaking();//�������� �����Ұ� ����
                    IsMoving = true; //������ Ȱ��ȭ
                    waitTime = Random.Range(0, 2f);
                    startWaitTime = waitTime;
                }
                else
                {
                    IsMoving = false;
                    animator.SetBool("Sprint", false);
                    waitTime -= Time.deltaTime;
                }
            }
            
        }
    }
    private void Move() //�����Ӱ� ȸ���� ��
    {
        IsMove = false;
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
