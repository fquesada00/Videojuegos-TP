using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlyWeights.EntitiesStats;

public abstract class Enemy : Entity
{

    public override EntityStats Stats => _enemyStats;
    public EnemyStats EnemyStats => _enemyStats;
    [SerializeField] private EnemyStats _enemyStats;

    public abstract void Attack();

}
