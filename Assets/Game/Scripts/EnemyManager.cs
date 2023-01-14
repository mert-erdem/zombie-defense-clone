using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Combat;
using Game.Scripts.Core;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] private HealthSystem player;
    private List<Enemy> enemies = new List<Enemy>();

    public void Join(Enemy enemy)
    {
        enemies.Add(enemy);
        enemy.SetPlayer(player);
    }

    public void Leave(Enemy enemy)
    {
        enemies.Remove(enemy);
    }
}
