using UnityEngine;

namespace CookCo_opGame
{
    public class FoodManager : ItemManager
    {
        public enum ItemState
        {
            None,
            Sliced,
            Boiled,
            Grilled,
            Mixed
        }
        ItemState _currentState = ItemState.None;
        public ItemState CurrentState { get { return _currentState; } set { _currentState = value; } }
        
    }
}
