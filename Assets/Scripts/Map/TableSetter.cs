using UnityEngine;

namespace CookCo_opGame
{
    public class TableSetter : MonoBehaviour
    {
        [SerializeField] GameObject[] _tables;
        void Start()
        {
            LoadTables();
        }
        public void LoadTables()
        {
            for (int i = 0; i < GameManager.Instance.ShopTables.Length; i++)
            {
                if (GameManager.Instance.ShopTables[i].isBought)
                {
                    _tables[i].SetActive(true);
                }
            }
        }
    }
}
