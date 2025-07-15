using UnityEngine;
using System.Linq;
namespace CookCo_opGame
{
    public class GrillTool : ToolManager
    {
        [SerializeField] GameObject _grillingFood;
        private float _grillDuration = 7f;
        public override bool CheckToolState(GameObject itemInHand)
        {
            FoodManager fm = itemInHand.GetComponent<FoodManager>();
            ToolManager tm = itemInHand.GetComponent<ToolManager>();
            if (fm != null && Ingredients.Count < _ingredientsMaxCount && fm.CurrentState == ItemState.Sliced)
            {
                return true;
            } else if(tm != null && tm.ThisToolPurpose == ToolPurpose.Dish){
                PlateTool pt = tm.GetComponent<PlateTool>();
                pt.InputFromTool(this.GetComponent<ToolManager>());
                _grillingFood.SetActive(false);
                EmptyTool();
                return false;
            }
            return false;
        }

        public override void StartCooking()
        {
            if (Ingredients.Count > 0 && CurrentTable.Purpose == TableManager.TablePurpose.Fire)
            {
                _grillingFood.SetActive(true);
                Duration = _grillDuration;
                IsCooking = true;
            }
        }
    }
}
