using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour, PoolableObject
{
    private Queue<T> pool = new Queue<T>();
    private Func<T> createMethod;
    public ObjectPool(Func<T> createMethod, int capacity)
    {
        if (this.createMethod == null)
            this.createMethod = createMethod;
        for (int i = 0; i < capacity; i++)
        {
            CreateObject();
        }
    }
    // Create new Object
    private T CreateObject()
    {
        T obj = createMethod();
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
        return obj;
    }

    // Get Object From Pool
    public T GetFromPool()
    {
        if (pool.Count == 0)
        {
            return createMethod();
        }

        T obj = pool.Dequeue();
        obj.gameObject.SetActive(true);
        obj.OnGameObjectSpawn();
        return obj;
    }


    // Return the game object to pool
    public void ReturnToPool(T obj)
    {
        obj.OnGameObjectReturn();
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}
