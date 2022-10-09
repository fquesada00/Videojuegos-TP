using Strategies;
using UnityEngine;

namespace FlyWeights.EntitiesStats
{
    public class EnemyStats : ScriptableObject, IEntityStats
    {
        [SerializeField] private EnemyStatsValues _statValues;


        public float MaxHealth => _statValues.MaxHealth;

        public float MovementSpeed => _statValues.MovementSpeed;

        public float RotationSmoothSpeed => _statValues.RotationSmoothSpeed;

        public float Damage => _statValues.damage;
    }
    
    [System.Serializable]
    public struct EnemyStatsValues
    {
        public float MaxHealth;
        public float MovementSpeed;
        public float RotationSmoothSpeed;
        public float damage;
    }
}