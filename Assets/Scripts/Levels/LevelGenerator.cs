using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Digx7.Zygote;

public class LevelGenerator : MonoBehaviour 
{

    #region Variables ================================

    [Header("Variables")]
    public GameObject blockPrefab;
    public GameObject coinPrefab;
    public GameObject[] obstaclePrefabs;
    public ObstaclePairData[] trippleObstaclePairDataPrefabs;
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
    public LayerMask coinPlacementLayerMask;

    public Queue<GameObject> activeBlocks = new Queue<GameObject>();
    public Queue<GameObject> activeObstacles = new Queue<GameObject>();
    public Queue<GameObject> activeCoins = new Queue<GameObject>();
    public Queue<GameObject> standbyCoins = new Queue<GameObject>();

    public List<ObstaclesState> obstacleStates = new List<ObstaclesState>() {
        new ObstaclesState() {
            checkStartTimeActive = true,
            startTimeActive = 120f,
            checkEndTimeActive = false,
            endTimeActive = 120f,
            minObstacleOffset = 3,
            maxObstacleOffset = 5,
            numberOfObstacles = 25,
            chanceOfDoubleObstacles = 0.2f,
            chanceOfTrippleObstacles = 0.05f,
            minCoinLineOffset = 15,
            maxCoinLineOffset = 30,
            numberOfCoinLines = 30
        },
        new ObstaclesState() {
            checkStartTimeActive = true,
            startTimeActive = 60f,
            checkEndTimeActive = false,
            endTimeActive = 60f,
            minObstacleOffset = 7,
            maxObstacleOffset = 10,
            numberOfObstacles = 20,
            chanceOfDoubleObstacles = 0.1f,
            chanceOfTrippleObstacles = 0.05f,
            minCoinLineOffset = 15,
            maxCoinLineOffset = 30,
            numberOfCoinLines = 20
        },
        new ObstaclesState() {
            checkStartTimeActive = true,
            startTimeActive = 30f,
            checkEndTimeActive = false,
            endTimeActive = 30f,
            minObstacleOffset = 8,
            maxObstacleOffset = 12,
            numberOfObstacles = 15,
            chanceOfDoubleObstacles = 0.01f,
            chanceOfTrippleObstacles = 0.05f,
            minCoinLineOffset = 15,
            maxCoinLineOffset = 30,
            numberOfCoinLines = 10
        },
        new ObstaclesState() {
            checkStartTimeActive = false,
            startTimeActive = 0f,
            checkEndTimeActive = false,
            endTimeActive = 0f,
            minObstacleOffset = 10,
            maxObstacleOffset = 15,
            numberOfObstacles = 10,
            chanceOfDoubleObstacles = 0.01f,
            chanceOfTrippleObstacles = 0.05f,
            minCoinLineOffset = 15,
            maxCoinLineOffset = 30,
            numberOfCoinLines = 10
        }
    };

    public Vector3 nextSpawnPoint;
    public int _obstacleNextZPos = 0;

    private float _timeSinceLevelLoad = 0f;
    public int GetNextObstacleZPos() 
    {
        ObstaclesState currentState = GetCurrentObstaclesState();

        _obstacleNextZPos += currentState.GetRandomObstacleOffset();

        return _obstacleNextZPos;        
    }
    public int _coinNextZPos = 0;
    public int GetNextCoinLineZStartPos() 
    {
        ObstaclesState currentState = GetCurrentObstaclesState();

        _coinNextZPos += currentState.GetRandomCoinLineOffset();

        return _coinNextZPos;
    }

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
        
        seed = UnityEngine.Random.Range(1, 99999);

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

        BalanceObstacles(currentState);

        for (int j = 0; j < currentState.numberOfCoinLines; j++)
        {
            InstantiateNewCoinLine();
        }

        // Preloads the coin standby queue to avoid popin when reusing coins for the first time
        for (int i = 0; i < 9; i++)
        {
            Vector3 coinPosition = new Vector3(0f, -30f, 0f);
            
            GameObject coin = Instantiate(coinPrefab, coinPosition, coinPrefab.transform.rotation);
            coin.SetActive(false);
            standbyCoins.Enqueue(coin);
        }

