using UnityEditor.ShaderGraph;
using UnityEngine;

namespace CookCo_opGame
{
    public abstract class TableManager : MonoBehaviour
    {
        [SerializeField] protected TablePurpose _purpose = TablePurpose.None;
        public TablePurpose purpose { get { return _purpose; } }
        [SerializeField] private bool _isFull = false;
        public bool IsFull { get { return _isFull; } set { _isFull = value; } }
        [SerializeField] private GameObject _currnetItem;
        public GameObject CurrentItem { get { return _currnetItem; } set { _currnetItem = value; } }

        public enum TablePurpose
        {
            None,
            Box,
            Cut,
            Fire
        }
        public abstract void PerformPurpose();
        
    }
}
