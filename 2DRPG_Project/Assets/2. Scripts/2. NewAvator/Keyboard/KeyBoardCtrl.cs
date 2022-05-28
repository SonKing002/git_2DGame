using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;
using System;
using System.Text;

namespace Main
{
    //�̸� �ۼ��ϴ� Ű���� ��ư �۵��ϵ���
    //�ѿ� ��ȯ, shift:(Eng) �빮�� �ҹ��� ��ȯ, (Kor) �ȼҸ�Consonant ��ȯ, Delete, Enter �̸� ���

    public class KeyBoardCtrl : MonoBehaviour
    {
        //�ν����� Ű���� : �Է¿� ���� ����ǥ��
        public Text[] keyRaws;

        //�����̽�, �ۼ��Ϸ� ��ư (Ȱ�� ��Ȱ�� �����ϱ� ����)
        public Button delete, shift, space, engKor, done;

        //�ȳ��� text 
        public Text notice_Text;

        //�ӽ� ���� ���� �ٲ�ִ� �뵵
        string[] tempEngKeys = new string[36]
        {
            "1","2","3","4","5","6","7","8","9","0"
            ,"q","w","e","r","t","y","u","i","o","p"
            ,"a","s","d","f","g","h","j","k","l"
            ,"z","x","c","v","b","n","m"
        };
        //�ӽ� �ѱ� ���� �ٲ�ִ� �뵵
        string[] tempKorKeys = new string[36]
        {
            "1", "2" , "3" , "4" , "5" , "6" , "7" , "8" , "9" , "0"
            ,"��","��","��","��","��","��","��","��","��","��"
            ,"��","��","��","��","��","��","��","��","��"
            ,"��","��","��","��","��","��","��"
        };

        /*��Ģ ã�� : �������Ʈhttps://mchch.tistory.com/84 
         
        _1. ����� ��_
        //�ڸ�, �ڸ���, �ڸ� �ڸ�, �ڸ�����, �ڸ��� �ڸ�  ex > ��, ��, �ƶ�, ��, �˰�
        //���, �ڸ��, �ڸ���� �ڸ�� �ڸ�, �ڸ������, �ڸ���� �ڸ� , ex > ��, ��, ��, �Ͷ� ,��, ����


        _2. ��Ģ�� ������ ���_

        *if(�ڸ� + ��(+��))
        
        //�ڸ� �ڸ� (��ħ����) : ex ���� ����
        //�ڸ��� �� : ����(���) '���� �Է��ϰ� (�����̽��� ������ ���ٰ� ���� ��) ���� �Է�

        *if(�ڸ� + ��) 
        
        //�� ���(o) : ( if( �� �� �� + ){}  else{} ) 
        //1. �� + "�� �� ��"
        //2. �� + "�� �� ��"
        //3. �� + "��" 
        //�ڸ� �� : �� �ܴ̿� �� ����, (��� = ��)�� ��

        *if(�ڸ� + ��(+��) + (+��))
        
        //���� ����  (o) : "��","��","��","��","��","��","��","��","��","��","��"
        //���� �� �� (x) : �� �ܴ̿� �� ���� ex) �ڸ�'�� �ڸ�' : ����
        //���� ���� + if���� : ����

        1�ܰ� �����ΰ� �����ΰ� �Ǵ� �ʿ�

        //�ʼ� �߼� ���� -> ��ģ�� �и��Ѵ�
        2�ܰ� �� ������ ���� ���� ���� �ʿ�
        3�ܰ� ������ ���� �ڿ� ���� �ٳĿ� ���� �б⹮ �ʿ�
        */
        /* �����ڵ带 �̿��� �ڸ� �и� ���� :
         * �������Ʈhttps://storycompiler.tistory.com/212
         * �������Ʈhttps://www.stechstar.com/user/zbxe/WinApps/63202
         */

        //�ʼ� �з� (�ѱ� �Լ��� ���� �迭)
        string[] chosung = new string[19]
        {
             "��","��","��","��","��","��","��","��","��","��",
             "��","��","��","��","��","��","��","��","��"
        };

        //�߼� �з� (�ѱ� �Լ��� ���� �迭)
        string[] joongsung = new string[21]
        {
            "��","��","��","��","��","��","��","��","��","��",
            "��","��","��","��","��","��","��","��","��","��","��"
        };

