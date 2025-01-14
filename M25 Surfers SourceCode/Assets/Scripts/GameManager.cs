using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public bool gameStarted {get; private set;}
    [SerializeField]
    UnityEvent onDeathEvents;

    public static GameManager Instance;
    // Start is called before the first frame update

    #region ObjectPooling
   

    [SerializeField]
    float delayBetweenObstacleSpawn = 5f;

    [SerializeField]
    Transform[] obstaclePositions = new Transform[3];

    /// <summary>
    /// Gets the all possible positions of the player. Due to how this gets this value it is recommend to make a another variable to store the results of this in to improve peformance.
    /// 
    /// 0 == Left
    /// 1 == Middle
    /// 2 == Right 
    /// </summary>
    public static Vector3[] PlayerPositionsStatic
    {
        get {
            Vector3[] temp = new Vector3[3];
            for (int i = 0; i < Instance.obstaclePositions.Length; i++)
            {
                temp[i] = Instance.obstaclePositions[i].position;
            }
            return temp;
            }
        private set {  }
    }
    [SerializeField]
    float obstacleSpawnDistance = 20f;

    [SerializeField]
    float obstacleMovementSpeed = 1f;

    [SerializeField]
    List<GameObject> activeObstacles = new List<GameObject>();

    int currentObj = 0;
    #endregion

    #region Difficulty
    [SerializeField]
    float currentDifficulty = 1f;

    [SerializeField]
    float rateDifficulty = 0.1f;

    [SerializeField]
    float rateDecreaseSpeed = 0.01f;
    #endregion

    [SerializeField]
    Vector2 rateRange = new Vector2(0.01f, 0.1f);
    void Awake()
    {
        gameStarted = false;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Duplicate Detected");
        }

       
    }

  
    private void Update()
    {
        MoveObstacles();

        controlDifficulty();
    }
    public void playerDeath()
    {
        onDeathEvents.Invoke();

        ControlGameState(false);
    }
    /// <summary>
    /// Spawn platform
    /// </summary>
    void SpawnObstacle()
    {
        if (gameStarted == false)
            return;
        GameObject temp = ObjectPool.instance.GetPooledObject();
        Debug.Log("Running 1");
        // Are there too much obstacles
        if (temp != null && ObjectPool.instance.CheckCurrentActiveObsticleCount() < ObjectPool.instance.getNoOfObjects())
        {
            Debug.Log("Running 2");

            int spawnedPosition = UnityEngine.Random.Range(0, 3);

            temp.transform.position = new Vector3(obstaclePositions[spawnedPosition].position.x, temp.transform.position.y, obstacleSpawnDistance);

            temp.SetActive(true);

            activeObstacles.Add(temp);

             //Makes the game move faster
        }
        else if (ObjectPool.instance.CheckCurrentActiveObsticleCount() >= ObjectPool.instance.getNoOfObjects())
        {
            currentObj++;
            currentObj = currentObj % ObjectPool.instance.getNoOfObjects();
            print(currentObj); 

            temp = activeObstacles[currentObj];
            Debug.Log("Running 3");

            int spawnedPosition = UnityEngine.Random.Range(0, 3);

            temp.transform.position = new Vector3(obstaclePositions[spawnedPosition].position.x, temp.transform.position.y, obstacleSpawnDistance);
        }
        try
        {
            temp.GetComponent<ObstableAction>().action();
        }
        catch (NullReferenceException) 
        {
            print("Object does not have action");
        }
        Invoke("SpawnObstacle", delayBetweenObstacleSpawn / currentDifficulty);
    }

    
    //Move Objects repeatly
    void MoveObstacles()
    {
        foreach (GameObject obstacle in activeObstacles)
        {
            obstacle.transform.position -= new Vector3(0,0, obstacleMovementSpeed * Time.deltaTime) * currentDifficulty;
        }
       
    }
    public void ControlGameState(bool state)
    {
        gameStarted = state;
        if (state)
        {
            SpawnObstacle();
        }
        else
        {
            gameStarted = false;
        }
    }

    void controlDifficulty()
    {
        if (!gameStarted)
            return;

        currentDifficulty += Time.deltaTime * rateDifficulty;

        rateDifficulty -= Time.deltaTime * rateDecreaseSpeed;

        rateDifficulty = Mathf.Clamp(rateDifficulty, rateRange.x, rateRange.y);
    }

    public float getDifficulty()
    {
        return currentDifficulty;
    }
}
