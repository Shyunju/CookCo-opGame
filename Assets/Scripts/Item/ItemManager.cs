using UnityEngine;

namespace CookCo_opGame
{
    public abstract class ItemManager : MonoBehaviour
    {
        Rigidbody _itemRigidbody;
        Collider _itemCollider;
        
        bool _isGrabed;
        public bool IsGrabed { get { return _isGrabed; } set { _isGrabed = value; } }
        bool _onTable = false;
        public bool OnTable { get { return _onTable; } set { _onTable = value;}}
        TableManager _currentTable;

        
        void Start()
        {
            _itemRigidbody = GetComponent<Rigidbody>();
        }
        public void PickedUp(GameObject parent)
        {
            if (parent.tag == "Hand")
            {
                IsGrabed = true;
                if (_currentTable != null)
                {
                    _currentTable.TopOfTableCollider.enabled = false;
                    _currentTable.TopOfTableCollider.enabled = true;
                    _currentTable.IsFull = false;
                }
            }
            if (parent.tag == "Table")
            {
                IsGrabed = false;
                _currentTable = parent.GetComponent<TableManager>();
            }
            this.transform.SetParent(parent.transform, true);
            this.transform.rotation = Quaternion.identity;
            this.transform.localPosition = Vector3.zero;
            // Vector3 originalScale = transform.lossyScale;
            // Vector3 parentScale = transform.parent != null ? transform.parent.lossyScale : Vector3.one;
            // transform.localScale = new Vector3(
            //     originalScale.x / parentScale.x,
            //     originalScale.y / parentScale.y,
            //     originalScale.z / parentScale.z
            // );      

            _itemRigidbody.useGravity = false;
            _itemRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            if (OnTable)
            {
                _itemRigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;
            }
        }
        public void PutDown()
        {
            IsGrabed = false;
            this.transform.SetParent(null);
            _itemRigidbody.useGravity = true;
            _itemRigidbody.constraints = RigidbodyConstraints.None;
            _itemRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
}
