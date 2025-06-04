using System.Collections;
using UnityEngine;

namespace CookCo_opGame
{
    public class PlayerMove : MonoBehaviour
    {
        private PlayerManager _playerManager;
        private Vector3 _moveDirection;
        public Vector3 MoveDirection { get { return _moveDirection; } set { _moveDirection = value; } }
        private float _moveSpeed;
        private float _defaultSpeed = 6f;
        private float _dashSpeed = 18f;
        private Rigidbody _playerRigidBody;
        private float _rotationSpeed = 8f;
        void Start()
        {
            _playerRigidBody = GetComponent<Rigidbody>();
            _playerManager = GetComponent<PlayerManager>();
            _moveSpeed = _defaultSpeed;
        }
        public void MoveCharacter()
        {
            if (_moveDirection != Vector3.zero)
            {
                _playerManager.StateMachine.ChaingeState(_playerManager.StateMachine.WalkState);
                Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    _rotationSpeed * Time.deltaTime
                );
                transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
                Vector3 movement = _moveDirection.normalized * _moveSpeed * Time.fixedDeltaTime;
                _playerRigidBody.MovePosition(_playerRigidBody.position + movement);
            }
            else
            {                
                _playerManager.StateMachine.ChaingeState(_playerManager.StateMachine.IdleState);
            }
        }

        public IEnumerator DashMoveCo()
        {
            _moveSpeed = _dashSpeed;
            yield return new WaitForSeconds(0.2f);
            _moveSpeed = _defaultSpeed;
        }
    }
}
