using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{
    //���� ��Ʈ�ڽ� �˻�
    public class MonsterColl : MonoBehaviour
    {
        //��ũ��Ʈ
        public BoxCollider2D hitBox; //�������� ����

        //������
        public float temp_FrontBox_X; //��Ʈ�ڽ� ��ġ ������ ( TorchLighter.x +-0.3044 )
        public float temp_FrontBox_Y; //��Ʈ�ڽ� ��ġ ������

        void Start()
        {
            //������Ʈ
            hitBox = GetComponent<BoxCollider2D>();

            //�������� (SCV ���͸��� �ٸ��� ���� �ʿ�)
            temp_FrontBox_X = 0.3044f;
            temp_FrontBox_Y = 0.5923f; 
        }

        //��Ʈ�ڽ��� ��� ����
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //tag �˻�
            switch (collision.transform.tag)
            {
                //�÷��̾���
                case "Player":
                    //�÷��̾��� �ǰ��Լ��� ȣ�� �޼����� ����
                    collision.SendMessage("Damaged");
                    break;
            }
        }

        //�������� �ٶ󺼶� (MonsterCtrl����)
        public void RightDirBox()
        {
            //�θ���� ��ġ ����
            transform.localPosition =
                new Vector2(temp_FrontBox_X, temp_FrontBox_Y);
        }

        //������ �ٶ󺼶� (MonsterCtrl����)
        public void LeftDirBox()
        {
            //�θ���� ��ġ ����
           transform.localPosition = 
                new Vector2(-temp_FrontBox_X, temp_FrontBox_Y);
        }
    }
}