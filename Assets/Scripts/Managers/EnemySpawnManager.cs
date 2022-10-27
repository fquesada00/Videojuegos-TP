using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Controllers.NavMesh;

public class EnemySpawnManager : MonoBehaviour
{
    public int enemiesToSpawnSize;
    public float spawnDelay;
    public List<Enemy> enemyPrefabs;
    private Actor _player;

    public SpawnMethod spawnMethod;

    [SerializeField] private Dictionary<int, ObjectPool> _enemyPools;
    
    private NavMeshTriangulation _navMeshTriangulation;

    private void Awake()
    {            
        _player = FindObjectOfType<Actor>();
        _enemyPools = new Dictionary<int, ObjectPool>();
        spawnMethod = SpawnMethod.Random;
        _navMeshTriangulation = NavMesh.CalculateTriangulation();
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            _enemyPools.Add(i, ObjectPool.CreateInstance(enemyPrefabs[i], enemiesToSpawnSize));
        }
    }

    private void EnemyDeath(int enemyId)
    {
        StartCoroutine(SpawnEnemyWithDelay());
    }

    private void Start()
    {
        EventsManager.instance.OnEnemyDeath += EnemyDeath;
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnDelay);

        int spawnedEnemies = 0;

        while (spawnedEnemies < enemiesToSpawnSize)
        {
            SpawnEnemy();
            spawnedEnemies++;

            yield return wait;
        }
    }

    private void SpawnEnemy()
    {
        if (spawnMethod == SpawnMethod.Random)
        {
            SpawnRandomEnemy();
        }
    }

    private IEnumerator SpawnEnemyWithDelay()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnDelay);
        yield return wait;
        SpawnEnemy();
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
                agent.Warp(GetRandomPositionOnNavMesh(_player.transform.position, 100));
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
