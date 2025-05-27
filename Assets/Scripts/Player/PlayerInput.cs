using UnityEngine;
using UnityEngine.InputSystem;

namespace CookCo_opGame
{
    public class PlayerInput : PlayerController
    {
        [SerializeField] int _playerNumber;
        [SerializeField] InputActionAsset _playerInputActions;
        InputActionMap _playerMap;
        InputAction _playerMoveAction;
        InputAction _playerPickAction;
        InputAction _playerDashAction;
        InputAction _playerThrowAction;
        public override void OnEnable()
        {
            if(_playerNumber == 1)
                _playerMap = _playerInputActions.FindActionMap("Player1Actions");
            else
                _playerMap = _playerInputActions.FindActionMap("Player2Actions");
            _playerMap.Enable();

            _playerMoveAction = _playerMap.FindAction("Move");
            _playerPickAction = _playerMap.FindAction("Pick");
            _playerDashAction = _playerMap.FindAction("Dash");
            _playerThrowAction = _playerMap.FindAction("Throw");

            _playerMoveAction.performed += OnPlayerMove;
            _playerMoveAction.canceled += OnPlayerMove;

            _playerPickAction.performed += OnPlayerPick;
            _playerThrowAction.performed += OnPlayerThrow;
            _playerDashAction.performed += OnPlayerDash;

            _playerMoveAction.Enable();
        }
    }
}
