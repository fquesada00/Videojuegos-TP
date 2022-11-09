using Strategies;

namespace Entities.Drops
{
    public class HealthDrop : Drop
    {
        public override void Take(Entity entity)
        {
            entity.GetComponentInChildren<IDamageable>().AddHealth(DropStats.Value);
        }
    }
}