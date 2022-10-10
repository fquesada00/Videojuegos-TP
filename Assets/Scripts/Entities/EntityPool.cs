using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private PoolableEntity _prefab;
    private List<PoolableEntity> _availableObjects;

    private ObjectPool(PoolableEntity prefab, int size)
    {
        _prefab = prefab;
        _availableObjects = new List<PoolableEntity>(size);
    }

    public static ObjectPool CreateInstance(PoolableEntity prefab, int size)
    {
        ObjectPool pool = new ObjectPool(prefab, size);
        
        GameObject poolObject = new GameObject(prefab.name + " Pool");
        pool.CreateObjects(poolObject.transform, size);

        return pool;
    }

    private void CreateObjects(Transform parent, int size)
    {
        for (int i = 0; i < size; i++)
        {
            PoolableEntity obj = GameObject.Instantiate(_prefab, Vector3.zero, Quaternion.identity, parent.transform);
            obj.parent = this;
            obj.gameObject.SetActive(false);
            //_availableObjects.Add(obj);
        }
    }

    public void ReturnObjectToPool(PoolableEntity obj)
    {
        _availableObjects.Add(obj);
    }

    public PoolableEntity GetObject()
    {
        if (_availableObjects.Count == 0)
        {
            Debug.LogError("No objects available in pool");
            return null;
        }

        PoolableEntity obj = _availableObjects[0];
        _availableObjects.RemoveAt(0);

        obj.gameObject.SetActive(true);

        return obj;
    }
}
