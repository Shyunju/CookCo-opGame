namespace CookCo_opGame
{
    public class WashState : PlayerWashState
    {
        public WashState(PlayerStateMachine stateMachine) : base(stateMachine)
        {}
        public override void Enter()
        {
            base.Enter();
            StartAnimation(_stateMachine.PlayerManager.PlayerAnimationData.WashParameterHash);
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(_stateMachine.PlayerManager.PlayerAnimationData.WashParameterHash);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
