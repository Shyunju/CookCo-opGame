using UnityEngine;

namespace CookCo_opGame
{
    public class TableManager : MonoBehaviour
    {
        [SerializeField] private bool _isFull = false;
        public bool IsFull { get { return _isFull; } set { _isFull = value; } }
        Collider _topOfTableCollider;
        public Collider TopOfTableCollider { get { return _topOfTableCollider; } set{ _topOfTableCollider = value; }}

        public enum TablePurpose
        {
            None,
            Box,
            Cut,
            Fire
        }

        void OnTriggerStay(Collider other)
        {
            if ((other.tag == "Food" || other.tag == "Tool") && !_isFull)
            {
                ItemManager itemManager = other.gameObject.GetComponent<ItemManager>();
                if (itemManager != null)
                {
                    if (!itemManager.IsGrabed)
                    {
                        itemManager.OnTable = true;
                        itemManager.PickedUp(this.gameObject);
                        _isFull = true;
                    }
                }
            }

        }
        void OnTriggerExit(Collider other)
        {
            _isFull = false;
        }
        
    }
}
