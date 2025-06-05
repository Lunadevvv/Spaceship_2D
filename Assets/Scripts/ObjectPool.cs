using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance; // Create an instance to use public variable (Singleton)
    
    // This class is a placeholder for the object pool system.
    // It can be used to manage the pooling of game objects to optimize performance.
    public GameObject prefab;
    public int poolSize = 5;
    private List<GameObject> pool;
    private void Awake()
    {
        if (Instance != null) // Instance already existed. Make sure not duplicate
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObject();
        }
    }

    private GameObject CreateNewObject()
    {
        GameObject obj = Instantiate(prefab, transform);
        obj.SetActive(false);
        pool.Add(obj);
        return obj;
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeSelf)
            {
                return obj;
            }
        }
        // If no inactive object is found, create a new one
        return CreateNewObject();
    }

}
