using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Context
{

}

public abstract class ObjectFactory
{
    public abstract ObjectView Create(Context context);
    public abstract ObjectView Create();

    protected T InstantiateView<T>(string path)
    {
        return GameObject.Instantiate(Resources.Load(path) as GameObject).GetComponent<T>();
    }
}
