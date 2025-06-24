using NUnit.Framework.Constraints;
using UnityEngine;

namespace CookCo_opGame
{
    public class TrashTable : TableManager
    {
        public override void ChaingeState(GameObject item)
        {
            ItemManager im = item.GetComponent<ItemManager>();
            ToolManager tm = item.GetComponent<ToolManager>();
            PlateTool pt = item.GetComponent<PlateTool>();
            if (pt != null)
            {
                pt.ObjectOnPlate.mesh = null;
            }
            if (tm != null)
                {
                    if (tm.Ingredients.Count > 0)
                    {
                        tm.EmptyTool();
                    }
                    im.ResetCookingState();
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
