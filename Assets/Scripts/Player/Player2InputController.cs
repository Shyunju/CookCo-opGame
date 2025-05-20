using UnityEngine;
using UnityEngine.InputSystem;

namespace CookCo_opGame
{
    public class Player2InputController : PlayerController
    {
        [SerializeField] InputActionAsset _playerInputActions;
        InputActionMap _player2Map;
        InputAction _player2MoveAction;
        InputAction _player2PickAction;
        InputAction _player2DashAction;
        InputAction _player2ThrowAction;
        public override void OnEnable()
        {
            _player2Map = _playerInputActions.FindActionMap("Player2Actions");
            _player2Map.Enable();

            _player2MoveAction = _player2Map.FindAction("Move");
            _player2PickAction = _player2Map.FindAction("Pick");
            _player2DashAction = _player2Map.FindAction("Dash");
            _player2ThrowAction = _player2Map.FindAction("Throw");

            _player2MoveAction.performed += OnPlayerMove;
            _player2MoveAction.canceled += OnPlayerMove;

            _player2PickAction.performed += OnPlayerPick;
            _player2ThrowAction.performed += OnPlayerThrow;

            _player2MoveAction.Enable();
        }
    }
}
