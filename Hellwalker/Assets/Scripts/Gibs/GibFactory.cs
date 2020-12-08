using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GibContext : Context
{
    public float dampenValue;

    public GibContext(Vector3 position, Quaternion rotation, float dampenValue)
    {
        this.position = position;
        this.rotation = rotation;
        this.dampenValue = dampenValue;
    }
}
public class GibFactory<T> : ObjectFactory<T>
{
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
        GibView gib = InstantiateView<GibView>("Prefabs/Effects/Gib");

        return gib;
    }


}
