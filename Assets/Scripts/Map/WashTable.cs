using System.Collections;
using UnityEngine;

namespace CookCo_opGame
{
    public class WashTable : TableManager
    {
        //더러운 접시가 있다면 사용가능
        //  물 보이고 설거지 애니메이션
        // 세척한 접시는 일정한 곳에 스폰
        [SerializeField] GameObject _cleanPlateSpot;
        [SerializeField] GameObject _water;
        GameObject[] _usedPlates;
        GameObject[] _cleanPlates;
        public override void ChangeState(GameObject item)
        {
            throw new System.NotImplementedException();
        }

        public override bool PerformPurpose()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator WashingDishCo()
        {
            yield return new WaitForSeconds(0);
        }
    }
}
