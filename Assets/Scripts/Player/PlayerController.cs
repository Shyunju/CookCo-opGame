using UnityEngine;
using UnityEngine.InputSystem;

namespace CookCo_opGame
{
    public abstract class PlayerController : MonoBehaviour
    {
        private Vector3 moveDirection;
        private float _moveSpeed = 4f;
        private Rigidbody _playerRigidBody;
        private float _rotationSpeed = 8f;
        private PlayerHand _playerHand;
        public abstract void OnEnable();

        void Start()
        {
            _playerRigidBody = GetComponent<Rigidbody>();
            _playerHand = GetComponentInChildren<PlayerHand>();
        }
        void Update()
        {
            MoveCharacter();
        }

        protected void OnPlayerMove(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                moveDirection = Vector3.zero; // 키를 뗐을 때 멈춤
                return;
            }
            Vector2 input = context.ReadValue<Vector2>();
            if (input != null)
            {
                moveDirection = new Vector3(input.x, 0f, input.y);
            }
        }
        public void MoveCharacter()
        {
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    _rotationSpeed * Time.deltaTime
                );
                transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
                Vector3 movement = moveDirection.normalized * _moveSpeed * Time.fixedDeltaTime;
                _playerRigidBody.MovePosition(_playerRigidBody.position + movement);
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
        
    }
}
