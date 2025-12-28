using DG.Tweening;
using UnityEngine;
using Patterns;
using System.Collections.Generic;

namespace Enemy.States
{
    public class BossAttackPatternState : State
    {
        private BossController boss;
        private List<BossProjectile> activeProjectiles;
        private bool isSpawning;

        public BossAttackPatternState(BossController boss, float duration) : base(boss)
        {
            this.boss = boss;
            this.activeProjectiles = new List<BossProjectile>();
        }

        public override void Enter()
        {
            Debug.Log("Boss Enter Attack State (Invulnerable)");
            boss.SetInvulnerable(true);
            isSpawning = true;
            activeProjectiles.Clear();
            
            SpawnProjectiles();
        }

        private void SpawnProjectiles()
        {
            if (boss.ProjectilePrefab == null || boss.PlayerTarget == null)
            {
                Debug.LogWarning("BossController missing ProjectilePrefab or PlayerTarget!");
                isSpawning = false;
                return;
            }

            int count = 3;
            // Spawn 3 projectiles sequentially
            for (int i = 0; i < count; i++)
            {
                int index = i; // Capture index for closure
                float delay = i * 1.5f; 

                DOVirtual.DelayedCall(delay, () =>
                {
                    if (boss == null) return;
                    
                    // Calculate offset positions
                    Vector3 offset = new Vector3((index - 1) * 2f, 2f, 0f); 
                    // Simple offset relative to world
                    Vector3 spawnPos = boss.transform.position + boss.transform.forward * 2f + offset;

                    // Instantiate
                    BossProjectile proj = Object.Instantiate(boss.ProjectilePrefab, spawnPos, Quaternion.identity);
                    proj.Spawn(proj.transform, boss.PlayerTarget);
                    activeProjectiles.Add(proj);
                    
                    if (index == count - 1) isSpawning = false;
                });
            }
        }


        public override void Update()
        {
            // Clean list
            for (int i = activeProjectiles.Count - 1; i >= 0; i--)
            {
                if (activeProjectiles[i] == null) activeProjectiles.RemoveAt(i);
            }
            
            // Only exit if spawning is done AND all projectiles are destroyed
            if (!isSpawning && activeProjectiles.Count == 0)
            {
                stateMachine.ChangeState(boss.IdleState);
            }
        }

        public override void FixedUpdate()
        {
            // Physics update for attacks if necessary
        }

        public override void Exit()
        {
            Debug.Log("Boss Attack Pattern Finished");
        }
    }
}
