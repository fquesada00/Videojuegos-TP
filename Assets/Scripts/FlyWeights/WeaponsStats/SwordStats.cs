using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SwordStats", menuName = "Stats/Sword", order = 0)]
public class SwordStats : WeaponStats
{
    [SerializeField] private SwordStatValues _statValues;

}

[System.Serializable]
public struct SwordStatValues
{

}
