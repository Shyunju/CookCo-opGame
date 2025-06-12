using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace CookCo_opGame
{
    public class IngredientUIController : MonoBehaviour
    {
        RectTransform _gridLayOut;
        [SerializeField] GameObject _UIObject;
        Transform _itemTransform;
        [SerializeField] float _ingredientsUIOffesetY = 55f;
        [SerializeField] Image[] _ingredients = new Image[4];
        

        void Start()
        {
            _itemTransform = gameObject.transform.parent;
            _gridLayOut = _UIObject.GetComponent<RectTransform>();
        }
        void Update()
        {
            if (_gridLayOut == null)
            {
                _gridLayOut = _UIObject.GetComponent<RectTransform>();
            }
            Vector3 screenPos = Camera.main.WorldToScreenPoint(_itemTransform.position);
            _gridLayOut.position = new Vector3(screenPos.x, screenPos.y + _ingredientsUIOffesetY, screenPos.z);
        }
        public void AddIngredientIcon(GameObject icon, int index)
        {
            GameObject iconTemp = Instantiate(icon, this.transform);
            SpriteRenderer iconImage = iconTemp.GetComponent<SpriteRenderer>();

            // Debug.Log(_ingredients[index].sprite);
            // Debug.Log(iconImage.sprite);
            _ingredients[index].sprite = iconImage.sprite;
            _ingredients[index].color = Color.white;
        }
    }
}
