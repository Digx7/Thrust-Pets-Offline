using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Digx7.Zygote;

public class LevelGenerator : MonoBehaviour 
{

    #region Variables ================================

    [Header("Variables")]
    public GameObject blockPrefab;
    public GameObject coinPrefab;
    public GameObject[] obstaclePrefabs;
    public Transform levelObjectsParent;
    // TODO: Get player to reference the player object in the scene
    public Transform player;
    public int numberOfBlocks = 5;
    public float blockLength = 10f;
    // public int numberOfCoins = 10;
    public float obstacleSpawnChance = 0.3f;
    public float reuseDistance = 30f;
    public float laneDistance = 3f;
    public int numberOfLanes = 3;

    public Queue<GameObject> activeBlocks = new Queue<GameObject>();
    public Queue<GameObject> activeObstacles = new Queue<GameObject>();
    public Queue<GameObject> activeCoins = new Queue<GameObject>();

    public List<ObstaclesState> obstacleStates = new List<ObstaclesState>() {
        new ObstaclesState() {
            checkStartTimeActive = true,
            startTimeActive = 120f,
            checkEndTimeActive = false,
            endTimeActive = 120f,
            minObstacleOffset = 3,
            maxObstacleOffset = 5,
            numberOfObstacles = 25,
            numberOfCoins = 40
        },
        new ObstaclesState() {
            checkStartTimeActive = true,
            startTimeActive = 60f,
            checkEndTimeActive = false,
            endTimeActive = 60f,
            minObstacleOffset = 7,
            maxObstacleOffset = 10,
            numberOfObstacles = 20,
            numberOfCoins = 25
        },
        new ObstaclesState() {
            checkStartTimeActive = true,
            startTimeActive = 30f,
            checkEndTimeActive = false,
            endTimeActive = 30f,
            minObstacleOffset = 8,
            maxObstacleOffset = 12,
            numberOfObstacles = 15,
            numberOfCoins = 25
        },
        new ObstaclesState() {
            checkStartTimeActive = false,
            startTimeActive = 0f,
            checkEndTimeActive = false,
            endTimeActive = 0f,
            minObstacleOffset = 10,
            maxObstacleOffset = 15,
            numberOfObstacles = 10,
            numberOfCoins = 25
        }
    };

    public Vector3 nextSpawnPoint;
    public int _obstacleNextZPos = 0;

    private float _timeSinceLevelLoad = 0f;
    public int GetNextObstacleZPos() 
    {
        // int offsetMax = 15;
        // int offsetMin = 10;

        // foreach (ObstaclesState offset in obstacleStates)
        // {
        //     if(offset.IsActive(_timeSinceLevelLoad)) 
        //     {
        //         Debug.Log("Increasing difficulty: LevelGenerator.GetNextObstacleZPos() _timeSinceLevelLoad > " + offset.minTimeActive);
            
        //         offsetMax = offset.maxObstacleOffset;
        //         offsetMin = offset.minObstacleOffset;
        //         break;
        //     }
        // }

        ObstaclesState currentState = GetCurrentObstaclesState();

        _obstacleNextZPos += currentState.GetRandomObstacleOffset();

        return _obstacleNextZPos;        
    }
    public int coinNextZPos = 0;

    public int seed = 0;
    public System.Random sharedRandom;

    private bool _isSetup = false;

    [Header("Incoming Channels")]
    [CreateScriptableObjectButton("Assets/ScriptableObjects/Channels/Player")]
    [SerializeField] InstantiatedObjectChannel _on_PlayerSpawned_Channel;

    #endregion

    #region Setup ================================

    public void OnEnable()
    {
        SetupChannels();
    }

    public void OnDisable()
    {
        TearDownChannels();
    }

    public void SetupChannels()
    {
        _on_PlayerSpawned_Channel.channelEvent.AddListener(OnRecieve_OnPlayerSpawned);

        UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnRecieve_ActiveSceneChanged;
    }

