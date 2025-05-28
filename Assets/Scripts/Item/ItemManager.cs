using UnityEngine;
using UnityEngine.UI;

namespace CookCo_opGame
{
    public abstract class ItemManager : MonoBehaviour
    {
        Rigidbody _itemRigidbody;
        [SerializeField] GameObject _stateUI;
        [SerializeField] Image _stateBar;
        float _stateBarScale;
        
        [SerializeField] bool _isGrabed;
        public bool IsGrabed { get { return _isGrabed; } set { _isGrabed = value; } }
        [SerializeField] bool _onTable = false;
        public bool OnTable { get { return _onTable; } set { _onTable = value;}}
        [SerializeField] TableManager _currentTable;
        bool _isCooking = false;

        
        void Awake()
        {
            _itemRigidbody = GetComponent<Rigidbody>();
        }
        void Start()
        {
            _stateBarScale = _stateBar.rectTransform.rect.width;
        }
        void FixedUpdate()
        {
            if (_isCooking)
            {
                _stateUI.SetActive(true);
                _stateBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _stateBarScale * Time.deltaTime);

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
            this.transform.SetParent(parent.transform, true);
            this.transform.rotation = Quaternion.identity;
            this.transform.localPosition = Vector3.zero;

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
            OnTable = false;
            this.transform.SetParent(null);
            _itemRigidbody.useGravity = true;
            _itemRigidbody.constraints = RigidbodyConstraints.None;
            _itemRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
}
