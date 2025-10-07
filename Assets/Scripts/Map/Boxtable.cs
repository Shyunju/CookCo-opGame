using UnityEngine;

namespace CookCo_opGame
{
    public class Boxtable : TableManager
    {
        [SerializeField] GameObject _itemInBox;
        [SerializeField] GameObject _plate;
        void Start()
        {
            //base.Start();
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
                    CurrentItem.GetComponent<ItemManager>().PickedUp(this.gameObject);
                }
                if (GameManager.Instance.CurrnetObjectCount == GameManager.Instance.MaxObjectCount)
                    CookingPlayManager.Instance.AlertInstantiateUI(true);
                return true;
            }
            return false;
        }

        public override void ChangeState(GameObject item)
        {
            ItemManager itemManager = item.GetComponent<ItemManager>();
            itemManager.CurrentState = ItemState.None;
        }

        public void SpawnPlate()
        {            
            CurrentItem = Instantiate(_plate, transform.position, Quaternion.identity) as GameObject;
            CurrentItem.GetComponent<ItemManager>().PickedUp(this.gameObject);
        }
    }
}
