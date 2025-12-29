using UnityEngine;
using UnityEngine.UI;
using Patterns;
using DamageNumbersPro;
using Enemy.States;

namespace Enemy
{
    public class BossController : StateMachine
    {
        [Header("Boss Settings")]
        public float MaxHealth = 100f;
        public float AttackInterval = 5f;
        public float VulnerableDuration = 3f;
        public float InitialIdleDuration = 6f; // New initial idle duration
        public BossProjectile ProjectilePrefab; // Reference to the prefab
        public DamageNumber damageNumberPrefab; // Reference to the DamageNumberPro prefab
        public Image healthBarImage; // Reference to the Health Bar Image
        public Transform PlayerTarget; // Reference to the player

        [Header("Debug")]
        [SerializeField] private float currentHealth;
        [SerializeField] private bool isInvulnerable;

        // States
        public BossIdleState IdleState { get; private set; }
        public BossIdleState InitialIdleState { get; private set; } // Separate state for initial wait
        public BossAttackPatternState AttackState { get; private set; }

        private void Start()
        {
            if (PlayerTarget == null)
            {
                var player = FindObjectOfType<PlayerController>();
                if (player != null) PlayerTarget = player.transform;
            }

            currentHealth = MaxHealth;
            
            // Initialize States
            IdleState = new BossIdleState(this, VulnerableDuration);
            InitialIdleState = new BossIdleState(this, InitialIdleDuration);
            AttackState = new BossAttackPatternState(this, AttackInterval);

            // Start in Initial Idle
            ChangeState(InitialIdleState);
        }

        public void TakeDamage(float amount)
        {
            if (isInvulnerable)
            {
                if (damageNumberPrefab != null)
                {
                    DamageNumber dn = damageNumberPrefab.Spawn(transform.position + Vector3.up * 2f, "Miss");
                    dn.enableNumber = false;
                }
                Debug.Log("Boss is Invulnerable! Attack blocked.");
                return;
            }

            currentHealth -= amount;

            if (healthBarImage != null)
            {
                healthBarImage.fillAmount = currentHealth / MaxHealth;
                // Debug.Log($"Health Bar updated. Fill Amount: {healthBarImage.fillAmount}");
            }
            else
            {
                Debug.LogWarning("Health Bar Image is not assigned in BossController!");
            }

            if (damageNumberPrefab != null)
            {
                damageNumberPrefab.Spawn(transform.position + Vector3.up * 2f, amount);
            }

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
