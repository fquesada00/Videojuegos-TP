

namespace Entities.Drops
{
    public class SpeedDrop : Drop
    {
        public override void Take(Entity entity)
        {
            GlobalDataManager.Instance.SpeedMultiplier *= (1 + DropStats.Value);
        }
    }
}