using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Main;

namespace Main
{
    //상점이나 컨텐츠에서 외형을 바꿀때 게임오브젝트에 버튼 함수를 호출하여 인덱스를 통해 제어하는 방식
    //Item(아이템 딕셔너리)와 ItemSprites (외형 이미지 배열) -> 캐릭터 기본 아이템과 외형을 변경

    //'적용'된 캐릭터 EnumPlayer_Information 내용을 저장 동기화는 따로 해야한다.

    //깔끔하게 DataController를 
    //(데이터기록을 보관하는 스크립트에서 저장및 동기화할 수 있게 따로 작성할 것) 
    //(예외 : 로비씬에서만 0,1,2,3의 구분을 위해 씬스크립트에서 작성했음)

    public class ChangeCtrl : MonoBehaviour
    {
        //다른 스크립트
        public EnumPlayer_Information player;//연결한다.
        public Sync_DataCtrlPlayer sync_data;//인덱스를 저장하기 위함
        //옷 능력치 받아오기 위함 Item스크립트, 종족 헤어 받아오기 위함 ItemSprite
        public Item tempItem;//saveCtrl 오브젝트 연결한다.
        public ItemSprites tempItemSprites; //saveCtrl 오브젝트 연결한다.
        public DataController saveData; //백업때문에 사용( 인덱스json이라서 상수가 아니면, 전부조건검사해야 함)

        //헤어 인덱스 
        int forStartShortHair;
        int forStartSportHair;
        int forEndSportHair;

        //색변경
        public Slider colorR;
        public Slider colorG;
        public Slider colorB;

        //백업용 색상 (public은 작동 확인용)
        Color backUp_hairColor;
        Color backUp_eyeColor;
        
        //나중에 혹시 파츠 수정한다면, 배열로 만들었을때 제가 작업하기에 읽고 찾기 힘들것 같아서
        //index 구분하기가 가독성이 떨어진다고 생각하고, 임시방편으로 길게 작성
       
        //백업용 외형
        Sprite backUp_head;
        Sprite backUp_body;
        Sprite backUp_leftArm;
        Sprite backUp_rightArm;
        Sprite backUp_leftLeg;
        Sprite backUp_rightLeg;
        Sprite backUp_hair;
        Sprite backUp_backEye;
        Sprite backUp_frontEye;
        Sprite backUp_myBodyCloth;
        Sprite backUp_myLeftCloth;
        Sprite backUp_myRightCloth;
        Sprite backUp_leftLegPant;
        Sprite backUp_rightLegPant;
        Sprite backUp_myLeftWeapon;

        int tempTotalIndex;

        //랜덤 외형 숫자 부여용 (index 0 : 파츠의 랜덤 종류 , index 1 : 해당 종류의 랜덤 디자인)
        int[] random_species = new int[2];
        int[] random_hair = new int[2];
        int[] random_Eye = new int[2]; //예외:  0 눈매, 1 눈동자
        int random_Cloth; //윗옷 (한벌옷 x :통일)
        int random_Pants;//바지는 한 종류
        int random_Weapon;//튜토리얼 용 5가지

        //랜덤 컬러 값 부여용 (0~100까지 랜덤값을 받아서 0.001f를 곱한 다음에 0 ~ 1 value값을 부여)
        int random_intR;
        int random_intG;
        int random_intB;
        float random_floatR;
        float random_floatG;
        float random_floatB;

        //리셋 버튼 제어용 (false 슬라이더 동작에 의한 변경 / true 리셋 값만큼 슬라이더 위치 조정)
        bool isClick_ResetHairColorBtn;
        bool isClick_ResetEyeColorBtn;

        //백업 버튼 제어용 (false 처음 착용상태 / true 현재 착용상태를 저장)
        bool isClick_BackUpLooks;

        //랜덤 버튼 제어용 (flase 슬라이더 동작에 의한 변경 / true 랜덤 값만큼 슬라이더 위치 조정)
        bool isClick_RandomHairColorBtn;
        bool isClick_RandomEyeColorBtn;

        //'직접' r,g,b를 SpriteRenderer.color에 '각각' 주고 받을 수 없다.
        //이미지의 컬러는 수정할 수 없다는 오류(?)가 있기 때문에
        //color에 한번 넘긴 다음, 캐릭터 외형에 대입한다.
        //(넘겨받은 color 전체 <- 컬러 값을 주고받는다 -> 이미지.color)

        //백업용, 장비 저장할 인덱스
        string[] tempEquiptable_ItemDataName = new string[11];

        //이미지 받아오는 배열
        public Sprite[] itemSprites = new Sprite[3];

        //무기콜라이더에 따라 변경
        public CapsuleCollider2D leftHandWeaponColider; //자식 오브젝트: 무기의 콜라이더 수정하기 위함
        public SpriteRenderer weaponSR; //임시_현재의 무기 spriterenderer


