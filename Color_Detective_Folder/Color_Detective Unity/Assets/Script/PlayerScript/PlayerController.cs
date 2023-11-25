using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerController : MonoBehaviour
{
    public int Hp; //플레이어 체력
    public float MaxSpeed; //플레이어 최대 이동속도
    public float Jumppower; //플레이어 점프 파워
    public float curtime; //쿨타임
    public bool SoundCheck = true;
    int jumpcunt = 2; //점프 횟수
    Rigidbody2D rigidBody; //물리적 행동을 하기위해 Rigidbody2D 컴포넌트의 값을 받아온다
    SpriteRenderer spriteRenderer; //이미지 변화를 설정하기 위해 SpriteRenderer 컴포넌트의 값을 받아온다
    Animator anim; //적 애니메이션값을 받기 위한 애니메이터
    public GameObject bullet; //총알 오브젝트를 받아올 게임오브젝트

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>(); //Rigidbody2D 컴포넌트의 값을 rigidbody에 불러온다
        spriteRenderer = GetComponent<SpriteRenderer>(); //SpriteRenderer 컴포넌트의 값을 spriteRenderer에 불러온다
        anim = GetComponent<Animator>(); //Animator 컴포넌트의 값을 anim에 불러온다
        bullet.GetComponent<SpriteRenderer>().color = Color.white;  //bullet의 SpriteRenderer컴포넌트의 색깔 값을 흰색으로 지정한다.
    } 

    private void Update() //게임 내내
    {

        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position); //캐릭터의 월드 좌표를 뷰포트 좌표계로 변환해준다.
        viewPos.x = Mathf.Clamp01(viewPos.x); //x값을 0이상, 1이하로 제한한다.
        viewPos.y = Mathf.Clamp01(viewPos.y); //y값을 0이상, 1이하로 제한한다.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos); //다시 월드 좌표로 변환한다.
        transform.position = worldPos; //좌표를 적용한다.

        //플레이어 이동속도 감속
        if (Input.GetButtonUp("Horizontal")) //Horizontal키를 때면
        {
            //rigidbody.velocity.normalized //벡터의 크기를 1로 정의한 상태,방향과 크기를 같이 가지고 있기 때문 방향을 구할때
            rigidBody.velocity = new Vector2(rigidBody.velocity.normalized.x * 0.8f, rigidBody.velocity.y); //좌우 어느 방향이든 0.8만큼 점점 감속한다.
        }

        //플레이어 이미지 방향
        if (Input.GetButton("Horizontal")) //Horizontal키를 누르고 있는 동안 
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1; //Horizontal축 방향의 값이 -1 이면 flipX의 값을 true로 하여라 / 왼쪽이면 -1 오른쪽이면 1

        //플레이어 애니메이션 변경
        if (Mathf.Abs(rigidBody.velocity.x) < 0.5f) //플레이어 x의 속도가 0.5 이하면 
            anim.SetBool("isWalk", false); //걷는 애니메이션 비활성화
        else //0.5 이상이면
            anim.SetBool("isWalk", true); //걷는 애니메이션 실행

        //플레이어 점프 
        if (Input.GetButtonDown("Jump") && jumpcunt > 1) //Jump버튼(스페이스버튼)를 누르고, 점프카운트가 0초과일때
        {
            rigidBody.AddForce(Vector2.up * Jumppower, ForceMode2D.Impulse); //위쪽방향으로 점프파워 값 만큼 힘을 가한다
            anim.SetBool("isJump", true); //점프 애니메이션을 호출
            jumpcunt--; //점프카운트를 1감소
            SoundManager.instance.SFXPlay("Jump", SoundManager.instance.Jump);
        }

        if (Input.GetKeyDown(KeyCode.Q)) //Q키를 누르면
        {
            SoundManager.instance.SFXPlay("Attack", SoundManager.instance.Attack);
            if (spriteRenderer.flipX != true) //flipX가 false면
            {
                Instantiate(bullet, new Vector2(transform.position.x + 0.8f, transform.position.y), transform.rotation); //플레이어 오브젝트이 오른쪽으로 0.8만큼의 더 간 거리에서 총알 생성
                anim.SetBool("Attack", true); //공격 애니메이션 호출
            }
            else //flipX가 true면
            {
                Instantiate(bullet, new Vector2(transform.position.x - 0.8f, transform.position.y), transform.rotation); //플레이어 오브젝트이 오른쪽으로 -0.8만큼의 더 간 거리에서 총알 생성
                anim.SetBool("Attack", true); //공격 애니메이션 호출
            }
        }

        if (Input.GetKeyUp(KeyCode.Q)) //Q버튼을 때면
        {
            anim.SetBool("Attack", false); //공격애니메이션 중지
        }

        if (Input.GetKeyDown(KeyCode.E)) //E버튼을 누르면
        {
            bullet.GetComponent<SpriteRenderer>().color = Color.white; //총알 오브젝트의 색을 흰색으로 설정
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.white; //플레이어 오브젝트의 색을 흰색으로 설정
        }

        if (Hp <= 0 && SoundCheck == true) //Hp가 0이면
        {
            SoundManager.instance.SFXPlay("over", SoundManager.instance.GameOver);
            SoundCheck = false;
            StartCoroutine(PlayerDied()); //PlayerDied함수 호출 딜레이가 걸리는 구문이 있기에 StartCoroutine를 사용
        }

        if(GameDirector.instance.Timer == 0 && EnemyGenerator._instance.deathcount >= 0 && SoundCheck == true)
        {
            SoundManager.instance.SFXPlay("over", SoundManager.instance.GameOver);
            SoundCheck = false;
            GameDirector.instance.Gameover();
        }

        //플레이어 이동속도
        float h = Input.GetAxisRaw("Horizontal"); //Horizontal축 방향의 값을 불러와 h에 대입 왼쪽이면 -1 오른쪽이면 1

        rigidBody.AddForce(Vector2.right * h, ForceMode2D.Impulse); //오른쪽*축방향 만큼 계속해서 힘을 가한다.

        if (rigidBody.velocity.x > MaxSpeed) //오른쪽으로 최대 이동속도를 넘으면
            rigidBody.velocity = new Vector2(MaxSpeed, rigidBody.velocity.y); //최대이동 속도로 고정한다
        else if (rigidBody.velocity.x < MaxSpeed * (-1)) //왼쪽으로 최대 이동속도를 넘으면
            rigidBody.velocity = new Vector2(MaxSpeed * (-1), rigidBody.velocity.y); //최대이동 속도로 고정한다
    }

    //적 오브젝트 충돌 데미지 처리 기능
    private void OnCollisionEnter2D(Collision2D collision) //충돌을 시작했을때
    {
        if (collision.gameObject.tag == "REnemy") //태그이름이 REnemy인 오브젝트와 충돌하면
        {
            OnDamaged(collision.transform.position); //OnDamaged에 충돌 오브젝트 죄표값을 넣어서 호출
        }

        if (collision.gameObject.tag == "GEnemy") //태그이름이 GEnemy인 오브젝트와 충돌하면
        {
            OnDamaged(collision.transform.position); //OnDamaged에 충돌 오브젝트 죄표값을 넣어서 호출
        }

        if (collision.gameObject.tag == "BEnemy") //태그이름이 BEnemy인 오브젝트와 충돌하면
        {
            OnDamaged(collision.transform.position); //OnDamaged에 충돌 오브젝트 죄표값을 넣어서 호출
        }

        if (collision.gameObject.tag == "WEnemy") //태그이름이 WEnemy인 오브젝트와 충돌하면
        {
            OnDamaged(collision.transform.position); //OnDamaged에 충돌 오브젝트 죄표값을 넣어서 호출
        }

        if (collision.gameObject.tag == "RBullet") //태그이름이 REnemy인 오브젝트와 충돌하면
        {
            OnDamaged(collision.transform.position); //OnDamaged에 충돌 오브젝트 죄표값을 넣어서 호출
        }

        if (collision.gameObject.tag == "GBullet") //태그이름이 GEnemy인 오브젝트와 충돌하면
        {
            OnDamaged(collision.transform.position); //OnDamaged에 충돌 오브젝트 죄표값을 넣어서 호출
        }

        if (collision.gameObject.tag == "BBullet") //태그이름이 BEnemy인 오브젝트와 충돌하면
        {
            OnDamaged(collision.transform.position); //OnDamaged에 충돌 오브젝트 죄표값을 넣어서 호출
        } 

        if (collision.gameObject.tag == "WBullet") //태그이름이 WEnemy인 오브젝트와 충돌하면
        {
            OnDamaged(collision.transform.position); //OnDamaged에 충돌 오브젝트 죄표값을 넣어서 호출
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "platform")
        {
            anim.SetBool("isJump", false); //점프 애니메이션 중지
            jumpcunt = 2; //점프 카운트를 2로 초기화
        }
    }

    //항아리 색 변화 및 튜토리얼 골 충돌기능
    private void OnTriggerEnter2D(Collider2D collision) //트리거 충돌을 시작했을때
    {
        if (collision.gameObject.CompareTag("rPot")) //태그이름이 rPot인 오브젝트와 충돌하면
        {
            bullet.GetComponent<SpriteRenderer>().color = new Color(206/255f, 85/255f, 57/255f, 255/255f); //총알 색깔을 해당 설정값으로 바꾼다.
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(206/255f, 85/255f, 57/255f,255/255f); //플레이어 색깔을 해당 설정값으로 바꾼다.
        }
        if (collision.gameObject.tag == "bPot") //태그이름이 bPot인 오브젝트와 충돌하면
        {
            bullet.GetComponent<SpriteRenderer>().color = new Color(115/255f, 190/255f, 213/255f,255/255f); //총알 색깔을 해당 설정값으로 바꾼다.
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(115/255f, 190/255f, 213/255f,255/255f); //플레이어 색깔을 해당 설정값으로 바꾼다.
        }
        if (collision.gameObject.tag == "gPot") //태그이름이 gPot인 오브젝트와 충돌하면
        { 
            bullet.GetComponent<SpriteRenderer>().color = new Color(157/255f, 196/255f, 87/255f,255/255f); //총알 색깔을 해당 설정값으로 바꾼다.
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(157/255f, 196/255f, 87/255f,255/255f); //플레이어 색깔을 해당 설정값으로 바꾼다.
        }

        if(collision.gameObject.name == "StartGo") //태그이름이 StartGo인 오브젝트와 충돌하면
        {
            SceneManager.LoadScene("GameStage1"); //게임플레이화면으로 이동한다.
        }
    }

    //색 가져오기 기능
    private void OnTriggerStay2D(Collider2D collision) //트리거 충돌을 하는 동안
    {
        if (collision.gameObject.CompareTag("rCoin")) //태그이름이 rCoin인 오브젝트와 충돌하는 동안엔
        {
            if (Input.GetKeyDown(KeyCode.W)) //W키를 누를때
            {
                bullet.GetComponent<SpriteRenderer>().color = new Color(206/255f, 85/255f, 57/255f,255/255f); //총알 색깔을 해당 설정값으로 바꾼다.
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(206/255f, 85/255f, 57/255f,255/255f); //플레이어 색깔을 해당 설정값으로 바꾼다.
                Destroy(collision.gameObject); // 충돌 오브젝트 삭제
            }
        }
        if (collision.gameObject.tag == "bCoin") //태그이름이 bCoin인 오브젝트와 충돌하는 동안엔
        {
            if (Input.GetKeyDown(KeyCode.W)) //W키를 누를때
            {
                bullet.GetComponent<SpriteRenderer>().color = new Color(115 / 255f, 190 / 255f, 213 / 255f,255/255f); //총알 색깔을 해당 설정값으로 바꾼다.
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(115 / 255f, 190 / 255f, 213 / 255f,255 / 255f); //플레이어 색깔을 해당 설정값으로 바꾼다.
                Destroy(collision.gameObject); // 충돌 오브젝트 삭제
            }
        }
        if (collision.gameObject.tag == "gCoin") //태그이름이 gCoin인 오브젝트와 충돌하는 동안엔
        {
            if (Input.GetKeyDown(KeyCode.W)) //W키를 누를때
            {
                bullet.GetComponent<SpriteRenderer>().color = new Color(157 / 255f, 196 / 255f, 87 / 255f,255 / 255f); //총알 색깔을 해당 설정값으로 바꾼다.
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(157 / 255f, 196 / 255f, 87 / 255f,255 / 255f); //플레이어 색깔을 해당 설정값으로 바꾼다.
                Destroy(collision.gameObject); //충돌 오브젝트 삭제
            }
        }
    }

    //데미지 받을 시 무적시간 생성
    void OnDamaged(Vector2 targetPos) //지정된 위치에서 데미지를 받는 함수
    {
        //레이어 전환
        gameObject.layer = 9; //게임오브젝트의 레이어 번호를 9으로 설정한다.

        //흐릿해지기
        spriteRenderer.color = new Color(1, 1, 1, 0.4f); //색깔 선명도를 0.4로 설정
        bullet.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f); //총알 색깔 선명도를 0.4로 설정

        //맞고 충격 반동 
        int dirct = transform.position.x - targetPos.x > 0 ? 1 : -1; //플레이어와 충돌 오브젝트 위치의 차가 0보다 크면 1 작으면 -1을 dirct에 대입
        rigidBody.AddForce(new Vector2(dirct, 1) * 7, ForceMode2D.Impulse); //dirct*7방향만큼의 힘을 가한다

        Invoke("OffDamaged", 2f); //2초후에 OffDamaged함수 호출

        //체력 감소
        Hp--; //Hp를 1만큼 줄인다
        if (GameDirector.instance.lifeImg.Count > Hp) //GameDirector의 UI이미지 개수가 HP보다 많을때
        {
            GameDirector.instance.lifeImg[GameDirector.instance.lifeImg.Count - 1].SetActive(false); //UI이미지 개수를 줄이고 그 번호의 UI를 비활성화 시킨다.
            GameDirector.instance.lifeImg.Remove(GameDirector.instance.lifeImg[GameDirector.instance.lifeImg.Count - 1]); //비활성화 된 오브젝트를 삭제시킨다.
        }


    }

    void OffDamaged() //데미지 받는 효과 끄기
    {
        gameObject.layer = 6; //게임오브젝트의 레이어 번호를 6으로 설정한다.

        spriteRenderer.color = new Color(1, 1, 1, 1); //색깔 선명도를 1로 설정
        bullet.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1); //총알 색깔 선명도를 1로 설정
    }

    IEnumerator PlayerDied() //플레이어 죽음 함수
    {
        anim.SetBool("isDie", true); //죽는 애니메이션 실행
        yield return new WaitForSeconds(0.4f); //0.4초 후에
        gameObject.SetActive(false); //플레이어 오브젝트 비활성화 -> CameraController에서 좌표값을 계속 받아와야해서 Destroy는 불가능 
        GameDirector.instance.Gameover(); //게임오버함수를 호출
    }

}
