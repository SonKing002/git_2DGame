using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

using Main;

// 케릭터 4명의 데이터 (단순기록 보관용 스크립트) 

namespace Main
{
    [Serializable]//직렬화
    public class GameData
    {
        //플래이어들 4명 //각각 게임오브젝트 -> 다른 스크립트
        //public EnumPlayer_Information[] players;
        //클래스를 받아올 수 없다.
        //오브젝트를 넣어서 받아올 수도 없다.

        //이미지 파츠별로 받아오기 위해서
//------착용중인 아이템
        //머리 베이스
        public Sprite[] heads = new Sprite[4];
        
        //몸 베이스
        public Sprite[] bodies = new Sprite[4];
        
        //왼팔 베이스
        public Sprite[] leftArms = new Sprite[4];

        //오른팔 베이스
        public Sprite[] rightArms = new Sprite[4];

        //왼다리 베이스
        public Sprite[] leftLegs = new Sprite[4];
   
        //오른다리 베이스
        public Sprite[] rightLegs = new Sprite[4];

        //헤어
        public Sprite[] myHairs = new Sprite[4];
        public bool[] isHairTrue = new bool[4]; //setActive == false 민머리

        //헤어 색상 정보 (2/17 추가)
        public Color[] myHairColors = new Color[4];

        //눈관련 스프라이트렌더러 배열 그룹
        public Sprite[] myLeftBackEyes = new Sprite[4];
        public Sprite[] myRightBackEyes = new Sprite[4];
        public Sprite[] myLeftFrontEyes = new Sprite[4];
        public Sprite[] myRightFrontEyes = new Sprite[4];
        
        //눈 색상 정보 (2/17 추가)
        public Color[] myLeftFrontEyeColors = new Color[4];  //왼눈
        public Color[] myRightFrontEyeColors = new Color[4]; //오른눈
    
        //수염 (사용안함)
        public Sprite[] myFaceHairs = new Sprite[4];

        //윗 옷 배열 그룹
        public Sprite[] myBodyCloths = new Sprite[4];
        public Sprite[] myLeftClothArms = new Sprite[4];
        public Sprite[] myRightClothArms = new Sprite[4];
        public bool[] isClothTrue = new bool[4]; //setActive 

        //바지 배열 그룹
        public Sprite[] myleftPantsLegs = new Sprite[4];
        public Sprite[] myRightPantsLegs = new Sprite[4];
        public bool[] isPantsTrue = new bool[4]; //setActive 

        //망또
        public Sprite[] backs = new Sprite[4];
        public bool[] isBackTrue = new bool[4]; //setActive 

        //벨트 악세사리
        public Sprite[] belts = new Sprite[4];
        public Sprite[] crosses = new Sprite[4];
        public bool[] isBeltTrue = new bool[4]; //setActive 

        //핼멧
        public Sprite[] helmets = new Sprite[4];
        public bool[] isHelmetTrue = new bool[4]; //setActive 

        //갑옷 배열 그룹
        public Sprite[] myBodyArmores = new Sprite[4];
        public Sprite[] myLeftShoulderArmers = new Sprite[4];
        public Sprite[] myRightShoulderArmers = new Sprite[4];
        public bool[] isArmorTrue = new bool[4]; //setActive

        //손 장착 그룹
        public Sprite[] myRightWeapons = new Sprite[4];
        public Sprite[] myLeftWeapons = new Sprite[4];
        public bool[] isLeftHandTrue = new bool[4]; //setActive

        public Sprite[] myRightShields = new Sprite[4];
        public Sprite[] myLeftShields = new Sprite[4];
        public bool[] isWriteHandTrue = new bool[4]; //setActive
//------착용중인 아이템

        //능력치
        public int[] myHps = new int[4];
        public int[] myMps = new int[4];
        public float[] myEXPs = new float[4];
        public int[] myLevels = new int[4];

        //스텟
        public int[] mySTRs = new int[4];
        public int[] myDEXs = new int[4];
        public int[] myINTs = new int[4];
        public int[] myLUCKs = new int[4];

        //소유금액
        public int[] myCoins = new int[4];

        //소유보석
        public int[] myGolds = new int[4];


        //Item에 있는 enum EquipParts 순서 
        //0 윗옷, 1바지, 2헬멧, 3갑옷, 4망또, 5허리띠, 6크로스 , 7장신구 목걸이, 8장신구 반지, 9무기, 10방패

        //string을 저장하여 장비 정보를 가져오기 위함

        //플래이어1
        public string[] equiptable_ItemDataName1 = new string[11];//장비파츠의 인덱스저장
        public bool[] isEqiup1 = new bool[11]; //장비파츠의 불형 저장
        //플래이어2
        public string[] equiptable_ItemDataName2 = new string[11];//장비파츠의 인덱스저장
        public bool[] isEqiup2 = new bool[11]; //장비파츠의 불형 저장
        //플래이어3
        public string[] equiptable_ItemDataName3 = new string[11];//장비파츠의 인덱스저장
        public bool[] isEqiup3 = new bool[11]; //장비파츠의 불형 저장
        //플래이어4
        public string[] equiptable_ItemDataName4 = new string[11];//장비파츠의 인덱스저장
        public bool[] isEqiup4 = new bool[11]; //장비파츠의 불형 저장

        //아이템에 의한 추가 능력치
        public float[] myAttacks = new float[4];
        public float[] myAcurrancies = new float[4];
        public float[] myCriticals = new float[4];
        public float[] myDefenses = new float[4];
        public float[] myEvationRates = new float[4];

        //체력 마력 회복률
        public int[] myHpCures = new int[4];
        public int[] myMpCures = new int[4];

        //이름 등록 (인스펙터)//string을 주고 text에서 연결하기로 하자
        public string[] myNames = new string[4];

        //캐릭터가 만들어져 있는지 판단 //캐릭터 있다면 삭제 전까지는 true
        public bool[] isMades = new bool[4];

        //인벤토리
        //아이템 갯수가 가변적임(인덱스 저장) //true인 곳에 아이템을 순서대로 배치
        public bool[] isOpened_player1Equiptable = new bool[35];
        public List<int> player1EquiptableItem = new List<int>();

        public bool[] isOpened_player2Equiptable = new bool[35];
        public List<int> player2EquiptableItem = new List<int>();

        public bool[] isOpened_player3Equiptable = new bool[35];
        public List<int> player3EquiptableItem = new List<int>();

        public bool[] isOpened_player4Equiptable = new bool[35];
        public List<int> player4EquiptableItem = new List<int>();

        //현재 캐릭터가 선택되었는지 판단 //캐릭터 선택 후 플래이 하는 동안까지는 true
        public bool[] isButtonSelected = new bool[4];

        //지금 사용중인인덱스 (종료시 5)
        public int nowUsingIndex;

        //각 챕터의 잠금 여부
        public bool[] isClear2 = new bool[4];
        public bool[] isClear3 = new bool[4];
        public bool[] isClear4 = new bool[4];
        public bool[] isClear5 = new bool[4];
    }
}
