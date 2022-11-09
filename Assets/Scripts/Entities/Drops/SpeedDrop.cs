

namespace Entities.Drops
{
    public class SpeedDrop : Drop
    {
        public override void Take(Entity entity)
        {
            entity.GetComponentInChildren<MovementController>().SpeedMultiplier *= (1 + DropStats.Value);
        }
    }
}