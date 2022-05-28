using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI ���
using Main;

namespace Main
{

    // ������
    // 0 �ΰ�, 1 ����, 2 ��ũ, 3 ���� : (== ��ư ���� 0)
    // 4 ��Ÿ�� (���), 5 ��, 6 ������ 4���� : (== ��ư ���� 1)
    // 7 ����, 8 ����, 9 ���� : (== ��ư ���� 2)

    //��ư Ŭ�� : ���� �̹��� -> �� ��������Ʈ������ ����
    //���� ���� �Ϸ�� �� ��������Ʈ ������ + �ɷ�ġ -> �����ͺ��̽� ����

    public class AvatorSelectYourAppearance : MonoBehaviour
    {
        //����â ���� �ѱ�
        bool isSwitch; //false <-> true

        int nowContentsPage; //���� ������
        int nowbuttonsIndex; //���� ��ư �ε��� ��ġ

        //�ٸ� ������Ʈ, ��ũ��Ʈ
        public GameObject[] contents; //���� â ���� (Ȱ�� ��Ȱ��)
        public ScrollRect scrollRect; //��å â�� �ٲ�� == ��ũ�� ���ᵵ �ٲ�

        public Button next, prev; // ���� <-> ��Ÿ��, ��, ���� <-> ��,����
        public Text nextText, prevText; // �۾� ǥ��
        public GameObject[] buttons;//��ư�� �������� (Ȱ�� ��Ȱ��)

        void Start()
        {
            //�ʱ�ȭ
            isSwitch = false;
            nowbuttonsIndex = 0;

            //ó�� ���� ��Ȱ��
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
            }
            //��������â Ȱ��ȭ
            buttons[0].SetActive(true);
        }

        //����â ��Ʈ��
        public void OnClick_Contents_Btn(int a)
        {
            //�ϴ� ���� ����
            for (int i = 0; i < contents.Length; i++)
            {
                contents[i].SetActive(false);
            }
            isSwitch = true;

            //true�϶�
            if (isSwitch)
            {
                //���ְ�
                contents[a].SetActive(true);
                
                //���� �ε����� �����ϰ�
                nowContentsPage = a;
                //(��ũ��) ���� �ε����� �������� �����Ѵ�
                scrollRect.content = contents[nowContentsPage].GetComponent<RectTransform>();
            }
        }

        //�ν����� button�� ���� ȣ�� �Լ�: �ڷ� ����
        public void OnClick_PrevButtons_btn()
        {
            //���� ��Ȱ��
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
            }

            //ii = 0,1,2 �� �ε����� ��ư Ȱ��ȭ �����ϱ� ����
            nowbuttonsIndex--;
            buttons[nowbuttonsIndex].SetActive(true);

            //����ġ �ʱ�ȭ
            isSwitch = false;
        }

        //�ν����� button�� ���� ȣ�� �Լ� : ������ ����
        public void OnClick_NextButtons_btn()
        {
            //���� ��Ȱ��
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(false);
            }

            nowbuttonsIndex++;
            buttons[nowbuttonsIndex].SetActive(true);

            //����ġ �ʱ�ȭ
            isSwitch = false;
        }

        void Update()
        {
            //��ư ���� �ε����� ���� Ȱ�� ��Ȱ��
            switch (nowbuttonsIndex)
            {
                case 0://�� ��
                    prev.interactable = false;
                    prevText.text = "";

                    next.interactable = true;
                    nextText.text = "��Ÿ�� & ������";
                    break;

                case 1://�߰�
                    prev.interactable = true;
                    prevText.text = "���� ����";

                    next.interactable = true;
                    nextText.text = "�Ƿ� & ����";
                    break;
                case 2://�� ��
                    prev.interactable = true;
                    prevText.text = "��Ÿ�� & ������";

                    next.interactable = false;
                    nextText.text = "";
                    break;
            }
   
            //false�϶�
            if (!isSwitch)
            {
                //���� ������ ���ش�
                contents[nowContentsPage].SetActive(false);

                //(��ũ��)���� �ε����� ����
                scrollRect.content = null;
            }
        }//update



    }//class
}//namespace