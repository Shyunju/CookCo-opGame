using UnityEngine;

namespace CookCo_opGame
{
    public class Boxtable : TableManager
    {
        [SerializeField] GameObject _itemInBox;
        void Start()
        {
            _purpose = TablePurpose.Box;
        }
        public override bool PerformPurpose()
        {
            if (!IsFull)
            {
                CurrentItem = Instantiate(_itemInBox, transform.position, Quaternion.identity) as GameObject;
                CurrentItem.GetComponent<ItemManager>().PickedUp(this.gameObject);
                
                return true;
            }
            return false;
        }

        public override void ChaingeState(GameObject item)
        {
            ItemManager itemManager = item.GetComponent<ItemManager>();
            itemManager.CurrentState = ItemState.None;
        }
    }
}
