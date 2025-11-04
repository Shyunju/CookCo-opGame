using System.Collections;
using UnityEngine;

namespace CookCo_opGame
{
    public class CutTable : TableBase
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
                FoodBase foodManager = CurrentItem.GetComponent<FoodBase>();
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
            yield return new WaitForSeconds(.1f);
            SoundManager.Instance.PlayCuttingSound(PlayerManager.PlayerInput.PlayerNumber);
            _knife.SetActive(false);
            ItemBase itemBase = CurrentItem.GetComponent<ItemBase>();
            itemBase.Duration = _cuttingDuration;
            itemBase.IsCooking = true;
        }
        public override void ChangeState(GameObject item)
        {
            FoodBase foodManager = item.GetComponent<FoodBase>();
            if (foodManager != null)
            {
                SoundManager.Instance.StopCuttingSound(PlayerManager.PlayerInput.PlayerNumber);
                foodManager.CurrentState = ItemState.Sliced;
                foodManager.ItemID += 100;
                foodManager.ChangeMesh(0);
                _knife.SetActive(true);
                foodManager.IsCooking = false;
            }
            PlayerManager.StateMachine.ChangeState(PlayerManager.StateMachine.IdleState);
            PlayerManager = null;

        }
    }
}
