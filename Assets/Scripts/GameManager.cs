using UnityEngine;

namespace CookCo_opGame
{
    public class GameManager : Singleton<GameManager>
    {
        private void Start()
        {
            ItemDataManager manager = new ItemDataManager();

            ItemData data = manager.GetItemByID(2);
            Debug.Log(data.ItemName);
        }
    }
}
