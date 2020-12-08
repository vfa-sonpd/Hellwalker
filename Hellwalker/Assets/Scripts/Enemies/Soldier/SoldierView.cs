using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierView : ObjectView
{
    [Header("References")]
    [SerializeField] private DestructibleObjectScript destructible;
    public override void OnCreate(Context context)
    {
        this.gameObject.name = "Soldier " + gameObject.GetInstanceID();
        destructible.myhealth = 50;
    }

    // Start is called before the first frame update
    void Awake()
    {
        destructible = this.GetComponent<DestructibleObjectScript>();
    }

    /// <summary>
    /// Kill this object
    /// </summary>
    public void Suicide()
    {
        destructible.myhealth = 0;
    }
}
