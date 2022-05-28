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

        //로드하기
        public void OnClick_LoadCharacter_Btn()
        {
            //만약 지금 마음에 들지 않을 수도 있기 때문에 따로 저장하지 않는다.
            //시간 풀기
            isTimeToWork = true;
            //로비로 돌아가기
            isGoLobby = true;
        }

        //삭제하기
        public void OnClick_Remove_GoHome_Btn()
        {
            //삭제하기
            saveData.DataDelete();

            //임시 인덱스
            int tempint = syncData.tempNowUsingIndex;

            //캐릭터 능력치 초기화
            Characterinits(tempint);

            //빈파일 
            OnClick_Save_Btn();

            //시간풀기
            isTimeToWork = true;
            //메인으로 돌아가기
            isGoMain = true;
        }

        //초기화 참조
        void Characterinits(int i)
        {
            //체력 마력 경험치 레벨
            saveData.gameData.myHps[i] = 100;
            saveData.gameData.myMps[i] = 100;
            saveData.gameData.myEXPs[i] = 0;
            saveData.gameData.myLevels[i] = 1;

            //힘 민첩 지능 행운
            saveData.gameData.mySTRs[i] = 4;
            saveData.gameData.myDEXs[i] = 4;
            saveData.gameData.myINTs[i] = 4;
            saveData.gameData.myLUCKs[i] = 4;

            //코인 골드
            saveData.gameData.myCoins[i] = 100;
            saveData.gameData.myGolds[i] = 100;

            //공격력, 명중률, 치명률, 방어력, 회피율
            saveData.gameData.myAttacks[i] = 0;
            saveData.gameData.myAcurrancies[i] = 0;
            saveData.gameData.myCriticals[i] = 0;
            saveData.gameData.myDefenses[i] = 0;
            saveData.gameData.myEvationRates[i] = 0;

            //체력회복율, 마력회복율
            saveData.gameData.myHpCures[i] = 0;
            saveData.gameData.myMpCures[i] = 0;

            //이름, 생성여부 판단
            saveData.gameData.myNames[i] = "";
            saveData.gameData.isMades[i] = false;

        }

        //저장하기
        public void OnClick_Save_Btn()
        {
            //(입고 있는 옷,외형)플레이어 -> gameData
            syncData.Match_Players_ToSaveData();
            //(저장)gameData -> json
            saveData.SaveGameData();
        }

        
        //홈으로 돌아가기 버튼
        public void OnClick_ToHome_Btn()
        {
            //시간 풀기
            isTimeToWork = true;
            //홈으로 돌아가기
            isGoMain = true;
        }

        public void OnClick_StartMission_Btn(int i)
        {
            switch (i)
            {
                //1스테이지
                case 0:
                    SceneManager.LoadScene("5. Stage1");
                    break;
                //2스테이지
                case 1:
                    //아직
                    break;
                //3스테이지
                case 2:
                    break;
                //4스테이지
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
