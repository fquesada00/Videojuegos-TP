using Controllers.NavMesh;
using FlyWeights.EntitiesStats;
using UnityEngine;

namespace Entities
{
    public abstract class Boss : Entity
    {
        protected EnemyFollowController _enemyFollowController;

        public override EntityStats Stats => _enemyStats;
        public EnemyStats EnemyStats => _enemyStats;
        [SerializeField] private EnemyStats _enemyStats;

        public abstract void Attack();

        public override void Die(Killer killer = Killer.PLAYER)
        {
            EventsManager.instance.BossKilled();
            base.Die(killer);
        }
    }
}