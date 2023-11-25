using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //씬 작업을 위해 넣어둔 내용
using UnityEngine.UI; //UI작업을 위해서 넣어야 하는 값 UI에서 생성한 오브젝트의 작업을 할 때 사용

public class GameDirector : MonoBehaviour //게임의 UI,씬전환등 모든 외부작업을 맡은 스크립트
{
    public static GameDirector instance; //GameDirector스크립트의 함수들을 호출하기 위한 instance생성
    public Animator anim; //플레이화면과 메인화면 애니메이션값을 받기 위한 애니메이터
    public Animator Nextanim; //플레이화면과 메인화면 애니메이션값을 받기 위한 애니메이터
    public Animator Clearanim; //클리어화면 애니메이션값을 받기 위한 애니메이터
    public Animator Pauseanim; //정지화면 애니메이션값을 받기 위한 애니메이터
    public List<GameObject> lifeImg; //HpUI로 게임 속 하트 그림이 데미지 입을때마다 지워지게 하기위한 리스트 생성

    private bool isPause = false; //게임정지를 인식하기위한 bool값 false로 기본 설정

    public Text EnemyCounttxt; //UI로 게임화면에 남은 적 수를 표시하기위한 텍스트 생성

    public float Timer;
    public Text Time_text;

    public void Awake() //대충 넘깁시다
    {
        if (instance == null) 
        {
            instance = this; 
        }
        else
        {
            if (instance != this) 
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 5) //씬 번호가 3번이면 
        {
            Invoke("ClearUI", 2); //2초후에 ClearUI함수를 호출한다
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //esc버튼을 누르면
        {
            if (!isPause) //isPause가 false면
            {
                SetPause(); //SetPause()함수를 호출
                Invoke("TimeStop", 0.65f); //0.65초 뒤에 TimeStop함수를 호출
            }
            else if (isPause) //isPause가 true면
            {
                Time.timeScale = 1; //게임 시간을 1로 설정 = 게임이 움직인다
                Continue(); //Continue()함수를 호출
            }
        }

        Timer -= Time.deltaTime;

        Time_text.text = Mathf.Round(Timer).ToString();

        if(Timer <= 0)
        {
            Timer = 0;
            Time_text.text = 0.ToString();
        }
    }

    public void SetPause() //PauseUI 띄우기
    {
        isPause = true; //isPause를 true로 바꾼다
        Pauseanim.SetBool("isDown", true); //Pause화면을 띄운다.
    }

    public void Continue() //게임 정지해제 후 PauseUI 숨기기 
    {
        isPause = false; //isPause를 false로 바꾼다
        Pauseanim.SetBool("isDown", false);//Pause화면을 숨기기.
    }

    public void TimeStop() //게임 정지 함수
    {
        Time.timeScale = 0; //게임을 정지 시킨다.
    }

    public void Restart() // 게임 플레이 화면으로 이동한다.
    {
        Time.timeScale = 1; //게임 시간을 1로 설정 = 게임이 움직인다
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene("GameStage1"); //GameScene = 플레이화면 으로 이동한다.
        }
        else if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadScene("GameStage1"); //GameScene = 플레이화면 으로 이동한다.
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            SceneManager.LoadScene("GameStage2"); //GameScene = 플레이화면 으로 이동한다.
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            SceneManager.LoadScene("GameStage3"); //GameScene = 플레이화면 으로 이동한다.
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            SceneManager.LoadScene("GameStage1"); //GameScene = 플레이화면 으로 이동한다.
        }
    }

    public void NextSTAGE() // 게임 플레이 화면으로 이동한다.
    {
        Time.timeScale = 1; //게임 시간을 1로 설정 = 게임이 움직인다
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void TutorialScene() //게임 튜토리얼 화면으로 이동한다.
    {
        Time.timeScale = 1; //게임 시간을 1로 설정 = 게임이 움직인다
        SceneManager.LoadScene("Tutorial"); //Tutorial = 튜토리얼화면 으로 이동한다.
    }

    public void StartMenu() //메인화면으로 이동
    {
        Time.timeScale = 1; //게임 시간을 1로 설정 = 게임이 움직인다
        SceneManager.LoadScene("StartMenu"); //StartMenu = 메인화면 으로 이동한다.
    }

    public void Gameover() //게임오버 호출 함수 
    {
        anim.SetBool("isShow", true); //게임오버 UI를 호출한다.
        Invoke("TimeStop", 1f);
    }

    public void NextStageAnim()
    {
        Nextanim.SetBool("isShow", true);
        Invoke("TimeStop", 1f);
    }

    public void Gameclear() //게임 클리어 씬으로 이동한다
    {
        ScreenManager._instance._LoadScreenTexture(); //흐릿해졌다가 화면 이동하는 Dissolve스크립트와 ScreenManager스크립트를 이용한 기능 호출
        SceneManager.LoadScene("GameClearScene"); //GameClearScene = 클리어화면 으로 이동한다.
    }

    public void ClearUI() //게임 클리어 시 클리어UI를 생성
    {
        Clearanim.SetBool("isClearShow", true); //클리어UI 애니메이션 호출
    }

    public void EnemyCount(int max, int count) //UI화면에 남은 적 수를 보여주는 함수
    {
        EnemyCounttxt.text = max.ToString() + " / " + count.ToString(); //EnemyCounttxt.text에 최대 적 생성 개수 / 남은적값을 보여준다 count는 다른 스크립트의 int 값을 받기 위해 넣어둔 매개변수
    }
}
