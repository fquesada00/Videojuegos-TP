using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.Drops;

public class DropSpawner : Spawner<Drop, DropEnum>
{
    [SerializeField] private List<DropEnumWithPrefab> _dropEnumWithPrefabs;
    public override Drop Create(DropEnum dropEnum)
    {
        return _dropEnumWithPrefabs.Find(x => x.DropEnum == dropEnum).Prefab;
    }

    [System.Serializable]
    public struct DropEnumWithPrefab
    {
        public DropEnum DropEnum;
        public Drop Prefab;
    }
}
