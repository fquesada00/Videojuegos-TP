using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactory<T,V>
{
    T Create(V prefab);
}

