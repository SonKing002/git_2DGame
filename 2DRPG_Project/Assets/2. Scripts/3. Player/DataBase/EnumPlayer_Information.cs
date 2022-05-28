using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;
using TMPro;

// �̳��� ���� : (ĳ���� ����, ������ �������� ���, ��...) ���� ĳ���Ͱ� ������ �ִ� ��ũ��Ʈ
namespace Main
{
    public class EnumPlayer_Information : MonoBehaviour
    {
        //�Ӹ� ���̽�
        public SpriteRenderer head;

        //�� ���̽�
        public SpriteRenderer body;

        //���� ���̽�
        public SpriteRenderer leftArm;

        //������ ���̽�
        public SpriteRenderer rightArm;

        //�޴ٸ� ���̽�
        public SpriteRenderer leftLeg;

        //�����ٸ� ���̽�
        public SpriteRenderer rightLeg;

        //���
        public SpriteRenderer myHair;

        //������ ��������Ʈ������ �迭 �׷�
        public MyEyesGroup[] myEyes;

        //�� ȭ��
        public SpriteRenderer faceMakeUp;

        //����
        public SpriteRenderer myFaceHair;

        //�� �� �迭 �׷�
        public MyClothGroup[] myCloths;

        //���� �迭 �׷�
        public MyLegsGroup[] myLegs;

        //����
        public SpriteRenderer back;

        //��Ʈ �Ǽ��縮
        public Accessory[] myAccessory;

        //�۸�
        public SpriteRenderer helmet;

        //���� �迭 �׷�
        public MyArmorGroup[] myArmor;

        //�� ���� �׷�
        public MyHandsGroup[] myHands;

        //�ֿϵ��� 
        public SpriteRenderer myPet;

        //��� ���� (2/17�߰�)
        public Color myHairColor = new Color();

        //������ ���� (2/17�߰�)
        public Color myLeftEyeColor = new Color();//�޴�
        public Color myRightEyeColor = new Color();//������

        public MyHorseGroup[] myHorse;

        //�ɷ�ġ
        public int myHp;
        public int myMp;
        public float myEXP;
        public int myLevel;

        //����
        public int mySTR;
        public int myDEX;
        public int myINT;
        public int myLUCK;

        //�����ݾ�
        public int myCoin;

        //��������
        public int myGold;

        //�����ۿ� ���� �߰� �ɷ�ġ
        public float myAttack;
        public float myAcurrancy;
        public float myCritical;
        public float myDefense;
        public float myEvationRate;

        //ü�� ���� ȸ����
        public int myHpCure;
        public int myMpCure;

        //�̸� ��� (�ν�����)
        public TextMeshProUGUI myName;
        
        //ĳ���Ͱ� ������� �ִ��� �Ǵ� //ĳ���� �ִٸ� ���� �������� true
        public bool isMade;

        //�� ���� ���� ���ο� ���� : ���� ������Ʈ Ȱ��ȭ ��Ȱ��ȭ ���� (gameData �����)
        


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
