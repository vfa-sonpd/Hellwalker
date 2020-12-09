using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPartsFactory<T> : ObjectFactory<T>
{
    public override ObjectView Create(Context context)
    {
        HitPartsView hp = CreateHitParts();

        hp.OnCreate(context);

        return hp;
    }

    public override ObjectView Create()
    {
        return CreateHitParts();
    }

    private HitPartsView CreateHitParts()
    {
        //Load character
        HitPartsView gib = InstantiateView<HitPartsView>("Prefabs/Effects/HitParts");

        return gib;
    }
}
