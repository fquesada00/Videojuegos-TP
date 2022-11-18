using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Controllers.NavMesh;
using UnityEngine.Serialization;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private int maxSimultaneousEnemiesSize;
    [SerializeField] private float batchSpawnDelay;
    [SerializeField] private int enemiesPerBatchSize;
    [SerializeField] private List<EnemyPoolConfig> enemyPoolConfigs;
    [SerializeField] private SpawnMethod spawnMethod;
    [SerializeField] private float maxDistance = 100;
    [SerializeField] private Dictionary<int, EntityPool> _enemyPools;
    
    private NavMeshTriangulation _navMeshTriangulation;
    private Actor _player;
    private int _currentEnemyCount;

    private void Awake()
    {            
        _player = FindObjectOfType<Actor>();
        _enemyPools = new Dictionary<int, EntityPool>();
        spawnMethod = SpawnMethod.Random;
        _navMeshTriangulation = NavMesh.CalculateTriangulation();
        _currentEnemyCount = 0;
        float accumProb = 0;
        for (int i = 0; i < enemyPoolConfigs.Count; i++)
        {
            _enemyPools.Add(i, EntityPool.CreateInstance(enemyPoolConfigs[i].EnemyPrefab, maxSimultaneousEnemiesSize));
            accumProb += enemyPoolConfigs[i].ElegibilityProb;
            enemyPoolConfigs[i].AccumProb = accumProb;
        }

        if (accumProb == 0)
        {
            Debug.LogError("No enemy elegible to spawn");
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        }
        else if (accumProb < 1)
        {
            Debug.LogError("EnemySpawnManager: Probabilities do not add up to 1...\n...adding the difference to the last enemy");
            enemyPoolConfigs[enemyPoolConfigs.Count - 1].AccumProb = 1;
        } else if (accumProb > 1)
        {
            Debug.LogError("EnemySpawnManager: Probabilities add up to more than 1...\n...normalizing probabilities");
            for (int i = 0; i < enemyPoolConfigs.Count; i++)
            {
                enemyPoolConfigs[i].AccumProb /= accumProb;
            }
        }
    }

    private void EnemyDeath(int enemyId, Killer killer)
    {
        _currentEnemyCount--;
    }

    private void Start()
    {
        EventsManager.instance.OnEnemyDeath += EnemyDeath;
        StartCoroutine(SpawnEnemyBatchAfterSeconds(batchSpawnDelay));
    }

    private IEnumerator SpawnEnemyBatchAfterSeconds(float batchSpawnDelay)
    {
        while (true)
        {
            SpawnEnemyBatch();
            yield return new WaitForSeconds(batchSpawnDelay);
        }
    }

    private void SpawnEnemyBatch()
    {
        int enemiesSpawnedInBatch = 0;

        while (_currentEnemyCount < maxSimultaneousEnemiesSize && enemiesSpawnedInBatch < enemiesPerBatchSize )
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
        // get a random probability
        float randomProb = Random.Range(0, 1f);
        
        // iterate through the enemy pool configs and find the one that matches the random probability
        for (int i = 0; i < enemyPoolConfigs.Count; i++)
        {
            if (randomProb <= enemyPoolConfigs[i].AccumProb)
            {
                DoSpawnEnemy(i);
                return;
            }
        }
    }

    private void DoSpawnEnemy(int index)
    {
        PoolableEntity PoolableEntity = _enemyPools[index].GetObject();
        if (PoolableEntity != null)
        {
            NavMeshAgent agent = PoolableEntity.GetComponent<NavMeshAgent>();

            if (agent != null)
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

    [System.Serializable]
    public class EnemyPoolConfig
    {
        [SerializeField] private Enemy enemyPrefab;
        public Enemy EnemyPrefab => enemyPrefab;
        
        [SerializeField] private float elegibilityProb;
        public float ElegibilityProb => elegibilityProb;

        private float _accumProb = 0;
        public float AccumProb
        {
            get => _accumProb;
            set => _accumProb = value;
        }
    }
}
