using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Idle Action")]
public class IdleAction : Action {

    public override void Act(Enemy controller)
    {
        if (!controller.initialized)
        {
            Debug.Log("CALL ONCE");
            controller.initialized = true;
            controller.PlayIdle();
        }
    }

}
