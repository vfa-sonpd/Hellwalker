using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GibFactory<T> : ObjectFactory<T>
{
    private static List<GibView> gibPool = new List<GibView>();

    public override ObjectView Create(Context context)
    {
        GibView gib = CreateGib();
        gib.OnCreate(context);

        gib.transform.position = context.position;

        return gib;
    }

    public override ObjectView Create()
    {
        GibView gib = CreateGib();

        return gib;
    }

    private GibView CreateGib()
    {
        //Load character
        GibView soldier = Pooling();
        if (!soldier)
        {
            soldier = InstantiateView<GibView>("Prefabs/Effects/Gib");
            gibPool.Add(soldier);
        }
        return soldier;
    }

    public GibView Pooling()
    {
        for (int i = 0; i < gibPool.Count; i++)
        {
            if (!gibPool[i].gameObject.activeInHierarchy)
            {
                gibPool[i].gameObject.SetActive(true);
                return gibPool[i];
            }
        }
        return null;
    }
}
