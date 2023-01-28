using Game.ScriptableObjects;
using UnityEngine;

namespace Game.Scripts.Combat
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform muzzle, bullet;
        [SerializeField] private WeaponSO specs;
    
        public WeaponSO Specs => specs;

        public void LaunchBullet(Vector3 targetPos)
        {
            // launch specific bullet trail object to target
            // using object pool
            var spawnedBullet = BulletProjectilePool.Instance.GetObject();
            spawnedBullet.transform.position = muzzle.position;
            var bulletProjectile = spawnedBullet.GetComponent<BulletProjectile>();
            bulletProjectile.TargetPos = targetPos;
        }

        public void UpgradeDamage(int amount)
        {
            specs.damage += amount;
        }

        public void UpgradeFireRate(int amount)
        {
            specs.fireRate += amount;
        }
    }
}
