using UnityEngine;
using System;

namespace CookCo_opGame
{
    public enum ItemState
    {
        None,
        Sliced,
        Boiled,
        Grilled,
        Mixed,
        Complete
    }    
    public class FoodManager : ItemManager
    {
        

        // public override void ChaingeState(string state)
        // {
        //     CurrentState = (ItemState)Enum.Parse(typeof(ItemState), state);
        // }
    }
}
