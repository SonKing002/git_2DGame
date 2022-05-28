using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //������ 
using UnityEngine.SceneManagement; //�� �ҷ�����
using Main;
using TMPro;

namespace Main
{
    //json�� ������ ���⼭ 1,2,3,4�� �ε� ��Ŵ
    //�迭�� �̿��ϸ�,�ε��� ������ ���� ����
    //saveData.gameData.nowUsingIndex �ҷ��� ĳ���͸� �����ְ� ����� �� �ֵ��� �ϴ� ��

    //����ִٸ� ���� �����ϵ��� �Ѵ�.
    //ĳ���Ͱ� �����Ѵٸ� ������ ������ ĳ���͸� ���ӿ� �ҷ������� �Ѵ�.

    public class LobbyCtrl : MonoBehaviour
    {
        //���� ���̵�� : �ִ� 4���� ĳ����, Ŀ���� ĳ���� ���� ������ ������ �� (����, �г���, ����, ���丮 ���� �ܰ�)

        //ĳ���� â �����ֱ� ���� �ӽ�
        public FormatUserCharacter[] userFormats;

        //save, load �� �������� ����ϴ� ����
        public DataController saveData;

        public Sync_DataCtrlPlayer sync;

        [System.Serializable]
        public struct FormatUserCharacter //�� ĳ������ ����
        {
            //ĳ���� instantiate �� �θ� ������Ʈ
            [SerializeField]
            public GameObject character;

            //Ŭ����ư �����ϱ�
            [SerializeField]
            public Button selectButton;

            [SerializeField]
            public EnumPlayer_Information enumPlayer_Information;

            //������ ĳ���Ͱ� �ִٸ�, �����ϱ� //���ٸ�, �����ϱ�
            [SerializeField]
            public TextMeshProUGUI myText;

            //���õɶ� ����
            public Image shinnyAct;

            //�ɷ�ġ ����â ����
            public Text level_A;
            public Text str_A;
            public Text dex_A;
            public Text int_A;
            public Text luck_A;

            //���� ĳ���Ͱ� ���õǾ����� �Ǵ�
            bool isButtonSelected;
        }

        void Start()
        {
            //SaveData == null�϶�  =  isMade false ĳ���Ͱ� ������
            //GameData [] ���� ����
            //LobbyCtrl ĳ���Ͱ� ���ٸ� = ó�� , 
            //LobbyCtrl     : (�ҷ�����) , Gamedata [��ü] ������ ĳ���Ϳ� ���� for(), ( ���� �ϱ� -> ĳ���� ������ index �ذ�)
            //NewAvatorCtrl : (�ҷ�����) , Gamedata [i] ������ ĳ���Ϳ� ���� ( �����ϰ� ���� ���۽� == ���� )

            //�ҷ�����
            saveData.LoadGameData();
            Check();
        }

        //ĳ���� �ִ��� üũ
        public void Check()
        {
            //��ü�� �˻� : 0,1,2,3 ĳ���� �����Ǿ��°�
            for (int i = 0; i < saveData.gameData.isMades.Length; i++)
            {
                //���� ĳ���Ͱ� ���ٸ� (nullüũ��) == �ű� ����
                if (!saveData.gameData.isMades[i])//���� : ĳ���� ���� �� true //ĳ���� ������ false
                {
                    //��ư text ����
                    userFormats[i].myText.text = "ĳ���� �����ϱ�";

                    //ĳ���� ����
                    userFormats[i].enumPlayer_Information.isMade = false;

                    //ĳ���� �ɷ�ġ �ʱ�ȭ
                    Characterinits(i);

                    //ĳ���͸� ��Ȱ��ȭ
                    userFormats[i].character.SetActive(false);   
                }
                else//���� �ҷ�����
                {

                    print( "�ε����� " + i  + "������°� " + saveData.gameData.isMades[i]);

                    //���� = ĳ���� �г���
                    userFormats[i].myText.text = saveData.gameData.myNames[i];

                    //ĳ���� �������
                    userFormats[i].enumPlayer_Information.isMade = true;

                    //ĳ���͸� Ȱ��ȭ
                    userFormats[i].character.SetActive(true);

                    //ĳ���Ϳ��� ������
                    Match_SaveData_ToPlayer(i);

                    //���� �ҷ����� (�÷��̾�� �����ϴ� �Լ�)
                    //Match_SaveData_ToPlayer(i);
                }
            }
        }

