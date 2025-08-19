using UnityEngine;

namespace CookCo_opGame
{
    public class TableSetter : MonoBehaviour
    {
        [SerializeField] GameObject[] _tables;
        void Start()
        {
            //LoadTables();
        }
        public void LoadTables()
        {
            for (int i = 0; i < GameManager.Instance.HasTable.Length; i++)
            {
                if (GameManager.Instance.HasTable[i])
                {
                    _tables[i].SetActive(true);
                }
            }
        }
    }
}
