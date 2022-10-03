using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ActorStats", menuName = "Stats/Actor", order = 0)]
public class ActorStats : ScriptableObject
{
    [SerializeField] private ActorStatValues _statValues;

    public int MaxLife => _statValues.MaxLife;
    public float MovementSpeed => _statValues.MovementSpeed;
    public float RotationSmoothSpeed => _statValues.RotationSmoothSpeed;
    public float JumpHeight => _statValues.JumpHeight;
    public int MaxContinuosJumps => _statValues.MaxContinuosJumps;
    public float DashSpeedMultiplier => _statValues.DashSpeedMultiplier;
    public float DashCooldown => _statValues.DashCooldown;
}

[System.Serializable]
public struct ActorStatValues
{
    public int MaxLife;
    public float MovementSpeed;
    public float RotationSmoothSpeed;
    public float JumpHeight;
    public int MaxContinuosJumps;
    public float DashSpeedMultiplier;
    public float DashCooldown;
}
