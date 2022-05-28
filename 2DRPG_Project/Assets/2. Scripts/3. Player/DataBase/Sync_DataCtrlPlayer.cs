using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;

using System.Linq;

namespace Main
{
    //json <-> 캐릭터[인덱스] 정보
    //저장과 불러오기 참조형으로 사용하기 좋게 작성해두는 스크립트

    //(외형 관련하여 아이템을 선택하고 캐릭터에 입히기 까지 가능 : 저장은 불가능하기 때문에)
    //저장이 필요한 시점에서 스크립트를 호출할 수 있도록 할 것

    // 추가 아이템 착용 변경에 따른 능력치 변화 : Update 처리
    // 싱크 (외형아이템이 어떤것인지 체크한 후 능력치까지 플레이어한테 대입)
    public class Sync_DataCtrlPlayer : MonoBehaviour
    {
        //타 스크립트
        public EnumPlayer_Information myPlayer;
        public DataController saveData;

        //추가 싱크 (아이템 관련 스크립트)
        public Item equiptable_Items;

        public ChangeCtrl changeCtrl;//무기 콜라이더 동기화하기 위함

        //옷, 바지, 투구, 갑옷, 망또, 방패, 무기, (목걸이, 반지,) 벨트, 크로스 띠

        //파츠를 착용하고 있는지 확인하기 위함
        //다시public bool[] isEquipOnce = new bool[9]; //각 착용여부에 따라서 착용해제하면 착용 아이템만큼 능력치를 낮춘다.

        //파츠를 바꿀때 딱 한번만 update에서 실행함
        //다시public bool[] isChanged = new bool[9];

        //public int tempIndex_For; //for 문의 i 를 어떤 함수의 매개변수로 사용하기 위함

        //각 파츠의 인덱스를 부여 == 찾기 쉽게하기 위함
        //public int[] tempItemList_Index = new int[9];
        //public int[] tempPrevList_Index = new int[9];

        //사용하고 있는 캐릭터 
        public int tempNowUsingIndex;


        void Start()
        {
            //사용하고 있는 캐릭터 인덱스 동기화
            tempNowUsingIndex = saveData.gameData.nowUsingIndex;
            //신규 생성이라면 그냥 현재 이미지를 저장하는걸로 마무리
            //NewAvatorCtrl에서 isSaveNow bool형이 true 일때
            //(나중에 아이템 슬롯 방식 결정나면 그곳에 추가하는 걸로) 고른 이미지의 아이템을 받아온다.

        }

