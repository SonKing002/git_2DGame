using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;// ����Ƽ ���� ���� ���� ���� (�����) : ���� ������ Ȯ���ϰ�, ���������� �Ǵ��Ͽ� �ҷ��� �� �ֵ��� �Ѵ�.
using Main;

namespace Main
{
    //����.https://yoonstone-games.tistory.com/43#google_vignette
    //data�� �����ϰ� �ҷ������� ��Ʈ�ѷ� ��ũ��Ʈ
    public class DataController : MonoBehaviour
    {
        //�̱������� ���ӿ�����Ʈ ����
        static GameObject _container;
        static GameObject Container
        {
            get
            {
                return _container;
            }
        }

        //�ν��Ͻ�ȭ
        static DataController _instance;
        static DataController Instance
        {
            get
            {
                //�ν��Ͻ��� �������� ������
                if (!_instance)
                {
                    //���ο� ���� ������Ʈ�� ������ְ�
                    _container = new GameObject();
                    //�̸��� �����ְ�
                    _container.name = "DataController";
                    //�ν��Ͻ��� DataController �ڷ����� �־��ش�
                    _instance = _container.AddComponent(typeof(DataController)) as DataController;
                    //�ı����� �ʵ��� �Ѵ�
                    DontDestroyOnLoad(_container);
                }
                //�ν��Ͻ� ����
                return _instance;
            }
        }

        //���� ������ �����̸� ����
        public string GameDataFileName = "StarfishData.json";

        //"���ϴ� �̸� (����).json"
        public GameData _gameData;
        public GameData gameData
        {
            get
            {
                if (_gameData == null)
                {
                    LoadGameData();
                    SaveGameData();
                }
                return _gameData;
            }
        }

        void Start()
        {
            LoadGameData();
            SaveGameData();
        }

        //���� �ҷ�����
        public void LoadGameData()
        {
            string filePath = Application.persistentDataPath + GameDataFileName;

            //����� ������ �ִٸ�
            if (File.Exists(filePath))
            {
                print("�ҷ����� ����");
                string FromJsonData = File.ReadAllText(filePath);
                _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
            }
            //���� ����� ������ ���ٸ�
            else
            {
                print("���ο� ���� ����");
                _gameData = new GameData();
            }
        }

        //���� �����ϱ�
        public void SaveGameData()
        {
            string ToJsonData = JsonUtility.ToJson(gameData);
            string filePath = Application.persistentDataPath + GameDataFileName;

            //���� �����
            File.WriteAllText(filePath, ToJsonData);

            print("����Ϸ�");

        }

        public void DataDelete()
        {
            string filePath = Application.persistentDataPath + GameDataFileName;
            
            File.Delete(filePath);
            print("���� �Ϸ�");
        }

        private void OnApplicationQuit()
        {
            SaveGameData();
        }
    }//class
}//namespace