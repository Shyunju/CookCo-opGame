using System.Collections.Generic;
using UnityEngine;

namespace CookCo_opGame
{
    public abstract class ToolManager : ItemManager
    {
        //뭐가 들어있는지 재료 유아이
        [SerializeField] GameObject _ingredientsUI;
        //재료에 대한 정보(오브젝트)
        [SerializeField] protected List<FoodManager> _ingredients;
        [SerializeField] protected int _ingredientsMaxCount;

    
        void Start()
        {
            _ingredients = new List<FoodManager>();
        }
        public abstract void CookIngredients();
        public void AddIngredient(FoodManager food)
        {
            if (_ingredients.Count < _ingredientsMaxCount)
            {
                _ingredients.Add(food);
            }
        }

    }
}
