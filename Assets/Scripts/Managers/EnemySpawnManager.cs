using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Controllers.NavMesh;

public class EnemySpawnManager : MonoBehaviour
{
    public int enemiesToSpawnSize;
    public float spawnDelay;
    public int batchSize;
    public List<Enemy> enemyPrefabs;
    private Actor _player;
    private int _currentEnemyCount;
    public SpawnMethod spawnMethod;

    public float maxDistance = 100;

    [SerializeField] private Dictionary<int, EntityPool> _enemyPools;
    
    private NavMeshTriangulation _navMeshTriangulation;

    private void Awake()
    {            
        _player = FindObjectOfType<Actor>();
        _enemyPools = new Dictionary<int, EntityPool>();
        spawnMethod = SpawnMethod.Random;
        _navMeshTriangulation = NavMesh.CalculateTriangulation();
        _currentEnemyCount = 0;
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            _enemyPools.Add(i, EntityPool.CreateInstance(enemyPrefabs[i], enemiesToSpawnSize));
        }
    }

    private void EnemyDeath(int enemyId, Killer killer)
    {
        _currentEnemyCount--;
    }

    private void Start()
    {
        EventsManager.instance.OnEnemyDeath += EnemyDeath;
        StartCoroutine(SpawnEnemyBatchAfterSeconds(spawnDelay));
    }

    private IEnumerator SpawnEnemyBatchAfterSeconds(float spawnDelay)
    {
        while (true)
        {
            SpawnEnemyBatch();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnEnemyBatch()
    {
        int enemiesSpawnedInBatch = 0;

        while (_currentEnemyCount < enemiesToSpawnSize && enemiesSpawnedInBatch < batchSize )
        {
            SpawnEnemy();
            _currentEnemyCount++;
            enemiesSpawnedInBatch++;
        }
    }

    private void SpawnEnemy()
    {
        if (spawnMethod == SpawnMethod.Random)
        {
            SpawnRandomEnemy();
        }
    }

    private void SpawnRandomEnemy()
    {
        DoSpawnEnemy(Random.Range(0, enemyPrefabs.Count));
    }

    private void DoSpawnEnemy(int index)
    {
        PoolableEntity PoolableEntity = _enemyPools[index].GetObject();
        if (PoolableEntity != null)
        {
            int vertexIndex = Random.Range(0, _navMeshTriangulation.vertices.Length);

            NavMeshAgent agent = PoolableEntity.GetComponent<NavMeshAgent>();

            if (agent != null) // TODO: MAGIC NUMBER
            {
                agent.Warp(GetRandomPositionOnNavMesh(_player.transform.position, maxDistance));
                agent.enabled = true;
            }
        }
    }

    public static Vector3 GetRandomPositionOnNavMesh(Vector3 origin, float maxDistance)
    {
        Vector3 randomPosition = origin + Random.insideUnitSphere * maxDistance;
        float positionMargin = 1000; // TODO: MAGIC NUMBER
        bool foundLocation = false;
        NavMeshHit hit;
        while (!foundLocation)
        {
            foundLocation = NavMesh.SamplePosition(randomPosition, out hit, positionMargin, NavMesh.AllAreas);
            if(foundLocation) return hit.position;

            positionMargin += 1000; // TODO: MAGIC NUMBER
        }

        return Vector3.zero;
    }

    public enum SpawnMethod
    {
        Random
    }
}
