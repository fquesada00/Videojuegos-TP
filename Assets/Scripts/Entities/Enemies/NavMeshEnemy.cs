using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using Controllers.NavMesh;

public abstract class NavMeshEnemy : Enemy
{

    protected override void Awake()
    {
        base.Awake();
        GetComponent<NavMeshAgent>().speed = _enemyStats.BaseMovementSpeed;
    }
}
