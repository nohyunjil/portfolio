using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour //Dissolve��ũ��Ʈȿ���� �ҷ����� ���� ��ũ��Ʈ
{
    private static ScreenManager m_instance; //�⺻ �ν��Ͻ���

    public static ScreenManager _instance // �߰� �ν��Ͻ� ��
    {
        get
        {
            return m_instance; //�⺻������ �޴´�
        }
        set
        {
            m_instance = value; //������ value�� �޴´�
        }
    }

    private void Awake()
    {
        if(_instance != null) //�ΰ� �̻��� �ν��Ͻ��� ���ÿ� ������ �� _instance�� �׳� �ı��ع�����.
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this; //�ΰ��̻��� �ƴ϶� �ϳ��� �� ���� _instance�� �д�.
        }

        DontDestroyOnLoad(gameObject); //�� ��ũ��Ʈ�� �� ������Ʈ�� �ı����� �ʴ´�.
    }

    public Texture2D ScreenTexture; //ȭ���� �ؽ���ȭ�� ���� �ޱ����� Texture2D���� ���� ScreenTexture�� �����Ѵ�.
    
    IEnumerator CaptureScreen() // Ienumerator�� �����̸� �� �� �ִ� CaptureScreen()�Լ� ����
    {
        //�ؽ��� �ڷ��� ������ ����
        Texture2D texture = new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);
        yield return new WaitForEndOfFrame(); //�������� ���� �Ŀ� ���� ��ɵ��� �����Ų��.
        //ȭ���� �ȼ� �����͸� �о �ؽ���ȭ �ϴ� ����
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
        texture.Apply();
        ScreenTexture = texture;
    }

    public void _LoadScreenTexture() //�ؽ���ȭ�� ���� �ҷ��´�
    {
        StartCoroutine(CaptureScreen()); //�����̸� ������ �Լ��� �����ϱ����� StartCoroutine()����� ����ؼ� ȣ���Ѵ�.
    }
}
