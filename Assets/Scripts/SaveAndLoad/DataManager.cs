using UnityEngine;

namespace CookCo_opGame
{
    public class DataManager : Singleton<DataManager>
    {
        PlayerData _playerData = new PlayerData();
        void Start()
        {
            _playerData.Name = "testName";
        }
    }
}
