using UnityEngine;
using UnityEngine.InputSystem;

namespace CookCo_opGame
{
    public abstract class PlayerController : MonoBehaviour
    {
        private PlayerMove _playerMove;
        private PlayerHand _playerHand;
        public abstract void OnEnable();
        public Vector2 Input { get; private set; }

        void Start()
        {
            _playerMove = GetComponent<PlayerMove>();
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
                Input = Vector3.zero;//********
                _playerMove.MoveDirection = Vector3.zero; // 키를 뗐을 때 멈춤
                return;
            }
            Input = context.ReadValue<Vector2>();
            Vector2 input = context.ReadValue<Vector2>();
            if (input != null)
            {
                _playerMove.MoveDirection = new Vector3(input.x, 0f, input.y);
            }
        }
        public void OnPlayerPick(InputAction.CallbackContext context)
        {
            if (_playerHand.CanPickUp)
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
            StartCoroutine(_playerMove.DashMoveCo());
        }
    }
}
