using UnityEngine;
using System.Collections.Generic;

public class RandomObjectSpawner : MonoBehaviour {
    
    #region Variables ================================
    [Header("Variables")]
    [SerializeField] List<GameObject> _objectsThatCanBeSpawned;
    [SerializeField] List<Transform> _spawnLocations;
    [SerializeField] Vector2 _spawnIntervalRange = new Vector2(1f, 3f);

    private float _spawnTimer;
    private float _currentSpawnInterval;

    #endregion

    #region Setup ================================

    private void Start()
    {
        _spawnTimer = 0f;
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
        if(_objectsThatCanBeSpawned.Count == 0 || _spawnLocations.Count == 0)
        {
            Debug.LogWarning("RandomObjectSpawner: SpawnRandomObject: Cannot spawn object because there are no objects that can be spawned or there are no spawn locations.");
            return null;
        }

        GameObject objectToSpawn = _objectsThatCanBeSpawned[Random.Range(0, _objectsThatCanBeSpawned.Count)];
        Transform spawnLocation = _spawnLocations[Random.Range(0, _spawnLocations.Count)];

        return Instantiate(objectToSpawn, spawnLocation.position, spawnLocation.rotation);
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