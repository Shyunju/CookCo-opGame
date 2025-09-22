using System.Collections.Generic;
using UnityEngine;

namespace CookCo_opGame
{
    public abstract class ToolManager : ItemManager        
    {

        [SerializeField] GameObject _ingredientsTemp;
        //재료에 대한 정보(오브젝트)
        [SerializeField] protected List<FoodManager> _ingredients;
        [SerializeField] protected int _ingredientsMaxCount;
        [SerializeField] GameObject _warningUI;
        private IngredientUIController _ingredientsUIController;
        public GameObject WarningUI { get { return _warningUI; } set { _warningUI = value; } }
        public GameObject IngredientsTemp { get { return _ingredientsTemp; } set { _ingredientsTemp = value; } }
        public IngredientUIController IngredientUIController { get { return _ingredientsUIController; } set { _ingredientsUIController = value; } }
        public List<FoodManager> Ingredients { get { return _ingredients; } set { _ingredients = value; } }
        public int IngredientsMaxCount { get { return _ingredientsMaxCount; } }
        // public enum ToolPurpose
        // {
        //     Grill,
        //     Boil,
        //     Dish
        // }
        [SerializeField] ToolPurpose _thisToolPurpose;
        public ToolPurpose ThisToolPurpose { get { return _thisToolPurpose; } }

        void Start()
        {
            _ingredients = new List<FoodManager>();
            _ingredientsUIController = GetComponentInChildren<IngredientUIController>();
            SetIngredintsLength();
        }
        public void SetIngredintsLength()
        {
            IngredientUIController.IngredientsUIMaxCount = IngredientsMaxCount;
        }
        public void AddIngredient(GameObject food)
        {
            if (_ingredientsUIController == null)
                _ingredientsUIController = GetComponentInChildren<IngredientUIController>();

            FoodManager fm = food.GetComponent<FoodManager>();
            Ingredients.Add(fm);
            CurrentState = ItemState.None;
            food.transform.SetParent(_ingredientsTemp.transform, true);
            if (fm != null)
            {
                _ingredientsUIController.AddIngredientIcon(fm.Icon, _ingredients.Count - 1);
            }
            StartCooking();
        }
        public abstract bool CheckToolState(GameObject itemInHand);  //재료추가 가능 상태인지 확인
        public abstract void StartCooking();  //음식이 도구에 담겼을때 실행할 함수
        public void EmptyTool()
        {
            Transform[] children = _ingredientsTemp.GetComponentsInChildren<Transform>(true);
            for (int i = 1; i < children.Length; i++) // i=0은 부모 자신
            {
                Destroy(children[i].gameObject);
            }
            Ingredients.Clear();
            IngredientUIController.ResetIngredientIcon();
            CurrentState = ItemState.None;
            Elapsed = 0f;
        }
        
        

    }
}
