using UnityEngine;

namespace CookCo_opGame
{
    public class GrillTool : ToolManager
    {

        void Start()
        {
            _ingredientsMaxCount = 1;
        }
        public override void CookIngredients()
        {
            //foreach (FoodManager food in _ingredients)
            {
                //food.CurrentState = ItemState.Grilled;
            }
        }
        public override bool CheckToolState(GameObject itemInHand)
        {
            FoodManager fm = itemInHand.GetComponent<FoodManager>();
            if (fm != null && Ingredients.Count < _ingredientsMaxCount && fm.CurrentState == ItemState.Sliced)
            {
                return true;
            }
            return false;
        }
    }
}
