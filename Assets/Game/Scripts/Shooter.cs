using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // enemy searching
    [SerializeField] private float searchRange = 10f;
    private LayerMask searchMask;
    private Collider[] searchBuffer = new Collider[1];
    private HealthSystem target;
    // specs (daha sonra weaponSO olarak atanacak)
    private int damage = 10;
    private float fireRate = 1;// per second
    private float lastFiredTime = Mathf.Infinity;
    // states
    private State stateCurrent, stateSearching, stateShooting;

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
            transform.position, searchRange, searchBuffer, searchMask);

        if (resultCount == 0) return;
        
        target = searchBuffer[0].GetComponent<HealthSystem>();
        SetState(stateShooting);
    }

    private void Shoot()
    {
        if (!IsTargetStillInRange() || !IsTargetAlive())
        {
            SetState(stateSearching);
            return;
        }
        
        lastFiredTime += Time.deltaTime;

        if (!(lastFiredTime >= fireRate)) return;
        
        target.TakeDamage(damage);

        lastFiredTime = 0;
    }

    private bool IsTargetStillInRange()
    {
        var distance = Vector3.Distance(transform.position, target.transform.position);

        return distance <= searchRange;
    }

    private bool IsTargetAlive()
    {
        return target.IsAlive;
    }
}
