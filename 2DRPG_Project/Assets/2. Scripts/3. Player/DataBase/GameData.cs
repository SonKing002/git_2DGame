using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

using Main;

// �ɸ��� 4���� ������ (�ܼ���� ������ ��ũ��Ʈ) 

namespace Main
{
    [Serializable]//����ȭ
    public class GameData
    {
        //�÷��̾�� 4�� //���� ���ӿ�����Ʈ -> �ٸ� ��ũ��Ʈ
        //public EnumPlayer_Information[] players;
        //Ŭ������ �޾ƿ� �� ����.
        //������Ʈ�� �־ �޾ƿ� ���� ����.

        //�̹��� �������� �޾ƿ��� ���ؼ�
//------�������� ������
        //�Ӹ� ���̽�
        public Sprite[] heads = new Sprite[4];
        
        //�� ���̽�
        public Sprite[] bodies = new Sprite[4];
        
        //���� ���̽�
        public Sprite[] leftArms = new Sprite[4];

        //������ ���̽�
        public Sprite[] rightArms = new Sprite[4];

        //�޴ٸ� ���̽�
        public Sprite[] leftLegs = new Sprite[4];
   
        //�����ٸ� ���̽�
        public Sprite[] rightLegs = new Sprite[4];

        //���
        public Sprite[] myHairs = new Sprite[4];
        public bool[] isHairTrue = new bool[4]; //setActive == false �θӸ�

        //��� ���� ���� (2/17 �߰�)
        public Color[] myHairColors = new Color[4];

        //������ ��������Ʈ������ �迭 �׷�
        public Sprite[] myLeftBackEyes = new Sprite[4];
        public Sprite[] myRightBackEyes = new Sprite[4];
        public Sprite[] myLeftFrontEyes = new Sprite[4];
        public Sprite[] myRightFrontEyes = new Sprite[4];
        
        //�� ���� ���� (2/17 �߰�)
        public Color[] myLeftFrontEyeColors = new Color[4];  //�޴�
        public Color[] myRightFrontEyeColors = new Color[4]; //������
    
        //���� (������)
        public Sprite[] myFaceHairs = new Sprite[4];

        //�� �� �迭 �׷�
        public Sprite[] myBodyCloths = new Sprite[4];
        public Sprite[] myLeftClothArms = new Sprite[4];
        public Sprite[] myRightClothArms = new Sprite[4];
        public bool[] isClothTrue = new bool[4]; //setActive 

        //���� �迭 �׷�
        public Sprite[] myleftPantsLegs = new Sprite[4];
        public Sprite[] myRightPantsLegs = new Sprite[4];
        public bool[] isPantsTrue = new bool[4]; //setActive 

        //����
        public Sprite[] backs = new Sprite[4];
        public bool[] isBackTrue = new bool[4]; //setActive 

        //��Ʈ �Ǽ��縮
        public Sprite[] belts = new Sprite[4];
        public Sprite[] crosses = new Sprite[4];
        public bool[] isBeltTrue = new bool[4]; //setActive 

        //�۸�
        public Sprite[] helmets = new Sprite[4];
        public bool[] isHelmetTrue = new bool[4]; //setActive 

        //���� �迭 �׷�
        public Sprite[] myBodyArmores = new Sprite[4];
        public Sprite[] myLeftShoulderArmers = new Sprite[4];
        public Sprite[] myRightShoulderArmers = new Sprite[4];
        public bool[] isArmorTrue = new bool[4]; //setActive

        //�� ���� �׷�
        public Sprite[] myRightWeapons = new Sprite[4];
        public Sprite[] myLeftWeapons = new Sprite[4];
        public bool[] isLeftHandTrue = new bool[4]; //setActive

        public Sprite[] myRightShields = new Sprite[4];
        public Sprite[] myLeftShields = new Sprite[4];
        public bool[] isWriteHandTrue = new bool[4]; //setActive
//------�������� ������

        //�ɷ�ġ
        public int[] myHps = new int[4];
        public int[] myMps = new int[4];
        public float[] myEXPs = new float[4];
        public int[] myLevels = new int[4];

        //����
        public int[] mySTRs = new int[4];
        public int[] myDEXs = new int[4];
        public int[] myINTs = new int[4];
        public int[] myLUCKs = new int[4];

        //�����ݾ�
        public int[] myCoins = new int[4];

        //��������
        public int[] myGolds = new int[4];


        //Item�� �ִ� enum EquipParts ���� 
        //0 ����, 1����, 2���, 3����, 4����, 5�㸮��, 6ũ�ν� , 7��ű� �����, 8��ű� ����, 9����, 10����

        //string�� �����Ͽ� ��� ������ �������� ����

        //�÷��̾�1
        public string[] equiptable_ItemDataName1 = new string[11];//��������� �ε�������
        public bool[] isEqiup1 = new bool[11]; //��������� ���� ����
        //�÷��̾�2
        public string[] equiptable_ItemDataName2 = new string[11];//��������� �ε�������
        public bool[] isEqiup2 = new bool[11]; //��������� ���� ����
        //�÷��̾�3
        public string[] equiptable_ItemDataName3 = new string[11];//��������� �ε�������
        public bool[] isEqiup3 = new bool[11]; //��������� ���� ����
        //�÷��̾�4
        public string[] equiptable_ItemDataName4 = new string[11];//��������� �ε�������
        public bool[] isEqiup4 = new bool[11]; //��������� ���� ����

        //�����ۿ� ���� �߰� �ɷ�ġ
        public float[] myAttacks = new float[4];
        public float[] myAcurrancies = new float[4];
        public float[] myCriticals = new float[4];
        public float[] myDefenses = new float[4];
        public float[] myEvationRates = new float[4];

        //ü�� ���� ȸ����
        public int[] myHpCures = new int[4];
        public int[] myMpCures = new int[4];

        //�̸� ��� (�ν�����)//string�� �ְ� text���� �����ϱ�� ����
        public string[] myNames = new string[4];

        //ĳ���Ͱ� ������� �ִ��� �Ǵ� //ĳ���� �ִٸ� ���� �������� true
        public bool[] isMades = new bool[4];

        //�κ��丮
        //������ ������ ��������(�ε��� ����) //true�� ���� �������� ������� ��ġ
        public bool[] isOpened_player1Equiptable = new bool[35];
        public List<int> player1EquiptableItem = new List<int>();

        public bool[] isOpened_player2Equiptable = new bool[35];
        public List<int> player2EquiptableItem = new List<int>();

        public bool[] isOpened_player3Equiptable = new bool[35];
        public List<int> player3EquiptableItem = new List<int>();

        public bool[] isOpened_player4Equiptable = new bool[35];
        public List<int> player4EquiptableItem = new List<int>();

        //���� ĳ���Ͱ� ���õǾ����� �Ǵ� //ĳ���� ���� �� �÷��� �ϴ� ���ȱ����� true
        public bool[] isButtonSelected = new bool[4];

        //���� ��������ε��� (����� 5)
        public int nowUsingIndex;

        //�� é���� ��� ����
        public bool[] isClear2 = new bool[4];
        public bool[] isClear3 = new bool[4];
        public bool[] isClear4 = new bool[4];
        public bool[] isClear5 = new bool[4];
    }
}
