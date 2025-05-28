using UnityEngine;

namespace CookCo_opGame
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] float rayDistance;
        PlayerHand _playerHand;
        RaycastHit _hit;
        void Start()
        {
            _playerHand = GetComponentInChildren<PlayerHand>();
        }
        void Update()
        {
            Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.red);
            ShootRay();
        }
        private void ShootRay()
        {
            if (Physics.Raycast(transform.position, transform.forward, out _hit, rayDistance, LayerMask.GetMask("Table")))
            {
                _playerHand.FrontTable = _hit.collider.gameObject;
                _playerHand.CurTableManager = _hit.collider.gameObject.GetComponent<TableManager>();
            }
            else
            {
                _playerHand.FrontTable = null;
                if (_playerHand.FrontTable != null)
                {
                    _playerHand.CurTableManager = null;
                }
            }
        }
    }
}
