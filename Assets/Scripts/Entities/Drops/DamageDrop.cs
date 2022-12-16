

namespace Entities.Drops
{
    public class DamageDrop : Drop
    {
        public override void Take(Entity entity)
        {
            GlobalDataManager.Instance.DamageMultiplier *= (1 + DropStats.Value);
        }
    }
}