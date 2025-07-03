using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CookCo_opGame
{
    public class SubmitTable : TableManager
    {
        [SerializeField] GameObject _usedPlate;
        [SerializeField] TableManager _respawnTable;
        List<int> _recipe = new List<int>();
        public override void ChangeState(GameObject item)
        {
            ToolManager toolManager = item.GetComponent<ToolManager>();
            List<FoodManager> ingredients = toolManager.Ingredients.ToList();
            if (ingredients.Count > 0)
            {
                _recipe.Clear();
                //submit
                foreach (var i in ingredients)
                {
                    _recipe.Add(i.ItemID);
                }
                _recipe.Sort();
                for (int i = 0; i < GameManager.Instance.Orders.Count; i++)
                {
                    if (_recipe.SequenceEqual(GameManager.Instance.Orders[i]))
                    {
                        GameManager.Instance.Orders.RemoveAt(i);
                        Debug.Log(GameManager.Instance.Orders.Count);
                        //점수추가
                        break;
                    }
                }
                Destroy(item);
            }
        }

        public override bool PerformPurpose()
        {
            //제출 시스템(레시피 비교할 어딘가에 요리를 넣음)
            //요리 아이템 사라지게함
            //올바른 요리인지는 ChangeState에서확인하게 하기
            return true;
        }

        IEnumerator RespawnUsedPlateCo()
        {
            yield return new WaitForSeconds(5f);
            if (!_respawnTable.IsFull)
            {
                CurrentItem = Instantiate(_usedPlate, _respawnTable.transform) as GameObject;
                CurrentItem.GetComponent<ItemManager>().PickedUp(_respawnTable.gameObject);
            }
            else
            {
                StartCoroutine(RespawnUsedPlateCo());
            }
        }
    }
}
