using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour //카메라가 플레이어를 따라다니게 하는 스크립트
{
    [SerializeField]
    Transform playerTransform; //Transform 오브젝트 생성 - 이 오브젝트칸에 넣은 오브젝트의 위치값을 불러온다.
    [SerializeField]
    Vector3 cameraPosition; //이 스크립트가 들어간 오브젝트의 transform값을 조정한다.

    [SerializeField]
    Vector2 center; //카메라의 센터값을 조정한다
    [SerializeField]
    Vector2 mapSize; //카메라의 크기를 정할 맵 사이즈를 조정한다

    [SerializeField]
    float cameraMoveSpeed; //카메라가 플레이어를 따라가는 속도
    float height; //세로길이
    float width; //가로길이
     
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>(); //playerTransform은 플레이어 오브젝트의 좌표값을 불러온다.

        height = Camera.main.orthographicSize; //세로길이는 카메라의 세로 사이즈를 자동으로 받아와 호출한다.
        width = height * Screen.width / Screen.height; //가로길이는 옆의 계산법을 이용하여 호출한다.
    }

    void FixedUpdate() 
    {
        LimitCameraArea(); //이 함수를 반복 호출한다
    }

    void LimitCameraArea() //카메라 행동반경 범위를 지정해주는 함수
    {
        transform.position = Vector3.Lerp(transform.position,
                                          playerTransform.position + cameraPosition,
                                          Time.deltaTime * cameraMoveSpeed); // 이 카메라의 위치가 플레이어 위치를 Time.deltaTime * cameraMoveSpeed의 속도로 뒤따라가는 기능
        float lx = mapSize.x - width; //맵 가로 사이즈에서 카메라 가로 사이즈를 뺀 길이
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x); //카메라가 지정한 맵 가로 사이즈를 벗어나지 않게 하는 길이

        float ly = mapSize.y - height; //맵 세로 사이즈에서 카메라 세로 사이즈를 뺀 길이
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y); //카메라가 지정한 맵 세로 사이즈를 벗어나지 않게 하는 길이

        transform.position = new Vector3(clampX, clampY, -10f); //카메라 위치값은 이 지도 범위를 한계로 둔다
    }

    private void OnDrawGizmos() //이 맵사이즈를 지정한것을 눈에 보이게 하기위한 효과를 주는 함수
    {
        Gizmos.color = Color.red; //빨간색으로
        Gizmos.DrawWireCube(center, mapSize * 2); //맵사이즈에 맞게 사각형으로 그린다.
    }
}
