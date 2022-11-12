using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : ScriptableObject
 {
        [SerializeField] private EntityStatValues _entityStatValues;


        public float MaxHealth => _entityStatValues.MaxHealth;

        public float BaseMovementSpeed => _entityStatValues.BaseMovementSpeed;

        public float Damage => _entityStatValues.Damage;

        public float AttackCooldown => _entityStatValues.AttackCooldown;

        public float CritProbability => _entityStatValues.CritProbability;
        
        public int Id => _entityStatValues.Id;
    }

[System.Serializable]
public struct EntityStatValues
{
    public int MaxHealth;
    public float BaseMovementSpeed;
    public float Damage;
    public float AttackCooldown;
    public float CritProbability;
    public int Id;

}