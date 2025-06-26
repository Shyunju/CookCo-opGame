using System.Collections.Generic;
using UnityEngine;

namespace CookCo_opGame
{
    public class SubmitTable : TableManager
    {
        [SerializeField] GameObject _usedPlate;
        [SerializeField] GameObject _respawnTable;
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
            List<GameObject> ingredients = toolManager.Ingredients;
            if (ingredients.Count > 0)
            {
                //submit
            }
        }

        public void RespawnUsedPlate()
        {
            //Instantiate(_usedPlate, _respawnTable.transform) as GameObject;
        }
    }
}
