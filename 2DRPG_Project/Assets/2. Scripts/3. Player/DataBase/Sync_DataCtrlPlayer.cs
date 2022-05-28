using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;

using System.Linq;

namespace Main
{
    //json <-> ĳ����[�ε���] ����
    //����� �ҷ����� ���������� ����ϱ� ���� �ۼ��صδ� ��ũ��Ʈ

    //(���� �����Ͽ� �������� �����ϰ� ĳ���Ϳ� ������ ���� ���� : ������ �Ұ����ϱ� ������)
    //������ �ʿ��� �������� ��ũ��Ʈ�� ȣ���� �� �ֵ��� �� ��

    // �߰� ������ ���� ���濡 ���� �ɷ�ġ ��ȭ : Update ó��
    // ��ũ (������������ ������� üũ�� �� �ɷ�ġ���� �÷��̾����� ����)
    public class Sync_DataCtrlPlayer : MonoBehaviour
    {
        //Ÿ ��ũ��Ʈ
        public EnumPlayer_Information myPlayer;
        public DataController saveData;

        //�߰� ��ũ (������ ���� ��ũ��Ʈ)
        public Item equiptable_Items;

        public ChangeCtrl changeCtrl;//���� �ݶ��̴� ����ȭ�ϱ� ����

        //��, ����, ����, ����, ����, ����, ����, (�����, ����,) ��Ʈ, ũ�ν� ��

        //������ �����ϰ� �ִ��� Ȯ���ϱ� ����
        //�ٽ�public bool[] isEquipOnce = new bool[9]; //�� ���뿩�ο� ���� ���������ϸ� ���� �����۸�ŭ �ɷ�ġ�� �����.

        //������ �ٲܶ� �� �ѹ��� update���� ������
        //�ٽ�public bool[] isChanged = new bool[9];

        //public int tempIndex_For; //for ���� i �� � �Լ��� �Ű������� ����ϱ� ����

        //�� ������ �ε����� �ο� == ã�� �����ϱ� ����
        //public int[] tempItemList_Index = new int[9];
        //public int[] tempPrevList_Index = new int[9];

        //����ϰ� �ִ� ĳ���� 
        public int tempNowUsingIndex;


        void Start()
        {
            //����ϰ� �ִ� ĳ���� �ε��� ����ȭ
            tempNowUsingIndex = saveData.gameData.nowUsingIndex;
            //�ű� �����̶�� �׳� ���� �̹����� �����ϴ°ɷ� ������
            //NewAvatorCtrl���� isSaveNow bool���� true �϶�
            //(���߿� ������ ���� ��� �������� �װ��� �߰��ϴ� �ɷ�) �� �̹����� �������� �޾ƿ´�.

        }

