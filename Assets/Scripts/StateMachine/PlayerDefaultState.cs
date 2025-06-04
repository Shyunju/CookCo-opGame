using UnityEngine;

namespace CookCo_opGame
{
    public class PlayerDefaultState : PlayerBaseState
    {
        public PlayerDefaultState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            StartAnimation(_stateMachine.PlayerManager.PlayerAnimationData.DefaultParameterHash);
        }

        public override void Exit()
        {
            StopAnimation(_stateMachine.PlayerManager.PlayerAnimationData.DefaultParameterHash);
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
