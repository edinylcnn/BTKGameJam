using UnityEngine;

namespace Patterns
{
    public abstract class State
    {
        protected StateMachine stateMachine;

        public State(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public abstract void Enter();
        public abstract void Update();
        public abstract void FixedUpdate();
        public abstract void Exit();
    }
}
