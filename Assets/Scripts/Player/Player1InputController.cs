using UnityEngine;
using UnityEngine.InputSystem;

namespace CookCo_opGame
{
    public class Player1InputController : PlayerController
    {
        [SerializeField] InputActionAsset _playerInputActions;
        InputActionMap _player1Map;
        InputAction _player1MoveAction;
        InputAction _player1PickAction;
        InputAction _player1DashAction;
        InputAction _player1ThrowAction;
        public override void OnEnable()
        {
            _player1Map = _playerInputActions.FindActionMap("Player1Actions");
            _player1Map.Enable();

            _player1MoveAction = _player1Map.FindAction("Move");
            _player1PickAction = _player1Map.FindAction("Pick");
            _player1DashAction = _player1Map.FindAction("Dash");
            _player1ThrowAction = _player1Map.FindAction("Throw");

            _player1MoveAction.performed += OnPlayerMove;
            _player1MoveAction.canceled += OnPlayerMove;

            _player1PickAction.performed += OnPlayerPick;
            _player1ThrowAction.performed += OnPlayerThrow;

            _player1MoveAction.Enable();
        }


    }
}
