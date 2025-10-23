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
            _stateMachine.PlayerManager.PlayerKnife.SetActive(true);
            StartAnimation(_stateMachine.PlayerManager.PlayerAnimationData.CutParameterHash);
        }

        public override void Exit()
        {
            base.Exit();
            _stateMachine.PlayerManager.PlayerKnife.SetActive(false);
            StopAnimation(_stateMachine.PlayerManager.PlayerAnimationData.CutParameterHash);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
