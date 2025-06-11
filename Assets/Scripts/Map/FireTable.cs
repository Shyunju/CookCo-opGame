using UnityEditor;
using UnityEngine;

namespace CookCo_opGame
{
    public class FireTable : TableManager
    {
        private float _fireDuration = 5f;
        void Start()
        {
            _purpose = TablePurpose.Fire;
        }
        
        //끓이기 와 굽기 분기 고민 필요
        public override void ChaingeState(GameObject item)    //의문??? 재료 아이템을 바꿔? 재료는 도구가 바구고 도구상태를 바꿔?       
        {
            ToolManager toolManager = item.GetComponent<ToolManager>();
            if (toolManager != null)
            {
                if (toolManager.ThisToolPurpose == ToolManager.ToolPurpose.Biol)
                {
                    toolManager.CurrentState = ItemState.Boiled;

                }
                else if (toolManager.ThisToolPurpose == ToolManager.ToolPurpose.Grill)
                {
                    toolManager.CurrentState = ItemState.Grilled;
                }
            }
        }

        public override bool PerformPurpose()
        {
            if (CurrentItem != null)
            {
                ItemManager itemManager = CurrentItem.GetComponent<ItemManager>();
                if (itemManager != null && itemManager.CurrentState == ItemState.None)
                {
                    return true;                    
                }
            }
            return false;
        }
    }
}
