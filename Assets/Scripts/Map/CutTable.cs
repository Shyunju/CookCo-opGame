using System.Collections;
using UnityEngine;

namespace CookCo_opGame
{
    public class CutTable : TableManager
    {
        [SerializeField] GameObject _knife;
        private float _cuttingDuration = 3f;

        public PlayerManager PlayerManager { get; set; }
        void Start()
        {
            _purpose = TablePurpose.Cut;
        }
        public override bool PerformPurpose()
        {
            if (CurrentItem != null)
            {
                FoodManager foodManager = CurrentItem.GetComponent<FoodManager>();
                if (foodManager != null && foodManager.CurrentState == ItemState.None)
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
            _knife.SetActive(false);
            ItemManager itemManager = CurrentItem.GetComponent<ItemManager>();
            itemManager.Duration = _cuttingDuration;
            itemManager.IsCooking = true;
        }
        public override void ChaingeState(GameObject item)
        {
            FoodManager foodManager = item.GetComponent<FoodManager>();
            if (foodManager != null)
            {
                foodManager.CurrentState = ItemState.Sliced;
                foodManager.ChangeMesh(0);
                _knife.SetActive(true); 
            }
            PlayerManager.StateMachine.ChaingeState(PlayerManager.StateMachine.IdleState);
            PlayerManager = null;

        }
    }
}
