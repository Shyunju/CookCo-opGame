using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;

namespace CookCo_opGame
{
    public abstract class PlayerController : MonoBehaviour
    {
        private PlayerMove _playerMove;
        private PlayerHand _playerHand;
        private PlayerManager _playerManager;
        public abstract void OnEnable();
        public Vector2 Input { get; private set; } //*****************
        private bool _isRunning = false;
        public bool IsCooking{ get; set; }

        void Start()
        {
            IsCooking = false;
            _playerMove = GetComponent<PlayerMove>();
            _playerManager = GetComponent<PlayerManager>();
            _playerHand = GetComponentInChildren<PlayerHand>();
        }
        void Update()
        {
            _playerMove.MoveCharacter();
        }

        protected void OnPlayerMove(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _playerManager.StateMachine.ChangeState(_playerManager.StateMachine.IdleState);
                if(!IsCooking)
                Input = Vector3.zero;//********
                _playerMove.MoveDirection = Vector3.zero; // 키를 뗐을 때 멈춤
                return;
            }
            Input = context.ReadValue<Vector2>();
            Vector2 input = context.ReadValue<Vector2>();
            if (input != null)
            {
                if (!_isRunning)
                {
                    _playerManager.StateMachine.ChangeState(_playerManager.StateMachine.WalkState);
                }
                _playerMove.MoveDirection = new Vector3(input.x, 0f, input.y);
            }
        }
        public void OnPlayerPick(InputAction.CallbackContext context)
        {
            if (_playerHand.IsHandFree)
            {
                _playerHand.PickUpItem();
            }
            else
            {
                _playerHand.PutDownItem();
            }
        }

        public void OnPlayerThrow(InputAction.CallbackContext context)
        {
            if (_playerHand.IsHandFree)
            {
                _playerHand.CookAnimation();
            }
            else
            {
                _playerHand.ThrowItem();
            }
        }

        public void OnPlayerDash(InputAction.CallbackContext context)
        {
            
            StartCoroutine(DashMoveCo());
        }
        public IEnumerator DashMoveCo()
        {
            _isRunning = true;
            _playerMove.MoveSpeed = _playerMove.DashSpeed;

            yield return new WaitForSeconds(0.3f);
            _isRunning = false;
            _playerMove.MoveSpeed = _playerMove.DefaultSpeed;
        }
    }
}
