using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 10;
    public bool canExpand = true;

    private List<GameObject> pool;

    private void Start()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = pool.Count - 1; i >= 0; i--)
        {
            GameObject obj = pool[i];
            if (obj == null)
            {
                pool.RemoveAt(i);
                continue;
            }

            if (!obj.activeSelf)
                return obj;
        }

        if (canExpand)
        {
            GameObject newObj = Instantiate(prefab, transform);
            newObj.SetActive(false);
            pool.Add(newObj);
            return newObj;
        }

        return null;
    }
}
