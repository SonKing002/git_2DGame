using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Main;

namespace Main
{
    public class LobbyMouseOn : MonoBehaviour
    {
        //�ִϸ��̼�
        public Animator playerAnim;

        //����â (�÷�.���İ� ����)
        public Image statView;
        //���� ���� (�÷�.���İ� ����)
        public Text[] allText = new Text[10];

        public Animator textAnim;

        //���콺�� �÷��� �ִ���
        bool isHighLight;
        //text anim�� Ʈ���� �ѹ��� �۵��ϵ��� �Ѵ�
        bool isTriggerCtrl;

        //������ �ӽ� ����
        int random;

        private void Start()
        {
            playerAnim.Play("Idle");
        }
        //���콺 Ŭ���Ҷ�
        private void OnMouseDown()
        {
            //���� ���׼� 
            random = Random.Range(0, 2);

            if (random == 0)
            {
                //�ǰݸ��
                playerAnim.SetTrigger("Deselected");
            }
            if (random == 1)
            {
                //���ݸ��
                playerAnim.SetTrigger("Selected");
            }
        }

        //���콺�� ���ö� 
        private void OnMouseEnter()
        {
            //ĳ���� ���� ����
            isHighLight = true;
        }

        //���콺�� ������
        private void OnMouseExit()
        {
            //ĳ���� ���� ����
            isHighLight = false;
       
        }


        private void Update()
        {
            //ĳ���� ������ ���콺�� ������
            if (isHighLight)
            {
                playerAnim.SetBool("isHighLight", true);

              
                
                //�ؽ�Ʈ Ʈ���� ����� false�϶�
                if (!isTriggerCtrl)
                {
                    // �ִϸ��̼� Fade In
                    textAnim.SetTrigger("On");
                    //true
                    isTriggerCtrl = true;
                }

                /* ���ɸ� ���� -> �ִϸ����͸� ���� ��������� �������� ����
                //����â�� ���δ�
                statView.color = Color.Lerp(statView.color, new Color(1, 1, 1, 1), 2f * Time.deltaTime);

                //���� ���δ�
                for (int i = 0; i < allText.Length; i++)
                {
                    allText[i].color = Color.Lerp(allText[i].color, new Color(0.1317f, 0.1357f, 0.1981f, 1), 1f * Time.deltaTime);
                }
                */
            }
            //ĳ���� ������ ���콺�� ����
            else
            {
                playerAnim.SetBool("isHighLight", false);
              
                //�ؽ�Ʈ Ʈ���� ����� true�϶�
                if (isTriggerCtrl)
                {
                    // �ִϸ��̼� Fade out
                    textAnim.SetTrigger("Off");

                    //false
                    isTriggerCtrl = false;
                }

            }
        }

    }//class
}//namespace