        //���� �з�(�ѱ� �Լ��� ���� �迭)
        string[] jongsung = new string[28]
        {
            "", "��", "��", "��", "��", "��", "��", "��", "��","��",
            "��", "��", "��", "��", "��", "��", "��","��","��", "��",
            "��", "��", "��", "��", "��", "��", "��", "��"
        };

        // name�� Lengthüũ
        int num;
        //�ʼ��߼����� ��� �ܰ躰�� ++���� (case 1,2,3,4...)
        int korCounting;

        //�ӽ�_����� �� �� ����
        string temp_Chosung, temp_Joongsung, temp_Jongsung; 
        string temp_oldJongsung, temp_newJongsung; // (���������� -> ������ ������) �ӽ������� ������ �ʿ��ϴ�.

        //�ϼ���
        ushort temp_JoogsungUint16; 

        //�ϼ��� �����ڵ� ��ġ�� Ȱ��
        static ushort unicode_KorBase = 0xAC00;

        //�������� �Ǵܿ�
        bool isMoum;
        bool isGyeupJaum;

        //�����
        bool isShift; //����Ʈ ���ȴ���
        bool isKor; //�ѱ۷� �ٲ������
        bool isConsonant; //�ȼҸ��� �ٲ�� �ϴ���
        [HideInInspector]
        public bool isDoneButtonClicked;//������Ʈ���� �ۼ��ϷḦ �����ϰ� ���� �ߺ� ������ ������ ����

        //�̸� �߰��ϱ�
        public Text showMyName; // ���ӿ�����Ʈ myName.text = name + blinkText;
        string nameText;//�̸��ִ� �κ�
        string blinkText;//�����̴� �κ�
        public EnumPlayer_Information usingMyNewPlayer;
        //���ڼ� ����
        bool isMaxLength;

        float deltaTime;//�� �ð�����
        float temp_TimeCheck;//�ð� üũ �ӽú���

        //�̸� �ۼ����̸� ���, �����̸� ȸ��
        public Color fontColor_default; //ȸ��
        public Color fontColor_bright;  //���

        DataController saveData;

        private void Start()
        {
            nameText = "";

            saveData = FindObjectOfType<DataController>();
        }

