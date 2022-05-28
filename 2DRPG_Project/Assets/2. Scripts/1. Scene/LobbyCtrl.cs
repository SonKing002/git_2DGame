using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //유아이 
using UnityEngine.SceneManagement; //씬 불러오기
using Main;
using TMPro;

namespace Main
{
    //json의 정보를 여기서 1,2,3,4에 로드 시킴
    //배열을 이용하며,인덱스 정보를 통해 구분
    //saveData.gameData.nowUsingIndex 불러올 캐릭터를 보여주고 사용할 수 있도록 하는 씬

    //비어있다면 새로 생성하도록 한다.
    //캐릭터가 존재한다면 유저가 선택한 캐릭터를 게임에 불러오도록 한다.

    public class LobbyCtrl : MonoBehaviour
    {
        //추후 아이디어 : 최대 4개의 캐릭터, 커서가 캐릭터 위에 있으면 정보가 뜸 (레벨, 닉네임, 스텟, 스토리 진행 단계)

        //캐릭터 창 보여주기 위한 임시
        public FormatUserCharacter[] userFormats;

        //save, load 및 저장정보 사용하는 변수
        public DataController saveData;

        public Sync_DataCtrlPlayer sync;

        [System.Serializable]
        public struct FormatUserCharacter //한 캐릭터의 정보
        {
            //캐릭터 instantiate 할 부모 오브젝트
            [SerializeField]
            public GameObject character;

            //클릭버튼 연결하기
            [SerializeField]
            public Button selectButton;

            [SerializeField]
            public EnumPlayer_Information enumPlayer_Information;

            //생성한 캐릭터가 있다면, 선택하기 //없다면, 생성하기
            [SerializeField]
            public TextMeshProUGUI myText;

            //선택될때 연출
            public Image shinnyAct;

            //능력치 정보창 띄우기
            public Text level_A;
            public Text str_A;
            public Text dex_A;
            public Text int_A;
            public Text luck_A;

            //현재 캐릭터가 선택되었는지 판단
            bool isButtonSelected;
        }

        void Start()
        {
            //SaveData == null일때  =  isMade false 캐릭터가 없을때
            //GameData [] 각각 정리
            //LobbyCtrl 캐릭터가 없다면 = 처음 , 
            //LobbyCtrl     : (불러오기) , Gamedata [전체] 정보를 캐릭터에 대입 for(), ( 저장 하기 -> 캐릭터 선택한 index 해결)
            //NewAvatorCtrl : (불러오기) , Gamedata [i] 정보를 캐릭터에 대입 ( 선택하고 게임 시작시 == 저장 )

            //불러오기
            saveData.LoadGameData();
            Check();
        }

        //캐릭터 있는지 체크
        public void Check()
        {
            //전체를 검사 : 0,1,2,3 캐릭터 생성되었는가
            for (int i = 0; i < saveData.gameData.isMades.Length; i++)
            {
                //기존 캐릭터가 없다면 (null체크용) == 신규 생성
                if (!saveData.gameData.isMades[i])//참고 : 캐릭터 생성 후 true //캐릭터 삭제시 false
                {
                    //버튼 text 문구
                    userFormats[i].myText.text = "캐릭터 생성하기";

                    //캐릭터 없음
                    userFormats[i].enumPlayer_Information.isMade = false;

                    //캐릭터 능력치 초기화
                    Characterinits(i);

                    //캐릭터를 비활성화
                    userFormats[i].character.SetActive(false);   
                }
                else//기존 불러오기
                {

                    print( "인덱스는 " + i  + "만들었는가 " + saveData.gameData.isMades[i]);

                    //문구 = 캐릭터 닉네임
                    userFormats[i].myText.text = saveData.gameData.myNames[i];

                    //캐릭터 만들어짐
                    userFormats[i].enumPlayer_Information.isMade = true;

                    //캐릭터를 활성화
                    userFormats[i].character.SetActive(true);

                    //캐릭터에게 입히기
                    Match_SaveData_ToPlayer(i);

                    //정보 불러오기 (플레이어에게 대입하는 함수)
                    //Match_SaveData_ToPlayer(i);
                }
            }
        }

        //새로 만든다면 버튼 클릭시 호출 함수
        public void New(int i) //0 1 2 3 위치
        {
            //버튼 인덱스 저장
            saveData.gameData.nowUsingIndex = i;

            saveData.SaveGameData();

            //생성씬으로
            if (!saveData.gameData.isMades[i])
            {
                //초기화 후 (재 확인)
                Characterinits(i);

                //씬 불러오기
                SceneManager.LoadScene("3. NewAvator"); 
            }
        }

