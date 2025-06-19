using System.Collections.Generic;
using UnityEngine;

namespace CookCo_opGame
{
    public abstract class ToolManager : ItemManager        //TODO 애니메이션이 없는 조리 시 유아이 진행 방식 설정 툴이 시작해줄거냐, 테이블이 시작해줄거냐
    {

        [SerializeField] GameObject _ingredientsTemp;
        //재료에 대한 정보(오브젝트)
        [SerializeField] protected List<GameObject> _ingredients;
        [SerializeField] protected int _ingredientsMaxCount;
        private IngredientUIController _ingredientsUIController;
        public IngredientUIController IngredientUIController { get { return _ingredientsUIController; } set { _ingredientsUIController = value; } }
        public List<GameObject> Ingredients { get { return _ingredients; } set { _ingredients = value;}}
        public int IngredientsMaxCount { get { return _ingredientsMaxCount; } }
        public enum ToolPurpose
        {
            Grill,
            Boil,
            Dish
            //,steam
        }
        [SerializeField] ToolPurpose _thisToolPurpose;
        public ToolPurpose ThisToolPurpose { get { return _thisToolPurpose; } }



        void Start()
        {
            _ingredients = new List<GameObject>();
            _ingredientsUIController = GetComponentInChildren<IngredientUIController>();
        }
        void Update()
        {
        }
        public void AddIngredient(GameObject food)
        {
            if (_ingredientsUIController == null)
                _ingredientsUIController = GetComponentInChildren<IngredientUIController>();
            _ingredients.Add(food);
            food.transform.SetParent(_ingredientsTemp.transform, true);
            FoodManager fm = food.GetComponent<FoodManager>();
            if (fm != null)
            {
                _ingredientsUIController.AddIngredientIcon(fm.Icon, _ingredients.Count - 1);
            }
            StartCooking();
        }
        public abstract bool CheckToolState(GameObject itemInHand);  //재료추가 가능 상태인지 확인
        public abstract void StartCooking();  //음식이 도구에 담겼을때 실행할 함수
        public virtual void ChangeFoodIcon()
        {
            foreach (var food in Ingredients)
            {
                FoodManager fm = food.GetComponent<FoodManager>();
                fm.ChangeMesh(fm.Index + 1);
            }
        }
        public void EmptyTool()
        {
            Ingredients.Clear();
            IngredientUIController.ResetIngredientIcon();
            CurrentState = ItemState.None;
        }

    }
}