        //�ܺ� �ν����Ϳ����� ���� ������ ���ڰ� ������ ��ưŰ
        public void OnClick_Keyboard_btn(int i)
        {
            //�ִ� ���� ���� ������ (��������)
            if (!isMaxLength)
            {
                //�ѱ��� �Է��� �� (�ѱ� Ű && 0~9���� ���ڸ� ����)
                if (isKor && i >= 10)
                {
                    //# �������� �˻� (���� ������ ���� ����� �� ó��)
                    Check(char.Parse(keyRaws[i].text));

                    //# �ʼ� -> �߼� -> ����: �ܰ躰�� �ۼ��ϱ� ���� = korCounting

                    //# � ���� ��� �ִ��� �˾ƾ� ���� �Ǵ� ���� ���ڷ� �ѱ� �� ����

                    //�߼�: ���� + ���� �� + "�� �� ��"
                    //�߼�: ���� + ���� �� + "�� �� ��"
                    //�߼�: ���� + ���� �� + "��" 
                    //����: ���� + ���� "��","��","��","��","��","��","��","��","��","��","��"
                    //����: ���� + ���� ex: �� + �� -> ����

                    print(korCounting + "����ܰ�");

                    switch(korCounting)
                    {
                        case 0://ù ���� (# �������� �Ǵ�)

                            //�����϶�
                            if (!isMoum)
                            {
                                //�ʼ����� ����
                                temp_Chosung = keyRaws[i].text;

                                //�ϴ� �ؽ�Ʈ�� "text"ǥ��
                                nameText += temp_Chosung;

                                //���� ī��Ʈ++
                                korCounting++;
                                print("0 ����");
                            }
                            //�����϶�
                            else
                            {
                                print("������ �ȵ˴ϴ�.");
                            }
                            break;
                       
                        case 1: //�ι�°����  (# �ʼ� + �߼�)

                            //(���� �����ٸ� ī���� 1���� ����)
                            if (!isMoum)
                            {
                                //���ο� �ʼ�
                                temp_Chosung = keyRaws[i].text;

                                //�ϴ� �ؽ�Ʈ�� "text"ǥ��
                                nameText += temp_Chosung;
                                print("case 1 ����");
                            }
                            //�����϶�
                            else
                            {
                                temp_Joongsung = keyRaws[i].text;

                                //��+�� : ���յǾ �����ؾ���
                                if (nameText.Length >= 1)
                                {
                                    //�и��� ���¸� ���� �����
                                   nameText = nameText.Substring(0, nameText.Length - 1);
                                }

                                //��ħ ���� ���� = jongsung 0��° �ε���  = "";
                                temp_Jongsung = jongsung[0];

                                //������ �ϼ��ڸ� �ٽ� ����
                                nameText += AddChar(temp_Chosung, temp_Joongsung, temp_Jongsung);

                                //���� ī��Ʈ
                                korCounting++;

                                print("case 1 ����");
                            }
                            break;
                       
                        case 2: //����° ���� (���� + ���� �������� �Ǵ�) # �� �� �� 

                            //(��+��+��) �����϶� = ����
                            if (!isMoum)
                            {
                                temp_Jongsung = keyRaws[i].text;

                                //���յǾ �����ؾ���
                                if (nameText.Length >= 1)
                                {
                                    //�и��� ���¸� ���� �����
                                   nameText = nameText.Substring(0, nameText.Length - 1);
                                }

                                //������ �ϼ��ڸ� �ٽ� ����
                                nameText += AddChar(temp_Chosung, temp_Joongsung, temp_Jongsung);

                                //���� ī��Ʈ
                                korCounting++;

                                print("case 2 ����"); //����case3 �Ǵ� ���ħ���� �Ǵ�
                            }
                            //�����϶�
                            else
                            {
                                print("case 2 ����"); //����case3 �Ǵ� ���ħ���� �Ǵ�

                                //temp_Joongsung �� ����(�Ǥ̤�)�̶��
                                if (temp_Joongsung == "��" || temp_Joongsung == "��" || temp_Joongsung == "��")
                                {
                                    //+keyRaw[i].text �� temp_Joongsung�� ������ �� �ִٸ�
                                    temp_Joongsung = Joongsung(temp_Joongsung, keyRaws[i].text);

                                    //��+�� : ���յǾ �����ؾ���
                                    if (nameText.Length >= 1)
                                    {
                                        //�и��� ���¸� ���� �����
                                         nameText = nameText.Substring(0, nameText.Length - 1);
                                    }

                                    //��ħ�� �����Ƿ� = jongsung 0��° �ε���  = ""; ����
                                    temp_Jongsung = jongsung[0];

                                    //Ȯ�ο� 
                                    print("�߼� Ȯ�ο� "+temp_Chosung + temp_Joongsung +"��" + keyRaws[i].text);

                                    //������ �ϼ��ڸ� �ٽ� ����
                                    nameText += AddChar(temp_Chosung, temp_Joongsung, temp_Jongsung);

                                    print("case 2 ��+����");
                                    //�ڸ�� => �ڸ� ���� 1->2�� �Ѿ�� ������ ���� : ī��Ʈ ����
                                }
                                //temp_Joongsung ������ (�̤Ǥ�)�� �ƴ϶��
                                else
                                {
                                    myKorRullReset();

                                    print("case 2 ���ο� ����");
                                }

                            }
                            break;

                        case 3: //�ڸ��� ����  (# ���ħ ������ �Ǵ°�)

                            //������ �����ٸ�
                            if (!isMoum)
                            {
                                //���� �Է°��� ���������� ���� �� ������
                                if (temp_Jongsung == "��" || temp_Jongsung == "��" || temp_Jongsung == "��" || temp_Jongsung == "��")
                                {
                                    //ù��° ������ �ӽ�����
                                    temp_oldJongsung = temp_Jongsung;

                                    //1. ���������� �Ǵ� "��","��","��","��","��","��","��","��","��","��","��"
                                    temp_Jongsung = Jongsung(temp_Jongsung, keyRaws[i].text);

                                    //�������̶��
                                    if (isGyeupJaum)
                                    {
                                        //�ι�° ������ �ӽ� ����
                                        temp_newJongsung = keyRaws[i].text;

                                        // ���յǾ �����ؾ���
                                        if (nameText.Length >= 1)
                                        {
                                            //�и��� ���¸� ���� �����
                                            nameText = nameText.Substring(0, nameText.Length - 1);
                                        }

                                        //�ϼ��� �ֱ�
                                        nameText += AddChar(temp_Chosung, temp_Joongsung, temp_Jongsung);
                                        print("case 3 ������ ����");
                                        //������ �׻� false
                                        isGyeupJaum = false;

                                        //������ ���¿��� ������ �����ٸ� ++ case 4;
                                        korCounting++;
                                    }

                                    //# ex �ƴ϶�� "��"  ���⼭ �׳� �Է»��� + ���� "��"
                                    else//�������� �ƴ϶��
                                    {
                                        //Ű�Է� �״�� �޾Ƽ�
                                        temp_Chosung = keyRaws[i].text;
                                        //�ʼ� ����
                                        nameText += temp_Chosung;

                                        //case 1 ���� �ǵ��ư���
                                        korCounting = 1;
                                        
                                        print("case 3 ������ ���ǿ��� ��� ���ο� ����");
                                    }
                                }//����������

                                else//������ ���Ǿƴ�, �� ������ �Է�������
                                {
                                    //Ű�Է� �״�� �޾Ƽ�
                                    temp_Chosung = keyRaws[i].text;
                                    //�ʼ� ����
                                    nameText += temp_Chosung;

                                    //case 1 ���� �ǵ��ư���
                                    korCounting = 1;

                                    print("case 3 �׳� ���ο� ����");
                                }

                            }//if (!isMoum)

                            //������ �����ٸ�
                            else
                            {
                                //1. ���� + ���� "�� + ���� => ����" 
                                //2. ���ħ ���� "�� + ���� => ����" 
                                //3. �ٽ� ���� �ִ� ��ġ��

                                // ���յǾ �����ؾ���
                                if (nameText.Length >= 1)
                                {
                                    //�и��� ���¸� ���� �����
                                    nameText = nameText.Substring(0, nameText.Length - 1);
                                }
                                //�ϼ��� �ֱ� 1
                                nameText += AddChar(temp_Chosung, temp_Joongsung, jongsung[0]);

                                //���� -> �ʼ��� ����
                                temp_Chosung = temp_Jongsung;
                                //�Է��� ���� �ϼ��� ������ְ� 
                                temp_Joongsung = keyRaws[i].text;
                                //�ϼ��� �ֱ� 2
                                nameText += AddChar(temp_Chosung, temp_Joongsung, jongsung[0]);

                                //case 2�� ������ ���� �Ǵ�
                                korCounting = 2;

                                print("case 3 ���� ���� �Է��� ��ħ���ڰ� �ƴ�");

                            }
                            break;

                        case 4:
                            
                            //������ �����ٸ� ���ο� �ʼ�
                            if (!isMoum)
                            {
                                //����
                                temp_Chosung = keyRaws[i].text;

                                //����
                                nameText += temp_Chosung;

                                //���� �ϳ��� �ִ� ���� case 1�� ����
                                korCounting = 1;
                            }

                            else//1a. ������ ���¿��� ������ �����ٸ�! case 3���� ���� �Ϸ� oldJongsung; newJongsung;
                            {
                                //���� �ϳ� �ٰ�
                                if (nameText.Length >= 1)
                                {
                                    //�и��� ���¸� ���� �����
                                    nameText = nameText.Substring(0, nameText.Length - 1);
                                }

                                // 1. (chosung + temp_joosung + oldJongsung)�ϼ���
                                nameText += AddChar(temp_Chosung, temp_Joongsung, temp_oldJongsung);

                                //������ ������ ��
                                temp_Joongsung = keyRaws[i].text;

                                // 2. (newJongsung + keyRaw[i].text)�ϼ���
                                nameText += AddChar(temp_newJongsung, temp_Joongsung, jongsung[0]);

                                //case 2�� ������ ���� �Ǵ�
                                korCounting = 2;

                                print("case 4 ������ + ���� = ��ħ���ڰ� �ƴ�");
                            }
                            break;
                    }//switch korCounting

                }//�ѱ��� && �����϶�
                //�ѱ�� �ƴҋ�
                if (!isKor)
                {
                    //���ĺ��� �ٷ� ���ӿ� ����
                    nameText += keyRaws[i].text;
                }
                
            }//�ִ���� ���϶��

        }//keyboard_button

