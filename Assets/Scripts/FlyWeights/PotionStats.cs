using UnityEngine;

namespace FlyWeights
{
    [CreateAssetMenu(fileName = "PotionStats", menuName = "Stats/Potion", order = 0)]
    public class PotionStats : ScriptableObject
    {
        [SerializeField] private PotionStatValues _statValues;

        public float Value => _statValues.Value;
        public PotionType Type => _statValues.Type;
    }
    
    [System.Serializable]
    public struct PotionStatValues
    {
        public float Value;
        public PotionType Type;
    }

    [System.Serializable]
    public enum PotionType
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