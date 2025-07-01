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
        public override void ChangeState(GameObject item)
        {
            throw new System.NotImplementedException();
        }

        public override bool PerformPurpose()
        {
            //제출 시스템(레시피 비교할 어딘가에 요리를 넣음)
            //요리 아이템 사라지게함
            //올바른 요리인지는 ChangeState에서확인하게 하기
            return true;
        }
        public void CheckRecipe(ToolManager toolManager)
        {
            //레시피(재료 리스트) 받아오기
            List<int> ingredients = toolManager.Ingredients.ToList();
            if (ingredients.Count > 0)
            {
                //submit
            }
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
