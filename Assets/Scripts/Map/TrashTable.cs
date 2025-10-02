using NUnit.Framework.Constraints;
using UnityEngine;

namespace CookCo_opGame
{
    public class TrashTable : TableManager
    {
        public override void ChangeState(GameObject item)
        {
            ItemManager im = item.GetComponent<ItemManager>();
            ToolManager tm = item.GetComponent<ToolManager>();
            PlateTool pt = item.GetComponent<PlateTool>();
            if (pt != null)
            {
                pt.ObjectOnPlate.mesh = null;
            }
            else if (tm != null)
            {
                GrillTool gt = tm.GetComponent<GrillTool>();
                BoilTool bt = tm.GetComponent<BoilTool>();
                if (gt != null || bt != null)
                {
                    if (gt != null)
                    {
                        gt.GrillFood.SetActive(false);
                        gt.StopAllCoroutines();
                    }
                    else
                    {
                        bt.StopAllCoroutines();
                    }
                }
                if (tm.Ingredients.Count > 0)
                {
                    tm.EmptyTool();
                    tm.ResetColor();
                }
                im.ResetCookingState();
                im.Elapsed = 0f;
            }
            else
            {
                Destroy(item);
                IsFull = false;
                GameManager.Instance.CurrnetObjectCount--;
                CookingPlayManager.Instance.AlertInstantiateUI(false);
            }
            
        }

        public override bool PerformPurpose()
        {
            return true;
        }
    }
}
