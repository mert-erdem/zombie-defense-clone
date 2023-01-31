using System;
using System.Collections;
using Game.Scripts.Combat;
using Game.Scripts.Core;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Scripts.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private HealthSystem healthSystem;
        [SerializeField] private Animator animator;
        [SerializeField] private SkinnedMeshRenderer meshRenderer;
        [Header("Specs")]
        [SerializeField] private float attackRange = 3f;
        [SerializeField] private int damage = 10;
        [SerializeField] private float attackDeltaTime = 1f;
        [Header("Effects")] 
        [SerializeField] private Material materialDefault;
        [SerializeField] private Material materialDeath;
    
        private HealthSystem target, player;
        private float lastTimeAttack = Mathf.Infinity;
        private State stateCurrent, stateChase, stateAttack, stateDeath;

        private void Awake()
        {
            stateDeath = new State(null, null, null);
            stateChase = new State(StartAgent, Move, StopAgent);
            stateAttack = new State(OnAttackStateStart, HandleWithAttack, OnAttackStateEnd);
        }

        private void Start()
        {
            healthSystem.OnDie += HealthSystem_OnDie;
        }

        private void OnEnable()
        {
            EnemyManager.Instance.Join(this);
        }

        private void Update()
        {
            stateCurrent.onUpdate();
        }

        public void SetTarget(HealthSystem target)
        {
            this.target = target;
            SetState(stateChase);
        }

        public void SetPlayer(HealthSystem player)
        {
            this.player = player;
        }

        private void Move()
        {
            agent.SetDestination(target.transform.position);
        
            if (!IsTargetInRange()) return;
            
            SetState(stateAttack);
        }

        private bool IsTargetInRange()
        {
            var distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        
            return distanceToTarget <= attackRange;
        }

        private void StopAgent()
        {
            agent.isStopped = true;
        }

        private void StartAgent()
        {
            agent.isStopped = false;
        }

        private void HandleWithAttack()
        {
            lastTimeAttack += Time.deltaTime;

            if (lastTimeAttack <= attackDeltaTime) return;
        
            Attack();
            lastTimeAttack = 0f;
        }

        private void Attack()
        {
            // player is escaping
            if (!IsTargetInRange())
            {
                SetState(stateChase);
            }

            if (target.IsAlive)
            {
                bool isDead = target.TakeDamage(damage);
                animator.SetTrigger("Punch");
                
                if (isDead)
                    SetTarget(player);
            }
            else
            {
                SetTarget(player);
            }
        }

        private void SetState(State state)
        {
            stateCurrent?.onStateExit();
            stateCurrent = state;
            stateCurrent.onStateEnter();
        }

        private void OnAttackStateStart()
        {
            animator.SetBool("Attacking", true);
        }

        private void OnAttackStateEnd()
        {
            animator.SetBool("Attacking", false);
        }

        private void HealthSystem_OnDie(object sender, EventArgs eventArgs)
        {
            StartCoroutine(PerformDeathSequence());
        }

        private IEnumerator PerformDeathSequence()
        {
            meshRenderer.material = materialDeath;
            animator.SetTrigger("Death");
            SetState(stateDeath);

            yield return new WaitForSeconds(2f);
            
            EnemyManager.Instance.Leave(this);
        }

        private void OnDisable()
        {
            EnemyManager.Instance.Leave(this);

            meshRenderer.material = materialDefault;
        }

        private void OnDestroy()
        {
            healthSystem.OnDie -= HealthSystem_OnDie;
        }
    }
}
