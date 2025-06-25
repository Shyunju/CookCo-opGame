using UnityEngine;

namespace CookCo_opGame
{
    public class PlayerWashState : PlayerBaseState
    {
        public PlayerWashState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            StartAnimation(_stateMachine.PlayerManager.PlayerAnimationData.WaterParameterHash);
        }

        public override void Exit()
        {
            StopAnimation(_stateMachine.PlayerManager.PlayerAnimationData.WaterParameterHash);
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
