using UnityEngine;
using System.Linq;
using System.Collections;
namespace CookCo_opGame
{
    public class GrillTool : ToolManager
    {
        [SerializeField] GameObject _grillingFood;
        public GameObject GrillFood { get { return _grillingFood; } set { _grillingFood = value; } }
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
                if (CurrentState == ItemState.Warning)
                {
                    StartCoroutine(BurnCo());
                }
                else
                {
                    _grillingFood.SetActive(true);
                    IsCooking = true;                    
                }
            }
        }
        public override void PickedUp(GameObject parent)
        {
            base.PickedUp(parent);
            if (parent.tag == "Hand")
            {
                StopAllCoroutines();
                WarningUI.SetActive(false);
            }
        }
        public override IEnumerator WarningCo()
        {
            print("grill warning co");
            yield return new WaitForSeconds(5f);
            CurrentState = ItemState.Warning;
            
            StartCoroutine(BurnCo());
        }
        IEnumerator BurnCo()
        {
            print("grill burn co");
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
