using UnityEngine;

namespace CookCo_opGame
{
    public abstract class TableBase : MonoBehaviour
    {
        [SerializeField] protected TablePurpose _purpose = TablePurpose.None;
        [SerializeField] private bool _isFull = false;
        [SerializeField] private GameObject _currnetItem;

        private MeshRenderer _renderer;
        private MaterialPropertyBlock _propBlock;
        private Color _originalColor;
        
        public TablePurpose Purpose { get { return _purpose; } }
        public bool IsFull { get { return _isFull; } set { _isFull = value; } }
        public GameObject CurrentItem { get { return _currnetItem; } set { _currnetItem = value; } }
        public StealZone StealZone{ get; private set; }
        void Awake()
        {
            _propBlock = new MaterialPropertyBlock();

            _renderer = GetComponentInParent<MeshRenderer>();

            // 원래 색상 저장
            _renderer.GetPropertyBlock(_propBlock);
            // _Color가 없을 수도 있으므로, material의 color 사용
            _originalColor = _renderer.material.color;
            
            Transform parent = transform.parent;
            foreach (Transform sibling in parent)
            {
                if (sibling == transform) continue; // 자기 자신 제외

                StealZone comp = sibling.GetComponent<StealZone>();
                if (comp != null)
                {
                    StealZone = comp;
                }
            }
        }
        public virtual bool PerformPurpose()
        {
            return true;
        }
        public abstract void ChangeState(GameObject item);

        public void SetHighlight()
        {
            _renderer.GetPropertyBlock(_propBlock);
            _propBlock.SetColor("_BaseColor", Color.red);
            _renderer.SetPropertyBlock(_propBlock);
        }

        public void ResetColor()
        {
            if (CurrentItem != null && (_purpose == TablePurpose.Cut || _purpose == TablePurpose.Wash))
            {
                ItemBase im = CurrentItem.GetComponent<ItemBase>();
                im.IsCooking = false;
            }
            _renderer.GetPropertyBlock(_propBlock);
            _propBlock.SetColor("_BaseColor", _originalColor);
            _renderer.SetPropertyBlock(_propBlock);
        }
        
    }
}
