using UnityEngine;

namespace CookCo_opGame
{
    public class PlayerCookState : PlayerBaseState
    {
        public PlayerCookState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            StartAnimation(_stateMachine.PlayerManager.PlayerAnimationData.CookParameterHash);
        }

        public override void Exit()
        {
            StopAnimation(_stateMachine.PlayerManager.PlayerAnimationData.CookParameterHash);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
