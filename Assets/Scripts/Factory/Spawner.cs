using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner<T,V> : MonoBehaviour, IFactory<T,V> where T : MonoBehaviour
{
    public abstract T Create(V prefab);
}
