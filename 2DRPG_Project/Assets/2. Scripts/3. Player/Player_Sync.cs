using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{

    //�ΰ��ӿ��� �ҷ��� ��ũ�̸� �����ϴ� ���Ұ� ������ �� ������ �� �ְ� �ϴ� �Լ�
    public class Player_Sync : MonoBehaviour
    {
        //���� ����
        public DataController saveData;
        //��ũ ��ũ��Ʈ ����
        public Sync_DataCtrlPlayer sync;

        //slot
        public BookInventory bookInventory;
        //�÷��̾�� ���� ����ֱ�
        EnumPlayer_Information player;

        CapsuleCollider2D weapon_Collider;

        void Start()
        {
            //�Ҵ�
            player = gameObject.GetComponent<EnumPlayer_Information>();

            //���� �ε��ϱ�
            saveData.LoadGameData();

            //�ε��� ����ȭ
            sync.tempNowUsingIndex = saveData.gameData.nowUsingIndex;

            print(saveData.gameData.nowUsingIndex + " : ���̺�� �ε��� " + sync.tempNowUsingIndex + " : ���� �޾ƿ� �ε��� ");

            //ĳ���� �ҷ�����
            sync.Match_SaveData_ToPlayer();

            //���� �ݶ��̴�
            weapon_Collider = player.myHands[0].myLeftWeapon.GetComponent<CapsuleCollider2D>();

            //���ش�.
            weapon_Collider.enabled = false;


            //������ �ҷ����� ĳ���Ϳ� �����Ѵ�.
            //Match_StatToPlayer();
        }



        //ĳ���Ͱ� �Դ� �ʿ� ���� ��ġ�� �ݿ��ؾ� �Ѵ�.
        //public void Match_StatToPlayer()
        //{
        //    //ĳ���� �� ������ ����� �ɷ�ġ �ҷ�����, ��ȭ�� �ȵǾ����� ����
        //    if (sync.tempNowUsingIndex == 0)
        //    {
        //        //0 �� ,1 ����, 2 ����, 3 ����, 4 ����, 5 �㸮��, 6 ũ�ν�, 7 �����, 8 ����, 9 ����, 10 ����
        //        for (int i = 0; i < saveData.gameData.equiptable_index1.Length; i++)
        //        {
        //            switch (i)
        //            {
        //                case 0:  //��
        //                         //�ε����� ���� //�ܴ̿� �������� �ε��� 0�� ������ ��� ������
        //                    if (saveData.gameData.equiptable_index1[i] == 0)
        //                    {
        //                        //���ܻ����� ���߿�
        //                    }
        //                    //������ Ÿ���� ����
        //                    bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                    //�з��� ��
        //                    bookInventory.slotItems[i].itemPart = EquipParts.Cloth;

        //                    //�κ��丮 �ּ�
        //                    bookInventory.slotItems[i].unitInventoryAddress = i;
        //                    //������ �ε���
        //                    bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];
        //                    //ȹ��ó�� [0] = ���
        //                    bookInventory.slotItems[i].isGot[0] = true;
        //                    //�� üũ
        //                    bookInventory.slotItems[i].TypeCheck();

        //                    //������ ������ ������.
        //                    sync.MakeItemInform(0,                                          //��ȭ ����
        //                        ref saveData.gameData.equiptable_index1[i],                     //ĳ����1�� ��ü i�ε��� = �������
        //                        ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                        ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                        ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                        ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                        ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                        ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                        ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                        ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                        ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                    bookInventory.slotItems[i].cloth[0].sprite = sync.temp_ChangeImage0;
        //                    bookInventory.slotItems[i].cloth[1].sprite = sync.temp_ChangeImage1;
        //                    bookInventory.slotItems[i].cloth[2].sprite = sync.temp_ChangeImage2;

        //                    //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                    PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,        
        //                        bookInventory.slotItems[i].itemStat.itemAcurrancy,          
        //                        bookInventory.slotItems[i].itemStat.itemCritical,           
        //                        bookInventory.slotItems[i].itemStat.itemDefense,            
        //                        bookInventory.slotItems[i].itemStat.itemEvationRate,        
        //                        bookInventory.slotItems[i].itemStat.itemHp,                 
        //                        bookInventory.slotItems[i].itemStat.itemMp,                 
        //                        bookInventory.slotItems[i].itemStat.itemHpCure,             
        //                        bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    break;
        //                case 1:  //����
        //                         //�ε����� ���� //�ܴ̿� �������� �ε��� 0�� ������ ��� ������
        //                    if (saveData.gameData.equiptable_index1[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Pants;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index1[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].pants[0].sprite = sync.temp_ChangeImage0;
        //                        bookInventory.slotItems[i].pants[1].sprite = sync.temp_ChangeImage1;
                                

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 2:  //����
        //                         //�ε����� ���� //�ܴ̿� �������� �ε��� 0�� ������ ��� ������
        //                    if (saveData.gameData.equiptable_index1[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Helmet;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index1[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                
        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 3:  //����
        //                         //�ε����� ���� //�ܴ̿� �������� �ε��� 0�� ������ ��� ������
        //                    if (saveData.gameData.equiptable_index1[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Armor;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index1[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].armor[0].sprite = sync.temp_ChangeImage0;
        //                        bookInventory.slotItems[i].armor[1].sprite = sync.temp_ChangeImage1;
        //                        bookInventory.slotItems[i].armor[2].sprite = sync.temp_ChangeImage2;

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;

        //                case 4:  //����
        //                    if (saveData.gameData.equiptable_index1[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Back;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index1[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 5:  //�㸮��
        //                    if (saveData.gameData.equiptable_index1[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� �㸮��
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Belt;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();


        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index1[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 6:  //ũ�ν�
        //                    if (saveData.gameData.equiptable_index1[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ũ�ν�
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Cross;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();


        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index1[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 7:  //�����
        //                    if (saveData.gameData.equiptable_index1[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� �����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Necklece;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index1[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 8:  //����
        //                    if (saveData.gameData.equiptable_index1[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Ring;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index1[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                
        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 9:  //����
        //                    if (saveData.gameData.equiptable_index1[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Weapon;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();


        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index1[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].weapon.sprite = sync.temp_ChangeImage0;
                                
        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);

        //                    }
        //                    break;
        //                case 10: //����
        //                    if (saveData.gameData.equiptable_index1[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Shield;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index1[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //            }//switch(��� ����������)

        //        }//For�� ������� �ε��� ��ŭ
        //    }//�÷��̾� 1
        //    if (sync.tempNowUsingIndex == 1)
        //    {
                
        //        //0 �� ,1 ����, 2 ����, 3 ����, 4 ����, 5 �㸮��, 6 ũ�ν�, 7 �����, 8 ����, 9 ����, 10 ����
        //        for (int i = 0; i < saveData.gameData.equiptable_index2.Length; i++)
        //        {
        //            //�ƿ����� ������?
        //            print(bookInventory.slotItems[i].isGot.Length);

        //            switch (i)
        //            {
        //                case 0:  //��
        //                         //�ε����� ���� //�ܴ̿� �������� �ε��� 0�� ������ ��� ������
        //                    if (saveData.gameData.equiptable_index2[i] == 0)
        //                    {
        //                        //���ܻ����� ���߿�
        //                    }
        //                    //������ Ÿ���� ����
        //                    bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                    //�з��� ��
        //                    bookInventory.slotItems[i].itemPart = EquipParts.Cloth;

        //                    //�κ��丮 �ּ�
        //                    bookInventory.slotItems[i].unitInventoryAddress = i;
        //                    //������ �ε���
        //                    bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                    //ȹ��ó�� [0] = ���
        //                    bookInventory.slotItems[i].isGot[0] = true;
        //                    //�� üũ
        //                    bookInventory.slotItems[i].TypeCheck();

        //                    //������ ������ ������.
        //                    sync.MakeItemInform(0,                                          //��ȭ ����
        //                        ref saveData.gameData.equiptable_index2[i],                     //ĳ����1�� i�ε��� = �������
        //                        ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                        ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                        ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                        ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                        ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                        ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                        ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                        ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                        ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                    bookInventory.slotItems[i].cloth[0].sprite = sync.temp_ChangeImage0;
        //                    bookInventory.slotItems[i].cloth[1].sprite = sync.temp_ChangeImage1;
        //                    bookInventory.slotItems[i].cloth[2].sprite = sync.temp_ChangeImage2;

        //                    //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                    PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                        bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                        bookInventory.slotItems[i].itemStat.itemCritical,
        //                        bookInventory.slotItems[i].itemStat.itemDefense,
        //                        bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                        bookInventory.slotItems[i].itemStat.itemHp,
        //                        bookInventory.slotItems[i].itemStat.itemMp,
        //                        bookInventory.slotItems[i].itemStat.itemHpCure,
        //                        bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    break;
        //                case 1:  //����
        //                         //�ε����� ���� //�ܴ̿� �������� �ε��� 0�� ������ ��� ������
        //                    if (saveData.gameData.equiptable_index2[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Pants;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index2[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].pants[0].sprite = sync.temp_ChangeImage0;
        //                        bookInventory.slotItems[i].pants[1].sprite = sync.temp_ChangeImage1;
        //                        bookInventory.slotItems[i].pants[2].sprite = sync.temp_ChangeImage2;

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 2:  //����
        //                         //�ε����� ���� //�ܴ̿� �������� �ε��� 0�� ������ ��� ������
        //                    if (saveData.gameData.equiptable_index2[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Helmet;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index2[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                
        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 3:  //����
        //                         //�ε����� ���� //�ܴ̿� �������� �ε��� 0�� ������ ��� ������
        //                    if (saveData.gameData.equiptable_index2[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Armor;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index2[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].armor[0].sprite = sync.temp_ChangeImage0;
        //                        bookInventory.slotItems[i].armor[1].sprite = sync.temp_ChangeImage1;
        //                        bookInventory.slotItems[i].armor[2].sprite = sync.temp_ChangeImage2;

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;

        //                case 4:  //����
        //                    if (saveData.gameData.equiptable_index2[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Back;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index2[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 5:  //�㸮��
        //                    if (saveData.gameData.equiptable_index2[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� �㸮��
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Belt;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index2[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                


        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 6:  //ũ�ν�
        //                    if (saveData.gameData.equiptable_index2[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ũ�ν�
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Cross;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index2[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 7:  //�����
        //                    if (saveData.gameData.equiptable_index2[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� �����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Necklece;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index2[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 8:  //����
        //                    if (saveData.gameData.equiptable_index2[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Ring;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index2[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 9:  //����
        //                    if (saveData.gameData.equiptable_index2[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Weapon;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index2[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].weapon.sprite = sync.temp_ChangeImage0;
                                


        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 10: //����
        //                    if (saveData.gameData.equiptable_index2[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Shield;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index2[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //            }//switch(��� ����������)

        //        }//For�� ������� �ε��� ��ŭ
        //    }//�÷��̾� 2
        //    if (sync.tempNowUsingIndex == 2)
        //    {
        //        print(bookInventory.slotItems[0] + "����");

        //        //0 �� ,1 ����, 2 ����, 3 ����, 4 ����, 5 �㸮��, 6 ũ�ν�, 7 �����, 8 ����, 9 ����, 10 ����
        //        for (int i = 0; i < saveData.gameData.equiptable_index3.Length; i++)
        //        {
                    
        //            switch (i)
        //            {
        //                case 0:  //��
        //                         //�ε����� ���� //�ܴ̿� �������� �ε��� 0�� ������ ��� ������
        //                    if (saveData.gameData.equiptable_index3[i] == 0)
        //                    {
        //                        //���ܻ����� ���߿�
        //                    }
        //                    //������ Ÿ���� ����
        //                    bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                    //�з��� ��
        //                    bookInventory.slotItems[i].itemPart = EquipParts.Cloth;

        //                    //�κ��丮 �ּ�
        //                    bookInventory.slotItems[i].unitInventoryAddress = i;
        //                    //������ �ε���
        //                    bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                    //ȹ��ó�� [0] = ���
        //                    bookInventory.slotItems[i].isGot[0] = true;
        //                    //�� üũ
        //                    bookInventory.slotItems[i].TypeCheck();

        //                    //������ ������ ������.
        //                    sync.MakeItemInform(0,                                          //��ȭ ����
        //                        ref saveData.gameData.equiptable_index3[i],                     //ĳ����1�� i�ε��� = �������
        //                        ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                        ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                        ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                        ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                        ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                        ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                        ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                        ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                        ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                    bookInventory.slotItems[i].cloth[0].sprite = sync.temp_ChangeImage0;
        //                    bookInventory.slotItems[i].cloth[1].sprite = sync.temp_ChangeImage1;
        //                    bookInventory.slotItems[i].cloth[2].sprite = sync.temp_ChangeImage2;

        //                    //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                    PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                        bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                        bookInventory.slotItems[i].itemStat.itemCritical,
        //                        bookInventory.slotItems[i].itemStat.itemDefense,
        //                        bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                        bookInventory.slotItems[i].itemStat.itemHp,
        //                        bookInventory.slotItems[i].itemStat.itemMp,
        //                        bookInventory.slotItems[i].itemStat.itemHpCure,
        //                        bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    break;
        //                case 1:  //����
        //                         //�ε����� ���� //�ܴ̿� �������� �ε��� 0�� ������ ��� ������
        //                    if (saveData.gameData.equiptable_index3[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Pants;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index3[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].pants[0].sprite = sync.temp_ChangeImage0;
        //                        bookInventory.slotItems[i].pants[1].sprite = sync.temp_ChangeImage1;
        //                        bookInventory.slotItems[i].pants[2].sprite = sync.temp_ChangeImage2;

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 2:  //����
        //                         //�ε����� ���� //�ܴ̿� �������� �ε��� 0�� ������ ��� ������
        //                    if (saveData.gameData.equiptable_index3[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Helmet;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index3[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 3:  //����
        //                         //�ε����� ���� //�ܴ̿� �������� �ε��� 0�� ������ ��� ������
        //                    if (saveData.gameData.equiptable_index3[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Armor;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index3[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].armor[0].sprite = sync.temp_ChangeImage0;
        //                        bookInventory.slotItems[i].armor[1].sprite = sync.temp_ChangeImage1;
        //                        bookInventory.slotItems[i].armor[2].sprite = sync.temp_ChangeImage2;

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;

        //                case 4:  //����
        //                    if (saveData.gameData.equiptable_index3[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Back;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index3[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 5:  //�㸮��
        //                    if (saveData.gameData.equiptable_index3[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� �㸮��
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Belt;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index3[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 6:  //ũ�ν�
        //                    if (saveData.gameData.equiptable_index3[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ũ�ν�
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Cross;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index3[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
 

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 7:  //�����
        //                    if (saveData.gameData.equiptable_index3[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� �����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Necklece;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index3[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 8:  //����
        //                    if (saveData.gameData.equiptable_index3[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Ring;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index3[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 9:  //����
        //                    if (saveData.gameData.equiptable_index3[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Weapon;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index3[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].weapon.sprite = sync.temp_ChangeImage0;
                                

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 10: //����
        //                    if (saveData.gameData.equiptable_index3[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Shield;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index3[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;


        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //            }//switch(��� ����������)

        //        }//For�� ������� �ε��� ��ŭ
        //    }//�÷��̾� 3
        //    if (sync.tempNowUsingIndex == 3)
        //    {
        //        //0 �� ,1 ����, 2 ����, 3 ����, 4 ����, 5 �㸮��, 6 ũ�ν�, 7 �����, 8 ����, 9 ����, 10 ����
        //        for (int i = 0; i < saveData.gameData.equiptable_index4.Length; i++)
        //        {
        //            switch (i)
        //            {
        //                case 0:  //��
        //                         //�ε����� ���� //�ܴ̿� �������� �ε��� 0�� ������ ��� ������
        //                    if (saveData.gameData.equiptable_index4[i] == 0)
        //                    {
        //                        //���ܻ����� ���߿�
        //                    }
        //                    //������ Ÿ���� ����
        //                    bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                    //�з��� ��
        //                    bookInventory.slotItems[i].itemPart = EquipParts.Cloth;

        //                    //�κ��丮 �ּ�
        //                    bookInventory.slotItems[i].unitInventoryAddress = i;
        //                    //������ �ε���
        //                    bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index4[i];

        //                    //ȹ��ó�� [0] = ���
        //                    bookInventory.slotItems[i].isGot[0] = true;
        //                    //�� üũ
        //                    bookInventory.slotItems[i].TypeCheck();

        //                    //������ ������ ������.
        //                    sync.MakeItemInform(0,                                          //��ȭ ����
        //                        ref saveData.gameData.equiptable_index4[i],                     //ĳ����1�� i�ε��� = �������
        //                        ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                        ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                        ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                        ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                        ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                        ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                        ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                        ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                        ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                    bookInventory.slotItems[i].cloth[0].sprite = sync.temp_ChangeImage0;
        //                    bookInventory.slotItems[i].cloth[1].sprite = sync.temp_ChangeImage1;
        //                    bookInventory.slotItems[i].cloth[2].sprite = sync.temp_ChangeImage2;

        //                    //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                    PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                        bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                        bookInventory.slotItems[i].itemStat.itemCritical,
        //                        bookInventory.slotItems[i].itemStat.itemDefense,
        //                        bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                        bookInventory.slotItems[i].itemStat.itemHp,
        //                        bookInventory.slotItems[i].itemStat.itemMp,
        //                        bookInventory.slotItems[i].itemStat.itemHpCure,
        //                        bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    break;
        //                case 1:  //����
        //                         //�ε����� ���� //�ܴ̿� �������� �ε��� 0�� ������ ��� ������
        //                    if (saveData.gameData.equiptable_index4[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Pants;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index4[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index4[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].pants[0].sprite = sync.temp_ChangeImage0;
        //                        bookInventory.slotItems[i].pants[1].sprite = sync.temp_ChangeImage1;
                                

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 2:  //����
        //                         //�ε����� ���� //�ܴ̿� �������� �ε��� 0�� ������ ��� ������
        //                    if (saveData.gameData.equiptable_index4[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Helmet;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index4[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index4[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����                                    
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                              

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 3:  //����
        //                         //�ε����� ���� //�ܴ̿� �������� �ε��� 0�� ������ ��� ������
        //                    if (saveData.gameData.equiptable_index4[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Armor;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index4[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index4[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].armor[0].sprite = sync.temp_ChangeImage0;
        //                        bookInventory.slotItems[i].armor[1].sprite = sync.temp_ChangeImage1;
        //                        bookInventory.slotItems[i].armor[2].sprite = sync.temp_ChangeImage2;

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;

        //                case 4:  //����
        //                    if (saveData.gameData.equiptable_index4[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Back;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index4[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index4[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;


        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 5:  //�㸮��
        //                    if (saveData.gameData.equiptable_index4[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� �㸮��
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Belt;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index4[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index4[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 6:  //ũ�ν�
        //                    if (saveData.gameData.equiptable_index4[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ũ�ν�
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Cross;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index4[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index4[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 7:  //�����
        //                    if (saveData.gameData.equiptable_index4[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� �����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Necklece;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index4[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index4[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 8:  //����
        //                    if (saveData.gameData.equiptable_index4[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Ring;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index4[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index4[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                
        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 9:  //����
        //                    if (saveData.gameData.equiptable_index4[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Weapon;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index4[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index4[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].weapon.sprite = sync.temp_ChangeImage0;
                                

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //                case 10: //����
        //                    if (saveData.gameData.equiptable_index4[i] != 0)
        //                    {
        //                        //���ܻ���
        //                        //������ Ÿ���� ����
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //�з��� ����
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Shield;

        //                        //�κ��丮 �ּ�
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //������ �ε���
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index4[i];

        //                        //ȹ��ó�� [0] = ���
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //�� üũ
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //������ ������ ������.
        //                        sync.MakeItemInform(0,                                          //��ȭ ����
        //                            ref saveData.gameData.equiptable_index4[i],                     //ĳ����1�� i�ε��� = �������
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //�������� ���ݷ�
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //�������� ���߷�
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //�������� ġ���
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //�������� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //�������� ü��
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //�������� ����
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //�������� �ʴ� ü�� ȸ����
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //�������� �ʴ� ���� ȸ����
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //�÷��̾�� �ϰ������� �ɷ�ġ��ŭ ���ϱ��Ѵ�. 
        //                        PlayerAddStat(bookInventory.slotItems[i].itemStat.itemAttack,
        //                            bookInventory.slotItems[i].itemStat.itemAcurrancy,
        //                            bookInventory.slotItems[i].itemStat.itemCritical,
        //                            bookInventory.slotItems[i].itemStat.itemDefense,
        //                            bookInventory.slotItems[i].itemStat.itemEvationRate,
        //                            bookInventory.slotItems[i].itemStat.itemHp,
        //                            bookInventory.slotItems[i].itemStat.itemMp,
        //                            bookInventory.slotItems[i].itemStat.itemHpCure,
        //                            bookInventory.slotItems[i].itemStat.itemMpCure);
        //                    }
        //                    break;
        //            }//switch(��� ����������)

        //        }//For�� ������� �ε��� ��ŭ
        //    }//�÷��̾� 4
        //}

        public void PlayerAddStat(float attacks, float accurancy, float critical, float defense, float evationRate
            ,int hp, int mp, int hpCure, int mpCure)
        {
            player.myAttack += attacks;
            player.myAcurrancy += accurancy;
            player.myCritical += critical;
            player.myDefense += defense;
            player.myEvationRate += evationRate;
            player.myHp += hp;
            player.myMp += mp;
            player.myHpCure += hpCure;
            player.myMpCure += mpCure;
        }
    }

}
