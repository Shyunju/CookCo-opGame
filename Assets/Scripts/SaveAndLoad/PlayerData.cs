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
        public SellingTable[] SellingTable;

    }
    [System.Serializable]
    public class PlayerDataList
    {
        public List<PlayerData> Items = new List<PlayerData>();
    }
}