        //���� ����ٸ� ��ư Ŭ���� ȣ�� �Լ�
        public void New(int i) //0 1 2 3 ��ġ
        {
            //��ư �ε��� ����
            saveData.gameData.nowUsingIndex = i;

            saveData.SaveGameData();

            //����������
            if (!saveData.gameData.isMades[i])
            {
                //�ʱ�ȭ �� (�� Ȯ��)
                Characterinits(i);

                //�� �ҷ�����
                SceneManager.LoadScene("3. NewAvator"); 
            }
        }

        //���� ĳ���Ͱ� �����Ѵٸ� ��ư Ŭ���� ȣ�� �Լ�
        public void Player(int i) //0 1 2 3
        {
            //��ư �ε��� ����
            saveData.gameData.nowUsingIndex = i;
            //�׽�Ʈ
            print(i + "�÷��̾� +1");
            print(saveData.gameData.nowUsingIndex + "���̷�Ʈ ����");

            Match_Players_ToSaveData(i);
            saveData.SaveGameData();

            //���� ������
            if (saveData.gameData.isMades[i])
            {
                //�켱�� ������
                SceneManager.LoadScene("4. Home_Zone");
            }
        }

        //(���� �ҷ�����) �����Ϳ��� -> �÷��̾�� ������ �־��ֱ�
        public void Match_SaveData_ToPlayer(int ii) //0,1,2,3
        {
            //ü�� ���� ����ġ ����
            userFormats[ii].enumPlayer_Information.myHp = saveData.gameData.myHps[ii];
            userFormats[ii].enumPlayer_Information.myMp = saveData.gameData.myMps[ii];
            userFormats[ii].enumPlayer_Information.myEXP = saveData.gameData.myEXPs[ii];
            userFormats[ii].enumPlayer_Information.myLevel = saveData.gameData.myLevels[ii];

            //�� ��ø ���� ���
            userFormats[ii].enumPlayer_Information.mySTR = saveData.gameData.mySTRs[ii];
            userFormats[ii].enumPlayer_Information.myDEX = saveData.gameData.myDEXs[ii];
            userFormats[ii].enumPlayer_Information.myINT = saveData.gameData.myINTs[ii];
            userFormats[ii].enumPlayer_Information.myLUCK = saveData.gameData.myLUCKs[ii];

            //����â ����
            userFormats[ii].level_A.text = userFormats[ii].enumPlayer_Information.myLevel.ToString();

            //����â �� ��ø ���� ���
            userFormats[ii].str_A.text = userFormats[ii].enumPlayer_Information.mySTR.ToString();
            userFormats[ii].dex_A.text = userFormats[ii].enumPlayer_Information.myDEX.ToString();
            userFormats[ii].int_A.text = userFormats[ii].enumPlayer_Information.myINT.ToString();
            userFormats[ii].luck_A.text = userFormats[ii].enumPlayer_Information.myLUCK.ToString();

            //���� ���
            userFormats[ii].enumPlayer_Information.myCoin = saveData.gameData.myCoins[ii];
            userFormats[ii].enumPlayer_Information.myGold = saveData.gameData.myGolds[ii];

            //���ݷ�, ���߷�, ġ���, ����, ȸ����
            userFormats[ii].enumPlayer_Information.myAttack = saveData.gameData.myAttacks[ii];
            userFormats[ii].enumPlayer_Information.myAcurrancy = saveData.gameData.myAcurrancies[ii];
            userFormats[ii].enumPlayer_Information.myCritical = saveData.gameData.myCriticals[ii];
            userFormats[ii].enumPlayer_Information.myDefense = saveData.gameData.myDefenses[ii];
            userFormats[ii].enumPlayer_Information.myEvationRate = saveData.gameData.myEvationRates[ii];

            //ü��ȸ����, ����ȸ����
            userFormats[ii].enumPlayer_Information.myHpCure = saveData.gameData.myHpCures[ii];
            userFormats[ii].enumPlayer_Information.myMpCure = saveData.gameData.myMpCures[ii];

            //�̸�, �������� �Ǵ�
            userFormats[ii].enumPlayer_Information.myName.text = saveData.gameData.myNames[ii];

            userFormats[ii].enumPlayer_Information.isMade = saveData.gameData.isMades[ii];

            //���� ( �Ӹ�, ��, ����, ������, �޴ٸ�, �����ٸ�, ��� )
            userFormats[ii].enumPlayer_Information.head.sprite = saveData.gameData.heads[ii];
            userFormats[ii].enumPlayer_Information.body.sprite = saveData.gameData.bodies[ii];
            userFormats[ii].enumPlayer_Information.leftArm.sprite = saveData.gameData.leftArms[ii];
            userFormats[ii].enumPlayer_Information.rightArm.sprite = saveData.gameData.rightArms[ii];
            userFormats[ii].enumPlayer_Information.leftLeg.sprite = saveData.gameData.leftLegs[ii];
            userFormats[ii].enumPlayer_Information.rightArm.sprite = saveData.gameData.rightArms[ii];

            //�� Ȱ��ȭ �Ǿ��ִٸ�
            if (saveData.gameData.isHairTrue[ii])
            {
                userFormats[ii].enumPlayer_Information.myHair.gameObject.SetActive(true);
                userFormats[ii].enumPlayer_Information.myHair.sprite = saveData.gameData.myHairs[ii];
            }
            else //if (!saveData.gameData.isHairTrue[ii]) : �� ���ٸ�
            {
                userFormats[ii].enumPlayer_Information.myHair.gameObject.SetActive(false);
            }
            

            //������ ( ���� ���̶���, ������ ���̶���, ���� ������, ������ ������ )
            userFormats[ii].enumPlayer_Information.myEyes[0].myLeftBackEye.sprite = saveData.gameData.myLeftBackEyes[ii];
            userFormats[ii].enumPlayer_Information.myEyes[0].myRightBackEye.sprite = saveData.gameData.myRightBackEyes[ii];
            userFormats[ii].enumPlayer_Information.myEyes[0].myLeftFrontEye.sprite = saveData.gameData.myLeftFrontEyes[ii];
            userFormats[ii].enumPlayer_Information.myEyes[0].myRightFrontEye.sprite = saveData.gameData.myRightFrontEyes[ii];

            //����
            userFormats[ii].enumPlayer_Information.myFaceHair.sprite = saveData.gameData.myFaceHairs[ii];

            //���� �԰� �ִٸ�
            if (saveData.gameData.isClothTrue[ii])
            {
                userFormats[ii].enumPlayer_Information.myCloths[0].myBodyCloth.gameObject.SetActive(true);
                userFormats[ii].enumPlayer_Information.myCloths[0].myLeftClothArm.gameObject.SetActive(true);
                userFormats[ii].enumPlayer_Information.myCloths[0].myRightClothArm.gameObject.SetActive(true);

                //���� ( ����, ����, ������ )
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

            //������ �԰� �ִٸ�
            if (saveData.gameData.isPantsTrue[ii])
            {
                userFormats[ii].enumPlayer_Information.myLegs[0].myLeftLeg.gameObject.SetActive(true);
                userFormats[ii].enumPlayer_Information.myLegs[0].myRightLeg.gameObject.SetActive(true);

                //���� (�޴ٸ�, �����ٸ�)
                userFormats[ii].enumPlayer_Information.myLegs[0].myLeftLeg.sprite = saveData.gameData.myleftPantsLegs[ii];
                userFormats[ii].enumPlayer_Information.myLegs[0].myRightLeg.sprite = saveData.gameData.myRightPantsLegs[ii];
            }
            else
            {
                userFormats[ii].enumPlayer_Information.myLegs[0].myLeftLeg.gameObject.SetActive(false);
                userFormats[ii].enumPlayer_Information.myLegs[0].myRightLeg.gameObject.SetActive(false);
            }

            //���Ǹ� �θ��� �ִٸ�
            if (saveData.gameData.isBackTrue[ii])
            {
                userFormats[ii].enumPlayer_Information.back.gameObject.SetActive(true);

                //����
                userFormats[ii].enumPlayer_Information.back.sprite = saveData.gameData.backs[ii];
            }
            else
            {
                userFormats[ii].enumPlayer_Information.back.gameObject.SetActive(false);
            }

            //������ �����ϰ� �ִٸ�
            if (saveData.gameData.isArmorTrue[ii])
            {
                userFormats[ii].enumPlayer_Information.myArmor[0].myBody.gameObject.SetActive(true);
                userFormats[ii].enumPlayer_Information.myArmor[0].myLeftArm.gameObject.SetActive(true);
                userFormats[ii].enumPlayer_Information.myArmor[0].myRightArm.gameObject.SetActive(true);

                //���� (����, �޾��, �������)
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

            //�� �տ� ��� �ִٸ�
            if (saveData.gameData.isLeftHandTrue[ii])
            {
                userFormats[ii].enumPlayer_Information.myHands[0].myLeftWeapon.gameObject.SetActive(true);

                //�� ����(�޼�)
                userFormats[ii].enumPlayer_Information.myHands[0].myLeftWeapon.sprite = saveData.gameData.myLeftWeapons[ii];
                //userFormats[ii].enumPlayer_Information.myHands[0].myLeftShield.sprite = saveData.gameData.myLeftShields[ii];
            }
            else
            {
                userFormats[ii].enumPlayer_Information.myHands[0].myLeftWeapon.gameObject.SetActive(false);
            }

            //���� �տ� ��� �ִٸ�
            if (saveData.gameData.isWriteHandTrue[ii])
            {
                userFormats[ii].enumPlayer_Information.myHands[0].myRightShield.gameObject.SetActive(true);

                //�� ����(������)
                //userFormats[ii].enumPlayer_Information.myHands[0].myRightWeapon.sprite = saveData.gameData.myRightWeapons[ii];
                userFormats[ii].enumPlayer_Information.myHands[0].myRightShield.sprite = saveData.gameData.myRightShields[ii];
            }
            else
            {
                userFormats[ii].enumPlayer_Information.myHands[0].myRightShield.gameObject.SetActive(false);
            }
            
            //���� (2/17 �߰�)
            //���� ��� //���
            userFormats[ii].enumPlayer_Information.myHairColor = saveData.gameData.myHairColors[ii];
            //���� ����
            userFormats[ii].enumPlayer_Information.myHair.color = userFormats[ii].enumPlayer_Information.myHairColor;

            //���� ��� //�޴�
            userFormats[ii].enumPlayer_Information.myLeftEyeColor = saveData.gameData.myLeftFrontEyeColors[ii];
            //���� ����
            userFormats[ii].enumPlayer_Information.myEyes[0].myLeftFrontEye.color = userFormats[ii].enumPlayer_Information.myLeftEyeColor;

            //���� ��� //������
            userFormats[ii].enumPlayer_Information.myRightEyeColor = saveData.gameData.myRightFrontEyeColors[ii];
            //���� ����
            userFormats[ii].enumPlayer_Information.myEyes[0].myRightFrontEye.color = userFormats[ii].enumPlayer_Information.myRightEyeColor;
        }

        //(���� �ҷ�����) �÷��̾�� -> �����ͷ� ������ �־��ֱ�
        public void Match_Players_ToSaveData(int ii) //0,1,2,3
        {
            //ü�� ���� ����ġ ����
            saveData.gameData.myHps[ii] = userFormats[ii].enumPlayer_Information.myHp;
            saveData.gameData.myMps[ii] = userFormats[ii].enumPlayer_Information.myMp;
            saveData.gameData.myEXPs[ii] = userFormats[ii].enumPlayer_Information.myEXP;
            saveData.gameData.myLevels[ii] = userFormats[ii].enumPlayer_Information.myLevel;

            //�� ��ø ���� ���
            saveData.gameData.mySTRs[ii] = userFormats[ii].enumPlayer_Information.mySTR;
            saveData.gameData.myDEXs[ii] = userFormats[ii].enumPlayer_Information.myDEX;
            saveData.gameData.myINTs[ii] = userFormats[ii].enumPlayer_Information.myINT;
            saveData.gameData.myLUCKs[ii] = userFormats[ii].enumPlayer_Information.myLUCK;

            //���� ���
            saveData.gameData.myCoins[ii] = userFormats[ii].enumPlayer_Information.myCoin;
            saveData.gameData.myGolds[ii] = userFormats[ii].enumPlayer_Information.myGold;

            //���ݷ�, ���߷�, ġ���, ����, ȸ����
            saveData.gameData.myAttacks[ii] = userFormats[ii].enumPlayer_Information.myAttack;
            saveData.gameData.myAcurrancies[ii] = userFormats[ii].enumPlayer_Information.myAcurrancy;
            saveData.gameData.myCriticals[ii] = userFormats[ii].enumPlayer_Information.myCritical;
            saveData.gameData.myDefenses[ii] = userFormats[ii].enumPlayer_Information.myDefense;
            saveData.gameData.myEvationRates[ii] = userFormats[ii].enumPlayer_Information.myEvationRate;

            //ü��ȸ����, ����ȸ����
            saveData.gameData.myHpCures[ii] = userFormats[ii].enumPlayer_Information.myHpCure;
            saveData.gameData.myMpCures[ii] = userFormats[ii].enumPlayer_Information.myMpCure;

            //�̸�, �������� �Ǵ�
            saveData.gameData.myNames[ii] = userFormats[ii].enumPlayer_Information.myName.text.ToString();
            saveData.gameData.isMades[ii] = userFormats[ii].enumPlayer_Information.isMade;

            //���� ( �Ӹ�, ��, ����, ������, �޴ٸ�, �����ٸ�, ��� )
            saveData.gameData.heads[ii] = userFormats[ii].enumPlayer_Information.head.sprite;
            saveData.gameData.bodies[ii] = userFormats[ii].enumPlayer_Information.body.sprite;
            saveData.gameData.leftArms[ii] = userFormats[ii].enumPlayer_Information.leftArm.sprite;
            saveData.gameData.rightArms[ii] = userFormats[ii].enumPlayer_Information.rightArm.sprite;
            saveData.gameData.leftLegs[ii] = userFormats[ii].enumPlayer_Information.leftLeg.sprite;
            saveData.gameData.rightArms[ii] = userFormats[ii].enumPlayer_Information.rightArm.sprite;
            saveData.gameData.myHairs[ii] = userFormats[ii].enumPlayer_Information.myHair.sprite;

            //������ ( ���� ���̶���, ������ ���̶���, ���� ������, ������ ������ )
            saveData.gameData.myLeftBackEyes[ii] = userFormats[ii].enumPlayer_Information.myEyes[0].myLeftBackEye.sprite;
            saveData.gameData.myRightBackEyes[ii] = userFormats[ii].enumPlayer_Information.myEyes[0].myRightBackEye.sprite;
            saveData.gameData.myLeftFrontEyes[ii] = userFormats[ii].enumPlayer_Information.myEyes[0].myLeftFrontEye.sprite;
            saveData.gameData.myRightFrontEyes[ii] = userFormats[ii].enumPlayer_Information.myEyes[0].myRightFrontEye.sprite;

            //����
            saveData.gameData.myFaceHairs[ii] = userFormats[ii].enumPlayer_Information.myFaceHair.sprite;

            //���� ( ����, ����, ������ )
            saveData.gameData.myBodyCloths[ii] = userFormats[ii].enumPlayer_Information.myCloths[0].myBodyCloth.sprite;
            saveData.gameData.myLeftClothArms[ii] = userFormats[ii].enumPlayer_Information.myCloths[0].myLeftClothArm.sprite;
            saveData.gameData.myRightClothArms[ii] = userFormats[ii].enumPlayer_Information.myCloths[0].myRightClothArm.sprite;

            //���� (�޴ٸ�, �����ٸ�)
            saveData.gameData.myleftPantsLegs[ii] = userFormats[ii].enumPlayer_Information.myLegs[0].myLeftLeg.sprite;
            saveData.gameData.myRightPantsLegs[ii] = userFormats[ii].enumPlayer_Information.myLegs[0].myRightLeg.sprite;

            //����
            saveData.gameData.backs[ii] = userFormats[ii].enumPlayer_Information.back.sprite;

            //���� (����, �޾��, �������)
            saveData.gameData.myBodyArmores[ii] = userFormats[ii].enumPlayer_Information.myArmor[0].myBody.sprite;
            saveData.gameData.myLeftShoulderArmers[ii] = userFormats[ii].enumPlayer_Information.myArmor[0].myLeftArm.sprite;
            saveData.gameData.myRightShoulderArmers[ii] = userFormats[ii].enumPlayer_Information.myArmor[0].myRightArm.sprite;

            //�� ����(������ �޼�)
            saveData.gameData.myLeftWeapons[ii] = userFormats[ii].enumPlayer_Information.myHands[0].myLeftWeapon.sprite;
            saveData.gameData.myRightWeapons[ii] = userFormats[ii].enumPlayer_Information.myHands[0].myRightWeapon.sprite;
            saveData.gameData.myLeftShields[ii] = userFormats[ii].enumPlayer_Information.myHands[0].myLeftShield.sprite;
            saveData.gameData.myRightShields[ii] = userFormats[ii].enumPlayer_Information.myHands[0].myRightShield.sprite;

            //���� (2/17 �߰�)
            saveData.gameData.myHairColors[ii] = userFormats[ii].enumPlayer_Information.myHairColor;
            saveData.gameData.myLeftFrontEyeColors[ii] = userFormats[ii].enumPlayer_Information.myLeftEyeColor;
            saveData.gameData.myRightFrontEyeColors[ii] = userFormats[ii].enumPlayer_Information.myRightEyeColor;
        }

        //���� �ʱ�ȭ ��ư
        public void OnClick_CharacterInitialze_Btn(int i) //ĳ���� 0, 1, 2, 3
        {
            //�ӽ� �ε���
            int tempint = i;

            //ĳ���� �ɷ�ġ �ʱ�ȭ
            Characterinits(tempint);

            //�ٽ� üũ
            Check();
        }


        //�ʱ�ȭ ��ư
        public void OnClick_AllInitialze_Btn()
        {
            //json ���� ��ü ����
            saveData.DataDelete();

            //4ĳ���� ���� �ʱ�ȭ
            for (int i = 0; i < userFormats.Length; i++)
            {
                //ĳ���� �ɷ�ġ �ʱ�ȭ
                Characterinits(i);
            }

            //�ٽ� üũ
            Check();
        }

        //���� ������ �����ϰ� �ֱ� ������ ȣ�� �Լ��� �ۼ�
        public void Characterinits(int i)
        {
            //ü�� ���� ����ġ ����
            saveData.gameData.myHps[i] = 100;
            saveData.gameData.myMps[i] = 100;
            saveData.gameData.myEXPs[i] = 0;
            saveData.gameData.myLevels[i] = 1;

            //�� ��ø ���� ���
            saveData.gameData.mySTRs[i] = 4;
            saveData.gameData.myDEXs[i] = 4;
            saveData.gameData.myINTs[i] = 4;
            saveData.gameData.myLUCKs[i] = 4;

            //���� ���
            saveData.gameData.myCoins[i] = 100;
            saveData.gameData.myGolds[i] = 100;

            //���ݷ�, ���߷�, ġ���, ����, ȸ����
            saveData.gameData.myAttacks[i] = 0;
            saveData.gameData.myAcurrancies[i] = 0;
            saveData.gameData.myCriticals[i] = 0;
            saveData.gameData.myDefenses[i] = 0;
            saveData.gameData.myEvationRates[i] = 0;

            //ü��ȸ����, ����ȸ����
            saveData.gameData.myHpCures[i] = 0;
            saveData.gameData.myMpCures[i] = 0;

            //�̸�, �������� �Ǵ�
            saveData.gameData.myNames[i] = "";
            saveData.gameData.isMades[i] = false;

            /*
            //���� ( ����, ����, ������ )
            saveData.gameData.myBodyCloths[tempNowUsingIndex];
            saveData.gameData.myLeftClothArms[tempNowUsingIndex];
            saveData.gameData.myRightClothArms[tempNowUsingIndex];

            //���� (�޴ٸ�, �����ٸ�)
            saveData.gameData.myleftPantsLegs[tempNowUsingIndex];
            saveData.gameData.myRightPantsLegs[tempNowUsingIndex];

            //���� (����, �޾��, �������)
            saveData.gameData.myBodyArmores[tempNowUsingIndex];
            saveData.gameData.myLeftShoulderArmers[tempNowUsingIndex];
            saveData.gameData.myRightShoulderArmers[tempNowUsingIndex];

            //����
            saveData.gameData.backs[tempNowUsingIndex];

            //�� ����(������ �޼�)
            saveData.gameData.myLeftWeapons[tempNowUsingIndex];
            saveData.gameData.myRightWeapons[tempNowUsingIndex];
            saveData.gameData.myLeftShields[tempNowUsingIndex];
            saveData.gameData.myRightShields[tempNowUsingIndex];


            //���� ��Ȱ��ȭ
            saveData.gameData.isHelmetTrue[tempNowUsingIndex] = false;
            //���� ��Ȱ��ȭ
            saveData.gameData.isArmorTrue[tempNowUsingIndex] = false;
            //���� ��Ȱ��ȭ
            saveData.gameData.isBackTrue[tempNowUsingIndex] = false;
            //���� ��Ȱ��ȭ
            saveData.gameData.isWriteHandTrue[tempNowUsingIndex] = false;
            //���� ��Ȱ��ȭ
            saveData.gameData.isLeftHandTrue[tempNowUsingIndex] = false;
            */

        }

    }//class
}//namespace