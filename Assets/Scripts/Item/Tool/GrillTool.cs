using UnityEngine;

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
            if (fm != null && Ingredients.Count < _ingredientsMaxCount && fm.CurrentState == ItemState.Sliced)
            {
                return true;
            }
            return false;
        }

        public override void StartCooking()
        {
            if (Ingredients.Count > 0 && CurrentTable.purpose == TableManager.TablePurpose.Fire)
            {
                Duration = _grillDuration;
                IsCooking = true;
            }
        }
    }
}
