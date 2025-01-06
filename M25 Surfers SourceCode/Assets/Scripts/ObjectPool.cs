using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    [SerializeField]
    List<GameObject> pooledObjects;

    [SerializeField]
    GameObject objectToPool;

    [SerializeField]
    int amountToPool;

    public static ObjectPool instance;
    
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
        pooledObjects = new List<GameObject>();

        GameObject tmp;

        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);

            tmp.SetActive(false);

            pooledObjects.Add(tmp);
        }

    }

    public GameObject GetPooledObject()
    {
        for (int i = 0;i < amountToPool;i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    public int CheckCurrentActiveObsticleCount()
    {
        int temp = 0;
        for (int i = 0; i < amountToPool; i++)
        {
            if (pooledObjects[i].activeInHierarchy)
            {
                temp++;
            }
        }
        return temp;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
