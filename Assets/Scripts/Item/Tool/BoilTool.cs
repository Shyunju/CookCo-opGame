using UnityEditor;
using UnityEngine;

namespace CookCo_opGame
{
    public class BoilTool : ToolManager
    {
        private float _boilDuration = 10f;
        private float _plusDuration = 2f;
        public override bool CheckToolState(GameObject itemInHand)
        {
            FoodManager fm = itemInHand.GetComponent<FoodManager>();
            ToolManager tm = itemInHand.GetComponent<ToolManager>();
            if (fm != null && Ingredients.Count < _ingredientsMaxCount && fm.CurrentState == ItemState.Sliced && CurrentState != ItemState.Burn)
            {
                return true;
            }
            else if (tm != null && tm.ThisToolPurpose == ToolPurpose.Dish)
            {
                PlateTool pt = tm.GetComponent<PlateTool>();
                pt.InputFromTool(this.GetComponent<ToolManager>());
                return false;
            }
            return false;
        }

        public override void StartCooking()
        {
            if (Ingredients.Count > 0 && CurrentTable.Purpose == TableManager.TablePurpose.Fire)
            {
                if (Ingredients.Count >= 2)
                {
                    FireTable ft = CurrentTable.GetComponent<FireTable>();
                    CurrentState = ItemState.None;
                    ft.OverTime = 0f;

                    ChangeElapsed(_plusDuration);
                }
                else
                {
                    Duration = _boilDuration;
                }
                IsCooking = true;
            }
        }

        
    }
}
