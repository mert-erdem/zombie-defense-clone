using Game.Scripts.Combat;
using UnityEngine;

namespace Game.Scripts.Enemies
{
    public class HordeSpawner : MonoBehaviour
    {
        [SerializeField] private HealthSystem targetDoor;
        [SerializeField] private Enemy enemyPrefab;
        [Header("Specs")]
        [SerializeField] private float spawnRate = 1f;
        [SerializeField] private int spawnCount = 10;

        private float lastTimeSpawned = Mathf.Infinity;

        private void Update()
        {
            if(spawnCount <= 0) return;
        
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            lastTimeSpawned += Time.deltaTime;
        
            if (lastTimeSpawned <= spawnRate) return;
            // will be replaced with object pool pattern
            var spawnedEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            spawnedEnemy.SetTarget(targetDoor);
            spawnCount--;
            lastTimeSpawned = 0f;
        }
    }
}
