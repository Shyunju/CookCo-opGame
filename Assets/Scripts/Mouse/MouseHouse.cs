using UnityEngine;

namespace CookCo_opGame
{
    public class MouseHouse : MonoBehaviour
    {
        [SerializeField] MouseMove[] mouses;
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Mouse"))
            {
                MouseMove mouseMove = other.gameObject.GetComponent<MouseMove>();
                //mouseMove.MouseCollider.isTrigger = true;

                var currentItem = mouseMove.CurrentItem;  // 변수에 복사

                if (currentItem != null)
                {
                    mouseMove.HasItem = false;
                    GameObject temp = currentItem.gameObject;

                    mouseMove.CurrentItem = null;  // 참조 먼저 null로 끊기
                    Destroy(temp);

                    CookingPlayManager.Instance.ChangeLife(-1);
                    mouseMove.IsMoving = false;
                }
            }
        }
        public void ExportMouse(Transform transform)
        {
            foreach (var mouse in mouses)
            {
                if (!mouse.IsMoving)
                {
                    mouse.SetTarget(transform);
                    mouse.IsMoving = true;
                    return;
                }
            }
        }
    }
}
