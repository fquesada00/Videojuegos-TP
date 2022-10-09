using System.Collections;
using System.Collections.Generic;
using Strategies;
using UnityEngine;


[CreateAssetMenu(fileName = "ActorStats", menuName = "Stats/Actor", order = 0)]
public class ActorStats : EntityStats
{
    [SerializeField] private ActorStatValues _actorStatValues;

    public float RotationSmoothSpeed => _actorStatValues.RotationSmoothSpeed;
    public float JumpHeight => _actorStatValues.JumpHeight;
    public int MaxContinuosJumps => _actorStatValues.MaxContinuosJumps;
    public float DashSpeedMultiplier => _actorStatValues.DashSpeedMultiplier;
    public float DashCooldown => _actorStatValues.DashCooldown;
}

[System.Serializable]
public struct ActorStatValues
{
    public float RotationSmoothSpeed;
    public float JumpHeight;
    public int MaxContinuosJumps;
    public float DashSpeedMultiplier;
    public float DashCooldown;
}