using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{
    public class PlayerColl : MonoBehaviour
    {
        //������Ʈ
        PlayerMove playerMove; //ĳ����

        void Start()
        {
            playerMove = GetComponent<PlayerMove>(); //���� ������Ʈ ���� ����
        }

        //�ݶ��̴��� ���� ����
        private void OnCollisionEnter2D(Collision2D collision)
        {
            //�±� �˻�
            switch (collision.gameObject.tag)
            {
                case "Ground":
                    playerMove.isGround = true; // ���� �ִ°� 
                    break;

            }
        }

        //�ݶ��̴��� �������� ����
        private void OnCollisionExit2D(Collision2D collision)
        {
            //�ױ� �˻�
            switch (collision.gameObject.tag)
            {
                case "Ground":
                    playerMove.isGround = false;// ���߿� �ִ°�
                    print("����");
                    break;
            }
        }//trigger
    }//class
}//namespace