using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGenerator : MonoBehaviour
{
    public int maxCount; //�ִ� ����
    public int spawnCount; //���������� ����
    public float spawnTime; //���� ��Ÿ��
    public float curTime; //��Ÿ�� üũ ����
    public GameObject prefabToSpawn; //������ �������� ���� ������
    public Transform spawnPoints; //�������� ������ ��ġ 
    private void Update()
    {
        if (curTime >= spawnTime && spawnCount < maxCount) //��Ÿ���� ���Ұ� ������������ �ִ� �������� ������
        {
            SpawnEnem(); //�����Լ� ȣ��
        }
        curTime += Time.deltaTime; //��Ÿ��üũ ���ڴ� ���ӽð������� ������. +1���� �ƴ� ��¥ �ð����  
    }

    public void SpawnEnem() //������ ȣ�� �Լ�
    {
        curTime = 0; //��Ÿ�� üũ�Լ��� 0���� �ʱ�ȭ
        spawnCount++; //���� ������ ������ +1�Ѵ�
        Instantiate(prefabToSpawn, spawnPoints); //���� ��ġ�� ���� �������� �����Ѵ�.
    }
}