        public void Check(char ch)
        {
            //������ ����
            if (0x3131 <=ch && ch <= 0x314E)
            {
                isMoum = false;
            }
            //������ ����
            if (0x314F <= ch && ch <= 0x3163)
            {
                isMoum = true;
            }
        }

        // ��ġ�� ���� �Լ�
        public string Joongsung(string prev, string now)
        {
            /*
            UInt16 temp;
            temp = 0x116A;// ���ѱ���..
            string d;
            d = Convert.ToChar(temp).ToString();
            print(d); : �� ��� ���߿� �ϼ��� ���� -1����
            */

            switch (prev)
            {
                case "��":
                    if (now == "��")
                    {
                        /* �ϼ������� ������ �´�
                        char d= Convert.ToChar(0x116A); 0x116A�� �� �ѱ�...�� -> addchar���� -1�� ���� �ȳ���
                        print(d); : ��
                        */
                        temp_JoogsungUint16 = 0x3158;
                    }
                    if (now == "��")
                    {
                        //��
                        temp_JoogsungUint16 = 0x3159;
                    }
                    if (now == "��")
                    {
                        //��
                        temp_JoogsungUint16 = 0x315A;
                    }
                    //�̿ܶ��
                    if (now != "��" && now != "��" && now != "��")
                    {
                        return now;
                    }
                    break;

                case "��":
                    if (now == "��")
                    {
                        temp_JoogsungUint16 = 0x315D;
                    }
                    if (now == "��")
                    {
                        temp_JoogsungUint16 = 0x315E;
                    }
                    if (now == "��")
                    {
                        temp_JoogsungUint16 = 0x315F;
                    }
                    //�̿ܶ��
                    if (now != "��" && now != "��" && now != "��")
                    {
                        return now;
                    }
                    break;
                case "��":
                    if (now == "��")
                    {
                        temp_JoogsungUint16 = 0x3162;
                    }
                    if (now != "��")
                    {
                        return now;
                    }
                    break;
                    
            }
            now = Convert.ToChar(temp_JoogsungUint16).ToString();
            return now;
        }