        enum TabIndex
        { 
            human,
            elf,
            orc,
            theOthers,
            hair,
            eyeblow,
            eye,
            items,
            hairColor,//(2/17 추가)
            eyeColor,//(2/17 추가)
        }

        //초기화
        TabIndex index = TabIndex.human;
        
        private void Start()
        {
            //길이 조절용
            forStartShortHair = tempItemSprites.hairs[0].longHair.Length;
            forStartSportHair = forStartShortHair + tempItemSprites.hairs[0].shortHair.Length;
            forEndSportHair = forStartSportHair  + tempItemSprites.hairs[0].sportHair.Length;

            //번호 확인 시 필요
            //print("1번 누적"+forStartShortHair + " " + forStartSportHair + " "+ forEndSportHair);
            //print("2번 " + tempItemSprites.hairs[0].longHair.Length + " "+ tempItemSprites.hairs[0].shortHair.Length + tempItemSprites.hairs[0].sportHair.Length);

            //초기화
            isClick_BackUpLooks = false;

            print("isMade : " + saveData.gameData.isMades[saveData.gameData.nowUsingIndex] + "nowUsingIndex" + saveData.gameData.nowUsingIndex);

            //캐릭터 생성 전이라면
            if (!saveData.gameData.isMades[saveData.gameData.nowUsingIndex])
            {
                //초기화
                player.myHair.color = Color.white;
                player.myEyes[0].myLeftFrontEye.color = Color.black;
                player.myEyes[0].myRightFrontEye.color = Color.black;

                //방지용
                backUp_hairColor.a = 1;
                backUp_eyeColor.a = 1;
            }

            //대입
            backUp_hairColor = player.myHair.color;
            backUp_eyeColor = player.myEyes[0].myLeftFrontEye.color;


            isClick_RandomHairColorBtn = false;
            isClick_RandomEyeColorBtn = false;
        }

        //엑셀CSV로부터 맞는 콜라이더를 넣어주는 함수 
        public void Weapon_Colider(string temp)//temp는 데이터네임 (인덱스 X)
        {
            //플래이어의 무기 이름
            string checkName = weaponSR.sprite.name;

            print("무기의 이름 : " + checkName);
            print("매개변수의 텍스트네임 : " + temp);

            //데이터 이름이 일치한다면
            if (checkName == temp)
            {
                //딕서너리의 속성들
                string offset_x = tempItem.itemChart_WeaponsColider_DataName[temp][WeaponColiderSetting.OffsetX];
                string offset_y = tempItem.itemChart_WeaponsColider_DataName[temp][WeaponColiderSetting.OffsetY];
                string size_x = tempItem.itemChart_WeaponsColider_DataName[temp][WeaponColiderSetting.SizeX];
                string size_y = tempItem.itemChart_WeaponsColider_DataName[temp][WeaponColiderSetting.SizeY];

                //오프셋 위치
                leftHandWeaponColider.offset = // new Vector2 ( 스트링-> float형 );
                    new Vector2(float.Parse(offset_x), float.Parse(offset_y));

                //사이즈 수정
                leftHandWeaponColider.size = // new Vector2 (오브젝트형 -> 스트링으로 -> float형);
                    new Vector2(float.Parse(size_x), float.Parse(size_y));
            }
        }

        //탭클릭할 때 버튼호출
        public void OnClick_TabIndex_btn(int i)
        {
            switch (i)
            {
                case 0:
                    index = TabIndex.human;
                    break;
                case 1:
                    index = TabIndex.elf;
                    break;
                case 2:
                    index = TabIndex.orc;
                    break;
                case 3:
                    index = TabIndex.theOthers;
                    break;
                case 4:
                    index = TabIndex.hair;
                    break;
                case 5:
                    index = TabIndex.eyeblow;
                    break;
                case 6:
                    index = TabIndex.eye;
                    break;
                case 7:
                    index = TabIndex.items;
                    break;
                case 8:// 머리 색상 변경 (2/17추가)
                    index = TabIndex.hairColor;

                    //현재 색상만큼 설정
                    colorR.value = player.myHair.color.r;
                    colorG.value = player.myHair.color.g;
                    colorB.value = player.myHair.color.b;
                    
                    break;
                case 9:// 눈 색상 변경 (2/17추가)
                    index = TabIndex.eyeColor;

                    //현재 색상만큼 설정
                    colorR.value = player.myEyes[0].myLeftFrontEye.color.r;
                    colorG.value = player.myEyes[0].myLeftFrontEye.color.g;
                    colorB.value = player.myEyes[0].myLeftFrontEye.color.b;

                    break;
            }
        }

