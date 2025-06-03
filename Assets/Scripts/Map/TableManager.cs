using UnityEditor.ShaderGraph;
using UnityEngine;

namespace CookCo_opGame
{
    public abstract class TableManager : MonoBehaviour
    {
        [SerializeField] protected TablePurpose _purpose = TablePurpose.None;
        public TablePurpose purpose { get { return _purpose; } }
        [SerializeField] private bool _isFull = false;
        public bool IsFull { get { return _isFull; } set { _isFull = value; } }
        [SerializeField] private GameObject _currnetItem;
        public GameObject CurrentItem { get { return _currnetItem; } set { _currnetItem = value; } }

        private MeshRenderer _renderer;
        private MaterialPropertyBlock _propBlock;
        private Color _originalColor;

        public enum TablePurpose
        {
            None,
            Box,
            Cut,
            Mix,
            Fire
        }
        void Awake()
        {
            _propBlock = new MaterialPropertyBlock();

            _renderer = GetComponentInParent<MeshRenderer>();

            // 원래 색상 저장
            _renderer.GetPropertyBlock(_propBlock);
            // _Color가 없을 수도 있으므로, material의 color 사용
            _originalColor = _renderer.material.color;
        }
        public abstract bool PerformPurpose();
        public abstract void ChaingeState(GameObject item);
        
        public void SetHighlight()
        {
            _renderer.GetPropertyBlock(_propBlock);
            _propBlock.SetColor("_BaseColor", Color.red);
            _renderer.SetPropertyBlock(_propBlock);
        }

        public void ResetColor()
        {
            if (CurrentItem != null)
            {
                ItemManager im = CurrentItem.GetComponent<ItemManager>();
                im.IsCooking = false;
            }
            _renderer.GetPropertyBlock(_propBlock);
            _propBlock.SetColor("_BaseColor", _originalColor);
            _renderer.SetPropertyBlock(_propBlock);
        }
        
    }
}
