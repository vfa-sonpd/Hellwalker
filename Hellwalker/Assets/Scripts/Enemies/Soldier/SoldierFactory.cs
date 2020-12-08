using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierFactory<T> : ObjectFactory<T>
{
    //private static List<SoldierView> soldierPool = new List<SoldierView>();
    public override ObjectView Create(Context context)
    {
        SoldierView soldier = CreateSoldier();
        soldier.OnCreate(context);

        soldier.transform.position = context.position;

        return soldier;
    }

    public override ObjectView Create()
    {
        SoldierView soldier = CreateSoldier();

        return soldier;
    }

    private SoldierView CreateSoldier()
    {
        //Load character
        SoldierView soldier = InstantiateView<SoldierView>("Prefabs/Characters/Soldier");

        return soldier;
    }
}
