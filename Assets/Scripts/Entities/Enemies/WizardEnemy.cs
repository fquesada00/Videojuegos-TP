using System;
using System.Collections;
using System.Collections.Generic;
using Commands.Sounds;
using UnityEngine;
using Controllers;
using Controllers.NavMesh;
using Controllers.Utils;
using Random = UnityEngine.Random;


[RequireComponent(typeof(LifeController))]
public class WizardEnemy : NavMeshEnemy
{
    private GameObject _weapon;

    private Cooldown _attackCooldown;
    private Cooldown _whisperCooldown;
    
    private CmdWhisperSound _cmdWhisperSound;

    private Wand _wand;

    // Start is called before the first frame update
    new void OnEnable()
    {
        base.OnEnable();

        _enemyFollowController = GetComponent<EnemyFollowController>();
        _attackCooldown = new Cooldown();
        
        GetComponent<Animator>().SetBool("idle_combat", true);
        _wand = GetComponentInChildren<Wand>();

        base.SoundController = GetComponent<SoundController>();
        
        _whisperCooldown = new Cooldown();
        _cmdWhisperSound = new CmdWhisperSound(base.SoundController);
        Whisper();  
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = _enemyFollowController.getDistanceFromPlayer();
        if (distanceFromPlayer < this.EnemyStats.AttackRange)
        {
            Attack();
        } else if (distanceFromPlayer < this.EnemyStats.MinSoundDistance)
        {
            // get a random number and with 0.2 probability, whisper
            if (Random.Range(0f, 1f) < 0.2f)
            {
                Whisper();
            }
        }
    }

    public override void Attack()
    {
        if (_attackCooldown.IsOnCooldown()) return;
        GetComponent<Animator>().SetTrigger("attack");
        _wand.Attack(false);
        StartCoroutine(_attackCooldown.BooleanCooldown(this.EnemyStats.AttackCooldown));
    }

    private void Whisper()
    {
        if(_whisperCooldown.IsOnCooldown()) return;
        _cmdWhisperSound.Execute();
        StartCoroutine(_whisperCooldown.BooleanCooldown(this.EnemyStats.SoundCooldown));
    }
}