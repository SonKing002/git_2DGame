using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;

namespace Main
{

    //������ ������Ʈ���� ��ü�� ����� ���� ��ũ��Ʈ (�⺻ ����)
    //0~ + (���� ��������):  ����, �¹ٲٱ�, ������ UintsAline
    public class SlotUnit : MonoBehaviour
    {
        //�������̽�â���� ������ �����
        public int unitInventoryAddress;
        //��� �����۽�ũ��Ʈ�� �ε��� ����
        public int itemIndex;


        //�������� ����
        public ItemType unitType = ItemType.Equiptable;
        //������ ��������
        public Image[] armor = new Image[3];
        public Image[] cloth = new Image[3];
        public Image[] pants = new Image[2];
        public Image weapon;
        public Image elseItem;
        //0 ��, 1 ����, 3 �Ƹ�, 3 ����, 4 ������
        public GameObject[] slotItemObject = new GameObject[5];
        StickLeverContrl drageCtrl;

        //ȣ�� �����(������Ʈ���� ��� ���� ����)
        public bool isFinishCheck;

        //�����ϰ� �ִٸ� true
        public bool[] isGot = new bool[3];
        //��� �����ϰ� �ִٸ� true (�Һ�� ������, ���â)
        public bool isUsingNow;

        //���������� ���� ��������
        public EquipmentStat itemStat;
        //��� ������ ���� 
        public EquipParts itemPart = EquipParts.None;

        //����
        [System.Serializable]
        public struct EquipmentStat
        {
            //������ �ε���
            public int itemIndex;
            //�ɷ�ġ
            public int itemHp;
            public int itemMp;

            //�����ۿ� ���� �߰� �ɷ�ġ
            public float itemAttack;
            public float itemAcurrancy;
            public float itemCritical;
            public float itemDefense;
            public float itemEvationRate;

            //ü�� ���� ȸ����
            public int itemHpCure;
            public int itemMpCure;
        }

        void Start()
        {
            isFinishCheck = false;
            drageCtrl = gameObject.GetComponent<StickLeverContrl>();
    }


        public void ChangeInventoryAddress(int index)
        {
            unitInventoryAddress = index;
        }
        public void GameItemIndex(int index)
        { 
        
        }

        //������ ���� parts[]�� �ش��ϸ� �ش� ������ Ȱ��ȭ���ִ� �Լ�
        public void E_PartsIndex()
        { 
        
        }


        //�ش� ������ �������
        public void SetItem(int getAddress,  ItemType getType, bool isGet, bool isEquip)
        {

            //������ ���� �ּҰ� �����
            unitInventoryAddress = getAddress;
            //������ ���� 
            unitType = getType;

            //������ ������ ����
            switch (unitType)
            {
                case ItemType.Equiptable://��������

                    //���� �����ϰ� �ִ��� ����
                    isUsingNow = isEquip;

                    //ȹ���ߴ��� 
                    if (isGot[0] == isGet)
                    {
                        //�����ϱ�
                        TypeCheck();
                    }
                    break;
                case ItemType.Consumable://�Һ������

                    //ȹ���ߴ��� 
                    if (isGot[1] == isGet)
                    {
                        //�����ϱ�
                        TypeCheck();
                    }
                    break;
                case ItemType.Etc://��Ÿ������

                    //ȹ���ߴ��� 
                    if (isGot[2] == isGet)
                    {
                        //�����ϱ�
                        TypeCheck();
                    }
                    break;

            }
            //(ItemProtocol���� true �������� ��ġ�ϴ��� �Ǵ�) -> ��ġ BookEquipment �̹��� �����ֱ� -> isUsingNow �����ߴ��� ���� ���� 
        }

        //Ÿ�Ժ��� üũ
        public void TypeCheck()
        {
            //�����ϱ�
            switch (unitType)
            {
                //���â ����
                case (ItemType.Equiptable):

                    //true �϶�
                    if (isGot[0])
                    {
                        //ȹ���ߴٸ� ���� ������Ʈ Ȱ��ȭ �Ѵ�. (���� ������Ʈ ��Ȱ��ȭ ���¿��� �����ϸ� null reference)
                        switch (itemPart)
                        {
                            //��������Ʈ ������ ����
                            case EquipParts.Cloth://3��
                                ActiveCtrl(0);

                                break;
                            case EquipParts.PantsLeg://2��
                                ActiveCtrl(1);

                                break;
                            case EquipParts.Armor://3��
                                ActiveCtrl(2);

                                break;
                            case EquipParts.Weapon://1��
                                ActiveCtrl(3);

                                break;
                           //1��
                            case EquipParts.Back:
                            case EquipParts.Belt:
                            case EquipParts.Cross:
                            case EquipParts.Helmet:
                            case EquipParts.Necklace:
                            case EquipParts.Ring:
                            case EquipParts.Shield:
                                ActiveCtrl(4);
                                
                                break;
                        }
                    }
                    else //if (!isGot)
                    {
                        for (int i = 0; i < slotItemObject.Length; i++)
                        {
                            slotItemObject[i].SetActive(false);
                        }
                    }

                    break;
                //�Һ�â ����
                case (ItemType.Consumable):
                    break;
                //��Ÿâ ����
                case (ItemType.Etc):
                    break;
            }
        }

        //��Ƽ�� ���� Ȱ�� 
        public void ActiveCtrl(int j)
        {
            //�ϴ� ���� ��Ȱ��ȭ
            for (int i = 0; slotItemObject.Length < i; i++)
            {
                slotItemObject[i].SetActive(false);
            }
            //�ش� ������Ʈ Ȱ��ȭ 
            slotItemObject[j].SetActive(true);
        }

        //������
        public void Destroy()
        {
            gameObject.SetActive(false);
            // �ʱ�ȭ

        }

    }
}

