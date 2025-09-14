using UnityEngine;
using System.IO;
using System;
using Unity.VisualScripting;
namespace CookCo_opGame
{
    public class DataManager : Singleton<DataManager>
    {
        PlayerData _nowPlayer = new PlayerData();
        string _path;
        string _fileName = "save";


        void Awake()
        {
            base.Awake();
            _path = Application.persistentDataPath + "/";
        }

        public void SaveData() // 새로운 파일 생성과 기존 파일 덮어쓰기 구분하기
        {
            if (!File.Exists(_path + _fileName)) //같은 이름의 파일이 없음 = 생성 가능
            {
                string data = JsonUtility.ToJson(_nowPlayer);
                File.WriteAllText(_path + _fileName, data);   //어디에 무엇을                
            }
            else  //있다면 생성 불가 혹은 덮어쓰기
            {

            }

        }
        public void LoadData()
        {
            string data = File.ReadAllText(_path + _fileName);
            _nowPlayer = JsonUtility.FromJson<PlayerData>(data);
        }
        public void InputFileName(string fileName)
        {
            _fileName = fileName;
            SaveData();
        }
    }
}
