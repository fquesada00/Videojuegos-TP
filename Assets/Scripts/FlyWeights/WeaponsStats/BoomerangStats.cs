using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BoomerangStats", menuName = "Stats/Boomerang", order = 0)]
public class BoomerangStats: WeaponStats
{
    [SerializeField] private BoomerangStatsValues _boomerangStatValues;
    public float Range => _boomerangStatValues.Range;
    public float MaxSpeed => _boomerangStatValues.MaxSpeed;
}

[System.Serializable]
public struct BoomerangStatsValues
{
    public float Range;
    public float MaxSpeed;
}
