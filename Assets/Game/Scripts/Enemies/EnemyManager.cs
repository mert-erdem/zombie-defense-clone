using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Combat;
using Game.Scripts.Core;
using Game.Scripts.Core.UI;
using UnityEngine;

namespace Game.Scripts.Enemies
{
    public class EnemyManager : Singleton<EnemyManager>
    {
        [SerializeField] private HealthSystem player;
        private readonly List<Enemy> enemies = new List<Enemy>();
        private readonly List<HordeSpawner> hordeSpawners = new List<HordeSpawner>();
        private int totalEnemyCount = 0;


        private void Start()
        {
            GameManager.ActionLevelStart += GameManager_ActionLevelStart;
            GameManager.ActionGameOverPost += GameManager_ActionGameOverPost;
        }
        
        public void Join(Enemy enemy)
        {
            enemies.Add(enemy);
            enemy.SetPlayer(player);
        }

        public void Leave(Enemy enemy)
        {
            enemies.Remove(enemy);
            EnemyBasicPool.Instance.PullObjectBack(enemy);
            // update UI
            var levelProgress = (float) (totalEnemyCount - enemies.Count) / totalEnemyCount;
            CanvasController.Instance.SetLevelProgressBar(levelProgress);
            
            if (enemies.Count == 0)
            {
                GameManager.ActionLevelPassed?.Invoke();
            }
        }
        
        public void JoinHordeSpawner(HordeSpawner hordeSpawner)
        {
            hordeSpawners.Add(hordeSpawner);
        }
        
        private void GameManager_ActionLevelStart()
        {
            int newEnemyCount = hordeSpawners.Sum(hordeSpawner => hordeSpawner.TotalSpawnCount);

            totalEnemyCount = newEnemyCount;
        }
        
        private void GameManager_ActionGameOverPost()
        {
            foreach (var enemy in enemies)
            {
                EnemyBasicPool.Instance.PullObjectBackImmediate(enemy);
            }
        }

        private void OnDestroy()
        {
            GameManager.ActionLevelStart -= GameManager_ActionLevelStart;
            GameManager.ActionGameOverPost -= GameManager_ActionGameOverPost;
        }
    }
}
