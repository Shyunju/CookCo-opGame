using TMPro;
using UnityEngine;

namespace CookCo_opGame
{
    public class PlayerHand : MonoBehaviour
    {
        [SerializeField] private GameObject _hand;
        [SerializeField] private bool _canPickUp = false;
        [SerializeField] private GameObject _itemInHand;
        [SerializeField] private ItemManager _itemManager;
        [SerializeField] GameObject _frontTable;
        [SerializeField] TableManager _curTableManager;

        private PlayerManager _playerManager;
        private Collider _pickUpCollider;
        private Rigidbody _itemRigidbody;
        private bool _isHandFree = true;
        private float _throwForce = 15f;

        public bool CanPickUp { get { return _canPickUp; } }
        public bool IsHandFree { get { return _isHandFree; } set { _isHandFree = value; } }

        public ItemManager ItemManager { get { return _itemManager; } set { _itemManager = value; } }

        public GameObject FrontTable { get { return _frontTable; } set { _frontTable = value; } }
        public TableManager CurTableManager { get { return _curTableManager; } set { _curTableManager = value; } }


        void Start()
        {
            _pickUpCollider = GetComponent<Collider>();
            _playerManager = GetComponentInParent<PlayerManager>();

        }
        void OnTriggerEnter(Collider other)
        {
            if ((other.tag == "Food" || other.tag == "Tool") && IsHandFree)
            {
                _itemManager = other.gameObject.GetComponent<ItemManager>();
            }

        }
        void OnTriggerExit(Collider other)
        {
            if (_itemInHand == null)
            {
                _itemManager = null;
            }
        }


        //집기
        public void PickUpItem()
        {
            if (_isHandFree && _itemManager != null)
            {
                if (!_itemManager.IsGrabed)
                {
                    _playerManager.StateMachine.ChangeState(_playerManager.StateMachine.IdleState);
                    _pickUpCollider.enabled = false;
                    _itemInHand = _itemManager.gameObject;
                    _itemRigidbody = _itemInHand.GetComponent<Rigidbody>();
                    _itemManager.OnTable = false;
                    _itemManager.IsGrabed = true;
                    _itemManager.PickedUp(_hand);
                    _isHandFree = false;
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
                        ToolManager toolManager = CurTableManager.CurrentItem.GetComponent<ToolManager>();
                        if (toolManager != null)
                        {
                            if (toolManager.CheckToolState(_itemInHand))
                            {
                                toolManager.AddIngredient(_itemInHand);
                            }
                            else
                                return;
                        }
                        else
                            return;
                    }
                    else
                    {
                        _itemManager.PutDown();
                        _itemManager.PickedUp(FrontTable);
                    }
                }
                else
                {
                    _itemManager.PutDown();
                }

                _itemInHand = null;
                _isHandFree = true;

                _pickUpCollider.enabled = true;
            }
            return;

        }


        //던지기
        public void ThrowItem()
        {
            if (_itemInHand != null)
            {
                _itemRigidbody.constraints = RigidbodyConstraints.None;
                _itemRigidbody.AddForce(-transform.forward * _throwForce, ForceMode.VelocityChange);
                PutDownItem();
            }
            return;
        }

        public void CookAnimation()
        {
            if (FrontTable != null)  //손이 비었고 앞에 테이블이 있음
            {
                if (CurTableManager != null && CurTableManager.PerformPurpose())
                {
                    _pickUpCollider.enabled = false;
                    _pickUpCollider.enabled = true;
                    if (CurTableManager.Purpose == TableManager.TablePurpose.Cut) //자르기
                    {
                        CutTable cutTable = CurTableManager.gameObject.GetComponent<CutTable>();
                        if (cutTable != null)
                            cutTable.PlayerManager = _playerManager;
                        _playerManager.PlayerController.IsCooking = true;
                        _playerManager.StateMachine.ChangeState(_playerManager.StateMachine.CutState);

                    }
                    if (CurTableManager.Purpose == TableManager.TablePurpose.Wash) //설거지
                    {
                        Debug.Log("wash");
                        WaterTable waterTable = CurTableManager.gameObject.GetComponent<WaterTable>();
                        if (waterTable != null)
                            waterTable.PlayerManager = _playerManager;
                        _playerManager.PlayerController.IsCooking = true;
                        _playerManager.StateMachine.ChangeState(_playerManager.StateMachine.CutState);

                    }
                    

                }
            }
        }
        
    }
}
