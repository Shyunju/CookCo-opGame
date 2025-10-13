using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

namespace CookCo_opGame
{
    public class SubmitTable : TableBase
    {
        [SerializeField] GameObject _usedPlate;
        [SerializeField] Boxtable _respawnTable;
        [SerializeField] int _failScore = -20;
        List<int> _recipe = new List<int>();
        private float _respawnCoolTime = 3f;
        private bool _hasRecipe = false;
        public override void ChangeState(GameObject item)
        {
            ToolBase toolManager = item.GetComponent<ToolBase>();
            List<FoodBase> ingredients = toolManager.Ingredients.ToList();
            _recipe.Clear();
            //submit
            foreach (var i in ingredients)
            {
                _recipe.Add(i.ItemID);
            }
            _recipe.Sort();
            for (int i = 0; i < CookingPlayManager.Instance.Orders.Count; i++)
            {
                if (_recipe.SequenceEqual(CookingPlayManager.Instance.Orders[i].Item1))
                {
                    CookingPlayManager.Instance.CompleteMenu(i);
                    CookingPlayManager.Instance.ChangeScore(CookingPlayManager.Instance.Orders[i].Item2);
                    CookingPlayManager.Instance.Orders.RemoveAt(i);
                    SoundManager.Instance.PlaySuccessSound();
                    _hasRecipe = true;
                    break;
                }
            }
            if (!_hasRecipe)
            {
                CookingPlayManager.Instance.ChangeScore(_failScore);
                SoundManager.Instance.PlayFailSound();
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
