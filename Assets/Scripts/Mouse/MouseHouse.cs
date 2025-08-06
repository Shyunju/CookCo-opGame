using UnityEngine;

namespace CookCo_opGame
{
    public class MouseHouse : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Mouse"))
            {
                MouseMove mouseMove = other.gameObject.GetComponent<MouseMove>();

                var currentItem = mouseMove.CurrentItem;  // 변수에 복사
        
                if (currentItem != null)
                {
                    GameObject temp = currentItem.gameObject;
        
                    mouseMove.CurrentItem = null;  // 참조 먼저 null로 끊기
                    Destroy(temp);
        
                    GameManager.Instance.ChangeLife(-1);
                    mouseMove.SetOff();
                }
            }
        }
    }
}
