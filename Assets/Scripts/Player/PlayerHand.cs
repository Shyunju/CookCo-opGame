using TMPro;
using UnityEngine;

namespace CookCo_opGame
{
    public class PlayerHand : MonoBehaviour
    {
        private Collider _pickUpCollider;
        [SerializeField] private GameObject _hand;
        private bool _isHandFree = true;
        private bool _canPickUp = false;
        public bool CanPickUp { get { return _canPickUp; } }
        private Rigidbody _itemRigidbody;
        private float _throwForce = 15f;
    
        [SerializeField] private GameObject _itemInHand;
        [SerializeField] private ItemManager _itemManager;
        void Start()
        {
            _pickUpCollider = GetComponent<Collider>();
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Food" || other.tag == "Tool")
            {
                _canPickUp = true;
                _itemManager = other.gameObject.GetComponent<ItemManager>();
            }

        }
        void OnTriggerExit(Collider other)
        {
            _canPickUp = false;
            //_itemManager = null;
        }


        //집기
        public void PickUpItem()
        {
            if (_isHandFree && _canPickUp && _itemManager != null)
            {
                if (!_itemManager.IsGrabed)
                {
                    _itemInHand = _itemManager.gameObject;
                    _itemRigidbody = _itemInHand.GetComponent<Rigidbody>();
                    _itemManager.OnTable = false;
                    _itemManager.IsGrabed = true;
                    //_itemManager.PickedUp(this.gameObject);
                    _itemManager.PickedUp(_hand);
                    _isHandFree = false;
                    _canPickUp = false;                    
                }
            }
            return;
        }


        //놓기
        public void PutDownItem()
        {
            if (_itemInHand != null)
            {
                _itemManager.PutDown();
                _itemManager = null;
                _itemInHand = null;
                _isHandFree = true;
                _canPickUp = true;
                _pickUpCollider.enabled = false;
                _pickUpCollider.enabled = true;
            }
            return;
        }


        //던지기
        public void ThrowItem()
        {
            if (_itemInHand != null)
            {
                _itemRigidbody.AddForce(transform.forward * _throwForce, ForceMode.VelocityChange);
                _itemRigidbody.constraints = RigidbodyConstraints.None;
                PutDownItem();
            }
            return;
        }
    }
}
