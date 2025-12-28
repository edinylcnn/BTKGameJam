using UnityEngine;
using DG.Tweening;

namespace Enemy
{
    public class BossProjectile : MonoBehaviour
    {
        [Header("Settings")]
        public float LaunchDelay = 1f;
        public float MoveSpeed = 20f;
        public float Damage = 10f;
        public float SpawnDuration = 0.5f;
        public float Health = 30f; // New Health variable
        public float SpawnJumpPower = 2f; // Power of the jump

        private Transform target;
        private bool isLaunched;
        private Tween delayedLaunchTween;
        private Collider col;
        private float currentHealth;

        /// <summary>
        /// Initialize the projectile.
        /// </summary>
        public void Spawn(Transform spawnPoint, Transform targetPlayer)
        {
            this.target = targetPlayer;
            transform.position = spawnPoint.position;
            transform.localScale = Vector3.zero;
            col = GetComponent<Collider>();
            currentHealth = Health; // Reset health

            // DOTween Animation: Scale up + Jump effect
            Sequence spawnSeq = DOTween.Sequence();
            
            spawnSeq.Join(transform.DOScale(Vector3.one, SpawnDuration).SetEase(Ease.OutBack));
            spawnSeq.Join(transform.DOJump(transform.position, SpawnJumpPower, 1, SpawnDuration));
            
            spawnSeq.OnComplete(() =>
            {
                // Wait for LaunchDelay then launch
                delayedLaunchTween = DOVirtual.DelayedCall(LaunchDelay, Launch);
            });
        }

        /// <summary>
        /// Launches the projectile towards the player.
        /// </summary>
        private void Launch()
        {
            if (target == null)
            {
                // Target lost/destroyed, destroy projectile immediately to avoid hanging
                Destroy(gameObject);
                return;
            }
            
            isLaunched = true;
            
            // Calculate travel time based on speed and distance
            float distance = Vector3.Distance(transform.position, target.position);
            float travelTime = distance / MoveSpeed;

            // Move towards target over time
            transform.DOMove(target.position, travelTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                // Logic when it hits the target (player)
                if (target != null)
                {
                    var player = target.GetComponent<PlayerController>();
                    if (player != null)
                    {
                        player.TakeDamage(Damage);
                    }
                }
                Debug.Log("Projectile hit player!");
                Destroy(gameObject);
            });
        }


        /// <summary>
        /// Player clicks the projectile to damage it.
        /// </summary>
        private void OnMouseDown()
        {
            if (isLaunched) return; 
            
            TakeDamage(10f); // Default click damage
        }

        private void TakeDamage(float amount)
        {
            currentHealth -= amount;
            
            // Optional: Shake effect on damage
            transform.DOShakeScale(0.1f, 0.2f);

            if (currentHealth <= 0)
            {
                Dismiss();
            }
        }

        private void Dismiss()
        {
            // Cancel the launch if it hasn't happened yet
            if (delayedLaunchTween != null) delayedLaunchTween.Kill();
            
            // Animate disappearance
            col.enabled = false; // Prevent double clicks
            transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }
    }
}
