using UnityEngine;
using UnityEngine.InputSystem;

namespace CookCo_opGame
{
    public abstract class PlayerController : MonoBehaviour
    {
        // [SerializeField] InputActionAsset _playerInputActions;
        // InputActionMap _player1Map;
        // InputAction _player1MoveAction;


        private Vector3 moveDirection;
        private float _moveSpeed = 4f;
        private Rigidbody _playerRigidBody;
        private float _rotationSpeed = 8f;
        public abstract void OnEnable();

        // _player1Map = _playerInputActions.FindActionMap("Player1Actions");
        // _player1Map.Enable();

        // _player1MoveAction = _player1Map.FindAction("Move");

        // _player1MoveAction.performed += OnPlayer1Move;

        // _player1MoveAction.Enable();
        void Start()
        {
            _playerRigidBody = GetComponent<Rigidbody>();
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
        // private void OnPlayer2Move(InputAction.CallbackContext context)
        // {
        //     Vector2 input = context.ReadValue<Vector2>();
        //     if(input != null)
        //     {
        //         moveDirection = new Vector3(input.x, 0f, input.y);
        //     }
        // }
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
                Vector3 movement = moveDirection.normalized  * _moveSpeed * Time.fixedDeltaTime;
                _playerRigidBody.MovePosition(_playerRigidBody.position + movement);
            }
        }
        void Update()
        {
            MoveCharacter();
        }
    }
}
