using UnityEngine;
namespace CookCo_opGame
{
    public class ItemUIController : MonoBehaviour
    {
        [SerializeField] GameObject _UIObject;
        [SerializeField] float _cookingUIOffsetY;
        Transform _itemTransform;
        RectTransform _stateBar;
        
        
        void Start()
        {
            _itemTransform = gameObject.transform.parent;
            _stateBar = _UIObject.GetComponent<RectTransform>();
        }
        void Update()
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(_itemTransform.position);
            _stateBar.position = new Vector3(screenPos.x, screenPos.y + _cookingUIOffsetY, screenPos.z);
            
        }

        
    }
}
