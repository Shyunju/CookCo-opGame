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
                if (_tableManager.CurrentItem != null)
                {
                    if (mouseMove.Target != _transform || mouseMove.ItemOnHead != null)
                        return;
                    ItemManager itemManager = _tableManager.CurrentItem.GetComponent<ItemManager>();
                    itemManager.PickedUp(mouseMove.PlateOfMouse);
                }
                else
                {
                    return;
                }

                mouseMove.SetTarget(GameManager.Instance.MouseHouse);
            }
        }
    }
}
