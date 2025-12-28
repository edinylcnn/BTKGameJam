using UnityEngine;
using Patterns;

namespace Enemy.States
{
    public class BossIdleState : State
    {
        private BossController boss;
        private float duration;
        private float timer;

        public BossIdleState(BossController boss, float duration) : base(boss)
        {
            this.boss = boss;
            this.duration = duration;
        }

        public override void Enter()
        {
            Debug.Log("Boss Enter Idle State (Vulnerable)");
            boss.SetInvulnerable(false);
            timer = 0f;
        }

        public override void Update()
        {
            timer += Time.deltaTime;
            if (timer >= duration)
            {
                stateMachine.ChangeState(boss.AttackState);
            }
        }

        public override void FixedUpdate()
        {
            // No physics logic needed for simplified idle
        }

        public override void Exit()
        {
            // Pickup needed cleanup
        }
    }
}
