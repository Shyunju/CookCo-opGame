using UnityEngine;

namespace CookCo_opGame
{
    public class RunState : PlayerDefaultState
    {
        public RunState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {

        }

        public override void Enter()
        {
            StartAnimation(_stateMachine.PlayerManager.PlayerAnimationData.RunParameterHash);
        }

        public override void Exit()
        {
            StopAnimation(_stateMachine.PlayerManager.PlayerAnimationData.RunParameterHash);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
