using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace CookCo_opGame
{
    public class Player1InputController : PlayerController
    {
        [SerializeField] InputActionAsset _playerInputActions;
        InputActionMap _player1Map;
        InputAction _player1MoveAction;
        public override void OnEnable()
        {
            _player1Map = _playerInputActions.FindActionMap("Player1Actions");
            _player1Map.Enable();

            _player1MoveAction = _player1Map.FindAction("Move");

            //_player1MoveAction.performed += OnPlayer1Move;

            _player1MoveAction.Enable();
        }


    }
}
