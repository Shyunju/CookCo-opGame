using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

namespace CookCo_opGame
{
    public class SubmitTable : TableManager
    {
        [SerializeField] GameObject _usedPlate;
        [SerializeField] Boxtable _respawnTable;
        [SerializeField] int _successScore = 100;
        [SerializeField] int _failScore = -20;
        List<int> _recipe = new List<int>();
        private float _respawnCoolTime = 3f;
        private bool _hasRecipe = false;
        public override void ChangeState(GameObject item)
        {
            ToolManager toolManager = item.GetComponent<ToolManager>();
            List<FoodManager> ingredients = toolManager.Ingredients.ToList();
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
                    Debug.Log(GameManager.Instance.OrdersUI.Count);
                    GameManager.Instance.Orders.RemoveAt(i);
                    GameManager.Instance.CompleteMenu(i);
                    Debug.Log("success");
                    GameManager.Instance.ChangeScore(_successScore);
                    _hasRecipe = true;
                    break;
                }
            }
            if (!_hasRecipe)
            {
                GameManager.Instance.ChangeScore(_failScore);
                Debug.Log("fail");
            }

            StartCoroutine(RespawnUsedPlateCo());
            Destroy(item);
        }

        IEnumerator RespawnUsedPlateCo()
        {
            yield return new WaitForSeconds(_respawnCoolTime);
            if (!_respawnTable.IsFull)
            {
                _respawnTable.SpawnPlate();
            }
            else
            {
                StartCoroutine(RespawnUsedPlateCo());
            }
        }
    }
}
