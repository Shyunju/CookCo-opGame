using System.Collections.Generic;
using UnityEngine;

namespace CookCo_opGame
{
    public abstract class ToolManager : ItemManager        //TODO 애니메이션이 없는 조리 시 유아이 진행 방식 설정 툴이 시작해줄거냐, 테이블이 시작해줄거냐
    {
        //뭐가 들어있는지 재료 유아이
        [SerializeField] GameObject _ingredientsUI;
        [SerializeField] GameObject _ingredientsTemp;
        //재료에 대한 정보(오브젝트)
        [SerializeField] protected List<GameObject> _ingredients;
        [SerializeField] protected int _ingredientsMaxCount;
        private IngredientUIController _ingredientUIController;
        public List<GameObject> Ingredients { get { return _ingredients; } }
        public int IngredientsMaxCount { get { return _ingredientsMaxCount; } }
        public enum ToolPurpose
        {
            Grill,
            Boil
            //,steam
        }
        [SerializeField] ToolPurpose _thisToolPurpose;
        public ToolPurpose ThisToolPurpose { get { return _thisToolPurpose; } }


        
        void Start()
        {
            _ingredients = new List<GameObject>();
            _ingredientUIController = GetComponentInChildren<IngredientUIController>();
        }
        void Update()
        {
        }
        public void AddIngredient(GameObject food)
        {
            if (_ingredientUIController == null)
                _ingredientUIController = GetComponentInChildren<IngredientUIController>();
            _ingredients.Add(food);
            food.transform.SetParent(_ingredientsTemp.transform, true);
            FoodManager fm = food.GetComponent<FoodManager>();
            if (fm != null)
            {
                //Debug.Log(_ingredientUIController);
                _ingredientUIController.AddIngredientIcon(fm.Icon, _ingredients.Count - 1);
            }
            StartCooking();
        }
        public abstract bool CheckToolState(GameObject itemInHand);  //재료추가 가능 상태인지 확인
        public abstract void StartCooking();

    }
}
