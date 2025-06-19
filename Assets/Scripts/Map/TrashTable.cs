using UnityEngine;

namespace CookCo_opGame
{
    public class TrashTable : TableManager
    {
        public override void ChaingeState(GameObject item)
        {
            ItemManager im = item.GetComponent<ItemManager>();
            ToolManager tm = item.GetComponent<ToolManager>();
            if (tm != null)
            {
                if (tm.Ingredients.Count > 0)
                {
                    tm.Ingredients.Clear();
                    tm.IngredientUIController.ResetIngredientIcon();
                }
                im.ResetState();
                im.CurrentState = ItemState.None;
            }
            else
            {
                Destroy(item);
                IsFull = false;
            }
            
        }

        public override bool PerformPurpose()
        {
            return true;
        }
    }
}
