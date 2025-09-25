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

        private void Awake()
        {
            base.Awake();
            Path = Application.persistentDataPath + "/save";	// 경로 지정
        }

        public void SaveData()
        {
            string data = JsonUtility.ToJson(_nowPlayer);
            File.WriteAllText(Path + NowSlot.ToString(), data);
        }

        public void LoadData()
        {
            string data = File.ReadAllText(Path + NowSlot.ToString());
            _nowPlayer = JsonUtility.FromJson<PlayerData>(data);
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
