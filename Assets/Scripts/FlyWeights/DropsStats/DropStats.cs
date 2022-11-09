using UnityEngine;

namespace FlyWeights.DropsStats
{
    [CreateAssetMenu(fileName = "DropStats", menuName = "Stats/Drop", order = 0)]
    public class DropStats : ScriptableObject
    {
        [SerializeField] private DropStatValues _statValues;
        public float Value => _statValues.Value;
    }
    
    [System.Serializable]
    public struct DropStatValues
    {
        public float Value;
    }

}