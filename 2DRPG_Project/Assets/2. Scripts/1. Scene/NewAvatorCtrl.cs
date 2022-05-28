using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //������ ����ϱ� ����
using Main; //main
using UnityEngine.SceneManagement; //�� �޴��� ���

namespace Main
{
    //���� ���� ���� ��, �� ĳ���͸� �����Ҷ� ����ϴ� �Լ�
    //�̸� ���ϱ�, �⺻���� �ٹ̱�

    //��ü �帧����
    public class NewAvatorCtrl : MonoBehaviour
    {
        public DataController saveData; //����ҷ����� ����
        
        public GameObject keyBoardCtrl; //Ű���� �޾ƿ��� ������
        public Transform cam;//ī�޶�// �����̼� -40.509 -> 0
        public Image screenDark;
        bool keyBoardFadeOut;//Ű���尡 ������°�
        bool isCameraTilting;//ī�޶� ƿ���� �����°�

        public Text title; //����
        public Text information;//���� ���̵� �ؽ�Ʈ

        //��������, �÷� ���� ��ư
        public Button[] selectCanvasButtons;

        int tempIndex;

        //ĳ���Ͱ� ���⸦ �����ߴ��� Ȯ���ϱ� ���ؼ� == true : ��ŸƮ ��ư Ȱ��ȭ
        public bool isClickReady;//�غ� �����°� -> ������Ʈ���� ȣ������ ��ư �����ϱ� ���� (ĵ���� ��ȯ�� �� �ִ� ��ư 2��)
        public EnumPlayer_Information player; 
        public Button gameStart; // ���� ���� ��ư
        public GameObject informationView; //���� ���۹�ư�� ������ ������ �ȳ�â
        public GameObject[] myCanvas_Dynamics;// ĵ���� �ΰ��� Ȱ�� ��Ȱ�� ����

        //�ϼ��� ���������� gameData�� �������ִ� �������� ����
        public Sync_DataCtrlPlayer syncData_Process;

        bool isSaveNow;// �� ĳ���͸� ����� true�� �ְ�, true�ϋ� ��� ��� �� �Ŀ� �κ�� �����ش�.

        private void Start()
        {
            //json���� �ҷ����� (�ӽ�)
            //saveData.LoadGameData();

            //����ϴ� �ε���
            tempIndex = saveData.gameData.nowUsingIndex;

            print("�ε���" + tempIndex + " " + saveData.gameData.nowUsingIndex);

            //sync�� gameData���� player�� �����ϱ�
            //syncData_Process.Match_SaveData_ToPlayer();

            //�ʱ�ȭ 
            isClickReady = false;
            
            isSaveNow = false;

        }

        void Update()
        {
            NamingEND_ReadyToNEXT();

            if (isCameraTilting)
            {
                title.text = "������ ������?";
                information.text = "";
            }

            //print(isCameraTilting +" "+ cam.rotation.x);

            //���� ������ �ϸ�
            if (player.myHands[0].myLeftWeapon.gameObject.activeSelf)
            {
                //���� ���� ��ư Ȱ��ȭ
                gameStart.interactable = true;
            }
            else//���� ������ ���� ������
            {
                //���� ���� ��ư ��Ȱ��ȭ
                gameStart.interactable = false;
                isClickReady = false;
            }

        }//update

