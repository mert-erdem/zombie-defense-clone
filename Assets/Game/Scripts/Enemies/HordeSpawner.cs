using System;
using Game.Core;
using Game.Scripts.Combat;
using Game.Scripts.Core;
using Game.Scripts.Environment;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Enemies
{
    public class HordeSpawner : MonoBehaviour
    {
        [SerializeField] private HealthSystem targetDoor;
        [SerializeField] private Enemy enemyPrefab;
        [Header("Specs")]
        [SerializeField] private float spawnRate = 1f;
        [SerializeField] private int spawnCount = 10;
        [SerializeField] private int spawnCountLevelDelta = 2;

        public int TotalSpawnCount => spawnCount;
        
        private int currentSpawnCount = 0;
        private float lastTimeSpawned = Mathf.Infinity;
        private bool canSpawn = false;

        private void Start()
        {
            GameManager.ActionLevelStart += GameManager_ActionLevelStart;
            GameManager.ActionLevelPassed += GameManager_ActionLevelPassed;
            
            EnemyManager.Instance.JoinHordeSpawner(this);
            SetSpawnCountViaLevel();
        }

        private void Update()
        {
            if (!canSpawn) return;
            
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            lastTimeSpawned += Time.deltaTime;
        
            if (lastTimeSpawned <= spawnRate) return;
            
            // will be replaced with object pool pattern
            var spawnedEnemy = EnemyBasicPool.Instance.GetObject();
            spawnedEnemy.transform.SetPositionAndRotation(transform.position, transform.rotation);
            spawnedEnemy.OnSpawn(targetDoor);
            currentSpawnCount++;
            lastTimeSpawned = 0f;

            if (currentSpawnCount != spawnCount) return;
            
            canSpawn = false;
            currentSpawnCount = 0;
            lastTimeSpawned = Mathf.Infinity;
        }
        
        private void GameManager_ActionLevelStart()
        {
            canSpawn = true;
        }
        
        private void GameManager_ActionLevelPassed()
        {
            spawnCount += spawnCountLevelDelta;
        }

        private void SetSpawnCountViaLevel()
        {
            var currentLevel = AreaManager.Instance.CurrentLevel;
            spawnCount = spawnCountLevelDelta * currentLevel - 1;
        }

        private void OnDestroy()
        {
            GameManager.ActionLevelStart -= GameManager_ActionLevelStart;
            GameManager.ActionLevelPassed -= GameManager_ActionLevelPassed;
        }
    }
}
