using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;
using UnityEngine.EventSystems;

namespace Main
{

    //���й�ư�� ���� ��
    public class ShieldBtnContrl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        //
        public Player_Attack_Defend player_Attack_Defend;

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("����");
            player_Attack_Defend.isDefense = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("��");
            player_Attack_Defend.isDefense = false;
        }
    }
}