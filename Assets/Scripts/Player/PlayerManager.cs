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
                if (_playerHand.FrontTable != null)
                {
                    if (_playerHand.FrontTable != _hit.collider.gameObject)
                    {
                        _playerHand.CurTableManager.ResetColor();
                        SetTable(_hit.collider.gameObject);
                    }
                }
                else
                {
                    SetTable(_hit.collider.gameObject);
                }

            }
            else
            {
                if (_playerHand.FrontTable != null)
                {
                    TableManager tb = _playerHand.FrontTable.GetComponent<TableManager>();
                    tb.ResetColor();
                }
                _playerHand.FrontTable = null;
                _playerHand.CurTableManager = null;
            }
        }
        private void SetTable(GameObject hit)
        {
            _playerHand.FrontTable = hit;
            _playerHand.CurTableManager = hit.GetComponent<TableManager>();
            _playerHand.CurTableManager.SetHighlight();
        }
        
    }
}
