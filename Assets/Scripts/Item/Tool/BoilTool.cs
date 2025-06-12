using UnityEngine;

namespace CookCo_opGame
{
    public class BoilTool : ToolManager
    {
        private float _boilDuration = 13f;
        private int _maxIngredientInPot = 4;
        private float _plusDuration = 7f;
        void Start()
        {
            _ingredientsMaxCount = _maxIngredientInPot;
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
                if (Ingredients.Count >= 2)
                {
                    Duration += _plusDuration;
                }
                else
                {
                    Duration = _boilDuration;
                    IsCooking = true;
                }
            }
        }
    }
}
