using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class obstacle  {
    [SerializeField] private GameObject Object;
    [SerializeField] private int Amount;

    public GameObject GetObj()
    {
        return Object;
    }
    public int GetAmount()
    {
        return Amount;
    }
    //To ensure it doesn't add more objects than necessary
    public void removeOne()
    {
        Amount--;
    }
}

public class ObjectPool : MonoBehaviour
{

    [SerializeField]
    List<GameObject> pooledObjects;

    [SerializeField] private obstacle[] objectToPool;

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
        int tempAmountToPool = 0;
        //Counts up how much objects are needed
        foreach (var obj in objectToPool)
        {
            tempAmountToPool+= obj.GetAmount();
        }
        amountToPool = tempAmountToPool;
        pooledObjects = new List<GameObject>();

        GameObject tmp;

        for (int i = 0; i < amountToPool; i++) 
        {
            int randomNum = Random.Range(0, objectToPool.Length);
            while (objectToPool[randomNum].GetAmount() <= 0) //Max amount of objects used
            {
                randomNum = Random.Range(0, objectToPool.Length);
            }
            tmp = Instantiate(objectToPool[randomNum].GetObj());

            tmp.SetActive(false);

            pooledObjects.Add(tmp);
            objectToPool[randomNum].removeOne();
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

    public int getNoOfObjects()
    {
        return amountToPool;
    }
}