        //(���� �ҷ�����) �����Ϳ��� -> �÷��̾�� ������ �־��ֱ�
        public void Match_SaveData_ToPlayer() //0,1,2,3
        {
            //�����ϴ� ĳ���Ͷ��
            if (saveData.gameData.isMades[tempNowUsingIndex])
            {
                //ü�� ���� ����ġ ����
                myPlayer.myHp = saveData.gameData.myHps[tempNowUsingIndex];
                myPlayer.myMp = saveData.gameData.myMps[tempNowUsingIndex];
                myPlayer.myEXP = saveData.gameData.myEXPs[tempNowUsingIndex];
                myPlayer.myLevel = saveData.gameData.myLevels[tempNowUsingIndex];

                //�� ��ø ���� ���
                myPlayer.mySTR = saveData.gameData.mySTRs[tempNowUsingIndex];
                myPlayer.myDEX = saveData.gameData.myDEXs[tempNowUsingIndex];
                myPlayer.myINT = saveData.gameData.myINTs[tempNowUsingIndex];
                myPlayer.myLUCK = saveData.gameData.myLUCKs[tempNowUsingIndex];

                //���� ���
                myPlayer.myCoin = saveData.gameData.myCoins[tempNowUsingIndex];
                myPlayer.myGold = saveData.gameData.myGolds[tempNowUsingIndex];
              
                //Ȱ��ȭ ��Ȱ��ȭ ���� üũ
                //���
                myPlayer.myHair.gameObject.SetActive(saveData.gameData.isHairTrue[tempNowUsingIndex]);
                //����
                myPlayer.myCloths[0].myBodyCloth.gameObject.SetActive(saveData.gameData.isClothTrue[tempNowUsingIndex]);
                //����
                myPlayer.myLegs[0].myLeftLeg.gameObject.SetActive(saveData.gameData.isPantsTrue[tempNowUsingIndex]);
                //����
                myPlayer.helmet.gameObject.SetActive(saveData.gameData.isHelmetTrue[tempNowUsingIndex]);
                //����
                myPlayer.myArmor[0].myBody.gameObject.SetActive(saveData.gameData.isArmorTrue[tempNowUsingIndex]);
                //����
                myPlayer.back.gameObject.SetActive(saveData.gameData.isBackTrue[tempNowUsingIndex]);
                //����
                myPlayer.myHands[0].myRightShield.gameObject.SetActive(saveData.gameData.isWriteHandTrue[tempNowUsingIndex]);
                //����
                myPlayer.myHands[0].myLeftWeapon.gameObject.SetActive(saveData.gameData.isLeftHandTrue[tempNowUsingIndex]);

                //���� ( �Ӹ�, ��, ����, ������, �޴ٸ�, �����ٸ�, ��� )          
                myPlayer.head.sprite = saveData.gameData.heads[tempNowUsingIndex];
                myPlayer.body.sprite = saveData.gameData.bodies[tempNowUsingIndex];
                myPlayer.leftArm.sprite = saveData.gameData.leftArms[tempNowUsingIndex];
                myPlayer.rightArm.sprite = saveData.gameData.rightArms[tempNowUsingIndex];
                myPlayer.leftLeg.sprite = saveData.gameData.leftLegs[tempNowUsingIndex];
                myPlayer.rightArm.sprite = saveData.gameData.rightArms[tempNowUsingIndex];
                myPlayer.myHair.sprite = saveData.gameData.myHairs[tempNowUsingIndex];

                //������ ( ���� ���̶���, ������ ���̶���, ���� ������, ������ ������ )
                myPlayer.myEyes[0].myLeftBackEye.sprite = saveData.gameData.myLeftBackEyes[tempNowUsingIndex];
                myPlayer.myEyes[0].myRightBackEye.sprite = saveData.gameData.myRightBackEyes[tempNowUsingIndex];
                myPlayer.myEyes[0].myLeftFrontEye.sprite = saveData.gameData.myLeftFrontEyes[tempNowUsingIndex];
                myPlayer.myEyes[0].myRightFrontEye.sprite = saveData.gameData.myRightFrontEyes[tempNowUsingIndex];

                //ü��ȸ����, ����ȸ����
                myPlayer.myHpCure = saveData.gameData.myHpCures[tempNowUsingIndex];
                myPlayer.myMpCure = saveData.gameData.myMpCures[tempNowUsingIndex];

                //����
                myPlayer.myFaceHair.sprite = saveData.gameData.myFaceHairs[tempNowUsingIndex];

                //���� ( ����, ����, ������ )
                myPlayer.myCloths[0].myBodyCloth.sprite = saveData.gameData.myBodyCloths[tempNowUsingIndex];
                myPlayer.myCloths[0].myLeftClothArm.sprite = saveData.gameData.myLeftClothArms[tempNowUsingIndex];
                myPlayer.myCloths[0].myRightClothArm.sprite = saveData.gameData.myRightClothArms[tempNowUsingIndex];

                //���� (�޴ٸ�, �����ٸ�)
                myPlayer.myLegs[0].myLeftLeg.sprite = saveData.gameData.myleftPantsLegs[tempNowUsingIndex];
                myPlayer.myLegs[0].myRightLeg.sprite = saveData.gameData.myRightPantsLegs[tempNowUsingIndex];

                //����
                myPlayer.back.sprite = saveData.gameData.backs[tempNowUsingIndex];

                //���� (����, �޾��, �������)
                myPlayer.myArmor[0].myBody.sprite = saveData.gameData.myBodyArmores[tempNowUsingIndex];
                myPlayer.myArmor[0].myLeftArm.sprite = saveData.gameData.myLeftShoulderArmers[tempNowUsingIndex];
                myPlayer.myArmor[0].myRightArm.sprite = saveData.gameData.myRightShoulderArmers[tempNowUsingIndex];

                //�� ����(������ �޼�)
                myPlayer.myHands[0].myLeftWeapon.sprite = saveData.gameData.myLeftWeapons[tempNowUsingIndex];
                myPlayer.myHands[0].myRightWeapon.sprite = saveData.gameData.myRightWeapons[tempNowUsingIndex];
                myPlayer.myHands[0].myLeftShield.sprite = saveData.gameData.myLeftShields[tempNowUsingIndex];
                myPlayer.myHands[0].myRightShield.sprite = saveData.gameData.myRightShields[tempNowUsingIndex];

                //������ �ݶ��̴� ����ȭ
                changeCtrl.Weapon_Colider(myPlayer.myHands[0].myLeftWeapon.sprite.name);

                //���� (2/17 �߰�)
                //�� ���� ����
                myPlayer.myHairColor = saveData.gameData.myHairColors[tempNowUsingIndex];
                //��� �÷� ����
                myPlayer.myHair.color = myPlayer.myHairColor;
                //�� ���� ����
                myPlayer.myLeftEyeColor = saveData.gameData.myLeftFrontEyeColors[tempNowUsingIndex];
                //�޴������÷� ����
                myPlayer.myEyes[0].myLeftFrontEye.color = myPlayer.myLeftEyeColor;
                //�� ���� ����
                myPlayer.myRightEyeColor = saveData.gameData.myRightFrontEyeColors[tempNowUsingIndex];
                //���� ������ �÷� ����
                myPlayer.myEyes[0].myRightFrontEye.color = myPlayer.myRightEyeColor;
            }
            else//�ű� ĳ���Ͷ��
            {
                //ü�� ���� ����ġ ����
                myPlayer.myHp = 100;
                myPlayer.myMp = 100;
                myPlayer.myEXP = 0;
                myPlayer.myLevel = 1;

                //�� ��ø ���� ���
                myPlayer.mySTR = 4;
                myPlayer.myDEX = 4;
                myPlayer.myINT = 4;
                myPlayer.myLUCK = 4;

                //���� ���
                myPlayer.myCoin = 100;
                myPlayer.myGold = 100;
          
                myPlayer.myHairColor = myPlayer.myHair.color;
                myPlayer.myLeftEyeColor = myPlayer.myEyes[0].myLeftFrontEye.color;
                myPlayer.myRightEyeColor = myPlayer.myEyes[0].myRightFrontEye.color;
            }

            //�̸�, �������� �Ǵ�
            if (saveData.gameData.myNames[tempNowUsingIndex] != null)
            {
                myPlayer.myName.text = saveData.gameData.myNames[tempNowUsingIndex];
            }
            myPlayer.isMade = saveData.gameData.isMades[tempNowUsingIndex];
        }

