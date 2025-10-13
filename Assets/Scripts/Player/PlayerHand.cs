using TMPro;
using UnityEngine;

namespace CookCo_opGame
{
    public class PlayerHand : MonoBehaviour
    {
        [SerializeField] private GameObject _hand;
        [SerializeField] private bool _canPickUp = false;
        [SerializeField] private GameObject _itemInHand;
        [SerializeField] private ItemBase _itemManager;
        [SerializeField] GameObject _frontTable;
        [SerializeField] TableBase _curTableManager;
        [SerializeField] AudioClip _handEffectSound;

        private PlayerManager _playerManager;
        private Collider _pickUpCollider;
        private Rigidbody _itemRigidbody;
        private AudioSource _audioSource;
        private bool _isHandFree = true;
        private float _throwForce = 15f;

        public bool CanPickUp { get { return _canPickUp; } }
        public bool IsHandFree { get { return _isHandFree; } set { _isHandFree = value; } }

        public ItemBase ItemManager { get { return _itemManager; } set { _itemManager = value; } }

        public GameObject FrontTable { get { return _frontTable; } set { _frontTable = value; } }
        public TableBase CurTableManager { get { return _curTableManager; } set { _curTableManager = value; } }


        void Start()
        {
            _pickUpCollider = GetComponent<Collider>();
            _playerManager = GetComponentInParent<PlayerManager>();
            _audioSource = GetComponent<AudioSource>();

        }
        void OnTriggerEnter(Collider other)
        {
            //Debug.Log(other.name);
            if ((other.tag == "Food" || other.tag == "Tool") && IsHandFree)
            {
                _itemManager = other.gameObject.GetComponent<ItemBase>();
            }
            if (other.tag == "Mouse")
            {
                MouseMove mm = other.gameObject.GetComponent<MouseMove>();
                if (mm.CurrentItem != null)
                {
                    _itemManager = mm.CurrentItem;
                    mm.HasItem = false;
                    mm.CurrentItem = null;
                }
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
                    _audioSource.PlayOneShot(_handEffectSound);
                    _pickUpCollider.enabled = false;
                    _itemInHand = _itemManager.gameObject;
                    _itemRigidbody = _itemInHand.GetComponent<Rigidbody>();
                    _itemManager.OnTable = false;
                    _itemManager.IsGrabed = true;
                    _itemManager.PickedUp(_hand);
                    _isHandFree = false;
                    _playerManager.StateMachine.ChangeState(_playerManager.StateMachine.IdleState);
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
                        SubmitTable submitTable = CurTableManager.gameObject.GetComponent<SubmitTable>();
                        if (submitTable != null) //제출테이블
                        {
                            if (_itemManager.CurrentState == ItemState.Plate)
                            {
                                ToolBase plateToolManager = _itemManager.gameObject.GetComponent<ToolBase>();
                                if (plateToolManager.Ingredients.Count > 0)
                                {
                                    _itemManager.PutDown();
                                    _itemManager.PickedUp(FrontTable);
                                    submitTable.ChangeState(_itemManager.gameObject);

                                    _itemInHand = null;
                                    _isHandFree = true;

                                    _pickUpCollider.enabled = true;
                                }
                            }
                            return;
                            
                        }
                        ToolBase toolManager = CurTableManager.CurrentItem.GetComponent<ToolBase>();
                        if (toolManager != null)
                        {
                            if (toolManager.CheckToolState(_itemInHand))
                            {
                                toolManager.AddIngredient(_itemInHand);
                                _audioSource.PlayOneShot(_handEffectSound);
                            }
                            else
                                return;
                        }
                        else
                            return;
                    }
                    else
                    {
                        _audioSource.PlayOneShot(_handEffectSound);
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
                    if (CurTableManager.Purpose == TablePurpose.Cut) //자르기
                    {
                        CutTable cutTable = CurTableManager.gameObject.GetComponent<CutTable>();
                        if (cutTable != null)
                            cutTable.PlayerManager = _playerManager;
                        _playerManager.PlayerController.IsCooking = true;
                        _playerManager.StateMachine.ChangeState(_playerManager.StateMachine.CutState);

                    }
                    if (CurTableManager.Purpose == TablePurpose.Wash) //설거지
                    {
                        WaterTable waterTable = CurTableManager.gameObject.GetComponent<WaterTable>();
                        if (waterTable != null)
                            waterTable.PlayerManager = _playerManager;
                        _playerManager.PlayerController.IsCooking = true;
                        _playerManager.StateMachine.ChangeState(_playerManager.StateMachine.WashState);

                    }
                    

                }
            }
        }
        
    }
}
