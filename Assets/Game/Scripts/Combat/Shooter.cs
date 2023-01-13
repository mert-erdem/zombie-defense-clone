using Game.Scripts.Core;
using UnityEngine;

namespace Game.Scripts.Combat
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private Transform visual;
        // enemy searching
        private LayerMask searchMask;
        private Collider[] searchBuffer = new Collider[1];
        private HealthSystem target;
        // component specs
        [Header("Component Specs")]
        [SerializeField] private float rotatingSpeed = 10f;
        // weapon
        [SerializeField] private Weapon currentWeapon;
        private float lastFiredTime = Mathf.Infinity;
        // states
        private State stateCurrent, stateSearching, stateShooting;
    
        public bool IsShooting { get; private set; }

        private void Start()
        {
            searchMask = LayerMask.GetMask("Combat");
            stateCurrent = new State(null, null, null);
            stateSearching = new State(null, SearchForEnemy, null);
            stateShooting = new State(null, Shoot, null);
            // start butonuna bastıktan sonra search e geçmeli
            SetState(stateSearching);
        }

        private void Update()
        {
            stateCurrent.onUpdate();
        }
    
        private void SetState(State state)
        {
            stateCurrent?.onStateExit();
            stateCurrent = state;
            stateCurrent.onStateEnter();
        }

        private void SearchForEnemy()
        {
            var resultCount = Physics.OverlapSphereNonAlloc(
                transform.position, currentWeapon.Specs.range, searchBuffer, searchMask);
        
            if (resultCount == 0) return;
        
            target = searchBuffer[0].GetComponent<HealthSystem>();
            SetState(stateShooting);
            IsShooting = true;
        }

        private void Shoot()
        {
            if (!IsTargetStillInRange() || !IsTargetAlive())
            {
                SetState(stateSearching);
                IsShooting = false;
                return;
            }
        
            var direction = (target.transform.position - transform.position).normalized;
            visual.forward = Vector3.Slerp(
                visual.forward,
                direction,
                Time.deltaTime * rotatingSpeed);
        
            lastFiredTime += Time.deltaTime;

            if (!(lastFiredTime >= currentWeapon.Specs.fireRate)) return;
        
            target.TakeDamage(currentWeapon.Specs.damage);

            lastFiredTime = 0;
        }

        private bool IsTargetStillInRange()
        {
            var distance = Vector3.Distance(transform.position, target.transform.position);

            return distance <= currentWeapon.Specs.range;
        }

        private bool IsTargetAlive()
        {
            return target.IsAlive;
        }
    }
}
