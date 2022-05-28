using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;
using UnityEngine.SceneManagement;

namespace Main
{
    public class InGameCtrl : MonoBehaviour
    {
        
        public DataController saveData;
        public Sync_DataCtrlPlayer syncData;
        public bool isTimeToWork;

        public bool isGoLobby;
        public bool isGoMain;

        //�ε��ϱ�
        public void OnClick_LoadCharacter_Btn()
        {
            //���� ���� ������ ���� ���� ���� �ֱ� ������ ���� �������� �ʴ´�.
            //�ð� Ǯ��
            isTimeToWork = true;
            //�κ�� ���ư���
            isGoLobby = true;
        }

        //�����ϱ�
        public void OnClick_Remove_GoHome_Btn()
        {
            //�����ϱ�
            saveData.DataDelete();

            //�ӽ� �ε���
            int tempint = syncData.tempNowUsingIndex;

            //ĳ���� �ɷ�ġ �ʱ�ȭ
            Characterinits(tempint);

            //������ 
            OnClick_Save_Btn();

            //�ð�Ǯ��
            isTimeToWork = true;
            //�������� ���ư���
            isGoMain = true;
        }

        //�ʱ�ȭ ����
        void Characterinits(int i)
        {
            //ü�� ���� ����ġ ����
            saveData.gameData.myHps[i] = 100;
            saveData.gameData.myMps[i] = 100;
            saveData.gameData.myEXPs[i] = 0;
            saveData.gameData.myLevels[i] = 1;

            //�� ��ø ���� ���
            saveData.gameData.mySTRs[i] = 4;
            saveData.gameData.myDEXs[i] = 4;
            saveData.gameData.myINTs[i] = 4;
            saveData.gameData.myLUCKs[i] = 4;

            //���� ���
            saveData.gameData.myCoins[i] = 100;
            saveData.gameData.myGolds[i] = 100;

            //���ݷ�, ���߷�, ġ���, ����, ȸ����
            saveData.gameData.myAttacks[i] = 0;
            saveData.gameData.myAcurrancies[i] = 0;
            saveData.gameData.myCriticals[i] = 0;
            saveData.gameData.myDefenses[i] = 0;
            saveData.gameData.myEvationRates[i] = 0;

            //ü��ȸ����, ����ȸ����
            saveData.gameData.myHpCures[i] = 0;
            saveData.gameData.myMpCures[i] = 0;

            //�̸�, �������� �Ǵ�
            saveData.gameData.myNames[i] = "";
            saveData.gameData.isMades[i] = false;

        }

        //�����ϱ�
        public void OnClick_Save_Btn()
        {
            //(�԰� �ִ� ��,����)�÷��̾� -> gameData
            syncData.Match_Players_ToSaveData();
            //(����)gameData -> json
            saveData.SaveGameData();
        }

        
        //Ȩ���� ���ư��� ��ư
        public void OnClick_ToHome_Btn()
        {
            //�ð� Ǯ��
            isTimeToWork = true;
            //Ȩ���� ���ư���
            isGoMain = true;
        }

        public void OnClick_StartMission_Btn(int i)
        {
            switch (i)
            {
                //1��������
                case 0:
                    SceneManager.LoadScene("5. Stage1");
                    break;
                //2��������
                case 1:
                    //����
                    break;
                //3��������
                case 2:
                    break;
                //4��������
                case 3:
                    break;
            }
        }

        public void Update()
        {
            if (isTimeToWork)
            {
                Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, 4f);

                isTimeToWork = false;
            }

            if (Time.timeScale == 1f && isGoLobby)
            {
                isGoLobby = false;
                SceneManager.LoadScene("2. Lobby");
            }

            if (Time.timeScale == 1f && isGoMain)
            {
                isGoMain = false;
                SceneManager.LoadScene("1. Main");
            }
        }
    }//class
}//namespace
