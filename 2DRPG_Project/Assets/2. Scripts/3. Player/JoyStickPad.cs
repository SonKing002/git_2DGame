using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; //유아이
using Main; //네임스페이스

namespace Main
{
    //캐릭터 움직임, 공격 점프 대쉬 스킬 버튼
    public class JoyStickPad : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {

        public Button dash_Btn;
        public PlayerMove playerMove;


        public void OnPointerDown(PointerEventData eventData)
        {
            playerMove.isDash = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            playerMove.isDash = false;
        }

    }//class

}//namespace
