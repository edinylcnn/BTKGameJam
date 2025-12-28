using UnityEngine;

namespace Patterns
{
    public abstract class StateMachine : MonoBehaviour
    {
        public State CurrentState { get; protected set; }

        protected virtual void Update()
        {
            CurrentState?.Update();
        }

        protected virtual void FixedUpdate()
        {
            CurrentState?.FixedUpdate();
        }

        public void ChangeState(State newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
        }
    }
}
