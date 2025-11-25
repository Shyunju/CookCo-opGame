using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CookCo_opGame
{
    public abstract class ItemBase : MonoBehaviour
    {
        [SerializeField] GameObject _stateUI;
        [SerializeField] Image _stateBar;
        [SerializeField] ItemState _currentState = ItemState.None;
        [SerializeField] TableBase _currentTable;
        [SerializeField] bool _isGrabed;
        [SerializeField] bool _onTable = false;
        [SerializeField] bool _isCooking = false;
        [SerializeField] int _itemID;
        private Collider _itemCollider;
        private Rigidbody _itemRigidbody;
        private float _targetStateBarScale;
        private float _duration;
        private float _elapsed = 0f;

        private MeshRenderer[] _renderers;
        private MaterialPropertyBlock _propBlock;
        private Color _originalColor;

        public GameObject StateUI { get { return _stateUI; } set { _stateUI = value; } }
        public bool IsGrabed { get { return _isGrabed; } set { _isGrabed = value; } }
        public bool OnTable { get { return _onTable; } set { _onTable = value; } }
        public bool IsCooking { get { return _isCooking; } set { _isCooking = value; } }
        public float Duration { get { return _duration; } set { _duration = value; } }
        public float Elapsed { get { return _elapsed; } set { _elapsed = value; } }
        public TableBase CurrentTable { get { return _currentTable; } set { _currentTable = value; } }
        public ItemState CurrentState { get { return _currentState; } set { _currentState = value; } }
        public int ItemID { get { return _itemID; } set { _itemID = value; } }
        public bool StopNextStep { get; set; } //설거지 새그릇 리스폰 가능여부 true == 다음단계를 멈춰야함



        void Awake()
        {
            _itemCollider = GetComponent<Collider>();
            _itemRigidbody = GetComponent<Rigidbody>();
            _targetStateBarScale = _stateBar.rectTransform.rect.width;

            _propBlock = new MaterialPropertyBlock();

            _renderers = GetComponentsInChildren<MeshRenderer>();

            // 원래 색상 저장
            _renderers[0].GetPropertyBlock(_propBlock);
            // _Color가 없을 수도 있으므로, material의 color 사용
            _originalColor = _renderers[0].material.color;
        }
        void FixedUpdate()
        {
            if (IsCooking)
            {
                if (_elapsed >= _duration)
                {
                    _currentTable.ChangeState(gameObject);  //재료 아이디 값 바꿔줌
                    if (!StopNextStep)
                        ResetCookingState();
                    StartCoroutine(WarningCo());
                    IsCooking = false;
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
            CurrentState = ItemState.None;
            _stateUI.SetActive(false);
            IsCooking = false;
        }
        public void ChangeElapsed(float mount)
        {
            _elapsed = Math.Clamp(_elapsed - mount, 0, Duration);
        }
        public virtual void PickedUp(GameObject parent)
        {
            if (parent.tag == "Hand" || parent.tag == "MouseHead")
            {
                if (parent.tag == "Hand")
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
                _currentTable = parent.GetComponent<TableBase>();
                _currentTable.IsFull = true;
                _currentTable.CurrentItem = this.gameObject;
                OnTable = true;

                ToolBase toolBase = this.gameObject.GetComponent<ToolBase>();
                if (toolBase != null)
                {
                    //요리가 가능한지 확인하는 함수 호출 // 그 함수에서 가능하다면 이즈쿠킹으로 바꾸고 듀레이션 주기
                    toolBase.StartCooking();
                }
                if (_currentTable.Purpose == TablePurpose.Trash) //버리기(리셋)
                {
                    TrashTable trashTable = _currentTable.gameObject.GetComponent<TrashTable>();
                    if (trashTable != null && _currentTable.CurrentItem != null)
                    {
                        if (trashTable.PerformPurpose())
                            trashTable.ChangeState(_currentTable.CurrentItem);
                    }
                }
                if(toolBase == null && _currentTable.Purpose != TablePurpose.Trash) //쥐 호출
                {
                    int percentage = UnityEngine.Random.Range(1, 100);
                    if (percentage % 2 == 0)
                        CookingPlayManager.Instance.GiveTargetToMouse(_currentTable.StealZone.gameObject.transform);
                }
                if (_currentTable.Purpose == TablePurpose.Wash && CurrentState == ItemState.Used)
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
            _itemRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            this.transform.localRotation = Quaternion.identity;
            this.transform.localPosition = Vector3.zero;
            _itemRigidbody.inertiaTensorRotation = Quaternion.identity;
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

        public void BurnState()
        {
            foreach (var item in _renderers)
            {
                item.GetPropertyBlock(_propBlock);
                _propBlock.SetColor("_BaseColor", Color.black);
                item.SetPropertyBlock(_propBlock);
            }
        }

        public void ResetColor()
        {
            foreach (var item in _renderers)
            {
                item.GetPropertyBlock(_propBlock);
                _propBlock.SetColor("_BaseColor", _originalColor);
                item.SetPropertyBlock(_propBlock);
                CurrentState = ItemState.None;
            }
        }

        public virtual IEnumerator WarningCo()
        {
            yield return new WaitForSeconds(5f);
        }
    }
}
