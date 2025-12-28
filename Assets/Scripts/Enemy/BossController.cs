using UnityEngine;
using Patterns;
using Enemy.States;

namespace Enemy
{
    public class BossController : StateMachine
    {
        [Header("Boss Settings")]
        public float MaxHealth = 100f;
        public float AttackInterval = 5f;
        public float VulnerableDuration = 3f;
        public BossProjectile ProjectilePrefab; // Reference to the prefab
        public Transform PlayerTarget; // Reference to the player

        [Header("Debug")]
        [SerializeField] private float currentHealth;
        [SerializeField] private bool isInvulnerable;

        // States
        public BossIdleState IdleState { get; private set; }
        public BossAttackPatternState AttackState { get; private set; }

        private void Start()
        {
            currentHealth = MaxHealth;
            
            // Initialize States
            IdleState = new BossIdleState(this, VulnerableDuration);
            AttackState = new BossAttackPatternState(this, AttackInterval);

            // Start in Idle
            ChangeState(IdleState);
        }

        public void TakeDamage(float amount)
        {
            if (isInvulnerable)
            {
                Debug.Log("Boss is Invulnerable! Attack blocked.");
                return;
            }

            currentHealth -= amount;
            Debug.Log($"Boss took {amount} damage. Current Health: {currentHealth}");

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        public void SetInvulnerable(bool state)
        {
            isInvulnerable = state;
        }

        public event System.Action OnDeath;

        private void Die()
        {
            Debug.Log("Boss Defeated!");
            // Add death logic here (animation, particles, etc.)
            
            OnDeath?.Invoke();
            
            gameObject.SetActive(false);
        }

        private void OnMouseDown()
        {
            // Simple click interaction for testing/gameplay
            // Assumes the Boss has a Collider
            TakeDamage(10f); // Arbitrary click damage
        }
    }
}
