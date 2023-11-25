using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //씬 작업을 위해 넣어둔 내용


public class EnemyGenerator : MonoBehaviour
{
    public int maxCount; //최대 개수
    public int enemyCount; //적 개수
    public int deathcount; //남은 적 개수
    public float spawnTime; //생성 쿨타임
    public float curTime; //쿨타임 체크 숫자
    public bool soundCheck = true;

    public Transform[] spawnPoints; //생성 위치를 배열로 생성 - 여러개의 위치를 한번에 등록하기 위해서 
    public List<GameObject> enemy = new List<GameObject>(); //생성할 적들을 리스트로 생성 - 여러 적을 한번에 등록하기 위해서
    public static EnemyGenerator _instance; //EnemyGenerator스크립트의 함수값을 불러올때 쓰기위한 instance


    private void Start()
    {
        _instance = this; //_instance는 EnemyGenerator스크립트라고 선언
    }

    private void Update()
    {
        if(curTime >= spawnTime && enemyCount < maxCount) //쿨타임이 돌았고 적이 최대생성갯수보다 적을때
        {
            int x = Random.Range(0, spawnPoints.Length); //생성위치배열의 0에서 생성한 배열 크기의 랜덤번호를 int x로 받아온다 
            SpawnEnem(x); //랜덤번호를 넣은 SpawnEnem를 호출
        }
        curTime += Time.deltaTime; //쿨타임체크 숫자는 게임시간단위로 오른다. +1씩이 아닌 진짜 시계단위  

        GameDirector.instance.EnemyCount(maxCount,deathcount); //최대 개수와 남은 적 수를 GameDirector의 EenmyCount함수로 불러온다.

        if (GameDirector.instance.Timer >=0 && deathcount == 0 && soundCheck == true) //남은 적이 0마리면
        {
            if(SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 3) { 
            SoundManager.instance.SFXPlay("clear", SoundManager.instance.GameClear);
            soundCheck = false;
            GameDirector.instance.NextStageAnim();
            }

            else if(SceneManager.GetActiveScene().buildIndex == 4 )
            {
                GameDirector.instance.Gameclear();
            }
        }
    }

    public void SpawnEnem(int ranNum) //랜덤 번호를 받아와 호출하는 함수
    {
        curTime = 0; //쿨타임 체크 숫자를 0으로 초기화
        enemyCount++; //적 개수 +1
        GameObject sEnemy = enemy[Random.Range(0, 4)]; //sEnemy에 리스트 0~3번의 오브젝트 중 랜덤값을 불러온다 / 0,4를 한 이유는 4-1한 값까지 랜덤범위이기 때문에
        Instantiate(sEnemy, spawnPoints[ranNum]); //랜덤 적 오브젝트를 생성 위치의 랜덤 번호값에서 생성
    }

    
}