        //�̸� ���� ���� �ĺ��� ���� ���� �������� ȭ�� ��ȯ�ϴ� �Լ�
        void NamingEND_ReadyToNEXT()
        {
            //���͸� �����ٸ� && Ű���尡 ������� �ʾҴٸ�
            if (gameObject.GetComponent<KeyBoardCtrl>().isDoneButtonClicked && !keyBoardFadeOut)
            {
                //�ȳ� ����
                title.text = "ĳ���� �ҷ����� ��";
                information.text = "��ø� ��ٷ��ּ���";

                //�Ʒ��� �����̰� �Ѵ�
                keyBoardCtrl.transform.position = Vector3.Lerp(keyBoardCtrl.transform.position, new Vector3(keyBoardCtrl.transform.position.x, -886, keyBoardCtrl.transform.position.z), Time.deltaTime * 3f);
                //����ũ�� arpha 0
                screenDark.color = Color.Lerp(screenDark.color, new Color(0, 0, 0, 0), Time.deltaTime * 3f);

                //��ġ�� �����ϸ�
                if (keyBoardCtrl.transform.position.y <= -880 || screenDark.color.a == 0)
                {
                    //Ű���� �����
                    keyBoardFadeOut = true;
                    keyBoardCtrl.SetActive(false);
                    screenDark.gameObject.SetActive(false);
                }
            }
;
            //Ű���� ���̵�ƿ� �Ǹ�
            if (keyBoardFadeOut)
            {
                //ȸ���Ѵ�
                cam.rotation = Quaternion.Lerp(cam.rotation, new Quaternion(0, 0, 0, cam.rotation.w), Time.deltaTime * 1f);
                //print(cam.rotation.x); -0.3 ~ 0.0000001
            }
            //�������� ��������
            if (cam.rotation.x >= -0.005f)
            {
                //�ٸ� ������ ���� true ����;
                isCameraTilting = true;

                //��� �׸�
                if (cam.rotation.x >= -0.0001f)
                {
                    cam.rotation = new Quaternion(0,0,0,cam.rotation.w);

                    if (!isClickReady)//�غ�Ϸ� ���ӹ�ư�� ������ ���� ����
                    {
                        //���ù�ư Ȱ��ȭ
                        selectCanvasButtons[0].interactable = true;
                        selectCanvasButtons[1].interactable = true;
                    }
                    else//if(isClickReady) �غ� �Ϸ� ���ӹ�ư�� �����ٸ�
                    {
                        //���ù�ư ��Ȱ��ȭ
                        selectCanvasButtons[0].interactable = false;
                        selectCanvasButtons[1].interactable = false;
                    }
                }//if ī�޶�ȸ��2
            }//if ī�޶�ȸ��
        }//���� �Լ�

        //�ʱ�ȭ������ ���ư���
        public void OnClick_PrevScene_Btn()
        {
            SceneManager.LoadScene("1. Main");
        }

        //�غ� �Ϸ� ��ư �Լ�: ���� �������� �ƹ��͵� Ŭ�� ���ϰ� ���´�.
        public void OnClick_gameStart_Btn()
        {
            information.text = "���谡��, �غ� �Ǽ̳���?";

            //�غ�Ϸ� bool
            isClickReady = true;
            
            //��¥ �����Ұ��� ����� â
            informationView.SetActive(true);

            //�̿��� ��ư�� ��Ȱ��ȭ (������Ʈ ����)

            //�����ϴ� ĵ���� â ���� ��Ȱ��ȭ
            myCanvas_Dynamics[0].SetActive(false);
            myCanvas_Dynamics[1].SetActive(false);
        }

        //�ƴϿ� ��ư�� ������ ȣ��
        public void OnClick_StartNo_btn()
        {
            //�غ���� bool 
            isClickReady = false;

            //��¥ �����Ұ��� ����� â ��Ȱ��ȭ
            informationView.SetActive(false);

            //�̿��� ��ư�� Ȱ��ȭ(������Ʈ ����)

            //�����ϴ� ĵ���� â ���� Ȱ��ȭ
            myCanvas_Dynamics[0].SetActive(true);
            //myCanvas_Dynamics[1].SetActive(true);

        }

        //�� ��ư�� ������ ȣ��
        public void OnClick_StartYes_btn()
        {
            //�����ϱ� bool �Ǵ�
            if (!isSaveNow)//���� ���̶��
            {
                //������ �޾ƿ���
                //syncData_Process.Item_SyncStat();

                //player[i] -> gameData ����ȭ ��ģ ��
                syncData_Process.Match_Players_ToSaveData();

                //bool : ���� �Ϸ�
                //true�� ���� �־����� ������ ���ؼ� �ӽ� ����isSaveNow�� �����ߴ���, ������ �� �ְ� �Ǿ���.
                isSaveNow = true;
                
                //bool : ĳ���͸� �����Ϸ�[ĳ���� ���� ��, �κ񿡼����� Ŭ������ �Ѱܹ��� �ӽ� �ε��� == �ش� ĳ����]
                saveData.gameData.isMades[syncData_Process.tempNowUsingIndex] = isSaveNow;

                //���� ĳ���� �̸��� ���̴��� �����Ѵ�
                saveData.gameData.myNames[syncData_Process.tempNowUsingIndex]=
                    gameObject.GetComponent<KeyBoardCtrl>().showMyName.text;

                //gameData -> Json���� ���� �� ����
                saveData.SaveGameData();
            }
            else//if(bool ������ �Ϸ�Ǹ�) �κ������
            {
                //�� ��ȯ
                SceneManager.LoadScene("2. Lobby");
            }
        }
    }//class
}//namespace