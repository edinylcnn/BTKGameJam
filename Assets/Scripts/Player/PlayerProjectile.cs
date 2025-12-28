using UnityEngine;

namespace Player
{
    public class PlayerProjectile : MonoBehaviour
    {
        [Header("Projectile Settings")]
        [SerializeField] private float speed = 20f;
        [SerializeField] private float lifeTime = 3f;
        [SerializeField] private float damage = 10f; // Default, can be overridden by shooter

        private Vector3 direction;
        private bool isInitialized = false;

        public void Initialize(Vector3 dir, float dmg)
        {
            direction = dir.normalized;
            damage = dmg;
            isInitialized = true;
            Destroy(gameObject, lifeTime); // Auto-destroy after lifetime
        }

        private void Update()
        {
            if (!isInitialized) return;

            // Move projectile
            transform.position += direction * speed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            // Logic to damage enemies
            // Assuming we have an IDamageable interface or specific Enemy scripts
            // For now, let's look for BossController or generic IDamageable
            
            var damageable = other.GetComponent<Enemy.BossController>();
            
            if (damageable != null)
            {
               damageable.TakeDamage(damage); 
            }

            
            // If it hits something that isn't the player, destroy.
            if (!other.CompareTag("Player")) 
            {
                Destroy(gameObject);
            }
        }
    }
}
