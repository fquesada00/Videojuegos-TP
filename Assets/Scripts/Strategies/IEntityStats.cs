namespace Strategies
{
    public interface IEntityStats
    {
        public float MaxHealth { get; }

        public float MovementSpeed { get; }
        public float RotationSmoothSpeed { get; }

        public float Damage { get; }


    }
}