    public void TearDownChannels()
    {
        _on_PlayerSpawned_Channel.channelEvent.RemoveListener(OnRecieve_OnPlayerSpawned);

        UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= OnRecieve_ActiveSceneChanged;
    }

    #endregion

    #region Channel Responses ================================

    public void OnRecieve_OnPlayerSpawned(InstantiatedObject obj)
    {

        player = obj.gameObject.transform;

    }

    public void OnRecieve_ActiveSceneChanged(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.Scene scene2)
    {
        
        Setup();
    
    }

    #endregion

    #region Main Functions ================================

    public void Setup()
    {
        
        seed = Random.Range(1, 99999);

        sharedRandom = new System.Random(seed);

        nextSpawnPoint = transform.position;

       
        for(int i = 0; i < levelObjectsParent.childCount; i++) 
        {
            activeBlocks.Enqueue(levelObjectsParent.GetChild(i).gameObject);
            nextSpawnPoint.z += blockLength;
        }

        StartCoroutine(Internal_Start());
    }

    IEnumerator Internal_Start() 
    {
        yield return new WaitUntil(() => seed != 0);

        ObstaclesState currentState = GetCurrentObstaclesState();
        for (int j = 0; j < currentState.numberOfObstacles; j++)
        {
            InstantiateNewObstacle();
        }

        for (int j = 0; j < currentState.numberOfCoins; j++)
        {
            InstantiateNewCoin();
        }

        // for (int i = 0; i < numberOfCoins; i++)
        // {
        //     // int randomObstacleIndex = sharedRandom.Next(0, obstaclePrefabs.Length);
        //     // int randomLaneIndex = sharedRandom.Next(0, numberOfLanes);

        //     // Vector3 obstaclePosition = new Vector3(0f, obstaclePrefabs[randomObstacleIndex].transform.position.y, GetNextObstacleZPos());
        //     // if (randomLaneIndex == 0) { obstaclePosition.x = laneDistance; }
        //     // else if (randomLaneIndex == 1) { obstaclePosition.x = 0; }
        //     // else if (randomLaneIndex == 2) { obstaclePosition.x = -1 * laneDistance; }

            
        //     // GameObject o = Instantiate(obstaclePrefabs[randomObstacleIndex], obstaclePosition, obstaclePrefabs[randomObstacleIndex].transform.rotation);
        //     // activeObstacles.Enqueue(o);
        //     // InstantiateNewObstacle();
        //     // _obstacleNextZPos += 15;
            
        //     Vector3 coinPosition = new Vector3(obstaclePosition.x, coinPrefab.transform.position.y, coinNextZPos);

        //     GameObject c = Instantiate(coinPrefab, coinPosition, coinPrefab.transform.rotation);
        //     activeCoins.Enqueue(c);

        //     coinNextZPos += 54;
        // }

        _isSetup = true;
    }

    // TODO: add 7 blocks to the Map in the scene

    void Update()
    {
        if(!_isSetup) return;
        if (player == null) return;

        if (player.position.z > activeBlocks.Peek().transform.position.z + reuseDistance)
        {
            ReuseBlock();
        }

        if(activeObstacles.Count > 0) 
        {
            
            if (player.position.z > activeObstacles.Peek().transform.position.z + (reuseDistance / 2))
            {
                ReuseObstacle();
            }
        }

        if (player.position.z > activeCoins.Peek().transform.position.z)
        {
            ReuseCoin();
        }

        _timeSinceLevelLoad += Time.deltaTime;
    }

    void SpawnNewBlock()
    {
        GameObject newBlock = Instantiate(blockPrefab, nextSpawnPoint, Quaternion.identity);
        activeBlocks.Enqueue(newBlock);
        nextSpawnPoint.z += blockLength;
    }

