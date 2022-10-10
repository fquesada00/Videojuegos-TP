using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FlyWeights.EntitiesStats;
using Controllers.NavMesh;
using Controllers;

public abstract class Enemy : PoolableEntity
{
    protected EnemyFollowController _enemyFollowController;

    public override EntityStats Stats => _enemyStats;
    public EnemyStats EnemyStats => _enemyStats;
    [SerializeField] private EnemyStats _enemyStats;

    public abstract void Attack();

    public override void OnDisable()
    {
        base.OnDisable();

        //_enemyFollowController.GetComponent<NavMeshAgent>().enabled = false;
    }

    public void OnEnable()
    {
        GetComponent<LifeController>().ResetLife();
    }



}
