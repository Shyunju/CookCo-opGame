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
            StartAnimation(_stateMachine.PlayerManager.PlayerAnimationData.CutParameterHash);
            Debug.Log("cutting");
        }

        public override void Exit()
        {
            //base.Exit();
            StopAnimation(_stateMachine.PlayerManager.PlayerAnimationData.CutParameterHash);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
