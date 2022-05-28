using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

//백과사전용 스크립트 1 : 모든 아이템 정보를 이 곳에 모아 사용하기 쉽게 정리할 것 ( 장비 // 소비, 기타 )
//백과사전용 스크립트 2 : 모든 스킬 정보를 이 곳에 모아 사용하기 쉽게 정리할 것 (마법공격, 물리공격, 버프, 디버프)
namespace Main
{
    //1. XML (공통 : 기본장비능력치, 설명) +(전체 texture 저장) = 스크립트 Item : 전체 아이템 모아놓은 백과사전용 스크립트.
    //(무기 = 콜라이더 + 기본장비능력치),(의류: 상의 하의 한벌옷),(방어구: 헬멧 아머 쉴드 망또),(악세사리: 벨트 크로스 반지 문장)

    //2. DB: 플레이어 소유, 능력치, 상태, 기술 등등 = 스크립트 PlayerData : 게임실행 중에(씬  A-> B-> A 이동시, 진행상황 임시 저장)
    //(싱글톤)

    //3. 게임 저장 버튼을 누르면, 씬 전환하는 순간에, 시간마다 자동저장 DB => (XML: bool자료형 비효율)//JSON 방식 저장을 공부
    //퀘스트 진행여부, 캐릭터 레벨, 경험치, 능력치,
    //참고.https://yoonstone-games.tistory.com/43

    //a. EnumPlayer_Infomation 스크립트 : 파츠별 현재 장착하는 SpriteRenderer로 연결할 수 있도록 세팅
    //a. BookEquipment 스크립트에서 : Item중 장비를 EnumPlayer_Information에 대입하겠다 + UI에 아이템도 보여준다.
    //a. Invantory 스크립트로 부터  SendMassage(장착했다는 함수) 사용, 아이템의 능력치

    //모든 아이템을 배열로 저장해둔다


    //엑셀의 parts랑 비교하기 위함
    public enum EquipParts //2/25일 추가 (장비별 착용 범위 수정)
    {
        None = -1,
        Cloth,
        PantsLeg,
        Helmet,
        Armor,
        Back,
        Belt,
        Cross,
        Necklace,
        Ring,
        Weapon,
        Shield
    }

    //엑셀 (행) 아이템필드
    public enum ItemField
    {
        Index,
        Parts,
        DataName,
        Attack,
        Critical,
        Accuracy,
        Defense,
        EvationRate,
        Hp,
        Mp,
        HpCure,
        MpCure,
        ItemName,
        Explaination,
        ResourcePath
    }

    //무기 콜라이더 조정
    public enum WeaponColiderSetting
    {
        Weapon,
        OffsetX,
        OffsetY,
        SizeX,
        SizeY
    }


    public class Item : MonoBehaviour
    {
        //아이템 인덱스
        public Dictionary<string, Dictionary<ItemField, string>> itemChart_Equiptable_Index = new Dictionary<string, Dictionary<ItemField, string>>();
        //아이템 이름
        public Dictionary<string, Dictionary<ItemField, string>> itemChart_Equiptable_DataName = new Dictionary<string, Dictionary<ItemField, string>>();
        //무기 콜라이더
        public Dictionary<string, Dictionary<WeaponColiderSetting, string>> itemChart_WeaponsColider_DataName = new Dictionary<string, Dictionary<WeaponColiderSetting, string>>();


        //옷 : 3sprite (바디 왼팔 오른팔), 바지: 2sprite (왼발 오른발) 이외: 1sprite

        void Awake()
        {
            //.https://www.youtube.com/watch?v=QYBYvfAIcnI
            //장착할 수 있는 아이템들
            List<Dictionary<string, object>> data = CSVReader.Read(("Player_Equiptable_re"));
            List<Dictionary<string, object>> dataColider = CSVReader.Read("Player_Weapon"); ; //무기별 콜라이더 : 사이즈 조정하기 위함 
            //equiptable_Data = CSVReader.Read(("Player_Equiptable"));

            //리스트 말고, 딕셔너리에 담아서 사용하기 위함
            for (int i = 0; i < data.Count ; i++)// 0 ~ 130
            {
                //Csv 코드 한줄 (행)
                Dictionary<string, object> rawData = data[i];
                
                //인덱스 키 저장 
                //string a = rawData["Index"].ToString();

                //만들 줄 (열)
                Dictionary<ItemField, string> line = new Dictionary<ItemField, string>();

                for (int j = 0; j < rawData.Count; j++)// 0 ~ 14
                {
                    //key변수 임시 변수 
                    ItemField key = (ItemField)j;

                    //string 형태로 변환
                    string rawDataKey = key.ToString();

                    //디버그로 잘 들어오는지 확인
                    Debug.Log(rawDataKey);

                    //ItemField형 Key string형 Key
                    line.Add(key, rawData[rawDataKey].ToString());
                }
                
                //누적 추가
                itemChart_Equiptable_Index.Add(rawData["Index"].ToString(), line);
                itemChart_Equiptable_DataName.Add(rawData["DataName"].ToString(), line);
            }

            //콜라이더 딕셔너리 만들기
            for (int i = 0; i < dataColider.Count; i++) //전체
            {
                //Csv 코드 한줄 (행)
                Dictionary<string, object> rawDataCol = dataColider[i];

                //Csv 코드 (열)
                Dictionary<WeaponColiderSetting, string> line = new Dictionary<WeaponColiderSetting, string>();

                for (int j = 0; j < rawDataCol.Count; j++) // 세로 줄 만큼
                {
                    WeaponColiderSetting key = (WeaponColiderSetting)j;
                    string rawDicKey = key.ToString();
                    Debug.Log(rawDicKey);
                    line.Add(key, rawDataCol[rawDicKey].ToString());
                }
                itemChart_WeaponsColider_DataName.Add(rawDataCol["Weapon"].ToString(), line);
            }
        }

    }//class

}//namespace