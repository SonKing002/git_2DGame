using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{
    //상황에 따라 카메라 흔들림 연출
    //일시적 참고.https://chameleonstudio.tistory.com/55
    //주기적 참고.https://www.youtube.com/watch?v=FoCJG0d0Iu8
    public class ShakeCam : MonoBehaviour
    {
        UIBookCtrl uiCtrl;    //isPopUp으로 정지할때, 움직이는현상.. 카메라 흔들림 역할이 멈추도록 하기 위함
        //진동세기 
        float shakeAmount = 0.05f;
        
        //저장 변수
        Vector3 initPosition; //카메라 위치
        Vector3 addShake; //흔들기

        //진동시간 
        float shakeTime = 0.2f;
        
        void Start()
        {
            uiCtrl = FindObjectOfType<UIBookCtrl>();
        }

        //다른 곳에서 일시적조건이 맞으면 함수를 사용한다.
        public void ViberateForOneTime(float shakeScale, float shakeTimer)
        {
            shakeTime = shakeTimer;
            shakeAmount = shakeScale;
        }

        void Ctrl()
        {
            //카메라가 이동하므로
            initPosition = Camera.main.transform.position;
            addShake.x = Random.value * shakeAmount * 2 - shakeAmount;
            addShake.y = Random.value * shakeAmount * 2 - shakeAmount;

            initPosition.x += addShake.x;
            initPosition.y += addShake.y;

            if (shakeTime > 0f)
            {
                //시간 차감만큼
                shakeTime -= Time.deltaTime;

                //카메라에 누적대입
                Camera.main.transform.position = initPosition;

            }
            else
            {
                //끝
                shakeTime = 0;

            }
        }

        void Update()
        {
            //책자를 닫은 상태에서만 움직인다
            if (!uiCtrl.isPopUp)
            {
                Ctrl();
            }
        }//update
    }//class
}//namespace