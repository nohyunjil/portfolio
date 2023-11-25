using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI�۾��� ���ؼ� �־�� �ϴ� �� UI���� ������ ������Ʈ�� �۾��� �� �� ���

public class Dissolve : MonoBehaviour //ȭ�� �� ��ȯ�� �帴�����ٰ� ���� ������ �ٲ�� ȿ���� �ִ� ��ũ��Ʈ
{
    private RawImage _ri; //RawImage�� �ؽ��� ��� �̹����� ���� �� ��ȯ �ڵ尡 �ؽ��� �������� �ҷ��ͼ� ó���ϱ� ������ RawImage�� ���

    private float ChangeSpeed = 2f; //�ӵ����� 2�� ����

    private void Start()
    {
        _ri = GetComponent<RawImage>(); //RawImage������Ʈ���� ���� �޾ƿ´�
        _ri.texture = ScreenManager._instance.ScreenTexture; //ȭ���� ��ȯ�Ҷ� ������ �ؽ��� ���� ����

        if(_ri.texture == null)//���࿡_ri�� �ؽ��İ��� �޾ƿ��� �� �ϰ� �Ǹ� 
        {
            gameObject.SetActive(false); //��Ȱ��ȭ ��Ų��.
        }

        _ri.color = Color.white; //���� ������� �����Ѵ�.
    }

    private void Update()
    {
        _ri.color = Color.Lerp(_ri.color, new Color(1, 1, 1, 0),ChangeSpeed * Time.deltaTime ); //lerp - ��������, ���� Color(1,1,1,0)���� �ӵ�*�ð� ��ŭ�� �ӵ��� ��ȭ��Ų��.
        if(_ri.color.a <= 0.01f) //�� ���İ�(ä��)�� 0�� ��������� ���ش� 
        {
            gameObject.SetActive(false); //�� ��ũ��Ʈ�� �� ������Ʈ�� ����� �����.
        }
    }
}
