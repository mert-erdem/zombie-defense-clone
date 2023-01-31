using UnityEngine;

namespace Game.Scripts.Combat
{
    public class BulletProjectile : MonoBehaviour
    {
        [SerializeField] private float speed = 100f;
        public Vector3 TargetPos { private get; set; }

        private void Update()
        {
            if (TargetPos == default) return;

            Move();
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                TargetPos,
                Time.deltaTime * speed);

            if(transform.position == TargetPos)
            {
                BulletProjectilePool.Instance.PullObjectBackImmediate(this);
            }
        }
    }
}