        //기존 캐릭터가 존재한다면 버튼 클릭시 호출 함수
        public void Player(int i) //0 1 2 3
        {
            //버튼 인덱스 저장
            saveData.gameData.nowUsingIndex = i;
            //테스트
            print(i + "플레이어 +1");
            print(saveData.gameData.nowUsingIndex + "다이렉트 저장");

            Match_Players_ToSaveData(i);
            saveData.SaveGameData();

            //게임 씬으로
            if (saveData.gameData.isMades[i])
            {
                //우선은 마을로
                SceneManager.LoadScene("4. Home_Zone");
            }
        }

        //(정보 불러오기) 데이터에서 -> 플레이어로 정보들 넣어주기
        public void Match_SaveData_ToPlayer(int ii) //0,1,2,3
        {
            //체력 마력 경험치 레벨
            userFormats[ii].enumPlayer_Information.myHp = saveData.gameData.myHps[ii];
            userFormats[ii].enumPlayer_Information.myMp = saveData.gameData.myMps[ii];
            userFormats[ii].enumPlayer_Information.myEXP = saveData.gameData.myEXPs[ii];
            userFormats[ii].enumPlayer_Information.myLevel = saveData.gameData.myLevels[ii];

            //힘 민첩 지능 행운
            userFormats[ii].enumPlayer_Information.mySTR = saveData.gameData.mySTRs[ii];
            userFormats[ii].enumPlayer_Information.myDEX = saveData.gameData.myDEXs[ii];
            userFormats[ii].enumPlayer_Information.myINT = saveData.gameData.myINTs[ii];
            userFormats[ii].enumPlayer_Information.myLUCK = saveData.gameData.myLUCKs[ii];

            //정보창 레벨
            userFormats[ii].level_A.text = userFormats[ii].enumPlayer_Information.myLevel.ToString();

            //정보창 힘 민첩 지능 행운
            userFormats[ii].str_A.text = userFormats[ii].enumPlayer_Information.mySTR.ToString();
            userFormats[ii].dex_A.text = userFormats[ii].enumPlayer_Information.myDEX.ToString();
            userFormats[ii].int_A.text = userFormats[ii].enumPlayer_Information.myINT.ToString();
            userFormats[ii].luck_A.text = userFormats[ii].enumPlayer_Information.myLUCK.ToString();

            //코인 골드
            userFormats[ii].enumPlayer_Information.myCoin = saveData.gameData.myCoins[ii];
            userFormats[ii].enumPlayer_Information.myGold = saveData.gameData.myGolds[ii];

            //공격력, 명중률, 치명률, 방어력, 회피율
            userFormats[ii].enumPlayer_Information.myAttack = saveData.gameData.myAttacks[ii];
            userFormats[ii].enumPlayer_Information.myAcurrancy = saveData.gameData.myAcurrancies[ii];
            userFormats[ii].enumPlayer_Information.myCritical = saveData.gameData.myCriticals[ii];
            userFormats[ii].enumPlayer_Information.myDefense = saveData.gameData.myDefenses[ii];
            userFormats[ii].enumPlayer_Information.myEvationRate = saveData.gameData.myEvationRates[ii];

            //체력회복율, 마력회복율
            userFormats[ii].enumPlayer_Information.myHpCure = saveData.gameData.myHpCures[ii];
            userFormats[ii].enumPlayer_Information.myMpCure = saveData.gameData.myMpCures[ii];

            //이름, 생성여부 판단
            userFormats[ii].enumPlayer_Information.myName.text = saveData.gameData.myNames[ii];

            userFormats[ii].enumPlayer_Information.isMade = saveData.gameData.isMades[ii];

            //인종 ( 머리, 몸, 왼팔, 오른팔, 왼다리, 오른다리, 헤어 )
            userFormats[ii].enumPlayer_Information.head.sprite = saveData.gameData.heads[ii];
            userFormats[ii].enumPlayer_Information.body.sprite = saveData.gameData.bodies[ii];
            userFormats[ii].enumPlayer_Information.leftArm.sprite = saveData.gameData.leftArms[ii];
            userFormats[ii].enumPlayer_Information.rightArm.sprite = saveData.gameData.rightArms[ii];
            userFormats[ii].enumPlayer_Information.leftLeg.sprite = saveData.gameData.leftLegs[ii];
            userFormats[ii].enumPlayer_Information.rightArm.sprite = saveData.gameData.rightArms[ii];

            //헤어가 활성화 되어있다면
            if (saveData.gameData.isHairTrue[ii])
            {
                userFormats[ii].enumPlayer_Information.myHair.gameObject.SetActive(true);
                userFormats[ii].enumPlayer_Information.myHair.sprite = saveData.gameData.myHairs[ii];
            }
            else //if (!saveData.gameData.isHairTrue[ii]) : 헤어가 없다면
            {
                userFormats[ii].enumPlayer_Information.myHair.gameObject.SetActive(false);
            }
            

            //눈관련 ( 왼쪽 아이라인, 오른쪽 아이라인, 왼쪽 눈동자, 오른쪽 눈동자 )
            userFormats[ii].enumPlayer_Information.myEyes[0].myLeftBackEye.sprite = saveData.gameData.myLeftBackEyes[ii];
            userFormats[ii].enumPlayer_Information.myEyes[0].myRightBackEye.sprite = saveData.gameData.myRightBackEyes[ii];
            userFormats[ii].enumPlayer_Information.myEyes[0].myLeftFrontEye.sprite = saveData.gameData.myLeftFrontEyes[ii];
            userFormats[ii].enumPlayer_Information.myEyes[0].myRightFrontEye.sprite = saveData.gameData.myRightFrontEyes[ii];

            //수염
            userFormats[ii].enumPlayer_Information.myFaceHair.sprite = saveData.gameData.myFaceHairs[ii];

            //옷을 입고 있다면
            if (saveData.gameData.isClothTrue[ii])
            {
                userFormats[ii].enumPlayer_Information.myCloths[0].myBodyCloth.gameObject.SetActive(true);
                userFormats[ii].enumPlayer_Information.myCloths[0].myLeftClothArm.gameObject.SetActive(true);
                userFormats[ii].enumPlayer_Information.myCloths[0].myRightClothArm.gameObject.SetActive(true);

                //윗옷 ( 몸통, 왼팔, 오른팔 )
                userFormats[ii].enumPlayer_Information.myCloths[0].myBodyCloth.sprite = saveData.gameData.myBodyCloths[ii];
                userFormats[ii].enumPlayer_Information.myCloths[0].myLeftClothArm.sprite = saveData.gameData.myLeftClothArms[ii];
                userFormats[ii].enumPlayer_Information.myCloths[0].myRightClothArm.sprite = saveData.gameData.myRightClothArms[ii];
            }
            else //if(!saveData.gameData.isClothTrue[ii])
            {
                userFormats[ii].enumPlayer_Information.myCloths[0].myBodyCloth.gameObject.SetActive(false);
                userFormats[ii].enumPlayer_Information.myCloths[0].myLeftClothArm.gameObject.SetActive(false);
                userFormats[ii].enumPlayer_Information.myCloths[0].myRightClothArm.gameObject.SetActive(false);
            }

            //바지를 입고 있다면
            if (saveData.gameData.isPantsTrue[ii])
            {
                userFormats[ii].enumPlayer_Information.myLegs[0].myLeftLeg.gameObject.SetActive(true);
                userFormats[ii].enumPlayer_Information.myLegs[0].myRightLeg.gameObject.SetActive(true);

                //바지 (왼다리, 오른다리)
                userFormats[ii].enumPlayer_Information.myLegs[0].myLeftLeg.sprite = saveData.gameData.myleftPantsLegs[ii];
                userFormats[ii].enumPlayer_Information.myLegs[0].myRightLeg.sprite = saveData.gameData.myRightPantsLegs[ii];
            }
            else
            {
                userFormats[ii].enumPlayer_Information.myLegs[0].myLeftLeg.gameObject.SetActive(false);
                userFormats[ii].enumPlayer_Information.myLegs[0].myRightLeg.gameObject.SetActive(false);
            }

            //망또를 두르고 있다면
            if (saveData.gameData.isBackTrue[ii])
            {
                userFormats[ii].enumPlayer_Information.back.gameObject.SetActive(true);

                //망또
                userFormats[ii].enumPlayer_Information.back.sprite = saveData.gameData.backs[ii];
            }
            else
            {
                userFormats[ii].enumPlayer_Information.back.gameObject.SetActive(false);
            }

            //갑옷을 착용하고 있다면
            if (saveData.gameData.isArmorTrue[ii])
            {
                userFormats[ii].enumPlayer_Information.myArmor[0].myBody.gameObject.SetActive(true);
                userFormats[ii].enumPlayer_Information.myArmor[0].myLeftArm.gameObject.SetActive(true);
                userFormats[ii].enumPlayer_Information.myArmor[0].myRightArm.gameObject.SetActive(true);

                //갑옷 (몸통, 왼어깨, 오른어깨)
                userFormats[ii].enumPlayer_Information.myArmor[0].myBody.sprite = saveData.gameData.myBodyArmores[ii];
                userFormats[ii].enumPlayer_Information.myArmor[0].myLeftArm.sprite = saveData.gameData.myLeftShoulderArmers[ii];
                userFormats[ii].enumPlayer_Information.myArmor[0].myRightArm.sprite = saveData.gameData.myRightShoulderArmers[ii];
            }
            else
            {
                userFormats[ii].enumPlayer_Information.myArmor[0].myBody.gameObject.SetActive(false);
                userFormats[ii].enumPlayer_Information.myArmor[0].myLeftArm.gameObject.SetActive(false);
                userFormats[ii].enumPlayer_Information.myArmor[0].myRightArm.gameObject.SetActive(false);
            }

            //왼 손에 쥐고 있다면
            if (saveData.gameData.isLeftHandTrue[ii])
            {
                userFormats[ii].enumPlayer_Information.myHands[0].myLeftWeapon.gameObject.SetActive(true);

                //손 장착(왼손)
                userFormats[ii].enumPlayer_Information.myHands[0].myLeftWeapon.sprite = saveData.gameData.myLeftWeapons[ii];
                //userFormats[ii].enumPlayer_Information.myHands[0].myLeftShield.sprite = saveData.gameData.myLeftShields[ii];
            }
            else
            {
                userFormats[ii].enumPlayer_Information.myHands[0].myLeftWeapon.gameObject.SetActive(false);
            }

            //오른 손에 쥐고 있다면
            if (saveData.gameData.isWriteHandTrue[ii])
            {
                userFormats[ii].enumPlayer_Information.myHands[0].myRightShield.gameObject.SetActive(true);

                //손 장착(오른손)
                //userFormats[ii].enumPlayer_Information.myHands[0].myRightWeapon.sprite = saveData.gameData.myRightWeapons[ii];
                userFormats[ii].enumPlayer_Information.myHands[0].myRightShield.sprite = saveData.gameData.myRightShields[ii];
            }
            else
            {
                userFormats[ii].enumPlayer_Information.myHands[0].myRightShield.gameObject.SetActive(false);
            }
            
            //색상 (2/17 추가)
            //색상 백업 //헤어
            userFormats[ii].enumPlayer_Information.myHairColor = saveData.gameData.myHairColors[ii];
            //색상 대입
            userFormats[ii].enumPlayer_Information.myHair.color = userFormats[ii].enumPlayer_Information.myHairColor;

            //색상 백업 //왼눈
            userFormats[ii].enumPlayer_Information.myLeftEyeColor = saveData.gameData.myLeftFrontEyeColors[ii];
            //색상 대입
            userFormats[ii].enumPlayer_Information.myEyes[0].myLeftFrontEye.color = userFormats[ii].enumPlayer_Information.myLeftEyeColor;

            //색상 백업 //오른눈
            userFormats[ii].enumPlayer_Information.myRightEyeColor = saveData.gameData.myRightFrontEyeColors[ii];
            //색상 대입
            userFormats[ii].enumPlayer_Information.myEyes[0].myRightFrontEye.color = userFormats[ii].enumPlayer_Information.myRightEyeColor;
        }

