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
                    SoundManager.Instance.PlayCuteSound(_playerManager.PlayerInput.PlayerNumber);
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

        //놓기 (리팩토링된 버전)
        public void PutDownItem()
        {
            // [가드] 손에 든 아이템이 없으면 즉시 종료
            if (_itemInHand == null) return;

            // [가드] 앞에 테이블이 없으면, 아이템을 바닥에 내려놓고 종료
            if (FrontTable == null)
            {
                _itemManager.PutDown();
                ClearHand();
                return;
            }

            // [가드] 테이블이 비어있으면, 아이템을 테이블에 올려놓고 종료
            if (CurTableManager != null && !CurTableManager.IsFull)
            {
                _audioSource.PlayOneShot(_handEffectSound);
                _itemManager.PutDown();
                _itemManager.PickedUp(FrontTable);
                ClearHand();
                return;
            }

            // --- 테이블이 꽉 차 있는 경우의 로직 ---
            if (CurTableManager != null)
            {
                // 제출 테이블과 상호작용 시도
                if (TryInteractWithSubmitTable()) return;

                // 도구와 상호작용 시도
                if (TryInteractWithTool()) return;
            }
        }

        private void ClearHand()
        {
            _itemInHand = null;
            _itemManager = null; // 아이템 매니저도 초기화
            _isHandFree = true;
            _pickUpCollider.enabled = true;
        }

        private bool TryInteractWithSubmitTable()
        {
            if (!CurTableManager.TryGetComponent(out SubmitTable submitTable)) return false;

            if (_itemManager.CurrentState == ItemState.Plate && _itemManager.TryGetComponent(out ToolBase plateToolManager) && plateToolManager.Ingredients.Count > 0)
            {
                _itemManager.PutDown();
                _itemManager.PickedUp(FrontTable);
                submitTable.ChangeState(_itemManager.gameObject);
                ClearHand();
                return true;
            }
            return false;
        }

        private bool TryInteractWithTool()
        {
            if (CurTableManager.CurrentItem == null) return false;

            if (!CurTableManager.CurrentItem.TryGetComponent(out ToolBase toolOnTable)) return false;

            if (toolOnTable.CheckToolState(_itemInHand))
            {
                toolOnTable.AddIngredient(_itemInHand);
                _audioSource.PlayOneShot(_handEffectSound);
                ClearHand();
                return true;
            }
            return false;
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
