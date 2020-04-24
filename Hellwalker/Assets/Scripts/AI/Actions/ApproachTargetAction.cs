using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Approach Action")]
public class ApproachTargetAction : Action
{
    public override void Act(Enemy controller)
    {
        if(!controller.initialized)
        {
            controller.initialized = true;
            controller.PlayWalking();
            controller.GoToTarget();
        }
    }

    void WalkToTarget(Enemy controller)
    {
       
    }
}
