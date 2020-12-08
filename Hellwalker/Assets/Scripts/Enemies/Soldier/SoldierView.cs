using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierView : ObjectView
{
    [Header("References")]
    [SerializeField] private DestructibleObjectScript destructible;
    [SerializeField] private float initialHP;
    public override void OnCreate(Context context)
    {
        destructible.doragdoll = false;
        destructible.myhealth = initialHP;
    }

    // Start is called before the first frame update
    void Awake()
    {
        destructible = this.GetComponent<DestructibleObjectScript>();
        initialHP = destructible.myhealth;
    }

    /// <summary>
    /// Kill this object
    /// </summary>
    public void Suicide(bool doragdoll = false)
    {
        if(doragdoll)
        {
            destructible.doragdoll = true;
        }
        destructible.myhealth = 0;
    }
}
