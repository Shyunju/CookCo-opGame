using UnityEngine;

namespace CookCo_opGame
{
    public class IdleState : PlayerDefaultState
    {
        public IdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            StartAnimation(_stateMachine.PlayerManager.PlayerAnimationData.IdleParameterHash);
        }

        public override void Exit()
        {
            //base.Exit();
            StopAnimation(_stateMachine.PlayerManager.PlayerAnimationData.IdleParameterHash);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
