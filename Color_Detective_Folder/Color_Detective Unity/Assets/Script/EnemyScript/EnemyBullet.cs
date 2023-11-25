using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject Enemy; //적 오브젝트를 받아온다
    Rigidbody2D rigidBody; //물리적 행동을 하기위해 Rigidbody2D 컴포넌트의 값을 받아온다
    Transform PlayerPos; // 플레이어가 존재하는 위치값

    public float speed; //총알 이동 속도

    Vector2 dir; //총알이 날아가기 위한 방향

    void Start()
    {
        Invoke("DestroyBullet", 2f); //2초후에 DestroyBullet함수 호출
        rigidBody = GetComponent<Rigidbody2D>(); //Rigidbody2D 컴포넌트의 값을 rigidbody에 불러온다

        PlayerPos = GameObject.Find("Player").GetComponent<Transform>(); //플레이어오브젝트의 값을 PlayerPos에 불러온다.
        dir = PlayerPos.position - transform.position; // 총알이 날아가기 위한 방향에 플레이어와 적의 위치값을 뺀 값을 대입한다
        GetComponent<Rigidbody2D>().AddForce(dir * Time.deltaTime * speed * 1000); // 적이 생성한 총알에 방향*시간*설정한 속도 * 1000만큼의 힘을 가해준다

    }
    
    void OnBecameInvisible() //화면 밖에 나가면 
    { 
        Destroy(gameObject); //총알 삭제
    }

    void DestroyBullet() //총알 삭제 함수
    { 
        Destroy(gameObject); //총알 삭제
    }

    private void OnCollisionEnter2D(Collision2D collision) //충돌을 시작했을 때
    {
        if (collision.gameObject.CompareTag("platform")) //태그이름이 platform인 오브젝트와 충돌하면
        {
            Destroy(gameObject); //총알 삭제
        }

        if (collision.gameObject.name == "Player") //이름이 Player인 오브젝트와 충돌하면
        {
            Destroy(gameObject); //총알 삭제
        }
    }
}
