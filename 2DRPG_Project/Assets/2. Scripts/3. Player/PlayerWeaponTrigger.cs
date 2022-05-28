using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{
    public class PlayerWeaponTrigger : MonoBehaviour
    {
        //���� üũ
        public bool isOnAttackRange; //���ݹ����� ������

        //�ӽ� ���ʹ� �޾ƿ���
        public CapsuleCollider2D weapon_CapsuleCollider;
        public GameObject temp_Enemy;
        public int targetCount;

        private void Start()
        {
            targetCount = 0;
        }

        //���⿡ � ��ü�� ���� �� 
        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Enemy":
                    if (targetCount == 0)
                    {
                        print("������ ����");

                        //�ش� ���� �޾ƿ���
                        temp_Enemy = collision.gameObject;

                        //������ �ǰ�
                        collision.gameObject.SendMessage("Damaged");
                        //�ѹ� ȣ�� �� �ݶ��̴� ����
                        weapon_CapsuleCollider.enabled = false;

                        targetCount++;
                    }

                    //�ʱ�ȭ
                    targetCount = 0;

                    break;
            }
        }

        //���⿡ � ��ü�� ������ ��
        private void OnTriggerExit2D(Collider2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Enemy":

                    /*
                    //�ʱ�ȭ
                    temp_Enemy = null;
                    */
                    print("������");
                    break;
            }
        }
    }

}