using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Enemy;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private PlayerController player;
        [SerializeField] private actionManager actionManager;

        [SerializeField] private ElevatorController elevator;
        [SerializeField] private Transform elevatorStartPoint;
        [SerializeField] private Transform arenaEntryPoint;
        [SerializeField] private Transform sceneSpawnPoint;

        [Header("Prefabs")] [SerializeField] private List<GameObject> scenePrefabs;

        [Header("Settings")] [SerializeField] private float startDelay = 1f;
        [SerializeField] private float waitAfterClear = 2f;
        [SerializeField] private float playerMoveDuration = 2f;
        [SerializeField] private float doorAnimationDuration = 1f; // Slightly more than 0.6s to be safe

        private void Start()
        {
            if (scenePrefabs == null || scenePrefabs.Count == 0)
            {
                Debug.LogError("No scene prefabs assigned in GameManager!");
                return;
            }
            StartCoroutine(GameLoop());
        }

        private IEnumerator GameLoop()
        {
            // Initial delay
            yield return new WaitForSeconds(startDelay);

            while (true)
            {
                // 1. Instantiate Random Level (Before Door Opens)
                GameObject currentScene = Instantiate(scenePrefabs[Random.Range(0, scenePrefabs.Count)],
                    sceneSpawnPoint.position, sceneSpawnPoint.rotation);

                // 2. Open Elevator
                elevator.OpenDoor();
                yield return new WaitForSeconds(doorAnimationDuration);

                // 3. Move Player to Arena (Optional: User didn't strictly ask for this but it was in previous code)
                player.transform.DOMove(arenaEntryPoint.position, playerMoveDuration).SetEase(Ease.Linear);
                yield return new WaitForSeconds(playerMoveDuration);

                // 4. Find Boss in the new scene and wait for death (Clear Condition)
                BossController boss = currentScene.GetComponentInChildren<BossController>();

                if (boss != null)
                {
                    bool bossDead = false;
                    System.Action onDeathHandler = () => bossDead = true;
                    boss.OnDeath += onDeathHandler;

                    // Wait until boss is dead
                    yield return new WaitUntil(() => bossDead);

                    boss.OnDeath -= onDeathHandler;
                }
                else
                {
                    Debug.LogWarning("No BossController found in the instantiated scene! Passing level automatically.");
                    yield return new WaitForSeconds(3f); // Fallback wait
                }

                // 5. Wait a bit after clear
                yield return new WaitForSeconds(waitAfterClear);

                // 6. Return Player to Elevator
                player.transform.DOMove(elevatorStartPoint.position, playerMoveDuration).SetEase(Ease.Linear);
                yield return new WaitForSeconds(playerMoveDuration);

                // 7. Show Upgrade Cards and Wait for Selection
                if (actionManager != null)
                {
                    player.enabled = false; // Disable player control
                    actionManager.OnWallTriggered();
                    yield return new WaitUntil(() => !actionManager.IsSelectionActive);
                    player.enabled = true; // Re-enable player control
                }
                else
                {
                    Debug.LogWarning("ActionManager reference is missing in GameManager!");
                }

                // 8. Close Elevator Door
                elevator.CloseDoor();
                yield return new WaitForSeconds(doorAnimationDuration); // Wait for close animation

                // 9. Destroy Old Scene (After Door Closes)
                Destroy(currentScene);

                // 10. Wait before next level (Simulation of elevator moving)
                yield return new WaitForSeconds(2f);
                
                // Loop repeats: Instantiate -> Open...
            }
        }
    }
}