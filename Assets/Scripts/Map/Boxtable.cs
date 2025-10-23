using UnityEngine;

namespace CookCo_opGame
{
    public class Boxtable : TableBase
    {
        [SerializeField] GameObject _itemInBox;
        [SerializeField] GameObject _plate;
        void Start()
        {
            _purpose = TablePurpose.Box;
        }
        public override bool PerformPurpose()
        {
            if (!IsFull && Purpose == TablePurpose.Box)
            {
                if (GameManager.Instance.CurrnetObjectCount < GameManager.Instance.MaxObjectCount)
                {
                    CurrentItem = Instantiate(_itemInBox, transform.position, Quaternion.identity);
                    GameManager.Instance.CurrnetObjectCount++;
                    CurrentItem.GetComponent<ItemBase>().PickedUp(this.gameObject);
                }
                if (GameManager.Instance.CurrnetObjectCount == GameManager.Instance.MaxObjectCount)
                    CookingPlayManager.Instance.AlertInstantiateUI(true);
                return true;
            }
            return false;
        }

        public override void ChangeState(GameObject item)
        {
            ItemBase itemManager = item.GetComponent<ItemBase>();
            itemManager.CurrentState = ItemState.None;
        }

        public void SpawnPlate()
        {            
            CurrentItem = Instantiate(_plate, transform.position, Quaternion.identity) as GameObject;
            CurrentItem.GetComponent<ItemBase>().PickedUp(this.gameObject);
        }
    }
}
