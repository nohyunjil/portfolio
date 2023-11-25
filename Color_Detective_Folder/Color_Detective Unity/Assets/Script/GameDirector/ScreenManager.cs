using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour //Dissolve스크립트효과를 불러오기 위한 스크립트
{
    private static ScreenManager m_instance; //기본 인스턴스값

    public static ScreenManager _instance // 추가 인스턴스 값
    {
        get
        {
            return m_instance; //기본값으로 받는다
        }
        set
        {
            m_instance = value; //씬값을 value로 받는다
        }
    }

    private void Awake()
    {
        if(_instance != null) //두개 이상의 인스턴스가 동시에 존재할 시 _instance는 그냥 파괴해버린다.
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this; //두개이상이 아니라 하나면 그 씬을 _instance로 둔다.
        }

        DontDestroyOnLoad(gameObject); //이 스크립트가 들어간 오브젝트는 파괴하지 않는다.
    }

    public Texture2D ScreenTexture; //화면을 텍스쳐화한 값을 받기위해 Texture2D값을 가진 ScreenTexture를 생성한다.
    
    IEnumerator CaptureScreen() // Ienumerator로 딜레이를 걸 수 있는 CaptureScreen()함수 생성
    {
        //텍스쳐 자료형 변수를 생성
        Texture2D texture = new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);
        yield return new WaitForEndOfFrame(); //프레임이 끝난 후에 밑의 기능들을 실행시킨다.
        //화면의 픽셀 데이터를 읽어서 텍스쳐화 하는 과정
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
        texture.Apply();
        ScreenTexture = texture;
    }

    public void _LoadScreenTexture() //텍스쳐화한 씬을 불러온다
    {
        StartCoroutine(CaptureScreen()); //딜레이를 포함한 함수를 실행하기위해 StartCoroutine()기능을 사용해서 호출한다.
    }
}
