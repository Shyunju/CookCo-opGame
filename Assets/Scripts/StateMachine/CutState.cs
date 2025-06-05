using UnityEngine;

namespace CookCo_opGame
{
    public class CutState : PlayerCookState
    {
        public CutState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();
            StartAnimation(_stateMachine.PlayerManager.PlayerAnimationData.CutParameterHash);
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(_stateMachine.PlayerManager.PlayerAnimationData.CutParameterHash);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
