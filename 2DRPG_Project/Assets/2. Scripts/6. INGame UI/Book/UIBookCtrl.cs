using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;

namespace Main
{
    //������ ������
    public class UIBookCtrl : MonoBehaviour
    {
        //�޾ƿ��� ������Ʈ
        public Slider menu;
        public GameObject menu_contents; //(�ɷ�ġ,����, ��ų ���) �������� �θ� fill ������Ʈ �޾ƿ��� 
        public GameObject joyStickPad;

        //����
        bool isSliderClick; //�ﰢ���� Ŭ���ϸ� true : �޴��� ��� ���´�

        //å ����
        public GameObject screenShielder;// ȭ�� ��ȣ��
        public GameObject[] books; //�ȳ� å�� ���̵�
                                   //(0 �ִϸ��̼�) (1 ����) (2 �ɷ�â) (3 ����,���â) (4 ��ųâ) (5 ����â)
        [HideInInspector]
        public Animator bookAnim; //å �ִϸ��̼�

        [HideInInspector]
        public int bookNum; //�ε��� ����� (å ������ 0 �ʱ�ȭ)

        public bool isPopUp; // true�϶� å�� â�� ���� 
        public bool isPrevExist; // true = ������ �̵��� �� ����, false = ó�� å�� ��ħ (å ������ false �ʱ�ȭ)
                                 //�ִϸ��̼� ������ �¿츦 �����ϱ� ����

        bool isScreenShielderOn; //��ũ�� ���� lerp�� ����ϱ� ���ؼ� �����
        public BookAnimatorCtrl bookAnimatorCtrl; 
        public Text bookTitle_txt; //���� â���� �˷��ִ� �ؽ�Ʈ

        void Start()
        {
            //�ʱ�ȭ
            isPrevExist = false;
            isSliderClick = false;
            menu.value = 0f;
            bookNum = 0;

            //å�߿���
            for (int i = 0; i < books.Length; i++)
            {
                //�ִϸ����Ͱ� �ִٸ�
                if (books[i].GetComponent<Animator>())
                {
                    //������ �Ѵ�.
                    bookAnim = books[i].GetComponent<Animator>();
                }
            }
            //���� print(bookAnim.name);

            //bookAnimatorCtrl = FindObjectOfType<BookAnimatorCtrl>(); ������ �����ϱ� ������ �Ҵ��� ã�� �� ����. �� ���۷��� �ͼ���
        }

        //�޴���ư�� ������ ȣ��Ǵ� �Լ�
        public void OnClick_SliderMenu_btn()
        {
            //true false ����
            isSliderClick = !isSliderClick;
            print(isSliderClick);
        }

        //���� �����̴� ���� �ݱ�
        public void SliderMenu()
        {
            //true�϶�
            if (isSliderClick)
            {
                //�޴��� �����ش�.
                menu.value = Mathf.Lerp(menu.value, 1f , Time.deltaTime * 4f);//lerp �����̴� ����, lerp ȭ�麸ȣ ����

                //�߰��� 
                if (0f < menu.value && menu.value < 1f)
                {
                    //���� ���ϵ��� �Ѵ�.
                    menu.interactable = false;
                }

                //������ ���̱�
                menu_contents.SetActive(true);

            }
            //false�϶�
            else if(!isSliderClick)
            {

                //�޴��� ������.
                menu.value = Mathf.Lerp(menu.value, 0f , Time.deltaTime * 4f);
                
                //�߰���
                if (0f < menu.value && menu.value < 1f)
                {
                    //���� ���ϵ��� �Ѵ�.
                    menu.interactable = false;
                }

                if (menu.value <= 0.004f)
                {
                    //������ ������
                    menu_contents.SetActive(false);
                }
            }
        }

        //�˾�â : ���â, �κ��丮, ��ųâ, ����â 
        public void OnClick_Something_Btn(int i)
        {
            if (!isPopUp) //�� �ѹ��� 
            {
                //����ġ
                isPopUp = true;

                //���� Ȱ��ȭ
                bookTitle_txt.gameObject.SetActive(true);

                //å�� Ȱ��ȭ
                books[0].SetActive(true);
                
                //ȭ�� ��ȣ �ѱ�
                screenShielder.SetActive(true);
                //�����е� ����
                joyStickPad.SetActive(false);

                //������Ʈ���� ȭ�麸ȣ�� arpha�� ���� 
                isScreenShielderOn = true;

                //���� : bookAnimatorCtrl���� �ش� ������ �� �� ����
                bookNum = i;

                //å�� �ִϸ��̼�
                bookAnim.SetTrigger("OpenBook"); //�ִϸ��̼��� ���κп� event function �־��� : bookAnimatorCtrl

            }

            //å�ڸ� ���������
            else
            {
                // ���â �������� �����ٸ� ��� ���� ����
                if (bookNum == 1)
                {
                    isPopUp = false;

                    //���� ��Ȱ��ȭ
                    bookTitle_txt.gameObject.SetActive(false);

                    //������Ʈ���� ȭ�麸ȣ�� arpha�� ����
                    isScreenShielderOn = false;

                    //ȭ�� ��ȣ ����
                    screenShielder.SetActive(false);
                    //�����е� �ѱ�
                    joyStickPad.SetActive(true);


                    for (int ii = 0; ii < books.Length; ii++)
                    {
                        //�ִϸ��̼Ǹ� �����ϰ�
                        if (ii != 0)
                        {
                            //���� ����
                            books[ii].SetActive(false);
                        }
                    }
                    //å�� �ִϸ��̼�
                    bookAnim.SetTrigger("CloseBook"); //�ִϸ��̼��� ���κп� event function �־��� : bookAnimatorCtrl
                }
            }
        }

