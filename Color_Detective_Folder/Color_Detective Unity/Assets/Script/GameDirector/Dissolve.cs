using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI작업을 위해서 넣어야 하는 값 UI에서 생성한 오브젝트의 작업을 할 때 사용

public class Dissolve : MonoBehaviour //화면 씬 전환시 흐릿해졌다가 다음 씬으로 바뀌는 효과를 주는 스크립트
{
    private RawImage _ri; //RawImage는 텍스쳐 기반 이미지로 현재 씬 전환 코드가 텍스쳐 형식으로 불러와서 처리하기 때문에 RawImage를 사용

    private float ChangeSpeed = 2f; //속도값을 2로 지정

    private void Start()
    {
        _ri = GetComponent<RawImage>(); //RawImage컴포넌트에서 값을 받아온다
        _ri.texture = ScreenManager._instance.ScreenTexture; //화면을 전환할때 나오는 텍스쳐 값을 대입

        if(_ri.texture == null)//만약에_ri가 텍스쳐값을 받아오지 못 하게 되면 
        {
            gameObject.SetActive(false); //비활성화 시킨다.
        }

        _ri.color = Color.white; //색을 흰색으로 설정한다.
    }

    private void Update()
    {
        _ri.color = Color.Lerp(_ri.color, new Color(1, 1, 1, 0),ChangeSpeed * Time.deltaTime ); //lerp - 선형보간, 색을 Color(1,1,1,0)으로 속도*시간 만큼의 속도로 변화시킨다.
        if(_ri.color.a <= 0.01f) //색 알파값(채도)가 0에 가까워지면 없앤다 
        {
            gameObject.SetActive(false); //이 스크립트가 들어간 오브젝트의 기능을 숨긴다.
        }
    }
}
