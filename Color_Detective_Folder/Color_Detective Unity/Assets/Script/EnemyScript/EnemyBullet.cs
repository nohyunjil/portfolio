using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject Enemy; //�� ������Ʈ�� �޾ƿ´�
    Rigidbody2D rigidBody; //������ �ൿ�� �ϱ����� Rigidbody2D ������Ʈ�� ���� �޾ƿ´�
    Transform PlayerPos; // �÷��̾ �����ϴ� ��ġ��

    public float speed; //�Ѿ� �̵� �ӵ�

    Vector2 dir; //�Ѿ��� ���ư��� ���� ����

    void Start()
    {
        Invoke("DestroyBullet", 2f); //2���Ŀ� DestroyBullet�Լ� ȣ��
        rigidBody = GetComponent<Rigidbody2D>(); //Rigidbody2D ������Ʈ�� ���� rigidbody�� �ҷ��´�

        PlayerPos = GameObject.Find("Player").GetComponent<Transform>(); //�÷��̾������Ʈ�� ���� PlayerPos�� �ҷ��´�.
        dir = PlayerPos.position - transform.position; // �Ѿ��� ���ư��� ���� ���⿡ �÷��̾�� ���� ��ġ���� �� ���� �����Ѵ�
        GetComponent<Rigidbody2D>().AddForce(dir * Time.deltaTime * speed * 1000); // ���� ������ �Ѿ˿� ����*�ð�*������ �ӵ� * 1000��ŭ�� ���� �����ش�

    }
    
    void OnBecameInvisible() //ȭ�� �ۿ� ������ 
    { 
        Destroy(gameObject); //�Ѿ� ����
    }

    void DestroyBullet() //�Ѿ� ���� �Լ�
    { 
        Destroy(gameObject); //�Ѿ� ����
    }

    private void OnCollisionEnter2D(Collision2D collision) //�浹�� �������� ��
    {
        if (collision.gameObject.CompareTag("platform")) //�±��̸��� platform�� ������Ʈ�� �浹�ϸ�
        {
            Destroy(gameObject); //�Ѿ� ����
        }

        if (collision.gameObject.name == "Player") //�̸��� Player�� ������Ʈ�� �浹�ϸ�
        {
            Destroy(gameObject); //�Ѿ� ����
        }
    }
}
