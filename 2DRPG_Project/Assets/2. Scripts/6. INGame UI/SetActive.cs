using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{
    //오브젝트 풀을 사용할 때마다, 오브젝트 활성화 제어할때 스크립트
    public class SetActive : MonoBehaviour
    {
        //
        bool isSwitchON;

        //호출 함수
        public void Make_SetActive_On()
        {
            gameObject.SetActive(true);
        }

        //애니메이션 끝에서 붙여넣을 호출 함수
        public void Make_SetActive_Off()
        {
            gameObject.SetActive(false);
        }

        //터치 반복시 함수
        public void Switch()
        {
            //제어
            isSwitchON = !isSwitchON;

            //true일때
            if (isSwitchON)
            {
                //활성화
                gameObject.SetActive(true);
            }
            //false일때
            else
            {
                //비활성화
                gameObject.SetActive(false);
            }
        }

    }//class
}//namespace
