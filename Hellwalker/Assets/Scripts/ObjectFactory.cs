using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Context
{
    public Vector3 position;
    public Quaternion rotation;

    public Context()
    {

    }
    public Context(Vector3 position)
    {
        this.position = position;
    }
    public Context(Quaternion rotation)
    {
        this.rotation = rotation;
    }
    public Context(Vector3 position,Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
}

public abstract class ObjectFactory<T>
{
    public abstract ObjectView Create(Context context);
    public abstract ObjectView Create();

    private static Dictionary<Type, List<T>> SharedPools = new Dictionary<Type, List<T>>();

    protected TKey InstantiateView<TKey>(string path, bool notPoolable = false)
    {
        if(notPoolable)
        {
            return GameObject.Instantiate(Resources.Load(path) as GameObject).GetComponent<TKey>();
        }

        List<T> pool;
        T returnee;

        // If there is a pool available for this object...
        if (SharedPools.TryGetValue(typeof(TKey), out pool))
        {
            returnee = Pooling();

            // If there is no free object in pool...
            if (object.Equals(returnee, default(T)))
            {
                returnee = GameObject.Instantiate(Resources.Load(path) as GameObject).GetComponent<T>();

                // Set name
                (returnee as ObjectView).name += " "+(returnee as ObjectView).GetInstanceID();

                // Add the newly created object to pool
                SharedPools[typeof(TKey)].Add(returnee);
            }
            return (TKey)Convert.ChangeType(returnee, typeof(TKey));
        }
        // If there is no pool available, create one...
        else
        {
            returnee = GameObject.Instantiate(Resources.Load(path) as GameObject).GetComponent<T>();

            // Set name
            (returnee as ObjectView).name += " " + (returnee as ObjectView).GetInstanceID();

            // Make new pool and add to shared pools...
            pool = new List<T>();

            pool.Add(returnee);

            SharedPools.Add(typeof(TKey), pool);

            return (TKey)Convert.ChangeType(returnee, typeof(TKey));
        }
    }

    public T Pooling()
    {
        List<T> pool;
        SharedPools.TryGetValue(typeof(T), out pool);

        for (int i = 0; i < pool.Count; i++)
        {
            // There might  be cases that the object has already been destroyed. In that case, delete it from list
            try
            {
                //... otherwise, get it from list as usual
                if (!(pool[i] as ObjectView).gameObject.activeInHierarchy)
                {
                    (pool[i] as ObjectView).gameObject.SetActive(true);
                    return pool[i];
                }
            }
            catch (Exception e)
            {
                // Expected, nothing no worry about
                Debug.Log(e.Message + " (Expected)");
                // Remove from list
                pool.RemoveAt(i);
            }

        }
        return default;
    }
}
