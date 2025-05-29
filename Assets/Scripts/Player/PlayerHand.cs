using TMPro;
using UnityEngine;

namespace CookCo_opGame
{
    public class PlayerHand : MonoBehaviour
    {
        private Collider _pickUpCollider;
        [SerializeField] private GameObject _hand;
        private bool _isHandFree = true;
        public bool IsHandFree {get { return _isHandFree; } set{ _isHandFree = value; } }
        [SerializeField] private bool _canPickUp = false;
        public bool CanPickUp { get { return _canPickUp; } }
        private Rigidbody _itemRigidbody;
        private float _throwForce = 15f;

        [SerializeField] private GameObject _itemInHand;
        [SerializeField] private ItemManager _itemManager;

        [SerializeField] GameObject _frontTable;
        public GameObject FrontTable { get { return _frontTable; } set { _frontTable = value; } }
        [SerializeField] TableManager _curTableManager;
        public TableManager CurTableManager { get { return _curTableManager; } set { _curTableManager = value; } }


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
            if (_itemInHand == null)
            {
                _itemManager = null;                
            }
        }


        //집기
        public void PickUpItem()
        {
            if (_isHandFree && _canPickUp && _itemManager != null)
            {
                if (!_itemManager.IsGrabed)
                {
                    _pickUpCollider.enabled = false;
                    _itemInHand = _itemManager.gameObject;
                    _itemRigidbody = _itemInHand.GetComponent<Rigidbody>();
                    _itemManager.OnTable = false;
                    _itemManager.IsGrabed = true;
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
                if (FrontTable != null)
                {
                    if (CurTableManager != null && CurTableManager.IsFull)
                    {
                        return;
                    }
                    _itemManager.PutDown();
                    _itemManager.PickedUp(FrontTable);
                }
                else
                {
                    _itemManager.PutDown();
                }
                
                _curTableManager = null;
                _itemManager = null;
                _itemInHand = null;
                _isHandFree = true;
                _canPickUp = true;
                
                
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

        public void CookAnimation()
        {
            if (FrontTable != null)  //손이 비었고 앞에 테이블이 있음
            {
                if (CurTableManager.PerformPurpose())
                {
                    switch (CurTableManager.purpose)
                        {
                            case TableManager.TablePurpose.None:
                                break;
                            case TableManager.TablePurpose.Box:
                                break;
                            case TableManager.TablePurpose.Cut:
                                break;
                            case TableManager.TablePurpose.Fire:
                                break;
                        }                    
                }
            }
        }

        
    }
}
