using UnityEngine;

namespace CookCo_opGame
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] float rayDistance;
        PlayerHand _playerHand;
        RaycastHit _hit;
        int layerMask;
        void Start()
        {
            _playerHand = GetComponentInChildren<PlayerHand>();
            layerMask = ~(1 << LayerMask.NameToLayer("Item"));
        }
        void Update()
        {
            Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.red);
            ShootRay();
        }
        private void ShootRay()
        {
            if (Physics.Raycast(transform.position, transform.forward, out _hit, rayDistance, layerMask))
            {
                if (_hit.collider.gameObject.tag == "Table")
                {
                    _playerHand.FrontTable = _hit.collider.gameObject;
                    _playerHand.TableManager = _hit.collider.gameObject.GetComponent<TableManager>();
                }
            }
            else
            {
                _playerHand.FrontTable = null;
                if (_playerHand.FrontTable != null)
                {
                    _playerHand.TableManager = null;
                }
            }
        }
    }
}
