using UnityEngine;

namespace CookCo_opGame
{
    public class MixTable : TableBase
    {
        void Start()
        {
            _purpose = TablePurpose.Mix;
        }
        public override void ChangeState(GameObject item)
        {
            FoodBase foodManager = item.GetComponent<FoodBase>();
            if (foodManager != null)
            {
                foodManager.CurrentState = ItemState.Mixed;
            }
        }

        public override bool PerformPurpose()
        {
            if (CurrentItem != null)
            {
                ItemBase itemManager = CurrentItem.GetComponent<ItemBase>();
                if (itemManager != null && itemManager.CurrentState == ItemState.Sliced)
                {
                    return true;                    
                }
            }
            return false;
        }
    }
}
