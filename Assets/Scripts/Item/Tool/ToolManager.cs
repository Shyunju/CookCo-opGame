using System.Collections.Generic;
using UnityEngine;

namespace CookCo_opGame
{
    public abstract class ToolManager : ItemManager
    {
        //뭐가 들어있는지 재료 유아이
        [SerializeField] GameObject _ingredientsUI;
        [SerializeField] GameObject _ingredientsTemp;
        //재료에 대한 정보(오브젝트)
        [SerializeField] protected List<GameObject> _ingredients;
        [SerializeField] protected int _ingredientsMaxCount;
        public List<GameObject> Ingredients { get { return _ingredients; } }
        public int IngredientsMaxCount { get { return _ingredientsMaxCount; } }
        public enum ToolPurpose
        {
            Grill,
            Biol
            //,steam
        }
        [SerializeField] ToolPurpose _thisToolPurpose;
        public ToolPurpose ThisToolPurpose { get { return _thisToolPurpose; } }



        void Start()
        {
            _ingredients = new List<GameObject>();
        }
        public abstract void CookIngredients();
        public void AddIngredient(GameObject food)
        {
            _ingredients.Add(food);
            food.transform.SetParent(_ingredientsTemp.transform, true);
        }
        public abstract bool CheckToolState(GameObject itemInHand);

    }
}