    void ReuseObstacle() 
    {
        ObstaclesState currentState = GetCurrentObstaclesState();
        if(currentState.numberOfObstacles > activeObstacles.Count) 
        {
            int obstaclesToSpawn = currentState.numberOfObstacles - activeObstacles.Count;
            
            for (int i = activeObstacles.Count; i < obstaclesToSpawn; i++)
            {
                InstantiateNewObstacle();
            }
        }
        
        GameObject oldObstacle = activeObstacles.Dequeue();
        int randomLaneIndex = sharedRandom.Next(0, 3);

        Vector3 obstaclePosition = new Vector3(0f, oldObstacle.transform.position.y, GetNextObstacleZPos());
        if (randomLaneIndex == 0) { obstaclePosition.x = laneDistance; }
        else if (randomLaneIndex == 1) { obstaclePosition.x = 0; }
        else if (randomLaneIndex == 2) { obstaclePosition.x = -1 * laneDistance; }

        oldObstacle.transform.position = obstaclePosition;
        // _obstacleNextZPos += 15;
        activeObstacles.Enqueue(oldObstacle);

        
    }

    void ReuseCoin()
    {
        GameObject oldCoin = activeCoins.Dequeue();

        float coinLanePosition = 0f;
        int randomLaneIndex = sharedRandom.Next(0, 3);
        if (randomLaneIndex == 0) { coinLanePosition = laneDistance; }
        else if (randomLaneIndex == 1) { coinLanePosition = 0; }
        else if (randomLaneIndex == 2) { coinLanePosition = -1 * laneDistance; }

        Vector3 coinPosition = new Vector3(coinLanePosition, oldCoin.transform.position.y, coinNextZPos);
        if (oldCoin.activeSelf == false)
        {
            oldCoin.SetActive(true);
        }

        oldCoin.transform.position = coinPosition;
        coinNextZPos += 54;
        activeCoins.Enqueue(oldCoin);
    }

    void ReuseBlock()
    {
        GameObject oldBlock = activeBlocks.Dequeue();
        oldBlock.transform.position = nextSpawnPoint;
        nextSpawnPoint.z += blockLength;
        activeBlocks.Enqueue(oldBlock);
    }

    void InstantiateNewObstacle()
    {
        int randomObstacleIndex = sharedRandom.Next(0, obstaclePrefabs.Length);
        int randomLaneIndex = sharedRandom.Next(0, numberOfLanes);

        Vector3 obstaclePosition = new Vector3(0f, obstaclePrefabs[randomObstacleIndex].transform.position.y, GetNextObstacleZPos());
        if (randomLaneIndex == 0) { obstaclePosition.x = laneDistance; }
        else if (randomLaneIndex == 1) { obstaclePosition.x = 0; }
        else if (randomLaneIndex == 2) { obstaclePosition.x = -1 * laneDistance; }

        GameObject o = Instantiate(obstaclePrefabs[randomObstacleIndex], obstaclePosition, obstaclePrefabs[randomObstacleIndex].transform.rotation);
        activeObstacles.Enqueue(o);
    }

    void InstantiateNewCoin()
    {
        int randomLaneIndex = sharedRandom.Next(0, numberOfLanes);

        Vector3 coinPosition = new Vector3(0f, coinPrefab.transform.position.y, coinNextZPos);
        if (randomLaneIndex == 0) { coinPosition.x = laneDistance; }
        else if (randomLaneIndex == 1) { coinPosition.x = 0; }
        else if (randomLaneIndex == 2) { coinPosition.x = -1 * laneDistance; }

        GameObject c = Instantiate(coinPrefab, coinPosition, coinPrefab.transform.rotation);
        activeCoins.Enqueue(c);

        coinNextZPos += 54;
    }

    ObstaclesState GetCurrentObstaclesState() 
    {
        foreach (ObstaclesState state in obstacleStates)
        {
            if(state.IsActive(_timeSinceLevelLoad)) 
            {
                Debug.Log($"LevelGenerator.GetCurrentObstaclesState() _timeSinceLevelLoad > " + state.startTimeActive);
                return state;
            }
        }

        return new ObstaclesState() {
            checkStartTimeActive = false,
            startTimeActive = 0f,
            checkEndTimeActive = false,
            endTimeActive = 0f,
            minObstacleOffset = 10,
            maxObstacleOffset = 15,
            numberOfObstacles = 10,
            numberOfCoins = 25
        };
    }

    #endregion


}