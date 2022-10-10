using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

public class Actor : Entity
{
    public override EntityStats Stats => _actorStats;

    public ActorStats ActorStats => _actorStats;
    [SerializeField] private ActorStats _actorStats;

    private void Start()
    {
        base.SoundController = GetComponent<SoundController>();
    }

    public override void Die()
    {
        Destroy(gameObject);
        EventsManager.instance.GameOver(false);
    }
}

