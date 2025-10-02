using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CookCo_opGame
{
    public class FireTable : TableManager
    {
        float _burnOutTime = 10f; //요리가 타는 데드라인
        ToolManager _toolManager;
        void Start()
        {
            _purpose = TablePurpose.Fire;
        }
        //끓이기 와 굽기 분기 고민 필요
        public override void ChangeState(GameObject item)
        {
            _toolManager = item.GetComponent<ToolManager>();
            if (_toolManager != null && _toolManager.Ingredients.Count > 0)
            {
                if (_toolManager.ThisToolPurpose == ToolPurpose.Boil)
                {
                    _toolManager.CurrentState = ItemState.Boiled;
                    ChangeFoodItemState(_toolManager, 1000,1);

                }
                else if (_toolManager.ThisToolPurpose == ToolPurpose.Grill)
                {
                    _toolManager.CurrentState = ItemState.Grilled;
                    ChangeFoodItemState(_toolManager, 10000,2);
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

        public IEnumerator BurnOutCo()
        {
            yield return new WaitForSeconds(_burnOutTime);
            if (_toolManager != null)
            {

                _toolManager.CurrentState = ItemState.Burn;
            }
        }

        public void ChangeFoodItemState(ToolManager tm, int mount, int meshIndex)
        {
            foreach (FoodManager food in tm.Ingredients)
            {
                food.ItemID += mount;
                food.ChangeMesh(meshIndex);
            }
        }
    }
}