        //(정보 불러오기) 데이터에서 -> 플레이어로 정보들 넣어주기
        public void Match_SaveData_ToPlayer() //0,1,2,3
        {
            //존재하는 캐릭터라면
            if (saveData.gameData.isMades[tempNowUsingIndex])
            {
                //체력 마력 경험치 레벨
                myPlayer.myHp = saveData.gameData.myHps[tempNowUsingIndex];
                myPlayer.myMp = saveData.gameData.myMps[tempNowUsingIndex];
                myPlayer.myEXP = saveData.gameData.myEXPs[tempNowUsingIndex];
                myPlayer.myLevel = saveData.gameData.myLevels[tempNowUsingIndex];

                //힘 민첩 지능 행운
                myPlayer.mySTR = saveData.gameData.mySTRs[tempNowUsingIndex];
                myPlayer.myDEX = saveData.gameData.myDEXs[tempNowUsingIndex];
                myPlayer.myINT = saveData.gameData.myINTs[tempNowUsingIndex];
                myPlayer.myLUCK = saveData.gameData.myLUCKs[tempNowUsingIndex];

                //코인 골드
                myPlayer.myCoin = saveData.gameData.myCoins[tempNowUsingIndex];
                myPlayer.myGold = saveData.gameData.myGolds[tempNowUsingIndex];
              
                //활성화 비활성화 여부 체크
                //헤어
                myPlayer.myHair.gameObject.SetActive(saveData.gameData.isHairTrue[tempNowUsingIndex]);
                //윗옷
                myPlayer.myCloths[0].myBodyCloth.gameObject.SetActive(saveData.gameData.isClothTrue[tempNowUsingIndex]);
                //바지
                myPlayer.myLegs[0].myLeftLeg.gameObject.SetActive(saveData.gameData.isPantsTrue[tempNowUsingIndex]);
                //투구
                myPlayer.helmet.gameObject.SetActive(saveData.gameData.isHelmetTrue[tempNowUsingIndex]);
                //갑옷
                myPlayer.myArmor[0].myBody.gameObject.SetActive(saveData.gameData.isArmorTrue[tempNowUsingIndex]);
                //망또
                myPlayer.back.gameObject.SetActive(saveData.gameData.isBackTrue[tempNowUsingIndex]);
                //방패
                myPlayer.myHands[0].myRightShield.gameObject.SetActive(saveData.gameData.isWriteHandTrue[tempNowUsingIndex]);
                //무기
                myPlayer.myHands[0].myLeftWeapon.gameObject.SetActive(saveData.gameData.isLeftHandTrue[tempNowUsingIndex]);

                //인종 ( 머리, 몸, 왼팔, 오른팔, 왼다리, 오른다리, 헤어 )          
                myPlayer.head.sprite = saveData.gameData.heads[tempNowUsingIndex];
                myPlayer.body.sprite = saveData.gameData.bodies[tempNowUsingIndex];
                myPlayer.leftArm.sprite = saveData.gameData.leftArms[tempNowUsingIndex];
                myPlayer.rightArm.sprite = saveData.gameData.rightArms[tempNowUsingIndex];
                myPlayer.leftLeg.sprite = saveData.gameData.leftLegs[tempNowUsingIndex];
                myPlayer.rightArm.sprite = saveData.gameData.rightArms[tempNowUsingIndex];
                myPlayer.myHair.sprite = saveData.gameData.myHairs[tempNowUsingIndex];

                //눈관련 ( 왼쪽 아이라인, 오른쪽 아이라인, 왼쪽 눈동자, 오른쪽 눈동자 )
                myPlayer.myEyes[0].myLeftBackEye.sprite = saveData.gameData.myLeftBackEyes[tempNowUsingIndex];
                myPlayer.myEyes[0].myRightBackEye.sprite = saveData.gameData.myRightBackEyes[tempNowUsingIndex];
                myPlayer.myEyes[0].myLeftFrontEye.sprite = saveData.gameData.myLeftFrontEyes[tempNowUsingIndex];
                myPlayer.myEyes[0].myRightFrontEye.sprite = saveData.gameData.myRightFrontEyes[tempNowUsingIndex];

                //체력회복율, 마력회복율
                myPlayer.myHpCure = saveData.gameData.myHpCures[tempNowUsingIndex];
                myPlayer.myMpCure = saveData.gameData.myMpCures[tempNowUsingIndex];

                //수염
                myPlayer.myFaceHair.sprite = saveData.gameData.myFaceHairs[tempNowUsingIndex];

                //윗옷 ( 몸통, 왼팔, 오른팔 )
                myPlayer.myCloths[0].myBodyCloth.sprite = saveData.gameData.myBodyCloths[tempNowUsingIndex];
                myPlayer.myCloths[0].myLeftClothArm.sprite = saveData.gameData.myLeftClothArms[tempNowUsingIndex];
                myPlayer.myCloths[0].myRightClothArm.sprite = saveData.gameData.myRightClothArms[tempNowUsingIndex];

                //바지 (왼다리, 오른다리)
                myPlayer.myLegs[0].myLeftLeg.sprite = saveData.gameData.myleftPantsLegs[tempNowUsingIndex];
                myPlayer.myLegs[0].myRightLeg.sprite = saveData.gameData.myRightPantsLegs[tempNowUsingIndex];

                //망또
                myPlayer.back.sprite = saveData.gameData.backs[tempNowUsingIndex];

                //갑옷 (몸통, 왼어깨, 오른어깨)
                myPlayer.myArmor[0].myBody.sprite = saveData.gameData.myBodyArmores[tempNowUsingIndex];
                myPlayer.myArmor[0].myLeftArm.sprite = saveData.gameData.myLeftShoulderArmers[tempNowUsingIndex];
                myPlayer.myArmor[0].myRightArm.sprite = saveData.gameData.myRightShoulderArmers[tempNowUsingIndex];

                //손 장착(오른손 왼손)
                myPlayer.myHands[0].myLeftWeapon.sprite = saveData.gameData.myLeftWeapons[tempNowUsingIndex];
                myPlayer.myHands[0].myRightWeapon.sprite = saveData.gameData.myRightWeapons[tempNowUsingIndex];
                myPlayer.myHands[0].myLeftShield.sprite = saveData.gameData.myLeftShields[tempNowUsingIndex];
                myPlayer.myHands[0].myRightShield.sprite = saveData.gameData.myRightShields[tempNowUsingIndex];

                //무기의 콜라이더 동기화
                changeCtrl.Weapon_Colider(myPlayer.myHands[0].myLeftWeapon.sprite.name);

                //색상 (2/17 추가)
                //색 정보 저장
                myPlayer.myHairColor = saveData.gameData.myHairColors[tempNowUsingIndex];
                //헤어 컬러 대입
                myPlayer.myHair.color = myPlayer.myHairColor;
                //색 정보 저장
                myPlayer.myLeftEyeColor = saveData.gameData.myLeftFrontEyeColors[tempNowUsingIndex];
                //왼눈동자컬러 대입
                myPlayer.myEyes[0].myLeftFrontEye.color = myPlayer.myLeftEyeColor;
                //색 정보 저장
                myPlayer.myRightEyeColor = saveData.gameData.myRightFrontEyeColors[tempNowUsingIndex];
                //오른 눈동자 컬러 대입
                myPlayer.myEyes[0].myRightFrontEye.color = myPlayer.myRightEyeColor;
            }
            else//신규 캐릭터라면
            {
                //체력 마력 경험치 레벨
                myPlayer.myHp = 100;
                myPlayer.myMp = 100;
                myPlayer.myEXP = 0;
                myPlayer.myLevel = 1;

                //힘 민첩 지능 행운
                myPlayer.mySTR = 4;
                myPlayer.myDEX = 4;
                myPlayer.myINT = 4;
                myPlayer.myLUCK = 4;

                //코인 골드
                myPlayer.myCoin = 100;
                myPlayer.myGold = 100;
          
                myPlayer.myHairColor = myPlayer.myHair.color;
                myPlayer.myLeftEyeColor = myPlayer.myEyes[0].myLeftFrontEye.color;
                myPlayer.myRightEyeColor = myPlayer.myEyes[0].myRightFrontEye.color;
            }

            //이름, 생성여부 판단
            if (saveData.gameData.myNames[tempNowUsingIndex] != null)
            {
                myPlayer.myName.text = saveData.gameData.myNames[tempNowUsingIndex];
            }
            myPlayer.isMade = saveData.gameData.isMades[tempNowUsingIndex];
        }

