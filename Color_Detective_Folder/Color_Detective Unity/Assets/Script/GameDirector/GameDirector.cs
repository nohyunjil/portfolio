using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //�� �۾��� ���� �־�� ����
using UnityEngine.UI; //UI�۾��� ���ؼ� �־�� �ϴ� �� UI���� ������ ������Ʈ�� �۾��� �� �� ���

public class GameDirector : MonoBehaviour //������ UI,����ȯ�� ��� �ܺ��۾��� ���� ��ũ��Ʈ
{
    public static GameDirector instance; //GameDirector��ũ��Ʈ�� �Լ����� ȣ���ϱ� ���� instance����
    public Animator anim; //�÷���ȭ��� ����ȭ�� �ִϸ��̼ǰ��� �ޱ� ���� �ִϸ�����
    public Animator Nextanim; //�÷���ȭ��� ����ȭ�� �ִϸ��̼ǰ��� �ޱ� ���� �ִϸ�����
    public Animator Clearanim; //Ŭ����ȭ�� �ִϸ��̼ǰ��� �ޱ� ���� �ִϸ�����
    public Animator Pauseanim; //����ȭ�� �ִϸ��̼ǰ��� �ޱ� ���� �ִϸ�����
    public List<GameObject> lifeImg; //HpUI�� ���� �� ��Ʈ �׸��� ������ ���������� �������� �ϱ����� ����Ʈ ����

    private bool isPause = false; //���������� �ν��ϱ����� bool�� false�� �⺻ ����

    public Text EnemyCounttxt; //UI�� ����ȭ�鿡 ���� �� ���� ǥ���ϱ����� �ؽ�Ʈ ����

    public float Timer;
    public Text Time_text;

    public void Awake() //���� �ѱ�ô�
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
        if (SceneManager.GetActiveScene().buildIndex == 5) //�� ��ȣ�� 3���̸� 
        {
            Invoke("ClearUI", 2); //2���Ŀ� ClearUI�Լ��� ȣ���Ѵ�
        }

        if (Time_text == null)
        {
            Time_text = GameObject.Find("TimeText").GetComponent<Text>(); // 또는 TMP_Text
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //esc��ư�� ������
        {
            if (!isPause) //isPause�� false��
            {
                SetPause(); //SetPause()�Լ��� ȣ��
                Invoke("TimeStop", 0.65f); //0.65�� �ڿ� TimeStop�Լ��� ȣ��
            }
            else if (isPause) //isPause�� true��
            {
                Time.timeScale = 1; //���� �ð��� 1�� ���� = ������ �����δ�
                Continue(); //Continue()�Լ��� ȣ��
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

    public void SetPause() //PauseUI ����
    {
        isPause = true; //isPause�� true�� �ٲ۴�
        Pauseanim.SetBool("isDown", true); //Pauseȭ���� ����.
    }

    public void Continue() //���� �������� �� PauseUI ����� 
    {
        isPause = false; //isPause�� false�� �ٲ۴�
        Pauseanim.SetBool("isDown", false);//Pauseȭ���� �����.
    }

    public void TimeStop() //���� ���� �Լ�
    {
        Time.timeScale = 0; //������ ���� ��Ų��.
    }

    public void Restart() // ���� �÷��� ȭ������ �̵��Ѵ�.
    {
        Time.timeScale = 1; //���� �ð��� 1�� ���� = ������ �����δ�
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene("GameStage1"); //GameScene = �÷���ȭ�� ���� �̵��Ѵ�.
        }
        else if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadScene("GameStage1"); //GameScene = �÷���ȭ�� ���� �̵��Ѵ�.
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            SceneManager.LoadScene("GameStage2"); //GameScene = �÷���ȭ�� ���� �̵��Ѵ�.
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            SceneManager.LoadScene("GameStage3"); //GameScene = �÷���ȭ�� ���� �̵��Ѵ�.
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            SceneManager.LoadScene("GameStage1"); //GameScene = �÷���ȭ�� ���� �̵��Ѵ�.
        }
    }

    public void NextSTAGE() // ���� �÷��� ȭ������ �̵��Ѵ�.
    {
        Time.timeScale = 1; //���� �ð��� 1�� ���� = ������ �����δ�
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void TutorialScene() //���� Ʃ�丮�� ȭ������ �̵��Ѵ�.
    {
        Time.timeScale = 1; //���� �ð��� 1�� ���� = ������ �����δ�
        SceneManager.LoadScene("Tutorial"); //Tutorial = Ʃ�丮��ȭ�� ���� �̵��Ѵ�.
    }

    public void StartMenu() //����ȭ������ �̵�
    {
        Time.timeScale = 1; //���� �ð��� 1�� ���� = ������ �����δ�
        SceneManager.LoadScene("StartMenu"); //StartMenu = ����ȭ�� ���� �̵��Ѵ�.
    }

    public void Gameover() //���ӿ��� ȣ�� �Լ� 
    {
        anim.SetBool("isShow", true); //���ӿ��� UI�� ȣ���Ѵ�.
        Invoke("TimeStop", 1f);
    }

    public void NextStageAnim()
    {
        Nextanim.SetBool("isShow", true);
        Invoke("TimeStop", 1f);
    }

    public void Gameclear() //���� Ŭ���� ������ �̵��Ѵ�
    {
        ScreenManager._instance._LoadScreenTexture(); //�帴�����ٰ� ȭ�� �̵��ϴ� Dissolve��ũ��Ʈ�� ScreenManager��ũ��Ʈ�� �̿��� ��� ȣ��
        SceneManager.LoadScene("GameClearScene"); //GameClearScene = Ŭ����ȭ�� ���� �̵��Ѵ�.
    }

    public void ClearUI() //���� Ŭ���� �� Ŭ����UI�� ����
    {
        Clearanim.SetBool("isClearShow", true); //Ŭ����UI �ִϸ��̼� ȣ��
    }

    public void EnemyCount(int max, int count) //UIȭ�鿡 ���� �� ���� �����ִ� �Լ�
    {
        EnemyCounttxt.text = max.ToString() + " / " + count.ToString(); //EnemyCounttxt.text�� �ִ� �� ���� ���� / ���������� �����ش� count�� �ٸ� ��ũ��Ʈ�� int ���� �ޱ� ���� �־�� �Ű�����
    }
}
