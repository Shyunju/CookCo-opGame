using System;
using UnityEngine;

namespace CookCo_opGame
{
    public interface IState
    {
        public void Enter();
        public void Exit();
        public void HandleInput();
        public void Update();
        public void PhysicsUpdate();
    }
    public abstract class StateMachine
    {

    }
}
