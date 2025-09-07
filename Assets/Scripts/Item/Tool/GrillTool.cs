using UnityEngine;
using System.Linq;
namespace CookCo_opGame
{
    public class GrillTool : ToolManager
    {
        [SerializeField] GameObject _grillingFood;
        public GameObject GrillFood { get { return _grillingFood; } set { _grillingFood = value; } }
        private float _grillDuration = 7f;
        public override bool CheckToolState(GameObject itemInHand)
        {
            FoodManager fm = itemInHand.GetComponent<FoodManager>();
            ToolManager tm = itemInHand.GetComponent<ToolManager>();
            if (fm != null && Ingredients.Count < _ingredientsMaxCount && (fm.ItemID == 106 || fm.ItemID == 5))
            {
                return true;
            }
            else if (tm != null && tm.ThisToolPurpose == ToolPurpose.Dish)
            {
                PlateTool pt = tm.GetComponent<PlateTool>();
                pt.InputFromTool(this.GetComponent<ToolManager>());
                _grillingFood.SetActive(false);
                //EmptyTool();
                return false;
            }
            return false;
        }

        public override void StartCooking()
        {
            if (Ingredients.Count > 0 && CurrentTable.Purpose == TablePurpose.Fire && CurrentState != ItemState.Burn)
            {
                CurrentState = ItemState.None;
                FireTable ft = CurrentTable.GetComponent<FireTable>();
                ft.OverTime = 0f;
                _grillingFood.SetActive(true);
                Duration = _grillDuration;
                IsCooking = true;
            }
        }
        public override void PickedUp(GameObject parent)
        {
            base.PickedUp(parent);
            if (parent.tag == "Hand")
            {
                WarningUI.SetActive(false);
            }
        }
    }
}
