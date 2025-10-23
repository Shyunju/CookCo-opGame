namespace CookCo_opGame
{
    public class WalkState : PlayerDefaultState
    {
        public WalkState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {}

        public override void Enter()
        {
            StartAnimation(_stateMachine.PlayerManager.PlayerAnimationData.WalkParameterHash);
        }

        public override void Exit()
        {
            StopAnimation(_stateMachine.PlayerManager.PlayerAnimationData.WalkParameterHash);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
