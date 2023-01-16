using System;
using Game.Scripts.Combat;
using Game.Scripts.Core;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Scripts.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private HealthSystem healthSystem;
        [Header("Specs")]
        [SerializeField] private float attackRange = 3f;
        [SerializeField] private int damage = 10;
        [SerializeField] private float attackDeltaTime = 1f;
    
        private HealthSystem target, player;
        private float lastTimeAttack = Mathf.Infinity;
        private State stateCurrent, stateChase, stateAttack;

        private void Awake()
        {
            stateCurrent = new State(null, null, null);
            stateChase = new State(StartAgent, Move, StopAgent);
            stateAttack = new State(null, HandleWithAttack, null);
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
        
            target.TakeDamage(damage);
        
            if(target.IsAlive) return;
        
            // kapı kırıldıktan sonra hedef direkt olarak oyuncu olmalı
            SetTarget(player); 
            // animation etc.
        }

        private void SetState(State state)
        {
            stateCurrent?.onStateExit();
            stateCurrent = state;
            stateCurrent.onStateEnter();
        }

        private void HealthSystem_OnDie(object sender, EventArgs eventArgs)
        {
            EnemyManager.Instance.Leave(this);
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            healthSystem.OnDie -= HealthSystem_OnDie;
        }
    }
}
