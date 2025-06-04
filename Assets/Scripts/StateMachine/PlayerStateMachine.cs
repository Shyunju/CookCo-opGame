using UnityEngine;

namespace CookCo_opGame
{
    public class PlayerStateMachine : StateMachine
    {
        public PlayerManager PlayerManager { get; }
        public Vector2 MovementInput { get; set; }
        public float MovementSpeed { get; private set; }
        public float RotateDamping { get; private set; }
        public float MovementSpeedModifier { get; set; } = 1f;

        public PlayerStateMachine(PlayerManager player)
        {
            this.PlayerManager = player;
            MovementSpeed = player.PlayerOS.DefaultData.BaseSpeed;
            RotateDamping = player.PlayerOS.DefaultData.BaseRotationDamping;
        }
    }
}