        //��ġ�� ���� �Լ�2 (���ħ ����)
        public string Jongsung(string prev, string now)
        {

            /*jongsung[]
            "", "��", "��", "��", "��", "��", "��", "��", "��","��",
            "��", "��", "��", "��", "��", "��", "��","��","��", "��",
            "��", "��", "��", "��", "��", "��", "��", "��"
            */
            //"��","��","��","��","��","��","��","��","��","��","��"
            switch (prev)
            {
                case "��":
                    if (now == "��")
                    {
                        //temp_JongsungUint16 =
                        now = jongsung[3];
                        isGyeupJaum = true;
                    }
                    break;
                case "��":
                    if (now == "��")
                    {
                        //temp_JongsungUint16 = 
                        now = jongsung[5];
                        isGyeupJaum = true;
                    }
                    if (now == "��")
                    {
                        now = jongsung[6];
                        isGyeupJaum = true;
                    }

                    if (now != "��" && now != "��")
                    {
                        return now;
                    }

                    break;
                case "��":
                    if (now == "��")
                    {
                        now = jongsung[9];
                        isGyeupJaum = true;
                    }
                    if (now == "��")
                    {
                        now = jongsung[10];
                        isGyeupJaum = true;
                    }
                    if (now == "��")
                    {
                        now = jongsung[11];
                        isGyeupJaum = true;
                    }
                    if (now == "��")
                    {
                        now = jongsung[12];
                        isGyeupJaum = true;
                    }
                    if (now == "��")
                    {
                        now = jongsung[13];
                        isGyeupJaum = true;
                    }
                    if (now == "��")
                    {
                        now = jongsung[14];
                        isGyeupJaum = true;
                    }
                    if (now == "��")
                    {
                        now = jongsung[15];
                        isGyeupJaum = true;
                    }
                    if (now != "��" && now != "��" && now != "��" && now != "��" && now != "��" && now != "��" && now != "��")
                    {
                        return now;
                    }
                    break;
                case "��":
                    if (now == "��")
                    {
                        now = jongsung[18];
                        isGyeupJaum = true;
                    }
                    else
                    {
                        return now;
                    }
                    break;
            }

            return now;
        }

