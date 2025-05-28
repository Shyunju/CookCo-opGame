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
        public override void PerformPurpose()
        {
            if (!IsFull)
            {
                CurrentItem = Instantiate(_itemInBox, transform.position, Quaternion.identity)as GameObject;
                CurrentItem.GetComponent<ItemManager>().PickedUp(this.gameObject);
                //IsFull = true;                
            }
        }
        public void PlayAnimation()
        {

        }
    }
}