        //아이템 선택하면 버튼 인스펙터에서 호출하는 함수
        public void OnClick_itemEquip_Btn(string temp)
        {
            //print("선택 : " + temp);//확인용

            // 엑셀 DataName == 아이템의 프로젝트의 이미지png 리소스 이름
            string dataName = tempItem.itemChart_Equiptable_Index[temp][ItemField.DataName];
            // 엑셀 ResourcePath == Resources 하위의 폴더 경로
            string resourcePath = tempItem.itemChart_Equiptable_Index[temp][ItemField.ResourcePath];

            // 1개 png 안의 multiSprite 갯수 만큼 전부 배열로 받아오기
            itemSprites = Resources.LoadAll<Sprite>(resourcePath + "/"  + dataName);
            //print("경로 : " + dataName + "/" + resourcePath + "\n");
            //print(itemSprites[0] + " " + itemSprites[1] + " " + itemSprites[2]);

            //temp로 받아온 아이템 종류 검사 : 파츠에 맞게 캐릭터 입히기
            switch (tempItem.itemChart_Equiptable_Index[temp][ItemField.Parts])
            {
                //윗옷의 경우
                case "Cloth":
                    print("윗옷");
                    player.myCloths[0].myBodyCloth.sprite = itemSprites[0]; //몸통
                    player.myCloths[0].myLeftClothArm.sprite = itemSprites[1]; //왼 소매
                    player.myCloths[0].myRightClothArm.sprite = itemSprites[2]; //오른 소매

                    break;
                //바지의 경우
                case "PantsLeg":
                    print("바지");
                    player.myLegs[0].myLeftLeg.sprite = itemSprites[0]; // 왼 바지
                    player.myLegs[0].myRightLeg.sprite = itemSprites[1]; // 오른 바지

                    break;
                //무기의 경우
                case "Weapon":
                    print("무기");
                    //무기게임오브젝트 활성화
                    player.myHands[0].myLeftWeapon.gameObject.SetActive(true);

                    //새 캐릭터를 고를때 고를 수 있는 무기는 axe1 , short2, middle1, long3, dagger knife1 한정
                    player.myHands[0].myLeftWeapon.sprite = itemSprites[0];
                    Weapon_Colider(dataName);
                    break;
            }
        }

     
        public void OnClick_On_Btn(int i)
        {
            switch (index)
            {
                case TabIndex.human:
                    //연결하기
                    UseforInsertSpeices(0, i);
             
                    break;
                case TabIndex.elf:
                    //연결하기
                    UseforInsertSpeices(1, i);
                  
                    break;
                case TabIndex.orc:
                    //연결하기
                    UseforInsertSpeices(2, i);

                    break;
                case TabIndex.theOthers:
                    //연결하기
                    UseforInsertSpeices(3, i);

                    break;
                case TabIndex.hair:
                    
                    if (i == 100)//민머리일때
                    {
                        //헤어 비활성화
                        player.myHair.gameObject.SetActive(false);
                    }
                    else//if (i != 0) 이외
                    {
                        //헤어 활성화
                        player.myHair.gameObject.SetActive(true);
                    }

                    //긴 머리 범위
                    if ( i < forStartShortHair)
                    {
                        //연결하기
                        useForHair(0, i);
                    }
                    //짧은 머리 범위
                    if (forStartShortHair <= i && i < forStartSportHair)
                    {
                        //연결하기
                        useForHair(1, i - forStartShortHair);
                    }
                    //스포츠 머리 범위
                    if (forStartSportHair <= i && i < forEndSportHair)
                    {
                        //연결하기
                        useForHair(2, i - forStartSportHair);
                    }

                    break;
                case TabIndex.eyeblow://backeye
                    //연결하기
                    useForEye(0, i);
      
                    break;
                case TabIndex.eye: //fronteye (0,1,4,9) : 중복은 사용하지 않음
                    //연결하기
                    useForEye(1, i);
         
                    break;
                  
            }
        }//clickbtn

