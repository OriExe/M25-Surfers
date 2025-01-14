using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is litterly just a mix of code from the object pooling script and game manager
public class BackgroundPool : MonoBehaviour
{
    [SerializeField]
    List<GameObject> pooledObjects;

    private int currentObj;

    [SerializeField]private float delayBetweenObstacleSpawn;
    [SerializeField] private float currentDifficulty;
    [SerializeField] private float rateDifficulty;
    [SerializeField] private float rateDecreaseSpeed;
    [SerializeField] private Vector2 rateRange;
    public static BackgroundPool instance;

    [SerializeField] private Transform[] startPoints;

    [SerializeField] private float backgroundMovementSpeed;   
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Duplicate Detected");
        }
    }
    private void Start()
    {
        GameObject.FindGameObjectsWithTag("Background");
        pooledObjects = new List<GameObject>();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Background"))
        {
            pooledObjects.Add(obj);
        }
        Invoke("SpawnObstacle", 5f);
    }

   


    // Update is called once per frame
    void Update()
    {
        controlDifficulty();

        MoveObstacles();
    }

 
    void MoveObstacles()
    {
        foreach (GameObject obstacle in pooledObjects)
        {
            obstacle.transform.position -= new Vector3(0, 0, backgroundMovementSpeed * Time.deltaTime) * currentDifficulty;
        }
    }

    void SpawnObstacle()
    {
            currentObj = (currentObj + 1) % (pooledObjects.Count);
        
            GameObject temp = pooledObjects[currentObj];
            Debug.Log("Running 3");

            int spawnedPosition = Random.Range(0, 2);

            temp.transform.position = new Vector3(startPoints[spawnedPosition].position.x, temp.transform.position.y, startPoints[spawnedPosition].position.z);
        
            Invoke("SpawnObstacle", delayBetweenObstacleSpawn / currentDifficulty);
    }

    void controlDifficulty()
    {
        if (!GameManager.Instance.gameStarted)
            return;

        currentDifficulty += Time.deltaTime * rateDifficulty;

        rateDifficulty -= Time.deltaTime * rateDecreaseSpeed;

        rateDifficulty = Mathf.Clamp(rateDifficulty, rateRange.x, rateRange.y);
    }
}
