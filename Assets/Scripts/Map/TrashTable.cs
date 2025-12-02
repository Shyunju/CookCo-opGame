using UnityEngine;

namespace CookCo_opGame
{
    public class TrashTable : TableBase
    {
        public override void ChangeState(GameObject item)
        {
            ItemBase im = item.GetComponent<ItemBase>();
            FireToolBase ftb = item.GetComponent<FireToolBase>();
            PlateTool pt = item.GetComponent<PlateTool>();
            if (pt != null)
            {
                pt.ObjectOnPlate.mesh = null;
                pt.EmptyTool();
            }
            else if (ftb != null)
            {
                GrillTool gt = ftb.GetComponent<GrillTool>();
                BoilTool bt = ftb.GetComponent<BoilTool>();
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
                if (ftb.Ingredients.Count > 0)
                {
                    ftb.EmptyTool();
                    ftb.ResetColor();
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
