using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    Rigidbody2D rigidBody; //������ �ൿ�� �ϱ����� Rigidbody2D ������Ʈ�� ���� �޾ƿ´�
    public float speed; //�Ѿ� �̵� �ӵ�

    void Start()
    {

        Invoke("DestroyBullet", 4);
        rigidBody = GetComponent<Rigidbody2D>(); //Rigidbody2D ������Ʈ�� ���� rigidbody�� �ҷ��´�
        if (GameObject.Find("Player").GetComponent<SpriteRenderer>().flipX == true) //�÷��̾� ������Ʈ�� SpriteRenderer�� flipX�� ����Ǹ�
        {
            rigidBody.velocity = new Vector2(speed * -1, rigidBody.velocity.y); //�������� ���ư���
        }
        else //flipX�� ���� �ȵǸ� 
            rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y); //���������� ���ư���.
    }   
    void OnBecameInvisible() //ȭ������� ������
    {
        Destroy(gameObject); //�Ѿ� ����
    }

    void DestroyBullet() //�Ѿ� ���� �Լ�
    {
        Destroy(gameObject); //�Ѿ� ����
    }

    private void OnCollisionEnter2D(Collision2D collision) //�浹�� ����������
    {
        if (collision.gameObject.CompareTag("platform")) //�±��̸��� platform�� ������Ʈ�� �浹�ϸ�
        {
            Destroy(gameObject); //�Ѿ� ����
        }

        if (collision.gameObject.CompareTag("REnemy")) //�±��̸��� REnemy�� ������Ʈ�� �浹�ϸ�
        {
            Destroy(gameObject); //�Ѿ� ����
            if (GameObject.FindGameObjectWithTag("PlayerBullet").GetComponent<SpriteRenderer>().color == new Color(206 / 255f, 85 / 255f, 57 / 255f,255 / 255f)) //PlayerBullet�±׸� ���� ������Ʈ�� SpriteRenderer�� Color���� �ش� �������̸�
                collision.gameObject.GetComponent<EnemyMoving>().Hp -= 1; //���� ü���� 1 ��´�
        }

        if (collision.gameObject.CompareTag("GEnemy")) //�±��̸��� GEnemy�� ������Ʈ�� �浹�ϸ�
        {
            Destroy(gameObject); //�Ѿ� ����
            if (GameObject.FindGameObjectWithTag("PlayerBullet").GetComponent<SpriteRenderer>().color == new Color(157 / 255f, 196 / 255f, 87 / 255f,255 / 255f)) //PlayerBullet�±׸� ���� ������Ʈ�� SpriteRenderer�� Color���� �ش� �������̸�
                collision.gameObject.GetComponent<EnemyMoving>().Hp -= 1; //���� ü���� 1 ��´�
        }

        if (collision.gameObject.CompareTag("BEnemy")) //�±��̸��� BEnemy�� ������Ʈ�� �浹�ϸ�
        {
            Destroy(gameObject); //�Ѿ� ����
            if (GameObject.FindGameObjectWithTag("PlayerBullet").GetComponent<SpriteRenderer>().color == new Color(115 / 255f, 190 / 255f, 213 / 255f,255 / 255f)) //PlayerBullet�±׸� ���� ������Ʈ�� SpriteRenderer�� Color���� �ش� �������̸�
                collision.gameObject.GetComponent<EnemyMoving>().Hp -= 1; //���� ü���� 1 ��´�
        }

        if (collision.gameObject.CompareTag("WEnemy")) //�±��̸��� WEnemy�� ������Ʈ�� �浹�ϸ�
        {
            Destroy(gameObject); //�Ѿ� ����
            if (GameObject.FindGameObjectWithTag("PlayerBullet").GetComponent<SpriteRenderer>().color == Color.white)
                collision.gameObject.GetComponent<EnemyMoving>().Hp -= 1; //���� ü���� 1 ��´�
        }
    }

}
