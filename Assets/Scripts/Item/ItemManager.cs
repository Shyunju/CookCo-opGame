using UnityEngine;

namespace CookCo_opGame
{
    public class ItemManager : MonoBehaviour
    {
        //포지션, 로테이션 얼리기
        //콜라이더 트리거로 변환
        //그래비티 오프
        //부모 위치 바꾸기
        //부모기준 위치 제로
        //
        Rigidbody _itemRigidbody;
        ItemState _currentState;
        public ItemState CurrentState {get { return _currentState;} set { _currentState = value; } }

        public enum ItemState
        {
            None,
            Grab,
            Sliced,
            Boiled,
            Grilled,
            Mixed
        }
        void Start()
        {
            _itemRigidbody = GetComponent<Rigidbody>();
        }
    }
}
