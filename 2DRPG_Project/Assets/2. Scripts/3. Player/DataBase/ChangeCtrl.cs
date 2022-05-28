using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Main;

namespace Main
{
    //�����̳� ���������� ������ �ٲܶ� ���ӿ�����Ʈ�� ��ư �Լ��� ȣ���Ͽ� �ε����� ���� �����ϴ� ���
    //Item(������ ��ųʸ�)�� ItemSprites (���� �̹��� �迭) -> ĳ���� �⺻ �����۰� ������ ����

    //'����'�� ĳ���� EnumPlayer_Information ������ ���� ����ȭ�� ���� �ؾ��Ѵ�.

    //����ϰ� DataController�� 
    //(�����ͱ���� �����ϴ� ��ũ��Ʈ���� ����� ����ȭ�� �� �ְ� ���� �ۼ��� ��) 
    //(���� : �κ�������� 0,1,2,3�� ������ ���� ����ũ��Ʈ���� �ۼ�����)

    public class ChangeCtrl : MonoBehaviour
    {
        //�ٸ� ��ũ��Ʈ
        public EnumPlayer_Information player;//�����Ѵ�.
        public Sync_DataCtrlPlayer sync_data;//�ε����� �����ϱ� ����
        //�� �ɷ�ġ �޾ƿ��� ���� Item��ũ��Ʈ, ���� ��� �޾ƿ��� ���� ItemSprite
        public Item tempItem;//saveCtrl ������Ʈ �����Ѵ�.
        public ItemSprites tempItemSprites; //saveCtrl ������Ʈ �����Ѵ�.
        public DataController saveData; //��������� ���( �ε���json�̶� ����� �ƴϸ�, �������ǰ˻��ؾ� ��)

        //��� �ε��� 
        int forStartShortHair;
        int forStartSportHair;
        int forEndSportHair;

        //������
        public Slider colorR;
        public Slider colorG;
        public Slider colorB;

        //����� ���� (public�� �۵� Ȯ�ο�)
        Color backUp_hairColor;
        Color backUp_eyeColor;
        
        //���߿� Ȥ�� ���� �����Ѵٸ�, �迭�� ��������� ���� �۾��ϱ⿡ �а� ã�� ����� ���Ƽ�
        //index �����ϱⰡ �������� �������ٰ� �����ϰ�, �ӽù������� ��� �ۼ�
       
        //����� ����
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

        //���� ���� ���� �ο��� (index 0 : ������ ���� ���� , index 1 : �ش� ������ ���� ������)
        int[] random_species = new int[2];
        int[] random_hair = new int[2];
        int[] random_Eye = new int[2]; //����:  0 ����, 1 ������
        int random_Cloth; //���� (�ѹ��� x :����)
        int random_Pants;//������ �� ����
        int random_Weapon;//Ʃ�丮�� �� 5����

        //���� �÷� �� �ο��� (0~100���� �������� �޾Ƽ� 0.001f�� ���� ������ 0 ~ 1 value���� �ο�)
        int random_intR;
        int random_intG;
        int random_intB;
        float random_floatR;
        float random_floatG;
        float random_floatB;

        //���� ��ư ����� (false �����̴� ���ۿ� ���� ���� / true ���� ����ŭ �����̴� ��ġ ����)
        bool isClick_ResetHairColorBtn;
        bool isClick_ResetEyeColorBtn;

        //��� ��ư ����� (false ó�� ������� / true ���� ������¸� ����)
        bool isClick_BackUpLooks;

        //���� ��ư ����� (flase �����̴� ���ۿ� ���� ���� / true ���� ����ŭ �����̴� ��ġ ����)
        bool isClick_RandomHairColorBtn;
        bool isClick_RandomEyeColorBtn;

        //'����' r,g,b�� SpriteRenderer.color�� '����' �ְ� ���� �� ����.
        //�̹����� �÷��� ������ �� ���ٴ� ����(?)�� �ֱ� ������
        //color�� �ѹ� �ѱ� ����, ĳ���� ������ �����Ѵ�.
        //(�Ѱܹ��� color ��ü <- �÷� ���� �ְ�޴´� -> �̹���.color)

        //�����, ��� ������ �ε���
        string[] tempEquiptable_ItemDataName = new string[11];

        //�̹��� �޾ƿ��� �迭
        public Sprite[] itemSprites = new Sprite[3];

