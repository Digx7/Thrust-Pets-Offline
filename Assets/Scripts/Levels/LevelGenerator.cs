using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public float obstacleSpawnChance = 0.3f;
    public float reuseDistance = 30f;

    public Queue<GameObject> activeBlocks = new Queue<GameObject>();
    public Queue<GameObject> activeObstacles = new Queue<GameObject>();
    public Queue<GameObject> activeCoins = new Queue<GameObject>();

    public Vector3 nextSpawnPoint;
    public int obstacleNextZPos = 0;
    public int coinNextZPos = 0;

    public int seed = 0;
    public System.Random sharedRandom;

    #endregion

    #region Setup ================================

    #endregion

    #region Channel Responses ================================

    #endregion

    #region Main Functions ================================

    void Start()
    {
        
        seed = Random.Range(1, 99999);

        sharedRandom = new System.Random(seed);

        nextSpawnPoint = transform.position;

       /* for (int i = 0; i < numberOfBlocks; i++)
        {
            SpawnNewBlock();
        }*/

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

        for (int i = 0; i < 50; i++)
        {
            int r = sharedRandom.Next(0, obstaclePrefabs.Length);
            int rTwo = sharedRandom.Next(0, 3);

            Vector3 obstaclePosition = new Vector3(0f, obstaclePrefabs[r].transform.position.y, obstacleNextZPos);
            if (rTwo == 0) { obstaclePosition.x = 3; }
            else if (rTwo == 1) { obstaclePosition.x = 0; }
            else if (rTwo == 2) { obstaclePosition.x = -3; }

            Vector3 coinPosition = new Vector3(obstaclePosition.x, coinPrefab.transform.position.y, coinNextZPos);
            
            GameObject o = Instantiate(obstaclePrefabs[r], obstaclePosition, obstaclePrefabs[r].transform.rotation);
            activeObstacles.Enqueue(o);

            GameObject c = Instantiate(coinPrefab, coinPosition, coinPrefab.transform.rotation);
            activeCoins.Enqueue(c);

            obstacleNextZPos += 15;
            coinNextZPos += 54;
        }
    }

    // TODO: add 7 blocks to the Map in the scene

    void Update()
    {
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
    }

    void SpawnNewBlock()
    {
        GameObject newBlock = Instantiate(blockPrefab, nextSpawnPoint, Quaternion.identity);
        activeBlocks.Enqueue(newBlock);
        nextSpawnPoint.z += blockLength;

        /*if (Random.value < obstacleSpawnChance)
        {
            SpawnObstacle(newBlock.transform.position);
        }*/
    }

    void ReuseObstacle() 
    {
        GameObject oldObstacle = activeObstacles.Dequeue();
        int rTwo = sharedRandom.Next(0, 3);

        Vector3 obstaclePosition = new Vector3(0f, oldObstacle.transform.position.y, obstacleNextZPos);
        if (rTwo == 0) { obstaclePosition.x = 3; }
        else if (rTwo == 1) { obstaclePosition.x = 0; }
        else if (rTwo == 2) { obstaclePosition.x = -3; }

        oldObstacle.transform.position = obstaclePosition;
        obstacleNextZPos += 15;
        activeObstacles.Enqueue(oldObstacle);

        if (player.position.z > activeCoins.Peek().transform.position.z)
        {
            GameObject oldCoin = activeCoins.Dequeue();

            Vector3 coinPosition = new Vector3(obstaclePosition.x, oldCoin.transform.position.y, coinNextZPos);
            if (oldCoin.activeSelf == false)
            {
                oldCoin.SetActive(true);
            }

            oldCoin.transform.position = coinPosition;
            coinNextZPos += 54;
            activeCoins.Enqueue(oldCoin);
        }
    }

    void ReuseBlock()
    {
        GameObject oldBlock = activeBlocks.Dequeue();
        oldBlock.transform.position = nextSpawnPoint;
        nextSpawnPoint.z += blockLength;
        activeBlocks.Enqueue(oldBlock);
/*
        ClearOldObstacles(oldBlock);

        if (Random.value < obstacleSpawnChance)
        {
            SpawnObstacle(oldBlock.transform.position);
        }*/
    }

    void SpawnObstacle(Vector3 blockPosition)
    {
     //   Vector3 obstaclePosition = new Vector3(blockPosition.x, obstaclePrefab.transform.position.y, blockPosition.z);
    //    Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity);
    }

    void ClearOldObstacles(GameObject block)
    {
       /* foreach (Transform child in block.transform)
        {
            if (child.CompareTag("Obstacle"))
            {
                Destroy(child.gameObject);
            }
        }*/
    }

    #endregion


}