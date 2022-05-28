using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;// 유니티 지원 폴더 안의 파일 (입출력) : 게임 파일을 확인하고, 존재유무를 판단하여 불러올 수 있도록 한다.
using Main;

namespace Main
{
    //참고.https://yoonstone-games.tistory.com/43#google_vignette
    //data를 저장하고 불러오기할 컨트롤러 스크립트
    public class DataController : MonoBehaviour
    {
        //싱글톤으로 게임오브젝트 선언
        static GameObject _container;
        static GameObject Container
        {
            get
            {
                return _container;
            }
        }

        //인스턴스화
        static DataController _instance;
        static DataController Instance
        {
            get
            {
                //인스턴스가 존재하지 않으면
                if (!_instance)
                {
                    //새로운 게임 오브젝트를 만들어주고
                    _container = new GameObject();
                    //이름을 지어주고
                    _container.name = "DataController";
                    //인스턴스에 DataController 자료형을 넣어준다
                    _instance = _container.AddComponent(typeof(DataController)) as DataController;
                    //파괴되지 않도록 한다
                    DontDestroyOnLoad(_container);
                }
                //인스턴스 리턴
                return _instance;
            }
        }

        //게임 데이터 파일이름 설정
        public string GameDataFileName = "StarfishData.json";

        //"원하는 이름 (영문).json"
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

        //게임 불러오기
        public void LoadGameData()
        {
            string filePath = Application.persistentDataPath + GameDataFileName;

            //저장된 게임이 있다면
            if (File.Exists(filePath))
            {
                print("불러오기 성공");
                string FromJsonData = File.ReadAllText(filePath);
                _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
            }
            //아직 저장된 게임이 없다면
            else
            {
                print("새로운 파일 생성");
                _gameData = new GameData();
            }
        }

        //게임 저장하기
        public void SaveGameData()
        {
            string ToJsonData = JsonUtility.ToJson(gameData);
            string filePath = Application.persistentDataPath + GameDataFileName;

            //파일 덮어쓰기
            File.WriteAllText(filePath, ToJsonData);

            print("저장완료");

        }

        public void DataDelete()
        {
            string filePath = Application.persistentDataPath + GameDataFileName;
            
            File.Delete(filePath);
            print("삭제 완료");
        }

        private void OnApplicationQuit()
        {
            SaveGameData();
        }
    }//class
}//namespace