        _isSetup = true;
    }

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
        
        BalanceObstacles(currentState);

        int obstaclesToSpawnAtOneTime = currentState.GetNumberOfObstaclesToSpawnAtOneTime();

        if(obstaclesToSpawnAtOneTime == 1) 
        {
            ReuseSingleObstacle(GetNextObstacleZPos(), currentState);
        }
        else if (obstaclesToSpawnAtOneTime == 2) 
        {
            ReuseDoubleObstacles(GetNextObstacleZPos(), currentState);
        }
        else if (obstaclesToSpawnAtOneTime == 3) 
        {
            ReuseTrippleObstacles(GetNextObstacleZPos(), currentState);
        }
        
    }

    void BalanceObstacles(ObstaclesState currentState)
    {
        // If there are more active obstacles than the current state's number of obstacles, destroy the excess obstacles
        if(activeObstacles.Count > currentState.numberOfObstacles && activeObstacles.Count > 0) 
        {
            GameObject oldObstacleToDelete = activeObstacles.Dequeue();
            Destroy(oldObstacleToDelete);
            return;
        }
        
        // If there are less active obstacles than the current state's number of obstacles, instantiate new obstacles
        if(currentState.numberOfObstacles > activeObstacles.Count) 
        {
            int obstaclesToSpawn = currentState.numberOfObstacles - activeObstacles.Count;
            
            for (int i = activeObstacles.Count; i < obstaclesToSpawn; i++)
            {
                InstantiateNewObstacle();
            }
        }
    }

    void ReuseSingleObstacle(int obstacleOffset, ObstaclesState currentState)
    {
        GameObject oldObstacle = activeObstacles.Dequeue();

        // Select random lane
        int randomLaneIndex = sharedRandom.Next(0, 3);

        // Set obstacle position based on lane and offset
        Vector3 obstaclePosition = new Vector3(0f, oldObstacle.transform.position.y, obstacleOffset);
        obstaclePosition.x = laneIndexToXPos(randomLaneIndex);

        // Set obstacle position and add it back to the active queue
        oldObstacle.transform.position = obstaclePosition;
        activeObstacles.Enqueue(oldObstacle);
    }

    void ReuseDoubleObstacles(int obstacleOffset, ObstaclesState currentState)
    {
        GameObject oldObstacle = activeObstacles.Dequeue();
        int randomObstacleIndex2 = sharedRandom.Next(0, obstaclePrefabs.Length);

        // Select random lane
        int randomLaneIndex = sharedRandom.Next(0, 3);
        int randomLaneIndex2 = -1;
        do 
        {
            randomLaneIndex2 = sharedRandom.Next(0, 3);
        } while (randomLaneIndex2 == randomLaneIndex);

        // Set obstacle position based on lane and offset
        Vector3 obstaclePosition = new Vector3(0f, oldObstacle.transform.position.y, obstacleOffset);
        obstaclePosition.x = laneIndexToXPos(randomLaneIndex);

        Vector3 obstaclePosition2 = new Vector3(0f, obstaclePrefabs[randomObstacleIndex2].transform.position.y, obstacleOffset);
        obstaclePosition2.x = laneIndexToXPos(randomLaneIndex2);

        // Set obstacle position and add it back to the active queue
        oldObstacle.transform.position = obstaclePosition;
        activeObstacles.Enqueue(oldObstacle);

        GameObject oldObstacle2 = Instantiate(obstaclePrefabs[randomObstacleIndex2], obstaclePosition2, obstaclePrefabs[randomObstacleIndex2].transform.rotation);
        activeObstacles.Enqueue(oldObstacle2);
    }

    void ReuseTrippleObstacles(int obstacleOffset, ObstaclesState currentState)
    {
        GameObject oldObstacleToDelete = activeObstacles.Dequeue();
        Destroy(oldObstacleToDelete);

        InstantiateTrippleObstacles(obstacleOffset, currentState);
    }

    void ReuseCoin()
    {
        GameObject oldCoin = activeCoins.Dequeue();
        standbyCoins.Enqueue(oldCoin);

        if(standbyCoins.Count >= 10)
        {
            List<GameObject> coinsToReuse = new List<GameObject>();
            for (int i = 0; i < 10; i++)
            {
                coinsToReuse.Add(standbyCoins.Dequeue());
            }
            StartCoroutine(ReuseCoinsInNewLine(coinsToReuse, GetNextCoinLineZStartPos(), sharedRandom.Next(0, numberOfLanes)));
        }
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
        ObstaclesState currentState = GetCurrentObstaclesState();
        int obstacleOffset = GetNextObstacleZPos();
        int numberOfObstaclesToSpawn = currentState.GetNumberOfObstaclesToSpawnAtOneTime();

        if(numberOfObstaclesToSpawn == 1) 
        {
            InstantiateSingleObstacle(obstacleOffset, currentState);
        }
        else if (numberOfObstaclesToSpawn == 2) 
        {
            InstantiateDoubleObstacles(obstacleOffset, currentState);
        }
        else if (numberOfObstaclesToSpawn == 3) 
        {
            InstantiateTrippleObstacles(obstacleOffset, currentState);
        }

        // bool shouldSpawnDoubleObstacles = currentState.ShouldSpawnDoubleObstacles();

        // // Select random obstacle
        // int randomObstacleIndex = sharedRandom.Next(0, obstaclePrefabs.Length);
        
        // // Select random lane
        // int randomLaneIndex = sharedRandom.Next(0, numberOfLanes);

        // // Set obstacle position based on lane and offset
        // Vector3 obstaclePosition = new Vector3(0f, obstaclePrefabs[randomObstacleIndex].transform.position.y, obstacleOffset);
        // if (randomLaneIndex == 0) { obstaclePosition.x = laneDistance; }
        // else if (randomLaneIndex == 1) { obstaclePosition.x = 0; }
        // else if (randomLaneIndex == 2) { obstaclePosition.x = -1 * laneDistance; }

        // // Instantiate obstacle and add to active queue
        // GameObject o = Instantiate(obstaclePrefabs[randomObstacleIndex], obstaclePosition, obstaclePrefabs[randomObstacleIndex].transform.rotation);
        // activeObstacles.Enqueue(o);

        // if(currentState.ShouldSpawnDoubleObstacles()) 
        // {
        //     // Select random obstacle
        //     int randomObstacleIndex2 = sharedRandom.Next(0, obstaclePrefabs.Length);
            
        //     // Select random lane for second obstacle, ensuring it's different from the first obstacle's lane
        //     int randomLaneIndex2 = -1;
        //     do 
        //     {
        //         randomLaneIndex2 = sharedRandom.Next(0, numberOfLanes);
        //     } while (randomLaneIndex2 == randomLaneIndex);

        //     // Set obstacle position based on lane and offset
        //     Vector3 obstaclePosition2 = new Vector3(0f, obstaclePrefabs[randomObstacleIndex2].transform.position.y, obstacleOffset);
        //     if (randomLaneIndex2 == 0) { obstaclePosition2.x = laneDistance; }
        //     else if (randomLaneIndex2 == 1) { obstaclePosition2.x = 0; }
        //     else if (randomLaneIndex2 == 2) { obstaclePosition2.x = -1 * laneDistance; }

        //     // Instantiate obstacle and add to active queue
        //     GameObject o2 = Instantiate(obstaclePrefabs[randomObstacleIndex2], obstaclePosition2, obstaclePrefabs[randomObstacleIndex2].transform.rotation);
        //     activeObstacles.Enqueue(o2);
        // }
    }

    void InstantiateSingleObstacle(int obstacleOffset, ObstaclesState currentState) 
    {
        // Select random obstacle
        int randomObstacleIndex = sharedRandom.Next(0, obstaclePrefabs.Length);
        
        // Select random lane
        int randomLaneIndex = sharedRandom.Next(0, numberOfLanes);

        // Set obstacle position based on lane and offset
        Vector3 obstaclePosition = new Vector3(0f, obstaclePrefabs[randomObstacleIndex].transform.position.y, obstacleOffset);
        obstaclePosition.x = laneIndexToXPos(randomLaneIndex);

        // Instantiate obstacle and add to active queue
        GameObject o = Instantiate(obstaclePrefabs[randomObstacleIndex], obstaclePosition, obstaclePrefabs[randomObstacleIndex].transform.rotation);
        activeObstacles.Enqueue(o);
    }

    void InstantiateDoubleObstacles(int obstacleOffset, ObstaclesState currentState) 
    {
        // Select random obstacle
        int randomObstacleIndex = sharedRandom.Next(0, obstaclePrefabs.Length);
        
        // Select random lane
        int randomLaneIndex = sharedRandom.Next(0, numberOfLanes);

        // Set obstacle position based on lane and offset
        Vector3 obstaclePosition = new Vector3(0f, obstaclePrefabs[randomObstacleIndex].transform.position.y, obstacleOffset);
        obstaclePosition.x = laneIndexToXPos(randomLaneIndex);

        // Instantiate obstacle and add to active queue
        GameObject o = Instantiate(obstaclePrefabs[randomObstacleIndex], obstaclePosition, obstaclePrefabs[randomObstacleIndex].transform.rotation);
        activeObstacles.Enqueue(o);

        // Select random obstacle
        int randomObstacleIndex2 = sharedRandom.Next(0, obstaclePrefabs.Length);
        
        // Select random lane for second obstacle, ensuring it's different from the first obstacle's lane
        int randomLaneIndex2 = -1;
        do 
        {
            randomLaneIndex2 = sharedRandom.Next(0, numberOfLanes);
        } while (randomLaneIndex2 == randomLaneIndex);

        // Set obstacle position based on lane and offset
        Vector3 obstaclePosition2 = new Vector3(0f, obstaclePrefabs[randomObstacleIndex2].transform.position.y, obstacleOffset);
        obstaclePosition2.x = laneIndexToXPos(randomLaneIndex2);

        // Instantiate obstacle and add to active queue
        GameObject o2 = Instantiate(obstaclePrefabs[randomObstacleIndex2], obstaclePosition2, obstaclePrefabs[randomObstacleIndex2].transform.rotation);
        activeObstacles.Enqueue(o2);
    }

    void InstantiateTrippleObstacles(int obstacleOffset, ObstaclesState currentState) 
    {
        // Select random tripple obstacle pair
        int randomTrippleObstaclePairIndex = sharedRandom.Next(0, trippleObstaclePairDataPrefabs.Length);

        // Assign lanes for the tripple obstacles
        List<int> laneIndices = new List<int>() { 0, 1, 2 };
        laneIndices = laneIndices.Randomize().ToList();

        // Set obstacle position based on lane and offset
        Vector3 obstaclePosition1 = new Vector3(0f, trippleObstaclePairDataPrefabs[randomTrippleObstaclePairIndex].obstaclePrefab1.transform.position.y, obstacleOffset);
        obstaclePosition1.x = laneIndexToXPos(laneIndices[0]);

        Vector3 obstaclePosition2 = new Vector3(0f, trippleObstaclePairDataPrefabs[randomTrippleObstaclePairIndex].obstaclePrefab2.transform.position.y, obstacleOffset);
        obstaclePosition2.x = laneIndexToXPos(laneIndices[1]);

        Vector3 obstaclePosition3 = new Vector3(0f, trippleObstaclePairDataPrefabs[randomTrippleObstaclePairIndex].obstaclePrefab3.transform.position.y, obstacleOffset);
        obstaclePosition3.x = laneIndexToXPos(laneIndices[2]);

        // Instantiate obstacles and add to active queue
        GameObject o1 = Instantiate(trippleObstaclePairDataPrefabs[randomTrippleObstaclePairIndex].obstaclePrefab1, obstaclePosition1, trippleObstaclePairDataPrefabs[randomTrippleObstaclePairIndex].obstaclePrefab1.transform.rotation);
        activeObstacles.Enqueue(o1);

        GameObject o2 = Instantiate(trippleObstaclePairDataPrefabs[randomTrippleObstaclePairIndex].obstaclePrefab2, obstaclePosition2, trippleObstaclePairDataPrefabs[randomTrippleObstaclePairIndex].obstaclePrefab2.transform.rotation);
        activeObstacles.Enqueue(o2);

        GameObject o3 = Instantiate(trippleObstaclePairDataPrefabs[randomTrippleObstaclePairIndex].obstaclePrefab3, obstaclePosition3, trippleObstaclePairDataPrefabs[randomTrippleObstaclePairIndex].obstaclePrefab3.transform.rotation);
        activeObstacles.Enqueue(o3);

    }

    void InstantiateNewCoinLine()
    {
        int randomLaneIndex = sharedRandom.Next(0, numberOfLanes);
        int coinLineZStartPos = GetNextCoinLineZStartPos();

        StartCoroutine(InstantiateCoinLine(10, coinLineZStartPos, randomLaneIndex));
    }

    IEnumerator InstantiateCoinLine(int numberOfCoinsToSpawn, int startingZPos, int laneIndex)
    {
        int startingLaneIndex = laneIndex;

        Vector3 coinPosition = FindCoinPosition(coinPrefab.transform.position.y, startingZPos, ref startingLaneIndex);

        List<float> valideLaneIndecies = GetValidLaneIndeciesForCoinLine(startingLaneIndex);

        GameObject firstCoin = Instantiate(coinPrefab, coinPosition, coinPrefab.transform.rotation);
        activeCoins.Enqueue(firstCoin);

        yield return new WaitForSeconds(0.01f);

        for (int i = 1; i < numberOfCoinsToSpawn; i++)
        {

            coinPosition = FindCoinPosition(coinPrefab.transform.position.y, startingZPos + i, valideLaneIndecies, out float selectedLaneIndex);

            valideLaneIndecies = GetValidLaneIndeciesForCoinLine(selectedLaneIndex);

            if(coinPosition.y > 2f) 
            {
                // If the coin position is too high, skip spawning this coin and continue to the next one
                yield return new WaitForSeconds(0.01f);
                continue;
            }

            GameObject c = Instantiate(coinPrefab, coinPosition, coinPrefab.transform.rotation);
            activeCoins.Enqueue(c);

            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator ReuseCoinsInNewLine(List<GameObject> coinsToReuse, int startingZPos, int laneIndex)
    {
        int startingLaneIndex = laneIndex;

        Vector3 coinPosition = FindCoinPosition(coinPrefab.transform.position.y, startingZPos, ref startingLaneIndex);

        List<float> valideLaneIndecies = GetValidLaneIndeciesForCoinLine(startingLaneIndex);

        GameObject coin = coinsToReuse[0];

        

        coin.transform.position = coinPosition;
        if (!coin.activeSelf)
        {
            coin.SetActive(true);
        }
        activeCoins.Enqueue(coin);

        yield return new WaitForSeconds(0.01f);

        for (int i = 1; i < coinsToReuse.Count; i++)
        {
            coin = coinsToReuse[i];

            coinPosition = FindCoinPosition(coinPrefab.transform.position.y, startingZPos + i, valideLaneIndecies, out float selectedLaneIndex);

            valideLaneIndecies = GetValidLaneIndeciesForCoinLine(selectedLaneIndex);

            if(coinPosition.y > 2f) 
            {
                // If the coin position is too high, skip spawning this coin and continue to the next one
                standbyCoins.Enqueue(coin);
                yield return new WaitForSeconds(0.01f);
                continue;
            }

            coin.transform.position = coinPosition;
            if (!coin.activeSelf)
            {
                coin.SetActive(true);
            }
            activeCoins.Enqueue(coin);

            yield return new WaitForSeconds(0.01f);
        }
    }

    private Vector3 FindCoinPosition(float startingY, float startingZ, ref int laneIndex, float maxCoinHeight = 2f)
    {
        float coinPositionY = startingY;
        float coinPositionZ = startingZ;
        float coinPositionX = 0f;

        bool foundValidPosition = false;

        coinPositionX = laneIndexToXPos(laneIndex);


        for (int i = 0; i < numberOfLanes; i++) 
        {
            Ray ray = new Ray(new Vector3(coinPositionX, coinPositionY + 20f, coinPositionZ), Vector3.down);

            // Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 60f);
            // DebugUtilities.DrawPrimitive(ray.origin, 0.2f, PrimitiveType.Sphere, Color.red, 60f);

            if(Physics.Raycast(ray, out RaycastHit hitInfo, 60f, coinPlacementLayerMask)) 
            {
                // DebugUtilities.DrawPrimitive(hitInfo.point, 0.2f, PrimitiveType.Sphere, Color.green, 60f);

                coinPositionY = hitInfo.point.y + coinPrefab.transform.position.y;
            }
            else
            {
                coinPositionY = coinPrefab.transform.position.y;
            }

            if(coinPositionY < maxCoinHeight)
            {
                foundValidPosition = true;
                break;
            }
            else 
            {
                // Retry in a different lane
                laneIndex++;

                if (laneIndex >= numberOfLanes) { laneIndex = 0; }

                coinPositionX = laneIndexToXPos(laneIndex);
            }
        }


        Vector3 coinPosition = new Vector3(coinPositionX, coinPositionY, coinPositionZ);

        return coinPosition;
    }

    private Vector3 FindCoinPosition(float startingY, float startingZ, List<float> valideLaneIndecies, out float selectedLaneIndex, float maxCoinHeight = 2f)
    {
        float coinPositionY = startingY;
        float coinPositionZ = startingZ;
        float coinPositionX = 0f;
        selectedLaneIndex = -1f;

        bool foundValidPosition = false;



        for (int i = 0; i < valideLaneIndecies.Count; i++) 
        {
            coinPositionX = laneIndexToXPos_includeBetweenLanes(valideLaneIndecies[i]);
            selectedLaneIndex = valideLaneIndecies[i];
            
            Ray ray = new Ray(new Vector3(coinPositionX, coinPositionY + 20f, coinPositionZ), Vector3.down);

            // Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 60f);
            // DebugUtilities.DrawPrimitive(ray.origin, 0.2f, PrimitiveType.Sphere, Color.red, 60f);

            if(Physics.Raycast(ray, out RaycastHit hitInfo, 60f, coinPlacementLayerMask)) 
            {
                // DebugUtilities.DrawPrimitive(hitInfo.point, 0.2f, PrimitiveType.Sphere, Color.green, 60f);

                coinPositionY = hitInfo.point.y + coinPrefab.transform.position.y;
            }
            else
            {
                coinPositionY = coinPrefab.transform.position.y;
            }

            if(coinPositionY < maxCoinHeight)
            {
                foundValidPosition = true;
                break;
            }
        }


        Vector3 coinPosition = new Vector3(coinPositionX, coinPositionY, coinPositionZ);

        return coinPosition;
    }

    float laneIndexToXPos(int laneIndex) 
    {
        if (laneIndex == 0) { return laneDistance; }
        else if (laneIndex == 1) { return 0; }
        else if (laneIndex == 2) { return -1 * laneDistance; }

        return 0f;
    }

    float laneIndexToXPos_includeBetweenLanes(float laneIndex) 
    {
        if (laneIndex == 0) { return laneDistance; }
        else if (laneIndex == 1) { return 0; }
        else if (laneIndex == 2) { return -1 * laneDistance; }
        else if (laneIndex > 0 && laneIndex < 1) 
        {
            // Between lane 0 and lane 1
            return Mathf.Lerp(laneDistance, 0, laneIndex);
        }
        else if (laneIndex > 1 && laneIndex < 2) 
        {
            // Between lane 1 and lane 2
            return Mathf.Lerp(0, -1 * laneDistance, laneIndex - 1);
        }

        return 0f;
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
            chanceOfDoubleObstacles = 0.01f,
            chanceOfTrippleObstacles = 0.05f,
            minCoinLineOffset = 40,
            maxCoinLineOffset = 60,
            numberOfCoinLines = 4
        };
    }

    List<float> GetValidLaneIndeciesForCoinLine(int startingLaneIndex)
    {
        List<float> valideLaneIndecies = new List<float>() { 0f, 1f, 2f };

        switch (startingLaneIndex)
        {
            case 0:
                valideLaneIndecies = new List<float>() { 0f, 0.5f};
                break;
            case 1:
                valideLaneIndecies = new List<float>() { 1f, 0.5f, 1.5f};
                break;
            case 2:
                valideLaneIndecies = new List<float>() { 2f, 1.5f};
                break;
            default:
                break;
        }

        return valideLaneIndecies;
    }

    List<float> GetValidLaneIndeciesForCoinLine(float startingLaneIndex)
    {
        List<float> valideLaneIndecies = new List<float>() { 0f, 1f, 2f };

        switch (startingLaneIndex)
        {
            case 0f:
                    valideLaneIndecies = new List<float>() { 0f, 0.5f, 1f };
                    break;
                case 0.5f:
                    valideLaneIndecies = new List<float>() { 0f, 1f};
                    break;
                case 1f:
                    valideLaneIndecies = new List<float>() { 1f, 0.5f, 0f, 1.5f, 2f};
                    break;
                case 1.5f:
                    valideLaneIndecies = new List<float>() { 1f, 2f };
                    break;
                case 2f:
                    valideLaneIndecies = new List<float>() { 2f, 1.5f, 1f};
                    break;
                default:
                    break;
        }

        return valideLaneIndecies;
    }

    #endregion


}