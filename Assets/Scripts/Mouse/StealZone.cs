using UnityEngine;

namespace CookCo_opGame
{
    public class StealZone : MonoBehaviour
    {
        [SerializeField] TableManager _tableManager;
        private Transform _transform;
        void Start()
        {
            _transform = GetComponent<Transform>();
            Transform parent = transform.parent;
            foreach (Transform sibling in parent)
            {
                if (sibling == transform) continue; // 자기 자신 제외

                TableManager comp = sibling.GetComponent<TableManager>();
                if (comp != null)
                {
                    _tableManager = comp;
                }
            }
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Mouse")
            {
                 
                MouseMove mouseMove = other.GetComponent<MouseMove>();
                if ( mouseMove.Target == _transform && mouseMove != null) //목표위치가 맞으면서 쥐가 맞음
                {
                    if (_tableManager.CurrentItem != null)  //테이블에 아이템이 있
                    {
                        ItemManager itemManager = _tableManager.CurrentItem.GetComponent<ItemManager>();
                        itemManager.PickedUp(mouseMove.PlateOfMouse);
                        mouseMove.CurrentItem = itemManager;
                        mouseMove.HasItem = true;
                    }
                    else
                    {
                        mouseMove.IsMoving = false;
                    }
                    mouseMove.SetTarget(mouseMove.MouseHouse);
                }
                return;

            }
        }
    }
}