        //�ϼ��� �����ڵ� �������Ʈhttp://www.unicode.org/charts/PDF/UAC00.pdf
        //��ġ�� = KorBase�����ڵ� +
        //(�ʼ� * 21 + �߼�) * 28 + ����;
        //���ڷ� ��ȯ
        public string AddChar(string cho, string joong, string jong)
        {
            //�ʼ���ġ �߼���ġ ������ġ
            int indexCho, indexjoong, indexjong, intUnicode;
            string temp;

            //Ű�Է¿��� �޾ƿ� ������ ������ �迭�� ���� ���� => �迭�� ��ġ �ε�������
            indexCho = Array.FindIndex<string>(chosung, x => x == cho);
            indexjoong = Array.FindIndex<string>(joongsung, x => x == joong);
            indexjong = Array.FindIndex<string>(jongsung, x => x == jong);

            //��ġ��ŭ ���ϱ� = �ϼ���
            intUnicode = unicode_KorBase + (indexCho * 21 + indexjoong) * 28 + indexjong;

            //��Ʈ -> ����Ʈ�� ��ȯ�ϱ� 
            Byte[] byte_CompleteMerge = BitConverter.GetBytes((short)intUnicode);

            //�ڵ尪�� ���ڷ� ��ȯ  System.Text.Unicode.GetBytes()
            temp = Encoding.Unicode.GetString(byte_CompleteMerge);

            //�׽�Ʈ�˻��
            print( "������ ������ " + temp + " �����ڵ� ��Ʈ�� " + intUnicode);
            print("�ʼ�" + cho + indexCho + "�߼�" + joong + indexjoong + "����" + jong + indexjong);

            return temp;
        }

        //shift , �ѿ� ��ȯ�� ���� �Լ�
        public void ButtonCall()
        {
            //���� �ҹ���
            if (!isKor && !isShift)
            {
                for (int i = 0; i < keyRaws.Length; i++)
                {
                    keyRaws[i].text = tempEngKeys[i];
                    keyRaws[i].text = keyRaws[i].text.ToLower();
                }
            }
            //���� �빮��
            if (!isKor && isShift)
            {
                for (int i = 0; i < keyRaws.Length; i++)
                {
                    keyRaws[i].text = tempEngKeys[i];
                    keyRaws[i].text = keyRaws[i].text.ToUpper();
                }
            }
            //�ѱ� ����Ʈx
            if (isKor && !isShift)
            {
                isConsonant = false;

                for (int i = 0; i < keyRaws.Length; i++)
                {
                    keyRaws[i].text = tempKorKeys[i];
                }
            }
            //�ѱ� ����Ʈo
            if (isKor && isShift)
            {
                isConsonant = true;
                for (int i = 0; i < keyRaws.Length; i++)
                {
                    keyRaws[i].text = tempKorKeys[i];
                }
            }
        }

        //�̸� �����̴� �Լ�
        void NameOutput()
        {
            if (!isDoneButtonClicked)
            {
                //�ð��� �帧 ����
                deltaTime += Time.deltaTime;
                //üũ�� �ð��� ����
                temp_TimeCheck = deltaTime;

                //�ִ� ���� 10���ڰ� ������
                if (isMaxLength)
                {
                    //������ ����
                    blinkText = "";
                }
                else
                {
                    //1�ʰ� ������
                    if (temp_TimeCheck >= 1f)
                    {
                        //�̸� _
                        blinkText = "_";

                        //2�ʰ� ������
                        if (temp_TimeCheck >= 2f)
                        {
                            //�̸� 
                            blinkText = "";

                            //0�ʷ� �ʱ�ȭ
                            deltaTime = 0;
                        }
                    }
                }//if(�ִ����)

                 //������Ʈ�� ����
                showMyName.text = nameText + blinkText;

            }
            else //if(isDone)���͸� ������
            {
                //�����̴� �������·�
                blinkText = "";
                //����
                showMyName.text = nameText;
            }//if(isDone)���͸� ������

            //color
            if (nameText.Length == 0)
            {
                //��Ӱ�
                showMyName.color = fontColor_default;
            }
            else
            {
                //���
                showMyName.color = fontColor_bright;
            }
        }

        //����Ʈ ���� ��ư OnOffX
        public void OnClick_KeyShift_btn()
        {
            isShift = !isShift;
        }