        //(정보 불러오기) 플레이어에서 -> 데이터로 정보들 넣어주기
        public void Match_Players_ToSaveData() //0,1,2,3
        {
            //체력 마력 경험치 레벨
            saveData.gameData.myHps[tempNowUsingIndex] = myPlayer.myHp;
            saveData.gameData.myMps[tempNowUsingIndex] = myPlayer.myMp;
            saveData.gameData.myEXPs[tempNowUsingIndex] = myPlayer.myEXP;
            saveData.gameData.myLevels[tempNowUsingIndex] = myPlayer.myLevel;

            //힘 민첩 지능 행운
            saveData.gameData.mySTRs[tempNowUsingIndex] = myPlayer.mySTR;
            saveData.gameData.myDEXs[tempNowUsingIndex] = myPlayer.myDEX;
            saveData.gameData.myINTs[tempNowUsingIndex] = myPlayer.myINT;
            saveData.gameData.myLUCKs[tempNowUsingIndex] = myPlayer.myLUCK;

            //코인 골드
            saveData.gameData.myCoins[tempNowUsingIndex] = myPlayer.myCoin;
            saveData.gameData.myGolds[tempNowUsingIndex] = myPlayer.myGold;

            //공격력, 명중률, 치명률, 방어력, 회피율
            saveData.gameData.myAttacks[tempNowUsingIndex] = myPlayer.myAttack;
            saveData.gameData.myAcurrancies[tempNowUsingIndex] = myPlayer.myAcurrancy;
            saveData.gameData.myCriticals[tempNowUsingIndex] = myPlayer.myCritical;
            saveData.gameData.myDefenses[tempNowUsingIndex] = myPlayer.myDefense;
            saveData.gameData.myEvationRates[tempNowUsingIndex] = myPlayer.myEvationRate;

            //체력회복율, 마력회복율
            saveData.gameData.myHpCures[tempNowUsingIndex] = myPlayer.myHpCure;
            saveData.gameData.myMpCures[tempNowUsingIndex] = myPlayer.myMpCure;

            //이름, 생성여부 판단
            saveData.gameData.myNames[tempNowUsingIndex] = myPlayer.myName.text;
            saveData.gameData.isMades[tempNowUsingIndex] = myPlayer.isMade;

            //인종 ( 머리, 몸, 왼팔, 오른팔, 왼다리, 오른다리, 헤어 )
            saveData.gameData.heads[tempNowUsingIndex] = myPlayer.head.sprite;
            saveData.gameData.bodies[tempNowUsingIndex] = myPlayer.body.sprite;
            saveData.gameData.leftArms[tempNowUsingIndex] = myPlayer.leftArm.sprite;
            saveData.gameData.rightArms[tempNowUsingIndex] = myPlayer.rightArm.sprite;
            saveData.gameData.leftLegs[tempNowUsingIndex] = myPlayer.leftLeg.sprite;
            saveData.gameData.rightArms[tempNowUsingIndex] = myPlayer.rightArm.sprite;
            saveData.gameData.myHairs[tempNowUsingIndex] = myPlayer.myHair.sprite;

            //눈관련 ( 왼쪽 아이라인, 오른쪽 아이라인, 왼쪽 눈동자, 오른쪽 눈동자 )
            saveData.gameData.myLeftBackEyes[tempNowUsingIndex] = myPlayer.myEyes[0].myLeftBackEye.sprite;
            saveData.gameData.myRightBackEyes[tempNowUsingIndex] = myPlayer.myEyes[0].myRightBackEye.sprite;
            saveData.gameData.myLeftFrontEyes[tempNowUsingIndex] = myPlayer.myEyes[0].myLeftFrontEye.sprite;
            saveData.gameData.myRightFrontEyes[tempNowUsingIndex] = myPlayer.myEyes[0].myRightFrontEye.sprite;

            //수염
            saveData.gameData.myFaceHairs[tempNowUsingIndex] = myPlayer.myFaceHair.sprite;

            //윗옷 ( 몸통, 왼팔, 오른팔 )
            saveData.gameData.myBodyCloths[tempNowUsingIndex] = myPlayer.myCloths[0].myBodyCloth.sprite;
            saveData.gameData.myLeftClothArms[tempNowUsingIndex] = myPlayer.myCloths[0].myLeftClothArm.sprite;
            saveData.gameData.myRightClothArms[tempNowUsingIndex] = myPlayer.myCloths[0].myRightClothArm.sprite;

            //바지 (왼다리, 오른다리)
            saveData.gameData.myleftPantsLegs[tempNowUsingIndex] = myPlayer.myLegs[0].myLeftLeg.sprite;
            saveData.gameData.myRightPantsLegs[tempNowUsingIndex] = myPlayer.myLegs[0].myRightLeg.sprite;

            //망또
            saveData.gameData.backs[tempNowUsingIndex] = myPlayer.back.sprite;

            //갑옷 (몸통, 왼어깨, 오른어깨)
            saveData.gameData.myBodyArmores[tempNowUsingIndex] = myPlayer.myArmor[0].myBody.sprite;
            saveData.gameData.myLeftShoulderArmers[tempNowUsingIndex] = myPlayer.myArmor[0].myLeftArm.sprite;
            saveData.gameData.myRightShoulderArmers[tempNowUsingIndex] = myPlayer.myArmor[0].myRightArm.sprite;

            //손 장착(오른손 왼손)
            saveData.gameData.myLeftWeapons[tempNowUsingIndex] = myPlayer.myHands[0].myLeftWeapon.sprite;
            saveData.gameData.myRightWeapons[tempNowUsingIndex] = myPlayer.myHands[0].myRightWeapon.sprite;
            saveData.gameData.myLeftShields[tempNowUsingIndex] = myPlayer.myHands[0].myLeftShield.sprite;
            saveData.gameData.myRightShields[tempNowUsingIndex] = myPlayer.myHands[0].myRightShield.sprite;

            //저장 색상 (2/17 추가)
            saveData.gameData.myHairColors[tempNowUsingIndex] = myPlayer.myHair.color;
            saveData.gameData.myLeftFrontEyeColors[tempNowUsingIndex] = myPlayer.myEyes[0].myLeftFrontEye.color;
            saveData.gameData.myRightFrontEyeColors[tempNowUsingIndex] = myPlayer.myEyes[0].myRightFrontEye.color;

            //활성화 비활성화 여부 체크
            //헤어
            saveData.gameData.isHairTrue[tempNowUsingIndex] = myPlayer.myHair.gameObject.activeSelf;
            //윗옷
            saveData.gameData.isClothTrue[tempNowUsingIndex] = myPlayer.myCloths[0].myBodyCloth.gameObject.activeSelf;
            //바지
            saveData.gameData.isPantsTrue[tempNowUsingIndex] = myPlayer.myLegs[0].myLeftLeg.gameObject.activeSelf;
            //투구
            saveData.gameData.isHelmetTrue[tempNowUsingIndex] = myPlayer.helmet.gameObject.activeSelf;
            //갑옷
            saveData.gameData.isArmorTrue[tempNowUsingIndex] = myPlayer.myArmor[0].myBody.gameObject.activeSelf;
            //망또
            saveData.gameData.isBackTrue[tempNowUsingIndex] = myPlayer.back.gameObject.activeSelf;
            //방패
            saveData.gameData.isWriteHandTrue[tempNowUsingIndex] = myPlayer.myHands[0].myRightShield.gameObject.activeSelf;
            //무기
            saveData.gameData.isLeftHandTrue[tempNowUsingIndex] = myPlayer.myHands[0].myLeftWeapon.gameObject.activeSelf;
        }

