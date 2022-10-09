using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Strategies;

public abstract class Entity : MonoBehaviour
{
    public abstract EntityStats Stats { get; }
    //private EntityStats _entityStats;
}


