using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolableEntity : Entity
{

    public ObjectPool parent;

    public virtual void OnDisable()
    {
        
        parent.ReturnObjectToPool(this);
        
    }

    public override void Die()
    {
        gameObject.SetActive(false);
        EventsManager.instance.EnemyDeath();
        base.DeathSound();
    }
}
