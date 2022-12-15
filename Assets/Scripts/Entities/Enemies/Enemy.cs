using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlyWeights.EntitiesStats;
using FlyWeights.DropsStats;
using Controllers;
using Controllers.NavMesh;
using Entities.Drops;

public abstract class Enemy : PoolableEntity
{
    protected EnemyFollowController _enemyFollowController;

    public override EntityStats Stats => _enemyStats;
    public EnemyStats EnemyStats => _enemyStats;
    [SerializeField] protected EnemyStats _enemyStats;
    public DropListStats DropListStats => _dropListStats;
    [SerializeField] private DropListStats _dropListStats;
    private DropSpawner _dropSpawner;

    protected virtual void Awake()
    {
        _dropSpawner = FindObjectOfType<DropSpawner>();
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
                var drop = Instantiate(_dropSpawner.Create(possibleDrop.DropEnum));
                Vector3 pos = transform.position;
                pos.y = Terrain.activeTerrain.SampleHeight(transform.position);
                drop.transform.position = pos;
                break; // only one drop per enemy
            }
        }

        base.Die(killer);
    }

}

