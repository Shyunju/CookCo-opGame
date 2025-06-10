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
        Complete
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
        private Collider _itemCollider;
        private Rigidbody _itemRigidbody;
        float _targetStateBarScale;
        private float _duration;
        private float _elapsed = 0f;

        public bool IsGrabed { get { return _isGrabed; } set { _isGrabed = value; } }
        public bool OnTable { get { return _onTable; } set { _onTable = value; } }
        public bool IsCooking { get { return _isCooking; } set { _isCooking = value; } }
        public float Duration { get { return _duration; } set { _duration = value; } }
        public ItemState CurrentState { get { return _currentState; } set { _currentState = value; } }



        void Awake()
        {
            _itemCollider = GetComponent<Collider>();
            _itemRigidbody = GetComponent<Rigidbody>();
        }
        void Start()
        {
            _targetStateBarScale = _stateBar.rectTransform.rect.width;
        }
        void FixedUpdate()
        {
            if (_isCooking)
            {
                if (_elapsed >= _duration)
                {
                    _currentTable.ChaingeState(gameObject);
                    _stateUI.SetActive(false);
                    _elapsed = 0f;
                    _isCooking = false;
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
            if (parent.tag == "Table")
            {
                IsGrabed = false;
                _currentTable = parent.GetComponent<TableManager>();
                _currentTable.IsFull = true;
                _currentTable.CurrentItem = this.gameObject;
                OnTable = true;
            }
            IsCooking = false;
            this.transform.SetParent(parent.transform, true);
            this.transform.rotation = Quaternion.identity;
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
