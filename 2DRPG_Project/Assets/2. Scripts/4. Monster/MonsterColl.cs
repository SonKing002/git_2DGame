using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{
    //���Ϳ��� ���� ��ü �˻�
    public class MonsterColl : MonoBehaviour
    {
        public MonsterCtrl monsterCtrl;

        //���� ����
        //(�ݶ��̴����� ĳ���Ͷ� ������ + !isAttack �϶�) ����
        private void OnCollisionEnter2D(Collision2D collision)
        {
            switch (collision.collider.tag)
            {
                //�÷��̾�
                case "Player":
                    //�������� �ƴ� ��
                    if (!monsterCtrl.isAttack)
                    {
                        //���� �ִϸ��̼� ���
                        monsterCtrl.anim.SetBool("isAttack", true);
                        monsterCtrl.anim.SetTrigger("attack");

                        print("���");

                        //���� �ֱ� �ʱ�ȭ
                        monsterCtrl.attackTimer = 0f;
                        monsterCtrl.temp_TimeCheck = 0;
                    }
                    break;
            }
        }

        //�������� ����
        private void OnCollisionExit2D(Collision2D collision)
        {
            switch (collision.collider.tag)
            {
                //�÷��̾�
                case "Player":
                    
                    break;
            }
        }
    }
}
