using UnityEngine;

namespace CookCo_opGame
{
    public class PlayerStateMachine : StateMachine
    {
        public PlayerManager PlayerManager { get; }
        public IdleState IdleState { get; }
        public WalkState WalkState { get; }
        public RunState RunState { get; }
        public CutState CutState { get; }

        public Vector2 MovementInput { get; set; }
        public float MovementSpeed { get; private set; }
        public float RotateDamping { get; private set; }
        public float MovementSpeedModifier { get; set; } = 1f;

        public PlayerStateMachine(PlayerManager player)
        {
            this.PlayerManager = player;

            IdleState = new IdleState(this);
            WalkState = new WalkState(this);
            RunState = new RunState(this);
            CutState = new CutState(this);

            MovementSpeed = player.PlayerOS.DefaultData.BaseSpeed;
            RotateDamping = player.PlayerOS.DefaultData.BaseRotationDamping;
        }
    }
}
