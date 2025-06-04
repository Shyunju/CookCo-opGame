using UnityEngine;

namespace CookCo_opGame
{
    public class PlayerBaseState : IState
    {
        protected PlayerStateMachine _stateMachine;
        protected readonly PlayerDefaultData _defaultData;

        public PlayerBaseState(PlayerStateMachine stateMachine)
        {
            this._stateMachine = stateMachine;
            _defaultData = stateMachine.PlayerManager.PlayerOS.DefaultData;
        }
        public virtual void Enter()
        {

        }

        public virtual void Exit()
        {
        }

        public virtual void HandleInput()
        {
            ReadMovementInput();
        }

        public virtual void PhysicsUpdate()
        {
        }

        public virtual void Update()
        {
        }
        protected void StartAnimation(int animatorHash)
        {
            Debug.Log(animatorHash);
            _stateMachine.PlayerManager.Animator.SetBool(animatorHash, true);
            // Debug.Log("animatorHash");
        }
        protected void StopAnimation(int animatorHash)
        {
            _stateMachine.PlayerManager.Animator.SetBool(animatorHash, false);
        }

        private void ReadMovementInput()
        {
            _stateMachine.MovementInput = _stateMachine.PlayerManager.PlayerController.Input;
        }
    }
}
