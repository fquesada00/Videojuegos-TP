using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers; 
using Controllers.NavMesh;
using Controllers.Utils;


[RequireComponent(typeof(LifeController))]
public class WizardEnemy : Enemy
{

    private GameObject _weapon;

    private Cooldown _attackCooldown;

    private EnemyFollowController _enemyFollowController;

    private Wand _wand;

    // Start is called before the first frame update
    void Start()
    {
        _enemyFollowController = GetComponent<EnemyFollowController>();
        _attackCooldown = new Cooldown();
        GetComponent<Animator>().SetBool("idle_combat", true);
        _wand = GetComponentInChildren<Wand>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyFollowController.getDistanceFromPlayer() < 2f)
        {
            Attack();
        }
    }

    public override void Attack()
    {   
        if(_attackCooldown.IsOnCooldown()) return;
        GetComponent<Animator>().SetTrigger("attack");
        _wand.Attack();
        StartCoroutine(_attackCooldown.BooleanCooldown(this.EnemyStats.AttackCooldown));

    }
}
