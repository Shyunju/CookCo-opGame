using UnityEngine;
using System.Linq;
namespace CookCo_opGame
{
    public class GrillTool : ToolManager
    {

        private float _grillDuration = 7f;
        void Start()
        {
            _ingredientsMaxCount = 1;
        }
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
                EmptyTool();
                return false;
            }
            return false;
        }

        public override void StartCooking()
        {
            if (Ingredients.Count > 0 && CurrentTable.Purpose == TableManager.TablePurpose.Fire)
            {
                Duration = _grillDuration;
                IsCooking = true;
            }
        }
    }
}
