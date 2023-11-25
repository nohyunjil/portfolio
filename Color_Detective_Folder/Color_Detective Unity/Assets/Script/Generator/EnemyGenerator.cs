using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //�� �۾��� ���� �־�� ����


public class EnemyGenerator : MonoBehaviour
{
    public int maxCount; //�ִ� ����
    public int enemyCount; //�� ����
    public int deathcount; //���� �� ����
    public float spawnTime; //���� ��Ÿ��
    public float curTime; //��Ÿ�� üũ ����
    public bool soundCheck = true;

    public Transform[] spawnPoints; //���� ��ġ�� �迭�� ���� - �������� ��ġ�� �ѹ��� ����ϱ� ���ؼ� 
    public List<GameObject> enemy = new List<GameObject>(); //������ ������ ����Ʈ�� ���� - ���� ���� �ѹ��� ����ϱ� ���ؼ�
    public static EnemyGenerator _instance; //EnemyGenerator��ũ��Ʈ�� �Լ����� �ҷ��ö� �������� instance


    private void Start()
    {
        _instance = this; //_instance�� EnemyGenerator��ũ��Ʈ��� ����
    }

    private void Update()
    {
        if(curTime >= spawnTime && enemyCount < maxCount) //��Ÿ���� ���Ұ� ���� �ִ������������ ������
        {
            int x = Random.Range(0, spawnPoints.Length); //������ġ�迭�� 0���� ������ �迭 ũ���� ������ȣ�� int x�� �޾ƿ´� 
            SpawnEnem(x); //������ȣ�� ���� SpawnEnem�� ȣ��
        }
        curTime += Time.deltaTime; //��Ÿ��üũ ���ڴ� ���ӽð������� ������. +1���� �ƴ� ��¥ �ð����  

        GameDirector.instance.EnemyCount(maxCount,deathcount); //�ִ� ������ ���� �� ���� GameDirector�� EenmyCount�Լ��� �ҷ��´�.

        if (GameDirector.instance.Timer >=0 && deathcount == 0 && soundCheck == true) //���� ���� 0������
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

    public void SpawnEnem(int ranNum) //���� ��ȣ�� �޾ƿ� ȣ���ϴ� �Լ�
    {
        curTime = 0; //��Ÿ�� üũ ���ڸ� 0���� �ʱ�ȭ
        enemyCount++; //�� ���� +1
        GameObject sEnemy = enemy[Random.Range(0, 4)]; //sEnemy�� ����Ʈ 0~3���� ������Ʈ �� �������� �ҷ��´� / 0,4�� �� ������ 4-1�� ������ ���������̱� ������
        Instantiate(sEnemy, spawnPoints[ranNum]); //���� �� ������Ʈ�� ���� ��ġ�� ���� ��ȣ������ ����
    }

    
}
