using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FlyWeights.EntitiesStats;
using FlyWeights.DropsStats;
using Controllers.NavMesh;
using Controllers;
using Entities.Drops;

public abstract class Enemy : PoolableEntity
{
    protected EnemyFollowController _enemyFollowController;

    public override EntityStats Stats => _enemyStats;
    public EnemyStats EnemyStats => _enemyStats;
    public DropListStats DropListStats => _dropListStats;
    [SerializeField] private EnemyStats _enemyStats;
    [SerializeField] private DropListStats _dropListStats;
    private Spawner<Drop> _dropSpawner;

    private void Awake()
    {
        _dropSpawner = new Spawner<Drop>();
    }

    public abstract void Attack();

    public override void OnDisable()
    {
        base.OnDisable();
    }

    protected virtual void OnEnable()
    {
        GetComponent<LifeController>().ResetLife();
    }

    public override void Die(Killer killer = Killer.PLAYER)
    {
        foreach (var possibleDrop in DropListStats.PossibleDropsStats)
        {
            if (Random.Range(0f, 1f) < possibleDrop.DropChance)
            {
                var drop = _dropSpawner.Create(possibleDrop.prefab);
                drop.transform.position = transform.position;
                break; // only one drop per enemy
            }
        }

        base.Die(killer);
    }

}
