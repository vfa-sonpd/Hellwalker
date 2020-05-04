using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject {

    public abstract void Act(Enemy controller);
    public virtual void OnInterrupt(Enemy controller)
    {

    }
}
