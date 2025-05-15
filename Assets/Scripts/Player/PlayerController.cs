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
        private float moveSpeed = 4f;
        public abstract void OnEnable();
        
            // _player1Map = _playerInputActions.FindActionMap("Player1Actions");
            // _player1Map.Enable();

            // _player1MoveAction = _player1Map.FindAction("Move");

            // _player1MoveAction.performed += OnPlayer1Move;

            // _player1MoveAction.Enable();
        
        private void OnPlayerMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            if(input != null)
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
                transform.rotation = Quaternion.LookRotation(moveDirection);
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
        }
    }
}
