using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

namespace Main
{

    //인게임에서 불러올 싱크이며 대입하는 역할과 나가기 전 저장할 수 있게 하는 함수
    public class Player_Sync : MonoBehaviour
    {
        //저장 관리
        public DataController saveData;
        //싱크 스크립트 내용
        public Sync_DataCtrlPlayer sync;

        //slot
        public BookInventory bookInventory;
        //플래이어에게 최종 담아주기
        EnumPlayer_Information player;

        CapsuleCollider2D weapon_Collider;

        void Start()
        {
            //할당
            player = gameObject.GetComponent<EnumPlayer_Information>();

            //정보 로드하기
            saveData.LoadGameData();

            //인덱스 동기화
            sync.tempNowUsingIndex = saveData.gameData.nowUsingIndex;

            print(saveData.gameData.nowUsingIndex + " : 세이브된 인덱스 " + sync.tempNowUsingIndex + " : 현재 받아온 인덱스 ");

            //캐릭터 불러오기
            sync.Match_SaveData_ToPlayer();

            //무기 콜라이더
            weapon_Collider = player.myHands[0].myLeftWeapon.GetComponent<CapsuleCollider2D>();

            //꺼준다.
            weapon_Collider.enabled = false;


            //스텟을 불러오고 캐릭터에 대입한다.
            //Match_StatToPlayer();
        }



        //캐릭터가 입는 옷에 대해 수치를 반영해야 한다.
        //public void Match_StatToPlayer()
        //{
        //    //캐릭터 별 착용중 장비의 능력치 불러오기, 강화가 안되었을때 기준
        //    if (sync.tempNowUsingIndex == 0)
        //    {
        //        //0 옷 ,1 바지, 2 투구, 3 갑옷, 4 망또, 5 허리띠, 6 크로스, 7 목걸이, 8 반지, 9 무기, 10 방패
        //        for (int i = 0; i < saveData.gameData.equiptable_index1.Length; i++)
        //        {
        //            switch (i)
        //            {
        //                case 0:  //옷
        //                         //인덱스가 있음 //이외는 파츠에서 인덱스 0이 들어오면 장비 미착용
        //                    if (saveData.gameData.equiptable_index1[i] == 0)
        //                    {
        //                        //예외사항은 나중에
        //                    }
        //                    //아이템 타입은 장비류
        //                    bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                    //분류는 옷
        //                    bookInventory.slotItems[i].itemPart = EquipParts.Cloth;

        //                    //인벤토리 주소
        //                    bookInventory.slotItems[i].unitInventoryAddress = i;
        //                    //아이템 인덱스
        //                    bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];
        //                    //획득처리 [0] = 장비
        //                    bookInventory.slotItems[i].isGot[0] = true;
        //                    //후 체크
        //                    bookInventory.slotItems[i].TypeCheck();

        //                    //아이템 정보를 가진다.
        //                    sync.MakeItemInform(0,                                          //강화 상태
        //                        ref saveData.gameData.equiptable_index1[i],                     //캐릭터1의 전체 i인덱스 = 장비파츠
        //                        ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                        ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                        ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                        ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                        ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                        ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                        ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                        ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                        ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                    bookInventory.slotItems[i].cloth[0].sprite = sync.temp_ChangeImage0;
        //                    bookInventory.slotItems[i].cloth[1].sprite = sync.temp_ChangeImage1;
        //                    bookInventory.slotItems[i].cloth[2].sprite = sync.temp_ChangeImage2;

