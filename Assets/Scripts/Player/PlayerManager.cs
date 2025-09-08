using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CookCo_opGame
{
    public class PlayerManager : MonoBehaviour
    {
        [field: SerializeField] public PlayerOS PlayerOS { get; private set; }
        [field: Header("Animaiton")]
        [field: SerializeField] public PlayerAnimationData PlayerAnimationData { get; private set; }


        public Animator Animator { get; private set; }
        [SerializeField] float _rayDistance;
        PlayerHand _playerHand;
        RaycastHit _hit;
        AudioSource _audioSource;
        string _targetAnimState = "Chef_WorkStation_WokStirFry_Loop";

        public PlayerStateMachine StateMachine { get; set; }
        public PlayerController PlayerController { get; private set; }
        [SerializeField] private GameObject _playerKnife;
        public GameObject PlayerKnife { get{ return _playerKnife; } set { _playerKnife = value;}}



        private void Awake()
        {
            PlayerAnimationData.Initialize();
            Animator = GetComponentInChildren<Animator>();
            _audioSource = GetComponent<AudioSource>();

            StateMachine = new PlayerStateMachine(this);
        }
        void Start()
        {
            _playerHand = GetComponentInChildren<PlayerHand>();
            Animator = GetComponentInChildren<Animator>();
            PlayerController = GetComponent<PlayerController>();
            StateMachine.ChangeState(StateMachine.IdleState);

        }
        void Update()
        {
            Debug.DrawRay(transform.position, transform.forward * _rayDistance, Color.red);
            ShootRay();

            StateMachine.HandleInput();
            StateMachine.Update();

            var stateInfo = Animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName(_targetAnimState)) {
                if (!_audioSource.isPlaying) {
                    _audioSource.Play();
                }
            } else {
                if (_audioSource.isPlaying) {
                    _audioSource.Stop();
                }
            }
        }
        void FixedUpdate()
        {
            StateMachine.PhysicsUpdate();
        }
        private void ShootRay()
        {
            if (Physics.Raycast(transform.position, transform.forward, out _hit, _rayDistance, LayerMask.GetMask("Table"))) 
            {
                if (_playerHand.FrontTable != null) //앞에 테이블이있다
                {
                    if (_playerHand.FrontTable != _hit.collider.gameObject)
                    {
                        TableManager tb = _playerHand.FrontTable.GetComponent<TableManager>();
                        tb.ResetColor();
                    }
                        SetTable(_hit.collider.gameObject);
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
            TableManager tm = hit.GetComponent<TableManager>();
            _playerHand.FrontTable = hit;
            _playerHand.CurTableManager = tm;
            if (_playerHand.IsHandFree && tm.CurrentItem != null)
            {
                _playerHand.ItemManager = tm.CurrentItem.GetComponent<ItemManager>();
            }
            _playerHand.CurTableManager.SetHighlight();

        }

        
    }
}