        //헤어, 눈 색상 제어(업데이트)
        void CtrlColor()
        {
            //색상 제어하기 위함
            if (index == TabIndex.hairColor)//헤어색상 탭
            {
                //리셋 버튼을 눌렀다면
                if (isClick_ResetHairColorBtn)
                {
                    //제어값을 돌려준다.
                    colorR.value = backUp_hairColor.r;
                    colorG.value = backUp_hairColor.g;
                    colorB.value = backUp_hairColor.b;

                    //리셋 역할 끝나면
                    isClick_ResetHairColorBtn = false;
                }
                if (isClick_RandomHairColorBtn)
                {
                    colorR.value = random_floatR;
                    colorG.value = random_floatG;
                    colorB.value = random_floatB;

                    isClick_RandomHairColorBtn = false;
                }

                //슬라이더 조정값 저장
                player.myHairColor.r = colorR.value;
                player.myHairColor.g = colorG.value;
                player.myHairColor.b = colorB.value;

                //항상 arpha값 = 1
                player.myHairColor.a = 1;

                //헤어 컬러에 대입
                player.myHair.color = player.myHairColor;
            }
            if (index == TabIndex.eyeColor)//눈색상 탭
            {
                //리셋 버튼을 눌렀다면
                if (isClick_ResetEyeColorBtn)
                {
                    //제어값을 돌려준다.
                    colorR.value = backUp_eyeColor.r;
                    colorG.value = backUp_eyeColor.g;
                    colorB.value = backUp_eyeColor.b;

                    //리셋 역할 끝나면
                    isClick_ResetEyeColorBtn = false;
                }
                if (isClick_RandomEyeColorBtn)
                {
                    colorR.value = random_floatR;
                    colorG.value = random_floatG;
                    colorB.value = random_floatB;

                    isClick_RandomEyeColorBtn = false;
                }

                //왼눈 슬라이더 조정값 저장
                player.myLeftEyeColor.r = colorR.value;
                player.myLeftEyeColor.g = colorG.value;
                player.myLeftEyeColor.b = colorB.value;

                //오른눈 슬라이더 조정값 저장
                player.myRightEyeColor.r = colorR.value;
                player.myRightEyeColor.g = colorG.value;
                player.myRightEyeColor.b = colorB.value;

                //항상 arpha값 = 1
                player.myLeftEyeColor.a = 1;
                player.myRightEyeColor.a = 1;

                //눈 컬러에 대입
                player.myEyes[0].myLeftFrontEye.color = player.myLeftEyeColor;
                player.myEyes[0].myRightFrontEye.color = player.myRightEyeColor;
            }
        }

        private void Update()
        {
             CtrlColor();
            //print(index);
        }//update

        //입고 있는 옷 전체 리셋 //백업한 적이 없다면 처음 상태로 돌리기
        
        public void OnClick_LookUpReset_btn()
        {
            //백업 누른 적이 없다면
            if (!isClick_BackUpLooks)
            {
                //인종 사람 1
                player.head.sprite = tempItemSprites.spacies[0].humans[0].head;
                player.body.sprite = tempItemSprites.spacies[0].humans[0].body;
                player.leftArm.sprite = tempItemSprites.spacies[0].humans[0].leftArm;
                player.rightArm.sprite = tempItemSprites.spacies[0].humans[0].rightArm;
                player.leftLeg.sprite = tempItemSprites.spacies[0].humans[0].leftLeg;
                player.rightLeg.sprite = tempItemSprites.spacies[0].humans[0].rightLeg;

                //헤어 //긴 머리 6
                //헤어 활성화
                player.myHair.gameObject.SetActive(true);
                player.myHair.sprite = tempItemSprites.hairs[0].longHair[5];

                //눈매
                player.myEyes[0].myLeftBackEye.sprite = tempItemSprites.eyes[5].backEyes;
                player.myEyes[0].myRightBackEye.sprite = tempItemSprites.eyes[5].backEyes;

                //눈동자
                player.myEyes[0].myLeftFrontEye.sprite = tempItemSprites.eyes[4].frontEyes;
                player.myEyes[0].myRightFrontEye.sprite = tempItemSprites.eyes[4].frontEyes;

                //윗옷
                OnClick_itemEquip_Btn("CC19");//기본 윗옷

                //바지
                OnClick_itemEquip_Btn("CP14");//기본 바지

                //무기게임오브젝트 비활성화
                player.myHands[0].myLeftWeapon.gameObject.SetActive(false);

            }

            //백업버튼을 눌렀다면 백업 누른 시점의 preset으로 입혀주기
            else//if (isClick_BackUpLooks)
            {
                //인종 백업
                player.head.sprite = backUp_head;
                player.body.sprite = backUp_body;
                player.leftArm.sprite = backUp_leftArm;
                player.rightArm.sprite = backUp_rightArm;
                player.leftLeg.sprite = backUp_leftLeg;
                player.rightLeg.sprite = backUp_rightLeg;

                //헤어 백업
                if (player.myHair.gameObject.activeSelf)//민머리가 아니라면
                {
                    player.myHair.sprite = backUp_hair;//스프라이트 넘겨주기
                }

                //눈매 ,눈동자 백업
                player.myEyes[0].myLeftBackEye.sprite = backUp_backEye;
                player.myEyes[0].myRightBackEye.sprite = backUp_backEye;
                player.myEyes[0].myLeftFrontEye.sprite = backUp_frontEye;
                player.myEyes[0].myRightFrontEye.sprite = backUp_frontEye;

                //윗옷 백업
                player.myCloths[0].myBodyCloth.sprite = backUp_myBodyCloth;
                player.myCloths[0].myLeftClothArm.sprite = backUp_myLeftCloth;
                player.myCloths[0].myRightClothArm.sprite = backUp_myRightCloth;

                //바지 백업
                player.leftLeg.sprite = backUp_leftLegPant;
                player.rightLeg.sprite = backUp_rightLegPant;

                //
                if (saveData.gameData.nowUsingIndex == 0)
                {
                    //인덱스 저장
                    saveData.gameData.equiptable_ItemDataName1[0] = tempEquiptable_ItemDataName[0]; //0 윗옷
                    saveData.gameData.equiptable_ItemDataName1[1] = tempEquiptable_ItemDataName[1]; //1 바지
                }
                if (saveData.gameData.nowUsingIndex == 1)
                {
                    //인덱스 저장
                    saveData.gameData.equiptable_ItemDataName2[0] = tempEquiptable_ItemDataName[0]; //0 윗옷
                    saveData.gameData.equiptable_ItemDataName2[1] = tempEquiptable_ItemDataName[1]; //1 바지
                }
                if (saveData.gameData.nowUsingIndex == 2)
                {
                    //인덱스 저장
                    saveData.gameData.equiptable_ItemDataName3[0] = tempEquiptable_ItemDataName[0]; //0 윗옷
                    saveData.gameData.equiptable_ItemDataName3[1] = tempEquiptable_ItemDataName[1]; //1 바지
                }
                if (saveData.gameData.nowUsingIndex == 3)
                {
                    //인덱스 저장
                    saveData.gameData.equiptable_ItemDataName4[0] = tempEquiptable_ItemDataName[0]; //0 윗옷
                    saveData.gameData.equiptable_ItemDataName4[1] = tempEquiptable_ItemDataName[1]; //1 바지
                }

                //무기 백업
                if (player.myHands[0].myLeftWeapon.gameObject.activeSelf)//무기를 장착하고 있다면
                {
                    player.myHands[0].myLeftWeapon.sprite = backUp_myLeftWeapon;
                }
            }
        }
    
