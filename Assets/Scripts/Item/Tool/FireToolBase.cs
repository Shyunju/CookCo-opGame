using UnityEngine;

namespace CookCo_opGame
{
    public class FireToolBase : ToolBase
    {
        MeshRenderer[] _renderers;
        MaterialPropertyBlock _propBlock;
        Color _originalColor;
        public override bool CheckToolState(GameObject itemInHand)
        {
            throw new System.NotImplementedException();
        }

        public override void StartCooking()
        {
            throw new System.NotImplementedException();
        }
        void Awake()
        {
            base.Awake();
            _propBlock = new MaterialPropertyBlock();
            _renderers = GetComponentsInChildren<MeshRenderer>();

            // 원래 색상 저장
            _renderers[0].GetPropertyBlock(_propBlock);
            // _Color가 없을 수도 있으므로, material의 color 사용
            _originalColor = _renderers[0].material.color;
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
    }
}
