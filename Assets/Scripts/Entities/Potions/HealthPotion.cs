using Strategies;

namespace Entities
{
    public class HealthPotion : Potion
    {
        public override void Take(IDamageable damageable)
        {
            damageable.AddHealth(PotionStats.Value);
        }
    }
}