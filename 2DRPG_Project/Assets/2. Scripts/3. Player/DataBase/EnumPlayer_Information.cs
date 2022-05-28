using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;
using TMPro;

// 이넘형 묶음 : (캐릭터 상태, 부위별 장착가능 요소, 돈...) 게임 캐릭터가 가지고 있는 스크립트
namespace Main
{
    public class EnumPlayer_Information : MonoBehaviour
    {
        //머리 베이스
        public SpriteRenderer head;

        //몸 베이스
        public SpriteRenderer body;

        //왼팔 베이스
        public SpriteRenderer leftArm;

        //오른팔 베이스
        public SpriteRenderer rightArm;

        //왼다리 베이스
        public SpriteRenderer leftLeg;

        //오른다리 베이스
        public SpriteRenderer rightLeg;

        //헤어
        public SpriteRenderer myHair;

        //눈관련 스프라이트렌더러 배열 그룹
        public MyEyesGroup[] myEyes;

        //얼굴 화장
        public SpriteRenderer faceMakeUp;

        //수염
        public SpriteRenderer myFaceHair;

        //윗 옷 배열 그룹
        public MyClothGroup[] myCloths;

        //바지 배열 그룹
        public MyLegsGroup[] myLegs;

        //망또
        public SpriteRenderer back;

        //벨트 악세사리
        public Accessory[] myAccessory;

        //핼멧
        public SpriteRenderer helmet;

        //갑옷 배열 그룹
        public MyArmorGroup[] myArmor;

        //손 장착 그룹
        public MyHandsGroup[] myHands;

        //애완동물 
        public SpriteRenderer myPet;

        //헤어 색상 (2/17추가)
        public Color myHairColor = new Color();

        //눈동자 색상 (2/17추가)
        public Color myLeftEyeColor = new Color();//왼눈
        public Color myRightEyeColor = new Color();//오른눈

        public MyHorseGroup[] myHorse;

        //능력치
        public int myHp;
        public int myMp;
        public float myEXP;
        public int myLevel;

        //스텟
        public int mySTR;
        public int myDEX;
        public int myINT;
        public int myLUCK;

        //소유금액
        public int myCoin;

        //소유보석
        public int myGold;

        //아이템에 의한 추가 능력치
        public float myAttack;
        public float myAcurrancy;
        public float myCritical;
        public float myDefense;
        public float myEvationRate;

        //체력 마력 회복률
        public int myHpCure;
        public int myMpCure;

        //이름 등록 (인스펙터)
        public TextMeshProUGUI myName;
        
        //캐릭터가 만들어져 있는지 판단 //캐릭터 있다면 삭제 전까지는 true
        public bool isMade;

        //각 파츠 착용 여부에 따라 : 파츠 오브젝트 활성화 비활성화 제어 (gameData 제어가능)
        


        [System.Serializable]
        public struct MyEyesGroup
        {
            [SerializeField]
            public SpriteRenderer myLeftBackEye;
            [SerializeField]
            public SpriteRenderer myRightBackEye;
            [SerializeField]
            public SpriteRenderer myLeftFrontEye;
            [SerializeField]
            public SpriteRenderer myRightFrontEye;
        }

        [System.Serializable]
        public struct MyClothGroup
        {
            [SerializeField]
            public SpriteRenderer myBodyCloth;
            [SerializeField]
            public SpriteRenderer myLeftClothArm;
            [SerializeField]
            public SpriteRenderer myRightClothArm;
        }

        [System.Serializable]
        public struct MyLegsGroup
        {
            [SerializeField]
            public SpriteRenderer myLeftLeg;
            [SerializeField]
            public SpriteRenderer myRightLeg;
        }

        [System.Serializable]
        public struct MyArmorGroup
        {
            [SerializeField]
            public SpriteRenderer myBody;
            [SerializeField]
            public SpriteRenderer myLeftArm;
            [SerializeField]
            public SpriteRenderer myRightArm;
        }

        [System.Serializable]
        public struct MyHandsGroup
        {
            [SerializeField]
            public SpriteRenderer myRightWeapon;
            [SerializeField]
            public SpriteRenderer myLeftWeapon;
            [SerializeField]
            public SpriteRenderer myRightShield;
            [SerializeField]
            public SpriteRenderer myLeftShield;
        }

        [System.Serializable]
        public struct MyHorseGroup
        {
            [SerializeField]
            public SpriteRenderer head;
            [SerializeField]
            public SpriteRenderer neck;

            [SerializeField]
            public SpriteRenderer chestBody;
            [SerializeField]
            public SpriteRenderer acc;
            [SerializeField]
            public SpriteRenderer backBody;

            [SerializeField]
            public SpriteRenderer frontTopLeg;
            [SerializeField]
            public SpriteRenderer frontBottomLeg;

            [SerializeField]
            public SpriteRenderer backTopLeg;
            [SerializeField]
            public SpriteRenderer backBottomLeg;

            [SerializeField]
            public SpriteRenderer tail;

        }

        [System.Serializable]
        public struct Accessory
        {
            [SerializeField]
            public SpriteRenderer belt;

            [SerializeField]
            public SpriteRenderer cross;
        }
    }//class EnumPlayer_Information



}//namespace InGame_Rougelike