        //(정보 불러오기) 플레이어에서 -> 데이터로 정보들 넣어주기
        public void Match_Players_ToSaveData(int ii) //0,1,2,3
        {
            //체력 마력 경험치 레벨
            saveData.gameData.myHps[ii] = userFormats[ii].enumPlayer_Information.myHp;
            saveData.gameData.myMps[ii] = userFormats[ii].enumPlayer_Information.myMp;
            saveData.gameData.myEXPs[ii] = userFormats[ii].enumPlayer_Information.myEXP;
            saveData.gameData.myLevels[ii] = userFormats[ii].enumPlayer_Information.myLevel;

            //힘 민첩 지능 행운
            saveData.gameData.mySTRs[ii] = userFormats[ii].enumPlayer_Information.mySTR;
            saveData.gameData.myDEXs[ii] = userFormats[ii].enumPlayer_Information.myDEX;
            saveData.gameData.myINTs[ii] = userFormats[ii].enumPlayer_Information.myINT;
            saveData.gameData.myLUCKs[ii] = userFormats[ii].enumPlayer_Information.myLUCK;

            //코인 골드
            saveData.gameData.myCoins[ii] = userFormats[ii].enumPlayer_Information.myCoin;
            saveData.gameData.myGolds[ii] = userFormats[ii].enumPlayer_Information.myGold;

            //공격력, 명중률, 치명률, 방어력, 회피율
            saveData.gameData.myAttacks[ii] = userFormats[ii].enumPlayer_Information.myAttack;
            saveData.gameData.myAcurrancies[ii] = userFormats[ii].enumPlayer_Information.myAcurrancy;
            saveData.gameData.myCriticals[ii] = userFormats[ii].enumPlayer_Information.myCritical;
            saveData.gameData.myDefenses[ii] = userFormats[ii].enumPlayer_Information.myDefense;
            saveData.gameData.myEvationRates[ii] = userFormats[ii].enumPlayer_Information.myEvationRate;

            //체력회복율, 마력회복율
            saveData.gameData.myHpCures[ii] = userFormats[ii].enumPlayer_Information.myHpCure;
            saveData.gameData.myMpCures[ii] = userFormats[ii].enumPlayer_Information.myMpCure;

            //이름, 생성여부 판단
            saveData.gameData.myNames[ii] = userFormats[ii].enumPlayer_Information.myName.text.ToString();
            saveData.gameData.isMades[ii] = userFormats[ii].enumPlayer_Information.isMade;

            //인종 ( 머리, 몸, 왼팔, 오른팔, 왼다리, 오른다리, 헤어 )
            saveData.gameData.heads[ii] = userFormats[ii].enumPlayer_Information.head.sprite;
            saveData.gameData.bodies[ii] = userFormats[ii].enumPlayer_Information.body.sprite;
            saveData.gameData.leftArms[ii] = userFormats[ii].enumPlayer_Information.leftArm.sprite;
            saveData.gameData.rightArms[ii] = userFormats[ii].enumPlayer_Information.rightArm.sprite;
            saveData.gameData.leftLegs[ii] = userFormats[ii].enumPlayer_Information.leftLeg.sprite;
            saveData.gameData.rightArms[ii] = userFormats[ii].enumPlayer_Information.rightArm.sprite;
            saveData.gameData.myHairs[ii] = userFormats[ii].enumPlayer_Information.myHair.sprite;

            //눈관련 ( 왼쪽 아이라인, 오른쪽 아이라인, 왼쪽 눈동자, 오른쪽 눈동자 )
            saveData.gameData.myLeftBackEyes[ii] = userFormats[ii].enumPlayer_Information.myEyes[0].myLeftBackEye.sprite;
            saveData.gameData.myRightBackEyes[ii] = userFormats[ii].enumPlayer_Information.myEyes[0].myRightBackEye.sprite;
            saveData.gameData.myLeftFrontEyes[ii] = userFormats[ii].enumPlayer_Information.myEyes[0].myLeftFrontEye.sprite;
            saveData.gameData.myRightFrontEyes[ii] = userFormats[ii].enumPlayer_Information.myEyes[0].myRightFrontEye.sprite;

            //수염
            saveData.gameData.myFaceHairs[ii] = userFormats[ii].enumPlayer_Information.myFaceHair.sprite;

            //윗옷 ( 몸통, 왼팔, 오른팔 )
            saveData.gameData.myBodyCloths[ii] = userFormats[ii].enumPlayer_Information.myCloths[0].myBodyCloth.sprite;
            saveData.gameData.myLeftClothArms[ii] = userFormats[ii].enumPlayer_Information.myCloths[0].myLeftClothArm.sprite;
            saveData.gameData.myRightClothArms[ii] = userFormats[ii].enumPlayer_Information.myCloths[0].myRightClothArm.sprite;

            //바지 (왼다리, 오른다리)
            saveData.gameData.myleftPantsLegs[ii] = userFormats[ii].enumPlayer_Information.myLegs[0].myLeftLeg.sprite;
            saveData.gameData.myRightPantsLegs[ii] = userFormats[ii].enumPlayer_Information.myLegs[0].myRightLeg.sprite;

            //망또
            saveData.gameData.backs[ii] = userFormats[ii].enumPlayer_Information.back.sprite;

            //갑옷 (몸통, 왼어깨, 오른어깨)
            saveData.gameData.myBodyArmores[ii] = userFormats[ii].enumPlayer_Information.myArmor[0].myBody.sprite;
            saveData.gameData.myLeftShoulderArmers[ii] = userFormats[ii].enumPlayer_Information.myArmor[0].myLeftArm.sprite;
            saveData.gameData.myRightShoulderArmers[ii] = userFormats[ii].enumPlayer_Information.myArmor[0].myRightArm.sprite;

            //손 장착(오른손 왼손)
            saveData.gameData.myLeftWeapons[ii] = userFormats[ii].enumPlayer_Information.myHands[0].myLeftWeapon.sprite;
            saveData.gameData.myRightWeapons[ii] = userFormats[ii].enumPlayer_Information.myHands[0].myRightWeapon.sprite;
            saveData.gameData.myLeftShields[ii] = userFormats[ii].enumPlayer_Information.myHands[0].myLeftShield.sprite;
            saveData.gameData.myRightShields[ii] = userFormats[ii].enumPlayer_Information.myHands[0].myRightShield.sprite;

            //색상 (2/17 추가)
            saveData.gameData.myHairColors[ii] = userFormats[ii].enumPlayer_Information.myHairColor;
            saveData.gameData.myLeftFrontEyeColors[ii] = userFormats[ii].enumPlayer_Information.myLeftEyeColor;
            saveData.gameData.myRightFrontEyeColors[ii] = userFormats[ii].enumPlayer_Information.myRightEyeColor;
        }

