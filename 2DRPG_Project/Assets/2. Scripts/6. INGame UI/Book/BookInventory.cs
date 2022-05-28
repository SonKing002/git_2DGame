using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{
    //������ ���� ���� : �ǿ��� Ŭ���ϸ� ����ǰ� �Ѵ�.
    public enum ItemType
    {
        None = -1,
        Equiptable,
        Consumable,
        Etc,
    }

    //UI �뵵
    public class BookInventory : MonoBehaviour
    {
        //����.https://www.youtube.com/watch?v=iGyd54-mIiE

        /*������ ������ �þ�� �پ��� ��
         * GridLayoutGroup , Image , Content Size filter ���� �������
         
        //94 ���� inventory ������ 4�� �þ������ 
        //1raw�� y ���� += 96
        */

        //�������� ���������� ������ �þ�� �ش� ���� �̹����� ������ �̹����� �־��ش�.
        public GameObject bag_UnderContent;
        int my_Equiptable_num; //ȹ���� ������ ��

        ItemType unitType;

        //�ִ밹�� ����
        public GameObject[] slotView = new GameObject[28];
        //
        public SlotUnit[] slotItems = new SlotUnit[28];

        //���â�� ����â ����ϱ� ����
        public BookEquipment bookEuipment;

        //�� �������� ȹ��������
        bool isGet;
        //�������� ������
        bool isDestroy;
        // �ڸ��� ���� �ٲܶ�
        bool isExchange;

        CursorCtrl cursorCtrl;

        //������ ������ ������
        public Item itemEuiptableDictionary; //��������

        void Awake()//
        {
            //�ʱ�ȭ
            unitType = ItemType.Equiptable;
            isGet = false;
            isExchange = false;
            isDestroy = false;

            //�Ҵ�
            cursorCtrl = FindObjectOfType<CursorCtrl>();

            //�ѹ���
            SyncEquiptableItem();
        }

        //�ǿ��� ��ư�� ������ �ش� Type�� ���� -> SlotUnit ���� ����
        public void OnClick_InventoryTAB_Btn(int i)
        {
            switch (i)
            {
                case 0://���â
                    unitType = ItemType.Equiptable;
                    break;
                    
                case 1://�Һ�â
                    unitType = ItemType.Consumable;
                    break;
                case 2://��Ÿâ
                    unitType = ItemType.Etc;
                    break;
            }
        }

        void Update()
        {
            for (int i = 0; i < slotItems.Length; i++)
            {

                //�޶����ٸ� 
                if (unitType != slotItems[i].unitType //1.invetory Ÿ���� �ٸ���.
                    || isGet || isDestroy             //2.��ų� ������.
                    || isExchange)                    //3. itemIndex�� �޶�����.
                {
                    slotItems[i].isFinishCheck = true;
                }

                if (slotItems[i].isFinishCheck)
                {
                    slotItems[i].TypeCheck();
                }
            }

            /*
            //Ŀ������ Ŭ���Ѵٸ�
            if (cursorCtrl.isClick)
            {
                Plus_OnDrag_ToEquip();
            }
            */

        }

        //�巹�׿��� �������
        public void Plus_OnDrag_ToEquip()
        {
            //+�� ���ش�.
            bookEuipment.PlusImageInit();

            for (int i = 0; i < slotView.Length; i++)
            {
                //�� ��ü�߿��� �巡�� ���� ��ü�� �ִٸ� && ���� Ÿ���� ���â�̶��
                if (slotItems[i].unitType == ItemType.Equiptable)
                {
                    //��� Ÿ�Կ� ���� ���� ������ �ٸ��� �Ѵ�.
                    switch (slotItems[i].itemPart)
                    {
                        case EquipParts.None:
                            
                            print("�ƹ��͵� ����");
                            
                            break;
                        case EquipParts.Helmet:
                            
                            //+�� Ȱ��ȭ�Ѵ�.
                            bookEuipment.Helmet_Slot_img.SetActive(true);
                            break;
                        case EquipParts.Cloth:
                            
                            //+�� Ȱ��ȭ�Ѵ�.
                            bookEuipment.UpCloth_Slot_img.SetActive(true);
                            break;
                        case EquipParts.PantsLeg:
                            
                            //+�� Ȱ��ȭ�Ѵ�.
                            bookEuipment.Leg_Slot_img.SetActive(true);
                            break;
                        case EquipParts.Armor:
                            //+�� Ȱ��ȭ�Ѵ�.
                            bookEuipment.Armor_Slot_img.SetActive(true);
                            break;
                        case EquipParts.Back:
                            //+�� Ȱ��ȭ�Ѵ�.
                            bookEuipment.Back_Slot_img.SetActive(true);
                            break;
                        case EquipParts.Weapon:
                            //+�� Ȱ��ȭ�Ѵ�.
                            bookEuipment.LeftWeapon_Slot_img.SetActive(true);
                            break;
                        case EquipParts.Shield:
                            //+�� Ȱ��ȭ�Ѵ�.
                            bookEuipment.Right_Weapon_Slot_img.SetActive(true);
                            break;
                        case EquipParts.Belt:
                            //+�� Ȱ��ȭ�Ѵ�.
                            bookEuipment.BeltAccessory_Slot_img.SetActive(true);
                            break;
                        case EquipParts.Cross:
                            //+�� Ȱ��ȭ�Ѵ�.
                            bookEuipment.CrossAccessory_Slot_img.SetActive(true);
                            break;
                        case EquipParts.Necklace:
                            //+�� Ȱ��ȭ�Ѵ�.
                            bookEuipment.Neckless_Slot_img.SetActive(true);
                            break;
                        case EquipParts.Ring:
                            //+�� Ȱ��ȭ�Ѵ�.
                            bookEuipment.Ring_Slot_img.SetActive(true);
                            break;
                    }//switch
                }//if
                //�巹�� ���� �ƴҶ� && ��� �������̶�� 
                /*
                if (slotItems[i].unitType == ItemType.Equiptable)
                {
                    //+�� ���ش�.
                    bookEuipment.PlusImageInit();
                }
                */
            }//for
        }//OnDrags

        //��񰡴��� ������ ȹ��� �Լ�
        public void SyncEquiptableItem()
        {
            for (int i = 0; i < slotView.Length; i++)
            {
                //����
                slotItems[i] = slotView[i].GetComponent<SlotUnit>();
                //������ ������ �ּҰ� ����
                slotItems[i].unitInventoryAddress = i;
            }
        }

        //�Һ񰡴��� ������ �Լ�
        public void SyncConsumableItem()
        { 
        
        }

        //��Ÿ����ϴ� ������ �Լ�
        public void SyncETCItem()
        { 
        
        }

   
    }//class


}//