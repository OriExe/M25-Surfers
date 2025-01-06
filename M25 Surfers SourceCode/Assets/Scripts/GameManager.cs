using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    UnityEvent onDeathEvents;

    public static GameManager Instance;
    // Start is called before the first frame update

    #region ObjectPooling
    [SerializeField]
    bool spawnObsticles = false; //Can spawn Obstacles

    [SerializeField]
    int maxObstaclesAtOnce = 5;

    [SerializeField]
    float delayBetweenObstacleSpawn = 5f;

    [SerializeField]
    Transform[] obstaclePositions = new Transform[3];

    [SerializeField]
    float obstacleSpawnDistance = 20f;

    [SerializeField]
    float obstacleMovementSpeed = 1f;

    [SerializeField]
    List<GameObject> activeObstacles = new List<GameObject>();
    #endregion
    void Awake()
    {
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
        ControlObstacleSpawning();

        MoveObstacles();
    }
    public void playerDeath()
    {
        onDeathEvents.Invoke();
    }
    void SpawnObstacle()
    {
        // Are there too much obstacles
        if (ObjectPool.instance.GetPooledObject() != null
            && ObjectPool.instance.CheckCurrentActiveObsticleCount() < maxObstaclesAtOnce)
        {
            GameObject temp = ObjectPool.instance.GetPooledObject();;

            int spawnedPosition = Random.Range(0, 3);

            temp.transform.position = new Vector3(obstaclePositions[spawnedPosition].position.x, 0, obstacleSpawnDistance);

            temp.SetActive(true);

            activeObstacles.Add(temp);

            Invoke("SpawnObstacle", delayBetweenObstacleSpawn);
        }
    }

    public void ControlObstacleSpawning()
    {
        if (!spawnObsticles)
            return;
        Invoke("SpawnObstacle", delayBetweenObstacleSpawn);

        spawnObsticles = false;
    }

    void MoveObstacles()
    {
        foreach (GameObject obstacle in activeObstacles)
        {
            obstacle.transform.position -= new Vector3(0,0, obstacleMovementSpeed * Time.deltaTime);
        }
    }

}
