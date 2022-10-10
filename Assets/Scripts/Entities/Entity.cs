using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Strategies;

public abstract class Entity : MonoBehaviour
{
    public abstract EntityStats Stats { get; }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
    //private EntityStats _entityStats;
}


