using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : Entity
{
    public override EntityStats Stats => _actorStats;

    public ActorStats ActorStats => _actorStats;
    [SerializeField] private ActorStats _actorStats;
}

