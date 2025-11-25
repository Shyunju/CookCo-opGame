using UnityEngine;
using System.IO;
namespace CookCo_opGame
{
    public class DataManager : Singleton<DataManager>
    {
        PlayerData _nowPlayer = new PlayerData(); // 플레이어 데이터 생성

        public string Path { get; set; } // 경로
        public int NowSlot { get; set; } // 현재 슬롯번호

        public PlayerData NowPlayer { get { return _nowPlayer; } set { _nowPlayer = value; } }

        protected override void Awake()
        {
            base.Awake();
            Path = Application.persistentDataPath + "/save";	// 경로 지정
        }

        public void SaveData()
        {
            _nowPlayer.year = System.DateTime.Now.Year;
            _nowPlayer.month = System.DateTime.Now.Month;
            _nowPlayer.day = System.DateTime.Now.Day;
            string data = JsonUtility.ToJson(_nowPlayer);
            string encryptedData = EncryptionUtility.Encrypt(data);
            File.WriteAllText(Path + NowSlot.ToString(), encryptedData);
            //Debug.Log(Path);
        }

        public void LoadData()
        {
            string encryptedData = File.ReadAllText(Path + NowSlot.ToString());
            string plainJson = EncryptionUtility.Decrypt(encryptedData);
            if (plainJson == encryptedData)
            {
                _nowPlayer = JsonUtility.FromJson<PlayerData>(plainJson);
                SaveData();
            }
            else
            {
                _nowPlayer = JsonUtility.FromJson<PlayerData>(plainJson);
            }
        }

        public void DataClear()
        {
            NowSlot = -1;
            _nowPlayer = new PlayerData();
        }
        
        public void ExitGame()
        {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit(); // 어플리케이션 종료
    #endif
        }
    }
}
