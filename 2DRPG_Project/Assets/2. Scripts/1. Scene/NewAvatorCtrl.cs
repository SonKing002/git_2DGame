using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //유아이 사용하기 위함
using Main; //main
using UnityEngine.SceneManagement; //씬 메니저 사용

namespace Main
{
    //게임 시작 누른 후, 새 캐릭터를 생성할때 사용하는 함수
    //이름 정하기, 기본외형 꾸미기

    //전체 흐름제어
    public class NewAvatorCtrl : MonoBehaviour
    {
        public DataController saveData; //저장불러오기 수정
        
        public GameObject keyBoardCtrl; //키보드 받아오기 움직임
        public Transform cam;//카메라// 로테이션 -40.509 -> 0
        public Image screenDark;
        bool keyBoardFadeOut;//키보드가 사라졌는가
        bool isCameraTilting;//카메라 틸팅이 끝났는가

        public Text title; //제목
        public Text information;//진행 가이드 텍스트

        //외형선택, 컬러 선택 버튼
        public Button[] selectCanvasButtons;

        int tempIndex;

        //캐릭터가 무기를 선택했는지 확인하기 위해서 == true : 스타트 버튼 활성화
        public bool isClickReady;//준비 끝났는가 -> 업데이트에서 호출중인 버튼 제어하기 위함 (캔버스 전환할 수 있는 버튼 2개)
        public EnumPlayer_Information player; 
        public Button gameStart; // 게임 시작 버튼
        public GameObject informationView; //게임 시작버튼을 누르면 나오는 안내창
        public GameObject[] myCanvas_Dynamics;// 캔버스 두개를 활성 비활성 제어

        //완성된 게임정보를 gameData에 전달해주는 프로토콜 역할
        public Sync_DataCtrlPlayer syncData_Process;

        bool isSaveNow;// 새 캐릭터를 만들면 true로 주고, true일떄 몇가지 제어를 한 후에 로비로 돌려준다.

        private void Start()
        {
            //json에서 불러오기 (임시)
            //saveData.LoadGameData();

            //사용하는 인덱스
            tempIndex = saveData.gameData.nowUsingIndex;

            print("인덱스" + tempIndex + " " + saveData.gameData.nowUsingIndex);

            //sync로 gameData에서 player로 대입하기
            //syncData_Process.Match_SaveData_ToPlayer();

            //초기화 
            isClickReady = false;
            
            isSaveNow = false;

        }

        void Update()
        {
            NamingEND_ReadyToNEXT();

            if (isCameraTilting)
            {
                title.text = "용사님의 외형은?";
                information.text = "";
            }

            //print(isCameraTilting +" "+ cam.rotation.x);

            //무기 선택을 하면
            if (player.myHands[0].myLeftWeapon.gameObject.activeSelf)
            {
                //게임 시작 버튼 활성화
                gameStart.interactable = true;
            }
            else//무기 선택을 하지 않으면
            {
                //게임 시작 버튼 비활성화
                gameStart.interactable = false;
                isClickReady = false;
            }

        }//update