        //�ѿ�Ű ���� ��ư OnOff
        public void OnClick_KeyTranslate_btn()
        {
            isKor = !isKor;
            if (isKor)
            {
                myKorRullReset();
            }
        }

        // ����� ��ư
        public void OnClick_DeleteKey_btn()
        {
            //Text ������ ���� �ϳ��� �����ϱ�
            if (nameText.Length != 0)
            {
                nameText = nameText.Remove(nameText.Length - 1);

                myKorRullReset();
            }
        }

        //�����̽� ��ư
        public void OnClick_Space_btn()
        {
            //���� �Ǵ� ����
            nameText += " ";
            
            myKorRullReset();
        }

        //enter��ư �Լ�
        public void OnClick_Done_btn()
        {
            //��ư Ȱ��ȭ ����
            delete.interactable = false;
            shift.interactable = false;
            space.interactable = false;
            engKor.interactable = false;
            done.interactable = false;

            //�̸����� �ϼ�
            isDoneButtonClicked = true;

            //�̸��� �����Ѵ�. : �ٸ� �������� EnumPlayer_Information ��ũ��Ʈ�� �����߱� ������ (6�� ����)
            usingMyNewPlayer.myName.text = showMyName.text;

            notice_Text.text = "�ۼ��Ϸ�";
        }

        //�ʱ�ȭ
        public void myKorRullReset()
        {
            //�ʼ� �߼� ���� �ʱ�ȭ
            korCounting = 0;
            temp_Chosung = "";
            temp_Joongsung = "";
            temp_Jongsung = jongsung[0];
            isGyeupJaum = false;
        }

        void Update()
        {

            //�̸� �����̱�
            NameOutput();
            
            

            //shift�� �ѿ�Ű�� ���� Ű���� ��ȯ
            ButtonCall();

            //�ѱ� shift ������ ���������� �¤�
            if (isConsonant)
            {
                //��ȭ�ϴ� ��
                tempKorKeys[10] = "��";
                tempKorKeys[11] = "��";
                tempKorKeys[12] = "��";
                tempKorKeys[13] = "��";
                tempKorKeys[14] = "��";
                tempKorKeys[18] = "��";
                tempKorKeys[19] = "��";
            }
            else //����Ʈ ��
            {
                tempKorKeys[10] = "��";
                tempKorKeys[11] = "��";
                tempKorKeys[12] = "��";
                tempKorKeys[13] = "��";
                tempKorKeys[14] = "��";
                tempKorKeys[18] = "��";
                tempKorKeys[19] = "��";
            }

            //�̸��� " " �̸� ����ϴ�.  �ƹ� �۾� ������, �����̽��� �ѹ���
            if (nameText.Length != 0 && !name.Contains(" "))
            {
                    //�����̽� BTN Ȱ��ȭ ����
                    space.interactable = true;
            }
            else
            {
                space.interactable = false;
            }

            //name�� ���ڰ� 10�̻��̸� �ۼ�x
            if (nameText.Length >= 10)
            {
                //�Է��� �� �� ����.
                isMaxLength = true;
            }
            else
            {
                //�Է��� �� �� �ִ�.
                isMaxLength = false;
            }

            //2 �̻��϶� done ��ư Ȱ��ȭ
            if (nameText.Length >= 2 && !isDoneButtonClicked)
            {
                //�̸� �ߺ� �˻�
                for (int i = 0; i < saveData.gameData.myNames.Length; i++)
                {
                    //�̸��� ������ ���� �ִٸ� 
                    if (nameText == saveData.gameData.myNames[i].ToString())
                    {
                        //��Ȱ��ȭ
                        done.interactable = false;

                        notice_Text.text = "�̹� �����մϴ� �ٸ� �̸��� �Է����ּ���";
                    }
                    if(nameText != saveData.gameData.myNames[0].ToString() &&
                        nameText != saveData.gameData.myNames[1].ToString() &&
                        nameText != saveData.gameData.myNames[2].ToString() &&
                        nameText != saveData.gameData.myNames[3].ToString())//�̸��� �ٸ��ٸ�
                    {
                        //Ȱ��ȭ
                        done.interactable = true;
                    }
                }
            }
            else
            {
                done.interactable = false;
            }

        }//update
    }//class
}//namespace