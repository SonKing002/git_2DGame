using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;
using UnityEngine.EventSystems;

namespace Main
{

    //방패버튼을 누를 때
    public class ShieldBtnContrl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        //
        public Player_Attack_Defend player_Attack_Defend;

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("누름");
            player_Attack_Defend.isDefense = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("뗌");
            player_Attack_Defend.isDefense = false;
        }
    }
}