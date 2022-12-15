using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class NavMeshEnemy : Enemy
{

    protected override void Awake()
    {
        base.Awake();
        GetComponent<NavMeshAgent>().speed = _enemyStats.BaseMovementSpeed;
    }

}
