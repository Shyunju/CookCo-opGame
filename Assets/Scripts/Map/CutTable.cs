using System.Collections;
using UnityEngine;

namespace CookCo_opGame
{
    public class CutTable : TableManager
    {
        void Start()
        {
            _purpose = TablePurpose.Cut;
        }
        public override bool PerformPurpose()
        {
            if (CurrentItem != null)
            {
                ItemManager itemManager = CurrentItem.GetComponent<ItemManager>();
                if (itemManager != null && itemManager.CurrentState == ItemState.None)
                {
                    StartCoroutine(CutFoodCo());
                    return true;                    
                }
            }
            return false;
        }
        IEnumerator CutFoodCo()
        {
            yield return new WaitForSeconds(.2f);
            ItemManager itemManager = CurrentItem.GetComponent<ItemManager>();
            itemManager.Duration = 3f;
            itemManager.IsCooking = true;
        }
        public override void ChaingeState(GameObject item)
        {
            FoodManager foodManager = item.GetComponent<FoodManager>();
            if (foodManager != null)
            {
                foodManager.CurrentState = ItemState.Sliced;                
            }
        }
    }
}
