using System.Collections.Generic;
using Game.Core;
using Game.Scripts.Combat;
using Game.Scripts.Core;
using UnityEngine;

namespace Game.Scripts.Enemies
{
    public class EnemyManager : Singleton<EnemyManager>
    {
        [SerializeField] private HealthSystem player;
        private readonly List<Enemy> enemies = new List<Enemy>();

        public void Join(Enemy enemy)
        {
            enemies.Add(enemy);
            enemy.SetPlayer(player);
        }

        public void Leave(Enemy enemy)
        {
            enemies.Remove(enemy);
            EnemyBasicPool.Instance.PullObjectBackImmediate(enemy);

            if (enemies.Count == 0)
            {
                GameManager.ActionLevelPassed?.Invoke();
            }
        }
    }
}