        //                    //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 1:  //바지
        //                         //인덱스가 있음 //이외는 파츠에서 인덱스 0이 들어오면 장비 미착용
        //                    if (saveData.gameData.equiptable_index1[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 바지
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Pants;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index1[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].pants[0].sprite = sync.temp_ChangeImage0;
        //                        bookInventory.slotItems[i].pants[1].sprite = sync.temp_ChangeImage1;
                                

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 2:  //투구
        //                         //인덱스가 있음 //이외는 파츠에서 인덱스 0이 들어오면 장비 미착용
        //                    if (saveData.gameData.equiptable_index1[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 투구
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Helmet;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index1[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                
        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 3:  //갑옷
        //                         //인덱스가 있음 //이외는 파츠에서 인덱스 0이 들어오면 장비 미착용
        //                    if (saveData.gameData.equiptable_index1[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 갑옷
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Armor;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index1[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].armor[0].sprite = sync.temp_ChangeImage0;
        //                        bookInventory.slotItems[i].armor[1].sprite = sync.temp_ChangeImage1;
        //                        bookInventory.slotItems[i].armor[2].sprite = sync.temp_ChangeImage2;

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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

        //                case 4:  //망또
        //                    if (saveData.gameData.equiptable_index1[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 망또
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Back;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index1[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 5:  //허리띠
        //                    if (saveData.gameData.equiptable_index1[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 허리띠
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Belt;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();


        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index1[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 6:  //크로스
        //                    if (saveData.gameData.equiptable_index1[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 크로스
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Cross;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();


        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index1[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 7:  //목걸이
        //                    if (saveData.gameData.equiptable_index1[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 목걸이
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Necklece;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index1[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 8:  //반지
        //                    if (saveData.gameData.equiptable_index1[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 반지
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Ring;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index1[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                
        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 9:  //무기
        //                    if (saveData.gameData.equiptable_index1[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 무기
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Weapon;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();


        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index1[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].weapon.sprite = sync.temp_ChangeImage0;
                                
        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 10: //방패
        //                    if (saveData.gameData.equiptable_index1[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 방패
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Shield;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index1[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //            }//switch(장비 착용파츠별)

        //        }//For문 장비착용 인덱스 만큼
        //    }//플레이어 1
        //    if (sync.tempNowUsingIndex == 1)
        //    {
                
        //        //0 옷 ,1 바지, 2 투구, 3 갑옷, 4 망또, 5 허리띠, 6 크로스, 7 목걸이, 8 반지, 9 무기, 10 방패
        //        for (int i = 0; i < saveData.gameData.equiptable_index2.Length; i++)
        //        {
        //            //아웃오브 레인지?
        //            print(bookInventory.slotItems[i].isGot.Length);

        //            switch (i)
        //            {
        //                case 0:  //옷
        //                         //인덱스가 있음 //이외는 파츠에서 인덱스 0이 들어오면 장비 미착용
        //                    if (saveData.gameData.equiptable_index2[i] == 0)
        //                    {
        //                        //예외사항은 나중에
        //                    }
        //                    //아이템 타입은 장비류
        //                    bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                    //분류는 옷
        //                    bookInventory.slotItems[i].itemPart = EquipParts.Cloth;

        //                    //인벤토리 주소
        //                    bookInventory.slotItems[i].unitInventoryAddress = i;
        //                    //아이템 인덱스
        //                    bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                    //획득처리 [0] = 장비
        //                    bookInventory.slotItems[i].isGot[0] = true;
        //                    //후 체크
        //                    bookInventory.slotItems[i].TypeCheck();

        //                    //아이템 정보를 가진다.
        //                    sync.MakeItemInform(0,                                          //강화 상태
        //                        ref saveData.gameData.equiptable_index2[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                        ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                        ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                        ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                        ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                        ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                        ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                        ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                        ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                        ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                    bookInventory.slotItems[i].cloth[0].sprite = sync.temp_ChangeImage0;
        //                    bookInventory.slotItems[i].cloth[1].sprite = sync.temp_ChangeImage1;
        //                    bookInventory.slotItems[i].cloth[2].sprite = sync.temp_ChangeImage2;

        //                    //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 1:  //바지
        //                         //인덱스가 있음 //이외는 파츠에서 인덱스 0이 들어오면 장비 미착용
        //                    if (saveData.gameData.equiptable_index2[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 바지
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Pants;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index2[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].pants[0].sprite = sync.temp_ChangeImage0;
        //                        bookInventory.slotItems[i].pants[1].sprite = sync.temp_ChangeImage1;
        //                        bookInventory.slotItems[i].pants[2].sprite = sync.temp_ChangeImage2;

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 2:  //투구
        //                         //인덱스가 있음 //이외는 파츠에서 인덱스 0이 들어오면 장비 미착용
        //                    if (saveData.gameData.equiptable_index2[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 투구
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Helmet;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index2[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                
        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 3:  //갑옷
        //                         //인덱스가 있음 //이외는 파츠에서 인덱스 0이 들어오면 장비 미착용
        //                    if (saveData.gameData.equiptable_index2[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 갑옷
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Armor;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index2[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].armor[0].sprite = sync.temp_ChangeImage0;
        //                        bookInventory.slotItems[i].armor[1].sprite = sync.temp_ChangeImage1;
        //                        bookInventory.slotItems[i].armor[2].sprite = sync.temp_ChangeImage2;

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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

        //                case 4:  //망또
        //                    if (saveData.gameData.equiptable_index2[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 망또
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Back;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index2[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 5:  //허리띠
        //                    if (saveData.gameData.equiptable_index2[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 허리띠
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Belt;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index2[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                


        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 6:  //크로스
        //                    if (saveData.gameData.equiptable_index2[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 크로스
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Cross;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index2[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 7:  //목걸이
        //                    if (saveData.gameData.equiptable_index2[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 목걸이
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Necklece;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index2[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 8:  //반지
        //                    if (saveData.gameData.equiptable_index2[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 반지
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Ring;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index2[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 9:  //무기
        //                    if (saveData.gameData.equiptable_index2[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 무기
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Weapon;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index2[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].weapon.sprite = sync.temp_ChangeImage0;
                                


        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 10: //방패
        //                    if (saveData.gameData.equiptable_index2[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 방패
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Shield;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index2[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //            }//switch(장비 착용파츠별)

        //        }//For문 장비착용 인덱스 만큼
        //    }//플레이어 2
        //    if (sync.tempNowUsingIndex == 2)
        //    {
        //        print(bookInventory.slotItems[0] + "슬롯");

        //        //0 옷 ,1 바지, 2 투구, 3 갑옷, 4 망또, 5 허리띠, 6 크로스, 7 목걸이, 8 반지, 9 무기, 10 방패
        //        for (int i = 0; i < saveData.gameData.equiptable_index3.Length; i++)
        //        {
                    
        //            switch (i)
        //            {
        //                case 0:  //옷
        //                         //인덱스가 있음 //이외는 파츠에서 인덱스 0이 들어오면 장비 미착용
        //                    if (saveData.gameData.equiptable_index3[i] == 0)
        //                    {
        //                        //예외사항은 나중에
        //                    }
        //                    //아이템 타입은 장비류
        //                    bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                    //분류는 옷
        //                    bookInventory.slotItems[i].itemPart = EquipParts.Cloth;

        //                    //인벤토리 주소
        //                    bookInventory.slotItems[i].unitInventoryAddress = i;
        //                    //아이템 인덱스
        //                    bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                    //획득처리 [0] = 장비
        //                    bookInventory.slotItems[i].isGot[0] = true;
        //                    //후 체크
        //                    bookInventory.slotItems[i].TypeCheck();

        //                    //아이템 정보를 가진다.
        //                    sync.MakeItemInform(0,                                          //강화 상태
        //                        ref saveData.gameData.equiptable_index3[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                        ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                        ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                        ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                        ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                        ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                        ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                        ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                        ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                        ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                    bookInventory.slotItems[i].cloth[0].sprite = sync.temp_ChangeImage0;
        //                    bookInventory.slotItems[i].cloth[1].sprite = sync.temp_ChangeImage1;
        //                    bookInventory.slotItems[i].cloth[2].sprite = sync.temp_ChangeImage2;

        //                    //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 1:  //바지
        //                         //인덱스가 있음 //이외는 파츠에서 인덱스 0이 들어오면 장비 미착용
        //                    if (saveData.gameData.equiptable_index3[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 바지
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Pants;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index3[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].pants[0].sprite = sync.temp_ChangeImage0;
        //                        bookInventory.slotItems[i].pants[1].sprite = sync.temp_ChangeImage1;
        //                        bookInventory.slotItems[i].pants[2].sprite = sync.temp_ChangeImage2;

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 2:  //투구
        //                         //인덱스가 있음 //이외는 파츠에서 인덱스 0이 들어오면 장비 미착용
        //                    if (saveData.gameData.equiptable_index3[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 투구
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Helmet;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index3[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 3:  //갑옷
        //                         //인덱스가 있음 //이외는 파츠에서 인덱스 0이 들어오면 장비 미착용
        //                    if (saveData.gameData.equiptable_index3[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 갑옷
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Armor;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index3[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].armor[0].sprite = sync.temp_ChangeImage0;
        //                        bookInventory.slotItems[i].armor[1].sprite = sync.temp_ChangeImage1;
        //                        bookInventory.slotItems[i].armor[2].sprite = sync.temp_ChangeImage2;

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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

        //                case 4:  //망또
        //                    if (saveData.gameData.equiptable_index3[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 망또
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Back;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index3[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 5:  //허리띠
        //                    if (saveData.gameData.equiptable_index3[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 허리띠
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Belt;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index3[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 6:  //크로스
        //                    if (saveData.gameData.equiptable_index3[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 크로스
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Cross;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index3[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
 

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 7:  //목걸이
        //                    if (saveData.gameData.equiptable_index3[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 목걸이
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Necklece;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index3[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 8:  //반지
        //                    if (saveData.gameData.equiptable_index3[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 반지
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Ring;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index3[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 9:  //무기
        //                    if (saveData.gameData.equiptable_index3[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 무기
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Weapon;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index3[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].weapon.sprite = sync.temp_ChangeImage0;
                                

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 10: //방패
        //                    if (saveData.gameData.equiptable_index3[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 방패
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Shield;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index1[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index3[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;


        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //            }//switch(장비 착용파츠별)

        //        }//For문 장비착용 인덱스 만큼
        //    }//플레이어 3
        //    if (sync.tempNowUsingIndex == 3)
        //    {
        //        //0 옷 ,1 바지, 2 투구, 3 갑옷, 4 망또, 5 허리띠, 6 크로스, 7 목걸이, 8 반지, 9 무기, 10 방패
        //        for (int i = 0; i < saveData.gameData.equiptable_index4.Length; i++)
        //        {
        //            switch (i)
        //            {
        //                case 0:  //옷
        //                         //인덱스가 있음 //이외는 파츠에서 인덱스 0이 들어오면 장비 미착용
        //                    if (saveData.gameData.equiptable_index4[i] == 0)
        //                    {
        //                        //예외사항은 나중에
        //                    }
        //                    //아이템 타입은 장비류
        //                    bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                    //분류는 옷
        //                    bookInventory.slotItems[i].itemPart = EquipParts.Cloth;

        //                    //인벤토리 주소
        //                    bookInventory.slotItems[i].unitInventoryAddress = i;
        //                    //아이템 인덱스
        //                    bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index4[i];

        //                    //획득처리 [0] = 장비
        //                    bookInventory.slotItems[i].isGot[0] = true;
        //                    //후 체크
        //                    bookInventory.slotItems[i].TypeCheck();

        //                    //아이템 정보를 가진다.
        //                    sync.MakeItemInform(0,                                          //강화 상태
        //                        ref saveData.gameData.equiptable_index4[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                        ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                        ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                        ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                        ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                        ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                        ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                        ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                        ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                        ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                    bookInventory.slotItems[i].cloth[0].sprite = sync.temp_ChangeImage0;
        //                    bookInventory.slotItems[i].cloth[1].sprite = sync.temp_ChangeImage1;
        //                    bookInventory.slotItems[i].cloth[2].sprite = sync.temp_ChangeImage2;

        //                    //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 1:  //바지
        //                         //인덱스가 있음 //이외는 파츠에서 인덱스 0이 들어오면 장비 미착용
        //                    if (saveData.gameData.equiptable_index4[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 바지
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Pants;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index4[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index4[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].pants[0].sprite = sync.temp_ChangeImage0;
        //                        bookInventory.slotItems[i].pants[1].sprite = sync.temp_ChangeImage1;
                                

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 2:  //투구
        //                         //인덱스가 있음 //이외는 파츠에서 인덱스 0이 들어오면 장비 미착용
        //                    if (saveData.gameData.equiptable_index4[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 투구
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Helmet;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index4[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index4[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율                                    
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                              

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 3:  //갑옷
        //                         //인덱스가 있음 //이외는 파츠에서 인덱스 0이 들어오면 장비 미착용
        //                    if (saveData.gameData.equiptable_index4[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 갑옷
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Armor;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index4[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index4[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].armor[0].sprite = sync.temp_ChangeImage0;
        //                        bookInventory.slotItems[i].armor[1].sprite = sync.temp_ChangeImage1;
        //                        bookInventory.slotItems[i].armor[2].sprite = sync.temp_ChangeImage2;

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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

        //                case 4:  //망또
        //                    if (saveData.gameData.equiptable_index4[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 망또
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Back;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index4[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index4[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;


        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 5:  //허리띠
        //                    if (saveData.gameData.equiptable_index4[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 허리띠
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Belt;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index4[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index4[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 6:  //크로스
        //                    if (saveData.gameData.equiptable_index4[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 크로스
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Cross;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index4[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index4[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 7:  //목걸이
        //                    if (saveData.gameData.equiptable_index4[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 목걸이
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Necklece;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index4[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index4[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 8:  //반지
        //                    if (saveData.gameData.equiptable_index4[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 반지
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Ring;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index4[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index4[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                
        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 9:  //무기
        //                    if (saveData.gameData.equiptable_index4[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 무기
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Weapon;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index4[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index4[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].weapon.sprite = sync.temp_ChangeImage0;
                                

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //                case 10: //방패
        //                    if (saveData.gameData.equiptable_index4[i] != 0)
        //                    {
        //                        //예외사항
        //                        //아이템 타입은 장비류
        //                        bookInventory.slotItems[i].unitType = ItemType.Equiptable;
        //                        //분류는 방패
        //                        bookInventory.slotItems[i].itemPart = EquipParts.Shield;

        //                        //인벤토리 주소
        //                        bookInventory.slotItems[i].unitInventoryAddress = i;
        //                        //아이템 인덱스
        //                        bookInventory.slotItems[i].itemIndex = saveData.gameData.equiptable_index4[i];

        //                        //획득처리 [0] = 장비
        //                        bookInventory.slotItems[i].isGot[0] = true;
        //                        //후 체크
        //                        bookInventory.slotItems[i].TypeCheck();

        //                        //아이템 정보를 가진다.
        //                        sync.MakeItemInform(0,                                          //강화 상태
        //                            ref saveData.gameData.equiptable_index4[i],                     //캐릭터1의 i인덱스 = 장비파츠
        //                            ref bookInventory.slotItems[i].itemStat.itemAttack,             //아이템의 공격력
        //                            ref bookInventory.slotItems[i].itemStat.itemAcurrancy,          //아이템의 명중률
        //                            ref bookInventory.slotItems[i].itemStat.itemCritical,           //아이템의 치명률
        //                            ref bookInventory.slotItems[i].itemStat.itemDefense,            //아이템의 방어력
        //                            ref bookInventory.slotItems[i].itemStat.itemEvationRate,        //아이템의 회피율
        //                            ref bookInventory.slotItems[i].itemStat.itemHp,                 //아이템의 체력
        //                            ref bookInventory.slotItems[i].itemStat.itemMp,                 //아이템의 마력
        //                            ref bookInventory.slotItems[i].itemStat.itemHpCure,             //아이템의 초당 체력 회복율
        //                            ref bookInventory.slotItems[i].itemStat.itemMpCure              //아이템의 초당 마력 회복율
        //                        );

        //                        bookInventory.slotItems[i].elseItem.sprite = sync.temp_ChangeImage0;
                                

        //                        //플레이어에게 일괄적으로 능력치만큼 더하기한다. 
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
        //            }//switch(장비 착용파츠별)

        //        }//For문 장비착용 인덱스 만큼
        //    }//플레이어 4
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