        //이름 짓기 끝난 후부터 외형 고르기 전까지의 화면 전환하는 함수
        void NamingEND_ReadyToNEXT()
        {
            //엔터를 눌렀다면 && 키보드가 사라지지 않았다면
            if (gameObject.GetComponent<KeyBoardCtrl>().isDoneButtonClicked && !keyBoardFadeOut)
            {
                //안내 문구
                title.text = "캐릭터 불러오는 중";
                information.text = "잠시만 기다려주세요";

                //아래로 움직이게 한다
                keyBoardCtrl.transform.position = Vector3.Lerp(keyBoardCtrl.transform.position, new Vector3(keyBoardCtrl.transform.position.x, -886, keyBoardCtrl.transform.position.z), Time.deltaTime * 3f);
                //블랙스크린 arpha 0
                screenDark.color = Color.Lerp(screenDark.color, new Color(0, 0, 0, 0), Time.deltaTime * 3f);

                //위치에 도달하면
                if (keyBoardCtrl.transform.position.y <= -880 || screenDark.color.a == 0)
                {
                    //키보드 사라짐
                    keyBoardFadeOut = true;
                    keyBoardCtrl.SetActive(false);
                    screenDark.gameObject.SetActive(false);
                }
            }
;
            //키보드 페이드아웃 되면
            if (keyBoardFadeOut)
            {
                //회전한다
                cam.rotation = Quaternion.Lerp(cam.rotation, new Quaternion(0, 0, 0, cam.rotation.w), Time.deltaTime * 1f);
                //print(cam.rotation.x); -0.3 ~ 0.0000001
            }
            //일정까지 내려오면
            if (cam.rotation.x >= -0.005f)
            {
                //다른 동작을 위한 true 전달;
                isCameraTilting = true;

                //계산 그만
                if (cam.rotation.x >= -0.0001f)
                {
                    cam.rotation = new Quaternion(0,0,0,cam.rotation.w);

                    if (!isClickReady)//준비완료 게임버튼을 누르지 않은 상태
                    {
                        //선택버튼 활성화
                        selectCanvasButtons[0].interactable = true;
                        selectCanvasButtons[1].interactable = true;
                    }
                    else//if(isClickReady) 준비 완료 게임버튼을 눌렀다면
                    {
                        //선택버튼 비활성화
                        selectCanvasButtons[0].interactable = false;
                        selectCanvasButtons[1].interactable = false;
                    }
                }//if 카메라회전2
            }//if 카메라회전
        }//연출 함수

        //초기화면으로 돌아가기
        public void OnClick_PrevScene_Btn()
        {
            SceneManager.LoadScene("1. Main");
        }

        //준비 완료 버튼 함수: 누른 순간부터 아무것도 클릭 못하게 막는다.
        public void OnClick_gameStart_Btn()
        {
            information.text = "모험가님, 준비 되셨나요?";

            //준비완료 bool
            isClickReady = true;
            
            //진짜 시작할건지 물어보는 창
            informationView.SetActive(true);

            //이외의 버튼들 비활성화 (업데이트 제어)

            //수정하는 캔버스 창 전부 비활성화
            myCanvas_Dynamics[0].SetActive(false);
            myCanvas_Dynamics[1].SetActive(false);
        }

        //아니오 버튼을 누르면 호출
        public void OnClick_StartNo_btn()
        {
            //준비아직 bool 
            isClickReady = false;

            //진짜 시작할건지 물어보는 창 비활성화
            informationView.SetActive(false);

            //이외의 버튼들 활성화(업데이트 제어)

            //수정하는 캔버스 창 전부 활성화
            myCanvas_Dynamics[0].SetActive(true);
            //myCanvas_Dynamics[1].SetActive(true);

        }

        //예 버튼을 누르면 호출
        public void OnClick_StartYes_btn()
        {
            //저장하기 bool 판단
            if (!isSaveNow)//저장 전이라면
            {
                //아이템 받아오기
                //syncData_Process.Item_SyncStat();

                //player[i] -> gameData 동기화 거친 후
                syncData_Process.Match_Players_ToSaveData();

                //bool : 저장 완료
                //true를 직접 넣었더니 반응을 안해서 임시 변수isSaveNow를 대입했더니, 저장할 수 있게 되었다.
                isSaveNow = true;
                
                //bool : 캐릭터를 생성완료[캐릭터 생성 전, 로비에서부터 클릭으로 넘겨받은 임시 인덱스 == 해당 캐릭터]
                saveData.gameData.isMades[syncData_Process.tempNowUsingIndex] = isSaveNow;

                //만든 캐릭터 이름을 데이더에 대입한다
                saveData.gameData.myNames[syncData_Process.tempNowUsingIndex]=
                    gameObject.GetComponent<KeyBoardCtrl>().showMyName.text;

                //gameData -> Json으로 저장 한 다음
                saveData.SaveGameData();
            }
            else//if(bool 저장이 완료되면) 로비씬으로
            {
                //씬 전환
                SceneManager.LoadScene("2. Lobby");
            }
        }
    }//class
}//namespace