        //입고 있는 옷 전체 백업
        public void OnClick_LookUpBackUp_btn()
        {
            print("백업 완료" + isClick_BackUpLooks);
            isClick_BackUpLooks = true;

            //인종 백업
            backUp_head = player.head.sprite;
            backUp_body = player.body.sprite; 
            backUp_leftArm = player.leftArm.sprite;
            backUp_rightArm = player.rightArm.sprite;
            backUp_leftLeg = player.leftLeg.sprite;
            backUp_rightLeg = player.rightLeg.sprite;

            //헤어 백업
            if (player.myHair.gameObject.activeSelf)//민머리가 아니라면
            {
                backUp_hair = player.myHair.sprite;//스프라이트 넘겨주기
            }
          
            //눈매 ,눈동자 백업
            backUp_backEye = player.myEyes[0].myLeftBackEye.sprite;
            backUp_frontEye = player.myEyes[0].myLeftFrontEye.sprite;

            //윗옷 백업
            backUp_myBodyCloth = player.myCloths[0].myBodyCloth.sprite;
            backUp_myLeftCloth = player.myCloths[0].myLeftClothArm.sprite;
            backUp_myRightCloth = player.myCloths[0].myRightClothArm.sprite;

            //바지 백업
            backUp_leftLegPant = player.leftLeg.sprite;
            backUp_rightLegPant = player.rightLeg.sprite;
            if (saveData.gameData.nowUsingIndex == 0)
            {
                //인덱스 저장
                tempEquiptable_ItemDataName[0] = saveData.gameData.equiptable_ItemDataName1[0]; //0 윗옷
                tempEquiptable_ItemDataName[1] = saveData.gameData.equiptable_ItemDataName1[1]; //1 바지
            }
            if (saveData.gameData.nowUsingIndex == 1)
            {
                //인덱스 저장
                tempEquiptable_ItemDataName[0] = saveData.gameData.equiptable_ItemDataName2[0]; //0 윗옷
                tempEquiptable_ItemDataName[1] = saveData.gameData.equiptable_ItemDataName2[1]; //1 바지
            }
            if (saveData.gameData.nowUsingIndex == 2)
            {
                //인덱스 저장
                tempEquiptable_ItemDataName[0] = saveData.gameData.equiptable_ItemDataName3[0]; //0 윗옷
                tempEquiptable_ItemDataName[1] = saveData.gameData.equiptable_ItemDataName3[1]; //1 바지
            }
            if (saveData.gameData.nowUsingIndex == 3)
            {
                //인덱스 저장
                tempEquiptable_ItemDataName[0] = saveData.gameData.equiptable_ItemDataName4[0]; //0 윗옷
                tempEquiptable_ItemDataName[1] = saveData.gameData.equiptable_ItemDataName4[1]; //1 바지
            }


            //무기 백업
            if (player.myHands[0].myLeftWeapon.gameObject.activeSelf)//무기를 장착하고 있다면
            {
                backUp_myLeftWeapon = player.myHands[0].myLeftWeapon.sprite;

                if (saveData.gameData.nowUsingIndex == 0)
                {
                    //인덱스 저장
                    tempEquiptable_ItemDataName[10] = saveData.gameData.equiptable_ItemDataName1[10];
                }
                if (saveData.gameData.nowUsingIndex == 1)
                {
                    //인덱스 저장
                    tempEquiptable_ItemDataName[10] = saveData.gameData.equiptable_ItemDataName2[10];
                }
                if (saveData.gameData.nowUsingIndex == 2)
                {
                    //인덱스 저장
                    tempEquiptable_ItemDataName[10] = saveData.gameData.equiptable_ItemDataName3[10];
                }
                if (saveData.gameData.nowUsingIndex == 3)
                {
                    //인덱스 저장
                    tempEquiptable_ItemDataName[10] = saveData.gameData.equiptable_ItemDataName4[10];
                }
            }
        }


