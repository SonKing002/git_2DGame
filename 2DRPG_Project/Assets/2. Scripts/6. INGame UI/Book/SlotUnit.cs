using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;

namespace Main
{

    //아이템 오브젝트마다 객체에 저장될 정보 스크립트 (기본 사항)
    //0~ + (누적 오름차순):  순서, 맞바꾸기, 정렬은 UintsAline
    public class SlotUnit : MonoBehaviour
    {
        //인터페이스창에서 정보값 저장용
        public int unitInventoryAddress;
        //장비 아이템스크립트의 인덱스 순서
        public int itemIndex;


        //아이템의 구분
        public ItemType unitType = ItemType.Equiptable;
        //아이템 파츠구분
        public Image[] armor = new Image[3];
        public Image[] cloth = new Image[3];
        public Image[] pants = new Image[2];
        public Image weapon;
        public Image elseItem;
        //0 옷, 1 바지, 3 아머, 3 무기, 4 나머지
        public GameObject[] slotItemObject = new GameObject[5];
        StickLeverContrl drageCtrl;

        //호출 제어용(업데이트에서 계속 돌면 낭비)
        public bool isFinishCheck;

        //소유하고 있다면 true
        public bool[] isGot = new bool[3];
        //장비를 착용하고 있다면 true (소비용 퀵슬롯, 장비창)
        public bool isUsingNow;

        //장비아이템의 스탯 구성정보
        public EquipmentStat itemStat;
        //장비 착용의 정보 
        public EquipParts itemPart = EquipParts.None;

        //구성
        [System.Serializable]
        public struct EquipmentStat
        {
            //아이템 인덱스
            public int itemIndex;
            //능력치
            public int itemHp;
            public int itemMp;

            //아이템에 의한 추가 능력치
            public float itemAttack;
            public float itemAcurrancy;
            public float itemCritical;
            public float itemDefense;
            public float itemEvationRate;

            //체력 마력 회복률
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

        //아이템 파츠 parts[]에 해당하면 해당 파츠를 활성화해주는 함수
        public void E_PartsIndex()
        { 
        
        }


        //해당 아이템 구성요소
        public void SetItem(int getAddress,  ItemType getType, bool isGet, bool isEquip)
        {

            //아이템 소지 주소값 저장용
            unitInventoryAddress = getAddress;
            //아이템 유형 
            unitType = getType;

            //아이템 유형에 따라서
            switch (unitType)
            {
                case ItemType.Equiptable://장비아이템

                    //현재 착용하고 있는지 대입
                    isUsingNow = isEquip;

                    //획득했는지 
                    if (isGot[0] == isGet)
                    {
                        //대입하기
                        TypeCheck();
                    }
                    break;
                case ItemType.Consumable://소비아이템

                    //획득했는지 
                    if (isGot[1] == isGet)
                    {
                        //대입하기
                        TypeCheck();
                    }
                    break;
                case ItemType.Etc://기타아이템

                    //획득했는지 
                    if (isGot[2] == isGet)
                    {
                        //대입하기
                        TypeCheck();
                    }
                    break;

            }
            //(ItemProtocol에서 true 아이템이 일치하는지 판단) -> 일치 BookEquipment 이미지 보여주기 -> isUsingNow 착용했는지 여부 결정 
        }

        //타입별로 체크
        public void TypeCheck()
        {
            //대입하기
            switch (unitType)
            {
                //장비창 전용
                case (ItemType.Equiptable):

                    //true 일때
                    if (isGot[0])
                    {
                        //획득했다면 게임 오브젝트 활성화 한다. (게임 오브젝트 비활성화 상태에서 수정하면 null reference)
                        switch (itemPart)
                        {
                            //스프라이트 갯수에 따라
                            case EquipParts.Cloth://3개
                                ActiveCtrl(0);

                                break;
                            case EquipParts.PantsLeg://2개
                                ActiveCtrl(1);

                                break;
                            case EquipParts.Armor://3개
                                ActiveCtrl(2);

                                break;
                            case EquipParts.Weapon://1개
                                ActiveCtrl(3);

                                break;
                           //1개
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
                //소비창 전용
                case (ItemType.Consumable):
                    break;
                //기타창 전용
                case (ItemType.Etc):
                    break;
            }
        }

        //액티브 여부 활용 
        public void ActiveCtrl(int j)
        {
            //일단 전부 비활성화
            for (int i = 0; slotItemObject.Length < i; i++)
            {
                slotItemObject[i].SetActive(false);
            }
            //해당 오브젝트 활성화 
            slotItemObject[j].SetActive(true);
        }

        //버리면
        public void Destroy()
        {
            gameObject.SetActive(false);
            // 초기화

        }

    }
}

