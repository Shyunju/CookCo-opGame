using UnityEngine;

namespace CookCo_opGame
{
    public class FireTable : TableManager
    {
        private float _fireDuration = 5f;
        void Start()
        {
            _purpose = TablePurpose.Fire;
        }
        
        //끓이기 와 굽기 분기 고민 필요
        public override void ChaingeState(GameObject item)
        {
            FoodManager foodManager = item.GetComponent<FoodManager>();
            if (foodManager != null)
            {
                foodManager.CurrentState = ItemState.Sliced;
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