        //색상 리셋 == 백업상태로 돌리기 //백업한 적이 없다면 처음상태로 돌리기
        public void OnClick_HairColorReset_btn()
        {
            //업데이트 제어용
            isClick_ResetHairColorBtn = true;
     
            //색 정보에 넘기고
            player.myHairColor = backUp_hairColor;

            //색상을 이미지에 대입
            player.myHair.color = player.myHairColor;
        }

        //색상 리셋 == 백업상태로 돌리기 //백업한 적이 없다면 처음상태로 돌리기
        public void OnClick_EyeColorReset_btn()
        {
            //업데이트 제어용
            isClick_ResetEyeColorBtn = true;

            //색 정보에 넘기고
            player.myLeftEyeColor = backUp_eyeColor;
            player.myRightEyeColor = backUp_eyeColor;

            //색상을 이미지에 대입
            player.myEyes[0].myLeftFrontEye.color = player.myLeftEyeColor;
            player.myEyes[0].myRightFrontEye.color = player.myRightEyeColor;
        }

        //헤어 색상 백업
        public void OnClick_HairColorBackUp_btn()
        {
            //머리 이미지 색상 넘겨주고
            player.myHairColor = player.myHair.color;
            //머리 색상 백업
            backUp_hairColor = player.myHairColor;
        }
        public void OnClick_EyeColorBackUp_btn()
        {
            //눈 이미지 색상 넘겨주고
            player.myLeftEyeColor = player.myEyes[0].myLeftFrontEye.color;
            //눈 색상 백업
            backUp_eyeColor = player.myLeftEyeColor;

        }