        //선택 초기화 버튼
        public void OnClick_CharacterInitialze_Btn(int i) //캐릭터 0, 1, 2, 3
        {
            //임시 인덱스
            int tempint = i;

            //캐릭터 능력치 초기화
            Characterinits(tempint);

            //다시 체크
            Check();
        }


        //초기화 버튼
        public void OnClick_AllInitialze_Btn()
        {
            //json 파일 자체 삭제
            saveData.DataDelete();

            //4캐릭터 전부 초기화
            for (int i = 0; i < userFormats.Length; i++)
            {
                //캐릭터 능력치 초기화
                Characterinits(i);
            }

            //다시 체크
            Check();
        }

        //여러 곳에서 참조하고 있기 때문에 호출 함수로 작성
        public void Characterinits(int i)
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

            /*
            //윗옷 ( 몸통, 왼팔, 오른팔 )
            saveData.gameData.myBodyCloths[tempNowUsingIndex];
            saveData.gameData.myLeftClothArms[tempNowUsingIndex];
            saveData.gameData.myRightClothArms[tempNowUsingIndex];

            //바지 (왼다리, 오른다리)
            saveData.gameData.myleftPantsLegs[tempNowUsingIndex];
            saveData.gameData.myRightPantsLegs[tempNowUsingIndex];

            //갑옷 (몸통, 왼어깨, 오른어깨)
            saveData.gameData.myBodyArmores[tempNowUsingIndex];
            saveData.gameData.myLeftShoulderArmers[tempNowUsingIndex];
            saveData.gameData.myRightShoulderArmers[tempNowUsingIndex];

            //망또
            saveData.gameData.backs[tempNowUsingIndex];

            //손 장착(오른손 왼손)
            saveData.gameData.myLeftWeapons[tempNowUsingIndex];
            saveData.gameData.myRightWeapons[tempNowUsingIndex];
            saveData.gameData.myLeftShields[tempNowUsingIndex];
            saveData.gameData.myRightShields[tempNowUsingIndex];


            //투구 비활성화
            saveData.gameData.isHelmetTrue[tempNowUsingIndex] = false;
            //갑옷 비활성화
            saveData.gameData.isArmorTrue[tempNowUsingIndex] = false;
            //망또 비활성화
            saveData.gameData.isBackTrue[tempNowUsingIndex] = false;
            //방패 비활성화
            saveData.gameData.isWriteHandTrue[tempNowUsingIndex] = false;
            //무기 비활성화
            saveData.gameData.isLeftHandTrue[tempNowUsingIndex] = false;
            */

        }

    }//class
}//namespace