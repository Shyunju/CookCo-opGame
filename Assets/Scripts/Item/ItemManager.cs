using UnityEngine;
using UnityEngine.UI;

namespace CookCo_opGame
{
    public enum ItemState
    {
        None,
        Sliced,
        Boiled,
        Grilled,
        Mixed,
        Plate,
        Used,
        Complete,
        Burn
    }
    public abstract class ItemManager : MonoBehaviour
    {
        [SerializeField] GameObject _stateUI;
        [SerializeField] Image _stateBar;
        [SerializeField] ItemState _currentState = ItemState.None;
        [SerializeField] TableManager _currentTable;
        [SerializeField] bool _isGrabed;
        [SerializeField] bool _onTable = false;
        [SerializeField] bool _isCooking = false;
        [SerializeField] int _itemID;
        private Collider _itemCollider;
        private Rigidbody _itemRigidbody;
        float _targetStateBarScale;
        private float _duration;
        private float _elapsed = 0f;

        public GameObject StateUI { get { return _stateUI; } set { _stateUI = value; } }
        public bool IsGrabed { get { return _isGrabed; } set { _isGrabed = value; } }
        public bool OnTable { get { return _onTable; } set { _onTable = value; } }
        public bool IsCooking { get { return _isCooking; } set { _isCooking = value; } }
        public float Duration { get { return _duration; } set { _duration = value; } }
        public TableManager CurrentTable { get { return _currentTable;} set { _currentTable = value; } }
        public ItemState CurrentState { get { return _currentState; } set { _currentState = value; } }
        public int ItemID { get { return _itemID;} set { _itemID = value; } }
        public bool StopNextStep { get; set; }



        void Awake()
        {
            _itemCollider = GetComponent<Collider>();
            _itemRigidbody = GetComponent<Rigidbody>();
            _targetStateBarScale = _stateBar.rectTransform.rect.width;
        }
        void FixedUpdate()
        {
            if (IsCooking)
            {
                if (_elapsed >= _duration)
                {
                    _currentTable.ChangeState(gameObject);
                    if(!StopNextStep)
                        ResetCookingState();
                }
                else
                {
                    _stateUI.SetActive(true);
                    _elapsed += Time.deltaTime;
                    float gab = Mathf.Clamp01(_elapsed / _duration);
                    float currentWidth = Mathf.Lerp(0, _targetStateBarScale, gab);

                    _stateBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentWidth);
                }

            }
        }
        public void ResetCookingState()
        {
            _stateUI.SetActive(false);
            IsCooking = false;
            _elapsed = 0f;
            //color change
        }
        public void PickedUp(GameObject parent)
        {
            if (parent.tag == "Hand")
            {
                IsGrabed = true;
                if (_currentTable != null)
                {
                    _currentTable.CurrentItem = null;
                    _currentTable.IsFull = false;
                    _currentTable = null;
                    OnTable = false;
                    _itemRigidbody.useGravity = false;
                    _itemCollider.isTrigger = true;
                }
            }
            IsCooking = false;
            if (parent.tag == "Table")
            {
                IsGrabed = false;
                _currentTable = parent.GetComponent<TableManager>();
                _currentTable.IsFull = true;
                _currentTable.CurrentItem = this.gameObject;
                OnTable = true;

                ToolManager tm = this.gameObject.GetComponent<ToolManager>();
                if (tm != null)
                {
                    //요리가 가능한지 확인하는 함수 호출 // 그 함수에서 가능하다면 이즈쿠킹으로 바꾸고 듀레이션 주기
                    tm.StartCooking();
                }
                if (_currentTable.Purpose == TableManager.TablePurpose.Trash) //버리기(리셋)
                {
                    TrashTable trashTable = _currentTable.gameObject.GetComponent<TrashTable>();
                    if (trashTable != null && _currentTable.CurrentItem != null)
                    {
                        if (trashTable.PerformPurpose())
                            trashTable.ChangeState(_currentTable.CurrentItem);
                    }
                }
                if (_currentTable.Purpose == TableManager.TablePurpose.Wash && CurrentState == ItemState.Used)
                {
                    WaterTable waterTable = _currentTable.gameObject.GetComponentInParent<WaterTable>();
                    if (waterTable != null)
                    {
                        waterTable.HasPlate = true;
                        waterTable.WaterInSink.SetActive(true);
                        Destroy(gameObject);
                    }
                }
            }
            this.transform.SetParent(parent.transform, true);
            this.transform.localRotation = Quaternion.identity;
            this.transform.localPosition = Vector3.zero;

            _itemRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            if (OnTable)
            {
                _itemRigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;
            }
        }
        public void PutDown()
        {
            IsGrabed = false;
            OnTable = false;
            this.transform.SetParent(null);
            _itemRigidbody.useGravity = true;
            _itemRigidbody.constraints = RigidbodyConstraints.None;
            _itemRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            _itemCollider.isTrigger = false;
        }



    }
}