        /*
        //캐릭터 정보에 따라 저장
        public void PlayerNum(int charNum)
        {

            //현재 사용중인 플래이어
            switch (tempNowUsingIndex)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
        }

        //착용중인 아이템의 이름을 엑셀에서 찾기
        public void PlayerEquipParts_Search(string tempFindName_ToSave)
        {
            //아이템 이름으로부터 찾은 키값 : 불러올 때 인덱스로 사용
            string key = equiptable_Items.itemChart_Equiptable_DataName[tempFindName_ToSave][ItemField.Index];

            //아이템 이름으로부터 찾은 장착 파츠
            string itemPart = equiptable_Items.itemChart_Equiptable_DataName[tempFindName_ToSave][ItemField.Parts];

            switch (itemPart)
            {
                case "Cloth":
                    break;
                case "PantsLeg":
                    break;
                case "Helmet":
                    break;
                case "Armor":
                    break;
                case "Shield":
                    break;
                case "Belt":
                    break;
                case "Cross":
                    break;
                case "Necklace":
                    break;
                case "Back":
                    break;
                case "Weapon":
                    break;
            }
        }

        //usingIndexNow = 캐릭터 주소, equipParts = 장비 파츠, equiptableIndex = 전체 장비인덱스
        //스크립트 GameData에 착용 장비를 저장하기 위한 함수
        public void PlayerParts_ToSave(int usingIndexNow,EquipParts equipParts, string TempitemdataName)
        {
            //임시: 장비 착용하는부분 인덱스
            int tempEquipParts;

            switch (equipParts)
            {
                case EquipParts.Cloth:
                    tempEquipParts = 0;
                    Player_MatchJson(usingIndexNow, tempEquipParts, TempitemdataName);//인덱스 저장

                    break;
                case EquipParts.PantsLeg:
                    tempEquipParts = 1;
                    Player_MatchJson(usingIndexNow, tempEquipParts, TempitemdataName);//인덱스 저장

                    break;
                case EquipParts.Helmet:
                    tempEquipParts = 2;
                    Player_MatchJson(usingIndexNow, tempEquipParts, TempitemdataName);//인덱스 저장

                    break;
                case EquipParts.Armor:
                    tempEquipParts = 3;
                    Player_MatchJson(usingIndexNow, tempEquipParts, TempitemdataName);//인덱스 저장

                    break;
                case EquipParts.Back:
                    tempEquipParts = 4;
                    Player_MatchJson(usingIndexNow, tempEquipParts, TempitemdataName);//인덱스 저장

                    break;
                case EquipParts.Belt:
                    tempEquipParts = 5;
                    Player_MatchJson(usingIndexNow, tempEquipParts, TempitemdataName);//인덱스 저장

                    break;
                case EquipParts.Cross:
                    tempEquipParts = 6;
                    Player_MatchJson(usingIndexNow, tempEquipParts, TempitemdataName);//인덱스 저장

                    break;
                case EquipParts.Necklace:
                    tempEquipParts = 7;
                    Player_MatchJson(usingIndexNow, tempEquipParts, TempitemdataName);//인덱스 저장

                    break;
                case EquipParts.Ring:
                    tempEquipParts = 8;
                    Player_MatchJson(usingIndexNow, tempEquipParts, TempitemdataName);//인덱스 저장

                    break;
                case EquipParts.Weapon:
                    tempEquipParts = 9;
                    Player_MatchJson(usingIndexNow, tempEquipParts, TempitemdataName);//인덱스 저장

                    break;
                case EquipParts.Shield:
                    tempEquipParts = 10;
                    Player_MatchJson(usingIndexNow, tempEquipParts, TempitemdataName);//인덱스 저장

                    break;
            }
        }

        //최대한 for문을 안돌리기 위함 : 게임 인덱스 저장하는 용도
        //usingIndexNow = 캐릭터인덱스, partIndex = 장비파츠, equiptableIndex = 장비사전,
        public void Player_MatchJson(int usingIndexNow,int partsIndex, string equiptableIndex)
        {
            switch (usingIndexNow)
            {
                case 0:
                    saveData.gameData.equiptable_ItemDataName1[partsIndex] = equiptableIndex;
                    break;
                case 1:
                    saveData.gameData.equiptable_ItemDataName2[partsIndex] = equiptableIndex;
                    break;
                case 2:
                    saveData.gameData.equiptable_ItemDataName3[partsIndex] = equiptableIndex;
                    break;
                case 3:
                    saveData.gameData.equiptable_ItemDataName4[partsIndex] = equiptableIndex;
                    break;
            }
        }
        */

    }//class
}//namespace
