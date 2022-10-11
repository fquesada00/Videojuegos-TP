using Strategies;
using UnityEngine;

namespace FlyWeights.EntitiesStats
{
    [CreateAssetMenu(fileName = "PatrolStats", menuName = "Stats/Patrol", order = 0)]
    public class PatrolStats: ScriptableObject
    {
        [SerializeField] private PatrolStatsValues _patrolStatsValues;
        
        public float Speed => _patrolStatsValues.Speed;
        public float MaxTargetDistance => _patrolStatsValues.MaxTargetDistance;
        public float MinWanderTargetDistance => _patrolStatsValues.MinWanderTargetDistance;
    }
    
    [System.Serializable]
    public struct PatrolStatsValues
    {
        public float Speed;
        public float MaxTargetDistance;
        public float MinWanderTargetDistance;
    }
}