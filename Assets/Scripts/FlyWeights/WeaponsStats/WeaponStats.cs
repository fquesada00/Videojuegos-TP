using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponStats: ScriptableObject
{
    [SerializeField] private WeaponStatsValues _weaponStatValues;
    public float Cooldown => _weaponStatValues.Cooldown;
    public float Damage => _weaponStatValues.Damage;

    public float VanashingTime => _weaponStatValues.VanashingTime;
}

[System.Serializable]
public struct WeaponStatsValues
{
    public float Cooldown;
    public float Damage;

    public float VanashingTime;
}