        //�����ݶ��̴��� ���� ����
        public CapsuleCollider2D leftHandWeaponColider; //�ڽ� ������Ʈ: ������ �ݶ��̴� �����ϱ� ����
        public SpriteRenderer weaponSR; //�ӽ�_������ ���� spriterenderer


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
            hairColor,//(2/17 �߰�)
            eyeColor,//(2/17 �߰�)
        }

        //�ʱ�ȭ
        TabIndex index = TabIndex.human;
        
        private void Start()
        {
            //���� ������
            forStartShortHair = tempItemSprites.hairs[0].longHair.Length;
            forStartSportHair = forStartShortHair + tempItemSprites.hairs[0].shortHair.Length;
            forEndSportHair = forStartSportHair  + tempItemSprites.hairs[0].sportHair.Length;

            //��ȣ Ȯ�� �� �ʿ�
            //print("1�� ����"+forStartShortHair + " " + forStartSportHair + " "+ forEndSportHair);
            //print("2�� " + tempItemSprites.hairs[0].longHair.Length + " "+ tempItemSprites.hairs[0].shortHair.Length + tempItemSprites.hairs[0].sportHair.Length);

            //�ʱ�ȭ
            isClick_BackUpLooks = false;

            print("isMade : " + saveData.gameData.isMades[saveData.gameData.nowUsingIndex] + "nowUsingIndex" + saveData.gameData.nowUsingIndex);

            //ĳ���� ���� ���̶��
            if (!saveData.gameData.isMades[saveData.gameData.nowUsingIndex])
            {
                //�ʱ�ȭ
                player.myHair.color = Color.white;
                player.myEyes[0].myLeftFrontEye.color = Color.black;
                player.myEyes[0].myRightFrontEye.color = Color.black;

                //������
                backUp_hairColor.a = 1;
                backUp_eyeColor.a = 1;
            }

            //����
            backUp_hairColor = player.myHair.color;
            backUp_eyeColor = player.myEyes[0].myLeftFrontEye.color;


            isClick_RandomHairColorBtn = false;
            isClick_RandomEyeColorBtn = false;
        }

        //����CSV�κ��� �´� �ݶ��̴��� �־��ִ� �Լ� 
        public void Weapon_Colider(string temp)//temp�� �����ͳ��� (�ε��� X)
        {
            //�÷��̾��� ���� �̸�
            string checkName = weaponSR.sprite.name;

            print("������ �̸� : " + checkName);
            print("�Ű������� �ؽ�Ʈ���� : " + temp);

            //������ �̸��� ��ġ�Ѵٸ�
            if (checkName == temp)
            {
                //�񼭳ʸ��� �Ӽ���
                string offset_x = tempItem.itemChart_WeaponsColider_DataName[temp][WeaponColiderSetting.OffsetX];
                string offset_y = tempItem.itemChart_WeaponsColider_DataName[temp][WeaponColiderSetting.OffsetY];
                string size_x = tempItem.itemChart_WeaponsColider_DataName[temp][WeaponColiderSetting.SizeX];
                string size_y = tempItem.itemChart_WeaponsColider_DataName[temp][WeaponColiderSetting.SizeY];

                //������ ��ġ
                leftHandWeaponColider.offset = // new Vector2 ( ��Ʈ��-> float�� );
                    new Vector2(float.Parse(offset_x), float.Parse(offset_y));

                //������ ����
                leftHandWeaponColider.size = // new Vector2 (������Ʈ�� -> ��Ʈ������ -> float��);
                    new Vector2(float.Parse(size_x), float.Parse(size_y));
            }
        }

        //��Ŭ���� �� ��ưȣ��
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
                case 8:// �Ӹ� ���� ���� (2/17�߰�)
                    index = TabIndex.hairColor;

                    //���� ����ŭ ����
                    colorR.value = player.myHair.color.r;
                    colorG.value = player.myHair.color.g;
                    colorB.value = player.myHair.color.b;
                    
                    break;
                case 9:// �� ���� ���� (2/17�߰�)
                    index = TabIndex.eyeColor;

                    //���� ����ŭ ����
                    colorR.value = player.myEyes[0].myLeftFrontEye.color.r;
                    colorG.value = player.myEyes[0].myLeftFrontEye.color.g;
                    colorB.value = player.myEyes[0].myLeftFrontEye.color.b;

