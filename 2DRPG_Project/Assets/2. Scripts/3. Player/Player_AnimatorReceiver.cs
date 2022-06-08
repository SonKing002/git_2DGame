using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;//���ӽ����̽�

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
        //�ִϸ��̼ǿ��� ȣ��Ǹ� ����Ÿ�̹��� ������ �˸��� �̺�Ʈ�Լ�
        public void RealAttackStart()
        {
            playerAttackDefend.attackCount = 1;
        }

        //�ִϸ��̼ǿ��� ȣ��Ǹ� ����Ÿ�̹��� ������ �˸��� �̺�Ʈ�Լ�
        public void RealAttackEnd()
        {
            //Ÿ�̹����� ��Ȱ��ȭ
            playerAttackDefend.attackCount = tempAttack;
            playerAttackDefend.playerWeaponTrigger_Component.enabled = false;
        }

        //�ǰ� �ִϸ��̼��� ������ ȣ��Ǵ� �̺�Ʈ �Լ�
        public void DamagedEnd()
        {
            playerAttackDefend.anim.SetBool("isDamaged", false);
        }

        public void sittingStart()
        {
            //��ư Ȱ��ȭ
            playerMove.else_Act_btn.interactable = true;
            playerMove.isSitting = true;

            //�ɾ��ִ� ����
            playerMove.anim.SetBool("isSitting", true);//�ɾ��ִ� ���� 
            //���� Ȱ��
            //playerMove.isSitting = true;
        }

        public void sittingEnd()
        {
            //��ư ��Ȱ��ȭ
            playerMove.interactableCtrl(true);
            //�ɱ� ��ư ��Ȱ��ȭ 
            //playerMove.isSitting = false;
            
        }
    }//class
}//namespace