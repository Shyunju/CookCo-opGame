using UnityEngine;

namespace CookCo_opGame
{
    public class PlayerMove : MonoBehaviour
    {
        private Vector3 _moveDirection;
        public Vector3 MoveDirection {get { return _moveDirection; }set { _moveDirection = value; } }
        private float _moveSpeed = 6f;
        private Rigidbody _playerRigidBody;
        private float _rotationSpeed = 8f;
        void Start()
        {
            _playerRigidBody = GetComponent<Rigidbody>();
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
