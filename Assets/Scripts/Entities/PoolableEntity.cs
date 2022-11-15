using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolableEntity : Entity
{

    public EntityPool parent;

    public virtual void OnDisable()
    {
        parent.ReturnObjectToPool(this);
    }

    public override void Die(Killer killer = Killer.PLAYER)
    {
        gameObject.SetActive(false);
        EventsManager.instance.EnemyDeath(Stats.Id, killer);
        base.DeathSound();
    }
}
