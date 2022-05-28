using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Main;

//��������� ��ũ��Ʈ 1 : ��� ������ ������ �� ���� ��� ����ϱ� ���� ������ �� ( ��� // �Һ�, ��Ÿ )
//��������� ��ũ��Ʈ 2 : ��� ��ų ������ �� ���� ��� ����ϱ� ���� ������ �� (��������, ��������, ����, �����)
namespace Main
{
    //1. XML (���� : �⺻���ɷ�ġ, ����) +(��ü texture ����) = ��ũ��Ʈ Item : ��ü ������ ��Ƴ��� ��������� ��ũ��Ʈ.
    //(���� = �ݶ��̴� + �⺻���ɷ�ġ),(�Ƿ�: ���� ���� �ѹ���),(��: ��� �Ƹ� ���� ����),(�Ǽ��縮: ��Ʈ ũ�ν� ���� ����)

    //2. DB: �÷��̾� ����, �ɷ�ġ, ����, ��� ��� = ��ũ��Ʈ PlayerData : ���ӽ��� �߿�(��  A-> B-> A �̵���, �����Ȳ �ӽ� ����)
    //(�̱���)

    //3. ���� ���� ��ư�� ������, �� ��ȯ�ϴ� ������, �ð����� �ڵ����� DB => (XML: bool�ڷ��� ��ȿ��)//JSON ��� ������ ����
    //����Ʈ ���࿩��, ĳ���� ����, ����ġ, �ɷ�ġ,
    //����.https://yoonstone-games.tistory.com/43

    //a. EnumPlayer_Infomation ��ũ��Ʈ : ������ ���� �����ϴ� SpriteRenderer�� ������ �� �ֵ��� ����
    //a. BookEquipment ��ũ��Ʈ���� : Item�� ��� EnumPlayer_Information�� �����ϰڴ� + UI�� �����۵� �����ش�.
    //a. Invantory ��ũ��Ʈ�� ����  SendMassage(�����ߴٴ� �Լ�) ���, �������� �ɷ�ġ

    //��� �������� �迭�� �����صд�


    //������ parts�� ���ϱ� ����
    public enum EquipParts //2/25�� �߰� (��� ���� ���� ����)
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

    //���� (��) �������ʵ�
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

    //���� �ݶ��̴� ����
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
        //������ �ε���
        public Dictionary<string, Dictionary<ItemField, string>> itemChart_Equiptable_Index = new Dictionary<string, Dictionary<ItemField, string>>();
        //������ �̸�
        public Dictionary<string, Dictionary<ItemField, string>> itemChart_Equiptable_DataName = new Dictionary<string, Dictionary<ItemField, string>>();
        //���� �ݶ��̴�
        public Dictionary<string, Dictionary<WeaponColiderSetting, string>> itemChart_WeaponsColider_DataName = new Dictionary<string, Dictionary<WeaponColiderSetting, string>>();


        //�� : 3sprite (�ٵ� ���� ������), ����: 2sprite (�޹� ������) �̿�: 1sprite

        void Awake()
        {
            //.https://www.youtube.com/watch?v=QYBYvfAIcnI
            //������ �� �ִ� �����۵�
            List<Dictionary<string, object>> data = CSVReader.Read(("Player_Equiptable_re"));
            List<Dictionary<string, object>> dataColider = CSVReader.Read("Player_Weapon"); ; //���⺰ �ݶ��̴� : ������ �����ϱ� ���� 
            //equiptable_Data = CSVReader.Read(("Player_Equiptable"));

            //����Ʈ ����, ��ųʸ��� ��Ƽ� ����ϱ� ����
            for (int i = 0; i < data.Count ; i++)// 0 ~ 130
            {
                //Csv �ڵ� ���� (��)
                Dictionary<string, object> rawData = data[i];
                
                //�ε��� Ű ���� 
                //string a = rawData["Index"].ToString();

                //���� �� (��)
                Dictionary<ItemField, string> line = new Dictionary<ItemField, string>();

                for (int j = 0; j < rawData.Count; j++)// 0 ~ 14
                {
                    //key���� �ӽ� ���� 
                    ItemField key = (ItemField)j;

                    //string ���·� ��ȯ
                    string rawDataKey = key.ToString();

                    //����׷� �� �������� Ȯ��
                    Debug.Log(rawDataKey);

                    //ItemField�� Key string�� Key
                    line.Add(key, rawData[rawDataKey].ToString());
                }
                
                //���� �߰�
                itemChart_Equiptable_Index.Add(rawData["Index"].ToString(), line);
                itemChart_Equiptable_DataName.Add(rawData["DataName"].ToString(), line);
            }

            //�ݶ��̴� ��ųʸ� �����
            for (int i = 0; i < dataColider.Count; i++) //��ü
            {
                //Csv �ڵ� ���� (��)
                Dictionary<string, object> rawDataCol = dataColider[i];

                //Csv �ڵ� (��)
                Dictionary<WeaponColiderSetting, string> line = new Dictionary<WeaponColiderSetting, string>();

                for (int j = 0; j < rawDataCol.Count; j++) // ���� �� ��ŭ
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