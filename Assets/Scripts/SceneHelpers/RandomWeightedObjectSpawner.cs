using UnityEngine;
using System.Collections.Generic;
using Digx7.Zygote;


public class RandomWeightedObjectSpawner : MonoBehaviour {
    
    #region Variables ================================
    [Header("Variables")]
    [SerializeField] List<GameObjectAndWeight> _weightedObjectsThatCanBeSpawned;
    [SerializeField] List<Transform> _spawnLocations;
    [SerializeField] Vector2 _spawnIntervalRange = new Vector2(1f, 3f);

    private float _spawnTimer;
    private float _totalWeight;
    private float _currentSpawnInterval;

    #endregion

    #region Setup ================================

    private void Start()
    {
        _spawnTimer = 0f;
        _totalWeight = GetTotalWeight();
        _currentSpawnInterval = Random.Range(_spawnIntervalRange.x, _spawnIntervalRange.y);
    }

    #endregion

    #region Main Functions ================================

    private void Update()
    {
        IncreaseTimer();
    }

    public GameObject SpawnRandomObject()
    {
        if(_weightedObjectsThatCanBeSpawned.Count == 0 || _spawnLocations.Count == 0)
        {
            Debug.LogWarning("RandomWeightedObjectSpawner: SpawnRandomObject: Cannot spawn object because there are no objects that can be spawned or there are no spawn locations.");
            return null;
        }

        GameObject objectToSpawn = GetRandomWeightedObject();
        Transform spawnLocation = _spawnLocations[Random.Range(0, _spawnLocations.Count)];

        return Instantiate(objectToSpawn, spawnLocation.position, spawnLocation.rotation);
    }

    private float GetTotalWeight()
    {
        float totalWeight = 0f;
        foreach (GameObjectAndWeight item in _weightedObjectsThatCanBeSpawned)
        {
            totalWeight += item.weight;
        }
        return totalWeight;
    }

    private GameObject GetRandomWeightedObject()
    {
        float randomValue = Random.Range(0f, _totalWeight);
        float cumulativeWeight = 0f;

        foreach (GameObjectAndWeight item in _weightedObjectsThatCanBeSpawned)
        {
            cumulativeWeight += item.weight;
            if (randomValue <= cumulativeWeight)
            {
                return item.gameObject;
            }
        }

        // Fallback in case of rounding errors
        return _weightedObjectsThatCanBeSpawned[_weightedObjectsThatCanBeSpawned.Count - 1].gameObject;
    }

    private void IncreaseTimer()
    {
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= _currentSpawnInterval)
        {
            SpawnRandomObject();
            _spawnTimer = 0f;
            _currentSpawnInterval = Random.Range(_spawnIntervalRange.x, _spawnIntervalRange.y);
        }
    }

    #endregion
}