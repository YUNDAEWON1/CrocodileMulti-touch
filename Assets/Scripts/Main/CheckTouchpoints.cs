using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTouchpoints : MonoBehaviour
{
    LayerMask touchpointsLayer;
    Vector3 boxSize = new Vector3(30f, 0.1f, 30f);
    [SerializeField] float rad = 30f;

    [SerializeField] Transform downLeft;    // ������ (0, 0)
    [SerializeField] Transform upLeft;
    [SerializeField] Transform upRight;
    [SerializeField] float screenWidth;
    [SerializeField] float screenHeight;

    [SerializeField] bool debugMode = false;
    [SerializeField] bool isTouchingCircle = false;

    void Start()
    {
        touchpointsLayer = LayerMask.GetMask("Effect");

        // ȭ�� ũ�� �� �ػ� ���
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    void Update()
    {
        // ��ġ �Է��� �߻����� ��
        if (Input.touchCount > 0)
        {
            // ��� ��ġ �Է¿� ���� �ݺ�
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                
                // ��ġ �Է��� ���۵��� ��
                if (touch.phase == TouchPhase.Began)
                {
                    // ��ġ ����Ʈ�� ȭ�� ���� ������ ��ȯ
                    Vector3 viewport = new Vector3(touch.position.x / screenWidth, touch.position.y / screenHeight, 0f);

                    // ȭ���� ������ Unity �� ���� ��ǥ�� ��ȯ
                    Vector3 touchPosition = new Vector3(
                        Mathf.Lerp(downLeft.position.x, upRight.position.x, viewport.x),
                        15f,
                        Mathf.Lerp(downLeft.position.z, upLeft.position.z, viewport.y)
                    );

                    if(debugMode)
                    {
                        Debug.Log("��ġ�� �� viewport : " + viewport);
                        Debug.Log("��ġ�� �� �� �� ��ġ : " + touchPosition);
                    }

                    // ���� ��ġ�� ���� ��ġ����Ʈ�� ����� ã��
                    Collider[] colliders = Physics.OverlapBox(touchPosition, boxSize, Quaternion.identity, touchpointsLayer);

                    if (colliders.Length == 0)
                    {
                        if (debugMode)
                        {
                            Debug.Log("�ȴ���");
                        }
                        // TODO : �ٽ� ��ġ�ش޶�� ������Ϸ� ���ư����� 
                    }
                    else
                    {
                        // ��Ȯ���� ������ ���ؼ� �Ÿ� üũ 
                        foreach (Collider col in colliders)
                        {
                            Transform circle = col.GetComponent<Transform>();
                            float distance = Vector3.Distance(circle.position, touchPosition);
                            
                            if (debugMode)
                            {
                                Debug.Log("�� ��ġ : " + circle.position);
                                Debug.Log("���� �Ÿ� : " + distance);
                            }
                                
                            if (distance < rad)
                            {
                                if (debugMode)
                                {
                                    Debug.Log("�н�");
                                }
                            }
                            else
                            {
                                if (debugMode)
                                {
                                    Debug.Log("�ȴ���");
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
