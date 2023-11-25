using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerController : MonoBehaviour
{
    public int Hp; //�÷��̾� ü��
    public float MaxSpeed; //�÷��̾� �ִ� �̵��ӵ�
    public float Jumppower; //�÷��̾� ���� �Ŀ�
    public float curtime; //��Ÿ��
    public bool SoundCheck = true;
    int jumpcunt = 2; //���� Ƚ��
    Rigidbody2D rigidBody; //������ �ൿ�� �ϱ����� Rigidbody2D ������Ʈ�� ���� �޾ƿ´�
    SpriteRenderer spriteRenderer; //�̹��� ��ȭ�� �����ϱ� ���� SpriteRenderer ������Ʈ�� ���� �޾ƿ´�
    Animator anim; //�� �ִϸ��̼ǰ��� �ޱ� ���� �ִϸ�����
    public GameObject bullet; //�Ѿ� ������Ʈ�� �޾ƿ� ���ӿ�����Ʈ

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>(); //Rigidbody2D ������Ʈ�� ���� rigidbody�� �ҷ��´�
        spriteRenderer = GetComponent<SpriteRenderer>(); //SpriteRenderer ������Ʈ�� ���� spriteRenderer�� �ҷ��´�
        anim = GetComponent<Animator>(); //Animator ������Ʈ�� ���� anim�� �ҷ��´�
        bullet.GetComponent<SpriteRenderer>().color = Color.white;  //bullet�� SpriteRenderer������Ʈ�� ���� ���� ������� �����Ѵ�.
    } 

    private void Update() //���� ����
    {

        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position); //ĳ������ ���� ��ǥ�� ����Ʈ ��ǥ��� ��ȯ���ش�.
        viewPos.x = Mathf.Clamp01(viewPos.x); //x���� 0�̻�, 1���Ϸ� �����Ѵ�.
        viewPos.y = Mathf.Clamp01(viewPos.y); //y���� 0�̻�, 1���Ϸ� �����Ѵ�.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos); //�ٽ� ���� ��ǥ�� ��ȯ�Ѵ�.
        transform.position = worldPos; //��ǥ�� �����Ѵ�.

        //�÷��̾� �̵��ӵ� ����
        if (Input.GetButtonUp("Horizontal")) //HorizontalŰ�� ����
        {
            //rigidbody.velocity.normalized //������ ũ�⸦ 1�� ������ ����,����� ũ�⸦ ���� ������ �ֱ� ���� ������ ���Ҷ�
            rigidBody.velocity = new Vector2(rigidBody.velocity.normalized.x * 0.8f, rigidBody.velocity.y); //�¿� ��� �����̵� 0.8��ŭ ���� �����Ѵ�.
        }

        //�÷��̾� �̹��� ����
        if (Input.GetButton("Horizontal")) //HorizontalŰ�� ������ �ִ� ���� 
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1; //Horizontal�� ������ ���� -1 �̸� flipX�� ���� true�� �Ͽ��� / �����̸� -1 �������̸� 1

        //�÷��̾� �ִϸ��̼� ����
        if (Mathf.Abs(rigidBody.velocity.x) < 0.5f) //�÷��̾� x�� �ӵ��� 0.5 ���ϸ� 
            anim.SetBool("isWalk", false); //�ȴ� �ִϸ��̼� ��Ȱ��ȭ
        else //0.5 �̻��̸�
            anim.SetBool("isWalk", true); //�ȴ� �ִϸ��̼� ����

        //�÷��̾� ���� 
        if (Input.GetButtonDown("Jump") && jumpcunt > 1) //Jump��ư(�����̽���ư)�� ������, ����ī��Ʈ�� 0�ʰ��϶�
        {
            rigidBody.AddForce(Vector2.up * Jumppower, ForceMode2D.Impulse); //���ʹ������� �����Ŀ� �� ��ŭ ���� ���Ѵ�
            anim.SetBool("isJump", true); //���� �ִϸ��̼��� ȣ��
            jumpcunt--; //����ī��Ʈ�� 1����
            SoundManager.instance.SFXPlay("Jump", SoundManager.instance.Jump);
        }

        if (Input.GetKeyDown(KeyCode.Q)) //QŰ�� ������
        {
            SoundManager.instance.SFXPlay("Attack", SoundManager.instance.Attack);
            if (spriteRenderer.flipX != true) //flipX�� false��
            {
                Instantiate(bullet, new Vector2(transform.position.x + 0.8f, transform.position.y), transform.rotation); //�÷��̾� ������Ʈ�� ���������� 0.8��ŭ�� �� �� �Ÿ����� �Ѿ� ����
                anim.SetBool("Attack", true); //���� �ִϸ��̼� ȣ��
            }
            else //flipX�� true��
            {
                Instantiate(bullet, new Vector2(transform.position.x - 0.8f, transform.position.y), transform.rotation); //�÷��̾� ������Ʈ�� ���������� -0.8��ŭ�� �� �� �Ÿ����� �Ѿ� ����
                anim.SetBool("Attack", true); //���� �ִϸ��̼� ȣ��
            }
        }

        if (Input.GetKeyUp(KeyCode.Q)) //Q��ư�� ����
        {
            anim.SetBool("Attack", false); //���ݾִϸ��̼� ����
        }

        if (Input.GetKeyDown(KeyCode.E)) //E��ư�� ������
        {
            bullet.GetComponent<SpriteRenderer>().color = Color.white; //�Ѿ� ������Ʈ�� ���� ������� ����
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.white; //�÷��̾� ������Ʈ�� ���� ������� ����
        }

        if (Hp <= 0 && SoundCheck == true) //Hp�� 0�̸�
        {
            SoundManager.instance.SFXPlay("over", SoundManager.instance.GameOver);
            SoundCheck = false;
            StartCoroutine(PlayerDied()); //PlayerDied�Լ� ȣ�� �����̰� �ɸ��� ������ �ֱ⿡ StartCoroutine�� ���
        }

        if(GameDirector.instance.Timer == 0 && EnemyGenerator._instance.deathcount >= 0 && SoundCheck == true)
        {
            SoundManager.instance.SFXPlay("over", SoundManager.instance.GameOver);
            SoundCheck = false;
            GameDirector.instance.Gameover();
        }

        //�÷��̾� �̵��ӵ�
        float h = Input.GetAxisRaw("Horizontal"); //Horizontal�� ������ ���� �ҷ��� h�� ���� �����̸� -1 �������̸� 1

        rigidBody.AddForce(Vector2.right * h, ForceMode2D.Impulse); //������*����� ��ŭ ����ؼ� ���� ���Ѵ�.

        if (rigidBody.velocity.x > MaxSpeed) //���������� �ִ� �̵��ӵ��� ������
            rigidBody.velocity = new Vector2(MaxSpeed, rigidBody.velocity.y); //�ִ��̵� �ӵ��� �����Ѵ�
        else if (rigidBody.velocity.x < MaxSpeed * (-1)) //�������� �ִ� �̵��ӵ��� ������
            rigidBody.velocity = new Vector2(MaxSpeed * (-1), rigidBody.velocity.y); //�ִ��̵� �ӵ��� �����Ѵ�
    }

    //�� ������Ʈ �浹 ������ ó�� ���
    private void OnCollisionEnter2D(Collision2D collision) //�浹�� ����������
    {
        if (collision.gameObject.tag == "REnemy") //�±��̸��� REnemy�� ������Ʈ�� �浹�ϸ�
        {
            OnDamaged(collision.transform.position); //OnDamaged�� �浹 ������Ʈ ��ǥ���� �־ ȣ��
        }

        if (collision.gameObject.tag == "GEnemy") //�±��̸��� GEnemy�� ������Ʈ�� �浹�ϸ�
        {
            OnDamaged(collision.transform.position); //OnDamaged�� �浹 ������Ʈ ��ǥ���� �־ ȣ��
        }

        if (collision.gameObject.tag == "BEnemy") //�±��̸��� BEnemy�� ������Ʈ�� �浹�ϸ�
        {
            OnDamaged(collision.transform.position); //OnDamaged�� �浹 ������Ʈ ��ǥ���� �־ ȣ��
        }

        if (collision.gameObject.tag == "WEnemy") //�±��̸��� WEnemy�� ������Ʈ�� �浹�ϸ�
        {
            OnDamaged(collision.transform.position); //OnDamaged�� �浹 ������Ʈ ��ǥ���� �־ ȣ��
        }

        if (collision.gameObject.tag == "RBullet") //�±��̸��� REnemy�� ������Ʈ�� �浹�ϸ�
        {
            OnDamaged(collision.transform.position); //OnDamaged�� �浹 ������Ʈ ��ǥ���� �־ ȣ��
        }

        if (collision.gameObject.tag == "GBullet") //�±��̸��� GEnemy�� ������Ʈ�� �浹�ϸ�
        {
            OnDamaged(collision.transform.position); //OnDamaged�� �浹 ������Ʈ ��ǥ���� �־ ȣ��
        }

        if (collision.gameObject.tag == "BBullet") //�±��̸��� BEnemy�� ������Ʈ�� �浹�ϸ�
        {
            OnDamaged(collision.transform.position); //OnDamaged�� �浹 ������Ʈ ��ǥ���� �־ ȣ��
        } 

        if (collision.gameObject.tag == "WBullet") //�±��̸��� WEnemy�� ������Ʈ�� �浹�ϸ�
        {
            OnDamaged(collision.transform.position); //OnDamaged�� �浹 ������Ʈ ��ǥ���� �־ ȣ��
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "platform")
        {
            anim.SetBool("isJump", false); //���� �ִϸ��̼� ����
            jumpcunt = 2; //���� ī��Ʈ�� 2�� �ʱ�ȭ
        }
    }

    //�׾Ƹ� �� ��ȭ �� Ʃ�丮�� �� �浹���
    private void OnTriggerEnter2D(Collider2D collision) //Ʈ���� �浹�� ����������
    {
        if (collision.gameObject.CompareTag("rPot")) //�±��̸��� rPot�� ������Ʈ�� �浹�ϸ�
        {
            bullet.GetComponent<SpriteRenderer>().color = new Color(206/255f, 85/255f, 57/255f, 255/255f); //�Ѿ� ������ �ش� ���������� �ٲ۴�.
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(206/255f, 85/255f, 57/255f,255/255f); //�÷��̾� ������ �ش� ���������� �ٲ۴�.
        }
        if (collision.gameObject.tag == "bPot") //�±��̸��� bPot�� ������Ʈ�� �浹�ϸ�
        {
            bullet.GetComponent<SpriteRenderer>().color = new Color(115/255f, 190/255f, 213/255f,255/255f); //�Ѿ� ������ �ش� ���������� �ٲ۴�.
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(115/255f, 190/255f, 213/255f,255/255f); //�÷��̾� ������ �ش� ���������� �ٲ۴�.
        }
        if (collision.gameObject.tag == "gPot") //�±��̸��� gPot�� ������Ʈ�� �浹�ϸ�
        { 
            bullet.GetComponent<SpriteRenderer>().color = new Color(157/255f, 196/255f, 87/255f,255/255f); //�Ѿ� ������ �ش� ���������� �ٲ۴�.
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(157/255f, 196/255f, 87/255f,255/255f); //�÷��̾� ������ �ش� ���������� �ٲ۴�.
        }

        if(collision.gameObject.name == "StartGo") //�±��̸��� StartGo�� ������Ʈ�� �浹�ϸ�
        {
            SceneManager.LoadScene("GameStage1"); //�����÷���ȭ������ �̵��Ѵ�.
        }
    }

    //�� �������� ���
    private void OnTriggerStay2D(Collider2D collision) //Ʈ���� �浹�� �ϴ� ����
    {
        if (collision.gameObject.CompareTag("rCoin")) //�±��̸��� rCoin�� ������Ʈ�� �浹�ϴ� ���ȿ�
        {
            if (Input.GetKeyDown(KeyCode.W)) //WŰ�� ������
            {
                bullet.GetComponent<SpriteRenderer>().color = new Color(206/255f, 85/255f, 57/255f,255/255f); //�Ѿ� ������ �ش� ���������� �ٲ۴�.
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(206/255f, 85/255f, 57/255f,255/255f); //�÷��̾� ������ �ش� ���������� �ٲ۴�.
                Destroy(collision.gameObject); // �浹 ������Ʈ ����
            }
        }
        if (collision.gameObject.tag == "bCoin") //�±��̸��� bCoin�� ������Ʈ�� �浹�ϴ� ���ȿ�
        {
            if (Input.GetKeyDown(KeyCode.W)) //WŰ�� ������
            {
                bullet.GetComponent<SpriteRenderer>().color = new Color(115 / 255f, 190 / 255f, 213 / 255f,255/255f); //�Ѿ� ������ �ش� ���������� �ٲ۴�.
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(115 / 255f, 190 / 255f, 213 / 255f,255 / 255f); //�÷��̾� ������ �ش� ���������� �ٲ۴�.
                Destroy(collision.gameObject); // �浹 ������Ʈ ����
            }
        }
        if (collision.gameObject.tag == "gCoin") //�±��̸��� gCoin�� ������Ʈ�� �浹�ϴ� ���ȿ�
        {
            if (Input.GetKeyDown(KeyCode.W)) //WŰ�� ������
            {
                bullet.GetComponent<SpriteRenderer>().color = new Color(157 / 255f, 196 / 255f, 87 / 255f,255 / 255f); //�Ѿ� ������ �ش� ���������� �ٲ۴�.
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(157 / 255f, 196 / 255f, 87 / 255f,255 / 255f); //�÷��̾� ������ �ش� ���������� �ٲ۴�.
                Destroy(collision.gameObject); //�浹 ������Ʈ ����
            }
        }
    }

    //������ ���� �� �����ð� ����
    void OnDamaged(Vector2 targetPos) //������ ��ġ���� �������� �޴� �Լ�
    {
        //���̾� ��ȯ
        gameObject.layer = 9; //���ӿ�����Ʈ�� ���̾� ��ȣ�� 9���� �����Ѵ�.

        //�帴������
        spriteRenderer.color = new Color(1, 1, 1, 0.4f); //���� ������ 0.4�� ����
        bullet.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f); //�Ѿ� ���� ������ 0.4�� ����

        //�°� ��� �ݵ� 
        int dirct = transform.position.x - targetPos.x > 0 ? 1 : -1; //�÷��̾�� �浹 ������Ʈ ��ġ�� ���� 0���� ũ�� 1 ������ -1�� dirct�� ����
        rigidBody.AddForce(new Vector2(dirct, 1) * 7, ForceMode2D.Impulse); //dirct*7���⸸ŭ�� ���� ���Ѵ�

        Invoke("OffDamaged", 2f); //2���Ŀ� OffDamaged�Լ� ȣ��

        //ü�� ����
        Hp--; //Hp�� 1��ŭ ���δ�
        if (GameDirector.instance.lifeImg.Count > Hp) //GameDirector�� UI�̹��� ������ HP���� ������
        {
            GameDirector.instance.lifeImg[GameDirector.instance.lifeImg.Count - 1].SetActive(false); //UI�̹��� ������ ���̰� �� ��ȣ�� UI�� ��Ȱ��ȭ ��Ų��.
            GameDirector.instance.lifeImg.Remove(GameDirector.instance.lifeImg[GameDirector.instance.lifeImg.Count - 1]); //��Ȱ��ȭ �� ������Ʈ�� ������Ų��.
        }


    }

    void OffDamaged() //������ �޴� ȿ�� ����
    {
        gameObject.layer = 6; //���ӿ�����Ʈ�� ���̾� ��ȣ�� 6���� �����Ѵ�.

        spriteRenderer.color = new Color(1, 1, 1, 1); //���� ������ 1�� ����
        bullet.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1); //�Ѿ� ���� ������ 1�� ����
    }

    IEnumerator PlayerDied() //�÷��̾� ���� �Լ�
    {
        anim.SetBool("isDie", true); //�״� �ִϸ��̼� ����
        yield return new WaitForSeconds(0.4f); //0.4�� �Ŀ�
        gameObject.SetActive(false); //�÷��̾� ������Ʈ ��Ȱ��ȭ -> CameraController���� ��ǥ���� ��� �޾ƿ;��ؼ� Destroy�� �Ұ��� 
        GameDirector.instance.Gameover(); //���ӿ����Լ��� ȣ��
    }

}