        //올 랜덤 누르면 자동 입히기 함수 04/20
        public void OnClick_RandomLookUp_Btn()
        {
            //종족
            //[0] :인종 범위  0 ~ 3
            //[1] :해당 인종의 피부 랜덤 범위
            random_species[0] = Random.Range(0, 4);

            //랜덤 종족
            switch (random_species[0])
            {
                case 0://humans

                    //피부 랜덤 값
                    random_species[1] = Random.Range(0, tempItemSprites.spacies[0].humans.Length);

                    //연결하기
                    UseforInsertSpeices(random_species[0], random_species[1]);

                    break;
                case 1://elfs

                    //피부 랜덤 값
                    random_species[1] = Random.Range(0, tempItemSprites.spacies[0].elfs.Length);

                    //연결하기
                    UseforInsertSpeices(random_species[0], random_species[1]);

                    break;
                case 2://orcs

                    //피부 랜덤 값
                    random_species[1] = Random.Range(0, tempItemSprites.spacies[0].orcs.Length);

                    //연결하기
                    UseforInsertSpeices(random_species[0], random_species[1]);

                    break;
                case 3://others

                    //피부 랜덤 값
                    random_species[1] = Random.Range(0, tempItemSprites.spacies[0].theOrders.Length);

                    //연결하기
                    UseforInsertSpeices(random_species[0], random_species[1]);

                    break;
            }
            //헤어
            //[0] :헤어 길이별 범위  0 ~ 3
            //[1] :헤어 디자인 랜덤 범위
            random_hair[0] = Random.Range(0, 3);

            //랜덤 길이
            switch (random_hair[0])
            {
                case 0://longHair

                    //디자인 랜덤 값
                    random_hair[1] = Random.Range(0, tempItemSprites.hairs[0].longHair.Length + 1);

                    // +1을 하는 이유 : 최대값이 나오면 민머리
                    if (random_hair[1] == tempItemSprites.hairs[0].longHair.Length)
                    {
                        //헤어 오브젝트 비활성화
                        player.myHair.gameObject.SetActive(false);
                    }
                    else //if(random_hair[1] != tempItemSprites.hairs[0].longHair.Length)
                    {
                        //헤어 오브젝트 활성화
                        player.myHair.gameObject.SetActive(true);

                        //연결하기
                        useForHair(random_hair[0], random_hair[1]);
                    }

                    break;
                case 1://shortHair

                    //디자인 랜덤 값
                    random_hair[1] = Random.Range(0, tempItemSprites.hairs[0].shortHair.Length + 1);

                    // +1을 하는 이유 : 최대값이 나오면 민머리
                    if (random_hair[1] == tempItemSprites.hairs[0].shortHair.Length)
                    {
                        //헤어 오브젝트 비활성화
                        player.myHair.gameObject.SetActive(false);
                    }
                    else //if(random_hair[1] != tempItemSprites.hairs[0].shortHair.Length)
                    {
                        //헤어 오브젝트 활성화
                        player.myHair.gameObject.SetActive(true);

                        //연결하기
                        useForHair(random_hair[0], random_hair[1]);
                    }

                    break;
                case 2://sportHair

                    //디자인 랜덤 값
                    random_hair[1] = Random.Range(0, tempItemSprites.hairs[0].sportHair.Length + 1);

                    // +1을 하는 이유 : 최대값이 나오면 민머리
                    if (random_hair[1] == tempItemSprites.hairs[0].sportHair.Length)
                    {
                        //헤어 오브젝트 비활성화
                        player.myHair.gameObject.SetActive(false);
                    }
                    else //if(random_hair[1] != tempItemSprites.hairs[0].sportHair.Length)
                    {
                        //헤어 오브젝트 활성화
                        player.myHair.gameObject.SetActive(true);

                        //연결하기
                        useForHair(random_hair[0], random_hair[1]);
                    }
                    break;
            }

            //눈
            //[0] : 눈매 디자인 
            //[1] : 눈동자 디자인
            random_Eye[0] = Random.Range(0, 6);
            random_Eye[1] = Random.Range(0, 4);

            //눈매
            useForEye(0, random_Eye[0]);
            //눈동자
            useForEye(1, random_Eye[1]);

            //윗옷 
            random_Cloth = Random.Range(01, 24); // 인덱스 넘버 설정
            string tempIndex = "CC" + random_Cloth.ToString(); // 인덱스 작성
            OnClick_itemEquip_Btn(tempIndex); //해당 윗옷 입히기

            //바지 
            random_Pants = Random.Range(01, 17);// 인덱스 넘버 설정
            tempIndex = "CP" + random_Pants.ToString(); //인덱스 작성
            OnClick_itemEquip_Btn(tempIndex); //해당 바지 입히기

            //무기
            random_Weapon = Random.Range(0, 5);
            player.myHands[0].myLeftWeapon.gameObject.SetActive(true); //무기오브젝트 활성화

            switch (random_Weapon)//랜덤
            {
                case 0://Axe1
                    tempIndex = "CWA01";
                    OnClick_itemEquip_Btn(tempIndex);
                    break;
                case 1://short2
                    tempIndex = "CWS02";
                    OnClick_itemEquip_Btn(tempIndex);
                    
                    break;
                case 2://middle1
                    tempIndex = "CWM01";
                    OnClick_itemEquip_Btn(tempIndex);
                    
                    break;
                case 3://long3
                    tempIndex = "CWL03";
                    OnClick_itemEquip_Btn(tempIndex);
                    
                    break;
                case 4://knife1
                    tempIndex = "CWK01";
                    OnClick_itemEquip_Btn(tempIndex);
                    break;
            }
        }
    
        //여러곳에서 중복작성하기 때문에 이곳에서 호출함수로 사용
        //parts 종류 , myResult 파츠 범위 : 캐릭터에게 이미지 연결하는 종족함수
        void UseforInsertSpeices(int parts, int myResult)
        {
            //종류
            switch (parts)
            {
                case 0://human
                    player.head.sprite = tempItemSprites.spacies[0].humans[myResult].head;
                    player.body.sprite = tempItemSprites.spacies[0].humans[myResult].body;
                    player.leftArm.sprite = tempItemSprites.spacies[0].humans[myResult].leftArm;
                    player.rightArm.sprite = tempItemSprites.spacies[0].humans[myResult].rightArm;
                    player.leftLeg.sprite = tempItemSprites.spacies[0].humans[myResult].leftLeg;
                    player.rightLeg.sprite = tempItemSprites.spacies[0].humans[myResult].rightLeg;
                    break;

                case 1://elfs
                    player.head.sprite = tempItemSprites.spacies[0].elfs[myResult].head;
                    player.body.sprite = tempItemSprites.spacies[0].elfs[myResult].body;
                    player.leftArm.sprite = tempItemSprites.spacies[0].elfs[myResult].leftArm;
                    player.rightArm.sprite = tempItemSprites.spacies[0].elfs[myResult].rightArm;
                    player.leftLeg.sprite = tempItemSprites.spacies[0].elfs[myResult].leftLeg;
                    player.rightLeg.sprite = tempItemSprites.spacies[0].elfs[myResult].rightLeg;
                    break;

                case 2://orcs
                    player.head.sprite = tempItemSprites.spacies[0].orcs[myResult].head;
                    player.body.sprite = tempItemSprites.spacies[0].orcs[myResult].body;
                    player.leftArm.sprite = tempItemSprites.spacies[0].orcs[myResult].leftArm;
                    player.rightArm.sprite = tempItemSprites.spacies[0].orcs[myResult].rightArm;
                    player.leftLeg.sprite = tempItemSprites.spacies[0].orcs[myResult].leftLeg;
                    player.rightLeg.sprite = tempItemSprites.spacies[0].orcs[myResult].rightLeg;
                    break;
                case 3://theOrders
                    player.head.sprite = tempItemSprites.spacies[0].theOrders[myResult].head;
                    player.body.sprite = tempItemSprites.spacies[0].theOrders[myResult].body;
                    player.leftArm.sprite = tempItemSprites.spacies[0].theOrders[myResult].leftArm;
                    player.rightArm.sprite = tempItemSprites.spacies[0].theOrders[myResult].rightArm;
                    player.leftLeg.sprite = tempItemSprites.spacies[0].theOrders[myResult].leftLeg;
                    player.rightLeg.sprite = tempItemSprites.spacies[0].theOrders[myResult].rightLeg;
                    break;
            }
        }

