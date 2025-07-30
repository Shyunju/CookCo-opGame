using UnityEngine;

namespace CookCo_opGame
{
    public class StealZone : MonoBehaviour
    {
        [SerializeField] TableManager _tableManager;

        void Start()
        {
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
                MouseMove mm = other.GetComponent<MouseMove>();
                ItemManager im = _tableManager.CurrentItem.GetComponent<ItemManager>();
                im.PickedUp(mm.PlateOfMouse);
            }
        }
    }
}
