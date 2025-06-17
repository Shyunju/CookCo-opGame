using UnityEngine;

namespace CookCo_opGame
{
    public class TrashTable : TableManager
    {
        public override void ChaingeState(GameObject item)
        {
            ItemManager im = item.GetComponent<ItemManager>();
            ToolManager tm = item.GetComponent<ToolManager>();
            tm.Ingredients.Clear();
            
            im.CurrentState = ItemState.None;
        }

        public override bool PerformPurpose()
        {
            return true;
        }
    }
}
