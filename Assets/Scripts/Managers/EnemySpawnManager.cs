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

    public SpawnMethod spawnMethod;

    [SerializeField]
    private Dictionary<int, ObjectPool> _enemyPools;
    private NavMeshTriangulation _navMeshTriangulation;

    private void Awake()
    {
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
        SpawnEnemy();
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

            NavMeshHit hit;
            EnemyFollowController enemyFollowController = PoolableEntity.GetComponent<EnemyFollowController>();

            if (enemyFollowController != null && NavMesh.SamplePosition(_navMeshTriangulation.vertices[vertexIndex], out hit, 1f, NavMesh.AllAreas))
            {
                NavMeshAgent agent = enemyFollowController.NavMeshAgent;
                Debug.Log("Entre");
                agent.Warp(hit.position);
                agent.enabled = true;
            }            
        }
    }


    public enum SpawnMethod
    {
        Random
    }
}
