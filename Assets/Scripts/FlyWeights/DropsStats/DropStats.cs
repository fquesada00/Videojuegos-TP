using UnityEngine;

namespace FlyWeights.DropsStats
{
    [CreateAssetMenu(fileName = "DropStats", menuName = "Stats/Drop", order = 0)]
    public class DropStats : ScriptableObject
    {
        [SerializeField] private DropStatValues _statValues;

        public float Value => _statValues.Value;
        public DropType Type => _statValues.Type;
    }
    
    [System.Serializable]
    public struct DropStatValues
    {
        public float Value;
        public DropType Type;
    }

    [System.Serializable]
    public enum DropType
    {
        HEALTH,
        SPEED,
        DAMAGE,
        DEFENSE,
        MANA,
        STAMINA,
        JUMP,
        REGEN
    }
}