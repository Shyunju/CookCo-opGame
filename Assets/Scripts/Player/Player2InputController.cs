using UnityEngine;
using UnityEngine.InputSystem;

namespace CookCo_opGame
{
    public class Player2InputController : PlayerController
    {
        [SerializeField] InputActionAsset _playerInputActions;
        InputActionMap _player2Map;
        InputAction _player2MoveAction;
        public override void OnEnable()
        {
            _player2Map = _playerInputActions.FindActionMap("Player2Actions");
            _player2Map.Enable();

            _player2MoveAction = _player2Map.FindAction("Move");
            _player2MoveAction = _player2Map.FindAction("Pick");
            _player2MoveAction = _player2Map.FindAction("Dash");
            _player2MoveAction = _player2Map.FindAction("Throw");


            _player2MoveAction.performed += OnPlayerMove;
            _player2MoveAction.canceled += OnPlayerMove;

            _player2MoveAction.Enable();
        }
    }
}