                    break;
            }
        }

        //������ �����ϸ� ��ư �ν����Ϳ��� ȣ���ϴ� �Լ�
        public void OnClick_itemEquip_Btn(string temp)
        {
            //print("���� : " + temp);//Ȯ�ο�

            // ���� DataName == �������� ������Ʈ�� �̹���png ���ҽ� �̸�
            string dataName = tempItem.itemChart_Equiptable_Index[temp][ItemField.DataName];
            // ���� ResourcePath == Resources ������ ���� ���
            string resourcePath = tempItem.itemChart_Equiptable_Index[temp][ItemField.ResourcePath];

            // 1�� png ���� multiSprite ���� ��ŭ ���� �迭�� �޾ƿ���
            itemSprites = Resources.LoadAll<Sprite>(resourcePath + "/"  + dataName);
            //print("��� : " + dataName + "/" + resourcePath + "\n");
            //print(itemSprites[0] + " " + itemSprites[1] + " " + itemSprites[2]);

            //temp�� �޾ƿ� ������ ���� �˻� : ������ �°� ĳ���� ������
            switch (tempItem.itemChart_Equiptable_Index[temp][ItemField.Parts])
            {
                //������ ���
                case "Cloth":
                    print("����");
                    player.myCloths[0].myBodyCloth.sprite = itemSprites[0]; //����
                    player.myCloths[0].myLeftClothArm.sprite = itemSprites[1]; //�� �Ҹ�
                    player.myCloths[0].myRightClothArm.sprite = itemSprites[2]; //���� �Ҹ�

                    break;
                //������ ���
                case "PantsLeg":
                    print("����");
                    player.myLegs[0].myLeftLeg.sprite = itemSprites[0]; // �� ����
                    player.myLegs[0].myRightLeg.sprite = itemSprites[1]; // ���� ����

                    break;
                //������ ���
                case "Weapon":
                    print("����");
                    //������ӿ�����Ʈ Ȱ��ȭ
                    player.myHands[0].myLeftWeapon.gameObject.SetActive(true);

                    //�� ĳ���͸� ���� �� �� �ִ� ����� axe1 , short2, middle1, long3, dagger knife1 ����
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
                    //�����ϱ�
                    UseforInsertSpeices(0, i);
             
                    break;
                case TabIndex.elf:
                    //�����ϱ�
                    UseforInsertSpeices(1, i);
                  
                    break;
                case TabIndex.orc:
                    //�����ϱ�
                    UseforInsertSpeices(2, i);

                    break;
                case TabIndex.theOthers:
                    //�����ϱ�
                    UseforInsertSpeices(3, i);

                    break;
                case TabIndex.hair:
                    
                    if (i == 100)//�θӸ��϶�
                    {
                        //��� ��Ȱ��ȭ
                        player.myHair.gameObject.SetActive(false);
                    }
                    else//if (i != 0) �̿�
                    {
                        //��� Ȱ��ȭ
                        player.myHair.gameObject.SetActive(true);
                    }

                    //�� �Ӹ� ����
                    if ( i < forStartShortHair)
                    {
                        //�����ϱ�
                        useForHair(0, i);
                    }
                    //ª�� �Ӹ� ����
                    if (forStartShortHair <= i && i < forStartSportHair)
                    {
                        //�����ϱ�
                        useForHair(1, i - forStartShortHair);
                    }
                    //������ �Ӹ� ����
                    if (forStartSportHair <= i && i < forEndSportHair)
                    {
                        //�����ϱ�
                        useForHair(2, i - forStartSportHair);
                    }

                    break;
                case TabIndex.eyeblow://backeye
                    //�����ϱ�
                    useForEye(0, i);
      
                    break;
                case TabIndex.eye: //fronteye (0,1,4,9) : �ߺ��� ������� ����
                    //�����ϱ�
                    useForEye(1, i);
         
                    break;
                  
            }
        }//clickbtn

        //���, �� ���� ����(������Ʈ)
        void CtrlColor()
        {
            //���� �����ϱ� ����
            if (index == TabIndex.hairColor)//������ ��
            {
                //���� ��ư�� �����ٸ�
                if (isClick_ResetHairColorBtn)
                {
                    //����� �����ش�.
                    colorR.value = backUp_hairColor.r;
                    colorG.value = backUp_hairColor.g;
                    colorB.value = backUp_hairColor.b;

                    //���� ���� ������
                    isClick_ResetHairColorBtn = false;
                }
                if (isClick_RandomHairColorBtn)
                {
                    colorR.value = random_floatR;
                    colorG.value = random_floatG;
                    colorB.value = random_floatB;

                    isClick_RandomHairColorBtn = false;
                }

                //�����̴� ������ ����
                player.myHairColor.r = colorR.value;
                player.myHairColor.g = colorG.value;
                player.myHairColor.b = colorB.value;

                //�׻� arpha�� = 1
                player.myHairColor.a = 1;

                //��� �÷��� ����
                player.myHair.color = player.myHairColor;
            }
            if (index == TabIndex.eyeColor)//������ ��
            {
                //���� ��ư�� �����ٸ�
                if (isClick_ResetEyeColorBtn)
                {
                    //����� �����ش�.
                    colorR.value = backUp_eyeColor.r;
                    colorG.value = backUp_eyeColor.g;
                    colorB.value = backUp_eyeColor.b;

                    //���� ���� ������
                    isClick_ResetEyeColorBtn = false;
                }
                if (isClick_RandomEyeColorBtn)
                {
                    colorR.value = random_floatR;
                    colorG.value = random_floatG;
                    colorB.value = random_floatB;

                    isClick_RandomEyeColorBtn = false;
                }

                //�޴� �����̴� ������ ����
                player.myLeftEyeColor.r = colorR.value;
                player.myLeftEyeColor.g = colorG.value;
                player.myLeftEyeColor.b = colorB.value;

                //������ �����̴� ������ ����
                player.myRightEyeColor.r = colorR.value;
                player.myRightEyeColor.g = colorG.value;
                player.myRightEyeColor.b = colorB.value;

                //�׻� arpha�� = 1
                player.myLeftEyeColor.a = 1;
                player.myRightEyeColor.a = 1;

                //�� �÷��� ����
                player.myEyes[0].myLeftFrontEye.color = player.myLeftEyeColor;
                player.myEyes[0].myRightFrontEye.color = player.myRightEyeColor;
            }
        }

        private void Update()
        {
             CtrlColor();
            //print(index);
        }//update

        //�԰� �ִ� �� ��ü ���� //����� ���� ���ٸ� ó�� ���·� ������
        
        public void OnClick_LookUpReset_btn()
        {
            //��� ���� ���� ���ٸ�
            if (!isClick_BackUpLooks)
            {
                //���� ��� 1
                player.head.sprite = tempItemSprites.spacies[0].humans[0].head;
                player.body.sprite = tempItemSprites.spacies[0].humans[0].body;
                player.leftArm.sprite = tempItemSprites.spacies[0].humans[0].leftArm;
                player.rightArm.sprite = tempItemSprites.spacies[0].humans[0].rightArm;
                player.leftLeg.sprite = tempItemSprites.spacies[0].humans[0].leftLeg;
                player.rightLeg.sprite = tempItemSprites.spacies[0].humans[0].rightLeg;

                //��� //�� �Ӹ� 6
                //��� Ȱ��ȭ
                player.myHair.gameObject.SetActive(true);
                player.myHair.sprite = tempItemSprites.hairs[0].longHair[5];

                //����
                player.myEyes[0].myLeftBackEye.sprite = tempItemSprites.eyes[5].backEyes;
                player.myEyes[0].myRightBackEye.sprite = tempItemSprites.eyes[5].backEyes;

                //������
                player.myEyes[0].myLeftFrontEye.sprite = tempItemSprites.eyes[4].frontEyes;
                player.myEyes[0].myRightFrontEye.sprite = tempItemSprites.eyes[4].frontEyes;

                //����
                OnClick_itemEquip_Btn("CC19");//�⺻ ����

                //����
                OnClick_itemEquip_Btn("CP14");//�⺻ ����

                //������ӿ�����Ʈ ��Ȱ��ȭ
                player.myHands[0].myLeftWeapon.gameObject.SetActive(false);

            }

            //�����ư�� �����ٸ� ��� ���� ������ preset���� �����ֱ�
            else//if (isClick_BackUpLooks)
            {
                //���� ���
                player.head.sprite = backUp_head;
                player.body.sprite = backUp_body;
                player.leftArm.sprite = backUp_leftArm;
                player.rightArm.sprite = backUp_rightArm;
                player.leftLeg.sprite = backUp_leftLeg;
                player.rightLeg.sprite = backUp_rightLeg;

                //��� ���
                if (player.myHair.gameObject.activeSelf)//�θӸ��� �ƴ϶��
                {
                    player.myHair.sprite = backUp_hair;//��������Ʈ �Ѱ��ֱ�
                }

                //���� ,������ ���
                player.myEyes[0].myLeftBackEye.sprite = backUp_backEye;
                player.myEyes[0].myRightBackEye.sprite = backUp_backEye;
                player.myEyes[0].myLeftFrontEye.sprite = backUp_frontEye;
                player.myEyes[0].myRightFrontEye.sprite = backUp_frontEye;

                //���� ���
                player.myCloths[0].myBodyCloth.sprite = backUp_myBodyCloth;
                player.myCloths[0].myLeftClothArm.sprite = backUp_myLeftCloth;
                player.myCloths[0].myRightClothArm.sprite = backUp_myRightCloth;

                //���� ���
                player.leftLeg.sprite = backUp_leftLegPant;
                player.rightLeg.sprite = backUp_rightLegPant;

                //
                if (saveData.gameData.nowUsingIndex == 0)
                {
                    //�ε��� ����
                    saveData.gameData.equiptable_ItemDataName1[0] = tempEquiptable_ItemDataName[0]; //0 ����
                    saveData.gameData.equiptable_ItemDataName1[1] = tempEquiptable_ItemDataName[1]; //1 ����
                }
                if (saveData.gameData.nowUsingIndex == 1)
                {
                    //�ε��� ����
                    saveData.gameData.equiptable_ItemDataName2[0] = tempEquiptable_ItemDataName[0]; //0 ����
                    saveData.gameData.equiptable_ItemDataName2[1] = tempEquiptable_ItemDataName[1]; //1 ����
                }
                if (saveData.gameData.nowUsingIndex == 2)
                {
                    //�ε��� ����
                    saveData.gameData.equiptable_ItemDataName3[0] = tempEquiptable_ItemDataName[0]; //0 ����
                    saveData.gameData.equiptable_ItemDataName3[1] = tempEquiptable_ItemDataName[1]; //1 ����
                }
                if (saveData.gameData.nowUsingIndex == 3)
                {
                    //�ε��� ����
                    saveData.gameData.equiptable_ItemDataName4[0] = tempEquiptable_ItemDataName[0]; //0 ����
                    saveData.gameData.equiptable_ItemDataName4[1] = tempEquiptable_ItemDataName[1]; //1 ����
                }

                //���� ���
                if (player.myHands[0].myLeftWeapon.gameObject.activeSelf)//���⸦ �����ϰ� �ִٸ�
                {
                    player.myHands[0].myLeftWeapon.sprite = backUp_myLeftWeapon;
                }
            }
        }
    
        //�԰� �ִ� �� ��ü ���
        public void OnClick_LookUpBackUp_btn()
        {
            print("��� �Ϸ�" + isClick_BackUpLooks);
            isClick_BackUpLooks = true;

            //���� ���
            backUp_head = player.head.sprite;
            backUp_body = player.body.sprite; 
            backUp_leftArm = player.leftArm.sprite;
            backUp_rightArm = player.rightArm.sprite;
            backUp_leftLeg = player.leftLeg.sprite;
            backUp_rightLeg = player.rightLeg.sprite;

            //��� ���
            if (player.myHair.gameObject.activeSelf)//�θӸ��� �ƴ϶��
            {
                backUp_hair = player.myHair.sprite;//��������Ʈ �Ѱ��ֱ�
            }
          
            //���� ,������ ���
            backUp_backEye = player.myEyes[0].myLeftBackEye.sprite;
            backUp_frontEye = player.myEyes[0].myLeftFrontEye.sprite;

            //���� ���
            backUp_myBodyCloth = player.myCloths[0].myBodyCloth.sprite;
            backUp_myLeftCloth = player.myCloths[0].myLeftClothArm.sprite;
            backUp_myRightCloth = player.myCloths[0].myRightClothArm.sprite;

            //���� ���
            backUp_leftLegPant = player.leftLeg.sprite;
            backUp_rightLegPant = player.rightLeg.sprite;
            if (saveData.gameData.nowUsingIndex == 0)
            {
                //�ε��� ����
                tempEquiptable_ItemDataName[0] = saveData.gameData.equiptable_ItemDataName1[0]; //0 ����
                tempEquiptable_ItemDataName[1] = saveData.gameData.equiptable_ItemDataName1[1]; //1 ����
            }
            if (saveData.gameData.nowUsingIndex == 1)
            {
                //�ε��� ����
                tempEquiptable_ItemDataName[0] = saveData.gameData.equiptable_ItemDataName2[0]; //0 ����
                tempEquiptable_ItemDataName[1] = saveData.gameData.equiptable_ItemDataName2[1]; //1 ����
            }
            if (saveData.gameData.nowUsingIndex == 2)
            {
                //�ε��� ����
                tempEquiptable_ItemDataName[0] = saveData.gameData.equiptable_ItemDataName3[0]; //0 ����
                tempEquiptable_ItemDataName[1] = saveData.gameData.equiptable_ItemDataName3[1]; //1 ����
            }
            if (saveData.gameData.nowUsingIndex == 3)
            {
                //�ε��� ����
                tempEquiptable_ItemDataName[0] = saveData.gameData.equiptable_ItemDataName4[0]; //0 ����
                tempEquiptable_ItemDataName[1] = saveData.gameData.equiptable_ItemDataName4[1]; //1 ����
            }


            //���� ���
            if (player.myHands[0].myLeftWeapon.gameObject.activeSelf)//���⸦ �����ϰ� �ִٸ�
            {
                backUp_myLeftWeapon = player.myHands[0].myLeftWeapon.sprite;

                if (saveData.gameData.nowUsingIndex == 0)
                {
                    //�ε��� ����
                    tempEquiptable_ItemDataName[10] = saveData.gameData.equiptable_ItemDataName1[10];
                }
                if (saveData.gameData.nowUsingIndex == 1)
                {
                    //�ε��� ����
                    tempEquiptable_ItemDataName[10] = saveData.gameData.equiptable_ItemDataName2[10];
                }
                if (saveData.gameData.nowUsingIndex == 2)
                {
                    //�ε��� ����
                    tempEquiptable_ItemDataName[10] = saveData.gameData.equiptable_ItemDataName3[10];
                }
                if (saveData.gameData.nowUsingIndex == 3)
                {
                    //�ε��� ����
                    tempEquiptable_ItemDataName[10] = saveData.gameData.equiptable_ItemDataName4[10];
                }
            }
        }


        //���� ���� == ������·� ������ //����� ���� ���ٸ� ó�����·� ������
        public void OnClick_HairColorReset_btn()
        {
            //������Ʈ �����
            isClick_ResetHairColorBtn = true;
     
            //�� ������ �ѱ��
            player.myHairColor = backUp_hairColor;

            //������ �̹����� ����
            player.myHair.color = player.myHairColor;
        }

        //���� ���� == ������·� ������ //����� ���� ���ٸ� ó�����·� ������
        public void OnClick_EyeColorReset_btn()
        {
            //������Ʈ �����
            isClick_ResetEyeColorBtn = true;

            //�� ������ �ѱ��
            player.myLeftEyeColor = backUp_eyeColor;
            player.myRightEyeColor = backUp_eyeColor;

            //������ �̹����� ����
            player.myEyes[0].myLeftFrontEye.color = player.myLeftEyeColor;
            player.myEyes[0].myRightFrontEye.color = player.myRightEyeColor;
        }

        //��� ���� ���
        public void OnClick_HairColorBackUp_btn()
        {
            //�Ӹ� �̹��� ���� �Ѱ��ְ�
            player.myHairColor = player.myHair.color;
            //�Ӹ� ���� ���
            backUp_hairColor = player.myHairColor;
        }
        public void OnClick_EyeColorBackUp_btn()
        {
            //�� �̹��� ���� �Ѱ��ְ�
            player.myLeftEyeColor = player.myEyes[0].myLeftFrontEye.color;
            //�� ���� ���
            backUp_eyeColor = player.myLeftEyeColor;

        }

        //�� ���� ������ �ڵ� ������ �Լ� 04/20
        public void OnClick_RandomLookUp_Btn()
        {
            //����
            //[0] :���� ����  0 ~ 3
            //[1] :�ش� ������ �Ǻ� ���� ����
            random_species[0] = Random.Range(0, 4);

            //���� ����
            switch (random_species[0])
            {
                case 0://humans

                    //�Ǻ� ���� ��
                    random_species[1] = Random.Range(0, tempItemSprites.spacies[0].humans.Length);

                    //�����ϱ�
                    UseforInsertSpeices(random_species[0], random_species[1]);

                    break;
                case 1://elfs

                    //�Ǻ� ���� ��
                    random_species[1] = Random.Range(0, tempItemSprites.spacies[0].elfs.Length);

                    //�����ϱ�
                    UseforInsertSpeices(random_species[0], random_species[1]);

                    break;
                case 2://orcs

                    //�Ǻ� ���� ��
                    random_species[1] = Random.Range(0, tempItemSprites.spacies[0].orcs.Length);

                    //�����ϱ�
                    UseforInsertSpeices(random_species[0], random_species[1]);

                    break;
                case 3://others

                    //�Ǻ� ���� ��
                    random_species[1] = Random.Range(0, tempItemSprites.spacies[0].theOrders.Length);

                    //�����ϱ�
                    UseforInsertSpeices(random_species[0], random_species[1]);

                    break;
            }
            //���
            //[0] :��� ���̺� ����  0 ~ 3
            //[1] :��� ������ ���� ����
            random_hair[0] = Random.Range(0, 3);

            //���� ����
            switch (random_hair[0])
            {
                case 0://longHair

                    //������ ���� ��
                    random_hair[1] = Random.Range(0, tempItemSprites.hairs[0].longHair.Length + 1);

                    // +1�� �ϴ� ���� : �ִ밪�� ������ �θӸ�
                    if (random_hair[1] == tempItemSprites.hairs[0].longHair.Length)
                    {
                        //��� ������Ʈ ��Ȱ��ȭ
                        player.myHair.gameObject.SetActive(false);
                    }
                    else //if(random_hair[1] != tempItemSprites.hairs[0].longHair.Length)
                    {
                        //��� ������Ʈ Ȱ��ȭ
                        player.myHair.gameObject.SetActive(true);

                        //�����ϱ�
                        useForHair(random_hair[0], random_hair[1]);
                    }

                    break;
                case 1://shortHair

                    //������ ���� ��
                    random_hair[1] = Random.Range(0, tempItemSprites.hairs[0].shortHair.Length + 1);

                    // +1�� �ϴ� ���� : �ִ밪�� ������ �θӸ�
                    if (random_hair[1] == tempItemSprites.hairs[0].shortHair.Length)
                    {
                        //��� ������Ʈ ��Ȱ��ȭ
                        player.myHair.gameObject.SetActive(false);
                    }
                    else //if(random_hair[1] != tempItemSprites.hairs[0].shortHair.Length)
                    {
                        //��� ������Ʈ Ȱ��ȭ
                        player.myHair.gameObject.SetActive(true);

                        //�����ϱ�
                        useForHair(random_hair[0], random_hair[1]);
                    }

                    break;
                case 2://sportHair

                    //������ ���� ��
                    random_hair[1] = Random.Range(0, tempItemSprites.hairs[0].sportHair.Length + 1);

                    // +1�� �ϴ� ���� : �ִ밪�� ������ �θӸ�
                    if (random_hair[1] == tempItemSprites.hairs[0].sportHair.Length)
                    {
                        //��� ������Ʈ ��Ȱ��ȭ
                        player.myHair.gameObject.SetActive(false);
                    }
                    else //if(random_hair[1] != tempItemSprites.hairs[0].sportHair.Length)
                    {
                        //��� ������Ʈ Ȱ��ȭ
                        player.myHair.gameObject.SetActive(true);

                        //�����ϱ�
                        useForHair(random_hair[0], random_hair[1]);
                    }
                    break;
            }

            //��
            //[0] : ���� ������ 
            //[1] : ������ ������
            random_Eye[0] = Random.Range(0, 6);
            random_Eye[1] = Random.Range(0, 4);

            //����
            useForEye(0, random_Eye[0]);
            //������
            useForEye(1, random_Eye[1]);

            //���� 
            random_Cloth = Random.Range(01, 24); // �ε��� �ѹ� ����
            string tempIndex = "CC" + random_Cloth.ToString(); // �ε��� �ۼ�
            OnClick_itemEquip_Btn(tempIndex); //�ش� ���� ������

            //���� 
            random_Pants = Random.Range(01, 17);// �ε��� �ѹ� ����
            tempIndex = "CP" + random_Pants.ToString(); //�ε��� �ۼ�
            OnClick_itemEquip_Btn(tempIndex); //�ش� ���� ������

            //����
            random_Weapon = Random.Range(0, 5);
            player.myHands[0].myLeftWeapon.gameObject.SetActive(true); //���������Ʈ Ȱ��ȭ

            switch (random_Weapon)//����
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
    
        //���������� �ߺ��ۼ��ϱ� ������ �̰����� ȣ���Լ��� ���
        //parts ���� , myResult ���� ���� : ĳ���Ϳ��� �̹��� �����ϴ� �����Լ�
        void UseforInsertSpeices(int parts, int myResult)
        {
            //����
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

        //parts ���� , myResult ���� ���� : ĳ���Ϳ��� �̹��� �����ϴ� ����Լ�
        void useForHair(int parts, int myResult)
        {
            //����
            switch (parts)
            {
                case 0://�� �Ӹ�
                    player.myHair.sprite = tempItemSprites.hairs[0].longHair[myResult];
                    break;
                case 1://�ܹ� �Ӹ�
                    player.myHair.sprite = tempItemSprites.hairs[0].shortHair[myResult];
                    break;
                case 2://������ �Ӹ�
                    player.myHair.sprite = tempItemSprites.hairs[0].sportHair[myResult];
                    break;
            }
        }

        //parts ���� , myResult ���� ���� : ĳ���Ϳ��� �̹��� �����ϴ� �� ���� �Լ�
        void useForEye(int parts, int myResult)
        {
            //����
            switch (parts)
            {
                case 0://���� 0~5����
                    player.myEyes[0].myLeftBackEye.sprite = tempItemSprites.eyes[myResult].backEyes; //����
                    player.myEyes[0].myRightBackEye.sprite = tempItemSprites.eyes[myResult].backEyes;//������
                    break;
                case 1://������ �ߺ� ����
                    if (myResult == 0)
                    {
                        player.myEyes[0].myLeftFrontEye.sprite = tempItemSprites.eyes[0].frontEyes; //����
                        player.myEyes[0].myRightFrontEye.sprite = tempItemSprites.eyes[0].frontEyes;//������
                    }
                    if (myResult == 1)
                    {
                        player.myEyes[0].myLeftFrontEye.sprite = tempItemSprites.eyes[1].frontEyes; //����
                        player.myEyes[0].myRightFrontEye.sprite = tempItemSprites.eyes[1].frontEyes;//������
                    }
                    if (myResult == 2)
                    {
                        player.myEyes[0].myLeftFrontEye.sprite = tempItemSprites.eyes[4].frontEyes; //����
                        player.myEyes[0].myRightFrontEye.sprite = tempItemSprites.eyes[4].frontEyes;//������
                    }
                    if (myResult == 3)
                    {
                        player.myEyes[0].myLeftFrontEye.sprite = tempItemSprites.eyes[9].frontEyes; //����
                        player.myEyes[0].myRightFrontEye.sprite = tempItemSprites.eyes[9].frontEyes;//������
                    }
                    break;
            }
        }
        
        public void OnClick_RandomHairColor_Btn()
        {
            isClick_RandomHairColorBtn = true;

            //0~ 1000���� ��������
            random_intR = Random.Range(0, 1000);
            random_intG = Random.Range(0, 1000);
            random_intB = Random.Range(0, 1000);

            // 0.001 ���ϱ� 
             random_floatR = random_intR * 0.001f;
             random_floatG = random_intG * 0.001f;
             random_floatB = random_intB * 0.001f;

            //������Ʈ �Լ��� ����
        }

        public void OnClick_RandomEyeColor_Btn()
        {
            isClick_RandomEyeColorBtn = true;

            //0~ 1000���� ��������
            random_intR = Random.Range(0, 1000);
            random_intG = Random.Range(0, 1000);
            random_intB = Random.Range(0, 1000);

            // 0.001 ���ϱ�
            random_floatR = random_intR * 0.001f;
            random_floatG = random_intG * 0.001f;
            random_floatB = random_intB * 0.001f;
            
            //������Ʈ �Լ��� ����
        }

    }//class
}//namespace
