using System.Collections;
using UnityEditor;
using UnityEditor.Build.Pipeline;
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
            if (fm != null && Ingredients.Count < _ingredientsMaxCount && CurrentState != ItemState.Burn)
            {
                return true;
            }
            else if (tm != null && tm.ThisToolPurpose == ToolPurpose.Dish && !IsCooking)
            {
                PlateTool pt = tm.GetComponent<PlateTool>();
                pt.InputFromTool(this.GetComponent<ToolManager>());
                return false;
            }
            return false;
        }

        public override void StartCooking()  //테이블에 놓아도 호출, 재료를 넣어도 호출
        {
            if (Ingredients.Count > 0 && CurrentTable.Purpose == TablePurpose.Fire)
            {
                if (CurrentState == ItemState.Burn) return;
                if (CurrentState == ItemState.Warning)
                {
                    StartCoroutine(BurnCo());
                }
                if (Ingredients.Count >= 2) // ?
                {
                    //FireTable ft = CurrentTable.GetComponent<FireTable>();
                    CurrentState = ItemState.None;
                    //ft.OverTime = 0f;

                    ChangeElapsed(_plusDuration);
                }
                else
                {
                    Duration = _boilDuration;
                }
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
        public override IEnumerator WarningCo()
        {
            yield return new WaitForSeconds(5f);
            CurrentState = ItemState.Warning;
            
            StartCoroutine(BurnCo());
        }
        IEnumerator BurnCo()
        {
            WarningUI.SetActive(true);
            yield return new WaitForSeconds(5f);
            WarningUI.SetActive(false);
            CurrentState = ItemState.Burn;
            //change fire icon
            IngredientUIController.ResetIngredientIcon();
            IngredientUIController.AddIngredientIcon(GameManager.Instance.ItemDataList.Find((x) => x.ItemID == 0).IconSprite, 0); 
            BurnState();
        }

        
    }
}
