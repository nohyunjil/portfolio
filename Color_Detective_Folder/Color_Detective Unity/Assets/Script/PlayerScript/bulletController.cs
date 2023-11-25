using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    Rigidbody2D rigidBody; //물리적 행동을 하기위해 Rigidbody2D 컴포넌트의 값을 받아온다
    public float speed; //총알 이동 속도

    void Start()
    {

        Invoke("DestroyBullet", 4);
        rigidBody = GetComponent<Rigidbody2D>(); //Rigidbody2D 컴포넌트의 값을 rigidbody에 불러온다
        if (GameObject.Find("Player").GetComponent<SpriteRenderer>().flipX == true) //플레이어 오브젝트의 SpriteRenderer의 flipX가 실행되면
        {
            rigidBody.velocity = new Vector2(speed * -1, rigidBody.velocity.y); //왼쪽으로 날아간다
        }
        else //flipX가 실행 안되면 
            rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y); //오른쪽으로 날아간다.
    }   
    void OnBecameInvisible() //화면밖으로 나가면
    {
        Destroy(gameObject); //총알 삭제
    }

    void DestroyBullet() //총알 삭제 함수
    {
        Destroy(gameObject); //총알 삭제
    }

    private void OnCollisionEnter2D(Collision2D collision) //충돌을 시작했을때
    {
        if (collision.gameObject.CompareTag("platform")) //태그이름이 platform인 오브젝트와 충돌하면
        {
            Destroy(gameObject); //총알 삭제
        }

        if (collision.gameObject.CompareTag("REnemy")) //태그이름이 REnemy인 오브젝트와 충돌하면
        {
            Destroy(gameObject); //총알 삭제
            if (GameObject.FindGameObjectWithTag("PlayerBullet").GetComponent<SpriteRenderer>().color == new Color(206 / 255f, 85 / 255f, 57 / 255f,255 / 255f)) //PlayerBullet태그를 가진 오브젝트의 SpriteRenderer의 Color값이 해당 설정값이면
                collision.gameObject.GetComponent<EnemyMoving>().Hp -= 1; //적의 체력을 1 깍는다
        }

        if (collision.gameObject.CompareTag("GEnemy")) //태그이름이 GEnemy인 오브젝트와 충돌하면
        {
            Destroy(gameObject); //총알 삭제
            if (GameObject.FindGameObjectWithTag("PlayerBullet").GetComponent<SpriteRenderer>().color == new Color(157 / 255f, 196 / 255f, 87 / 255f,255 / 255f)) //PlayerBullet태그를 가진 오브젝트의 SpriteRenderer의 Color값이 해당 설정값이면
                collision.gameObject.GetComponent<EnemyMoving>().Hp -= 1; //적의 체력을 1 깍는다
        }

        if (collision.gameObject.CompareTag("BEnemy")) //태그이름이 BEnemy인 오브젝트와 충돌하면
        {
            Destroy(gameObject); //총알 삭제
            if (GameObject.FindGameObjectWithTag("PlayerBullet").GetComponent<SpriteRenderer>().color == new Color(115 / 255f, 190 / 255f, 213 / 255f,255 / 255f)) //PlayerBullet태그를 가진 오브젝트의 SpriteRenderer의 Color값이 해당 설정값이면
                collision.gameObject.GetComponent<EnemyMoving>().Hp -= 1; //적의 체력을 1 깍는다
        }

        if (collision.gameObject.CompareTag("WEnemy")) //태그이름이 WEnemy인 오브젝트와 충돌하면
        {
            Destroy(gameObject); //총알 삭제
            if (GameObject.FindGameObjectWithTag("PlayerBullet").GetComponent<SpriteRenderer>().color == Color.white)
                collision.gameObject.GetComponent<EnemyMoving>().Hp -= 1; //적의 체력을 1 깍는다
        }
    }

}
