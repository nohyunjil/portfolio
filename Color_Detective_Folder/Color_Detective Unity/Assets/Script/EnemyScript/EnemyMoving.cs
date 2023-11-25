using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    Rigidbody2D rigidBody; //������ �ൿ�� �ϱ����� Rigidbody2D ������Ʈ�� ���� �޾ƿ´�
    Animator anim; //�� �ִϸ��̼ǰ��� �ޱ� ���� �ִϸ�����
    public GameObject bullet; //�Ѿ� ������Ʈ�� �޾ƿ� ���ӿ�����Ʈ
    public int nextMove; //�����ൿ ���� �� ����
    public float distance; //�����ɽ�Ʈ�� �޾ƿ� �����Ÿ�
    public float atkDistance; //�����ɽ�Ʈ�� �޾ƿ� ���ݰ��� �Ÿ�
    public float cooltime; //�Ѿ˹߻� ��Ÿ��
    private float currenttime; // �Ѿ� �߻� �⺻ ������
    public int Hp; //�� ü��
    public List<GameObject> coin = new List<GameObject>(); //���� ������ ������ ������ ������ ����ϱ� ���� ����Ʈ

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>(); //Rigidbody2D ������Ʈ�� ���� rigidbody�� �ҷ��´�
        anim = GetComponent<Animator>(); //Animator ������Ʈ�� ���� anim�� �ҷ��´�
        Think(); //Think �Լ��� ȣ���Ѵ�

        //Invoke("Think", 2); //2�� �� Think�Լ��� ȣ���Ѵ� -> ���� Think�Լ��� ����ؼ� ȣ���ϰ� �����. Update������ �����Ӹ��ٶ� �ʹ� ���� �����δ�. �̰� �ϴ� ���� ���� ����
    }

    void FixedUpdate()
    {
        //�̵�
        rigidBody.velocity = new Vector2(nextMove, rigidBody.velocity.y); //nextMove����ŭ �����ؼ� �����δ�.

        //�������� üũ
        Vector2 frontVector = new Vector2(rigidBody.position.x + nextMove * 0.3f, rigidBody.position.y); //frontVector�� ��ǥ���� �� ������Ʈ���� nextmove*0.3��ŭ �� �տ� ��ġ�ϵ��� �Ѵ�
        Debug.DrawRay(rigidBody.position, Vector2.down, new Color(0, 1, 0)); //����ĳ��Ʈ�� �ʷϻ����� �ؼ� ���� ���̵��� �Ѵ�.
        RaycastHit2D rayHit = Physics2D.Raycast(frontVector, Vector2.down, 1, LayerMask.GetMask("platform")); //���̾� �̸��� platform�� ������Ʈ�� frontVector��ġ���� �Ʒ��������� 1��ŭ ����� ����ĳ��Ʈ�� rayHit���� �����Ѵ�.
        if (rayHit.collider == null)   //null�� �����ɽ�Ʈ�� �ƹ��͵� �������� ���� ��츦 ��Ī 
        {
            Turn(); //Turn�Լ��� ȣ���Ѵ�.
        }

        // �� ���ݹ��� ���� �� ����ü �߻�
        if (transform.localScale.x == 6) //������Ʈ�� ������ ���� 6�̸� 
        {
            Debug.DrawRay(rigidBody.position, Vector2.right * distance, new Color(1, 0, 0)); //����ĳ��Ʈ�� ���������� �ؼ� ���� ���̵��� �Ѵ�.
            RaycastHit2D raycastR = Physics2D.Raycast(transform.position, transform.right, distance, LayerMask.GetMask("Player")); //���̾� �̸��� Player�� ������Ʈ�� distance��ŭ ���������� ����� ����ĳ��Ʈ�� raycastR�� �����Ѵ�.
            if (raycastR.collider != null) //�����ɽ�Ʈ�� �������� ���� ��� 
            {
                if (Vector2.Distance(transform.position, raycastR.collider.transform.position) < atkDistance) //���� �����Ÿ����� raycastR�� �浹�� ������Ʈ�� �Ÿ��� ª����
                {
                    if (currenttime <= 0) //��Ÿ���� 0�̸�
                    {
                        Instantiate(bullet, new Vector2(transform.position.x + 0.9f, transform.position.y), transform.rotation); //������Ʈ�� ���������� 0.9��ŭ �� �� �Ÿ����� �Ѿ� ����
                        currenttime = cooltime; //�Ѿ� �߻� �⺻�������� ������ ��Ÿ�� ������ �ʱ�ȭ 
                        anim.SetBool("Attack", true); //���� �ִϸ��̼��� �����Ѵ�.
                    }
                }
                currenttime -= Time.deltaTime;//�Ѿ� �߻� �⺻ �������� ���ӽð�������ŭ �پ���. -1���� �ƴ� ��¥ �ð����  
            }
            else //���� ���� ������
            {
                anim.SetBool("Attack", false); //���� �ִϸ��̼��� �����Ѵ�.
            }
        }
        else if(transform.localScale.x == -6) //������Ʈ�� ������ ���� -6�̸�
        {
            Debug.DrawRay(rigidBody.position, Vector2.left * distance, new Color(1, 0, 0)); //����ĳ��Ʈ�� ���������� �ؼ� ���� ���̵��� �Ѵ�.
            RaycastHit2D raycastL = Physics2D.Raycast(transform.position, transform.right * -1f, distance, LayerMask.GetMask("Player")); //���̾� �̸��� Player�� ������Ʈ�� distance��ŭ �������� ����� ����ĳ��Ʈ�� raycastR�� �����Ѵ�.
            if (raycastL.collider != null) //�����ɽ�Ʈ�� �������� ���� ��� 
            {
                if (Vector2.Distance(transform.position, raycastL.collider.transform.position) < atkDistance) //���� �����Ÿ����� raycastL�� �浹�� ������Ʈ�� �Ÿ��� ª����
                {
                    if (currenttime <= 0) //Ÿ���� 0�̸�
                    {
                        Instantiate(bullet, new Vector2(transform.position.x - 0.9f, transform.position.y), transform.rotation); //������Ʈ�� �������� 0.9��ŭ �� �� �Ÿ����� �Ѿ� ����
                        currenttime = cooltime; //�Ѿ� �߻� �⺻�������� ������ ��Ÿ�� ������ �ʱ�ȭ 
                        anim.SetBool("Attack", true); //���� �ִϸ��̼��� �����Ѵ�.
                    }
                }
                currenttime -= Time.deltaTime; //�Ѿ� �߻� �⺻ �������� ���ӽð�������ŭ �پ���. -1���� �ƴ� ��¥ �ð����   
            }
            else //���� ���� ������
            {
                anim.SetBool("Attack", false); //���� �ִϸ��̼��� �����Ѵ�.
            }
        }
        if(Hp <= 0) //Hp�� 0�̸�
        {
            StartCoroutine(Die()); //Die()�Լ��� ȣ�� �����̸� �ɾ�� ���� �ֱ⿡ StartCoroutine()�� ���
        }
    }

    //�ൿ�� �������� �Լ� -> ������ȯ
    //����Լ� -> �ڱ� �ڽ��� ȣ��
    void Think() //�� ������Ʈ�� �ڵ����� �� �� ���� �ൿ�� �ϴ� �Լ�
    {
        //���� Ȱ��
        nextMove = Random.Range(-1, 2);//�ּҰ��� �������� ���Եǳ� �ִ밪�� ���� x

        //��������Ʈ �ִϸ��̼�
        anim.SetInteger("WalkSpeed", nextMove);

        //��������Ʈ ������ȯ
        if (nextMove != 0) //nextMove�� 0�� �ƴϸ�
        {
            if(nextMove == -1) //nextMove�� -1�̸�
                transform.localScale = new Vector2(-6, 6); //������Ʈ�� �����ϰ��� -6,6���� ����
            else if(nextMove == 1) //nextMove�� 1�̸�
                transform.localScale = new Vector2(6, 6); //������Ʈ�� �����ϰ��� 6,6���� ����
        }

        //����Լ� - �ڱ��ڽ��� ȣ���� �ݺ������� �ϴ� ���
        float nextThinkTime = Random.Range(2f, 4f); //���� �Լ� ȣ�� �ð��� 2~3���� �������� nextThinkTime ����
        Invoke("Think", nextThinkTime); // Think�Լ��� nextThinkTime �ڿ� ȣ��
    }

    void Turn() //���� ���������� ���� �ڵ��� �Լ�
    {
        nextMove *= -1; //nectMove���� �ݴ�� ����
        if (nextMove == -1) //nextMove�� -1�̸�
            transform.localScale = new Vector2(-6, 6); //������Ʈ�� �����ϰ��� -6,6���� ����
        else if (nextMove == 1) //nextMove�� 1�̸�
            transform.localScale = new Vector2(6, 6); //������Ʈ�� �����ϰ��� 6,6���� ����

        CancelInvoke(); //Think�� Invoke�� ���� ����Ѵ�.
        Invoke("Think", 3); //3���Ŀ� �ٽ� Think�Լ��� ȣ���Ѵ�.
    }

    void DropItem() //���� ��� �Լ�
    {
        GameObject Drop = coin[Random.Range(0, 3)]; //coin�迭�� 0~2 3������ ������ �� �ϳ��� Drop�� ����
        Instantiate(Drop, transform.position, transform.rotation); //Drop�� �� ������Ʈ�� ��ġ�� ����
    }

    IEnumerator Die() //������ �ൿ�� �Լ�
    {
        anim.SetBool("Die", true); //�״� �ִϸ��̼� ���� �� 
        yield return new WaitForSeconds(0.833f); //0.833�� ��
        DropItem(); //���� ����Ѵ�
        Destroy(gameObject); //������Ʈ �����Ѵ�
        EnemyGenerator._instance.deathcount--; //���� �� �������� 1 ����
    }
}
