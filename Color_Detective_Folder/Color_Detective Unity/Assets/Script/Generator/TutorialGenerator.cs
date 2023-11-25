using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGenerator : MonoBehaviour
{
    public int maxCount; //최대 개수
    public int spawnCount; //스폰아이템 개수
    public float spawnTime; //생성 쿨타임
    public float curTime; //쿨타임 체크 숫자
    public GameObject prefabToSpawn; //생성할 아이템을 넣을 프리팹
    public Transform spawnPoints; //아이템을 생성할 위치 
    private void Update()
    {
        if (curTime >= spawnTime && spawnCount < maxCount) //쿨타임이 돌았고 생성아이템이 최대 개수보다 적을때
        {
            SpawnEnem(); //생성함수 호출
        }
        curTime += Time.deltaTime; //쿨타임체크 숫자는 게임시간단위로 오른다. +1씩이 아닌 진짜 시계단위  
    }

    public void SpawnEnem() //아이템 호출 함수
    {
        curTime = 0; //쿨타임 체크함수를 0으로 초기화
        spawnCount++; //스폰 아이템 개수에 +1한다
        Instantiate(prefabToSpawn, spawnPoints); //지정 위치에 지정 아이템을 생성한다.
    }
}
