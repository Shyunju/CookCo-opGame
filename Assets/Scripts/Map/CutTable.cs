using System.Collections;
using UnityEngine;

namespace CookCo_opGame
{
    public class CutTable : TableManager
    {
        void Start()
        {
            _purpose = TablePurpose.Cut;
        }
        public override bool PerformPurpose()
        {
            if (CurrentItem != null)
            {
                StartCoroutine(CutFoodCo());
                return true;
            }
            return false;
        }
        IEnumerator CutFoodCo()
        {
            yield return new WaitForSeconds(.2f);
            Debug.Log("cut");
        }
    }
}
