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
        public void Enter()
        {

        }

        public void Exit()
        {
        }

        public void HandleInput()
        {
            ReadMovementInput();
        }

        public void PhysicsUpdate()
        {
        }

        public void Update()
        {
        }
        protected void StartAimation(int animatorHash)
        {
            _stateMachine.PlayerManager.Animator.SetBool(animatorHash, true);
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
