using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;//네임스페이스

namespace Main
{
    public class Player_AnimatorReceiver : MonoBehaviour
    {
        public Player_Attack_Defend playerAttackDefend;
        public PlayerMove playerMove;
        int tempAttack;

        private void Start()
        {
            tempAttack = 0;
        }
        //애니메이션에서 호출되며 공격타이밍의 시작을 알리는 이벤트함수
        public void RealAttackStart()
        {
            playerAttackDefend.attackCount = 1;
        }

        //애니메이션에서 호출되며 공격타이밍의 끝남을 알리는 이벤트함수
        public void RealAttackEnd()
        {
            //타이밍조건 비활성화
            playerAttackDefend.attackCount = tempAttack;
            playerAttackDefend.playerWeaponTrigger_Component.enabled = false;
        }

        //피격 애니메이션이 끝나면 호출되는 이벤트 함수
        public void DamagedEnd()
        {
            playerAttackDefend.anim.SetBool("isDamaged", false);
        }

        public void sittingStart()
        {
            //버튼 활성화
            playerMove.else_Act_btn.interactable = true;
            playerMove.isSitting = true;

            //앉아있는 상태
            playerMove.anim.SetBool("isSitting", true);//앉아있는 상태 
            //조건 활성
            //playerMove.isSitting = true;
        }

        public void sittingEnd()
        {
            //버튼 재활성화
            playerMove.interactableCtrl(true);
            //앉기 버튼 비활성화 
            //playerMove.isSitting = false;
            
        }
    }//class
}//namespace