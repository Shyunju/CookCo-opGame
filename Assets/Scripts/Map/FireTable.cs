using System.Collections;
using UnityEditor;
using UnityEngine;

namespace CookCo_opGame
{
    public class FireTable : TableManager
    {
        [SerializeField] float _burnOutTime = 5f;
        float _overTime = 0f;
        float _warningTime = 1.5f;
        ToolManager _toolManager;
        void Start()
        {
            _purpose = TablePurpose.Fire;
        }
        void Update()
        {
            if (IsFull)
            {
                ItemManager im = CurrentItem.GetComponent<ItemManager>();
                if (im != null && (im.CurrentState == ItemState.Boiled || im.CurrentState == ItemState.Grilled))
                {
                    _toolManager = CurrentItem.GetComponent<ToolManager>();
                    if (_overTime > _burnOutTime)
                    {
                        _toolManager.CurrentState = ItemState.Burn;
                        _overTime = 0f;
                        //change color
                    }
                    if (_overTime > _warningTime)
                    {
                        //warning UI show
                    }
                    _overTime += Time.deltaTime;
                }
            }
        }

        //끓이기 와 굽기 분기 고민 필요
        public override void ChaingeState(GameObject item)    //의문??? 재료 아이템을 바꿔? 재료는 도구가 바구고 도구상태를 바꿔?       
        {
            _toolManager = item.GetComponent<ToolManager>();
            if (_toolManager != null && _toolManager.Ingredients.Count > 0)
            {
                if (_toolManager.ThisToolPurpose == ToolManager.ToolPurpose.Boil)
                {
                    _toolManager.CurrentState = ItemState.Boiled;

                }
                else if (_toolManager.ThisToolPurpose == ToolManager.ToolPurpose.Grill)
                {
                    _toolManager.CurrentState = ItemState.Grilled;
                }
                _toolManager.ChangeFoodIcon();
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
            //UI showing
            yield return new WaitForSeconds(_burnOutTime);
            if (_toolManager != null)
            {
                
                _toolManager.CurrentState = ItemState.Burn;                
            }
        }
    }
}