        //(���� �ҷ�����) �÷��̾�� -> �����ͷ� ������ �־��ֱ�
        public void Match_Players_ToSaveData() //0,1,2,3
        {
            //ü�� ���� ����ġ ����
            saveData.gameData.myHps[tempNowUsingIndex] = myPlayer.myHp;
            saveData.gameData.myMps[tempNowUsingIndex] = myPlayer.myMp;
            saveData.gameData.myEXPs[tempNowUsingIndex] = myPlayer.myEXP;
            saveData.gameData.myLevels[tempNowUsingIndex] = myPlayer.myLevel;

            //�� ��ø ���� ���
            saveData.gameData.mySTRs[tempNowUsingIndex] = myPlayer.mySTR;
            saveData.gameData.myDEXs[tempNowUsingIndex] = myPlayer.myDEX;
            saveData.gameData.myINTs[tempNowUsingIndex] = myPlayer.myINT;
            saveData.gameData.myLUCKs[tempNowUsingIndex] = myPlayer.myLUCK;

            //���� ���
            saveData.gameData.myCoins[tempNowUsingIndex] = myPlayer.myCoin;
            saveData.gameData.myGolds[tempNowUsingIndex] = myPlayer.myGold;

            //���ݷ�, ���߷�, ġ���, ����, ȸ����
            saveData.gameData.myAttacks[tempNowUsingIndex] = myPlayer.myAttack;
            saveData.gameData.myAcurrancies[tempNowUsingIndex] = myPlayer.myAcurrancy;
            saveData.gameData.myCriticals[tempNowUsingIndex] = myPlayer.myCritical;
            saveData.gameData.myDefenses[tempNowUsingIndex] = myPlayer.myDefense;
            saveData.gameData.myEvationRates[tempNowUsingIndex] = myPlayer.myEvationRate;

            //ü��ȸ����, ����ȸ����
            saveData.gameData.myHpCures[tempNowUsingIndex] = myPlayer.myHpCure;
            saveData.gameData.myMpCures[tempNowUsingIndex] = myPlayer.myMpCure;

            //�̸�, �������� �Ǵ�
            saveData.gameData.myNames[tempNowUsingIndex] = myPlayer.myName.text;
            saveData.gameData.isMades[tempNowUsingIndex] = myPlayer.isMade;

            //���� ( �Ӹ�, ��, ����, ������, �޴ٸ�, �����ٸ�, ��� )
            saveData.gameData.heads[tempNowUsingIndex] = myPlayer.head.sprite;
            saveData.gameData.bodies[tempNowUsingIndex] = myPlayer.body.sprite;
            saveData.gameData.leftArms[tempNowUsingIndex] = myPlayer.leftArm.sprite;
            saveData.gameData.rightArms[tempNowUsingIndex] = myPlayer.rightArm.sprite;
            saveData.gameData.leftLegs[tempNowUsingIndex] = myPlayer.leftLeg.sprite;
            saveData.gameData.rightArms[tempNowUsingIndex] = myPlayer.rightArm.sprite;
            saveData.gameData.myHairs[tempNowUsingIndex] = myPlayer.myHair.sprite;

            //������ ( ���� ���̶���, ������ ���̶���, ���� ������, ������ ������ )
            saveData.gameData.myLeftBackEyes[tempNowUsingIndex] = myPlayer.myEyes[0].myLeftBackEye.sprite;
            saveData.gameData.myRightBackEyes[tempNowUsingIndex] = myPlayer.myEyes[0].myRightBackEye.sprite;
            saveData.gameData.myLeftFrontEyes[tempNowUsingIndex] = myPlayer.myEyes[0].myLeftFrontEye.sprite;
            saveData.gameData.myRightFrontEyes[tempNowUsingIndex] = myPlayer.myEyes[0].myRightFrontEye.sprite;

            //����
            saveData.gameData.myFaceHairs[tempNowUsingIndex] = myPlayer.myFaceHair.sprite;

            //���� ( ����, ����, ������ )
            saveData.gameData.myBodyCloths[tempNowUsingIndex] = myPlayer.myCloths[0].myBodyCloth.sprite;
            saveData.gameData.myLeftClothArms[tempNowUsingIndex] = myPlayer.myCloths[0].myLeftClothArm.sprite;
            saveData.gameData.myRightClothArms[tempNowUsingIndex] = myPlayer.myCloths[0].myRightClothArm.sprite;

            //���� (�޴ٸ�, �����ٸ�)
            saveData.gameData.myleftPantsLegs[tempNowUsingIndex] = myPlayer.myLegs[0].myLeftLeg.sprite;
            saveData.gameData.myRightPantsLegs[tempNowUsingIndex] = myPlayer.myLegs[0].myRightLeg.sprite;

            //����
            saveData.gameData.backs[tempNowUsingIndex] = myPlayer.back.sprite;

            //���� (����, �޾��, �������)
            saveData.gameData.myBodyArmores[tempNowUsingIndex] = myPlayer.myArmor[0].myBody.sprite;
            saveData.gameData.myLeftShoulderArmers[tempNowUsingIndex] = myPlayer.myArmor[0].myLeftArm.sprite;
            saveData.gameData.myRightShoulderArmers[tempNowUsingIndex] = myPlayer.myArmor[0].myRightArm.sprite;

            //�� ����(������ �޼�)
            saveData.gameData.myLeftWeapons[tempNowUsingIndex] = myPlayer.myHands[0].myLeftWeapon.sprite;
            saveData.gameData.myRightWeapons[tempNowUsingIndex] = myPlayer.myHands[0].myRightWeapon.sprite;
            saveData.gameData.myLeftShields[tempNowUsingIndex] = myPlayer.myHands[0].myLeftShield.sprite;
            saveData.gameData.myRightShields[tempNowUsingIndex] = myPlayer.myHands[0].myRightShield.sprite;

            //���� ���� (2/17 �߰�)
            saveData.gameData.myHairColors[tempNowUsingIndex] = myPlayer.myHair.color;
            saveData.gameData.myLeftFrontEyeColors[tempNowUsingIndex] = myPlayer.myEyes[0].myLeftFrontEye.color;
            saveData.gameData.myRightFrontEyeColors[tempNowUsingIndex] = myPlayer.myEyes[0].myRightFrontEye.color;

            //Ȱ��ȭ ��Ȱ��ȭ ���� üũ
            //���
            saveData.gameData.isHairTrue[tempNowUsingIndex] = myPlayer.myHair.gameObject.activeSelf;
            //����
            saveData.gameData.isClothTrue[tempNowUsingIndex] = myPlayer.myCloths[0].myBodyCloth.gameObject.activeSelf;
            //����
            saveData.gameData.isPantsTrue[tempNowUsingIndex] = myPlayer.myLegs[0].myLeftLeg.gameObject.activeSelf;
            //����
            saveData.gameData.isHelmetTrue[tempNowUsingIndex] = myPlayer.helmet.gameObject.activeSelf;
            //����
            saveData.gameData.isArmorTrue[tempNowUsingIndex] = myPlayer.myArmor[0].myBody.gameObject.activeSelf;
            //����
            saveData.gameData.isBackTrue[tempNowUsingIndex] = myPlayer.back.gameObject.activeSelf;
            //����
            saveData.gameData.isWriteHandTrue[tempNowUsingIndex] = myPlayer.myHands[0].myRightShield.gameObject.activeSelf;
            //����
            saveData.gameData.isLeftHandTrue[tempNowUsingIndex] = myPlayer.myHands[0].myLeftWeapon.gameObject.activeSelf;
        }

