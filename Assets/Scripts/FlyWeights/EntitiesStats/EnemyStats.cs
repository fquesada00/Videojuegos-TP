using Strategies;
using UnityEngine;

namespace FlyWeights.EntitiesStats
{
    public class EnemyStats : ScriptableObject, IEntityStats
    {
        [SerializeField] private EnemyStatsValues _statValues;


        public float MaxHealth => _statValues.MaxHealth;
    }
    
    [System.Serializable]
    public struct EnemyStatsValues
    {
        public float MaxHealth;
    }
}