        //parts 종류 , myResult 파츠 범위 : 캐릭터에게 이미지 연결하는 헤어함수
        void useForHair(int parts, int myResult)
        {
            //종류
            switch (parts)
            {
                case 0://긴 머리
                    player.myHair.sprite = tempItemSprites.hairs[0].longHair[myResult];
                    break;
                case 1://단발 머리
                    player.myHair.sprite = tempItemSprites.hairs[0].shortHair[myResult];
                    break;
                case 2://스포츠 머리
                    player.myHair.sprite = tempItemSprites.hairs[0].sportHair[myResult];
                    break;
            }
        }

        //parts 종류 , myResult 파츠 범위 : 캐릭터에게 이미지 연결하는 눈 관련 함수
        void useForEye(int parts, int myResult)
        {
            //종류
            switch (parts)
            {
                case 0://눈매 0~5까지
                    player.myEyes[0].myLeftBackEye.sprite = tempItemSprites.eyes[myResult].backEyes; //왼쪽
                    player.myEyes[0].myRightBackEye.sprite = tempItemSprites.eyes[myResult].backEyes;//오른쪽
                    break;
                case 1://눈동자 중복 제외
                    if (myResult == 0)
                    {
                        player.myEyes[0].myLeftFrontEye.sprite = tempItemSprites.eyes[0].frontEyes; //왼쪽
                        player.myEyes[0].myRightFrontEye.sprite = tempItemSprites.eyes[0].frontEyes;//오른쪽
                    }
                    if (myResult == 1)
                    {
                        player.myEyes[0].myLeftFrontEye.sprite = tempItemSprites.eyes[1].frontEyes; //왼쪽
                        player.myEyes[0].myRightFrontEye.sprite = tempItemSprites.eyes[1].frontEyes;//오른쪽
                    }
                    if (myResult == 2)
                    {
                        player.myEyes[0].myLeftFrontEye.sprite = tempItemSprites.eyes[4].frontEyes; //왼쪽
                        player.myEyes[0].myRightFrontEye.sprite = tempItemSprites.eyes[4].frontEyes;//오른쪽
                    }
                    if (myResult == 3)
                    {
                        player.myEyes[0].myLeftFrontEye.sprite = tempItemSprites.eyes[9].frontEyes; //왼쪽
                        player.myEyes[0].myRightFrontEye.sprite = tempItemSprites.eyes[9].frontEyes;//오른쪽
                    }
                    break;
            }
        }
        
        public void OnClick_RandomHairColor_Btn()
        {
            isClick_RandomHairColorBtn = true;

            //0~ 1000까지 랜덤숫자
            random_intR = Random.Range(0, 1000);
            random_intG = Random.Range(0, 1000);
            random_intB = Random.Range(0, 1000);

            // 0.001 곱하기 
             random_floatR = random_intR * 0.001f;
             random_floatG = random_intG * 0.001f;
             random_floatB = random_intB * 0.001f;

            //업데이트 함수에 대입
        }

        public void OnClick_RandomEyeColor_Btn()
        {
            isClick_RandomEyeColorBtn = true;

            //0~ 1000까지 랜덤숫자
            random_intR = Random.Range(0, 1000);
            random_intG = Random.Range(0, 1000);
            random_intB = Random.Range(0, 1000);

            // 0.001 곱하기
            random_floatR = random_intR * 0.001f;
            random_floatG = random_intG * 0.001f;
            random_floatB = random_intB * 0.001f;
            
            //업데이트 함수에 대입
        }

    }//class
}//namespace
