using System.Collections.Generic;
namespace CookCo_opGame
{
    [System.Serializable]
    public class PlayerData
    {
        //이름, 지갑, 누적금, 테이블 구매 현황
        public string Name;
        public int Wallet;
        public int Aggregate;
        public bool[] IsTablesBought = new bool[12];  
        public List<int> HasRecipes = new List<int>();
        public int year;
        public int month;
        public int day;
        public int CurLanguage;

    }
    [System.Serializable]
    public class PlayerDataList
    {
        public List<PlayerData> PlayerDatas = new List<PlayerData>();
    }
}
