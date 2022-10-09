using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SwordStats", menuName = "Stats/Sword", order = 0)]
public class SwordStats : ScriptableObject
{
    [SerializeField] private SwordStatValues _statValues;
    public float AttackCooldown => _statValues.AttackCooldown;
    public float Damage => _statValues.Damage;
}

[System.Serializable]
public struct SwordStatValues
{
    public float AttackCooldown;
    public float Damage;
}
