using TMPro;
using UnityEngine;

namespace CookCo_opGame
{
    public class PlayerHand : MonoBehaviour
    {
        private Collider _handCollider;
        private Collider _tempCollider;
        private bool _isHandFree = true;
        private bool _canPickUp = false;
        private Rigidbody _itemRigidBody;
        public bool CanPickUp { get { return _canPickUp; } }
        [SerializeField] private GameObject _itemInHand;
        void Start()
        {
            _handCollider = GetComponent<Collider>();
        }
        void OnTriggerEnter(Collider other)
        {
            _canPickUp = true;
            _tempCollider = other;

        }
        void OnTriggerExit(Collider other)
        {
            _canPickUp = false;
            _tempCollider = null;
        }
        public void PickUpItem()
        {
            if (_isHandFree && _canPickUp)
            {
                _tempCollider.transform.SetParent(this.transform);
                _tempCollider.transform.localPosition = Vector3.zero;
                _itemInHand = _tempCollider.gameObject;
                _itemRigidBody = _itemInHand.GetComponent<Rigidbody>();
                _itemRigidBody.useGravity = false;
                _tempCollider = null;
                _handCollider.enabled = false;
                _isHandFree = false;
                _canPickUp = false;
            }
            return;
        }
        public void PutDownItem()
        {
            if (_itemInHand != null)
            {
                _itemRigidBody.useGravity = true;
                _itemInHand.transform.SetParent(null);
                _itemInHand = null;
                _isHandFree = true;
                _handCollider.enabled = true;
            }
            return;
        }
    }
}
