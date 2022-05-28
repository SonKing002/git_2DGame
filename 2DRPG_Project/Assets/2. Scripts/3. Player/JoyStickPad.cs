using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; //������
using Main; //���ӽ����̽�

namespace Main
{
    //ĳ���� ������, ���� ���� �뽬 ��ų ��ư
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
