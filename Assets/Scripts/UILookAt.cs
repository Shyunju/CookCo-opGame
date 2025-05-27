using UnityEngine;

namespace CookCo_opGame
{
    public class UILookAt : MonoBehaviour
    {
        [SerializeField] GameObject UIObject;
        Transform itemTransform;
        RectTransform uiElement;
        [SerializeField] float offsetY;
        void Start()
        {
            itemTransform = gameObject.transform.parent;
            uiElement = UIObject.GetComponent<RectTransform>();
        }
        void Update() {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(itemTransform.position);
            uiElement.position = new Vector3(screenPos.x, screenPos.y +offsetY, screenPos.z);
        }
    }
}