        /*
        //ĳ���� ������ ���� ����
        public void PlayerNum(int charNum)
        {

            //���� ������� �÷��̾�
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

        //�������� �������� �̸��� �������� ã��
        public void PlayerEquipParts_Search(string tempFindName_ToSave)
        {
            //������ �̸����κ��� ã�� Ű�� : �ҷ��� �� �ε����� ���
            string key = equiptable_Items.itemChart_Equiptable_DataName[tempFindName_ToSave][ItemField.Index];

            //������ �̸����κ��� ã�� ���� ����
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

        //usingIndexNow = ĳ���� �ּ�, equipParts = ��� ����, equiptableIndex = ��ü ����ε���
        //��ũ��Ʈ GameData�� ���� ��� �����ϱ� ���� �Լ�
        public void PlayerParts_ToSave(int usingIndexNow,EquipParts equipParts, string TempitemdataName)
        {
            //�ӽ�: ��� �����ϴºκ� �ε���
            int tempEquipParts;

            switch (equipParts)
            {
                case EquipParts.Cloth:
                    tempEquipParts = 0;
                    Player_MatchJson(usingIndexNow, tempEquipParts, TempitemdataName);//�ε��� ����

                    break;
                case EquipParts.PantsLeg:
                    tempEquipParts = 1;
                    Player_MatchJson(usingIndexNow, tempEquipParts, TempitemdataName);//�ε��� ����

                    break;
                case EquipParts.Helmet:
                    tempEquipParts = 2;
                    Player_MatchJson(usingIndexNow, tempEquipParts, TempitemdataName);//�ε��� ����

                    break;
                case EquipParts.Armor:
                    tempEquipParts = 3;
                    Player_MatchJson(usingIndexNow, tempEquipParts, TempitemdataName);//�ε��� ����

                    break;
                case EquipParts.Back:
                    tempEquipParts = 4;
                    Player_MatchJson(usingIndexNow, tempEquipParts, TempitemdataName);//�ε��� ����

                    break;
                case EquipParts.Belt:
                    tempEquipParts = 5;
                    Player_MatchJson(usingIndexNow, tempEquipParts, TempitemdataName);//�ε��� ����

                    break;
                case EquipParts.Cross:
                    tempEquipParts = 6;
                    Player_MatchJson(usingIndexNow, tempEquipParts, TempitemdataName);//�ε��� ����

                    break;
                case EquipParts.Necklace:
                    tempEquipParts = 7;
                    Player_MatchJson(usingIndexNow, tempEquipParts, TempitemdataName);//�ε��� ����

                    break;
                case EquipParts.Ring:
                    tempEquipParts = 8;
                    Player_MatchJson(usingIndexNow, tempEquipParts, TempitemdataName);//�ε��� ����

                    break;
                case EquipParts.Weapon:
                    tempEquipParts = 9;
                    Player_MatchJson(usingIndexNow, tempEquipParts, TempitemdataName);//�ε��� ����

                    break;
                case EquipParts.Shield:
                    tempEquipParts = 10;
                    Player_MatchJson(usingIndexNow, tempEquipParts, TempitemdataName);//�ε��� ����

                    break;
            }
        }

        //�ִ��� for���� �ȵ����� ���� : ���� �ε��� �����ϴ� �뵵
        //usingIndexNow = ĳ�����ε���, partIndex = �������, equiptableIndex = ������,
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