        //x �� �����ٸ� �Ǵ� �ڷΰ��⸦ �����ٸ�
        public void OnClick_Exit_btn()
        {
            if (isPopUp)
            {
                //���â����
                if (bookNum == 1)
                {
                    isPopUp = false;

                    //���� ��Ȱ��ȭ
                    bookTitle_txt.gameObject.SetActive(false);

                    //ȭ�� ��ȣ�� ����� ���
                    isScreenShielderOn = false;

                    //ȭ�� ��ȣ ����
                    screenShielder.SetActive(false);
                    //�����е� �ѱ�
                    joyStickPad.SetActive(true);

                    for (int ii = 0; ii < books.Length; ii++)
                    {
                        //�ִϸ��̼Ǹ� �����ϰ�
                        if (ii != 0)
                        {
                            //���� ����
                            books[ii].SetActive(false);
                        }
                    }
                    //å�� �ִϸ��̼�
                    bookAnim.SetTrigger("CloseBook"); //�ִϸ��̼��� ���κп� event function �־��� : bookAnimatorCtrl
                }
                else //�ٸ� ��� â������
                {
                    //���� �ۼ� Ȱ��
                    bookAnimatorCtrl.SendMessage("RealOpen",-bookNum +1); 
                    //bookAnimatorCtrl.RealOpen(); leftTurn�ִϸ��̼� �����ϰ� �ϰ� bookNum = 1 ���â�� ����
                }
            }
        }

        //Ư�� �������� �̵���Ű��
        public void OnClick_GotoPage_btn(int i)
        {
            //+������
            bookAnimatorCtrl.SendMessage("RealOpen", i);
        }


        //Ű���� �Է°���
        void KeyboardInput()
        {

            //I Ű���忡�� �κ��丮â�� ����
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (!isPopUp) //�ѹ��� ����
                {
                    OnClick_Something_Btn(3);
                }
            }
            //K Ű���忡�� ��ųâ�� ����
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (!isPopUp)
                {
                    OnClick_Something_Btn(4);
                }
            }
            //U Ű���忡�� �ɷ�ġ�� ����
            if (Input.GetKeyDown(KeyCode.L))
            {
                if (!isPopUp)
                {
                    OnClick_Something_Btn(2);
                }
            }
            //ESC Ű���忡�� ����â -> ��� -> �ݱ� 
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPopUp)
                {
                    //����â
                    OnClick_Something_Btn(5);
                }
                else
                {
                    //�ڷΰ���, â ������
                    OnClick_Exit_btn();
                }
            }
        }//Ű����

        void Update()
        {
            //������ ������
            if (isScreenShielderOn)
            {
                //��ũ�� �Ǵ� �ѱ�
                screenShielder.SetActive(true);
                //�����е� ����
                joyStickPad.SetActive(false);

                //�ð� ���߱�
                Time.timeScale = Mathf.Lerp(Time.timeScale, 0f, 4f);
                //�ִϸ����� �ð� ����x
                bookAnim.updateMode = AnimatorUpdateMode.UnscaledTime;
            }
            else//������ ������
            {
                //��ũ�� �Ǵ� ����
                screenShielder.SetActive(false);
                //�����е� �ѱ�
                joyStickPad.SetActive(true);

                //�ð� ����
                Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, 4f);
                //�ִϸ����� �ð� ����
                if (Time.timeScale == 1)
                {
                    bookAnim.updateMode = AnimatorUpdateMode.Normal;
                }
            }

            //����

            //�����̴� ����
            SliderMenu();

            //���� �۾� ǥ�ÿ� (�˾������϶�, ������ ��ȯ�� �ƴҶ�
            if (isPopUp)
            {
                switch (bookNum)
                {

                    case 1:
                        bookTitle_txt.text = "1��. ����";
                        break;
                    case 2:
                        bookTitle_txt.text = "2��. �⺻ �ɷ�ġ";
                        break;
                    case 3:
                        bookTitle_txt.text = "3��. ���� & ���";
                        break;
                    case 4:
                        bookTitle_txt.text = "4��. ��ų â";
                        break;
                    case 5:
                        bookTitle_txt.text = "5 ��. ����";
                        break;
                }//1,2,3,4,5 
             
            }//isPopUp

            //Ű���� ��Ʈ��
            KeyboardInput();
            
        }
    }//class
}//namespace