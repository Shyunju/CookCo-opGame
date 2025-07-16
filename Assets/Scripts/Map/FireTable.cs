using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CookCo_opGame
{
    public class FireTable : TableManager
    {
        [SerializeField] float _burnOutTime = 2f;
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
                        //change fire icon
                        _toolManager.IngredientUIController.ResetIngredientIcon();
                        _toolManager.IngredientUIController.AddIngredientIcon(GameManager.Instance.ItemDataList.Find((x) => x.ItemID == 0).IconSprite, 0); //불 아이템 추가해서 수정 필요
                        //change color
                        _toolManager.BurnState();
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
        public override void ChangeState(GameObject item)
        {
            _toolManager = item.GetComponent<ToolManager>();
            if (_toolManager != null && _toolManager.Ingredients.Count > 0)
            {
                if (_toolManager.ThisToolPurpose == ToolManager.ToolPurpose.Boil)
                {
                    _toolManager.CurrentState = ItemState.Boiled;
                    ChangeFoodItemState(_toolManager, 1000,1);

                }
                else if (_toolManager.ThisToolPurpose == ToolManager.ToolPurpose.Grill)
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
            //UI showing
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
