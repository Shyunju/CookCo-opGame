using System;
using System.Linq;
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
        [SerializeField] float _ingredientsUIOffesetY;
        [SerializeField] Image[] _ingredients = new Image[4];
        public int IngredientsUIMaxCount { get; set; }
        Sprite spr;


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
        public void AddIngredientIcon(Sprite icon, int index)
        {

            _ingredients[index].sprite = icon;
            _ingredients[index].color = Color.white;
        }
        public void ResetIngredientIcon()  
        {
            for (int i = 0; i < IngredientsUIMaxCount; i++)
            {
                _ingredients[i].sprite = GameManager.Instance.ItemDataList.Find((item) => item.ItemID == -1).IconSprite;
            }
        }
    }
}
