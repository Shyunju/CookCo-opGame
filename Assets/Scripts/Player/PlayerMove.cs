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
        public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value;}}
        private float _defaultSpeed = 6f;
        public float DefaultSpeed { get { return _defaultSpeed; }}
        private float _dashSpeed = 16f;
        public float DashSpeed { get { return _dashSpeed; }}
        private Rigidbody _playerRigidBody;
        private float _rotationSpeed = 15f;
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
        }

        
    }
}
