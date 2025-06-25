using UnityEngine;

namespace CookCo_opGame
{
    public class MixTable : TableManager
    {
        private float _mixingDuration = 4f;
        void Start()
        {
            _purpose = TablePurpose.Mix;
        }
        public override void ChangeState(GameObject item)
        {
            FoodManager foodManager = item.GetComponent<FoodManager>();
            if (foodManager != null)
            {
                foodManager.CurrentState = ItemState.Mixed;
            }
        }

        public override bool PerformPurpose()
        {
            if (CurrentItem != null)
            {
                ItemManager itemManager = CurrentItem.GetComponent<ItemManager>();
                if (itemManager != null && itemManager.CurrentState == ItemState.Sliced)
                {
                    //StartCoroutine(CutFoodCo());
                    return true;                    
                }
            }
            return false;
        }
    }
}
