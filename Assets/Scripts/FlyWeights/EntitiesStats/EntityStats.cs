using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : ScriptableObject
 {
        [SerializeField] private EntityStatValues _entityStatValues;


        public float MaxHealth => _entityStatValues.MaxHealth;

        public float MovementSpeed => _entityStatValues.MovementSpeed;

        public float Damage => _entityStatValues.Damage;

        public float AttackCooldown => _entityStatValues.AttackCooldown;
        

    }

[System.Serializable]
public struct EntityStatValues
{
    public int MaxHealth;
    public float MovementSpeed;
    public float Damage;

    public float AttackCooldown;
}