using UnityEngine;
using System.Collections.Generic;
using Entities.Drops;

namespace FlyWeights.DropsStats
{
    [CreateAssetMenu(fileName = "DropListStats", menuName = "Stats/DropListStats", order = 0)]
    public class DropListStats : ScriptableObject
    {
        [SerializeField] private List<PossibleDrop> _possibleDropsStats;

        public List<PossibleDrop> PossibleDropsStats => _possibleDropsStats;
    }

    [System.Serializable]
    public struct PossibleDrop
    {
        public float DropChance;
        public DropEnum DropEnum;
    }
}