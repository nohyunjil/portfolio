using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    Rigidbody2D rigidBody; //물리적 행동을 하기위해 Rigidbody2D 컴포넌트의 값을 받아온다
    Animator anim; //적 애니메이션값을 받기 위한 애니메이터
    public GameObject bullet; //총알 오브젝트를 받아올 게임오브젝트
    public int nextMove; //다음행동 변수 및 방향
    public float distance; //레이케스트로 받아올 사정거리
    public float atkDistance; //레이케스트로 받아올 공격개시 거리
    public float cooltime; //총알발사 쿨타임
    private float currenttime; // 총알 발사 기본 설정값
    public int Hp; //적 체력
    public List<GameObject> coin = new List<GameObject>(); //적이 죽으면 생성할 코인을 여러개 등록하기 위한 리스트

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>(); //Rigidbody2D 컴포넌트의 값을 rigidbody에 불러온다
        anim = GetComponent<Animator>(); //Animator 컴포넌트의 값을 anim에 불러온다
        Think(); //Think 함수를 호출한다

        //Invoke("Think", 2); //2초 뒤 Think함수를 호출한다 -> 적이 Think함수를 계속해서 호출하게 만든다. Update에서는 프레임마다라 너무 많이 움직인다. 이거 일단 보류 쓰지 마요
    }

    void FixedUpdate()
    {
        //이동
        rigidBody.velocity = new Vector2(nextMove, rigidBody.velocity.y); //nextMove값만큼 연속해서 움직인다.

        //낭떨어지 체크
        Vector2 frontVector = new Vector2(rigidBody.position.x + nextMove * 0.3f, rigidBody.position.y); //frontVector의 죄표값을 적 오브젝트보다 nextmove*0.3만큼 더 앞에 위치하도록 한다
        Debug.DrawRay(rigidBody.position, Vector2.down, new Color(0, 1, 0)); //레이캐스트를 초록색으로 해서 눈에 보이도록 한다.
        RaycastHit2D rayHit = Physics2D.Raycast(frontVector, Vector2.down, 1, LayerMask.GetMask("platform")); //레이어 이름이 platform인 오브젝트에 frontVector위치에서 아래방향으로 1만큼 쏘아진 레이캐스트를 rayHit으로 선언한다.
        if (rayHit.collider == null)   //null은 레이케스트가 아무것도 맞은것이 없을 경우를 지칭 
        {
            Turn(); //Turn함수를 호출한다.
        }

        // 적 공격방향 설정 및 공격체 발사
        if (transform.localScale.x == 6) //오브젝트의 스케일 값이 6이면 
        {
            Debug.DrawRay(rigidBody.position, Vector2.right * distance, new Color(1, 0, 0)); //레이캐스트를 빨간색으로 해서 눈에 보이도록 한다.
            RaycastHit2D raycastR = Physics2D.Raycast(transform.position, transform.right, distance, LayerMask.GetMask("Player")); //레이어 이름이 Player인 오브젝트에 distance만큼 오른쪽으로 쏘아진 레이캐스트를 raycastR로 선언한다.
            if (raycastR.collider != null) //레이케스트가 맞은것이 있을 경우 
            {
                if (Vector2.Distance(transform.position, raycastR.collider.transform.position) < atkDistance) //공격 사정거리보다 raycastR와 충돌한 오브젝트의 거리가 짧으면
                {
                    if (currenttime <= 0) //쿨타임이 0이면
                    {
                        Instantiate(bullet, new Vector2(transform.position.x + 0.9f, transform.position.y), transform.rotation); //오브젝트의 오른쪽으로 0.9만큼 더 간 거리에서 총알 생성
                        currenttime = cooltime; //총알 발사 기본설정값을 설정한 쿨타임 값으로 초기화 
                        anim.SetBool("Attack", true); //공격 애니메이션을 실행한다.
                    }
                }
                currenttime -= Time.deltaTime;//총알 발사 기본 설정값은 게임시간단위만큼 줄어든다. -1씩이 아닌 진짜 시계단위  
            }
            else //맞은 것이 없으면
            {
                anim.SetBool("Attack", false); //공격 애니메이션을 중지한다.
            }
        }
        else if(transform.localScale.x == -6) //오브젝트의 스케일 값이 -6이면
        {
            Debug.DrawRay(rigidBody.position, Vector2.left * distance, new Color(1, 0, 0)); //레이캐스트를 빨간색으로 해서 눈에 보이도록 한다.
            RaycastHit2D raycastL = Physics2D.Raycast(transform.position, transform.right * -1f, distance, LayerMask.GetMask("Player")); //레이어 이름이 Player인 오브젝트에 distance만큼 왼쪽으로 쏘아진 레이캐스트를 raycastR로 선언한다.
            if (raycastL.collider != null) //레이케스트가 맞은것이 있을 경우 
            {
                if (Vector2.Distance(transform.position, raycastL.collider.transform.position) < atkDistance) //공격 사정거리보다 raycastL과 충돌한 오브젝트의 거리가 짧으면
                {
                    if (currenttime <= 0) //타임이 0이면
                    {
                        Instantiate(bullet, new Vector2(transform.position.x - 0.9f, transform.position.y), transform.rotation); //오브젝트의 왼쪽으로 0.9만큼 더 간 거리에서 총알 생성
                        currenttime = cooltime; //총알 발사 기본설정값을 설정한 쿨타임 값으로 초기화 
                        anim.SetBool("Attack", true); //공격 애니메이션을 실행한다.
                    }
                }
                currenttime -= Time.deltaTime; //총알 발사 기본 설정값은 게임시간단위만큼 줄어든다. -1씩이 아닌 진짜 시계단위   
            }
            else //맞은 것이 없으면
            {
                anim.SetBool("Attack", false); //공격 애니메이션을 중지한다.
            }
        }
        if(Hp <= 0) //Hp가 0이면
        {
            StartCoroutine(Die()); //Die()함수를 호출 딜레이를 걸어둔 것이 있기에 StartCoroutine()을 사용
        }
    }

    //행동을 변경해줄 함수 -> 방향전환
    //재귀함수 -> 자기 자신을 호출
    void Think() //적 오브젝트가 자동으로 좌 우 정지 행동을 하는 함수
    {
        //다음 활동
        nextMove = Random.Range(-1, 2);//최소값은 랜덤값에 포함되나 최대값은 포함 x

        //스프라이트 애니메이션
        anim.SetInteger("WalkSpeed", nextMove);

        //스프라이트 방향전환
        if (nextMove != 0) //nextMove가 0이 아니면
        {
            if(nextMove == -1) //nextMove가 -1이면
                transform.localScale = new Vector2(-6, 6); //오브젝트의 스케일값을 -6,6으로 설정
            else if(nextMove == 1) //nextMove가 1이면
                transform.localScale = new Vector2(6, 6); //오브젝트의 스케일값을 6,6으로 설정
        }

        //재귀함수 - 자기자신을 호출해 반복행위를 하는 기능
        float nextThinkTime = Random.Range(2f, 4f); //다음 함수 호출 시간을 2~3초의 랜덤값을 nextThinkTime 대입
        Invoke("Think", nextThinkTime); // Think함수를 nextThinkTime 뒤에 호출
    }

    void Turn() //적이 낭떨어지에 가면 뒤도는 함수
    {
        nextMove *= -1; //nectMove값을 반대로 설정
        if (nextMove == -1) //nextMove가 -1이면
            transform.localScale = new Vector2(-6, 6); //오브젝트의 스케일값을 -6,6으로 설정
        else if (nextMove == 1) //nextMove가 1이면
            transform.localScale = new Vector2(6, 6); //오브젝트의 스케일값을 6,6으로 설정

        CancelInvoke(); //Think의 Invoke를 전부 취소한다.
        Invoke("Think", 3); //3초후에 다시 Think함수를 호출한다.
    }

    void DropItem() //코인 드랍 함수
    {
        GameObject Drop = coin[Random.Range(0, 3)]; //coin배열의 0~2 3가지의 랜덤값 중 하나를 Drop에 대입
        Instantiate(Drop, transform.position, transform.rotation); //Drop를 적 오브젝트의 위치에 생성
    }

    IEnumerator Die() //죽을때 행동할 함수
    {
        anim.SetBool("Die", true); //죽는 애니메이션 실행 후 
        yield return new WaitForSeconds(0.833f); //0.833초 후
        DropItem(); //코인 드랍한다
        Destroy(gameObject); //오브젝트 삭제한다
        EnemyGenerator._instance.deathcount--; //남은 적 갯수에서 1 뺀다
    }
}
