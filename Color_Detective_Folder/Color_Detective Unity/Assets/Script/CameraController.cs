using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour //ī�޶� �÷��̾ ����ٴϰ� �ϴ� ��ũ��Ʈ
{
    [SerializeField]
    Transform playerTransform; //Transform ������Ʈ ���� - �� ������Ʈĭ�� ���� ������Ʈ�� ��ġ���� �ҷ��´�.
    [SerializeField]
    Vector3 cameraPosition; //�� ��ũ��Ʈ�� �� ������Ʈ�� transform���� �����Ѵ�.

    [SerializeField]
    Vector2 center; //ī�޶��� ���Ͱ��� �����Ѵ�
    [SerializeField]
    Vector2 mapSize; //ī�޶��� ũ�⸦ ���� �� ����� �����Ѵ�

    [SerializeField]
    float cameraMoveSpeed; //ī�޶� �÷��̾ ���󰡴� �ӵ�
    float height; //���α���
    float width; //���α���
     
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>(); //playerTransform�� �÷��̾� ������Ʈ�� ��ǥ���� �ҷ��´�.

        height = Camera.main.orthographicSize; //���α��̴� ī�޶��� ���� ����� �ڵ����� �޾ƿ� ȣ���Ѵ�.
        width = height * Screen.width / Screen.height; //���α��̴� ���� ������ �̿��Ͽ� ȣ���Ѵ�.
    }

    void FixedUpdate() 
    {
        LimitCameraArea(); //�� �Լ��� �ݺ� ȣ���Ѵ�
    }

    void LimitCameraArea() //ī�޶� �ൿ�ݰ� ������ �������ִ� �Լ�
    {
        transform.position = Vector3.Lerp(transform.position,
                                          playerTransform.position + cameraPosition,
                                          Time.deltaTime * cameraMoveSpeed); // �� ī�޶��� ��ġ�� �÷��̾� ��ġ�� Time.deltaTime * cameraMoveSpeed�� �ӵ��� �ڵ��󰡴� ���
        float lx = mapSize.x - width; //�� ���� ������� ī�޶� ���� ����� �� ����
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x); //ī�޶� ������ �� ���� ����� ����� �ʰ� �ϴ� ����

        float ly = mapSize.y - height; //�� ���� ������� ī�޶� ���� ����� �� ����
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y); //ī�޶� ������ �� ���� ����� ����� �ʰ� �ϴ� ����

        transform.position = new Vector3(clampX, clampY, -10f); //ī�޶� ��ġ���� �� ���� ������ �Ѱ�� �д�
    }

    private void OnDrawGizmos() //�� �ʻ���� �����Ѱ��� ���� ���̰� �ϱ����� ȿ���� �ִ� �Լ�
    {
        Gizmos.color = Color.red; //����������
        Gizmos.DrawWireCube(center, mapSize * 2); //�ʻ���� �°� �簢������ �׸���.
    }
}
