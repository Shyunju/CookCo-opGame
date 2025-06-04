using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CookCo_opGame
{
    public class PlayerManager : MonoBehaviour
    {
        [field :SerializeField] public PlayerOS PlayerOS { get; private set; }
        [field :Header("Animaiton")]
        [field :SerializeField] public PlayerAnimationData PlayerAnimationData { get; private set; }


        public Animator Animator { get; private set; }
        [SerializeField] float rayDistance;
        PlayerHand _playerHand;
        RaycastHit _hit;

        private PlayerStateMachine _stateMachine;
        public PlayerController PlayerController { get; private set; }



        private void Awake()
        {
            PlayerAnimationData.Initialize();
            Animator = GetComponentInChildren<Animator>();

            _stateMachine = new PlayerStateMachine(this);
        }
        void Start()
        {
            _playerHand = GetComponentInChildren<PlayerHand>();
            Animator = GetComponentInChildren<Animator>();
            PlayerController = GetComponent<PlayerController>();

        }
        void Update()
        {
            Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.red);
            ShootRay();

            _stateMachine.HandleInput();
            _stateMachine.Update();
        }
        void FixedUpdate()
        {
            _stateMachine.PhysicsUpdate();
        }
        private void ShootRay()
        {
            if (Physics.Raycast(transform.position, transform.forward, out _hit, rayDistance, LayerMask.GetMask("Table")))
            {
                if (_playerHand.FrontTable != null)
                {
                    if (_playerHand.FrontTable != _hit.collider.gameObject)
                    {
                        //_playerHand.CurTableManager.ResetColor();
                        TableManager tb = _playerHand.FrontTable.GetComponent<TableManager>();
                        tb.ResetColor();
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
