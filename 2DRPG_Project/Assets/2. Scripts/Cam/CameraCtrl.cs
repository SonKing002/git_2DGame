using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{
    //카메라 이동 제한 캐릭터 따라가기 : 학원 수업 내용참고
    public class CameraCtrl : MonoBehaviour
    {
        //타 오브젝트의 스크립트
        Transform player; // 플레이어 가져오기
        Transform cam;    //카메라 가져오기

        //변수
        Vector3 targetPosition; //목표 위치
        public float camSpeed; //카메라 속도 조절

        //에디터용 기즈모
        public Vector2 areaCenter, areaSize; //카메라 제한 영역의 중심점, 크기
        float camera_HalfWeight, camera_HalfHeight; //카메라 가로 절반, 세로 절반
        float limitX, limitY; //가로 세로 움직일 수 있는 한계거리
        float clampX, clampY; // 계산된 제한 거리

        void Start()
        {
            //find 할당
            player = FindObjectOfType<PlayerMove>().transform;

            //카메라 할당
            cam = Camera.main.transform;

            camera_HalfHeight = Camera.main.orthographicSize; //카메라 세로의 절반
            camera_HalfWeight = (float)( Screen.width / Screen.height ) * camera_HalfHeight; //카메라 가로의 절반
        }

        //기즈모를 그리는 함수
        private void OnDrawGizmos()
        {
            //붉은 색 기즈모
            Gizmos.color = Color.red;

            //카메라 제한 영역 그리기
            Gizmos.DrawWireCube(areaCenter, areaSize);
        }

        //update함수보다 나중 순서로 호출
        void LateUpdate()
        {
         
            //카메라의 위치 y +3f 보정치
            targetPosition = new Vector3(player.position.x, player.position.y + 2f, cam.position.z); //플레이어의 x,y 값을 대입

            //fallowing cam 속도를 선형보간 대입
            cam.position = Vector3.Lerp(cam.position, targetPosition, Time.deltaTime * camSpeed);

            //center와 카메라 둘 길이의 차이
            limitX = areaSize.x * 0.5f - camera_HalfHeight; //(0 ~ areaSize.x = 지름) / 2 = 절반  - 카메라의 절반거리 = 거리 차이
            limitY = areaSize.y * 0.5f - camera_HalfWeight;

            //범위 제한 areaCenter로부터 limit까지 움직일 수 있다.
            clampX = Mathf.Clamp(cam.position.x, areaCenter.x - limitX, areaCenter.x + limitX);
            clampY = Mathf.Clamp(cam.position.y, areaCenter.y - limitY, areaCenter.y + limitY);

            cam.position = new Vector3(clampX, clampY, cam.position.z);
            
        }
    }

}