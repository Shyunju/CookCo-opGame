using UnityEngine;

namespace CookCo_opGame
{
    public class PlayerData : MonoBehaviour
    {
        //이름, 지갑, 누적금, 테이블 구매 현황
        public string Name;
        public int Wallet;
        public int Aggregate;
        public SellingTable[] SellingTable;
    }
}
