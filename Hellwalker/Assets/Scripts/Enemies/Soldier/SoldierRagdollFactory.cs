using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoldierRagdollContext : Context
{
    public Vector3 ragdollVelocity;

    public SoldierRagdollContext(Vector3 position, Quaternion rotation, Vector3 ragdollVelocity)
    {
        this.position = position;
        this.rotation = rotation;
        this.ragdollVelocity = ragdollVelocity;
    }
}
public class SoldierRagdollFactory<T> : ObjectFactory<T>
{
    public override ObjectView Create(Context context)
    {
        SoldierRagdollView ragdoll = CreateSoldier();

        ragdoll.OnCreate(context);

        return ragdoll;
    }

    public override ObjectView Create()
    {
        SoldierRagdollView ragdoll = CreateSoldier();

        return ragdoll;
    }

    private SoldierRagdollView CreateSoldier()
    {
        //Load character
        SoldierRagdollView ragdoll = InstantiateView<SoldierRagdollView>("Prefabs/Characters/SoldierRagdoll");

        return ragdoll;
    }
}
