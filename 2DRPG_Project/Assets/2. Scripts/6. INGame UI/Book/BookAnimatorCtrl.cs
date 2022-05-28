using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;
using UnityEngine.UI;

namespace Main
{
    //�ִϸ��̼ǿ� ���� ������ ��ȭ�� ���
    public class BookAnimatorCtrl : MonoBehaviour
    {
        //Ÿ ��ũ��Ʈ
        UIBookCtrl uiCtrl;
        public Button turn_LeftPage;
        public Button turn_RightPage;

        //����� ������
        int prev_BookNum;//�������� �������̶� ������ �������� �ٸ� �� �ִ� ��� == �������� �ε��� ��ȣ ����
        [HideInInspector]
        public bool isPageChangeMotion; //true ��� ���ϴ� ��
        void Start()
        {
            //Find �Ҵ�
            uiCtrl = FindObjectOfType<UIBookCtrl>();
        }

        // �����ϴ� Ʈ���� �ִϸ��̼��� ������ ȣ�� �Լ� RealOpen, (�� ������ ��, ���� ������) ��ư ������ ȣ�� �Լ� 
        public void RealOpen(int i)//(int i�� book�ε����� �ش��ϴ� ���� ����޶�� ��û)
        {
            //��� ������ = ���������� �ε��� ����
            prev_BookNum = uiCtrl.bookNum;

            //�ִϸ��̼� �� �������, ����� (event function���� ���� ����� �Լ��� RealTurn...�Լ��� ����)
            if (!uiCtrl.isPrevExist)// ó�� ��ĥ��
            {
                //������ �ִϸ��̼� ��, 
                uiCtrl.bookAnim.SetTrigger("PageTurnRight");
                //��� ������ ����
                uiCtrl.isPrevExist = true;
            }
            else// ��� �������� �ִ� ���
            {
                //��ư (�������� : i = -1, ���� ������ : i = +1);
                uiCtrl.bookNum = uiCtrl.bookNum + i;

                //��� ������ �ε������� ū ��� 
                if (uiCtrl.bookNum > prev_BookNum)
                {
                    //��������� ��Ȱ��ȭ
                    uiCtrl.books[prev_BookNum].SetActive(false);

                    //���������� �������� �ѱ�� �ִϸ��̼�
                    uiCtrl.bookAnim.SetTrigger("PageTurnRight");
                }
                else//��� ������ �ε����� ū ���
                {
                    //��������� ��Ȱ��ȭ
                    uiCtrl.books[prev_BookNum].SetActive(false);

                    //�������� �������� �ѱ�� �ִϸ��̼�
                    uiCtrl.bookAnim.SetTrigger("PageTurnLeft");
                }
            }
        }//void RealOpen()


        //������ �ѱ�� Ʈ���� �ִϸ��̼��� ������ ȣ�� �Լ�
        public void RealTurnRight()
        {
            //ó�� ��ģ ���������
            if (!uiCtrl.isPrevExist)
            {
                //������ Ȱ��ȭ
                uiCtrl.books[uiCtrl.bookNum].SetActive(true);
            }
            else //������������ �Ѿ��
            {
                //������ġ
                if (uiCtrl.bookNum <= 5)
                {
                    //�ش� �������� �̵�
                    uiCtrl.books[uiCtrl.bookNum].SetActive(true);
                }
            }
        }
        //���� �ѱ�� Ʈ���� �ִϸ��̼��� ������ ȣ�� �Լ�
        public void RealTurnLeft()
        {
            //������ġ
            if (uiCtrl.bookNum >= 1)
            {
                //�ش� �������� �̵�
                uiCtrl.books[uiCtrl.bookNum].SetActive(true);
            }
        }

        //Ŭ�����ϴ� Ʈ���� �ִϸ��̼��� ������ ȣ�� �Լ�
        public void RealClose()
        {
            //���� 0 �ʱ�ȭ
            prev_BookNum = uiCtrl.bookNum = 0;
            
            //�ִϸ��̼��� ������ ������ ���� å ������Ʈ ��Ȱ��ȭ
            gameObject.SetActive(false);
        }

        void Update()
        {
            //������ �ѱ�� ��ư Ȱ��ȭ ����
            switch (uiCtrl.isPopUp)
            {
                case true:
                    turn_LeftPage.gameObject.SetActive(true);
                    turn_RightPage.gameObject.SetActive(true);

                    //�� ù������ ���϶��
                    if (uiCtrl.bookNum <= 1)
                    {
                        turn_LeftPage.interactable = false;
                    }
                    else
                    {
                        turn_LeftPage.interactable = true;
                    }
                    //�� ���������̰ų� �ִϸ��̼� ���������
                    if (uiCtrl.bookNum == 5 || uiCtrl.bookNum == 0)
                    {
                        turn_RightPage.interactable = false;
                    }
                    else
                    {
                        turn_RightPage.interactable = true;
                    }
                    break;

                default:
                    turn_LeftPage.gameObject.SetActive(false);
                    turn_RightPage.gameObject.SetActive(false);
                    break;
            }
        }//update
    }//class
}//namespace