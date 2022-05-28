using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{
    //아이템 종류 정보 : 탭에서 클릭하면 변경되게 한다.
    public enum ItemType
    {
        None = -1,
        Equiptable,
        Consumable,
        Etc,
    }

    //UI 용도
    public class BookInventory : MonoBehaviour
    {
        //참고.https://www.youtube.com/watch?v=iGyd54-mIiE

        /*아이템 슬롯이 늘어나고 줄어드는 것
         * GridLayoutGroup , Image , Content Size filter 으로 만들었음
         
        //94 시작 inventory 슬롯이 4개 늘어갈때마다 
        //1raw당 y 증가 += 96
        */

        //아이템을 얻을때마다 슬롯이 늘어나고 해당 슬롯 이미지에 아이템 이미지를 넣어준다.
        public GameObject bag_UnderContent;
        int my_Equiptable_num; //획득한 아이템 수

        ItemType unitType;

        //최대갯수 제한
        public GameObject[] slotView = new GameObject[28];
        //
        public SlotUnit[] slotItems = new SlotUnit[28];

        //장비창과 가방창 통신하기 위함
        public BookEquipment bookEuipment;

        //새 아이템을 획득했을때
        bool isGet;
        //아이템을 버릴때
        bool isDestroy;
        // 자리를 서로 바꿀때
        bool isExchange;

        CursorCtrl cursorCtrl;

        //참고할 아이템 사전들
        public Item itemEuiptableDictionary; //장비아이템

        void Awake()//
        {
            //초기화
            unitType = ItemType.Equiptable;
            isGet = false;
            isExchange = false;
            isDestroy = false;

            //할당
            cursorCtrl = FindObjectOfType<CursorCtrl>();

            //한번만
            SyncEquiptableItem();
        }

        //탭에서 버튼을 누르면 해당 Type이 대입 -> SlotUnit 변경 가능
        public void OnClick_InventoryTAB_Btn(int i)
        {
            switch (i)
            {
                case 0://장비창
                    unitType = ItemType.Equiptable;
                    break;
                    
                case 1://소비창
                    unitType = ItemType.Consumable;
                    break;
                case 2://기타창
                    unitType = ItemType.Etc;
                    break;
            }
        }

        void Update()
        {
            for (int i = 0; i < slotItems.Length; i++)
            {

                //달라진다면 
                if (unitType != slotItems[i].unitType //1.invetory 타입이 다르다.
                    || isGet || isDestroy             //2.얻거나 버리다.
                    || isExchange)                    //3. itemIndex가 달라졌다.
                {
                    slotItems[i].isFinishCheck = true;
                }

                if (slotItems[i].isFinishCheck)
                {
                    slotItems[i].TypeCheck();
                }
            }

            /*
            //커서에서 클릭한다면
            if (cursorCtrl.isClick)
            {
                Plus_OnDrag_ToEquip();
            }
            */

        }

        //드레그에서 착용까지
        public void Plus_OnDrag_ToEquip()
        {
            //+를 꺼준다.
            bookEuipment.PlusImageInit();

            for (int i = 0; i < slotView.Length; i++)
            {
                //각 객체중에서 드래그 중인 객체가 있다면 && 유닛 타입이 장비창이라면
                if (slotItems[i].unitType == ItemType.Equiptable)
                {
                    //장비 타입에 따라 착용 파츠를 다르게 한다.
                    switch (slotItems[i].itemPart)
                    {
                        case EquipParts.None:
                            
                            print("아무것도 없다");
                            
                            break;
                        case EquipParts.Helmet:
                            
                            //+를 활성화한다.
                            bookEuipment.Helmet_Slot_img.SetActive(true);
                            break;
                        case EquipParts.Cloth:
                            
                            //+를 활성화한다.
                            bookEuipment.UpCloth_Slot_img.SetActive(true);
                            break;
                        case EquipParts.PantsLeg:
                            
                            //+를 활성화한다.
                            bookEuipment.Leg_Slot_img.SetActive(true);
                            break;
                        case EquipParts.Armor:
                            //+를 활성화한다.
                            bookEuipment.Armor_Slot_img.SetActive(true);
                            break;
                        case EquipParts.Back:
                            //+를 활성화한다.
                            bookEuipment.Back_Slot_img.SetActive(true);
                            break;
                        case EquipParts.Weapon:
                            //+를 활성화한다.
                            bookEuipment.LeftWeapon_Slot_img.SetActive(true);
                            break;
                        case EquipParts.Shield:
                            //+를 활성화한다.
                            bookEuipment.Right_Weapon_Slot_img.SetActive(true);
                            break;
                        case EquipParts.Belt:
                            //+를 활성화한다.
                            bookEuipment.BeltAccessory_Slot_img.SetActive(true);
                            break;
                        case EquipParts.Cross:
                            //+를 활성화한다.
                            bookEuipment.CrossAccessory_Slot_img.SetActive(true);
                            break;
                        case EquipParts.Necklace:
                            //+를 활성화한다.
                            bookEuipment.Neckless_Slot_img.SetActive(true);
                            break;
                        case EquipParts.Ring:
                            //+를 활성화한다.
                            bookEuipment.Ring_Slot_img.SetActive(true);
                            break;
                    }//switch
                }//if
                //드레그 중이 아닐때 && 장비 아이템이라면 
                /*
                if (slotItems[i].unitType == ItemType.Equiptable)
                {
                    //+를 꺼준다.
                    bookEuipment.PlusImageInit();
                }
                */
            }//for
        }//OnDrags

        //장비가능한 아이템 획득시 함수
        public void SyncEquiptableItem()
        {
            for (int i = 0; i < slotView.Length; i++)
            {
                //대입
                slotItems[i] = slotView[i].GetComponent<SlotUnit>();
                //아이템 슬롯의 주소값 저장
                slotItems[i].unitInventoryAddress = i;
            }
        }

        //소비가능한 아이템 함수
        public void SyncConsumableItem()
        { 
        
        }

        //기타사용하는 아이템 함수
        public void SyncETCItem()
        { 
        
        }

   
    }//class


}//