using Strategies;

namespace Entities.Drops
{
    public class HealthDrop : Drop
    {
        public override void Take(IDamageable damageable)
        {
            damageable.AddHealth(DropStats.Value);
        }
    }
}